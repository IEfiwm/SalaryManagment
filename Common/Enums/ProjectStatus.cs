using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum ProjectStatus : byte
    {
        [Display(Name = "شروع شده")]
        Started = 0,

        [Display(Name = "پایان یافته")]
        Ended = 1,

        [Display(Name = "شروع نشده")]
        NotStarted = 2,

        [Display(Name = "معلق شده")]
        Suspended = 3,
    }
}
