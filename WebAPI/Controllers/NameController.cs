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
        
        /// <summary>
        /// Upload recorded Audio file for employee. This services converts the file into bytes and stores on database for respective employee
        /// </summary>
        /// <param name="audio">Recorded Audio File</param>
        /// <param name="employeeID">Employee ID for tagging audio file</param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult Upload(IFormFile audio, [FromForm] string employeeID)
        {
            //web app will record the mp3 and will send the mp3 to this api
            if (ModelState.IsValid)
            {
                // userlanID = lanID;
                if (audio != null && audio.Length > 0)
                {
                    var reader = new System.IO.BinaryReader(audio.OpenReadStream());
                    content = reader.ReadBytes(Convert.ToInt32(audio.Length));

                    // TODO : Store in database for given employee
                }

                return Ok("uploaded successful"); ;
            }
            return Ok("unsuccessful");

        }

        /// <summary>
        /// Converts Text to Speech
        /// </summary>
        /// <param name="text">Provide Text to listen it back</param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [Route("TextToSpeech")]
        public ActionResult Upload([FromForm] string text)
        {
            SpeechSynthesizer speech = new SpeechSynthesizer();
            speech.Speak(text);
            return Ok("successful");
        }


        /// <summary>
        /// Download Audio File Directly on Client based on Employee ID
        /// </summary>
        /// <param name="empID">Employee tagged to Audio File</param>
        /// <returns></returns>
        [HttpGet, DisableRequestSizeLimit]
       [Route("DownloadFile")]
        public ActionResult DownloadFile(int empID)
        {
            //await
            return File(content, "audio/mpeg", empID + ".mp3");
        }

        /// <summary>
        /// Get the audio bytes for the respective employee based on ID
        /// </summary>
        /// <param name="empID">Employee tagged to Audio bytes</param>
        /// <returns></returns>
        [HttpGet, DisableRequestSizeLimit]
        public byte[] Download(int empID)
        {
            byte[] mybytearray = content;
            return mybytearray;
        }
    }

}
