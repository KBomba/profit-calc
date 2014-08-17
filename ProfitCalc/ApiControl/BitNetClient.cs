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

        public string GetBlockByCount(int aHeight)
        {
            return InvokeMethod("getblockbycount", aHeight)["result"].ToString();
        }

        public int GetBlockCount()
        {
            return (int)InvokeMethod("getblockcount")["result"];
        }

        public int GetBlockNumber()
        {
            return (int)InvokeMethod("getblocknumber")["result"];
        }

        public int GetConnectionCount()
        {
            return (int)InvokeMethod("getconnectioncount")["result"];
        }

        public double GetDifficulty()
        {
            return (double)InvokeMethod("getdifficulty")["result"];
        }

        public bool GetGenerate()
        {
            return (bool)InvokeMethod("getgenerate")["result"];
        }

        public float GetHashesPerSec()
        {
            return (float)InvokeMethod("gethashespersec")["result"];
        }

        public double GetNetworkHashesPerSec()
        {
            return (double)InvokeMethod("getnetworkhashps")["result"];
        }
        
        public JObject GetInfo()
        {
            return InvokeMethod("getinfo")["result"] as JObject;
        }

        public JObject GetMiningInfo()
        {
            return InvokeMethod("getmininginfo")["result"] as JObject;
        }

        public JObject GetWork()
        {
            return InvokeMethod("getwork")["result"] as JObject;
        }

        public bool GetWork(string aData)
        {
            return (bool)InvokeMethod("getwork", aData)["result"];
        }
    }
}
