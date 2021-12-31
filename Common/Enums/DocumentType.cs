using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum DocumentType
    {
        [Display(Name = "صفحه اول شناسنامه")]
        IdentityPageOne = 1,
        [Display(Name = "صفحه دوم شناسنامه")]
        IdentityPageTwo = 2,
        [Display(Name = "صفحه ازدواج شناسنامه")]
        IdentityPageMarriage = 3,
        [Display(Name = "صفحه فرزندان شناسنامه")]
        IdentityPageChilds = 4,
        [Display(Name = "جلو کارت ملی")]
        NationalCartFront = 5,
        [Display(Name = "پشت کارت ملی")]
        NationalCartBack = 6
    }
}