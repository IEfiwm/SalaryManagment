using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class NewFieldsInImported : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bonuses",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Deposit5Percent",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoodPerformance10Percent",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Insurance23Percent",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Insured",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Leave",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LifeAndAccidents",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OverHead",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pure",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sum",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SumWithInsurance",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplementaryInsurance",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Total",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalBonusesAndLeave",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalWithoutBonusesAndLeave",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VAT",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValueAddedAggregates",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValueAddedInsurance",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonuses",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "Deposit5Percent",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "GoodPerformance10Percent",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "Insurance23Percent",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "Insured",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "Leave",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "LifeAndAccidents",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "OverHead",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "Pure",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "Sum",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "SumWithInsurance",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "SupplementaryInsurance",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "Total",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "TotalBonusesAndLeave",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "TotalWithoutBonusesAndLeave",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "VAT",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "ValueAddedAggregates",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "ValueAddedInsurance",
                schema: "Data",
                table: "TbImported");
        }
    }
}
