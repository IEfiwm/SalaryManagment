using Application.Extensions;
using Common.Enums;
using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class SummaryTaxController : BaseController<SummaryTaxController>
    {
        public IActionResult Index()
        {
            return View(new MKViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> TXT(MKViewModel model)
        {
            var fileName = "WK" + model.Year + CommonHelper.GetTowDigits(model.Month) + ".txt";

            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot", fileName);

            var memory = new MemoryStream();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromHours(6);

                var json = JsonConvert.SerializeObject(model);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($@"{ _configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TXTTaxSummary", data))
                {
                    var test = await response.Content.ReadAsByteArrayAsync();

                    await System.IO.File.WriteAllBytesAsync(path, test);

                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }

                    memory.Position = 0;

                    System.IO.File.Delete(path);
                }
            }
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }

        [HttpPost]
        public async Task<IActionResult> PDF(MKViewModel model)
        {
            model.PaymentMethodStr = EnumHelper<PaymentType>.GetDisplayValue((PaymentType)model.PaymentMethod);

            model.BankName = EnumHelper<BankType>.GetDisplayValue((BankType)model.BankIndex);

            var fileName = "WK" + model.Year + CommonHelper.GetTowDigits(model.Month) + ".pdf";

            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot", fileName);

            var memory = new MemoryStream();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromHours(6);

                var json = JsonConvert.SerializeObject(model);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($@"{ _configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TaxSummary", data))
                {
                    var test = await response.Content.ReadAsByteArrayAsync();

                    await System.IO.File.WriteAllBytesAsync(path, test);

                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }

                    memory.Position = 0;

                    System.IO.File.Delete(path);
                }
            }
            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }
    }
}
