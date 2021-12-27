using Common.Enums;
using Domain.Base.Entity;

namespace Domain.Entities.Basic
{
    public class Document : IdentityBaseEntity
    {
        public string FileName { get; set; }

        public string FullPath { get; set; }

        public DocumentType DocumentType { get; set; }
    }
}
