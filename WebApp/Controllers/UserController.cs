using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _config;

        public UserController(ILogger<UserController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        // GET: UserController

        public ActionResult Index()
        {
            List<User> lstSUM = UserData.GetUsers();
            ViewBag.Users = lstSUM;
            return View(lstSUM);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            User user = UserData.GetUsers().Find(x => x.EmployeeNumber == id);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                string apiUri = _config.GetValue<string>("WebAPIEndPoint"); // "Information"
                byte[] data;
                MultipartFormDataContent multiForm = new MultipartFormDataContent();
                var postedFile = collection.Files[0];
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
                HttpResponseMessage httpResponseMessage = client.PostAsync(apiUri, multiForm).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    var content = httpResponseMessage.Content.ReadAsStringAsync().Result;
                }

                byte[] mybytearray = AudioHelper.GetAudioBytesByEmployeeId(id, _config.GetValue<string>("WebAPIEndPoint"));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
