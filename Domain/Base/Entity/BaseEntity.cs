namespace Domain.Base.Entity
{
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
