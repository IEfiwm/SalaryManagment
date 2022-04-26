using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addPersianNameInPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<string>(
                name: "PersianName",
                schema: "Basic",
                table: "TbPermission",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersianName",
                schema: "Basic",
                table: "TbPermission");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}
