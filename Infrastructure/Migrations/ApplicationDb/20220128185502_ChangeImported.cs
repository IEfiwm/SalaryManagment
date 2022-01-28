using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class ChangeImported : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "DegreeEducation",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "InsuranceNumber",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "PersonnelCode",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.RenameColumn(
                name: "ServiceLocation",
                schema: "Data",
                table: "TbImported",
                newName: "Monthly");

            migrationBuilder.RenameColumn(
                name: "Province",
                schema: "Data",
                table: "TbImported",
                newName: "Daily");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Monthly",
                schema: "Data",
                table: "TbImported",
                newName: "ServiceLocation");

            migrationBuilder.RenameColumn(
                name: "Daily",
                schema: "Data",
                table: "TbImported",
                newName: "Province");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DegreeEducation",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceNumber",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonnelCode",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
