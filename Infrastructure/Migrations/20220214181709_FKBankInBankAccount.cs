using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FKBankInBankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProject_TbBank_BankId",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropIndex(
                name: "IX_TbProject_BankId",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropColumn(
                name: "BankId",
                schema: "Basic",
                table: "TbProject");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                schema: "Basic",
                table: "TbBankAccount",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbBankAccount_BankId",
                schema: "Basic",
                table: "TbBankAccount",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbBankAccount_TbBank_BankId",
                schema: "Basic",
                table: "TbBankAccount",
                column: "BankId",
                principalSchema: "Basic",
                principalTable: "TbBank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbBankAccount_TbBank_BankId",
                schema: "Basic",
                table: "TbBankAccount");

            migrationBuilder.DropIndex(
                name: "IX_TbBankAccount_BankId",
                schema: "Basic",
                table: "TbBankAccount");

            migrationBuilder.DropColumn(
                name: "BankId",
                schema: "Basic",
                table: "TbBankAccount");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                schema: "Basic",
                table: "TbProject",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProject_BankId",
                schema: "Basic",
                table: "TbProject",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbProject_TbBank_BankId",
                schema: "Basic",
                table: "TbProject",
                column: "BankId",
                principalSchema: "Basic",
                principalTable: "TbBank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
