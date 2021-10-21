using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskAssign.Migrations.EmpTask
{
    public partial class TaskModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "EmpTask");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "EmpTask",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "EmpTask");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "EmpTask",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
