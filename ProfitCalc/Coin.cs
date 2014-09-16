using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProfitCalc.ApiControl.TemplateClasses;

namespace ProfitCalc
{
    public class Coin
    {
        public string FullName { get; set; }
        public string TagName { get; set; }
        public string Algo { get; set; }

        public double Difficulty { get; set; }
        public double Avg24HDifficulty { get;set;}
        public double BlockReward { get; set; }
        public double BlockTime { get; set; }
        public double NetHashRate { get; set; }
        public uint Height { get; set; }

        public List<Exchange> Exchanges { get; set; }
        public Exchange TotalExchange { get; set; }
        public class Exchange
        {
            public string ExchangeName { get; set; }
            public double BtcPrice { get; set; }
            public double BtcVolume { get; set; }
            public double Weight { get; set; }
            public bool IsFrozen { get; set; }

            public double FallThroughPrice { get; set; }
            public double LeftOverInFallThrough { get; set; }
            public List<Order> BuyOrders { get; set; }
            public List<Order> SellOrders { get; set; }
            public class Order
            {
                public double BtcPrice { get; set; }
                public double BtcVolume { get; set; }
                public double CoinVolume { get; set; }
            }

            public override string ToString()
            {
                return ExchangeName + " | BTC Price: " + BtcPrice + " | BTC Volume: " + BtcVolume;
            }
        }

        // Following is for the sake of datagridviews <3
        public double TotalVolume { get; set; }
        public string BestExchangeName { get; set; }
        public double BestExchangePrice { get; set; }
        public double BestExchangeVolume { get; set; }

        public double WeightedBtcPrice { get; set; }
        
        public double CoinsPerDay { get; set; }
        public double BtcPerDay { get; set; }

        public double UsdPerDay { get; set; }
        public double EurPerDay { get; set; }
        public double GbpPerDay { get; set; }
        public double CnyPerDay { get; set; }

        public string Source { get; set; }
        public DateTime Retrieved { get; set; }

        // Easy identifiers to filter it out later
        public bool HasImplementedMarketApi { get; set; }
        public bool IsMultiPool;
        public bool UsedInCoinlist { get; set; }

        public Coin()
        {
            Exchanges = new List<Exchange>();
        }

        public Coin(CustomCoin customCoin)
        {
            FullName = customCoin.FullName;
            TagName = customCoin.Tag;
            Algo = customCoin.Algo;
            Difficulty = customCoin.Difficulty;
            BlockReward = customCoin.BlockReward;
            BlockTime = customCoin.BlockTime;
            NetHashRate = customCoin.NetHashRate*1000000;
            Height = customCoin.Height;
            Exchange customExchange = new Exchange
            {
                ExchangeName = "Unknown (Custom coin)",
                BtcPrice = customCoin.CustomPrice,
                BtcVolume = 0,
                Weight = 1,
                IsFrozen = false
            };
            Exchanges = new List<Exchange>{customExchange};
            TotalExchange = customExchange;
            Source = "Custom coin";
            Retrieved = DateTime.Now;
            IsMultiPool = false;
            HasImplementedMarketApi = false;
        }

        public Coin(NiceHash.Result.Stat niceHashStat, string name)
        {
            switch (niceHashStat.Algo)
            {
                case 0:
                    Algo = "SCRYPT";
                    break;
                case 1:
                    Algo = "SHA256";
                    break;
                case 2:
                    Algo = "SCRYPTN";
                    break;
                case 3:
                    Algo = "X11";
                    break;
                case 4:
                    Algo = "X13";
                    break;
                case 5:
                    Algo = "KECCAK";
                    break;
                case 6:
                    Algo = "X15";
                    break;
                case 7:
                    Algo = "NIST5";
                    break;
                default:
                    Algo = "UNKNOWN";
                    break;
            }

            FullName = "Act. " + name + " " + Algo;
            TagName = name.Substring(0, 4).ToUpper() + Algo;
            Difficulty = 0;
            BlockReward = 0;

            Exchange nhExchange = new Exchange
            {
                ExchangeName = name,
                BtcPrice = niceHashStat.Algo == 1 ? niceHashStat.Price /1000: niceHashStat.Price,
                BtcVolume = 0,
                Weight = 1,
                IsFrozen = false
            };
            Exchanges = new List<Exchange> {nhExchange};
            TotalExchange = nhExchange;
            Source = "NiceHash";
            Retrieved = DateTime.Now;
            IsMultiPool = true;
            HasImplementedMarketApi = true;
        }

        public Coin(KeyValuePair<string, WhatToMine.Coin> wtmCoin)
        {
            FullName = wtmCoin.Key;
            TagName = wtmCoin.Value.Tag.ToUpper();
            Algo = wtmCoin.Value.Algorithm.ToUpperInvariant();
            if (TagName == "MYR" && Algo == "GROESTL")
            {
                Algo = "MYRIADGROESTL";
            }

            Difficulty = wtmCoin.Value.Difficulty;
            Avg24HDifficulty = wtmCoin.Value.Difficulty24;
            BlockReward = wtmCoin.Value.BlockReward;
            BlockTime = wtmCoin.Value.BlockTime;
            NetHashRate = wtmCoin.Value.Nethash;
            Height = wtmCoin.Value.LastBlock;

            Exchange wtmExchange = new Exchange
            {
                ExchangeName = "Unknown (WhatToMine)",
                BtcPrice = wtmCoin.Value.ExchangeRate,
                BtcVolume = wtmCoin.Value.Volume*wtmCoin.Value.ExchangeRate,
                Weight = 1,
                IsFrozen = false
            };
            Exchanges = new List<Exchange> {wtmExchange};
            TotalExchange = wtmExchange;
            Source = "WhatToMine";
            Retrieved = DateTime.Now;
            IsMultiPool = false;
            HasImplementedMarketApi = false;
        }

        public Coin(CoinTweak.Coin ctwCoin)
        {
            FullName = ctwCoin.CoinFullname;
            TagName = ctwCoin.CoinName.ToUpper();
            Algo = ctwCoin.AlgoName.ToUpperInvariant();
            if (TagName == "MYR" && Algo == "GROESTL")
            {
                Algo = "MYRIADGROESTL";
            }

            Difficulty = ctwCoin.Difficulty;
            Avg24HDifficulty = ctwCoin.AvgDiff;
            BlockReward = ctwCoin.BlockCoins;

            Exchange ctwExchange = new Exchange
            {
                ExchangeName = ctwCoin.ExName + " (CoinTweak)",
                BtcPrice = ctwCoin.ConversionRateToBtc,
                BtcVolume = ctwCoin.BtcVol,
                Weight = 1,
                IsFrozen = !ctwCoin.HasBuyOffers
            };
            Exchanges = new List<Exchange> {ctwExchange};
            TotalExchange = ctwExchange;
            Source = "CoinTweak";
            Retrieved = DateTime.Now;
            IsMultiPool = false;
            HasImplementedMarketApi = false;
        }

        public Coin(CoinWarz.Coin cwzCoin)
        {
            FullName = cwzCoin.CoinName;
            TagName = cwzCoin.CoinTag.ToUpper();
            if (FullName == "Starcoin" && TagName == "STR") 
            {
                TagName = "STAR";
            }
            Algo = cwzCoin.Algorithm.ToUpperInvariant();
            if (TagName == "MYR" && Algo == "GROESTL")
            {
                Algo = "MYRIADGROESTL";
            }

            Difficulty = cwzCoin.Difficulty;
            BlockReward = cwzCoin.BlockReward;
            BlockTime = cwzCoin.BlockTimeInSeconds;
            Height = cwzCoin.BlockCount;

            Exchange cwzExchange = new Exchange
            {
                ExchangeName = cwzCoin.Exchange + " (CoinWarz)",
                BtcPrice = cwzCoin.ExchangeRate,
                BtcVolume = cwzCoin.ExchangeVolume*cwzCoin.ExchangeRate,
                Weight = 1,
                IsFrozen = cwzCoin.HealthStatus.Contains("Unhealthy")
            };
            Exchanges = new List<Exchange> {cwzExchange};
            TotalExchange = cwzExchange;
            Source = "CoinWarz";
            Retrieved = DateTime.Now;
            IsMultiPool = false;
            HasImplementedMarketApi = false;
        }

        public void CalcProfitability(double hashRateMh, bool useWeightedCalculations, bool useFallThroughPrice,
            int multiplier, string diffCalculationCalcStyle, double target, bool use24HDiff)
        {
            if (IsMultiPool)
            {
                CoinsPerDay = 0;
                BtcPerDay = ((hashRateMh/1000)*Exchanges[0].BtcPrice) * multiplier;
            }
            else
            {
                // Calculating coins/day
                double diff = use24HDiff && Avg24HDifficulty != 0 ? Avg24HDifficulty : Difficulty;
                switch (diffCalculationCalcStyle)
                {
                    case "CryptoNight":
                        //Cryptonight's difficulty is net hashrate * 60
                        CoinsPerDay = (BlockReward * ((24 * 60 * 60) / BlockTime)) 
                            * ((hashRateMh * 1000000) / (diff / 60)) * multiplier;
                        break;
                    case "NetHashRate":
                        double netHash = NetHashRate != 0
                            ? NetHashRate
                            : diff*Math.Pow(2, target)/BlockTime;
                        CoinsPerDay = (BlockReward * ((24 * 60 * 60) / BlockTime))
                            * ((hashRateMh * 1000000) / netHash) * multiplier;
                        break;
                    //case "Classic":
                    default:
                        CoinsPerDay = (BlockReward / (diff* Math.Pow(2, target)
                            / (hashRateMh * 1000000) / 3600 / 24)) * multiplier;
                        break;
                }

                // Calculating weighted and fallthrough prices
                foreach (Exchange exchange in Exchanges)
                {
                    if (TotalExchange.BtcVolume > 0)
                    {
                        exchange.Weight = exchange.BtcVolume/TotalExchange.BtcVolume;
                        WeightedBtcPrice += (exchange.Weight*exchange.BtcPrice);
                    }

                    Tuple<double, double> fallthroughPrices = GetFallThroughPrices(exchange.BuyOrders);
                    exchange.FallThroughPrice = fallthroughPrices.Item1;
                    exchange.LeftOverInFallThrough = fallthroughPrices.Item2;
                }

                // Sorting the orders in the right way
                // And adding to total orders
                foreach (Exchange exchange in Exchanges)
                {
                    if (exchange.BuyOrders != null)
                    {
                        exchange.BuyOrders = 
                            exchange.BuyOrders.OrderByDescending(order => order.BtcPrice).ToList();
                        TotalExchange.BuyOrders = 
                            AddToTotalOrders(exchange.BuyOrders, TotalExchange.BuyOrders);
                    }

                    if (exchange.SellOrders != null)
                    {
                        exchange.SellOrders = 
                            exchange.SellOrders.OrderBy(order => order.BtcPrice).ToList();
                        TotalExchange.SellOrders = 
                            AddToTotalOrders(exchange.SellOrders, TotalExchange.SellOrders);
                    }
                }

                if (TotalExchange.BuyOrders != null)
                {
                    TotalExchange.BuyOrders = 
                        TotalExchange.BuyOrders.OrderByDescending(order => order.BtcPrice).ToList();
                }
                if (TotalExchange.SellOrders != null)
                {
                    TotalExchange.SellOrders = 
                        TotalExchange.SellOrders.OrderBy(order => order.BtcPrice).ToList();
                }

                Tuple<double,double> totalFallthroughPrices = GetFallThroughPrices(TotalExchange.BuyOrders);
                TotalExchange.FallThroughPrice = totalFallthroughPrices.Item1;
                TotalExchange.LeftOverInFallThrough = totalFallthroughPrices.Item2;

                Exchanges = useFallThroughPrice
                    ? Exchanges.OrderByDescending(exchange => exchange.FallThroughPrice).ToList()
                    : Exchanges.OrderByDescending(exchange => exchange.BtcVolume).ToList();

                double priceToUse = TotalExchange.BtcVolume > 0 && useWeightedCalculations
                    ? WeightedBtcPrice
                    : (useFallThroughPrice && Exchanges[0].FallThroughPrice > 0
                    ? Exchanges[0].FallThroughPrice 
                    : Exchanges[0].BtcPrice);
                BtcPerDay = CoinsPerDay*priceToUse;

                // Only the brave remove the following line
                if (TagName == "BTC") BtcPerDay = CoinsPerDay;

                TotalVolume = TotalExchange.BtcVolume;
                BestExchangeName = Exchanges[0].ExchangeName;
                BestExchangePrice = Exchanges[0].BtcPrice;
                BestExchangeVolume = Exchanges[0].BtcVolume;
            }
        }

        private Tuple<double, double> GetFallThroughPrices(List<Exchange.Order> buyOrders)
        {
            double leftOver = 0;
            double fallThroughPrice = 0;

            if (buyOrders != null && buyOrders.Any())
            {
                double coinsToSell = CoinsPerDay;
                double collectedBtc = 0;
                
                foreach (Exchange.Order order in buyOrders)
                {
                    if (coinsToSell - order.CoinVolume < 0)
                    {
                        collectedBtc += coinsToSell * order.BtcPrice;
                        leftOver = 0;
                        break;
                    }

                    coinsToSell -= order.CoinVolume;
                    collectedBtc += order.CoinVolume * order.BtcPrice;
                    leftOver = coinsToSell;
                }

                fallThroughPrice = collectedBtc / (CoinsPerDay - leftOver);
            }

            return new Tuple<double, double>(fallThroughPrice, leftOver);
        }

        private List<Exchange.Order> AddToTotalOrders(List<Exchange.Order> orders,
            List<Exchange.Order> totalOrders)
        {
            if (orders != null && orders.Any())
            {
                if (totalOrders == null)
                {
                    totalOrders = new List<Exchange.Order>();
                }

                foreach (Exchange.Order newOrder in orders)
                {
                    bool found = false;
                    foreach (Exchange.Order order in totalOrders)
                    {
                        if (newOrder.BtcPrice == order.BtcPrice)
                        {
                            found = true;
                            order.BtcVolume += newOrder.BtcVolume;
                            order.CoinVolume += newOrder.CoinVolume;
                            break;
                        }
                    }

                    if (!found)
                    {
                        totalOrders.Add(newOrder);
                    }
                }
            }

            return totalOrders;
        }

        public override string ToString()
        {
            return "TAG: " + TagName + " | Name:" + FullName + " | Algo: " + Algo + " | BTC/day: " +
                   BtcPerDay.ToString("#.00000000") + " | Coins/day: " + CoinsPerDay.ToString("#.00000000") + 
                   GetExchanges() + 
                   " | Weighted price: " + WeightedBtcPrice.ToString("#.00000000") + " | Total volume: " +
                   TotalExchange.BtcVolume.ToString("#.0000") + " | Difficulty: " + Difficulty.ToString("#.###") + 
                   " | Blockreward: " + BlockReward.ToString("#.###");
        }

        public string GetExchanges()
        {
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < Exchanges.Count; index++)
            {
                sb.Append(" | Exchange #").Append(index + 1).Append(": ").Append(Exchanges[index].ExchangeName)
                    .Append(" | BTC volume: ").Append(Exchanges[index].BtcVolume.ToString("#.0000"))
                    .Append(" | BTC price: ").Append(Exchanges[index].BtcPrice.ToString("#.00000000"))
                    .Append(" | Weight: ").Append(Exchanges[index].Weight.ToString("#.000"));
            }

            return sb.ToString();
        }
    }
}