using Domain.Base.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.Basic
{
    public class ProjectRule : IdentityBaseEntity
    {
        public string Name { get; set; }

        public string Rule { get; set; }

        [ForeignKey("Project")]
        public long ProjectId { get; set; }

        public Project Project { get; set; }

    }
}
