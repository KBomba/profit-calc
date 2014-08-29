using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    internal class CCexPair
    {
        /*[JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("avg")]
        public double Avg { get; set; }

        [JsonProperty("lastbuy")]
        public double Lastbuy { get; set; }

        [JsonProperty("lastsell")]
        public double Lastsell { get; set; }*/

        [JsonProperty("buy")]
        public double Buy { get; set; }

        [JsonProperty("sell")]
        public double Sell { get; set; }

        [JsonProperty("lastprice")]
        public double Lastprice { get; set; }

        /*[JsonProperty("updated")]
        public int Updated { get; set; }*/
    }

    internal class CCexVolume
    {
        [JsonProperty("return")]
        public List<Dictionary<string, string>> Returns { get; set; }
    }

    internal class CCexOrders
    {
        [JsonProperty("return")]
        public Dictionary<uint, CCexOrder> Return { get; set; }
        internal class CCexOrder
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            /*[JsonProperty("c1")]
            public string C1 { get; set; }

            [JsonProperty("c2")]
            public string C2 { get; set; }*/

            [JsonProperty("amount")]
            public double Amount { get; set; }

            [JsonProperty("price")]
            public double Price { get; set; }

            /*[JsonProperty("self")]
            public int Self { get; set; }*/
        }
    }
}
