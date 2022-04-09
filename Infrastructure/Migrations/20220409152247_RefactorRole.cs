using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RefactorRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbRole_Menu_TbRole_RoleId",
                schema: "Basic",
                table: "TbRole_Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_TbRole_Project_Permission_TbRole_RoleId",
                schema: "Basic",
                table: "TbRole_Project_Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_TbUser_Role_TbRole_RoleId",
                schema: "Basic",
                table: "TbUser_Role");

            migrationBuilder.DropTable(
                name: "TbRole",
                schema: "Basic");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "Basic",
                table: "TbUser_Role",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "Basic",
                table: "TbRole_Project_Permission",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "Basic",
                table: "TbRole_Menu",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                schema: "Identity",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TbRole_Menu_Roles_RoleId",
                schema: "Basic",
                table: "TbRole_Menu",
                column: "RoleId",
                principalSchema: "Identity",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TbRole_Project_Permission_Roles_RoleId",
                schema: "Basic",
                table: "TbRole_Project_Permission",
                column: "RoleId",
                principalSchema: "Identity",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TbUser_Role_Roles_RoleId",
                schema: "Basic",
                table: "TbUser_Role",
                column: "RoleId",
                principalSchema: "Identity",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbRole_Menu_Roles_RoleId",
                schema: "Basic",
                table: "TbRole_Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_TbRole_Project_Permission_Roles_RoleId",
                schema: "Basic",
                table: "TbRole_Project_Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_TbUser_Role_Roles_RoleId",
                schema: "Basic",
                table: "TbUser_Role");

            migrationBuilder.DropColumn(
                name: "Active",
                schema: "Identity",
                table: "Roles");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                schema: "Basic",
                table: "TbUser_Role",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                schema: "Basic",
                table: "TbRole_Project_Permission",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "RoleId",
                schema: "Basic",
                table: "TbRole_Menu",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TbRole",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbRole", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TbRole_Menu_TbRole_RoleId",
                schema: "Basic",
                table: "TbRole_Menu",
                column: "RoleId",
                principalSchema: "Basic",
                principalTable: "TbRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbRole_Project_Permission_TbRole_RoleId",
                schema: "Basic",
                table: "TbRole_Project_Permission",
                column: "RoleId",
                principalSchema: "Basic",
                principalTable: "TbRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TbUser_Role_TbRole_RoleId",
                schema: "Basic",
                table: "TbUser_Role",
                column: "RoleId",
                principalSchema: "Basic",
                principalTable: "TbRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
