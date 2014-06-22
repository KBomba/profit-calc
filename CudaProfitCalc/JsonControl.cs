using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace CudaProfitCalc
{
    static class JsonControl
    {
        public static T DownloadSerializedApi<T>(string address) where T : new()
        {
            T newT = new T();
            HttpClient client = new HttpClient();

            using (Stream s = client.GetStreamAsync(address).Result)
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();

                newT = serializer.Deserialize<T>(reader);
            }

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
