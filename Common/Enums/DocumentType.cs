using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum DocumentType
    {
        [Display(Name = "عکس پرسنلی")]
        Avatar = 1,
        [Display(Name = "کارت ملی")]
        NationalCart = 2,
        [Display(Name = "پشت کارت ملی")]
        NationalCartBack = 3,
        [Display(Name = "صفحه اول شناسنامه")]
        IdentityPageOne = 4,
        [Display(Name = "صفحه دوم شناسنامه")]
        IdentityPageTwo = 5,
        [Display(Name = "صفحه سوم شناسنامه")]
        IdentityPageThree = 6,
        [Display(Name = "کارت پایان خدمت")]
        MilitaryServiceCart  = 7,
        [Display(Name = "آخرین مدرک تحصیلی")]
        LatestEducation = 8,
        [Display(Name = "تضامین 1")]
        InsuranceFirst = 9,
        [Display(Name = "تضامین 2")]
        InsuranceSecond = 10,
        [Display(Name = "سایر")]
        other = 11,
    }
}