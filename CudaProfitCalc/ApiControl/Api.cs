using System.Net;
using Newtonsoft.Json;

namespace CudaProfitCalc.ApiControl
{
    class Api
    {
        public static T DownloadSerializedApi<T>(string address) where T : new()
        {
            string jsonData;
            using (WebClient web = new WebClient())
            {
               jsonData = web.DownloadString(address);
            }

            T newT = !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
            return newT;
        }
    }
}
