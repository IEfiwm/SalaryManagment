using System.ComponentModel.DataAnnotations;

namespace Domain.Base.Entity
{
    public abstract class IdentityBaseEntity<TKey> : BaseEntity
    {
        [Key]
        public TKey Id { get; set; }
    }

    public abstract class IdentityBaseEntity : IdentityBaseEntity<long>
    {
    }
}
