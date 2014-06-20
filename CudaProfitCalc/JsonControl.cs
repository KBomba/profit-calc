using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace CudaProfitCalc
{
    static class JsonControl
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

        public static T GetSerializedApiFile<T>(string location) where T : new()
        {
            T newT = new T();

            using (StreamReader file = File.OpenText(location))
            {
                JsonSerializer serializer = new JsonSerializer();
                newT = (T)serializer.Deserialize(file, typeof(T));
            }
            
            return newT;
        }
    }
}
