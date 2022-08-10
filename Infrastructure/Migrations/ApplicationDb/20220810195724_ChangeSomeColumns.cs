using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class ChangeSomeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsenceF",
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
                name: "RightLeaveF",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.RenameColumn(
                name: "ViolationsDeductionsF",
                schema: "Data",
                table: "TbImported",
                newName: "TransferWork");

            migrationBuilder.RenameColumn(
                name: "SupplementaryInsuranceChildrenF",
                schema: "Data",
                table: "TbImported",
                newName: "SickLeave");

            migrationBuilder.RenameColumn(
                name: "SickLeaveF",
                schema: "Data",
                table: "TbImported",
                newName: "Shift");

            migrationBuilder.RenameColumn(
                name: "ShiftWorkingF",
                schema: "Data",
                table: "TbImported",
                newName: "Guard");

            migrationBuilder.RenameColumn(
                name: "ShiftWorkPercentageF",
                schema: "Data",
                table: "TbImported",
                newName: "FridayWorking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransferWork",
                schema: "Data",
                table: "TbImported",
                newName: "ViolationsDeductionsF");

            migrationBuilder.RenameColumn(
                name: "SickLeave",
                schema: "Data",
                table: "TbImported",
                newName: "SupplementaryInsuranceChildrenF");

            migrationBuilder.RenameColumn(
                name: "Shift",
                schema: "Data",
                table: "TbImported",
                newName: "SickLeaveF");

            migrationBuilder.RenameColumn(
                name: "Guard",
                schema: "Data",
                table: "TbImported",
                newName: "ShiftWorkingF");

            migrationBuilder.RenameColumn(
                name: "FridayWorking",
                schema: "Data",
                table: "TbImported",
                newName: "ShiftWorkPercentageF");

            migrationBuilder.AddColumn<int>(
                name: "AbsenceF",
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
                name: "RightLeaveF",
                schema: "Data",
                table: "TbImported",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
