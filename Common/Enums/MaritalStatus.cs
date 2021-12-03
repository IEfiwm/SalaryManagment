using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum MaritalStatus
    {
        [Display(Name = "متاهل")]
        Married = 1,
        [Display(Name = "مجرد")]
        Single = 0,
    }
}