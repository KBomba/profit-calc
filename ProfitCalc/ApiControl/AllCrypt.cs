using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    public class AllCrypt
    {
        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("return")]
        public Return Returns { get; set; }

        public class Return
        {
            [JsonProperty("markets")]
            public Dictionary<string, Coin> Markets { get; set; }

            public class Coin
            {
                /*[JsonProperty("marketid")]
                public string Marketid { get; set; }

                [JsonProperty("label")]
                public string Label { get; set; }

                [JsonProperty("primaryname")]
                public string PrimaryName { get; set; }*/

                [JsonProperty("primarycode")]
                public string PrimaryCode { get; set; }

                /*[JsonProperty("secondaryname")]
                public string SecondaryName { get; set; }*/

                [JsonProperty("secondarycode")]
                public string SecondaryCode { get; set; }

                /*[JsonProperty("lasttradeprice")]
                public string LastTradePrice { get; set; }

                [JsonProperty("lasttradetime")]
                public string LastTradeTime { get; set; }

                [JsonProperty("volume")]
                public string Volume { get; set; }*/

                [JsonProperty("high_buy")]
                public double HighBuy { get; set; }

                /*[JsonProperty("low_sell")]
                public string LowSell { get; set; }*/

                [JsonProperty("volume_by_pair")]
                public double VolumeByPair { get; set; }
            }
        }
    }
}