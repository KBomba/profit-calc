using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    internal class CrypToday
    {
        [JsonProperty("cols")]
        public List<Col> Cols { get; set; }
        public class Col
        {

            /*[JsonProperty("id")]
            public string Id { get; set; }*/

            [JsonProperty("label")]
            public string Label { get; set; }

            /*[JsonProperty("pattern")]
            public string Pattern { get; set; }*/

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        [JsonProperty("rows")]
        public List<Row> Rows { get; set; }
        public class Row
        {

            [JsonProperty("c")]
            public List<Result> Results { get; set; }
            public class Result
            {

                [JsonProperty("v")]
                public string Btc { get; set; }
            }
        }
    }
}