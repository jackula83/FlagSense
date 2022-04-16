using Common.Infra.RDBMS;
using FlagSense.FlagService.Core.Models;
using FlagSense.FlagService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Domain.Data
{
    /// <summary>
    /// Context for SqlServer, use Context Factory to support different database providers
    /// </summary>
    public class FsDbContext : FxSqlServerDbContext
    {
        public DbSet<Env> Environments { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<Entities.Rule> Rules { get; set; }
        public DbSet<RuleGroup> RuleGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }

#pragma warning disable 8618
        public FsDbContext(DbContextOptions options) : base(options)
        {
        }
#pragma warning restore 8618

        protected override void Setup<TEntityType>(ModelBuilder builder)
        {
            base.Setup<TEntityType>(builder);

            var model = Activator.CreateInstance<TEntityType>() as FsEntity;
            model!.Setup(builder);
        }
    }
}