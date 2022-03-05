using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class attendancePropsExtra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AlterColumn<decimal>(
                name: "ShiftWork",
                schema: "Data",
                table: "TbAttendance",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ProjectRef",
                schema: "Data",
                table: "TbAttendance",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "ProjectRef",
                schema: "Data",
                table: "TbAttendance");

            migrationBuilder.DropColumn(
                name: "Year",
                schema: "Data",
                table: "TbAttendance");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftWork",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
