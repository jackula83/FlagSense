using FlagSense.FlagService.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlagSense.FlagService.Domain.Entities
{
    /// <summary>
    /// Encapsulates user information for use in flag rules
    /// </summary>
    public class User : FsEntity
    {
        public static string AnonymousPropertyKey = "IsAnonymous";
        private static bool DefaultAnonymousSetting = true;

        public int EnvironmentId { get; set; }
        public Env? Environment { get; set; }

        public List<UserProperty> Properties { get; set; } = new() { new(AnonymousPropertyKey, DefaultAnonymousSetting.ToString())};

        /// <summary>
        /// Anonymous users, public users, unregistered users
        /// </summary>
        /// <returns><see cref="DefaultAnonymousSetting"/> by default</returns>
        [NotMapped]
        public bool IsAnonymous
        {
            get
            {
                var anonymousProp = this.Properties.FirstOrDefault(x => string.Compare(x.Key, AnonymousPropertyKey, true) == 0);
                if (anonymousProp == default)
                    return DefaultAnonymousSetting;

                bool returnValue;
                if (bool.TryParse(anonymousProp.Value, out returnValue))
                    return returnValue;

                return DefaultAnonymousSetting;
            }
            set
            {
                this.Properties.RemoveAll(x => string.Compare(x.Key, AnonymousPropertyKey, true) == 0);
                this.Properties.Add(new(AnonymousPropertyKey, value.ToString()));
            }
        }

        public override void SetupEntity(ModelBuilder builder)
        {
            var entity = builder.Entity<User>();
            entity
                .HasMany(e => e.Properties)
                .WithOne(e => e.User);
            entity
                .HasOne(e => e.Environment)
                .WithMany(v => v.Users)
                .HasForeignKey(nameof(EnvironmentId));
        }
    }
}
