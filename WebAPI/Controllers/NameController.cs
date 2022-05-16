using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        public static byte[] content;
        private const string Key = ".............";
        private const string Location = "eastus"; // Azure Speech Service Location

        private IHostingEnvironment Environment;

        public NameController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }



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
            var result = TextToBytesAsync(text);
            return Ok(result.Result);
        }

       
        private async Task<byte[]> TextToBytesAsync(string audioText)
        {
            try
            {
                byte[] buffer = new byte[160000000];
                var config = SpeechConfig.FromSubscription(Key, Location);
                config.SpeechSynthesisVoiceName = "en-US-AriaNeural";
                config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Raw8Khz16BitMonoPcm);
                // Creates a speech synthesizer using the default speaker as audio output.
                using (var synthesizer = new SpeechSynthesizer(config, null))
                {
                    while (true)
                    {
                        using (SpeechSynthesisResult result = await synthesizer.SpeakSsmlAsync(audioText))
                        {
                            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                            {
                                string folderName = Environment.ContentRootPath + @"\audiofiles";
                                // If directory does not exist, create it
                                if (!Directory.Exists(folderName))
                                {
                                    Directory.CreateDirectory(folderName);
                                }

                                AudioDataStream stream = AudioDataStream.FromResult(result);  // to return in Memory
                                await stream.SaveToWaveFileAsync(folderName + @"//" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + ".wav");

                                //_logger.LogInformation("AzureSynthesisController_AzureSynthesisToBytesAsync", new Dictionary<string, string> { { "Id", Id }, { "ResultReasonMessage", "SynthesizingAudioCompleted" }, { "OriginalText", audioText } });


                                var buffer2 = result.AudioData;
                                Stream stream2 = new MemoryStream(buffer2);
                                return buffer2;

                                //return result.AudioData;
                            }

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task Conversion(string text,
        SpeechSynthesizer synthesizer)
        {
            using (var r = await synthesizer.SpeakTextAsync(text))
            {
                if (r.Reason == ResultReason.SynthesizingAudioCompleted)
                    Console.WriteLine($"Speech converted " +
                    $"to speaker for text [{text}]");
                else if (r.Reason == ResultReason.Canceled)
                {
                    var cancellation =
                    SpeechSynthesisCancellationDetails.FromResult(r);
                    Console.WriteLine($"CANCELED: " +
                    $"Reason={cancellation.Reason}");
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"Cancelled with " +
                        $"Error Code {cancellation.ErrorCode}");
                        Console.WriteLine($"Cancelled with " +
                        $"Error Details " +
                        $"[{cancellation.ErrorDetails}]");
                    }
                }
            }

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
