using FlagService.Core.Models;
using FlagService.Domain.Aggregates;
using FlagService.Domain.Aggregates.Users;
using FlagService.Domain.Entities.Rules;
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
        public DbSet<User> User { get; set; }
        public DbSet<UserAttribute> UserProperty { get; set; }


#pragma warning disable 8618
        public FsSqlServerContext(DbContextOptions options) : base(options)
        {
        }
#pragma warning restore

        protected override void Setup<TEntityType>(ModelBuilder builder)
        {
            base.Setup<TEntityType>(builder);

            var model = Activator.CreateInstance<TEntityType>() as FsDataObject;
            model!.Setup(builder);
        }
    }
}