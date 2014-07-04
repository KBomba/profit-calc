using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using ProfitCalc.ApiControl;

namespace ProfitCalc
{
    static class JsonControl
    {
        public static T DownloadSerializedApi<T>(string address) where T : new()
        {
            HttpClient client = new HttpClient();

            int buffersize = 16384;
            if (typeof (T) == typeof (Cryptsy))
            {
                //8MB bufferedstream, yes, cryptsy is that large
                buffersize = 8000000;
            }

            using (Stream s = client.GetStreamAsync(address).Result)
            using (BufferedStream bs = new BufferedStream(s, buffersize))
            using (StreamReader sr = new StreamReader(bs))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(reader);
            }
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
