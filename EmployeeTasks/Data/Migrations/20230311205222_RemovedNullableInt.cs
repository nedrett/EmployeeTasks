using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeTasks.Data.Migrations
{
    public partial class RemovedNullableInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_AssigneeId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "AssigneeId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_AssigneeId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "AssigneeId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
