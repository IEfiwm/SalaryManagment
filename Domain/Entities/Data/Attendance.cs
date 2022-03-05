using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Data
{
    public class Attendance : IdentityBaseEntity
    {
        public string NationalCode { get; set; }

        public int WorkingDays { get; set; }
        
        public int OvertimeWorkingTime { get; set; }
       
        public int HolidayWorkingTime { get; set; }

        public int NightWorkingTime { get; set; }
   
        public int ShiftWork { get; set; }

        public int MissionTime { get; set; }

        public int FoodTime { get; set; }

        public int RightLeaveTime { get; set; }

        public int SickLeaveTime { get; set; }

        public int AbsenceTime { get; set; }

    }
}
