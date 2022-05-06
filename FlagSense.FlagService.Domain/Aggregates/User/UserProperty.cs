using FlagService.Infra.Data.Abstracts;
using Framework2.Infra.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlagSense.FlagService.Domain.Entities
{
    public class UserProperty : FsDataObject
    {
        #region EF Relationships
        public int UserId { get; set; }
        public User? User { get; set; }
        #endregion

        [StringLength(0x200)]
        public string Key { get; set; } = string.Empty;
        [MaxLength]
        public string Value { get; set; } = string.Empty;

        public UserProperty() { }

        public UserProperty(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<UserProperty>();
            entity
                .HasOne(x => x.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(nameof(UserId));
        }
    }
}
