using Domain.Base.Entity;
using Domain.Entities.Basic;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities.Data
{
    public class Field : IdentityBaseEntity
    {

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsCalculate { get; set; } = false;

        public bool IsCalculatedBy { get; set; } = false;
        
        public virtual ICollection<ProjectRule> ProjectRules { get; set; }


    }
}
