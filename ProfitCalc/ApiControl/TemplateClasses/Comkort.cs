using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProfitCalc.ApiControl.TemplateClasses
{
    internal class Comkort
    {
        [JsonProperty("markets")]
        public Dictionary<string, Pair> Markets { get; set; }
        internal class Pair
        {

            /*[JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("alias")]
            public string Alias { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }*/

            [JsonProperty("last_price")]
            public double LastPrice { get; set; }

            /*[JsonProperty("last_trade_time")]
            public string LastTradeTime { get; set; }

            [JsonProperty("volume")]
            public string Volume { get; set; }*/

            [JsonProperty("currency_volume")]
            public double CurrencyVolume { get; set; }

            [JsonProperty("item_code")]
            public string ItemCode { get; set; }

            [JsonProperty("currency_code")]
            public string CurrencyCode { get; set; }

            /*[JsonProperty("low")]
            public string Low { get; set; }

            [JsonProperty("high")]
            public string High { get; set; }

            [JsonProperty("recent_trades")]
            public Trade[] RecentTrades { get; set; }
            internal class Trade
            {

                [JsonProperty("id")]
                public string Id { get; set; }

                [JsonProperty("amount")]
                public string Amount { get; set; }

                [JsonProperty("price")]
                public string Price { get; set; }

                [JsonProperty("total")]
                public string Total { get; set; }

                [JsonProperty("time")]
                public string Time { get; set; }

                [JsonProperty("type")]
                public string Type { get; set; }
            }*/

            [JsonProperty("sell_orders")]
            public Order[] SellOrders { get; set; }

            [JsonProperty("buy_orders")]
            public Order[] BuyOrders { get; set; }
            internal class Order
            {

                [JsonProperty("price")]
                public double Price { get; set; }

                /*[JsonProperty("type")]
                public string Type { get; set; }

                [JsonProperty("item")]
                public string Item { get; set; }

                [JsonProperty("total_price")]
                public string TotalPrice { get; set; }

                [JsonProperty("price_currency")]
                public string PriceCurrency { get; set; }

                [JsonProperty("amount")]
                public string Amount { get; set; }*/
            }
            
            /*[JsonProperty("last_update")]
            public string LastUpdate { get; set; }*/
        }
    }
}
