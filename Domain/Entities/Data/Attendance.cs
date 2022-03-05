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

    }
}
