using Newtonsoft.Json;

namespace ProfitCalc.ApiControl
{
    public class CoinDesk
    {
        /*[JsonProperty("time")]
        public Time Updated { get; set; }
        public class Time
        {
            [JsonProperty("updated")]
            public string Updated { get; set; }

            [JsonProperty("updatedISO")]
            public string UpdatedIso { get; set; }

            [JsonProperty("updateduk")]
            public string Updateduk { get; set; }
        }

        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }*/

        [JsonProperty("bpi")]
        public Bpi BpiPrice { get; set; }

        public class Bpi
        {
            [JsonProperty("USD")]
            public USD UsdPrice { get; set; }

            public class USD
            {
                /*[JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("rate")]
                public string Rate { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }*/

                [JsonProperty("rate_float")]
                public double RateFloat { get; set; }
            }

            [JsonProperty("EUR")]
            public EUR EurPrice { get; set; }

            public class EUR
            {
                /*[JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("rate")]
                public string Rate { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }*/

                [JsonProperty("rate_float")]
                public double RateFloat { get; set; }
            }

            [JsonProperty("GBP")]
            public GBP GbpPrice { get; set; }

            public class GBP
            {
                /*[JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("rate")]
                public string Rate { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }*/

                [JsonProperty("rate_float")]
                public double RateFloat { get; set; }
            }

            [JsonProperty("CNY")]
            public CNY CnyPrice { get; set; }

            public class CNY
            {
                /*[JsonProperty("code")]
                public string Code { get; set; }

                [JsonProperty("rate")]
                public string Rate { get; set; }

                [JsonProperty("description")]
                public string Description { get; set; }*/

                [JsonProperty("rate_float")]
                public double RateFloat { get; set; }
            }
        }
    }
}