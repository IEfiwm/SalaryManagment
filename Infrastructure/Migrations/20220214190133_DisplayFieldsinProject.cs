using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class DisplayFieldsinProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<string>(
                name: "DisplayAddress",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayEmail",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayPhoneNumber",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayPostalCode",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayAddress",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "DisplayEmail",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "DisplayPhoneNumber",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "DisplayPostalCode",
                schema: "Basic",
                table: "TbProject");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}
