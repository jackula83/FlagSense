using Common.Domain.Core.Data;
using FlagSense.FlagService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Domain.Data
{
    public class FsContext : FxSqlServerDbContext
    {
        public DbSet<FsEnvironment> Environments { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<FlagValue> FlagValues { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<RuleGroup> RuleGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }

#pragma warning disable 8618
        public FsContext(DbContextOptions options) : base(options)
        {
        }
#pragma warning restore 8618
    }
}
