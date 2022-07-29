using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class NewFieldsInImported3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbsenceF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AmenitiesF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourtOrderDeductionsF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FixedOvertimeF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuardF",
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
                name: "RightLeaveF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShiftWorkPercentageF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShiftWorkingF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SickLeaveF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplementaryInsuranceChildrenF",
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

            migrationBuilder.AddColumn<int>(
                name: "ViolationsDeductionsF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsenceF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "AmenitiesF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "CourtOrderDeductionsF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "FixedOvertimeF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "GuardF",
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
                name: "RightLeaveF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "ShiftWorkPercentageF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "ShiftWorkingF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "SickLeaveF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "SupplementaryInsuranceChildrenF",
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

            migrationBuilder.DropColumn(
                name: "ViolationsDeductionsF",
                schema: "Data",
                table: "TbImported");
        }
    }
}
