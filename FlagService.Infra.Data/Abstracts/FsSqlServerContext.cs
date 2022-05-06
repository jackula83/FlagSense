using FlagService.Infra.Data.Abstracts;
using Framework2.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FlagSense.FlagService.Domain.Data
{
    public class FsSqlServerContext : FxSqlServerDbContext
    {
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