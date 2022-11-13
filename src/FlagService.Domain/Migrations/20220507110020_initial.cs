using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlagService.Domain.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Env",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColourCoding = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65536, nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Env", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Segment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnvironmentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65536, nullable: false),
                    ColourCoding = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    DefaultServeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segment_Env_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Env",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProperty_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SegmentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 65536, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", maxLength: 65536, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    DefaultServeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flag", x => x.Id);
                    table.UniqueConstraint("idx_Environment_FlagName", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Flag_Segment_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RuleGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SegmentId = table.Column<int>(type: "int", nullable: true),
                    FlagId = table.Column<int>(type: "int", nullable: true),
                    ServeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuleGroup_Flag_FlagId",
                        column: x => x.FlagId,
                        principalTable: "Flag",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RuleGroup_Segment_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleGroupId = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    RuleType = table.Column<int>(type: "int", nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rule_RuleGroup_RuleGroupId",
                        column: x => x.RuleGroupId,
                        principalTable: "RuleGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 65536, nullable: false),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Condition_Rule_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_deleteFlag3",
                table: "Condition",
                column: "DeleteFlag");

            migrationBuilder.CreateIndex(
                name: "idx_uuid3",
                table: "Condition",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Condition_RuleId",
                table: "Condition",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "idx_deleteFlag",
                table: "Env",
                column: "DeleteFlag");

            migrationBuilder.CreateIndex(
                name: "idx_uuid",
                table: "Env",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Env_CreatedBy_CreatedAt",
                table: "Env",
                columns: new[] { "CreatedBy", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Env_UpdatedBy_UpdatedAt",
                table: "Env",
                columns: new[] { "UpdatedBy", "UpdatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Env_Uuid",
                table: "Env",
                column: "Uuid")
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "idx_deleteFlag1",
                table: "Flag",
                column: "DeleteFlag");

            migrationBuilder.CreateIndex(
                name: "idx_uuid1",
                table: "Flag",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Flag_CreatedBy_CreatedAt",
                table: "Flag",
                columns: new[] { "CreatedBy", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Flag_SegmentId",
                table: "Flag",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Flag_UpdatedBy_UpdatedAt",
                table: "Flag",
                columns: new[] { "UpdatedBy", "UpdatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Flag_Uuid",
                table: "Flag",
                column: "Uuid")
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "idx_deleteFlag4",
                table: "Rule",
                column: "DeleteFlag");

            migrationBuilder.CreateIndex(
                name: "idx_uuid4",
                table: "Rule",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_CreatedBy_CreatedAt",
                table: "Rule",
                columns: new[] { "CreatedBy", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Rule_RuleGroupId",
                table: "Rule",
                column: "RuleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_UpdatedBy_UpdatedAt",
                table: "Rule",
                columns: new[] { "UpdatedBy", "UpdatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Rule_Uuid",
                table: "Rule",
                column: "Uuid")
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "idx_deleteFlag2",
                table: "RuleGroup",
                column: "DeleteFlag");

            migrationBuilder.CreateIndex(
                name: "idx_uuid2",
                table: "RuleGroup",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_RuleGroup_CreatedBy_CreatedAt",
                table: "RuleGroup",
                columns: new[] { "CreatedBy", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RuleGroup_FlagId",
                table: "RuleGroup",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_RuleGroup_SegmentId",
                table: "RuleGroup",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RuleGroup_UpdatedBy_UpdatedAt",
                table: "RuleGroup",
                columns: new[] { "UpdatedBy", "UpdatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_RuleGroup_Uuid",
                table: "RuleGroup",
                column: "Uuid")
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "idx_deleteFlag5",
                table: "Segment",
                column: "DeleteFlag");

            migrationBuilder.CreateIndex(
                name: "idx_uuid5",
                table: "Segment",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_Segment_CreatedBy_CreatedAt",
                table: "Segment",
                columns: new[] { "CreatedBy", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Segment_EnvironmentId",
                table: "Segment",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Segment_UpdatedBy_UpdatedAt",
                table: "Segment",
                columns: new[] { "UpdatedBy", "UpdatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Segment_Uuid",
                table: "Segment",
                column: "Uuid")
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "idx_deleteFlag6",
                table: "User",
                column: "DeleteFlag");

            migrationBuilder.CreateIndex(
                name: "idx_uuid6",
                table: "User",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_User_CreatedBy_CreatedAt",
                table: "User",
                columns: new[] { "CreatedBy", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedBy_UpdatedAt",
                table: "User",
                columns: new[] { "UpdatedBy", "UpdatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Uuid",
                table: "User",
                column: "Uuid")
                .Annotation("SqlServer:Include", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "idx_deleteFlag7",
                table: "UserProperty",
                column: "DeleteFlag");

            migrationBuilder.CreateIndex(
                name: "idx_uuid7",
                table: "UserProperty",
                column: "Uuid");

            migrationBuilder.CreateIndex(
                name: "IX_UserProperty_CreatedBy_CreatedAt",
                table: "UserProperty",
                columns: new[] { "CreatedBy", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProperty_UpdatedBy_UpdatedAt",
                table: "UserProperty",
                columns: new[] { "UpdatedBy", "UpdatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserProperty_UserId",
                table: "UserProperty",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProperty_Uuid",
                table: "UserProperty",
                column: "Uuid")
                .Annotation("SqlServer:Include", new[] { "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "UserProperty");

            migrationBuilder.DropTable(
                name: "Rule");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RuleGroup");

            migrationBuilder.DropTable(
                name: "Flag");

            migrationBuilder.DropTable(
                name: "Segment");

            migrationBuilder.DropTable(
                name: "Env");
        }
    }
}
