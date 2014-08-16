using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    internal class NiceHash
    {
        [JsonProperty("result")]
        public Result Results { get; set; }

        public class Result
        {
            [JsonProperty("stats")]
            public List<Stat> Stats { get; set; }

            public class Stat
            {
                [JsonProperty("price")]
                public double Price { get; set; }

                [JsonProperty("algo")]
                public int Algo { get; set; }

                /*[JsonProperty("speed")]
                public double Speed { get; set; }*/
            }
        }

        /*[JsonProperty("method")]
        public string Method { get; set; }*/
    }
}