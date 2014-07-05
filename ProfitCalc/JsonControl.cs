using System.IO;
using Newtonsoft.Json;

namespace ProfitCalc
{
    public static class JsonControl
    {
        public static T DownloadSerializedApi<T>(Stream rawStream) where T : new()
        {
            using (JsonReader reader = 
                new JsonTextReader(
                    new StreamReader(
                        new BufferedStream(
                            rawStream))))
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