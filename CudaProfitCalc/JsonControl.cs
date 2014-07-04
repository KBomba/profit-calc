using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace ProfitCalc
{
    internal static class JsonControl
    {
        public static T DownloadSerializedApi<T>(string address) where T : new()
        {
            HttpClient client = new HttpClient {Timeout = new TimeSpan(0, 0, 66)};

            using (BufferedStream bs = new BufferedStream(client.GetStreamAsync(address).Result))
            using (JsonReader reader = new JsonTextReader(new StreamReader(bs)))
            {
                JsonSerializer serializer = new JsonSerializer {NullValueHandling = NullValueHandling.Ignore};
                return serializer.Deserialize<T>(reader);
            }
        }


        public static T GetSerializedApiFile<T>(string location) where T : new()
        {
            using (StreamReader file = File.OpenText(location))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (T) serializer.Deserialize(file, typeof (T));
            }
        }
    }
}