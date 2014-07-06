using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    internal class PoolPicker
    {
        [JsonProperty("pools")]
        public List<Pool> Pools { get; set; }

        public class Pool
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            /*[JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("note")]
            public string Note { get; set; }*/

            [JsonProperty("rating")]
            public string Rating { get; set; }

            /*[JsonProperty("reviews")]
            public int Reviews { get; set; }*/

            [JsonProperty("profitability")]
            public Profitability PoolProfitability { get; set; }

            public class Profitability
            {
                [JsonProperty("Scrypt")]
                public List<Algo> Scrypt { get; set; }

                [JsonProperty("ScryptN")]
                public List<Algo> ScryptN { get; set; }

                [JsonProperty("X11")]
                public List<Algo> X11 { get; set; }

                /*[JsonProperty("SHA256")]
                public List<Algo> Sha256 { get; set; }*/

                [JsonProperty("X13")]
                public List<Algo> X13 { get; set; }

                [JsonProperty("Keccak")]
                public List<Algo> Keccak { get; set; }

                public class Algo
                {
                    [JsonProperty("date")]
                    public string Date { get; set; }

                    [JsonProperty("btc")]
                    public double Btc { get; set; }
                }
            }
        }
    }
}