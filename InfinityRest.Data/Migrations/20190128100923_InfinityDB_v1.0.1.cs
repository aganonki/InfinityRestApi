using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfinityRest.Data.Migrations
{
    public partial class InfinityDB_v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Runs_RunId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "RunId",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Runs",
                columns: new[] { "Id", "Date", "Priority" },
                values: new object[] { 1, new DateTime(2019, 1, 28, 0, 0, 0, 0, DateTimeKind.Local), 100 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Name", "ProcessStateId", "RunId", "TaskSettingsId", "TaskTypeId" },
                values: new object[] { 1, "mockTest", 10, 1, null, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Runs_RunId",
                table: "Tasks",
                column: "RunId",
                principalTable: "Runs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Runs_RunId",
                table: "Tasks");

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "RunId",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Runs_RunId",
                table: "Tasks",
                column: "RunId",
                principalTable: "Runs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
