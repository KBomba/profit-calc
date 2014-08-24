using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    internal class PoloniexPairs
    {
        [JsonProperty("last")]
        public double Last { get; set; }

        [JsonProperty("lowestAsk")]
        public double LowestAsk { get; set; }

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

    internal class PoloniexOrders
    {
        [JsonProperty("asks")]
        public double[][] Asks { get; set; }

        [JsonProperty("bids")]
        public double[][] Bids { get; set; }

        /*[JsonProperty("isFrozen")]
        public string IsFrozen { get; set; }*/
    }
}