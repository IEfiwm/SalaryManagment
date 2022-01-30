using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum EmployeeStatus
    {
        [Display(Name = "مشغول به کار با بیمه")]
        ActiveWithInsurance = 0,
        [Display(Name = "مشغول به کار بدون بیمه")]
        ActiveWithoutInsurance = 1,
        [Display(Name = "ترک کار با بیمه بیکاری")]
        LeaveWithInsurance = 2,
        [Display(Name = "ترک کار")]
        Leave = 3,
     }
}