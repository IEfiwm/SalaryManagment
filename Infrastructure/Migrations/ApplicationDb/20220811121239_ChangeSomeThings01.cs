using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class ChangeSomeThings01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Guard",
                schema: "Data",
                table: "TbImported",
                newName: "GuardPay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuardPay",
                schema: "Data",
                table: "TbImported",
                newName: "Guard");
        }
    }
}
