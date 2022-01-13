﻿using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
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
            return View();
        }

        [HttpPost]
        public IActionResult TXT(MKViewModel model)
        {
            var client = new RestClient("https://localhost:44384/Report/TXTTaxSummary");

            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/json");

            request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            var fileName = "WK" + model.Year + CommonHelper.GetTowDigits(model.Month) + ".txt";

            return File(response.RawBytes, "application/octet-stream", fileName);
        }

        [HttpPost]
        public IActionResult PDF(MKViewModel model)
        {
            var client = new RestClient("https://localhost:44384/Report/TaxSummary");

            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/json");

            request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            var fileName = "WK" + model.Year + CommonHelper.GetTowDigits(model.Month) + ".pdf";

            return File(response.RawBytes, "application/octet-stream", fileName);
        }
    }
}