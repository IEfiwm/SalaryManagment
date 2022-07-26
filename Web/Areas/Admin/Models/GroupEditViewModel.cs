using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.Models
{
    public class GroupEditViewModel
    {
        public string NationalCode { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int MonthlyBaseYear { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int MonthlySalary { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int ChildrenRight { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int WorkerRight { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int FoodAndHouseRight { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int DailyBaseYear { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public int DailySalary { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public string InsuranceCode { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public string BankAccount { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public string iBanNumber { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public byte NotIncludedChildren { get; set; }

        [RegularExpression(@"^[0-9]+(\.{0,1}[0-9]{0,3})$", ErrorMessage = "لطفا فیلد را با عدد وارد کنید. (حداکثر سه رقم اعشار).")]
        public byte IncludedChildren { get; set; }
    }
}
