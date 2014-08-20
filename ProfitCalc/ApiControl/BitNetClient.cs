// COPYRIGHT 2011 Konstantin Ineshin, Irkutsk, Russia.
// sourceforge.net/projects/bitnet/

using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProfitCalc.ApiControl
{
    public class BitnetClient
    {
        public BitnetClient()
        {
        }

        public BitnetClient(string asUri)
        {
            Url = new Uri(asUri);
        }

        public Uri Url;

        public ICredentials Credentials;

        public JObject InvokeMethod(string asMethod, params object[] aParams)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            webRequest.Credentials = Credentials;

            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            JObject joe = new JObject();
            joe["jsonrpc"] = "1.0";
            joe["id"] = "1";
            joe["method"] = asMethod;

            if (aParams != null)
            {
                if (aParams.Length > 0)
                {
                    JArray props = new JArray();
                    foreach (var p in aParams)
                    {
                        props.Add(p);
                    }
                    joe.Add(new JProperty("params", props));
                }
            }

            string s = JsonConvert.SerializeObject(joe);
            // serialize json for the request
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            webRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (Stream str = webResponse.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        return JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                    }
                }
            }
        }

        public JObject GetMiningInfo()
        {
            return InvokeMethod("getmininginfo")["result"] as JObject;
        }
    }
}
