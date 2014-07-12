using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    internal class CoinTweak
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("callsRemaining")]
        public int CallsRemaining { get; set; }

        /*[JsonProperty("btc")]
        public BtcInfo Btc { get; set; }
        public class BtcInfo
        {
            [JsonProperty("conversionRateToUsd")]
            public double ConversionRateToUsd { get; set; }

            [JsonProperty("exchange")]
            public string Exchange { get; set; }
        }

        [JsonProperty("ltc")]
        public LtcInfo Ltc { get; set; }
        public class LtcInfo
        {
            [JsonProperty("conversionRateToBtc")]
            public double ConversionRateToBtc { get; set; }
        }*/

        [JsonProperty("coin")] public List<Coin> Coins;

        public class Coin
        {
            [JsonProperty("coin_fullname")]
            public string CoinFullname { get; set; }

            [JsonProperty("coin_name")]
            public string CoinName { get; set; }

            [JsonProperty("difficulty")]
            public double Difficulty { get; set; }

            /*[JsonProperty("avg_diff")]
            public double AvgDiff { get; set; }*/

            [JsonProperty("blockCoins")]
            public double BlockCoins { get; set; }

            [JsonProperty("algo_name")]
            public string AlgoName { get; set; }

            [JsonProperty("hasBuyOffers")]
            public bool HasBuyOffers { get; set; }

            /*[JsonProperty("hasBuyOffersLtc")]
            public bool HasBuyOffersLtc { get; set; }

            [JsonProperty("hashRate")]
            public int HashRate { get; set; }

            [JsonProperty("daysPerBlock")]
            public double DaysPerBlock { get; set; }

            [JsonProperty("coinsPerDay")]
            public double CoinsPerDay { get; set; }

            [JsonProperty("coinsPerDayAvg")]
            public double CoinsPerDayAvg { get; set; }*/

            [JsonProperty("Btc_vol")]
            public double BtcVol { get; set; }

            [JsonProperty("ex_name")]
            public string ExName { get; set; }

            [JsonProperty("conversionRateToBtc")]
            public double ConversionRateToBtc { get; set; }

            /*[JsonProperty("conversionRateToLtc")]
            public object ConversionRateToLtc { get; set; }

            [JsonProperty("conversionRateToUsd")]
            public double ConversionRateToUsd { get; set; }

            [JsonProperty("btcPerDay")]
            public double BtcPerDay { get; set; }

            [JsonProperty("ltcPerDay")]
            public double LtcPerDay { get; set; }

            [JsonProperty("dollarPerDay")]
            public double DollarPerDay { get; set; }

            [JsonProperty("difficulty_error")]
            public string DifficultyError { get; set; }

            [JsonProperty("Ltc_vol")]
            public string LtcVol { get; set; }

            [JsonProperty("blockReward_error")]
            public string BlockRewardError { get; set; }

            [JsonProperty("Ltc_ex_name")]
            public string LtcExName { get; set; }*/
        }
    }
}