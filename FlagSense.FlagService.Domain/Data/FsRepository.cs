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
        protected FsRepository(TContext context) : base(context)
        {
        }

        public override async Task<int> Add(TEntity entity)
        {
            var id = await base.Add(entity);

            // write audit entries
            if (entity as IAuditable != null)
            {
                var savedEntity = await this.Get(id);
                await _context.AddAuditEntry(default, savedEntity!);
            }
            return id;
        }

        public override async Task<bool> Update(TEntity entity)
        {
            var oldEntity = await this.Get(entity.Id);
            if (oldEntity == default)
                return false;

            var success = await base.Update(entity);

            // write audit entries
            if (!success)
                return false;

            if (entity as IAuditable != null)
            {
                var savedEntity = await this.Get(entity.Id);
                await _context.AddAuditEntry(oldEntity, savedEntity!);
            }

            return success;
        }
    }
}
