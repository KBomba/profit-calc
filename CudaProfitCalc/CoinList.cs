using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                    if (mpCoin.Exchange == "BTC" && mpCoin.Code == c.TagName && (mpCoin.Last24hVol > c.BestExchange.BtcVolume || !c.HasImplementedMarketApi))
                    {
                        c.BestExchange = new Coin.Exchange { ExchangeName = "MintPal", BtcPrice = mpCoin.TopBid, BtcVolume = mpCoin.Last24hVol };
                        c.HasImplementedMarketApi = true;
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
                    if (cpCoin.Value.SecondaryCode == "BTC" && cpCoin.Value.PrimaryCode == c.TagName &&
                        ((cpCoin.Value.Volume*cpCoin.Value.LastTradePrice) > c.BestExchange.BtcVolume ||
                         !c.HasImplementedMarketApi) && cpCoin.Value.BuyOrders != null)
                    {
                        c.BestExchange = new Coin.Exchange
                        {
                            ExchangeName = "Cryptsy",
                            BtcPrice = cpCoin.Value.BuyOrders[0].Price,
                            BtcVolume = (cpCoin.Value.Volume*cpCoin.Value.LastTradePrice)
                        };
                        c.HasImplementedMarketApi = true;
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
                    String[] splitMarket = btCoin.MarketName.Split('-');
                    if (!String.IsNullOrEmpty(btCoin.BaseVolume) && !String.IsNullOrEmpty(btCoin.Bid))
                    {
                        double volume = Double.Parse(btCoin.BaseVolume, NumberStyles.Float, CultureInfo.InvariantCulture);
                        double price = Double.Parse(btCoin.Bid, NumberStyles.Float, CultureInfo.InvariantCulture);

                        if (splitMarket[0].Trim().ToUpper() == "BTC" && splitMarket[1].Trim().ToUpper() == c.TagName &&
                            (volume > c.BestExchange.BtcVolume || !c.HasImplementedMarketApi))
                        {
                            c.BestExchange = new Coin.Exchange { ExchangeName = "Bittrex", BtcPrice = price, BtcVolume = volume };
                            c.HasImplementedMarketApi = true;
                            break;
                        }
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
                        BestExchange =
                            new Coin.Exchange
                            {
                                ExchangeName = pool.Name,
                            }
                    };

                    c.Algo = HashAlgo.Algo.Scrypt;
                    c.CoinName = pool.Name + c.Algo;
                    c.TagName = "PPicker" + pool.Id + c.Algo;
                    c.BlockReward = pool.PoolProfitability.Scrypt.Count;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo scrypt in pool.PoolProfitability.Scrypt)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(scrypt.Btcmhs, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.BestExchange.BtcPrice = dAverage / iCounter * 1000;
                    c.BestExchange.BtcVolume = dAverage;

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
                        BestExchange =
                            new Coin.Exchange
                            {
                                ExchangeName = pool.Name,
                            }
                    };

                    c.Algo = HashAlgo.Algo.ScryptN;
                    c.CoinName = pool.Name + c.Algo;
                    c.TagName = "PPicker" + pool.Id + c.Algo;
                    c.BlockReward = pool.PoolProfitability.ScryptN.Count;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo scryptN in pool.PoolProfitability.ScryptN)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(scryptN.Btcmhs, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.BestExchange.BtcPrice = dAverage / iCounter * 1000;
                    c.BestExchange.BtcVolume = dAverage;

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
                        BestExchange =
                            new Coin.Exchange
                            {
                                ExchangeName = pool.Name,
                            }
                    };

                    c.Algo = HashAlgo.Algo.X11;
                    c.CoinName = pool.Name + c.Algo;
                    c.TagName = "PPicker" + pool.Id + c.Algo;
                    c.BlockReward = pool.PoolProfitability.X11.Count;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo x11 in pool.PoolProfitability.X11)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(x11.Btcmhs, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.BestExchange.BtcPrice = dAverage / iCounter * 1000;
                    c.BestExchange.BtcVolume = dAverage;

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
                        BestExchange =
                            new Coin.Exchange
                            {
                                ExchangeName = pool.Name,
                            }
                    };

                    c.Algo = HashAlgo.Algo.X13;
                    c.CoinName = pool.Name + c.Algo;
                    c.TagName = "PPicker" + pool.Id + c.Algo;
                    c.BlockReward = pool.PoolProfitability.X13.Count;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo x13 in pool.PoolProfitability.X13)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(x13.Btcmhs, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.BestExchange.BtcPrice = dAverage / iCounter * 1000;
                    c.BestExchange.BtcVolume = dAverage;

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
                        BestExchange =
                            new Coin.Exchange
                            {
                                ExchangeName = pool.Name,
                            }
                    };

                    c.Algo = HashAlgo.Algo.Keccak;
                    c.CoinName = pool.Name + c.Algo;
                    c.TagName = "PPicker" + pool.Id + c.Algo;
                    c.BlockReward = pool.PoolProfitability.Keccak.Count;

                    double dAverage = 0;
                    int iCounter = 0;
                    foreach (PoolPicker.Pool.Profitability.Algo keccak in pool.PoolProfitability.Keccak)
                    {
                        if (iCounter == average) break;
                        iCounter++;
                        dAverage += Double.Parse(keccak.Btcmhs, NumberStyles.Float, CultureInfo.InvariantCulture);
                    }

                    c.BestExchange.BtcPrice = dAverage / iCounter;
                    c.BestExchange.BtcVolume = dAverage;

                    Add(c);
                }
            }
        }

        public void Sort(Dictionary<HashAlgo.Algo, double> hashList)
        {
            foreach (Coin coin in List)
            {
                if (hashList.ContainsKey(coin.Algo))
                {
                    coin.CalcProfitability(hashList[coin.Algo]);
                }
                else
                {
                    coin.BtcPerDay = 0;
                    coin.CoinsPerDay = 0;
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
