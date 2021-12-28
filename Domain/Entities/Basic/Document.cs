using Common.Enums;
using Domain.Base.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Basic
{
    public class Document : IdentityBaseEntity
    {
        public string FileName { get; set; }

        public string FullPath { get; set; }
        public DocumentType DocumentType { get; set; }

        [ForeignKey("AdditionalUserData")]
        public long AdditionalRef { get; set; }


        public virtual AdditionalUserData User { get; set; }
    }
}
