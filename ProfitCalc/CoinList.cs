using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProfitCalc.ApiControl;

namespace ProfitCalc
{
    internal class CoinList
    {
        public List<Coin> List { get; set; }
        private readonly HttpClient _client;

        public CoinList(HttpClient client)
        {
            List = new List<Coin>();
            _client = client;
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
            NiceHash niceHashData = JsonControl.DownloadSerializedApi<NiceHash>(_client.GetStreamAsync(address).Result);
            Add(new Coin(niceHashData.Results.Stats[0]));
            Add(new Coin(niceHashData.Results.Stats[2]));
            Add(new Coin(niceHashData.Results.Stats[3]));
            Add(new Coin(niceHashData.Results.Stats[4]));
            Add(new Coin(niceHashData.Results.Stats[5]));
            Add(new Coin(niceHashData.Results.Stats[6]));
            Add(new Coin(niceHashData.Results.Stats[7]));
        }

        public void UpdateWhatToMine(string address)
        {
            WhatToMine wtmData = JsonControl.DownloadSerializedApi<WhatToMine>(_client.GetStreamAsync(address).Result);
            foreach (KeyValuePair<string, WhatToMine.Coin> wtmCoin in wtmData.Coins)
            {
                Coin c = new Coin(wtmCoin);
                Add(c);
            }
        }

        public void UpdateCoinTweak(string address)
        {
            CoinTweak ctwData = JsonControl.DownloadSerializedApi<CoinTweak>(_client.GetStreamAsync(address).Result);
            foreach (CoinTweak.Coin ctwCoin in ctwData.Coins)
            {
                Coin c = new Coin(ctwCoin);
                Add(c);
            }
        }

        public void UpdateCoinWarz(string address)
        {
            CoinWarz cwzData = JsonControl.DownloadSerializedApi<CoinWarz>(_client.GetStreamAsync(address).Result);
            foreach (CoinWarz.Coin cwzCoin in cwzData.Data)
            {
                Coin c = new Coin(cwzCoin);
                Add(c);
            }
        }

        public void UpdateMintPal(string address)
        {
            MintPal mp = JsonControl.DownloadSerializedApi<MintPal>(_client.GetStreamAsync(address).Result);
            Parallel.ForEach(List, c => Parallel.ForEach(mp.Data, mpCoin =>
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
                }
            }));
        }

        public void UpdateCryptsy(string address)
        {
            Cryptsy cp = JsonControl.DownloadSerializedApi<Cryptsy>(_client.GetStreamAsync(address).Result);
            Parallel.ForEach(List, c => Parallel.ForEach(cp.Returns.Markets, cpCoin =>
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
                }
            }));
        }

        public void UpdateBittrex(string address)
        {
            Bittrex bt = JsonControl.DownloadSerializedApi<Bittrex>(_client.GetStreamAsync(address).Result);
            Parallel.ForEach(List, c => Parallel.ForEach(bt.Results, btCoin =>
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
                }
            }));
        }

        public void UpdatePoloniex(string address)
        {
            Dictionary<string, Poloniex> pol = JsonControl.DownloadSerializedApi<Dictionary<string, Poloniex>>(_client.GetStreamAsync(address).Result);
            Parallel.ForEach(List, c => Parallel.ForEach(pol, polCoin =>
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
                }
            }));
        }

        public void UpdateAllCoin(string address)
        {
            AllCoin ac = JsonControl.DownloadSerializedApi<AllCoin>(_client.GetStreamAsync(address).Result);
            Parallel.ForEach(List, c => Parallel.ForEach(ac.Data, acCoin =>
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
            }));
        }

        public void UpdateAllCrypt(string address)
        {
            AllCrypt ac = JsonControl.DownloadSerializedApi<AllCrypt>(_client.GetStreamAsync(address).Result);

            Parallel.ForEach(List, c => Parallel.ForEach(ac.Returns.Markets, acCoin =>
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
            }));
        }


        public void UpdatePoolPicker(string address, decimal average, bool reviewCalc)
        {
            DateTime whenToEnd = DateTime.UtcNow - new TimeSpan((int) average, 0, 0,0);

            PoolPicker pp = JsonControl.DownloadSerializedApi<PoolPicker>(_client.GetStreamAsync(address).Result);
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
                    AddPoolPickerPool(pool, pool.PoolProfitability.Scrypt, HashAlgo.Algo.Scrypt, whenToEnd, reviewCalc, reviewPercentage);
                

                if (pool.PoolProfitability.ScryptN != null)
                    AddPoolPickerPool(pool, pool.PoolProfitability.ScryptN, HashAlgo.Algo.ScryptN, whenToEnd, reviewCalc, reviewPercentage);

                if (pool.PoolProfitability.X11 != null)
                    AddPoolPickerPool(pool, pool.PoolProfitability.X11, HashAlgo.Algo.X11, whenToEnd, reviewCalc, reviewPercentage);


                if (pool.PoolProfitability.X13 != null)
                    AddPoolPickerPool(pool, pool.PoolProfitability.X13, HashAlgo.Algo.X13, whenToEnd, reviewCalc, reviewPercentage);

                if (pool.PoolProfitability.Keccak != null)
                    AddPoolPickerPool(pool, pool.PoolProfitability.Keccak, HashAlgo.Algo.Keccak, whenToEnd, reviewCalc, reviewPercentage);
            }
        }

        private void AddPoolPickerPool(PoolPicker.Pool pool, List<PoolPicker.Pool.Profitability.Algo> profitList, HashAlgo.Algo algo, 
            DateTime whenToEnd, bool reviewCalc, double reviewPercentage)
        {
            Coin c = new Coin
            {
                HasImplementedMarketApi = true,
                IsMultiPool = true,
                HasMarketErrors = false,
                Exchanges = new List<Coin.Exchange>(),
            };
            Coin.Exchange ppExchange = new Coin.Exchange { ExchangeName = pool.Name, };
            c.Exchanges.Add(ppExchange);

            c.Algo = algo;
            c.CoinName = pool.Name + " " + c.Algo;
            c.TagName = "PP" + pool.Id + c.Algo;

            double dAverage = 0;
            int iCounter;
            for (iCounter = 0; iCounter < profitList.Count; iCounter++)
            {
                PoolPicker.Pool.Profitability.Algo profit = profitList[iCounter];
                dAverage += profit.Btc;
                DateTime profitDate = DateTime.ParseExact(profit.Date, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture);
                if (profitDate.Date.Equals(whenToEnd.Date)) break;
            }

            c.Exchanges[0].BtcPrice = c.Algo == HashAlgo.Algo.Keccak
                ? dAverage/(iCounter + 1)
                : dAverage/(iCounter + 1)*1000;

            if (reviewCalc)
            {
                c.Exchanges[0].BtcPrice *= reviewPercentage;
            }

            Add(c);
        }

        public void AddMoneroWorkAround()
        {
            MoneroChain mon = JsonControl.DownloadSerializedApi<MoneroChain>(_client.GetStreamAsync("http://monerochain.info/api/stats").Result);
            MoneroLatestBlock moneroLatest;
            try
            {
                moneroLatest = JsonControl.DownloadSerializedApi<MoneroLatestBlock>(
                    _client.GetStreamAsync("http://monerochain.info/api/block/").Result);
            }
            catch (Exception)
            {
                moneroLatest =
                    JsonControl.DownloadSerializedApi<MoneroLatestBlock>(
                    _client.GetStreamAsync("http://monerochain.info/api/block/" + (mon.Height - 1)).Result);
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

        public void Sort(HashRateJson hashList, bool useWeightedCalculation, string coindeskAdress,
            string coindeskCnyAdress)
        {
            CoinDesk cd = JsonControl.DownloadSerializedApi<CoinDesk>(_client.GetStreamAsync(coindeskAdress).Result);
            double usdPrice = cd.BpiPrice.UsdPrice.RateFloat;
            double eurPrice = cd.BpiPrice.EurPrice.RateFloat;
            double gbpPrice = cd.BpiPrice.GbpPrice.RateFloat;

            cd = JsonControl.DownloadSerializedApi<CoinDesk>(_client.GetStreamAsync(coindeskCnyAdress).Result);
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
                List.AsParallel().Where(x => x.BtcPerDay != 0 && (x.TotalVolume != 0 || x.IsMultiPool))
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
                List.AsParallel().Where(x => x.BtcPerDay != 0 && (x.TotalVolume != 0 || x.IsMultiPool))
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