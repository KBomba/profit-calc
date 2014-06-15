using System.Collections.Generic;
using Newtonsoft.Json;

namespace CudaProfitCalc.ApiControl
{
    class Cryptsy
    {
        [JsonProperty("success")]
        public int Success { get; set; }

        [JsonProperty("return")]
        public Return Returns { get; set; }
        public class Return
        {
            [JsonProperty("markets")]
            public Dictionary<string, Market> Markets { get; set; }
            public class Market
            {
                [JsonProperty("marketid")]
                public string MarketId { get; set; }

                [JsonProperty("label")]
                public string Label { get; set; }

                [JsonProperty("lasttradeprice")]
                public double LastTradePrice { get; set; }

                [JsonProperty("volume")]
                public double Volume { get; set; }

                [JsonProperty("lasttradetime")]
                public string LastTradetime { get; set; }

                [JsonProperty("primaryname")]
                public string PrimaryName { get; set; }

                [JsonProperty("primarycode")]
                public string PrimaryCode { get; set; }

                [JsonProperty("secondaryname")]
                public string SecondaryName { get; set; }

                [JsonProperty("secondarycode")]
                public string SecondaryCode { get; set; }

                [JsonProperty("recenttrades")]
                public List<Trades> RecentTrades { get; set; }
                public class Trades
                {
                    [JsonProperty("id")]
                    public string Id { get; set; }

                    [JsonProperty("time")]
                    public string Time { get; set; }

                    [JsonProperty("price")]
                    public double Price { get; set; }

                    [JsonProperty("quantity")]
                    public double Quantity { get; set; }

                    [JsonProperty("total")]
                    public double Total { get; set; }
                }

                [JsonProperty("sellorders")]
                public List<Order> SellOrders { get; set; }


                [JsonProperty("buyorders")]
                public List<Order> BuyOrders { get; set; }
                public class Order
                {
                    [JsonProperty("price")]
                    public double Price { get; set; }

                    [JsonProperty("quantity")]
                    public double Quantity { get; set; }

                    [JsonProperty("total")]
                    public double Total { get; set; }
                }
            }
        }
    }
}
