using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum PersonnelType
    {
        [Display(Name = "ثابت")]
        Fix,
        [Display(Name = "نماینده")]
        Agent,
        [Display(Name = "جایگزین")]
        Replace,
        [Display(Name = "متفرقه")]
        Other
    }
}