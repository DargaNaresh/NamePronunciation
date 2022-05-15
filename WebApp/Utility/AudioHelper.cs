using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Utility
{
    public class AudioHelper
    {
        public static byte[] GetAudioBytesByEmployeeId(int empnum, string apiUri)
        {
            apiUri += "?empID=" + Convert.ToString( empnum);

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

            return mybytearray;
        }
    }
}
