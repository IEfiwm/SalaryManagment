using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RenameRelationUserInDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbDocument_TbAdditionalUserData_UserId",
                schema: "Basic",
                table: "TbDocument");

            migrationBuilder.DropIndex(
                name: "IX_TbDocument_UserId",
                schema: "Basic",
                table: "TbDocument");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Basic",
                table: "TbDocument");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.CreateIndex(
                name: "IX_TbDocument_AdditionalRef",
                schema: "Basic",
                table: "TbDocument",
                column: "AdditionalRef");

            migrationBuilder.AddForeignKey(
                name: "FK_TbDocument_TbAdditionalUserData_AdditionalRef",
                schema: "Basic",
                table: "TbDocument",
                column: "AdditionalRef",
                principalSchema: "Basic",
                principalTable: "TbAdditionalUserData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbDocument_TbAdditionalUserData_AdditionalRef",
                schema: "Basic",
                table: "TbDocument");

            migrationBuilder.DropIndex(
                name: "IX_TbDocument_AdditionalRef",
                schema: "Basic",
                table: "TbDocument");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                schema: "Basic",
                table: "TbDocument",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbDocument_UserId",
                schema: "Basic",
                table: "TbDocument",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbDocument_TbAdditionalUserData_UserId",
                schema: "Basic",
                table: "TbDocument",
                column: "UserId",
                principalSchema: "Basic",
                principalTable: "TbAdditionalUserData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
