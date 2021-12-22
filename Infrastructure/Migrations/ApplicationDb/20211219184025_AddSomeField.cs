using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class AddSomeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContinuousBaseSalaryAndBaseYears",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContinuousBasicRightsToHousingAndChildrenRights",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonContinuousIncluded",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonContinuousIncludedNotIncluded",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContinuousBaseSalaryAndBaseYears",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "ContinuousBasicRightsToHousingAndChildrenRights",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "NonContinuousIncluded",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "NonContinuousIncludedNotIncluded",
                schema: "Data",
                table: "TbImported");
        }
    }
}
