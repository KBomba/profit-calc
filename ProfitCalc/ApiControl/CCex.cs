using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    internal class CCexPair
    {
        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("avg")]
        public double Avg { get; set; }

        [JsonProperty("lastbuy")]
        public double Lastbuy { get; set; }

        [JsonProperty("lastsell")]
        public double Lastsell { get; set; }

        [JsonProperty("buy")]
        public double Buy { get; set; }

        [JsonProperty("sell")]
        public double Sell { get; set; }

        [JsonProperty("lastprice")]
        public double Lastprice { get; set; }

        [JsonProperty("updated")]
        public int Updated { get; set; }
    }

    internal class CCexVolume
    {
        [JsonProperty("return")]
        public List<Dictionary<string, string>> Returns { get; set; }
    }
}
