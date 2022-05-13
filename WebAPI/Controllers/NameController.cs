using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private IHostingEnvironment Environment;

        public NameController(IHostingEnvironment _environment)
        {
            Environment = _environment;
            //_logger = logger;
        }
        public static byte[] content;


        [HttpPost, DisableRequestSizeLimit]
        public ActionResult Upload(IFormFile audio, [FromForm] string email, [FromForm] string lanID, [FromForm] string employeeID)
        {
            //web app will record the mp3 and will send the mp3 to this api
            if (ModelState.IsValid)
            {
                if (audio != null && audio.Length > 0)
                {
                    var reader = new System.IO.BinaryReader(audio.OpenReadStream());
                    content = reader.ReadBytes(Convert.ToInt32(audio.Length));

                }
                return Ok("uploaded successful"); ;
            }
            return Ok("unsuccessful");

        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("text")]
        public ActionResult Upload([FromForm] string text)
        {
                string contentPath = this.Environment.ContentRootPath;

                string path = Path.Combine(this.Environment.ContentRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fullpath = string.Format("{0}\\{1}", path, "Test.wav");

                byte[] data;

                using (var fs = new FileStream(fullpath, FileMode.Create, FileAccess.Write))
                {
                    SpeechSynthesizer speech = new SpeechSynthesizer();
                    speech.Speak(text);
                   // int length = Convert.ToInt32(fs.Length);
                   // data = new byte[length];
                //    fs.Read(data, 0, length);
                  //  speech.SetOutputToWaveStream(fs);
                };
            return Ok("successful");

            //return File(data, "audio/vnd.wav");
        }



        [HttpGet, DisableRequestSizeLimit]
    public async Task<IActionResult> Download()
    {
        //await
        return File(content, "audio/mpeg");
    }
}
}
