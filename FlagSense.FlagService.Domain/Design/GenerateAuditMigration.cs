using FlagSense.FlagService.Domain.Data;
using System.Text.RegularExpressions;

namespace FlagSense.FlagService.Domain.Design
{
    public static class GenerateAuditMigration
    {
        public static string TableGeneration(string tableName)
            /// we do this to remove indenting added to <see cref="TableGenerationTemplate(string, string)"/>, making it easier to read
            => Regex.Replace(TableGenerationTemplate(tableName, FsContext.AuditSchema), @"^\s{12}", string.Empty, RegexOptions.Multiline);
        public static string TableOptimisation(string tableName)
            /// we do this to remove indenting added to <see cref="TableOptimisationTemplate(string, string)"/>, making it easier to read
            => Regex.Replace(TableOptimisationTemplate(tableName, FsContext.AuditSchema), @"^\s{12}", string.Empty, RegexOptions.Multiline);

        private static string TableGenerationTemplate(string tableName, string schemaName) => $@";
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
                    CreatedAt = table.Column<DateTime>(type: ""datetime2"", nullable: false),
                    CreatedBy = table.Column<string>(type: ""nvarchar(512)"", nullable: false)
                }},
                constraints: table =>
                {{
                    table.PrimaryKey(""PK_Audit_{tableName}"", x => x.Id);
                }});
            ";

        private static string TableOptimisationTemplate(string tableName, string schemaName) => $@"
            migrationBuilder.CreateIndex(
                name: ""IX_Audit_{tableName}_Uuid"",
                schema: ""{schemaName}"",
                table: ""Segments"",
                column: ""Uuid"")
                .Annotation(""SqlServer:Include"", new[] {{ ""Id"" }});
            ";
    }
}
