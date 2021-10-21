using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskAssign.Migrations
{
    public partial class FieldAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "EmpAssign",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "EmpAssign");
        }
    }
}
