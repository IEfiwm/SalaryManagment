using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class attendanceProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<int>(
                name: "AbsenceTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HolidayWorkingTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MissionTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                schema: "Data",
                table: "TbAttendance",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NightWorkingTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OvertimeWorkingTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RightLeaveTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShiftWork",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SickLeaveTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkingDays",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsenceTime",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "FoodTime",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "HolidayWorkingTime",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "MissionTime",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "NationalCode",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "NightWorkingTime",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "OvertimeWorkingTime",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "RightLeaveTime",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "ShiftWork",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "SickLeaveTime",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "WorkingDays",
                schema: "Data",
                table: "TbAttendance");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}
