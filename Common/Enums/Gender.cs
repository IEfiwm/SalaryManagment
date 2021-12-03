using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum Gender
    {
        [Display(Name = "زن")]
        Female = 0,
        [Display(Name = "مرد")]
        Male = 1
    }
}
