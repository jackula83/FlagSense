using FlagService.Core.Models;
using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Rules;
using FlagService.Domain.Aggregates.Users;
using Framework2.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FlagService.Domain.Contexts
{
    public class FsSqlServerContext : FxSqlServerDbContext
    {
        public DbSet<Env> Env { get; set; }
        public DbSet<Segment> Segment { get; set; }
        public DbSet<Flag> Flag { get; set; }
        public DbSet<Rule> Rule { get; set; }
        public DbSet<RuleGroup> RuleGroup { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserProperty> UserProperty { get; set; }

        public FsSqlServerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void Setup<TEntityType>(ModelBuilder builder)
        {
            base.Setup<TEntityType>(builder);

            var model = Activator.CreateInstance<TEntityType>() as FsDataObject;
            model!.Setup(builder);
        }
    }
}