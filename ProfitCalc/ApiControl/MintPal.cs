using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    internal class MintPal
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

            /*[JsonProperty("last_price")]
            public double LastPrice { get; set; }

            [JsonProperty("yesterday_price")]
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

            /*[JsonProperty("top_ask")]
            public double TopAsk { get; set; }*/
        }
    }
}