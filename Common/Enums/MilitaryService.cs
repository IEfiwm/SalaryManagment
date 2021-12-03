using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum MilitaryService
    {
        [Display(Name = "انجام شده")]
        Done = 0,
        [Display(Name = "مشمول به خدمت")]
        Conscript = 1,
        [Display(Name = "معافیت")]
        MilitaryExemption = 2,
        [Display(Name = "ندارد")]
        None = 3,
    }
}
