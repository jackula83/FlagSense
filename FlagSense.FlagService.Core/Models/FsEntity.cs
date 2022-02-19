using Common.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Core.Models
{
    public abstract class FsEntity : FxEntity
    {
        public abstract void SetupEntity(ModelBuilder builder);

        public virtual void Setup(ModelBuilder builder)
        {
            var entity = builder.Entity(this.GetType());
            entity
                .HasAlternateKey(nameof(this.CreatedBy), nameof(this.UpdatedBy))
                .HasName($"idx_{this.GetType().Name}_AuditBy");
            entity
                .HasAlternateKey(nameof(this.CreatedAt), nameof(this.UpdatedAt))
                .HasName($"idx_{this.GetType().Name}_AuditAt");

            this.SetupEntity(builder);
        }
    }
}
