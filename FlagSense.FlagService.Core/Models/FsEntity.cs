using Common.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Core.Models
{
    [Index(nameof(CreatedAt), Name = "idx_createdAt")]
    [Index(nameof(UpdatedAt), Name = "idx_updatedAt")]
    [Index(nameof(CreatedBy), Name = "idx_createdBy")]
    [Index(nameof(UpdatedBy), Name = "idx_updatedBy")]
    public abstract class FsEntity : FxEntity
    {
        public abstract void SetupEntity(ModelBuilder builder);
    }
}
