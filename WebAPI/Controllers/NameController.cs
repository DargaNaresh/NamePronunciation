using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

        public NameController()
        {

        }
        public static byte[] content;
        public static byte[] content1;
        public static string userlanID;


        [HttpPost, DisableRequestSizeLimit]
        public ActionResult Upload(IFormFile audio, [FromForm] string email, [FromForm] string lanID, [FromForm] string employeeID)
        {
            //web app will record the mp3 and will send the mp3 to this api
            if (ModelState.IsValid)
            {
                userlanID = lanID;
                if (audio != null && audio.Length > 0)
                {
                    var reader = new System.IO.BinaryReader(audio.OpenReadStream());
                   //for testing : naresh
                    if (employeeID == "1234")
                        content = reader.ReadBytes(Convert.ToInt32(audio.Length));
                    else
                        content1 = reader.ReadBytes(Convert.ToInt32(audio.Length));

                }
                return Ok("uploaded successful"); ;
            }
            return Ok("unsuccessful");

        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("text")]
        public ActionResult Upload([FromForm] string text)
        {
            //string contentPath = this.Environment.ContentRootPath;

            //string path = Path.Combine(this.Environment.ContentRootPath,             "Uploads");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //string fullpath = string.Format("{0}\\{1}", path, "Test.wav");

            //byte[] data;

            //using (var fs = new FileStream(fullpath, FileMode.Create, 

            //FileAccess.Write))
            //{
            SpeechSynthesizer speech = new SpeechSynthesizer();
            speech.Speak(text);
            // int length = Convert.ToInt32(fs.Length);
            // data = new byte[length];
            //    fs.Read(data, 0, length);
            //  speech.SetOutputToWaveStream(fs);
            return Ok("successful");

            //return File(data, "audio/vnd.wav");
        }


        // Download MP3 Driectly : Naresh - Dnt delete
        //[HttpGet, DisableRequestSizeLimit]
        //public async Task<IActionResult> Download()
        //{
        //    //await
        //    return File(content, "audio/mpeg", userlanID + ".mp3");
        //}

        [HttpGet, DisableRequestSizeLimit]
        public byte[] Download(int empID)
        {
            byte[] mybytearray = empID==1234? content:content1;
            return mybytearray;
        }


    }




}
