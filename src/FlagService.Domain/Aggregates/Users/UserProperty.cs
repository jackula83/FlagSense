using FlagService.Infra.Data.Abstracts;
using FlagService.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlagService.Domain.Aggregates.Users
{
    public class UserProperty : FsDataObject, IUserProperty
    {
        #region EF Relationships
        public int UserId { get; set; }
        public IUser? User { get; set; }
        #endregion

        [StringLength(0x200)]
        public string Key { get; set; } = string.Empty;
        [MaxLength]
        public string Value { get; set; } = string.Empty;

        public UserProperty() { }

        public UserProperty(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<UserProperty>();
            entity
                .HasOne(x => x.User)
                .WithMany(u => u.Properties.Cast<UserProperty>())
                .HasForeignKey(nameof(UserId));
        }
    }
}
