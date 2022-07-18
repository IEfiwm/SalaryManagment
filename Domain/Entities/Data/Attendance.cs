using Domain.Base.Entity;

namespace Domain.Entities.Data
{
    public class Attendance : IdentityBaseEntity
    {
        public long ProjectRef { get; set; }

        public int Month { get; set; }
      
        public int Year { get; set; }

        public string NationalCode { get; set; }

        public int WorkingDays { get; set; }
        
        public int OvertimeWorkingTime { get; set; }
       
        public int HolidayWorkingTime { get; set; }

        public int NightWorkingTime { get; set; }
   
        public decimal ShiftWork { get; set; }

        public int MissionTime { get; set; }

        public int FoodTime { get; set; }

        public int RightLeaveTime { get; set; }

        public int SickLeaveTime { get; set; }

        public int AbsenceTime { get; set; }

        public string ServiceLocation { get; set; }

        public string ServiceProvince { get; set; }

        public int ShiftWorkPercentage  { get; set; }

        public int OvertimeWorking { get; set; }

        public int ShiftWorking { get; set; }

        public int Guard { get; set; }

        public int FridayWorkDays { get; set; }

        public int TransferWorkDays { get; set; }

        public int TransferByProvinceWorkDays { get; set; }

        public int FoodWorkDays { get; set; }

        public int FixedOvertimePay { get; set; }

        public int FixedHolidayPay { get; set; }

        public int Performance { get; set; }

        public int FoodPay { get; set; }

        public int FixedTransferPay { get; set; }

        public int FixedAmenitiesPay { get; set; }

        public int OtherIncludeInsuranceAndTax { get; set; }

        public int OtherIncludeInsuranceAndNotIncludeTax { get; set; }

        public int OtherNotIncludeInsuranceAndIncludeTax { get; set; }

        public int OtherNotIncludeInsuranceAndTax { get; set; }

        public int Reward { get; set; }

        public int ClothesPay { get; set; }

        public int OtherAdvantages1 { get; set; }

        public int OtherAdvantages2 { get; set; }

        public int OtherAdvantages3 { get; set; }

        public int OtherAdvantages4 { get; set; }

        public int OtherAdvantages5 { get; set; }

        public int OtherAdvantages6 { get; set; }

        public int OtherAdvantages7 { get; set; }

        public int OtherAdvantages8 { get; set; }

        public int OtherAdvantages9{ get; set; }

        public int OtherAdvantages10 { get; set; }

        public int Help { get; set; }

        public int TemporaryPay { get; set; }

        public int Loan { get; set; }

        public int SupplementaryInsurance { get; set; }

        public int SupplementaryInsuranceChildren { get; set; }

        public int CourtOrderDeductions { get; set; }

        public int ViolationsDeductions { get; set; }

        public int OtherDeductions { get; set; }

        public int OtherDeductions1 { get; set; }

        public int OtherDeductions2 { get; set; }

        public int OtherDeductions3 { get; set; }

        public int OtherDeductions4 { get; set; }

        public int OtherDeductions5 { get; set; }

        public int OtherDeductions6 { get; set; }

        public int OtherDeductions7 { get; set; }

        public int OtherDeductions8 { get; set; }

        public int OtherDeductions9 { get; set; }

        public int OtherDeductions10 { get; set; }

    }
}
