using Common.Models.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IFileRepository
    {
        Task<string> SaveImageAsync(IFormFile image, string path = "");
        bool CheckImageExists(string fileName);
        ImagePathViewModel GetFileFullPath(string fullPath, string basePath = "");
    }
}
