using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Dashboard.Models
{
    public class DocumentViewModel
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public string FullPath { get; set; }
        public DocumentType DocumentType { get; set; }
        public long AdditionalRef { get; set; }

    }
}
