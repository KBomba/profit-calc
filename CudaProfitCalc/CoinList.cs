using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CudaProfitCalc.ApiControl;

namespace CudaProfitCalc
{
    class CoinList
    {
        public List<Coin> List { get; set; }

        public CoinList(List<Coin> list)
        {
            List = list;
        }

        public CoinList()
        {
            List = new List<Coin>();
        }

        public void Add(Coin newCoin)
        {
            bool found = false;
            foreach (Coin c in List)
            {
                if (c.TagName == newCoin.TagName && c.Algo == newCoin.Algo && !newCoin.IsMultiPool)
                {
                    if (c.Difficulty != newCoin.Difficulty)
                    {
                        List.Remove(c);
                    }
                    else
                    {
                        found = true;
                    }

                    break;
                }
            }

            if (!found && newCoin.Algo != HashAlgo.Algo.Unknown)
            {
                List.Add(newCoin);
            }
        }

        public void UpdateNiceHash(string address)
        {
            NiceHash niceHashData = JsonControl.DownloadSerializedApi<NiceHash>(address);
            Add(new Coin(niceHashData.Results.Stats[0]));
            Add(new Coin(niceHashData.Results.Stats[3]));
            Add(new Coin(niceHashData.Results.Stats[4]));
            Add(new Coin(niceHashData.Results.Stats[5]));
        }

        public void UpdateWhatToMine(string address)
        {
            WhatToMine wtmData = JsonControl.DownloadSerializedApi<WhatToMine>(address);
            foreach (KeyValuePair<string, WhatToMine.Coin> wtmCoin in wtmData.Coins)
            {
                Coin c = new Coin(wtmCoin);
                if(!c.HasMarketErrors)Add(c);
            }
        }

        public void UpdateCoinTweak(string address)
        {
            CoinTweak ctwData = JsonControl.DownloadSerializedApi<CoinTweak>(address);
            foreach (CoinTweak.Coin ctwCoin in ctwData.Coins)
            {
                Coin c = new Coin(ctwCoin);
                if (!c.HasMarketErrors) Add(c);
            }
        }

        public void UpdateCoinWarz(string address)
        {
            CoinWarz cwzData = JsonControl.DownloadSerializedApi<CoinWarz>(address);
            foreach (CoinWarz.Coin cwzCoin in cwzData.Data)
            {
                Coin c = new Coin(cwzCoin);
                if (!c.HasMarketErrors) Add(c);
            }
        }

        public void UpdateMintPal(string address)
        {
            MintPal mp = JsonControl.DownloadSerializedApi<MintPal>(address);
            foreach (Coin c in List)
            {
                foreach (MintPal.Coin mpCoin in mp.Data)
                {
                    if (mpCoin.Exchange == "BTC" && mpCoin.Code == c.TagName)
                    {
                        Coin.Exchange mpExchange = new Coin.Exchange { ExchangeName = "MintPal", BtcPrice = mpCoin.TopBid, BtcVolume = mpCoin.Last24hVol };
                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(mpExchange);
                            c.TotalVolume += mpExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges[0] = mpExchange;
                            c.TotalVolume = mpExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }
                        
                        break;
                    }
                }
            }
        }

        public void UpdateCryptsy(string address)
        {
            Cryptsy cp = JsonControl.DownloadSerializedApi<Cryptsy>(address);
            foreach (Coin c in List)
            {
                foreach (KeyValuePair<string, Cryptsy.Return.Market> cpCoin in cp.Returns.Markets)
                {
                    if (cpCoin.Value.SecondaryCode == "BTC" && cpCoin.Value.PrimaryCode == c.TagName && cpCoin.Value.BuyOrders != null)
                    {
                        Coin.Exchange cpExchange = new Coin.Exchange {ExchangeName = "Cryptsy",BtcPrice = cpCoin.Value.BuyOrders[0].Price,
                            BtcVolume = (cpCoin.Value.Volume*cpCoin.Value.LastTradePrice) };
                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(cpExchange);
                            c.TotalVolume += cpExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges[0] = cpExchange;
                            c.TotalVolume = cpExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }
                        break;
                    }
                }
            }
        }

        public void UpdateBittrex(string address)
        {
            Bittrex bt = JsonControl.DownloadSerializedApi<Bittrex>(address);
            foreach (Coin c in List)
            {
                foreach (Bittrex.Result btCoin in bt.Results)
                {
                    if (!String.IsNullOrEmpty(btCoin.BaseVolume) && !String.IsNullOrEmpty(btCoin.Bid))
                    {
                        double volume = Double.Parse(btCoin.BaseVolume, NumberStyles.Float, CultureInfo.InvariantCulture);
                        double price = Double.Parse(btCoin.Bid, NumberStyles.Float, CultureInfo.InvariantCulture);

                        String[] splitMarket = btCoin.MarketName.Split('-');
                        if (splitMarket[0].Trim().ToUpper() == "BTC" && splitMarket[1].Trim().ToUpper() == c.TagName)
                        {
                            Coin.Exchange btExchange = new Coin.Exchange { ExchangeName = "Bittrex", BtcPrice = price, BtcVolume = volume };
                            if (c.HasImplementedMarketApi)
                            {
                                c.Exchanges.Add(btExchange);
                                c.TotalVolume += btExchange.BtcVolume;
                            }
                            else
                            {
                                c.Exchanges[0] = btExchange;
                                c.TotalVolume = btExchange.BtcVolume;
                                c.HasImplementedMarketApi = true;
                            }
                            break;
                        }
                    }
                }
            }
        }

        public void UpdatePoloniex(string address)
        {
            Dictionary<string, Poloniex> pol = JsonControl.DownloadSerializedApi<Dictionary<string, Poloniex>>(address);
            foreach (Coin c in List)
            {
                foreach (KeyValuePair<string, Poloniex> polCoin in pol)
                {
                    String[] splitMarket = polCoin.Key.Split('_');
                    if (splitMarket[0].Trim().ToUpper() == "BTC" && splitMarket[1].Trim().ToUpper() == c.TagName)
                    {
                        double volume = Double.Parse(polCoin.Value.BaseVolume, NumberStyles.Float, CultureInfo.InvariantCulture);
                        double price = Double.Parse(polCoin.Value.HighestBid, NumberStyles.Float, CultureInfo.InvariantCulture);

                        Coin.Exchange polExchange = new Coin.Exchange { ExchangeName = "Poloniex", BtcPrice = price, BtcVolume = volume };
                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(polExchange);
                            c.TotalVolume += polExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges[0] = polExchange;
                            c.TotalVolume = polExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }
                        break;
                    }
                }
            }
        }


        public void UpdatePoolPicker(string address, decimal average)
        {
            PoolPicker pp = JsonControl.DownloadSerializedApi<PoolPicker>(address);

            foreach (PoolPicker.Pool pool in pp.Pools)
            {
                if (pool.PoolProfitability.Scrypt != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double)average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange { ExchangeName = pool.Name, };
                    c.Exchanges.Add(ppExchange);

                    c.Algo = HashAlgo.Algo.Scrypt;
                    c.CoinName = pool.Name + " " + c.Algo;
                    c.TagName = "PP" + pool.Id + c.Algo;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo scrypt in pool.PoolProfitability.Scrypt)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(scrypt.Btc, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.Exchanges[0].BtcPrice = dAverage / iCounter * 1000;

                    Add(c);
                }

                if (pool.PoolProfitability.ScryptN != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double)average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange { ExchangeName = pool.Name, };
                    c.Exchanges.Add(ppExchange);

                    c.Algo = HashAlgo.Algo.ScryptN;
                    c.CoinName = pool.Name + " " + c.Algo;
                    c.TagName = "PP" + pool.Id + c.Algo;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo scryptN in pool.PoolProfitability.ScryptN)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(scryptN.Btc, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.Exchanges[0].BtcPrice = dAverage / iCounter * 1000;

                    Add(c);
                }

                if (pool.PoolProfitability.X11 != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double)average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange { ExchangeName = pool.Name, };
                    c.Exchanges.Add(ppExchange);

                    c.Algo = HashAlgo.Algo.X11;
                    c.CoinName = pool.Name + " " + c.Algo;
                    c.TagName = "PP" + pool.Id + c.Algo;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo x11 in pool.PoolProfitability.X11)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(x11.Btc, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.Exchanges[0].BtcPrice = dAverage / iCounter * 1000;

                    Add(c);
                }

                if (pool.PoolProfitability.X13 != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double)average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange { ExchangeName = pool.Name, };
                    c.Exchanges.Add(ppExchange);

                    c.Algo = HashAlgo.Algo.X13;
                    c.CoinName = pool.Name + " " + c.Algo;
                    c.TagName = "PP" + pool.Id + c.Algo;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo x13 in pool.PoolProfitability.X13)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(x13.Btc, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.Exchanges[0].BtcPrice = dAverage / iCounter * 1000;

                    Add(c);
                }

                if (pool.PoolProfitability.Keccak != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double)average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange { ExchangeName = pool.Name, };
                    c.Exchanges.Add(ppExchange);

                    c.Algo = HashAlgo.Algo.Keccak;
                    c.CoinName = pool.Name + " " + c.Algo;
                    c.TagName = "PP" + pool.Id + c.Algo;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo keccak in pool.PoolProfitability.Keccak)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(keccak.Btc, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.Exchanges[0].BtcPrice = dAverage / iCounter;

                    Add(c);
                }
            }
        }

        public void Sort(Dictionary<HashAlgo.Algo, double> hashList, bool useWeightedCalculation, string coindeskAddress, string coindeskCnyAdress)
        {
            CoinDesk cd = JsonControl.DownloadSerializedApi<CoinDesk>(coindeskAddress);
            double usdPrice = cd.BpiPrice.UsdPrice.RateFloat;
            double eurPrice = cd.BpiPrice.EurPrice.RateFloat;
            double gbpPrice = cd.BpiPrice.GbpPrice.RateFloat;

            cd = JsonControl.DownloadSerializedApi<CoinDesk>(coindeskCnyAdress);
            double cnyPrice = cd.BpiPrice.CnyPrice.RateFloat;

            foreach (Coin coin in List)
            {
                if (hashList.ContainsKey(coin.Algo))
                {
                    coin.CalcProfitability(hashList[coin.Algo], useWeightedCalculation);
                    coin.UsdPerDay = coin.BtcPerDay * usdPrice;
                    coin.EurPerDay = coin.BtcPerDay * eurPrice;
                    coin.GbpPerDay = coin.BtcPerDay * gbpPrice;
                    coin.CnyPerDay = coin.BtcPerDay * cnyPrice;
                }
            }

            List = List.OrderByDescending(o => o.BtcPerDay).ToList();
        }

        public void Sort(Dictionary<HashAlgo.Algo, double> hashList, bool useWeightedCalculation)
        {
            foreach (Coin coin in List)
            {
                if (hashList.ContainsKey(coin.Algo))
                {
                    coin.CalcProfitability(hashList[coin.Algo], useWeightedCalculation);
                    coin.UsdPerDay = 0;
                }
            }

            List = List.OrderByDescending(o => o.BtcPerDay).ToList();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Coin c in List)
            {
                sb.Append(c + Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
