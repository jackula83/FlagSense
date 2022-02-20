using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using FlagSense.FlagService.Domain.Design;

namespace FlagSense.FlagService.Api.Data
{
    public class FsEntityAuditGenerator : CSharpMigrationOperationGenerator
    {
        public FsEntityAuditGenerator(CSharpMigrationOperationGeneratorDependencies dependencies)
            : base(dependencies)
        {

        }

        protected override void Generate(CreateTableOperation operation, IndentedStringBuilder builder)
        {
            base.Generate(operation, builder);
            var tableName = operation.Name;

            var auditMigrationCode = GenerateAuditMigration.TableGeneration(tableName);
            var auditMigrationIndex = GenerateAuditMigration.TableOptimisation(tableName);

            builder.AppendLines(auditMigrationCode, true);
            builder.AppendLines(auditMigrationIndex, true);
        }
    }
}
