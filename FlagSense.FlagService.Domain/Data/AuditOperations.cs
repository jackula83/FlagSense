using Common.Domain.Core.Interfaces;
using FlagSense.FlagService.Core.Events;
using FlagSense.FlagService.Core.Extensions;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Entities;
using FlagSense.FlagService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FlagSense.FlagService.Domain.Data
{
    public class AuditOperations
    {
        private readonly IEventQueue _eventQueue;
        private readonly FsDbContext _dbContext;

        public AuditOperations(IEventQueue eventQueue, FsDbContext dbContext)
        {
            _eventQueue = eventQueue;
            _dbContext = dbContext;
        }

        public async Task AddAuditEntry<TEntity>(TEntity? before, TEntity after)
            where TEntity : FsEntity
        {
            var (schema, table) = this.GetTableProperties<TEntity>();
            var auditEvent = new EntityAuditEvent<TEntity>(table, schema)
            {
                OldEntity = before,
                NewEntity = after
            };

            await _eventQueue.Publish(auditEvent);
        }

        private (string, string) GetTableProperties<TEntity>()
            where TEntity : FsEntity
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(TEntity));
            return (entityType?.GetSchema(), entityType?.GetTableName())!;
        }
    }
}
