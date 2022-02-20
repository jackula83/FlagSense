using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Design;

namespace FlagSense.FlagService.Api.Data
{
    public class FsEntityDesignInjections : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICSharpMigrationOperationGenerator, FsEntityAuditGenerator>();
        }
    }
}
