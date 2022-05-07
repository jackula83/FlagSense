using Framework2.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace FlagService.Core.Models
{
    public abstract class FsDataObject : FxDataObject
    {
        public abstract void SetupEntity(ModelBuilder builder);

        public virtual void Setup(ModelBuilder builder)
        {
            var entity = builder.Entity(GetType());
            entity
                .HasIndex(nameof(Uuid))
                .IncludeProperties(nameof(Id));
            entity.HasIndex(nameof(CreatedBy), nameof(CreatedAt));
            entity.HasIndex(nameof(UpdatedBy), nameof(UpdatedAt));

            SetupEntity(builder);
        }
    }
}
