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
                if (c.TagName == newCoin.TagName && c.Algo == newCoin.Algo)
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
            NiceHash niceHashData = Api.DownloadSerializedApi<NiceHash>(address);
            Add(new Coin(niceHashData.Results.Stats[0]));
            Add(new Coin(niceHashData.Results.Stats[3]));
            Add(new Coin(niceHashData.Results.Stats[4]));
            Add(new Coin(niceHashData.Results.Stats[5]));
        }

        public void UpdateWhatToMine(string address)
        {
            WhatToMine wtmData = Api.DownloadSerializedApi<WhatToMine>(address);
            foreach (KeyValuePair<string, WhatToMine.Coin> wtmCoin in wtmData.Coins)
            {
                Coin c = new Coin(wtmCoin);
                if(!c.HasMarketErrors)Add(c);
            }
        }

        public void UpdateCoinTweak(string address)
        {
            CoinTweak ctwData = Api.DownloadSerializedApi<CoinTweak>(address);
            foreach (CoinTweak.Coin ctwCoin in ctwData.Coins)
            {
                Coin c = new Coin(ctwCoin);
                if (!c.HasMarketErrors) Add(c);
            }
        }

        public void UpdateCoinWarz(string address)
        {
            CoinWarz cwzData = Api.DownloadSerializedApi<CoinWarz>(address);
            foreach (CoinWarz.Coin cwzCoin in cwzData.Data)
            {
                Coin c = new Coin(cwzCoin);
                if (!c.HasMarketErrors) Add(c);
            }
        }

        public void UpdateMintPal(string address)
        {
            MintPal mp = Api.DownloadSerializedApi<MintPal>(address);
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
            Cryptsy cp = Api.DownloadSerializedApi<Cryptsy>(address);
            foreach (Coin c in List)
            {
                foreach (KeyValuePair<string, Cryptsy.Return.Market> cpCoin in cp.Returns.Markets)
                {
                    if (cpCoin.Value.SecondaryCode == "BTC" && cpCoin.Value.PrimaryCode == c.TagName &&
                        ((cpCoin.Value.Volume/cpCoin.Value.LastTradePrice) > c.BestExchange.BtcVolume ||
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
            Bittrex bt = Api.DownloadSerializedApi<Bittrex>(address);
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
                sb.Append("TAG: " + c.TagName + " | Name:" + c.CoinName + " | Algo: " + c.Algo 
                    + " | BTC/day: " + c.BtcPerDay.ToString("0.000000000") + " | Coins/day: " + c.CoinsPerDay
                    + " | Best exchange: " + c.BestExchange.ExchangeName + " | BTC price: " + c.BestExchange.BtcPrice.ToString("0.000000000")
                    + " | BTC volume: " + c.BestExchange.BtcVolume + " | Difficulty: " + c.Difficulty + " | Blockreward: " + c.BlockReward + Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
