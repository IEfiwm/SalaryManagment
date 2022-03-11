using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FkProjectRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<long>(
                name: "FieldId",
                schema: "Basic",
                table: "TbProjectRule",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TbProjectRule_FieldId",
                schema: "Basic",
                table: "TbProjectRule",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProjectRule_TbField_FieldId",
                schema: "Basic",
                table: "TbProjectRule",
                column: "FieldId",
                principalSchema: "Data",
                principalTable: "TbField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProjectRule_TbField_FieldId",
                schema: "Basic",
                table: "TbProjectRule");

            migrationBuilder.DropIndex(
                name: "IX_TbProjectRule_FieldId",
                schema: "Basic",
                table: "TbProjectRule");

            migrationBuilder.DropColumn(
                name: "FieldId",
                schema: "Basic",
                table: "TbProjectRule");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}
