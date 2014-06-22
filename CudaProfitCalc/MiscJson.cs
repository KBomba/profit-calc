using System.Collections.Generic;
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
