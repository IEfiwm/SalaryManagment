using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeUserEntity03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisabledChildInsuranceExemption",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DisabledChildTaxExemption",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InsuranceExemptionForDeprivedAreas",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IshargarhInsuranceExemption",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IshargarhTaxExemption",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RetiredTaxExemptionAndSecondJob",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaxExemptionForDeprivedAreas",
                schema: "Identity",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "VeteranTaxExemption",
                schema: "Identity",
                table: "Users",
                newName: "TaxExemption");

            migrationBuilder.RenameColumn(
                name: "VeteranInsuranceExemption",
                schema: "Identity",
                table: "Users",
                newName: "InsuranceExemption");

            migrationBuilder.AddColumn<byte>(
                name: "InsuranceExemptionType",
                schema: "Identity",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "TaxExemptionType",
                schema: "Identity",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuranceExemptionType",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaxExemptionType",
                schema: "Identity",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TaxExemption",
                schema: "Identity",
                table: "Users",
                newName: "VeteranTaxExemption");

            migrationBuilder.RenameColumn(
                name: "InsuranceExemption",
                schema: "Identity",
                table: "Users",
                newName: "VeteranInsuranceExemption");

            migrationBuilder.AddColumn<long>(
                name: "DisabledChildInsuranceExemption",
                schema: "Identity",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DisabledChildTaxExemption",
                schema: "Identity",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InsuranceExemptionForDeprivedAreas",
                schema: "Identity",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IshargarhInsuranceExemption",
                schema: "Identity",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "IshargarhTaxExemption",
                schema: "Identity",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RetiredTaxExemptionAndSecondJob",
                schema: "Identity",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TaxExemptionForDeprivedAreas",
                schema: "Identity",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
