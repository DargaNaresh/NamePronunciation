using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Utility;

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
        public IActionResult Index(int empnum)
        {
            byte[] mybytearray = AudioHelper.GetAudioBytesByEmployeeId(empnum, _config.GetValue<string>("WebAPIEndPoint"));
            ViewBag.AudioData = "data:audio/wav;base64," + Convert.ToBase64String(mybytearray);
            return PartialView();
        }

    
    }
}
