using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum Condition
    {
        
        [Display(Name = "IIF(")]
        Condition = 1,
        [Display(Name = ",")]
        Comma = 2,
        [Display(Name = "=")]
        Equal = 3,
        [Display(Name = ">")]
        Greater = 4,
        [Display(Name = "<")]
        Lower = 5,
        [Display(Name = ">=")]
        GreaterEqual = 6,
        [Display(Name = "<=")]
        LowerEqual = 7,

    }
}