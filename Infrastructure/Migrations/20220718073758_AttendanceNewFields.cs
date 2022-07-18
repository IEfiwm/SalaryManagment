using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AttendanceNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<int>(
                name: "ClothesPay",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourtOrderDeductions",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FixedAmenitiesPay",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FixedHolidayPay",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FixedOvertimePay",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FixedTransferPay",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodPay",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodWorkDays",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FridayWorkDays",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Guard",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Help",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Loan",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages1",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages10",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages2",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages3",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages4",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages5",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages6",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages7",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages8",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherAdvantages9",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions1",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions10",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions2",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions3",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions4",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions5",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions6",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions7",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions8",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherDeductions9",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherIncludeInsuranceAndNotIncludeTax",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherIncludeInsuranceAndTax",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherNotIncludeInsuranceAndIncludeTax",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherNotIncludeInsuranceAndTax",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OvertimeWorking",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Performance",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Reward",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ServiceLocation",
                schema: "Data",
                table: "TbAttendance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceProvince",
                schema: "Data",
                table: "TbAttendance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShiftWorkPercentage",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShiftWorking",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplementaryInsurance",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplementaryInsuranceChildren",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TemporaryPay",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransferByProvinceWorkDays",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransferWorkDays",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViolationsDeductions",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClothesPay",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "CourtOrderDeductions",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "FixedAmenitiesPay",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "FixedHolidayPay",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "FixedOvertimePay",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "FixedTransferPay",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "FoodPay",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "FoodWorkDays",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "FridayWorkDays",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "Guard",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "Help",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "Loan",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages1",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages10",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages2",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages3",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages4",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages5",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages6",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages7",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages8",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherAdvantages9",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions1",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions10",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions2",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions3",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions4",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions5",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions6",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions7",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions8",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherDeductions9",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherIncludeInsuranceAndNotIncludeTax",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherIncludeInsuranceAndTax",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherNotIncludeInsuranceAndIncludeTax",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OtherNotIncludeInsuranceAndTax",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OvertimeWorking",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "Performance",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "Reward",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "ServiceLocation",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "ServiceProvince",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "ShiftWorkPercentage",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "ShiftWorking",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "SupplementaryInsurance",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "SupplementaryInsuranceChildren",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "TemporaryPay",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "TransferByProvinceWorkDays",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "TransferWorkDays",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "ViolationsDeductions",
                schema: "Data",
                table: "TbAttendance");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}
