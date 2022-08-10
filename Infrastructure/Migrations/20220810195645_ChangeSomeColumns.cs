using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeSomeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.RenameColumn(
                name: "ShiftWorking",
                schema: "Data",
                table: "TbAttendance",
                newName: "ShiftWorkTime");

            migrationBuilder.AddColumn<int>(
                name: "ShiftTime",
                schema: "Data",
                table: "TbAttendance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShiftTime",
                schema: "Data",
                table: "TbAttendance");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");

            migrationBuilder.RenameColumn(
                name: "ShiftWorkTime",
                schema: "Data",
                table: "TbAttendance",
                newName: "ShiftWorking");
        }
    }
}
