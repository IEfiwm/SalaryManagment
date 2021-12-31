using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum DocumentType
    {
        [Display(Name = "صفحه اول شناسنامه")]
        IdentityPageOne = 1,
        [Display(Name = "صفحه دوم شناسنامه")]
        IdentityPageTwo = 2,
        [Display(Name = "صفحه سوم شناسنامه")]
        IdentityPageThree = 3,
        [Display(Name = "صفحه چهارم شناسنامه")]
        IdentityPageFour = 4,
        [Display(Name = "جلو کارت ملی")]
        NationalCartUp = 5,
        [Display(Name = "پشت کارت ملی")]
        NationalCartDouwn = 6
    }
}