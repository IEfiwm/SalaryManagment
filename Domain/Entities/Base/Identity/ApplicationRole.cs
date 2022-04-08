using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Base.Identity
{
    public partial class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole(string name) : base(name)
        {
        }

        public ApplicationRole() : base()
        {
        }
    }
}