using FlagService.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlagService.Domain.Aggregates.Users
{
    public class UserAttribute : FsDataObject
    {
        #region EF Relationships
        public int UserId { get; set; }
        public User? User { get; set; }
        #endregion

        [StringLength(0x200)]
        public string Key { get; set; } = string.Empty;
        [MaxLength]
        public string Value { get; set; } = string.Empty;

        public UserAttribute() { }

        public UserAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<UserAttribute>();
            entity
                .HasOne(x => x.User)
                .WithMany(u => u.Attributes)
                .HasForeignKey(nameof(UserId));
        }
    }
}
