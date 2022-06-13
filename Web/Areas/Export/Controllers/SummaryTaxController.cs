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
    [Authorize]
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

            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(fileName, $@"{ _configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TXTTaxSummary", model);

            return File(viewModel.FileStream, "application/octet-stream", viewModel.FileName);
        }

        [HttpPost]
        public async Task<IActionResult> PDF(MKViewModel model)
        {
            
            model.PaymentMethodStr = EnumHelper<PaymentType>.GetDisplayValue((PaymentType)model.PaymentMethod);

            model.BankName = EnumHelper<BankType>.GetDisplayValue((BankType)model.BankIndex);

            var fileName = "WK" + model.Year + CommonHelper.GetTowDigits(model.Month) + ".pdf";

            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(fileName, $@"{ _configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TaxSummary", model);

            return File(viewModel.FileStream, "application/octet-stream", viewModel.FileName);
        }
    }
}
