using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RoleDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    newName: "ApplicationUserApplicationUser",
            //    newSchema: "Identity");

            //migrationBuilder.AddColumn<long>(
            //    name: "FieldId",
            //    schema: "Basic",
            //    table: "TbProjectRule",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TbMenu",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Root = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbMenu", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_TbMenu_TbMenu_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Basic",
                        principalTable: "TbMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbPermission",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPermission", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_TbPermission_TbPermission_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Basic",
                        principalTable: "TbPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbRole",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbRole", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "TbRole_Menu",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    Disable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbRole_Menu", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_TbRole_Menu_TbMenu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "Basic",
                        principalTable: "TbMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbRole_Menu_TbRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Basic",
                        principalTable: "TbRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbRole_Project_Permission",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbRole_Project_Permission", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_TbRole_Project_Permission_TbPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Basic",
                        principalTable: "TbPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbRole_Project_Permission_TbProject_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Basic",
                        principalTable: "TbProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbRole_Project_Permission_TbRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Basic",
                        principalTable: "TbRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbUser_Role",
                schema: "Basic",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbUser_Role", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_TbUser_Role_TbRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Basic",
                        principalTable: "TbRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbUser_Role_Users_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "Identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_TbProjectRule_FieldId",
            //    schema: "Basic",
            //    table: "TbProjectRule",
            //    column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TbMenu_ParentId",
                schema: "Basic",
                table: "TbMenu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TbPermission_ParentId",
                schema: "Basic",
                table: "TbPermission",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TbRole_Menu_MenuId",
                schema: "Basic",
                table: "TbRole_Menu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_TbRole_Menu_RoleId",
                schema: "Basic",
                table: "TbRole_Menu",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TbRole_Project_Permission_PermissionId",
                schema: "Basic",
                table: "TbRole_Project_Permission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_TbRole_Project_Permission_ProjectId",
                schema: "Basic",
                table: "TbRole_Project_Permission",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TbRole_Project_Permission_RoleId",
                schema: "Basic",
                table: "TbRole_Project_Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUser_Role_MenuId",
                schema: "Basic",
                table: "TbUser_Role",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_TbUser_Role_RoleId",
                schema: "Basic",
                table: "TbUser_Role",
                column: "RoleId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TbProjectRule_TbField_FieldId",
            //    schema: "Basic",
            //    table: "TbProjectRule",
            //    column: "FieldId",
            //    principalSchema: "Data",
            //    principalTable: "TbField",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_TbProjectRule_TbField_FieldId",
            //    schema: "Basic",
            //    table: "TbProjectRule");

            migrationBuilder.DropTable(
                name: "TbRole_Menu",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "TbRole_Project_Permission",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "TbUser_Role",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "TbMenu",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "TbPermission",
                schema: "Basic");

            migrationBuilder.DropTable(
                name: "TbRole",
                schema: "Basic");

            //migrationBuilder.DropIndex(
            //    name: "IX_TbProjectRule_FieldId",
            //    schema: "Basic",
            //    table: "TbProjectRule");

            //migrationBuilder.DropColumn(
            //    name: "FieldId",
            //    schema: "Basic",
            //    table: "TbProjectRule");

            //migrationBuilder.RenameTable(
            //    name: "ApplicationUserApplicationUser",
            //    schema: "Identity",
            //    newName: "ApplicationUserApplicationUser");
        }
    }
}
