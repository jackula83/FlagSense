using FlagService.Core.Auditing;
using Framework2.Infra.Data.Entity;
using Framework2.Infra.Data.Repository;

namespace FlagService.Infra.Data.Abstracts
{
    public abstract class FsRepository<TAggregateRoot> : FxRepository<FsSqlServerContext, TAggregateRoot>
       where TAggregateRoot : class, IAggregateRoot
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

        public override async Task<TAggregateRoot> Add(TAggregateRoot entity)
        {
            var savedEntity = await base.Add(entity);

            // write audit entries
            if (entity != null)
                await _auditOperations.AddAuditEntry(default, savedEntity!);
            return savedEntity;
        }

        public override async Task<TAggregateRoot?> Update(TAggregateRoot entity)
        {
            var oldEntity = await Get(entity.Id);
            if (oldEntity == default)
                return default;

            var savedEntity = await base.Update(entity);

            // write audit entries
            if (savedEntity == default)
                return default;

            if (entity != null)
                await _auditOperations.AddAuditEntry(oldEntity, savedEntity!);

            return savedEntity;
        }
    }
}
