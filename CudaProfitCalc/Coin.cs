using System;
using System.Collections.Generic;
using CudaProfitCalc.ApiControl;

namespace CudaProfitCalc
{
    class Coin
    {
        public string CoinName { get; set; }
        public string TagName { get; set; }

        public HashAlgo.Algo Algo { get; set; }
        public double Difficulty { get; set; }
        public double BlockReward { get; set; }

        public Exchange BestExchange { get; set; }
        internal class Exchange
        {
            public string ExchangeName { get; set; }
            public double BtcPrice { get; set; }
            public double BtcVolume { get; set; }

            public override string ToString()
            {
                return ExchangeName + " | BTC Price: " + BtcPrice + " | BTC Volume: " + BtcVolume;
            }
        }

        public double CoinsPerDay { get; set; }
        public double BtcPerDay { get; set; }

        public bool HasMarketErrors { get; set; }
        public bool HasImplementedMarketApi { get; set; }
        public readonly bool IsNiceHash;

        public Coin(string coinName, string tagName, HashAlgo.Algo algo,
            double difficulty, double last24HDifficulty, double blockReward, Exchange exchange)
        {
            CoinName = coinName;
            TagName = tagName.ToUpper();
            Algo = algo;
            if (TagName == "MYR" && Algo == HashAlgo.Algo.Groestl) Algo = HashAlgo.Algo.MyriadGroestl;
            Difficulty = difficulty;
            BlockReward = blockReward;
            BestExchange = exchange;
            HasMarketErrors = false;
            IsNiceHash = false;
            HasImplementedMarketApi = false;
        }

        public Coin(NiceHash.Result.Stat niceHashStat)
        {
            switch (niceHashStat.Algo)
            {
                case 0: 
                    Algo = HashAlgo.Algo.Scrypt;
                    break;
                case 3: 
                    Algo = HashAlgo.Algo.X11;
                    break;
                case 4:
                    Algo = HashAlgo.Algo.X13;
                    break;
                case 5:
                    Algo = HashAlgo.Algo.Keccak;
                    break;
                default:
                    Algo = HashAlgo.Algo.Unknown;
                    break;
            }

            CoinName = "NiceHash" + Algo;
            TagName = "NICE" + Algo.ToString().ToUpper();
            Difficulty = niceHashStat.Speed;
            BlockReward = niceHashStat.Price;

            BestExchange = new Exchange { ExchangeName = "NiceHash", BtcPrice = niceHashStat.Price, BtcVolume = 0 };

            HasMarketErrors = false;
            IsNiceHash = true;
            HasImplementedMarketApi = true;

        }

        public Coin(KeyValuePair<string, WhatToMine.Coin> wtmCoin)
        {
            CoinName = wtmCoin.Key;
            TagName = wtmCoin.Value.Tag.ToUpper();
            Algo = HashAlgo.GetAlgorithm(wtmCoin.Value.Algorithm);
            if (TagName == "MYR" && Algo == HashAlgo.Algo.Groestl) Algo = HashAlgo.Algo.MyriadGroestl;
            Difficulty = wtmCoin.Value.Difficulty;
            BlockReward = wtmCoin.Value.BlockReward;
            BestExchange = new Exchange { ExchangeName = "Unknown (WhatToMine)", BtcPrice = wtmCoin.Value.ExchangeRate, BtcVolume = wtmCoin.Value.Volume * wtmCoin.Value.ExchangeRate };
            HasMarketErrors = false;
            IsNiceHash = false;
            HasImplementedMarketApi = false;
        }

        public Coin(CoinTweak.Coin ctwCoin)
        {
            CoinName = ctwCoin.CoinFullname;
            TagName = ctwCoin.CoinName.ToUpper();
            Algo = HashAlgo.GetAlgorithm(ctwCoin.AlgoName);
            if (TagName == "MYR" && Algo == HashAlgo.Algo.Groestl) Algo = HashAlgo.Algo.MyriadGroestl;
            Difficulty = ctwCoin.Difficulty;
            BlockReward = ctwCoin.BlockCoins;
            BestExchange = new Exchange { ExchangeName = ctwCoin.ExName + " (CoinTweak)", BtcPrice = ctwCoin.ConversionRateToBtc, BtcVolume = ctwCoin.BtcVol };
            HasMarketErrors = !ctwCoin.HasBuyOffers;
            IsNiceHash = false;
            HasImplementedMarketApi = false;
        }

        public Coin(CoinWarz.Coin cwzCoin)
        {
            CoinName = cwzCoin.CoinName;
            TagName = cwzCoin.CoinTag.ToUpper();
            Algo = HashAlgo.GetAlgorithm(cwzCoin.Algorithm);
            if (TagName == "MYR" && Algo == HashAlgo.Algo.Groestl) Algo = HashAlgo.Algo.MyriadGroestl;
            Difficulty = cwzCoin.Difficulty;
            BlockReward = cwzCoin.BlockReward;
            BestExchange = new Exchange { ExchangeName = cwzCoin.Exchange + " (CoinWarz)", BtcPrice = cwzCoin.ExchangeRate, BtcVolume = cwzCoin.ExchangeVolume * cwzCoin.ExchangeRate };
            HasMarketErrors = cwzCoin.HealthStatus.Contains("Unhealthy");
            IsNiceHash = false;
            HasImplementedMarketApi = false;
        }

        public void CalcProfitability(double hashRateMh)
        {
            if (IsNiceHash)
            {
                CoinsPerDay = 0;
                BtcPerDay = (hashRateMh/1000)*BestExchange.BtcPrice;
            }
            else
            {
                double magicNumber = Math.Pow(2, 32);
                if (Algo == HashAlgo.Algo.Quark) magicNumber = Math.Pow(2, 24);

                CoinsPerDay = BlockReward / (Difficulty * magicNumber / (hashRateMh * 1000000) / 3600 / 24);
                BtcPerDay = CoinsPerDay * BestExchange.BtcPrice;
            }
        }
    }
}
