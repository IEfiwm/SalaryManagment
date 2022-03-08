using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddIncludedNumberOfChildrenAndNotIncludedNumberOfChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.RenameColumn(
                name: "NumberOfChildren",
                schema: "Identity",
                table: "Users",
                newName: "NotIncludedNumberOfChildern");

            migrationBuilder.AddColumn<byte>(
                name: "IncludedNumberOfChildren",
                schema: "Identity",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncludedNumberOfChildren",
                schema: "Identity",
                table: "Users");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");

            migrationBuilder.RenameColumn(
                name: "NotIncludedNumberOfChildern",
                schema: "Identity",
                table: "Users",
                newName: "NumberOfChildren");
        }
    }
}
