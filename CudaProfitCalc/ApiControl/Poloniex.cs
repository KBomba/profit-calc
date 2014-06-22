using Newtonsoft.Json;

namespace CudaProfitCalc.ApiControl
{
    class Poloniex
    {
        /*[JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("lowestAsk")]
        public string LowestAsk { get; set; }*/

        [JsonProperty("highestBid")]
        public string HighestBid { get; set; }

        /*[JsonProperty("percentChange")]
        public string PercentChange { get; set; }*/

        [JsonProperty("baseVolume")]
        public string BaseVolume { get; set; }

        /*[JsonProperty("quoteVolume")]
        public string QuoteVolume { get; set; }

        [JsonProperty("isFrozen")]
        public string IsFrozen { get; set; }*/
    }
}
