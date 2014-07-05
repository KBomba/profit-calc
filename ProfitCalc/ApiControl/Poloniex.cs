using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    internal class Poloniex
    {
        /*[JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("lowestAsk")]
        public string LowestAsk { get; set; }*/

        [JsonProperty("highestBid")]
        public double HighestBid { get; set; }

        /*[JsonProperty("percentChange")]
        public string PercentChange { get; set; }*/

        [JsonProperty("baseVolume")]
        public double BaseVolume { get; set; }

        /*[JsonProperty("quoteVolume")]
        public string QuoteVolume { get; set; }*/

        [JsonProperty("isFrozen")]
        public string IsFrozen { get; set; }
    }
}