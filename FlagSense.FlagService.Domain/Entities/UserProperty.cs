using FlagSense.FlagService.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlagSense.FlagService.Domain.Entities
{
    public class UserProperty : FsEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Key { get; set; } = string.Empty;
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
