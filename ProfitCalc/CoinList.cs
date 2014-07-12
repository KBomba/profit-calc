using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProfitCalc.ApiControl;

namespace ProfitCalc
{
    internal class CoinList
    {
        public List<Coin> List { get; set; }
        private readonly HttpClient _client;
        private readonly ParallelOptions _po;

        public CoinList(HttpClient client)
        {
            List = new List<Coin>();
            _client = client;

            _po = new ParallelOptions
            {
                CancellationToken = new CancellationTokenSource().Token,
                MaxDegreeOfParallelism = Environment.ProcessorCount * 4,
            };
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

        public void UpdateNiceHash()
        {
            NiceHash niceHashData = JsonControl.DownloadSerializedApi<NiceHash>(
                _client.GetStreamAsync("http://www.nicehash.com/api?method=stats.global.current").Result);
            Add(new Coin(niceHashData.Results.Stats[0]));
            Add(new Coin(niceHashData.Results.Stats[2]));
            Add(new Coin(niceHashData.Results.Stats[3]));
            Add(new Coin(niceHashData.Results.Stats[4]));
            Add(new Coin(niceHashData.Results.Stats[5]));
            Add(new Coin(niceHashData.Results.Stats[6]));
            Add(new Coin(niceHashData.Results.Stats[7]));
        }

        public void UpdateWhatToMine()
        {
            WhatToMine wtmData = JsonControl.DownloadSerializedApi<WhatToMine>(_client.GetStreamAsync("http://www.whattomine.com/coins.json").Result);
            foreach (KeyValuePair<string, WhatToMine.Coin> wtmCoin in wtmData.Coins)
            {
                Coin c = new Coin(wtmCoin);
                Add(c);
            }
        }

        public void UpdateCoinTweak(string apiKey)
        {
            CoinTweak ctwData = JsonControl.DownloadSerializedApi<CoinTweak>(
                _client.GetStreamAsync("http://cointweak.com/API/getProfitOverview/&key=" + apiKey).Result);

            if (ctwData.Success)
            {
                foreach (CoinTweak.Coin ctwCoin in ctwData.Coins)
                {
                    Coin c = new Coin(ctwCoin);
                    if (c.TagName == "RUBY")
                    {
                        c.TagName = "RBY";
                    }
                    Add(c);
                }
            }
            else
            {
                throw new Exception(ctwData.CallsRemaining.ToString(CultureInfo.InvariantCulture) + " calls remaining");
            }
        }

        public void UpdateCoinWarz(string apiKey)
        {
            CoinWarz cwzData = JsonControl.DownloadSerializedApi<CoinWarz>(
                _client.GetStreamAsync("http://www.coinwarz.com/v1/api/profitability/?algo=all&apikey=" + apiKey).Result);

            if (cwzData.Success)
            {
                foreach (CoinWarz.Coin cwzCoin in cwzData.Data)
                {
                    Coin c = new Coin(cwzCoin);
                    Add(c);
                }
            }
            else
            {
                throw new Exception(cwzData.Message);
            }
        }

        public void UpdateMintPal(int selectedIndex)
        {
            MintPal mp = JsonControl.DownloadSerializedApi<MintPal>(
                _client.GetStreamAsync("https://api.mintpal.com/v2/market/summary/BTC").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(mp.Data, _po, mpCoin =>
            {
                if (mpCoin.Exchange == "BTC" && mpCoin.Code == c.TagName)
                {
                    double priceToUse;
                    switch (selectedIndex)
                    {
                        case 0:
                            priceToUse = mpCoin.TopBid;
                            break;
                        case 1:
                            priceToUse = mpCoin.LastPrice;
                            break;
                        case 2:
                            priceToUse = mpCoin.TopAsk;
                            break;
                        default:
                            priceToUse = mpCoin.TopBid;
                            break;
                    }

                    Coin.Exchange mpExchange = new Coin.Exchange
                    {
                        ExchangeName = "MintPal",
                        BtcPrice = priceToUse,
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

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
            }));
        }

        public void UpdateCryptsy(int selectedIndex)
        {
            Cryptsy cp = JsonControl.DownloadSerializedApi<Cryptsy>(_client.GetStreamAsync("http://pubapi.cryptsy.com/api.php?method=marketdatav2").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(cp.Returns.Markets, _po, cpCoin =>
            {
                if (cpCoin.Value.SecondaryCode == "BTC" && cpCoin.Value.PrimaryCode == c.TagName &&
                    cpCoin.Value.BuyOrders != null)
                {
                    double priceToUse;
                    switch (selectedIndex)
                    {
                        case 0:
                            priceToUse = cpCoin.Value.BuyOrders[0].Price;
                            break;
                        case 1:
                            priceToUse = cpCoin.Value.LastTradePrice;
                            break;
                        case 2:
                            priceToUse = cpCoin.Value.SellOrders[0].Price;
                            break;
                        default:
                            priceToUse = cpCoin.Value.BuyOrders[0].Price;
                            break;
                    }

                    Coin.Exchange cpExchange = new Coin.Exchange
                    {
                        ExchangeName = "Cryptsy",
                        BtcPrice = priceToUse,
                        BtcVolume = (cpCoin.Value.Volume*priceToUse)
                    };

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

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
            }));
        }

        public void UpdateBittrex(int selectedIndex)
        {
            Bittrex bt = JsonControl.DownloadSerializedApi<Bittrex>(
                _client.GetStreamAsync("http://bittrex.com/api/v1/public/getmarketsummaries").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(bt.Results, _po, btCoin =>
            {
                String[] splitMarket = btCoin.MarketName.Split('-');
                if (splitMarket[0] == "BTC" && splitMarket[1] == c.TagName)
                {
                    double priceToUse;
                    switch (selectedIndex)
                    {
                        case 0:
                            priceToUse = btCoin.Bid;
                            break;
                        case 1:
                            priceToUse = btCoin.Last;
                            break;
                        case 2:
                            priceToUse = btCoin.Ask;
                            break;
                        default:
                            priceToUse = btCoin.Bid;
                            break;
                    }

                    Coin.Exchange btExchange = new Coin.Exchange
                    {
                        ExchangeName = "Bittrex",
                        BtcPrice = priceToUse,
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

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
            }));
        }

        public void UpdatePoloniex(int selectedIndex)
        {
            Dictionary<string, Poloniex> pol = JsonControl.DownloadSerializedApi<Dictionary<string, Poloniex>>(
                _client.GetStreamAsync("http://poloniex.com/public?command=returnTicker").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(pol, _po, polCoin =>
            {
                String[] splitMarket = polCoin.Key.Split('_');
                if (splitMarket[0] == "BTC" && splitMarket[1] == c.TagName)
                {
                    double priceToUse;
                    switch (selectedIndex)
                    {
                        case 0:
                            priceToUse = polCoin.Value.HighestBid;
                            break;
                        case 1:
                            priceToUse = polCoin.Value.Last;
                            break;
                        case 2:
                            priceToUse = polCoin.Value.LowestAsk;
                            break;
                        default:
                            priceToUse = polCoin.Value.HighestBid;
                            break;
                    }

                    Coin.Exchange polExchange = new Coin.Exchange
                    {
                        ExchangeName = "Poloniex",
                        BtcPrice = priceToUse,
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

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
            }));
        }

        public void UpdateAllCoin(int selectedIndex)
        {
            AllCoin ac = JsonControl.DownloadSerializedApi<AllCoin>(
                _client.GetStreamAsync("https://www.allcoin.com/api2/pairs").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(ac.Data, _po, acCoin =>
            {
                String[] splitMarket = acCoin.Key.Split('_');
                if (splitMarket[1] == "BTC" && splitMarket[0] == c.TagName)
                {
                    double volume, price;
                    bool hasOrder;

                    switch (selectedIndex)
                    {
                        case 0:
                            hasOrder = Double.TryParse(acCoin.Value.TopBid, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out price);
                            break;
                        case 1:
                            hasOrder = Double.TryParse(acCoin.Value.TradePrice, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out price);
                            break;
                        case 2:
                            hasOrder = Double.TryParse(acCoin.Value.TopAsk, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out price);
                            break;
                        default:
                            hasOrder = Double.TryParse(acCoin.Value.TopBid, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out price);
                            break;
                    }

                    if (
                        Double.TryParse(acCoin.Value.Volume24HBtc, NumberStyles.Float, 
                        CultureInfo.InvariantCulture, out volume) && hasOrder)
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

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
            }));
        }

        public void UpdateAllCrypt(int selectedIndex)
        {
            AllCrypt ac = JsonControl.DownloadSerializedApi<AllCrypt>(
                _client.GetStreamAsync("http://www.allcrypt.com/api?method=cmcmarketdata").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(ac.Returns.Markets, _po, acCoin =>
            {
                if (acCoin.Value.SecondaryCode == "BTC" && acCoin.Value.PrimaryCode== c.TagName)
                {
                    double priceToUse;
                    switch (selectedIndex)
                    {
                        case 0:
                            priceToUse = acCoin.Value.HighBuy;
                            break;
                        case 1:
                            priceToUse = acCoin.Value.LastTradePrice;
                            break;
                        case 2:
                            priceToUse = acCoin.Value.LowSell;
                            break;
                        default:
                            priceToUse = acCoin.Value.HighBuy;
                            break;
                    }

                    Coin.Exchange acExchange = new Coin.Exchange
                    {
                        ExchangeName = "AllCrypt",
                        BtcVolume = acCoin.Value.VolumeByPair,
                        BtcPrice = priceToUse
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

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
            }));
        }
        
        public void UpdatePoolPicker(decimal average, bool reviewCalc)
        {
            DateTime whenToEnd = DateTime.UtcNow - new TimeSpan((int) average, 0, 0,0);

            PoolPicker pp = JsonControl.DownloadSerializedApi<PoolPicker>(
                _client.GetStreamAsync("http://poolpicker.eu/api").Result);
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
                    AddPoolPickerPool(pool, pool.PoolProfitability.Scrypt, HashAlgo.Algo.Scrypt, whenToEnd, reviewCalc, reviewPercentage);
                }


                if (pool.PoolProfitability.ScryptN != null)
                {
                    AddPoolPickerPool(pool, pool.PoolProfitability.ScryptN, HashAlgo.Algo.ScryptN, whenToEnd, reviewCalc, reviewPercentage);
                }

                if (pool.PoolProfitability.X11 != null)
                {
                    AddPoolPickerPool(pool, pool.PoolProfitability.X11, HashAlgo.Algo.X11, whenToEnd, reviewCalc, reviewPercentage);
                }


                if (pool.PoolProfitability.X13 != null)
                {
                    AddPoolPickerPool(pool, pool.PoolProfitability.X13, HashAlgo.Algo.X13, whenToEnd, reviewCalc, reviewPercentage);
                }

                if (pool.PoolProfitability.Keccak != null)
                {
                    AddPoolPickerPool(pool, pool.PoolProfitability.Keccak, HashAlgo.Algo.Keccak, whenToEnd, reviewCalc, reviewPercentage);
                }
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
            };
            Coin.Exchange ppExchange = new Coin.Exchange { ExchangeName = pool.Name, };
            c.Exchanges.Add(ppExchange);

            c.Algo = algo;
            c.FullName = pool.Name + " " + c.Algo + " (PP)";
            c.TagName = "PP" + pool.Id + c.Algo;

            double dAverage = 0;
            int iCounter;
            for (iCounter = 0; iCounter < profitList.Count; iCounter++)
            {
                PoolPicker.Pool.Profitability.Algo profit = profitList[iCounter];
                DateTime profitDate = DateTime.ParseExact(profit.Date, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture);

                if (profitDate.Date < whenToEnd.Date)
                {
                    break;
                }

                dAverage += profit.Btc;
                
                if (profitDate.Date.Equals(whenToEnd.Date)) break;
            }

            c.Exchanges[0].BtcPrice = 
                c.Algo == HashAlgo.Algo.Keccak
                ? dAverage/(iCounter + 1)
                : dAverage/(iCounter + 1)*1000;

            if (reviewCalc)
            {
                c.Exchanges[0].BtcPrice *= reviewPercentage;
            }

            Add(c);
        }

        public void UpdateCrypToday(decimal average)
        {
            CrypToday ct = JsonControl.DownloadSerializedApi<CrypToday>(
                _client.GetStreamAsync("http://cryp.today/data").Result);
            Coin[] tempMultipools = new Coin[ct.Cols.Count-1];

            Parallel.For(0, ct.Cols.Count - 1, i =>
            {
                string[] splitNameAndAlgo = ct.Cols[i + 1].Label.Split(' ');
                tempMultipools[i] = new Coin();
                switch (splitNameAndAlgo[1])
                {
                    case "X11":
                        tempMultipools[i].Algo = HashAlgo.Algo.X11;
                        break;
                    case "X13":
                        tempMultipools[i].Algo = HashAlgo.Algo.X13;
                        break;
                    case "N":
                        tempMultipools[i].Algo = HashAlgo.Algo.ScryptN;
                        break;
                    //case "S":
                    default:
                        tempMultipools[i].Algo = HashAlgo.Algo.Scrypt;
                        break;
                }

                tempMultipools[i].FullName = ct.Cols[i + 1].Label + " (CT)";
                tempMultipools[i].TagName = "CT" + i + tempMultipools[i].Algo;

                tempMultipools[i].HasImplementedMarketApi = true;
                tempMultipools[i].IsMultiPool = true;
                tempMultipools[i].HasMarketErrors = false;

                Coin.Exchange ctExchange = new Coin.Exchange {ExchangeName = splitNameAndAlgo[0]};
                tempMultipools[i].Exchanges.Add(ctExchange);
            });

            double priceHolder;
            for (int row = ct.Rows.Count - 1; row >= ct.Rows.Count - average; row--)
            {
                for (int column = 1; column < ct.Rows[row].Results.Count; column++)
                {
                    if (!string.IsNullOrWhiteSpace(ct.Rows[row].Results[column].Btc) &&
                        double.TryParse(ct.Rows[row].Results[column].Btc, NumberStyles.Float,
                            CultureInfo.InvariantCulture, out priceHolder))
                    {
                        tempMultipools[column - 1].Exchanges[0].BtcPrice += priceHolder;
                        // Temp storing amount of not-null BtcPerDays into BlockReward
                        tempMultipools[column - 1].BlockReward++;
                    }
                }
            }

            foreach (Coin c in tempMultipools)
            {
                switch (c.Algo)
                {
                    case HashAlgo.Algo.X11:
                        c.Exchanges[0].BtcPrice /= 5.2;
                        break;
                    case HashAlgo.Algo.X13:
                        c.Exchanges[0].BtcPrice /= 3;
                        break;
                    case HashAlgo.Algo.ScryptN:
                        c.Exchanges[0].BtcPrice /= 0.47;
                        break;
                }

                c.Exchanges[0].BtcPrice /= c.BlockReward;
                c.Exchanges[0].BtcPrice *= 1000;
                Add(c);
            }
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
                FullName = "Monero",
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