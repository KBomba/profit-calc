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
        public Profile UsedProfile { get; set; }

        public CoinList(HttpClient client, Profile profile)
        {
            List = new List<Coin>();
            _client = client;
            UsedProfile = profile;

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

            if (!found && UsedInProfile(newCoin.Algo, UsedProfile.CustomAlgoList))
            {
                List.Add(newCoin);
            }
        }

        private bool UsedInProfile(string algo, IEnumerable<CustomAlgo> customAlgoList)
        {
            bool used = false;

            Parallel.ForEach(customAlgoList, _po, savedAlgo =>
            {
                if (savedAlgo.Name == algo && savedAlgo.Use)
                {
                    used = true;
                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
            });

            return used;
        }

        public void AddCustomCoins(IEnumerable<CustomCoin> customCoins)
        {
            foreach (CustomCoin customCoin in customCoins)
            {
                if (customCoin.Use)
                {
                    Add(new Coin(customCoin));
                }
            }
        }

        public string GetCleanedAlgo(string algo)
        {
            string cleanAlgo = algo.Trim();
            Parallel.ForEach(UsedProfile.CustomAlgoList, _po, savedAlgo =>
            {
                if (savedAlgo.Name == cleanAlgo)
                {
                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
                else if (!string.IsNullOrWhiteSpace(savedAlgo.SynonymsCsv))
                {
                    string[] synonyms = savedAlgo.SynonymsCsv.Split(',');
                    Parallel.ForEach(synonyms, synonym =>
                    {
                        if (synonym == cleanAlgo)
                        {
                            cleanAlgo = savedAlgo.Name;
                            _po.CancellationToken.ThrowIfCancellationRequested();
                        }
                    });
                }
            });

            return cleanAlgo;
        }

        public void UpdateNiceHash()
        {
            NiceHash niceHashData = JsonControl.DownloadSerializedApi<NiceHash>(
                _client.GetStreamAsync("http://www.nicehash.com/api?method=stats.global.current").Result);
            NiceHash westHashData = JsonControl.DownloadSerializedApi<NiceHash>(
                _client.GetStreamAsync("http://www.westhash.com/api?method=stats.global.current").Result);

            for (int i = 0; i < niceHashData.Results.Stats.Count; i++)
            {
                    Add(new Coin(niceHashData.Results.Stats[i], "NiceHash"));
                    Add(new Coin(westHashData.Results.Stats[i], "WestHash"));
            }
        }

        public void UpdateWhatToMine()
        {
            WhatToMine wtmData = JsonControl.DownloadSerializedApi<WhatToMine>(_client.GetStreamAsync("http://www.whattomine.com/coins.json").Result);
            foreach (KeyValuePair<string, WhatToMine.Coin> wtmCoin in wtmData.Coins)
            {
                Coin c = new Coin(wtmCoin);
                c.Algo = GetCleanedAlgo(c.Algo);
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
                    c.Algo = GetCleanedAlgo(c.Algo);
                    Add(c);
                }
            }
            else
            {
                throw new Exception(ctwData.CallsRemaining.ToString(CultureInfo.InvariantCulture) + " calls remaining or invalid API key");
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
                    c.Algo = GetCleanedAlgo(c.Algo);
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
                if (cpCoin.Value.SecondaryCode == "BTC" && cpCoin.Value.PrimaryCode == c.TagName)
                {
                    double priceToUse;
                    switch (selectedIndex)
                    {
                        case 0:
                            priceToUse = cpCoin.Value.BuyOrders != null
                                && cpCoin.Value.BuyOrders.Any()
                                ? cpCoin.Value.BuyOrders[0].Price
                                : cpCoin.Value.LastTradePrice;
                            break;
                        case 1:
                            priceToUse = cpCoin.Value.LastTradePrice;
                            break;
                        case 2:
                            priceToUse = cpCoin.Value.SellOrders != null
                                && cpCoin.Value.SellOrders.Any()
                                ? cpCoin.Value.SellOrders[0].Price
                                : cpCoin.Value.LastTradePrice;
                            break;
                        default:
                            priceToUse = cpCoin.Value.BuyOrders != null
                                && cpCoin.Value.BuyOrders.Any()
                                ? cpCoin.Value.BuyOrders[0].Price
                                : cpCoin.Value.LastTradePrice;
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
                _client.GetStreamAsync("http://bittrex.com/api/v1.1/public/getmarketsummaries").Result);

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
                    if (polCoin.Value.IsFrozen == "1")
                    {
                        polExchange.IsFrozen = true;
                    }

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
                        if (acCoin.Value.Status != "1" || acCoin.Value.WalletStatus != "1")
                        {
                            acExchange.IsFrozen = true;
                        }

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
                if (acCoin.Value.SecondaryCode == "BTC" && acCoin.Value.PrimaryCode == c.TagName)
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

        public void UpdateCCex(int selectedIndex)
        {
            Dictionary<string, CCexPair> ccPairs = JsonControl.DownloadSerializedApi<Dictionary<string, CCexPair>>(
                _client.GetStreamAsync("https://c-cex.com/t/prices.json").Result);
            CCexVolume ccVolumes = JsonControl.DownloadSerializedApi<CCexVolume>(
                _client.GetStreamAsync("https://c-cex.com/t/s.html?a=lastvolumes&h=24").Result);

            Parallel.ForEach(List, c => Parallel.For(0, ccPairs.Count, _po, i =>
            /*foreach (Coin c in List)
            {
                for(int i = 0; i < ccPairs.Count; i++)*/
                {
                    var splitPair = ccPairs.Keys.ElementAt(i).Split('-');

                    if (splitPair[1] == "btc" && splitPair[0] == c.TagName.ToLowerInvariant())
                    {
                        CCexPair ccPair = ccPairs.Values.ElementAt(i);
                        double priceToUse;
                        switch (selectedIndex)
                        {
                            case 0:
                                priceToUse = ccPair.Buy;
                                break;
                            case 1:
                                priceToUse = ccPair.Lastprice;
                                break;
                            case 2:
                                priceToUse = ccPair.Sell;
                                break;
                            default:
                                priceToUse = ccPair.Buy;
                                break;
                        }
                        
                        ParallelOptions optionsVolumeLoop = new ParallelOptions
                        {
                            CancellationToken = new CancellationTokenSource().Token,
                        };
                        double volumeToUse = 0;
                        Parallel.ForEach(ccVolumes.Returns, optionsVolumeLoop, ccVolume =>
                        //foreach (Dictionary<string, string> ccVolume in ccVolumes.Returns)
                        {
                            if (ccVolume.ContainsKey("volume_btc") && ccVolume.ContainsKey("volume_" + splitPair[0]) &&
                                Double.TryParse(ccVolume["volume_btc"], NumberStyles.Any, CultureInfo.InvariantCulture, out volumeToUse))
                            {
                                //break;
                                optionsVolumeLoop.CancellationToken.ThrowIfCancellationRequested();
                            }
                        });

                        Coin.Exchange ccExchange = new Coin.Exchange
                        {
                            ExchangeName = "C-Cex",
                            BtcVolume = volumeToUse,
                            BtcPrice = priceToUse
                        };

                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(ccExchange);
                            c.TotalVolume += ccExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges = new List<Coin.Exchange> { ccExchange };
                            c.TotalVolume = ccExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }

                        _po.CancellationToken.ThrowIfCancellationRequested();
                    }
                //}
            }));
        }

        public void UpdateComkort(int selectedIndex)
        {
            Comkort com = JsonControl.DownloadSerializedApi<Comkort>(
                _client.GetStreamAsync("https://api.comkort.com/v1/public/market/summary").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(com.Markets, _po, comCoin =>
            {
                /*foreach (Coin c in List)
                {
                    foreach (KeyValuePair<string, Comkort.Pair> comCoin in com.Markets)
                    {*/
                        if (comCoin.Value.CurrencyCode == "BTC" && comCoin.Value.ItemCode == c.TagName)
                        {
                            double priceToUse;
                            switch (selectedIndex)
                            {
                                case 0:
                                    priceToUse = comCoin.Value.BuyOrders != null
                                        && comCoin.Value.BuyOrders.Any()
                                        ? comCoin.Value.BuyOrders[0].Price
                                        : comCoin.Value.LastPrice;
                                    break;
                                case 1:
                                    priceToUse = comCoin.Value.LastPrice;
                                    break;
                                case 2:
                                    priceToUse = comCoin.Value.SellOrders != null
                                        && comCoin.Value.SellOrders.Any()
                                        ? comCoin.Value.SellOrders[0].Price
                                        : comCoin.Value.LastPrice;
                                    break;
                                default:
                                    priceToUse = comCoin.Value.BuyOrders != null 
                                        && comCoin.Value.BuyOrders.Any()
                                        ? comCoin.Value.BuyOrders[0].Price
                                        : comCoin.Value.LastPrice;
                                    break;
                            }

                            Coin.Exchange comExchange = new Coin.Exchange
                            {
                                ExchangeName = "Comkort",
                                BtcPrice = priceToUse,
                                BtcVolume = (comCoin.Value.CurrencyVolume)
                            };

                            if (c.HasImplementedMarketApi)
                            {
                                c.Exchanges.Add(comExchange);
                                c.TotalVolume += comExchange.BtcVolume;
                            }
                            else
                            {
                                c.Exchanges = new List<Coin.Exchange> {comExchange};
                                c.TotalVolume = comExchange.BtcVolume;
                                c.HasImplementedMarketApi = true;
                            }

                            _po.CancellationToken.ThrowIfCancellationRequested();
                        }
                    //}
                }));
        }

        public void UpdateCryptoine(int selectedIndex)
        {
            Cryptoine cry = JsonControl.DownloadSerializedApi<Cryptoine>(
                _client.GetStreamAsync("https://cryptoine.com/api/1/markets").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(cry.Data, _po, cryCoin =>
            {
                string[] split = cryCoin.Key.Split('_');
                if (split[1] == "btc" && split[0] == c.TagName.ToLowerInvariant())
                {
                    double priceToUse;
                    switch (selectedIndex)
                    {
                        case 0:
                            if (!double.TryParse(cryCoin.Value.Buy, NumberStyles.Float, 
                                CultureInfo.InvariantCulture, out priceToUse))
                            {
                                priceToUse = cryCoin.Value.Last;
                            }
                            break;
                        case 1:
                            priceToUse = cryCoin.Value.Last;
                            break;
                        case 2:
                            if (!double.TryParse(cryCoin.Value.Sell, NumberStyles.Float, 
                                CultureInfo.InvariantCulture, out priceToUse))
                            {
                                priceToUse = cryCoin.Value.Last;
                            }
                            break;
                        default:
                            if (!double.TryParse(cryCoin.Value.Buy, NumberStyles.Float, 
                                CultureInfo.InvariantCulture, out priceToUse))
                            {
                                priceToUse = cryCoin.Value.Last;
                            }
                            break;
                    }

                    Coin.Exchange cryExchange = new Coin.Exchange
                    {
                        ExchangeName = "Cryptoine",
                        BtcPrice = priceToUse,
                        BtcVolume = (cryCoin.Value.VolBase)
                    };

                    if (c.HasImplementedMarketApi)
                    {
                        c.Exchanges.Add(cryExchange);
                        c.TotalVolume += cryExchange.BtcVolume;
                    }
                    else
                    {
                        c.Exchanges = new List<Coin.Exchange> { cryExchange };
                        c.TotalVolume = cryExchange.BtcVolume;
                        c.HasImplementedMarketApi = true;
                    }

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
                //}
            }));
        }

        public void UpdateBTer(int selectedIndex)
        {
            Dictionary<string, BTer> btPairs = JsonControl.DownloadSerializedApi<Dictionary<string, BTer>>(
                _client.GetStreamAsync("http://data.bter.com/api/1/tickers").Result);

            Parallel.ForEach(List, c => Parallel.ForEach(btPairs, _po, btCoin =>
            {
                /*foreach (Coin c in List)
                {
                    foreach (KeyValuePair<string, ...)
                    {*/
                string[] split = btCoin.Key.Split('_');
                if (split[1] == "btc" && split[0].ToUpperInvariant() == c.TagName)
                {
                    double priceToUse;
                    switch (selectedIndex)
                    {
                        case 0:
                            if (!double.TryParse(btCoin.Value.Buy, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out priceToUse)
                                && !double.TryParse(btCoin.Value.Last, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out priceToUse))
                            {
                                priceToUse = 0;
                            }
                            break;
                        case 1:
                            if (!double.TryParse(btCoin.Value.Last, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out priceToUse))
                            {
                                priceToUse = 0;
                            }
                            break;
                        case 2:
                            if (!double.TryParse(btCoin.Value.Sell, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out priceToUse)
                                && !double.TryParse(btCoin.Value.Last, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out priceToUse))
                            {
                                priceToUse = 0;
                            }
                            break;
                        default:
                            if (!double.TryParse(btCoin.Value.Buy, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out priceToUse)
                                && !double.TryParse(btCoin.Value.Last, NumberStyles.Float,
                                CultureInfo.InvariantCulture, out priceToUse))
                            {
                                priceToUse = 0;
                            }
                            break;
                    }

                    Coin.Exchange btExchange = new Coin.Exchange
                    {
                        ExchangeName = "BTer",
                        BtcPrice = priceToUse,
                        BtcVolume = Convert.ToDouble(btCoin.Value.Vols["vol_btc"].ToString())
                    };

                    if (c.HasImplementedMarketApi)
                    {
                        c.Exchanges.Add(btExchange);
                        c.TotalVolume += btExchange.BtcVolume;
                    }
                    else
                    {
                        c.Exchanges = new List<Coin.Exchange> { btExchange };
                        c.TotalVolume = btExchange.BtcVolume;
                        c.HasImplementedMarketApi = true;
                    }

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
                //}
            }));
        }

        public void UpdateAtomicTrade(int selectedIndex)
        {
            List<AtomicTradePair> atPairs = JsonControl.DownloadSerializedApi<List<AtomicTradePair>>(
                _client.GetStreamAsync("https://www.atomic-trade.com/SimpleAPI?a=marketsv2").Result);

            /*Parallel.ForEach(List, c => Parallel.ForEach(atPairs, _po, atCoin =>
            {*/
            foreach (Coin c in List)
            {
                foreach (var atCoin in atPairs)
                {
                    string[] split = atCoin.Market.Split('/');
                    if (split[1] == "BTC" && split[0] == c.TagName)
                    {
                        AtomicTradeOrders atOrders = JsonControl.DownloadSerializedApi<AtomicTradeOrders>(
                            _client.GetStreamAsync("https://www.atomic-trade.com/SimpleAPI?a=orderbook&p=BTC&c=" +
                                                   c.TagName).Result);

                        double priceToUse;
                        switch (selectedIndex)
                        {
                            case 0:
                                if (atOrders.Market.Buyorders == null || !atOrders.Market.Buyorders.Any()
                                    || !double.TryParse(atOrders.Market.Buyorders[0].Price,
                                        NumberStyles.Float, CultureInfo.InvariantCulture, out priceToUse))
                                {
                                    priceToUse = 0;
                                }
                                break;
                            case 1:
                                if (!double.TryParse(atCoin.Price, NumberStyles.Float, 
                                    CultureInfo.InvariantCulture, out priceToUse))
                                {
                                    priceToUse = 0;
                                }
                                break;
                            case 2:
                                if (atOrders.Market.Sellorders == null || !atOrders.Market.Sellorders.Any()
                                    || !double.TryParse(atOrders.Market.Sellorders[0].Price,
                                        NumberStyles.Float, CultureInfo.InvariantCulture, out priceToUse))
                                {
                                    priceToUse = 0;
                                }
                                break;
                            default:
                                if (atOrders.Market.Buyorders == null || !atOrders.Market.Buyorders.Any()
                                    || !double.TryParse(atOrders.Market.Buyorders[0].Price,
                                        NumberStyles.Float, CultureInfo.InvariantCulture, out priceToUse))
                                {
                                    priceToUse = 0;
                                }
                                break;
                        }

                        Coin.Exchange atExchange = new Coin.Exchange
                        {
                            ExchangeName = "Atomic Trade",
                            BtcPrice = priceToUse,
                            BtcVolume = double.Parse(atCoin.Volume, NumberStyles.Float, CultureInfo.InvariantCulture)*priceToUse
                        };

                        if (c.HasImplementedMarketApi)
                        {
                            c.Exchanges.Add(atExchange);
                            c.TotalVolume += atExchange.BtcVolume;
                        }
                        else
                        {
                            c.Exchanges = new List<Coin.Exchange> {atExchange};
                            c.TotalVolume = atExchange.BtcVolume;
                            c.HasImplementedMarketApi = true;
                        }

                        _po.CancellationToken.ThrowIfCancellationRequested();
                    }
                }
            }
            //}));
        }

        public void UpdatePoolPicker(decimal average, bool reviewCalc)
        {
            DateTime whenToEnd = DateTime.UtcNow - new TimeSpan((int) average, 0, 0,0);

            PoolPicker pp = JsonControl.DownloadSerializedApi<PoolPicker>(
                _client.GetStreamAsync("http://poolpicker.eu/fullapi").Result);
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

                foreach (KeyValuePair<string, List<PoolPicker.Pool.Algo>> algoResults in pool.PoolProfitability)
                {
                    AddPoolPickerPool(pool,algoResults.Value, algoResults.Key.ToUpperInvariant(),  whenToEnd, reviewCalc, reviewPercentage);
                }
            }
        }

        private void AddPoolPickerPool(PoolPicker.Pool pool, List<PoolPicker.Pool.Algo> profitList, string algo, 
            DateTime whenToEnd, bool reviewCalc, double reviewPercentage)
        {
            Coin c = new Coin
            {
                HasImplementedMarketApi = true,
                IsMultiPool = true,
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
                PoolPicker.Pool.Algo profit = profitList[iCounter];
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
                c.Algo == "KECCAK" || c.Algo == "SHA256"
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

            Parallel.For(0, ct.Cols.Count - 1, _po, i =>
            {
                string[] splitNameAndAlgo = ct.Cols[i + 1].Label.Split(' ');
                tempMultipools[i] = new Coin();
                switch (splitNameAndAlgo[1])
                {
                    case "X11":
                    case "X13":
                        tempMultipools[i].Algo = splitNameAndAlgo[1];
                        break;
                    case "N":
                        tempMultipools[i].Algo = "SCRYPTN";
                        break;
                    //case "S":
                    default:
                        tempMultipools[i].Algo = "SCRYPT";
                        break;
                }

                tempMultipools[i].FullName = ct.Cols[i + 1].Label + " (CT)";
                tempMultipools[i].TagName = "CT" + i + tempMultipools[i].Algo;

                tempMultipools[i].HasImplementedMarketApi = true;
                tempMultipools[i].IsMultiPool = true;

                Coin.Exchange ctExchange = new Coin.Exchange {ExchangeName = splitNameAndAlgo[0]};
                tempMultipools[i].Exchanges.Add(ctExchange);
            });

            for (int i = ct.Rows.Count - 1; i >= ct.Rows.Count - average; i--)
            {
                int row = i;
                Parallel.For(1, ct.Rows[i].Results.Count, _po, column =>
                {
                    double priceHolder;
                    if (!string.IsNullOrWhiteSpace(ct.Rows[row].Results[column].Btc) &&
                        double.TryParse(ct.Rows[row].Results[column].Btc, NumberStyles.Float,
                            CultureInfo.InvariantCulture, out priceHolder))
                    {
                        tempMultipools[column - 1].Exchanges[0].BtcPrice += priceHolder;
                        // Temp storing amount of not-null BtcPerDays into BlockReward
                        tempMultipools[column - 1].BlockReward++;
                    }
                });
            }

            foreach (Coin c in tempMultipools)
            {
                switch (c.Algo)
                {
                    case "X11":
                        c.Exchanges[0].BtcPrice /= 5.2;
                        break;
                    case "X13":
                        c.Exchanges[0].BtcPrice /= 3;
                        break;
                    case "SCRYPTN":
                        c.Exchanges[0].BtcPrice /= 0.47;
                        break;
                }

                c.Exchanges[0].BtcPrice /= c.BlockReward;
                c.Exchanges[0].BtcPrice *= 1000;
                c.BlockReward = 0;
                Add(c);
            }
        }

        public void CalculatePrices(bool useWeightedCalculation, bool calcFiat)
        {
            double usdPrice = 0, eurPrice = 0, gbpPrice = 0, cnyPrice = 0;

            if (calcFiat)
            {
                CoinDesk cd = JsonControl.DownloadSerializedApi<CoinDesk>(
                    _client.GetStreamAsync("https://api.coindesk.com/v1/bpi/currentprice.json").Result);
                usdPrice = cd.BpiPrice.UsdPrice.RateFloat;
                eurPrice = cd.BpiPrice.EurPrice.RateFloat;
                gbpPrice = cd.BpiPrice.GbpPrice.RateFloat;

                cd = JsonControl.DownloadSerializedApi<CoinDesk>(
                        _client.GetStreamAsync("https://api.coindesk.com/v1/bpi/currentprice/CNY.json").Result);
                cnyPrice = cd.BpiPrice.CnyPrice.RateFloat;
            }

            Parallel.ForEach(List, coin => Parallel.ForEach(UsedProfile.CustomAlgoList, _po, algo =>
            {
                if (coin.Algo == algo.Name)
                {
                    coin.CalcProfitability(algo.HashRate, useWeightedCalculation, UsedProfile.Multiplier, algo.Style, algo.Target);

                    if (calcFiat)
                    {
                        double fiatElectricityCost = (algo.Wattage / 1000) * 24 * UsedProfile.FiatPerKwh;
                        switch (UsedProfile.FiatOfChoice)
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

                    _po.CancellationToken.ThrowIfCancellationRequested();
                }
            }));

            List = List.AsParallel().OrderByDescending(o => o.BtcPerDay).ToList();
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