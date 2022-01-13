namespace Web.Areas.Admin.Models
{
    public class MKViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int RegisteredYear { get; set; }

        public int RegisteredMonth { get; set; }

        public int RegisteredDay { get; set; }

        public double Debt { get; set; }

        public double PreviousDebt { get; set; }

        public string SerialCheck { get; set; }

        public int CheckYear { get; set; }

        public int CheckMonth { get; set; }

        public int CheckDay { get; set; }

        public string BranchName { get; set; }

        public string BankIndex { get; set; }

        public string AccountNo { get; set; }

        public int PaymentMethod { get; set; } = 0;

        public string PaymentAmount { get; set; }

        public int PaymentYear { get; set; }

        public int PaymentMonth { get; set; }

        public int PaymentDay { get; set; }

        public int PaymentAmountOfTreasury { get; set; }
    }
}
