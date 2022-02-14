using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class bankTablesWithBrothers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AddColumn<long>(
                name: "BankId",
                schema: "Basic",
                table: "TbProject",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbBank",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbBank", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "TbBank_Account",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    iBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    BankId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbBank_Account", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_TbBank_Account_TbBank_BankId",
                        column: x => x.BankId,
                        principalSchema: "Basic",
                        principalTable: "TbBank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbProjectBankAccount",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bank_AccountId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProjectBankAccount", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_TbProjectBankAccount_TbBank_Account_Bank_AccountId",
                        column: x => x.Bank_AccountId,
                        principalSchema: "Basic",
                        principalTable: "TbBank_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbProjectBankAccount_TbProject_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Basic",
                        principalTable: "TbProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbProject_BankId",
                schema: "Basic",
                table: "TbProject",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_TbBank_Account_BankId",
                schema: "Basic",
                table: "TbBank_Account",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_TbProjectBankAccount_Bank_AccountId",
                schema: "Basic",
                table: "TbProjectBankAccount",
                column: "Bank_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TbProjectBankAccount_ProjectId",
                schema: "Basic",
                table: "TbProjectBankAccount",
                column: "ProjectId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProject_TbBank_BankId",
                schema: "Basic",
                table: "TbProject");

            migrationBuilder.DropTable(
                name: "TbProjectBankAccount",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "TbBank_Account",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "TbBank",
                schema: "Basic");

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
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}
