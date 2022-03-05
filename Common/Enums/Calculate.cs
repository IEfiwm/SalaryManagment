using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum Calculate
    {
        [Display(Name ="+")]
        Plus = 0,
        [Display(Name = "-")]
        Mines = 1,
        [Display(Name = "*")]
        Multiple = 2,
        [Display(Name = "/")]
        Divided = 3,
    }
}