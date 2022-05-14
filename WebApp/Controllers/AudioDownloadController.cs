using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    public class AudioDownloadController : Controller
    {
        private readonly ILogger<AudioDownloadController> _logger;
        private readonly IConfiguration _config;
        public AudioDownloadController(ILogger<AudioDownloadController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        [HttpGet]
        public IActionResult Index(string empnum)
        {
            string apiUri = _config.GetValue<string>("WebAPIEndPoint"); // "Information"
            apiUri += "?empID="+empnum;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            byte[] mybytearray = null;
            response = client.GetAsync(apiUri).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = null;
                result = response.Content.ReadAsStringAsync().Result.Replace("\"", string.Empty);
                mybytearray = Convert.FromBase64String(result);
            }
            ViewBag.AudioData = "data:audio/wav;base64," + Convert.ToBase64String(mybytearray);
            return PartialView();
        }
    }
}
