﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    internal class AllCoinPairs
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, Coin> Data { get; set; }

        public class Coin
        {
            /*[JsonProperty("volume_24h_**")]
            public string Volume24h** { get; set; }*/

            [JsonProperty("volume_24h_BTC")]
            public string Volume24HBtc { get; set; }

            /*[JsonProperty("avg_24h")]
            public string Avg24H { get; set; }

            [JsonProperty("change_24h")]
            public string Change24H { get; set; }

            [JsonProperty("min_24h_price")]
            public string Min24HPrice { get; set; }

            [JsonProperty("max_24h_price")]
            public string Max24HPrice { get; set; }*/

            [JsonProperty("trade_price")]
            public string TradePrice { get; set; }

            /*[JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("exchange")]
            public string Exchange { get; set; }

            [JsonProperty("type_volume")]
            public string TypeVolume { get; set; }

            [JsonProperty("exchange_volume")]
            public string ExchangeVolume { get; set; }*/

            [JsonProperty("top_bid")]
            public string TopBid { get; set; }

            [JsonProperty("top_ask")]
            public string TopAsk { get; set; }

            /*[JsonProperty("name")]
            public string Name { get; set; }*/

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("wallet_status")]
            public string WalletStatus { get; set; }
        }
    }

    internal class AllCoinOrders
    {
        /*[JsonProperty("code")]
        public int Code { get; set; }*/

        [JsonProperty("data")]
        public Datas Data { get; set; }
        internal class Datas
        {
            [JsonProperty("sell")]
            public Dictionary<string, double> Sell { get; set; }

            [JsonProperty("buy")]
            public Dictionary<string, double> Buy { get; set; }
        }
    }
}