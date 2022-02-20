using Common.Domain.Core.Data;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagSense.FlagService.Domain.Data
{
    public abstract class FsRepository<TContext, TEntity> : FxEntityRepository<TContext, TEntity>
       where TContext : FsContext
       where TEntity : FsEntity
    {
        private readonly AuditOperations _auditOperations;

        protected FsRepository(TContext context, AuditOperations auditOperations) 
            : base(context)
        {
            _auditOperations = auditOperations;
        }

        public override async Task<TEntity> Add(TEntity entity)
        {
            var savedEntity = await base.Add(entity);

            // write audit entries
            if (entity as IAuditable != null)
            {
                await _auditOperations.AddAuditEntry(default, savedEntity!);
            }
            return savedEntity;
        }

        public override async Task<TEntity?> Update(TEntity entity)
        {
            var oldEntity = await this.Get(entity.Id);
            if (oldEntity == default)
                return default;

            var savedEntity = await base.Update(entity);

            // write audit entries
            if (savedEntity == default)
                return default;

            if (entity as IAuditable != null)
                await _auditOperations.AddAuditEntry(oldEntity, savedEntity!);

            return savedEntity;
        }
    }
}
