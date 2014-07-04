using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfitCalc.ApiControl;

namespace ProfitCalc
{
    internal class CoinList
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
                    if (c.Height < newCoin.Height)
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
            Add(new Coin(niceHashData.Results.Stats[6]));
            Add(new Coin(niceHashData.Results.Stats[7]));
        }

        public void UpdateWhatToMine(string address)
        {
            WhatToMine wtmData = JsonControl.DownloadSerializedApi<WhatToMine>(address);
            foreach (KeyValuePair<string, WhatToMine.Coin> wtmCoin in wtmData.Coins)
            {
                Coin c = new Coin(wtmCoin);
                Add(c);
            }
        }

        public void UpdateCoinTweak(string address)
        {
            CoinTweak ctwData = JsonControl.DownloadSerializedApi<CoinTweak>(address);
            foreach (CoinTweak.Coin ctwCoin in ctwData.Coins)
            {
                Coin c = new Coin(ctwCoin);
                Add(c);
            }
        }

        public void UpdateCoinWarz(string address)
        {
            CoinWarz cwzData = JsonControl.DownloadSerializedApi<CoinWarz>(address);
            foreach (CoinWarz.Coin cwzCoin in cwzData.Data)
            {
                Coin c = new Coin(cwzCoin);
                Add(c);
            }
        }

        public void UpdateMintPal(string address)
        {
            MintPal mp = JsonControl.DownloadSerializedApi<MintPal>(address);
            Parallel.ForEach(List, c =>
            {
                foreach (MintPal.Coin mpCoin in mp.Data)
                {
                    if (mpCoin.Exchange == "BTC" && mpCoin.Code == c.TagName)
                    {
                        Coin.Exchange mpExchange = new Coin.Exchange
                        {
                            ExchangeName = "MintPal",
                            BtcPrice = mpCoin.TopBid,
                            BtcVolume = mpCoin.Last24HVol
                        };
                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(mpExchange);
                            c.TotalVolume += mpExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges = new List<Coin.Exchange> {mpExchange};
                            c.TotalVolume = mpExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }

                        break;
                    }
                }
            });
        }

        public void UpdateCryptsy(string address)
        {
            Cryptsy cp = JsonControl.DownloadSerializedApi<Cryptsy>(address);
            Parallel.ForEach(List, c =>
            {
                foreach (KeyValuePair<string, Cryptsy.Return.Market> cpCoin in cp.Returns.Markets)
                {
                    if (cpCoin.Value.SecondaryCode == "BTC" && cpCoin.Value.PrimaryCode == c.TagName &&
                        cpCoin.Value.BuyOrders != null)
                    {
                        Coin.Exchange cpExchange = new Coin.Exchange
                        {
                            ExchangeName = "Cryptsy",
                            BtcPrice = cpCoin.Value.BuyOrders[0].Price
                        };
                        cpExchange.BtcVolume = (cpCoin.Value.Volume*cpExchange.BtcPrice);

                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(cpExchange);
                            c.TotalVolume += cpExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges = new List<Coin.Exchange> {cpExchange};
                            c.TotalVolume = cpExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }
                        break;
                    }
                }
            });
        }

        public void UpdateBittrex(string address)
        {
            Bittrex bt = JsonControl.DownloadSerializedApi<Bittrex>(address);
            foreach (Coin c in List)
            {
                foreach (Bittrex.Result btCoin in bt.Results)
                {
                    String[] splitMarket = btCoin.MarketName.Split('-');
                    if (splitMarket[0].Trim().ToUpper() == "BTC" && splitMarket[1].Trim().ToUpper() == c.TagName)
                    {
                        Coin.Exchange btExchange = new Coin.Exchange
                        {
                            ExchangeName = "Bittrex",
                            BtcPrice = btCoin.Bid,
                            BtcVolume = btCoin.BaseVolume
                        };

                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(btExchange);
                            c.TotalVolume += btExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges = new List<Coin.Exchange> {btExchange};
                            c.TotalVolume = btExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }
                        break;
                    }
                }
            }
        }

        public void UpdatePoloniex(string address)
        {
            Dictionary<string, Poloniex> pol = JsonControl.DownloadSerializedApi<Dictionary<string, Poloniex>>(address);
            Parallel.ForEach(List, c =>
            {
                foreach (KeyValuePair<string, Poloniex> polCoin in pol)
                {
                    String[] splitMarket = polCoin.Key.Split('_');
                    if (splitMarket[0].Trim().ToUpper() == "BTC" && splitMarket[1].Trim().ToUpper() == c.TagName)
                    {
                        Coin.Exchange polExchange = new Coin.Exchange
                        {
                            ExchangeName = "Poloniex",
                            BtcPrice = polCoin.Value.HighestBid,
                            BtcVolume = polCoin.Value.BaseVolume
                        };

                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(polExchange);
                            c.TotalVolume += polExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges = new List<Coin.Exchange> {polExchange};
                            c.TotalVolume = polExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }

                        if (polCoin.Value.IsFrozen == "1") c.HasMarketErrors = true;
                        break;
                    }
                }
            });
        }

        public void UpdateAllCoin(string address)
        {
            AllCoin ac = JsonControl.DownloadSerializedApi<AllCoin>(address);
            Parallel.ForEach(List, c =>
            {
                foreach (KeyValuePair<string, AllCoin.Coin> acCoin in ac.Data)
                {
                    String[] splitMarket = acCoin.Key.Split('_');
                    if (splitMarket[1].Trim().ToUpper() == "BTC" && splitMarket[0].Trim().ToUpper() == c.TagName)
                    {
                        double volume, price;

                        if (
                            Double.TryParse(acCoin.Value.Volume24HBtc, NumberStyles.Float, CultureInfo.InvariantCulture,
                                out volume) &&
                            Double.TryParse(acCoin.Value.TopBid, NumberStyles.Float, CultureInfo.InvariantCulture,
                                out price))
                        {
                            Coin.Exchange acExchange = new Coin.Exchange
                            {
                                ExchangeName = "AllCoin",
                                BtcVolume = volume,
                                BtcPrice = price
                            };

                            if (c.HasImplementedMarketApi)
                            {
                                c.Exchanges.Add(acExchange);
                                c.TotalVolume += acExchange.BtcVolume;
                            }
                            else
                            {
                                c.Exchanges = new List<Coin.Exchange> {acExchange};
                                c.TotalVolume = acExchange.BtcVolume;
                                c.HasImplementedMarketApi = true;
                            }

                            if (acCoin.Value.Status != "1" || acCoin.Value.WalletStatus != "1")
                                c.HasMarketErrors = true;
                        }
                    }
                    break;
                }
            });
        }

        public void UpdateAllCrypt(string address)
        {
            AllCrypt ac = JsonControl.DownloadSerializedApi<AllCrypt>(address);

            Parallel.ForEach(List, c =>
            {
                foreach (KeyValuePair<string, AllCrypt.Return.Coin> acCoin in ac.Returns.Markets)
                {
                    if (acCoin.Value.SecondaryCode.Trim().ToUpperInvariant() == "BTC"
                        && acCoin.Value.PrimaryCode.Trim().ToUpper() == c.TagName)
                    {
                        Coin.Exchange acExchange = new Coin.Exchange
                        {
                            ExchangeName = "AllCrypt",
                            BtcVolume = acCoin.Value.VolumeByPair,
                            BtcPrice = acCoin.Value.HighBuy
                        };

                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(acExchange);
                            c.TotalVolume += acExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges = new List<Coin.Exchange> {acExchange};
                            c.TotalVolume = acExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }
                    }

                    break;
                }
            });
        }


        public void UpdatePoolPicker(string address, decimal average, bool reviewCalc)
        {
            PoolPicker pp = JsonControl.DownloadSerializedApi<PoolPicker>(address);
            foreach (PoolPicker.Pool pool in pp.Pools)
            {
                double reviewPercentage, rating;
                if (Double.TryParse(pool.Rating, NumberStyles.Float, CultureInfo.InvariantCulture, out rating))
                {
                    reviewPercentage = rating/5;
                }
                else
                {
                    reviewPercentage = 1;
                }


                if (pool.PoolProfitability.Scrypt != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double) average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange {ExchangeName = pool.Name,};
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
                        dAverage += scrypt.Btc;
                    }

                    c.Exchanges[0].BtcPrice = dAverage/iCounter*1000;
                    if (reviewCalc)
                    {
                        c.Exchanges[0].BtcPrice *= reviewPercentage;
                    }

                    Add(c);
                }

                if (pool.PoolProfitability.ScryptN != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double) average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange {ExchangeName = pool.Name,};
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
                        dAverage += scryptN.Btc;
                    }

                    c.Exchanges[0].BtcPrice = dAverage/iCounter*1000;
                    if (reviewCalc)
                    {
                        c.Exchanges[0].BtcPrice *= reviewPercentage;
                    }

                    Add(c);
                }

                if (pool.PoolProfitability.X11 != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double) average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange {ExchangeName = pool.Name,};
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
                        dAverage += x11.Btc;
                    }

                    c.Exchanges[0].BtcPrice = dAverage/iCounter*1000;
                    if (reviewCalc)
                    {
                        c.Exchanges[0].BtcPrice *= reviewPercentage;
                    }

                    Add(c);
                }

                if (pool.PoolProfitability.X13 != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double) average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange {ExchangeName = pool.Name,};
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
                        dAverage += x13.Btc;
                    }

                    c.Exchanges[0].BtcPrice = dAverage/iCounter*1000;
                    if (reviewCalc)
                    {
                        c.Exchanges[0].BtcPrice *= reviewPercentage;
                    }

                    Add(c);
                }

                if (pool.PoolProfitability.Keccak != null)
                {
                    Coin c = new Coin
                    {
                        Difficulty = (double) average,
                        HasImplementedMarketApi = true,
                        IsMultiPool = true,
                        HasMarketErrors = false,
                        Exchanges = new List<Coin.Exchange>(),
                    };
                    Coin.Exchange ppExchange = new Coin.Exchange {ExchangeName = pool.Name,};
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
                        dAverage += keccak.Btc;
                    }

                    c.Exchanges[0].BtcPrice = dAverage/iCounter;
                    if (reviewCalc)
                    {
                        c.Exchanges[0].BtcPrice *= reviewPercentage;
                    }

                    Add(c);
                }
            }
        }

        public void AddMoneroWorkAround()
        {
            MoneroChain mon = JsonControl.DownloadSerializedApi<MoneroChain>("http://monerochain.info/api/stats");
            MoneroLatestBlock moneroLatest;
            try
            {
                moneroLatest = JsonControl.DownloadSerializedApi<MoneroLatestBlock>(
                    "http://monerochain.info/api/block/" + mon.Height);
            }
            catch (Exception)
            {
                moneroLatest =
                    JsonControl.DownloadSerializedApi<MoneroLatestBlock>(
                    "http://monerochain.info/api/block/" + (mon.Height - 1));
            }

            Coin c = new Coin
            {
                Algo = HashAlgo.Algo.CryptoNight,
                CoinName = "Monero",
                TagName = "XMR",
                Height = (uint) moneroLatest.Height,
                Difficulty = moneroLatest.Difficulty,
                BlockReward = moneroLatest.Reward,
                Exchanges = new List<Coin.Exchange>(),
                TotalVolume = 0,
                HasMarketErrors = false,
                IsMultiPool = false,
                HasImplementedMarketApi = false
            };

            Add(c);
        }

        public void Sort(HashRateJson hashList, bool useWeightedCalculation, string coindeskAddress,
            string coindeskCnyAdress)
        {
            CoinDesk cd = JsonControl.DownloadSerializedApi<CoinDesk>(coindeskAddress);
            double usdPrice = cd.BpiPrice.UsdPrice.RateFloat;
            double eurPrice = cd.BpiPrice.EurPrice.RateFloat;
            double gbpPrice = cd.BpiPrice.GbpPrice.RateFloat;

            cd = JsonControl.DownloadSerializedApi<CoinDesk>(coindeskCnyAdress);
            double cnyPrice = cd.BpiPrice.CnyPrice.RateFloat;

            Parallel.ForEach(List, coin =>
            {
                if (hashList.ListHashRate.ContainsKey(coin.Algo))
                {
                    coin.CalcProfitability(hashList.ListHashRate[coin.Algo], useWeightedCalculation);

                    double fiatElectricityCost = (hashList.ListWattage[coin.Algo]/1000)*24*hashList.FiatPerKwh;
                    switch (hashList.FiatOfChoice)
                    {
                        case 1:
                            coin.BtcPerDay -= (fiatElectricityCost/eurPrice);
                            break;
                        case 2:
                            coin.BtcPerDay -= (fiatElectricityCost/gbpPrice);
                            break;
                        case 3:
                            coin.BtcPerDay -= (fiatElectricityCost/cnyPrice);
                            break;
                        default:
                            coin.BtcPerDay -= (fiatElectricityCost/usdPrice);
                            break;
                    }


                    coin.UsdPerDay = coin.BtcPerDay*usdPrice;
                    coin.EurPerDay = coin.BtcPerDay*eurPrice;
                    coin.GbpPerDay = coin.BtcPerDay*gbpPrice;
                    coin.CnyPerDay = coin.BtcPerDay*cnyPrice;
                }
            });

            //Removing all errored coins and actually sorting them
            List =
                List.Where(x => x.BtcPerDay != 0 && (x.TotalVolume != 0 || x.IsMultiPool))
                    .OrderByDescending(o => o.BtcPerDay)
                    .ToList();
        }

        public void Sort(HashRateJson hashList, bool useWeightedCalculation)
        {
            Parallel.ForEach(List, coin =>
            {
                if (hashList.ListHashRate.ContainsKey(coin.Algo))
                {
                    coin.CalcProfitability(hashList.ListHashRate[coin.Algo], useWeightedCalculation);
                }
            });

            //Removing all errored coins and actually sorting them
            List =
                List.Where(x => x.BtcPerDay != 0 && (x.TotalVolume != 0 || x.IsMultiPool))
                    .OrderByDescending(o => o.BtcPerDay)
                    .ToList();
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