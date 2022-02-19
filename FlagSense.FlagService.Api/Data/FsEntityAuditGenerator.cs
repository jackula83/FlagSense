using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FlagSense.FlagService.Api.Data
{
    public class FsEntityAuditGenerator : CSharpMigrationOperationGenerator
    {
        public FsEntityAuditGenerator(CSharpMigrationOperationGeneratorDependencies  dependencies)
            : base(dependencies)
        {

        }

        protected override void Generate(CreateTableOperation operation, IndentedStringBuilder builder)
        {
            base.Generate(operation, builder);

            var tableName = operation.Name;
            var schemaName = "audit";

            var auditMigrationCode = $@";
migrationBuilder.CreateTable(
    name: ""{tableName}"",
    schema: ""{schemaName}"",
    columns: table => new
    {{
        Id = table.Column<int>(type: ""int"", nullable: false)
            .Annotation(""SqlServer:Identity"", ""1, 1""),
        Uuid = table.Column<Guid>(type: ""uniqueidentifier"", nullable: false),
        Old = table.Column<string>(type: ""nvarchar(max)"", nullable: true),
        New = table.Column<string>(type: ""nvarchar(max)"", nullable: false),
        CreatedAt = table.Column<DateTime>(type: ""datetime2"", nullable: false)
    }},
    constraints: table =>
    {{
        table.PrimaryKey(""PK_Audit_{tableName}"", x => x.Id);
    }});
            ";

            var auditMigrationIndex = $@"
migrationBuilder.CreateIndex(
    name: ""IX_Audit_{tableName}_Uuid"",
    schema: ""{schemaName}"",
    table: ""Segments"",
    column: ""Uuid"")
    .Annotation(""SqlServer:Include"", new[] {{ ""Id"" }})
            ";

            builder.AppendLines(auditMigrationCode, true);
            builder.AppendLines(auditMigrationIndex, true);
        }
    }
}
