using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    class Cryptoine
    {
        /*[JsonProperty("result")]
        public bool Result { get; set; }*/

        [JsonProperty("data")]
        public Dictionary<string, CryptoineCoin> Data { get; set; }
        internal class CryptoineCoin
        {
            /*[JsonProperty("open")]
            public double Open { get; set; }*/

            [JsonProperty("last")]
            public double Last { get; set; }

            /*[JsonProperty("high")]
            public string High { get; set; }

            [JsonProperty("low")]
            public string Low { get; set; }*/

            [JsonProperty("buy")]
            public string Buy { get; set; }

            [JsonProperty("sell")]
            public string Sell { get; set; }

            [JsonProperty("vol_base")]
            public double VolBase { get; set; }

            /*[JsonProperty("vol_exchange")]
            public double VolExchange { get; set; }*/
        }
    }
}
