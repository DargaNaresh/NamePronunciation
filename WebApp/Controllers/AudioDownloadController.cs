using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AudioDownloadController : Controller
    {
        [HttpGet]
        public IActionResult Index(string empnum)
        {
            string apiUrl = "http://localhost:50015/api/name";
            apiUrl += "?empID="+empnum;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            byte[] mybytearray = null;
            response = client.GetAsync(apiUrl).Result;
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
