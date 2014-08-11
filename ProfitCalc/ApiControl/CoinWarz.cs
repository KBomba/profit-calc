using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    internal class CoinWarz
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public List<Coin> Data { get; set; }

        public class Coin
        {
            [JsonProperty("CoinName")]
            public string CoinName { get; set; }

            [JsonProperty("CoinTag")]
            public string CoinTag { get; set; }

            [JsonProperty("Algorithm")]
            public string Algorithm { get; set; }

            [JsonProperty("Difficulty")]
            public double Difficulty { get; set; }

            [JsonProperty("BlockReward")]
            public double BlockReward { get; set; }

            [JsonProperty("BlockCount")]
            public uint BlockCount { get; set; }

            /*[JsonProperty("ProfitRatio")]
            public double ProfitRatio { get; set; }

            [JsonProperty("AvgProfitRatio")]
            public double AvgProfitRatio { get; set; }*/

            [JsonProperty("Exchange")]
            public string Exchange { get; set; }

            [JsonProperty("ExchangeRate")]
            public double ExchangeRate { get; set; }

            [JsonProperty("ExchangeVolume")]
            public double ExchangeVolume { get; set; }

            /*[JsonProperty("IsBlockExplorerOnline")]
            public bool IsBlockExplorerOnline { get; set; }

            [JsonProperty("IsExchangeOnline")]
            public bool IsExchangeOnline { get; set; }

            [JsonProperty("Message")]
            public string Message { get; set; }*/

            [JsonProperty("BlockTimeInSeconds")]
            public double BlockTimeInSeconds { get; set; }

            [JsonProperty("HealthStatus")]
            public string HealthStatus { get; set; }
        }
    }
}