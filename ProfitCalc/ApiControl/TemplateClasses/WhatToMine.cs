using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    internal class WhatToMine
    {
        [JsonProperty("coins")] 
        public Dictionary<string, Coin> Coins;
        public class Coin
        {
            [JsonProperty("tag")]
            public string Tag { get; set; }

            [JsonProperty("algorithm")]
            public string Algorithm { get; set; }

            [JsonProperty("block_reward")]
            public double BlockReward { get; set; }

            [JsonProperty("block_time")]
            public double BlockTime { get; set; }

            [JsonProperty("last_block")]
            public uint LastBlock { get; set; }

            [JsonProperty("difficulty")]
            public double Difficulty { get; set; }

            [JsonProperty("difficulty24")]
            public double Difficulty24 { get; set; }

            [JsonProperty("nethash")]
            public double Nethash { get; set; }

            [JsonProperty("exchange_rate")]
            public double ExchangeRate { get; set; }

            /*[JsonProperty("market_cap")]
            public string MarketCap { get; set; }*/

            [JsonProperty("volume")]
            public double Volume { get; set; }

            /*[JsonProperty("profitability")]
            public int Profitability { get; set; }

            [JsonProperty("profitability24")]
            public int Profitability24 { get; set; }*/
        }
    }
}