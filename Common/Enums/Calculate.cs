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
        [Display(Name = "%")]
        Percent = 4,
        [Display(Name = "(")]
        ParenthesesO = 5,
        [Display(Name = ")")]
        ParenthesesC = 6,
    }
}