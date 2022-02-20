using FlagSense.FlagService.Domain.Data;
using System.Text.RegularExpressions;

namespace FlagSense.FlagService.Domain.Design
{
    public static class GenerateAuditMigration
    {
        public static string TableGeneration(string tableName)
            /// we do this to remove indenting added to <see cref="TableGenerationTemplate(string, string)"/>, making it easier to read
            => Regex.Replace(TableGenerationTemplate(tableName, AuditOperations.AuditSchema), @"^\s{12}", string.Empty, RegexOptions.Multiline);
        public static string TableOptimisation(string tableName)
            /// we do this to remove indenting added to <see cref="TableOptimisationTemplate(string, string)"/>, making it easier to read
            => Regex.Replace(TableOptimisationTemplate(tableName, AuditOperations.AuditSchema), @"^\s{12}", string.Empty, RegexOptions.Multiline);

        private static string TableGenerationTemplate(string tableName, string schemaName) => $@";
            migrationBuilder.CreateTable(
                name: ""{tableName}"",
                schema: ""{schemaName}"",
                columns: table => new
                {{
                    Id = table.Column<int>(type: ""int"", nullable: false)
                        .Annotation(""SqlServer:Identity"", ""1, 1""),
                    RefId = table.Column<int>(type: ""int"", nullable: false),
                    Uuid = table.Column<Guid>(type: ""uniqueidentifier"", nullable: false),
                    Old = table.Column<string>(type: ""nvarchar(max)"", nullable: true),
                    New = table.Column<string>(type: ""nvarchar(max)"", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: ""datetime2"", nullable: false),
                    CreatedBy = table.Column<string>(type: ""nvarchar(512)"", nullable: false)
                }},
                constraints: table =>
                {{
                    table.PrimaryKey(""PK_Audit_{tableName}"", x => x.Id);
                    table.ForeignKey(
                        name: ""FK_Audit{tableName}_{tableName}_RefId"",
                        column: x => x.RefId,
                        principalTable: ""{tableName}"",
                        principalColumn: ""Id"",
                        onDelete: ReferentialAction.Cascade);
                }});
            ";

        private static string TableOptimisationTemplate(string tableName, string schemaName) => $@"
            migrationBuilder.CreateIndex(
                name: ""IX_Audit_{tableName}_Uuid"",
                schema: ""{schemaName}"",
                table: ""{tableName}"",
                column: ""Uuid"")
                .Annotation(""SqlServer:Include"", new[] {{ ""Id"" }});
            migrationBuilder.CreateIndex(
                name: ""IX_Audit_{tableName}_CreatedAt"",
                schema: ""{schemaName}"",
                table: ""{tableName}"",
                column: ""CreatedAt"")
                .Annotation(""SqlServer:Include"", new[] {{ ""Id"", ""Uuid"", ""RefId"", ""Old"", ""New"", ""CreatedBy"" }});
            migrationBuilder.CreateIndex(
                name: ""IX_Audit_{tableName}_RefId"",
                schema: ""{schemaName}"",
                table: ""{tableName}"",
                column: ""RefId"")
                .Annotation(""SqlServer:Include"", new[] {{ ""Id"", ""Uuid"", ""Old"", ""New"", ""CreatedAt"", ""CreatedBy"" }});
            ";
    }
}
