using Domain.Base.Entity;

namespace Domain.Entities.Data
{
    public class Field : IdentityBaseEntity
    {

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsCalculate { get; set; } = false;

        public bool IsCalculatedBy { get; set; } = false;


    }
}
