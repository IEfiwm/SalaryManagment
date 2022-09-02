using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum ExemptionReasons : byte
    {
        [Display(Name = "بدون معافیت")]
        WithoutExemption,
        [Display(Name = "ایثارگری")]
        Isargari,
        [Display(Name = "جانبازی")]
        Veteran,
        [Display(Name = "فرزند معلول")]
        DisabledChild,
        [Display(Name = "مناطق محروم")]
        DeprivedAreas,
        [Display(Name = "بازنشستگی")]
        Retired,
        [Display(Name = "شغل دوم")]
        SecondJob
    }
}