using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum PaymentType
    {
        [Display(Name = "واریز نقدی")]
        Cash = 1,
        [Display(Name = "ارائه لیست بدون پرداخت مالبات")]
        WithoutPayTax = 2,
        [Display(Name = "پرداخت خزانه")]
        Treasury = 3,
        [Display(Name = "چک شخصی")]
        PersonnalCheck = 4,
        [Display(Name = "پرداخت با کارت اعتباری")]
        CreditCard = 5,
        [Display(Name = "انتقال بانکی")]
        Bank = 6,
        [Display(Name = "سفته")]
        PromissoryNote = 7,
        [Display(Name = "چک تضمین شده")]
        GuaranteedCheck = 8,
    }
}