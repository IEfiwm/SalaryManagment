using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class ChangeImportedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmenitiesF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "LoanF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "OtherIncludeInsuranceAndNotIncludeTaxF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "OtherNotIncludeInsuranceAndIncludeTaxF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "OtherNotIncludeInsuranceAndTaxF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "PerformanceF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "RewardF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "TemporaryF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "TransferPayF",
                schema: "Data",
                table: "TbImported");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmenitiesF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LoanF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherIncludeInsuranceAndNotIncludeTaxF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherNotIncludeInsuranceAndIncludeTaxF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherNotIncludeInsuranceAndTaxF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerformanceF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RewardF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TemporaryF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransferPayF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
