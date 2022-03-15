using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ProjectNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyOwnerName",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyRegistrationCode",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyAddress",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "CompanyOwnerName",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "CompanyRegistrationCode",
                schema: "Basic",
                table: "TbProject");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}
