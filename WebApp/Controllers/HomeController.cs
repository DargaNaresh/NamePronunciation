using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //upload
        [HttpPost]
        public IActionResult Index(IFormFile postedFile)
        {

            byte[] data;
            MultipartFormDataContent multiForm = new MultipartFormDataContent();
            multiForm.Add(new StringContent("narehdarga@gmail.com"), "email");
            multiForm.Add(new StringContent("1473635"), "employeeID");
            multiForm.Add(new StringContent("u164207"), "lanID");

            if (postedFile.Length > 0)
            {
                ByteArrayContent bytes;
                using (var br = new BinaryReader(postedFile.OpenReadStream()))
                {
                    data = br.ReadBytes((int)postedFile.Length);
                }
                bytes = new ByteArrayContent(data);
                multiForm.Add(bytes, "audio", postedFile.FileName);
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponseMessage = client.PostAsync("http://localhost:50015/api/name", multiForm).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            else
            {
                var content = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            return View();
        }

        //download
        public IActionResult Privacy()
        {
            string apiUrl = "http://localhost:50015/api/name";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            byte[] mybytearray = null;
            response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = null;
                result = response.Content.ReadAsStringAsync().Result.Replace("\"",string.Empty);
                mybytearray = Convert.FromBase64String(result);
            }
            ViewBag.Data = "data:audio/wav;base64," + Convert.ToBase64String(mybytearray);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore= true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ??HttpContext.TraceIdentifier
            });
        }


    }
}
