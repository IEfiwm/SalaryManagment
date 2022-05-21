using Common.Models.File;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IHostingEnvironment _hostingEnvironmen;

        public FileRepository(IHostingEnvironment hostingEnvironmen)
        {
            _hostingEnvironmen = hostingEnvironmen;
        }
        public async Task<string> SaveImageAsync(IFormFile image, string path = "")
        {
            if (path == "")
            {
                path = Path.Combine(_hostingEnvironmen.WebRootPath,@"Files\Images");
            }

            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(image.FileName);

            string imagePath = Path.Combine(path + "image", fileName);

            string thumbPath = Path.Combine(path + "thumb", fileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            new ImageConvertor().Image_resize(imagePath, thumbPath, 300);

            return fileName;
        }
        public bool CheckImageExists(string fileName)
        {
            string filePath = Path.Combine(_hostingEnvironmen.WebRootPath, "Files\\Images\\image", fileName);
            return File.Exists(filePath);
        }
        public ImagePathViewModel GetFileFullPath(string fullPath)
        {
            ImagePathViewModel path = new ImagePathViewModel();
            path.ThumbPath = Path.Combine("/", "Files\\Images\\thumb", fullPath).Replace("\\", "/");
            path.ImagePath = Path.Combine("/", "Files\\Images\\image", fullPath).Replace("\\", "/");
            return path;
        }

    }
}
