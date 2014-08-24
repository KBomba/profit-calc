using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    internal class MintPalPairs
    {
        /*[JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }*/

        [JsonProperty("data")]
        public List<Coin> Data { get; set; }

        public class Coin
        {
            /*[JsonProperty("market_id")]
            public string MarketId { get; set; }

            [JsonProperty("coin")]
            public string CoinName { get; set; }*/

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("exchange")]
            public string Exchange { get; set; }

            [JsonProperty("last_price")]
            public double LastPrice { get; set; }

            /*[JsonProperty("yesterday_price")]
            public double YesterdayPrice { get; set; }

            [JsonProperty("change")]
            public double Change { get; set; }

            [JsonProperty("24hhigh")]
            public double Last24hHigh { get; set; }

            [JsonProperty("24hlow")]
            public double Last24hLow { get; set; }*/

            [JsonProperty("24hvol")]
            public double Last24HVol { get; set; }

            [JsonProperty("top_bid")]
            public double TopBid { get; set; }

            [JsonProperty("top_ask")]
            public double TopAsk { get; set; }
        }
    }

    internal class MintPalOrders
    {
        /*[JsonProperty("status")]
        public string Status { get; set; }*/

        [JsonProperty("data")]
        public Datas[] Data { get; set; }
        internal class Datas
        {

            [JsonProperty("type")]
            public string Type { get; set; }

            /*[JsonProperty("count")]
            public int Count { get; set; }*/

            [JsonProperty("orders")]
            public Order[] Orders { get; set; }
            internal class Order
            {

                [JsonProperty("price")]
                public string Price { get; set; }

                [JsonProperty("amount")]
                public string Amount { get; set; }

                [JsonProperty("total")]
                public string Total { get; set; }
            }
        }
    }
}