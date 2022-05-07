using FlagService.Domain.Contexts;
using Framework2.Infra.Data.Entity;
using Framework2.Infra.MQ.Core;
using Microsoft.EntityFrameworkCore;

namespace FlagService.Domain.Auditing
{
    public class AuditOperations
    {
        private readonly IEventQueue _eventQueue;
        private readonly DbContext _dbContext;

        public AuditOperations(IEventQueue eventQueue, FsSqlServerContext dbContext)
        {
            _eventQueue = eventQueue;
            _dbContext = dbContext;
        }

        public async Task AddAuditEntry<TAggregateRoot>(TAggregateRoot? before, TAggregateRoot after)
            where TAggregateRoot : IAggregateRoot
        {
            var (schema, table) = GetTableProperties<TAggregateRoot>();
            var auditEvent = new EntityAuditEvent<TAggregateRoot>(table, schema)
            {
                OldAggregate = before,
                NewAggregate = after
            };

            await _eventQueue.Publish(auditEvent);
        }

        private (string, string) GetTableProperties<TAggregateRoot>()
            where TAggregateRoot : IAggregateRoot
        {
            var entityType = _dbContext.Model.FindEntityType(typeof(TAggregateRoot));
            return (entityType?.GetSchema(), entityType?.GetTableName())!;
        }
    }
}
