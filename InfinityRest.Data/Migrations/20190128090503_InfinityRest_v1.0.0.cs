using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfinityRest.Data.Migrations
{
    public partial class InfinityRest_v100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Runs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Priority = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskRunTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskRunTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Integer = table.Column<int>(nullable: false),
                    ExampleBool = table.Column<bool>(nullable: false),
                    FolderName = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    RunTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSettings_TaskRunTypes_RunTypeId",
                        column: x => x.RunTypeId,
                        principalTable: "TaskRunTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TaskTypeId = table.Column<int>(nullable: false),
                    ProcessStateId = table.Column<int>(nullable: false),
                    TaskSettingsId = table.Column<int>(nullable: true),
                    RunId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_ProcessStates_ProcessStateId",
                        column: x => x.ProcessStateId,
                        principalTable: "ProcessStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Runs_RunId",
                        column: x => x.RunId,
                        principalTable: "Runs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskSettings_TaskSettingsId",
                        column: x => x.TaskSettingsId,
                        principalTable: "TaskSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProcessStates",
                columns: new[] { "Id", "State" },
                values: new object[,]
                {
                    { 0, "Waiting" },
                    { 10, "Processing" },
                    { 100, "Complete" }
                });

            migrationBuilder.InsertData(
                table: "TaskRunTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Parallel" },
                    { 2, "Sync" },
                    { 3, "Default" }
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Formatting" },
                    { 2, "Moving" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProcessStateId",
                table: "Tasks",
                column: "ProcessStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_RunId",
                table: "Tasks",
                column: "RunId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskSettingsId",
                table: "Tasks",
                column: "TaskSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskTypeId",
                table: "Tasks",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSettings_RunTypeId",
                table: "TaskSettings",
                column: "RunTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "ProcessStates");

            migrationBuilder.DropTable(
                name: "Runs");

            migrationBuilder.DropTable(
                name: "TaskSettings");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "TaskRunTypes");
        }
    }
}
