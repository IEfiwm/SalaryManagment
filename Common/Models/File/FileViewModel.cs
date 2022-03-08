using System.IO;

namespace Common.Models.File
{
    public class FileViewModel
    {
        public string DownloadedFileName { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public MemoryStream FileStream { get; set; }
    }
}
