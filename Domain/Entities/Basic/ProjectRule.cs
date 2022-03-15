using Domain.Base.Entity;
using Domain.Entities.Data;
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

        [ForeignKey("Field")]
        public long FieldId { get; set; }

        public Field Field { get; set; }

    }
}
