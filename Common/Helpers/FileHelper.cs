using Common.Models.File;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class FileHelper
    {
        public async Task<FileViewModel> DownloadAndReturnMemorySreamAsync(string fileName, string downloadLink, object model = null)
        {
            var result = new FileViewModel();

            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot", fileName);

            result.FileName = Path.GetFileName(path);

            result.Extension = Path.GetExtension(path);

            var memory = new MemoryStream();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromHours(6);
                if (model != null)
                {
                    var json = JsonConvert.SerializeObject(model);

                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(downloadLink, data))
                    {
                        var test = await response.Content.ReadAsByteArrayAsync();

                        result.DownloadedFileName = response.Content.Headers.ContentDisposition.FileName;

                        await System.IO.File.WriteAllBytesAsync(path, test);

                        using (var stream = new FileStream(path, FileMode.Open))
                        {
                            await stream.CopyToAsync(memory);
                        }

                        memory.Position = 0;

                        System.IO.File.Delete(path);

                        result.FileStream = memory;

                        return result;
                    }
                }
                else
                {
                    var json = JsonConvert.SerializeObject(model);

                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.GetAsync(downloadLink))
                    {
                        var test = await response.Content.ReadAsByteArrayAsync();

                        if (response.Content.Headers.ContentDisposition is null)
                        {
                            return null;
                        }
                        result.DownloadedFileName = response.Content.Headers.ContentDisposition.FileName;

                        await System.IO.File.WriteAllBytesAsync(path, test);

                        using (var stream = new FileStream(path, FileMode.Open))
                        {
                            await stream.CopyToAsync(memory);
                        }

                        memory.Position = 0;

                        System.IO.File.Delete(path);

                        result.FileStream = memory;

                        return result;
                    }
                }
            }
        }
    }
}