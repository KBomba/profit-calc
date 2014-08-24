using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    internal class AtomicTradePair
    {
        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("volume")]
        public string Volume { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    internal class AtomicTradeOrders
    {
        [JsonProperty("market")]
        public MarketData Market { get; set; }

        internal class MarketData
        {
            /*[JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("recenttrades")]
            public List<Order> Recenttrades { get; set; }*/

            [JsonProperty("sellorders")]
            public List<Order> Sellorders { get; set; }

            [JsonProperty("buyorders")]
            public List<Order> Buyorders { get; set; }

            internal class Order
            {
                /*[JsonProperty("id")]
                public string Id { get; set; }

                [JsonProperty("time")]
                public string Time { get; set; }*/

                [JsonProperty("price")]
                public string Price { get; set; }

                [JsonProperty("quantity")]
                public string Quantity { get; set; }

                [JsonProperty("total")]
                public string Total { get; set; }
            }
        }
    }
}