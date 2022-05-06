using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class NewFieldsInImported2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImpurePerformance",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartPaymentRecieved",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecievedRemainedPastMonth",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemainedGoodPerformance",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemainedInsurance",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnGoodPerformance",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnInsurance",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TotalBonusesAndSanavatAndLeaveAndOverHead",
                schema: "Data",
                table: "TbImported",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpurePerformance",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "PartPaymentRecieved",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "RecievedRemainedPastMonth",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "RemainedGoodPerformance",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "RemainedInsurance",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "ReturnGoodPerformance",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "ReturnInsurance",
                schema: "Data",
                table: "TbImported");

            migrationBuilder.DropColumn(
                name: "TotalBonusesAndSanavatAndLeaveAndOverHead",
                schema: "Data",
                table: "TbImported");
        }
    }
}
