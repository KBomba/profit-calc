using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc
{
    internal class HashRateJson
    {
        [JsonProperty("HashRateList")]
        public Dictionary<HashAlgo.Algo, double> ListHashRate { get; set; }

        [JsonProperty("WattageList")]
        public Dictionary<HashAlgo.Algo, double> ListWattage { get; set; }

        [JsonProperty("FiatPerKwh")]
        public double FiatPerKwh { get; set; }

        [JsonProperty("FiatOfChoice")]
        public int FiatOfChoice { get; set; }

        [JsonProperty("CheckedHashRates")]
        public Dictionary<string, bool> CheckedHashRates { get; set; }
    }

    internal class ApiSettingsJson
    {
        [JsonProperty("ApiSettings")]
        public Dictionary<string, string> ApiSettings { get; set; }

        [JsonProperty("CheckedApis")]
        public Dictionary<string, bool> CheckedApis { get; set; }
    }
}