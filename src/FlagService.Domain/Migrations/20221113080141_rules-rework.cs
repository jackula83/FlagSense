using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlagService.Domain.Migrations
{
    public partial class rulesrework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_RuleGroup_RuleGroupId",
                table: "Rule");

            migrationBuilder.DropTable(
                name: "RuleGroup");

            migrationBuilder.DropIndex(
                name: "IX_Rule_RuleGroupId",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "RuleGroupId",
                table: "Rule");

            migrationBuilder.RenameIndex(
                name: "idx_uuid7",
                table: "UserProperty",
                newName: "idx_uuid4");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag7",
                table: "UserProperty",
                newName: "idx_deleteFlag4");

            migrationBuilder.RenameIndex(
                name: "idx_uuid6",
                table: "User",
                newName: "idx_uuid3");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag6",
                table: "User",
                newName: "idx_deleteFlag3");

            migrationBuilder.RenameIndex(
                name: "idx_uuid5",
                table: "Segment",
                newName: "idx_uuid2");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag5",
                table: "Segment",
                newName: "idx_deleteFlag2");

            migrationBuilder.RenameColumn(
                name: "RuleType",
                table: "Rule",
                newName: "FlagId");

            migrationBuilder.RenameIndex(
                name: "idx_uuid4",
                table: "Rule",
                newName: "idx_uuid6");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag4",
                table: "Rule",
                newName: "idx_deleteFlag6");

            migrationBuilder.RenameIndex(
                name: "idx_uuid3",
                table: "Condition",
                newName: "idx_uuid5");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag3",
                table: "Condition",
                newName: "idx_deleteFlag5");

            migrationBuilder.AddColumn<int>(
                name: "SegmentId",
                table: "Rule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Rule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeName",
                table: "Condition",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Operator",
                table: "Condition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rule_FlagId",
                table: "Rule",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_Rule_SegmentId",
                table: "Rule",
                column: "SegmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Flag_FlagId",
                table: "Rule",
                column: "FlagId",
                principalTable: "Flag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_Segment_SegmentId",
                table: "Rule",
                column: "SegmentId",
                principalTable: "Segment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Flag_FlagId",
                table: "Rule");

            migrationBuilder.DropForeignKey(
                name: "FK_Rule_Segment_SegmentId",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_FlagId",
                table: "Rule");

            migrationBuilder.DropIndex(
                name: "IX_Rule_SegmentId",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Rule");

            migrationBuilder.DropColumn(
                name: "AttributeName",
                table: "Condition");

            migrationBuilder.DropColumn(
                name: "Operator",
                table: "Condition");

            migrationBuilder.RenameIndex(
                name: "idx_uuid4",
                table: "UserProperty",
                newName: "idx_uuid7");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag4",
                table: "UserProperty",
                newName: "idx_deleteFlag7");

            migrationBuilder.RenameIndex(
                name: "idx_uuid3",
                table: "User",
                newName: "idx_uuid6");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag3",
                table: "User",
                newName: "idx_deleteFlag6");

            migrationBuilder.RenameIndex(
                name: "idx_uuid2",
                table: "Segment",
                newName: "idx_uuid5");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag2",
                table: "Segment",
                newName: "idx_deleteFlag5");

            migrationBuilder.RenameColumn(
                name: "FlagId",
                table: "Rule",
                newName: "RuleType");

            migrationBuilder.RenameIndex(
                name: "idx_uuid6",
                table: "Rule",
                newName: "idx_uuid4");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag6",
                table: "Rule",
                newName: "idx_deleteFlag4");

            migrationBuilder.RenameIndex(
                name: "idx_uuid5",
                table: "Condition",
                newName: "idx_uuid3");

            migrationBuilder.RenameIndex(
                name: "idx_deleteFlag5",
                table: "Condition",
                newName: "idx_deleteFlag3");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Rule",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RuleGroupId",
                table: "Rule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RuleGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlagId = table.Column<int>(type: "int", nullable: true),
                    SegmentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    ServeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Rule_RuleGroupId",
                table: "Rule",
                column: "RuleGroupId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Rule_RuleGroup_RuleGroupId",
                table: "Rule",
                column: "RuleGroupId",
                principalTable: "RuleGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
