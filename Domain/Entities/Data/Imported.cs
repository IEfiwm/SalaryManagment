using Domain.Base.Entity;
using Domain.Entities.Basic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Data
{
    public class Imported : IdentityBaseEntity
    {
        public string Name { get; set; }

        public string FamilyName { get; set; }

        public string NationalCode { get; set; }

        public string DurationOperation { get; set; }

        public string OvertimeworkingTime { get; set; }

        public string NightworkingTime { get; set; }

        public string HolidayworkingTime { get; set; }

        public string MissionTime { get; set; }

        public string FoodTime { get; set; }

        public string NumberChildren { get; set; }

        public string Salary { get; set; }

        public string SeveranceDaily { get; set; }

        public string SeveranceMonthly { get; set; }

        public string DailyPay { get; set; }

        public string FoodAndHousingRight { get; set; }

        public string WorkerRight { get; set; }

        public string OvertimePay { get; set; }

        public string MonthlyPay { get; set; }

        public string OvertimeworkingPay { get; set; }

        public string ChildrenRightPay { get; set; }

        public string HouseRightPay { get; set; }

        public string WorkerRightPay { get; set; }

        public string NightworkingPay { get; set; }

        public string HolidayworkingPay { get; set; }

        public string MissionPay { get; set; }

        public string FoodPay { get; set; }

        public string Other01 { get; set; }

        public string Other02 { get; set; }

        public string Disparity { get; set; }

        public string PreviousReceipt { get; set; }

        public string SumSalaryAndBenefit { get; set; }

        public string TaxationPay { get; set; }

        public string InsurancePay { get; set; }

        public string Insurance7Percent { get; set; }

        public string Taxation { get; set; }

        public string HelpPay { get; set; }

        public string Absence { get; set; }

        public string Debt { get; set; }

        public string OtherDeductions { get; set; }

        public string SumDeductions { get; set; }

        public string PureIncome { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public string ShiftWorkTime { get; set; }

        public string ShiftWorkPay { get; set; }

        public string SupplementaryInsuranceDeduction { get; set; }

        public string RewardTime { get; set; }

        public string RewardPay { get; set; }

        public string YearsPay { get; set; }

        public string FestalPay { get; set; }

        public string BasicOverTimePay { get; set; }

        public string SupplementaryInsuranceSupervisor { get; set; }

        public string SupplementaryInsuranceForDependents { get; set; }

        public string NonDependentSupplementaryInsurance { get; set; }

        public string WelfareCostPay { get; set; }

        public string TransportationPay { get; set; }

        public string DelayedTime { get; set; }

        public string InstitutionaLoan { get; set; }

        public string SamanLoan { get; set; }

        public string DelayedTransportationPay { get; set; }

        public string DelayedSupplementaryInsuranceDeduction { get; set; }

        public string WelfareAllowancePay { get; set; }

        public string PerformancePay { get; set; }

        public string Daily { get; set; }

        public string Monthly { get; set; }

        public string MonthlyBenefits { get; set; }

        public string MonthlyWagesAndBenefitsIncluded { get; set; }

        public string IncludedAndNotIncluded { get; set; }

        public string UnemploymentInsurance { get; set; }

        public string Insurance30Percent { get; set; }

        public string EmployerShareInsurance { get; set; }

        public string ContinuousBasicRightsToHousingAndChildrenRights { get; set; }

        public string ContinuousBaseSalaryAndBaseYears { get; set; }

        public string NonContinuousIncludedNotIncluded { get; set; }

        public string NonContinuousIncluded { get; set; }
       
        public string Bonuses { get; set; }

        public string Leave { get; set; }

        public string Sanavat { get; set; }

        public string OverHead { get; set; }

        public string LifeAndAccidents { get; set; }

        public string Sum { get; set; }

        public string Insured { get; set; }

        public string Insurance23Percent { get; set; }

        public string SumWithInsurance { get; set; }

        public string VAT { get; set; }

        public string ValueAddedAggregates { get; set; }

        public string SupplementaryInsurance { get; set; }

        public string ValueAddedInsurance { get; set; }

        public string Total { get; set; }

        public string Description { get; set; }

        public string GoodPerformance10Percent { get; set; }

        public string Deposit5Percent { get; set; }

        public string Pure { get; set; }

        public string TotalBonusesAndLeave { get; set; }

        public string TotalWithoutBonusesAndLeave { get; set; }


        public long? ProjectRef { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
