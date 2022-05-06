using Common.Domain.Core.Data;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Interfaces;

namespace FlagSense.FlagService.Domain.Data
{
    public abstract class FsRepository<TEntity> : FxEntityRepository<FsSqlServerContext, TEntity>
       where TEntity : FsEntity
    {
        /// <summary>
        /// Move AuditOperations to Shared lib, auditing should occur in <see cref="FxEntityRepository{TContext, TEntity}"/>
        /// </summary>
        private readonly AuditOperations _auditOperations;

        protected FsRepository(FsSqlServerContext context, AuditOperations auditOperations) 
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
