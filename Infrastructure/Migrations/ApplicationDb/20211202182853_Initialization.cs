using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Data");

            migrationBuilder.CreateTable(
                name: "TbImported",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeEducation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationOperation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OvertimeworkingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NightworkingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolidayworkingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MissionTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberChildren = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeveranceDaily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeveranceMonthly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodAndHousingRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerRight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OvertimePay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OvertimeworkingPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildrenRightPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseRightPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerRightPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NightworkingPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolidayworkingPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MissionPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other01 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Other02 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disparity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousReceipt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SumSalaryAndBenefit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxationPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsurancePay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Insurance7Percent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HelpPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Absence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDeductions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SumDeductions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PureIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShiftWorkTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShiftWorkPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplementaryInsuranceDeduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RewardPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FestalPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicOverTimePay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplementaryInsuranceSupervisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplementaryInsuranceForDependents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NonDependentSupplementaryInsurance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WelfareCostPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportationPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelayedTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionaLoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SamanLoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelayedTransportationPay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelayedSupplementaryInsuranceDeduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WelfareAllowancePay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformancePay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbImported", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbImported",
                schema: "Data");
        }
    }
}
