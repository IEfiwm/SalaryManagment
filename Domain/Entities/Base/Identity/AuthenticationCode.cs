using Domain.Base.Entity;
using System;

namespace Domain.Entities.Base.Identity
{
    public class AuthenticationCode : IdentityBaseEntity
    {
        public string PhoneNumber { get; set; }

        public string Code { get; set; }

        public DateTime ExpireDate { get; set; }

        public DateTime RequestedDate { get; set; } = DateTime.Now;

        public bool IsUsed { get; set; } = false;

        public bool IsActive { get; set; } = true;
    }
}
