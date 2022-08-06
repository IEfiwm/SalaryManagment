using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddColumnToFieldEntity02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DelayedSupplementaryInsuranceDeduction",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LifeAndAccidents",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelayedSupplementaryInsuranceDeduction",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "LifeAndAccidents",
                schema: "Data",
                table: "TbAttendance");
        }
    }
}
