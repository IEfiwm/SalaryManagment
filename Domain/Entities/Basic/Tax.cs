using Domain.Base.Entity;


namespace Domain.Entities.Basic
{
    public class Tax : IdentityBaseEntity
    {
        public decimal Start { get; set; }

        public decimal End { get; set; }

        public decimal Percentage { get; set; }

        public int year { get; set; }
    }
}
