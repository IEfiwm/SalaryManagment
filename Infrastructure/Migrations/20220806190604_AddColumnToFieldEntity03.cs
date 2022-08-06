using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddColumnToFieldEntity03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LifeAndAccidents",
                schema: "Data",
                table: "TbAttendance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LifeAndAccidents",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
