using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CudaProfitCalc
{
    class MiscJson
    {
    }

    class HashRateJson
    {
        [JsonProperty("HashRateList")]
        public Dictionary<HashAlgo.Algo, double> List { get; set; }
    }

    class ApiJson
    {
        [JsonProperty("ApiSettings")]
        public Dictionary<string, string> ApiSettings { get; set; }

        [JsonProperty("CheckedApis")]
        public Dictionary<string, bool> CheckedApis { get; set; } 
    }
}
