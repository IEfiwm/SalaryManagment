namespace Web.Areas.Admin.Models
{
    public class Bank_AccountViewModel
    {
        public long Id { get; set; }

        public string AccountNumber { get; set; }

        public string BranchName { get; set; }

        public string BranchCode { get; set; }

        public string iBan { get; set; }

        public string CardNumber { get; set; }

        public bool Active { get; set; }

        public long BankId { get; set; }
    }
}
