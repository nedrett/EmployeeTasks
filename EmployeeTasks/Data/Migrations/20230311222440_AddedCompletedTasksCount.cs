using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeTasks.Data.Migrations
{
    public partial class AddedCompletedTasksCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedTasksCount",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedTasksCount",
                table: "Employees");
        }
    }
}
