using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddSomeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<bool>(
                name: "IsInsurance",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RowOfCovenant",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxAuthorityCode",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxAuthorityName",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkshopCode",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkshopName",
                schema: "Basic",
                table: "TbProject",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInsurance",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RowOfCovenant",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "TaxAuthorityCode",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "TaxAuthorityName",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "WorkshopCode",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "WorkshopName",
                schema: "Basic",
                table: "TbProject");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}