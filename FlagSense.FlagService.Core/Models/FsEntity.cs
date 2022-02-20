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
                .HasIndex(nameof(this.Uuid))
                .IncludeProperties(nameof(this.Id));
            entity.HasIndex(nameof(this.CreatedBy), nameof(this.CreatedAt));
            entity.HasIndex(nameof(this.UpdatedBy), nameof(this.UpdatedAt));

            this.SetupEntity(builder);
        }
    }
}
