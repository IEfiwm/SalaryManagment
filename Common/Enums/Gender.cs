using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum Gender
    {
        [Display(Name = "خانم")]
        Female = 0,
        [Display(Name = "آقا")]
        Male = 1
    }
}
