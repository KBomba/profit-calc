using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProfitCalc
{
    public sealed partial class DetailedResult : Form
    {
        private readonly Coin _usedCoin;

        private bool _useBtc = true;
        private int _depth = 20, _boxPercentage = 25, _whiskerPercentage = 5;

        public DetailedResult(Coin usedCoin)
        {
            InitializeComponent();
            _usedCoin = usedCoin;

            Text = "[" + _usedCoin.TagName + "] " + _usedCoin.FullName + " - Detailed results";

            FillGeneralInfo();
            FillMarketInfo();
        }

        private void FillGeneralInfo()
        {
            txtTag.Text = _usedCoin.TagName;
            txtName.Text = _usedCoin.FullName;
            txtAlgo.Text = _usedCoin.Algo;
            txtSource.Text = _usedCoin.Source;
            txtRetrieved.Text = _usedCoin.Retrieved.ToString("dd MMMM HH:mm");

            txtHeight.Text = _usedCoin.Height.ToString(CultureInfo.InvariantCulture);
            txtDiff.Text = _usedCoin.Difficulty.ToString("0.########");
            txt24HAvgDiff.Text = _usedCoin.Avg24HDifficulty.ToString("0.########");
            txtNetHashrate.Text = (_usedCoin.NetHashRate / 1000000).ToString("0.####");
            txtBlockTime.Text = _usedCoin.BlockTime.ToString("0.####");
            txtBlockReward.Text = _usedCoin.BlockReward.ToString("0.####");

            txtCoinsPerDay.Text = _usedCoin.CoinsPerDay.ToString("0.####");
            txtBtcPerDay.Text = _usedCoin.BtcPerDay.ToString("0.########");
            txtUsdPerDay.Text = _usedCoin.UsdPerDay.ToString("0.####");
            txtEurPerDay.Text = _usedCoin.EurPerDay.ToString("0.####");
            txtGbpPerDay.Text = _usedCoin.GbpPerDay.ToString("0.####");
            txtCnyPerDay.Text = _usedCoin.CnyPerDay.ToString("0.####");
        }

        private void FillMarketInfo()
        {
            tbcMarkets.TabPages.Clear();

            InitAllMarketsTab();

            foreach (Coin.Exchange exchange in _usedCoin.Exchanges)
            {
                switch (exchange.ExchangeName)
                {
                    case "Bittrex":
                        InitBittrex(exchange);
                        break;
                    case "MintPal":
                        InitMintPal(exchange);
                        break;
                    case "Poloniex":
                        InitPoloniex(exchange);
                        break;
                    case "Cryptsy":
                        InitCryptsy(exchange);
                        break;
                    case "BTer":
                        InitBTer(exchange);
                        break;
                    case "C-Cex":
                        InitCCex(exchange);
                        break;
                    case "Comkort":
                        InitComkort(exchange);
                        break;
                    case "AllCrypt":
                        InitAllCrypt(exchange);
                        break;
                    case "AllCoin":
                        InitAllCoin(exchange);
                        break;
                    case "Atomic Trade":
                        InitAtomic(exchange);
                        break;
                    case "Cryptoine":
                        InitCryptoine(exchange);
                        break;
                }
            }
        }

        private void InitAllMarketsTab()
        {
            tbcMarkets.TabPages.Add(tabAllMarkets);
            
            txtAllFallthrough.Text = _usedCoin.TotalExchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtAllLeftover.Text = _usedCoin.TotalExchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtAllDailyvolume.Text = _usedCoin.TotalExchange.BtcVolume.ToString("0.####") + " BTC";

            int amountOfExchanges = _usedCoin.HasImplementedMarketApi ? _usedCoin.Exchanges.Count : 0;
            string exchanges = amountOfExchanges == 1 ? " exchange" : " exchanges";
            txtAllExchanges.Text = amountOfExchanges.ToString(CultureInfo.InvariantCulture) + exchanges;

            linkAll.Text = _usedCoin.TagName + " (Google Search)";
            linkAll.Links[0] = new LinkLabel.Link(0, linkAll.Text.Length,
                "https://www.google.com/?gws_rd=ssl#q=" + _usedCoin.TagName + "+" + _usedCoin.FullName + "+crypto");

            UpdateAllMarketsTab();
        }

        

        private void InitBittrex(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabBittrex);

            txtBittrexFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtBittrexLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " +_usedCoin.TagName;
            txtBittrexDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtBittrexWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkBittrex.Text = "Bittrex: " + _usedCoin.TagName + "/BTC";
            linkBittrex.Links[0] = new LinkLabel.Link(0, linkBittrex.Text.Length, 
                "https://bittrex.com/Market/Index?MarketName=BTC-" + _usedCoin.TagName);

            UpdateBittrex(exchange);
        }

        private void InitMintPal(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabMintpal);

            txtMintpalFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtMintpalLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtMintpalDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtMintpalWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkMintpal.Text = "MintPal: " + _usedCoin.TagName + "/BTC";
            linkMintpal.Links[0] = new LinkLabel.Link(0, linkMintpal.Text.Length,
                "https://www.mintpal.com/market/" + _usedCoin.TagName + "/BTC");

            UpdateMintpal(exchange);
        }

        private void InitPoloniex(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabPoloniex);

            txtPoloniexFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtPoloniexLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtPoloniexDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtPoloniexWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkPoloniex.Text = "Poloniex: " + _usedCoin.TagName + "/BTC";
            linkPoloniex.Links[0] = new LinkLabel.Link(0, linkPoloniex.Text.Length,
                "https://poloniex.com/exchange/btc_" + _usedCoin.TagName.ToLowerInvariant());

            UpdatePoloniex(exchange);
        }

        private void InitCryptsy(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabCryptsy);

            txtCryptsyFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtCryptsyLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtCryptsyDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtCryptsyWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkCryptsy.Text = "Cryptsy: " + _usedCoin.TagName + "/BTC";
            linkCryptsy.Links[0] = new LinkLabel.Link(0, linkCryptsy.Text.Length,
                "https://www.google.com/search?q=cryptsy+" + _usedCoin.TagName + "+btc&btnI=&gws_rd=ssl");

            UpdateCryptsy(exchange);
        }

        private void InitBTer(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabBTer);

            txtBterFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtBterLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtBterDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtBterWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkBter.Text = "BTer: " + _usedCoin.TagName + "/BTC";
            linkBter.Links[0] = new LinkLabel.Link(0, linkBter.Text.Length,
                "https://bter.com/trade/" + _usedCoin.TagName.ToLowerInvariant() + "_btc");

            UpdateBTer(exchange);
        }

        private void InitCCex(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabCCex);

            txtCcexFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtCcexLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtCcexDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtCcexWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkCcex.Text = "C-Cex: " + _usedCoin.TagName + "/BTC";
            linkCcex.Links[0] = new LinkLabel.Link(0, linkCcex.Text.Length,
                "https://c-cex.com/?p=" + _usedCoin.TagName.ToLowerInvariant() + "-btc");

            UpdateCCex(exchange);
        }
        
        private void InitComkort(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabComkort);

            txtComkortFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtComkortLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtComkortDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtComkortWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkComkort.Text = "Comkort: " + _usedCoin.TagName + "/BTC";
            linkComkort.Links[0] = new LinkLabel.Link(0, linkComkort.Text.Length,
                "https://comkort.com/market/trade/" + _usedCoin.TagName.ToLowerInvariant() + "_btc");

            UpdateComkort(exchange);
        }
        
        private void InitAllCrypt(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabAllCrypt);

            txtAllcryptFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtAllcryptLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtAllcryptDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtAllcryptWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkAllcrypt.Text = "Allcrypt: " + _usedCoin.TagName + "/BTC";
            linkAllcrypt.Links[0] = new LinkLabel.Link(0, linkAllcrypt.Text.Length,
                "https://www.google.com/search?q=allcrypt+" + _usedCoin.TagName + "+btc&btnI=&gws_rd=ssl");

            UpdateAllCrypt(exchange);
        }
        
        private void InitAllCoin(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabAllCoin);

            txtAllcoinFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtAllcoinLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtAllcoinDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtAllcoinWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkAllcoin.Text = "Allcoin: " + _usedCoin.TagName + "/BTC";
            linkAllcoin.Links[0] = new LinkLabel.Link(0, linkAllcoin.Text.Length,
                "https://www.allcoin.com/trade/" + _usedCoin.TagName + "_BTC");

            UpdateAllcoin(exchange);
        }

        private void InitAtomic(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabAtomicTrade);

            txtAtomicFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtAtomicLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtAtomicDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtAtomicWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkAtomic.Text = "Atomic: " + _usedCoin.TagName + "/BTC";
            linkAtomic.Links[0] = new LinkLabel.Link(0, linkAtomic.Text.Length,
                "https://www.atomic-trade.com/markets");

            UpdateAtomic(exchange);
        }

        private void InitCryptoine(Coin.Exchange exchange)
        {
            tbcMarkets.TabPages.Add(tabCryptoine);

            txtCryptoineFallthrough.Text = exchange.FallThroughPrice.ToString("0.00000000") + " BTC";
            txtCryptoineLeftover.Text = exchange.LeftOverInFallThrough.ToString("0.########") + " " + _usedCoin.TagName;
            txtCryptoineDailyvolume.Text = exchange.BtcVolume.ToString("0.####") + " BTC";
            txtCryptoineWeight.Text = (exchange.Weight * 100).ToString("0.##") + "%";

            linkCryptoine.Text = "Cryptoine: " + _usedCoin.TagName + "/BTC";
            linkCryptoine.Links[0] = new LinkLabel.Link(0, linkCryptoine.Text.Length,
                "https://cryptoine.com/trade/" + _usedCoin.TagName.ToLowerInvariant() + "_btc");

            UpdateCryptoine(exchange);
        }
        
        private double[] GetBoxPlotValues(int boxPercentage, int whiskerPercentage,
            int depth, List<Coin.Exchange.Order> orderList, bool useBtcVolume)
        {
            double whiskerLowest = whiskerPercentage/(double)100;
            double whiskerHighest = 1 - whiskerLowest;
            double boxLowest = boxPercentage / (double)100;
            double boxHighest = 1 - boxLowest;

            return new[]
            {
                GetDepthPercentile(whiskerLowest, depth, orderList, useBtcVolume),
                GetDepthPercentile(whiskerHighest, depth, orderList, useBtcVolume),
                GetDepthPercentile(boxLowest, depth, orderList, useBtcVolume),
                GetDepthPercentile(boxHighest, depth, orderList, useBtcVolume),
                GetDepthPercentile(0.5, depth, orderList, useBtcVolume),
                GetWeightedAverage(depth, orderList, useBtcVolume)
            };
        }

        private double GetDepthPercentile(double percentage, int depth, 
            List<Coin.Exchange.Order> orderList, bool useBtcVolume)
        {
            if (orderList != null && orderList.Any())
            {
                double[] data = new double[orderList.Count];
                double[] weight = new double[orderList.Count];
                int max = orderList.Count < depth ? orderList.Count : depth;
                Parallel.For(0, max, i =>
                {
                    data[i] = orderList[i].BtcPrice;
                    weight[i] = useBtcVolume
                        ? orderList[i].BtcVolume
                        : orderList[i].CoinVolume;
                });

                Array.Sort(data, weight);

                double sumOfAllWeights = weight.Sum();
                double weightCounter = 0;
                for (int i = 0; i < weight.Length; i++)
                {
                    weight[i] = weight[i]/sumOfAllWeights;
                    weightCounter += weight[i];
                    if (weightCounter >= percentage)
                    {
                        return data[i];
                    }
                }
            }
            return 0;
        }

        private double GetWeightedAverage(int depth, List<Coin.Exchange.Order> orderList, bool useBtcVolume)
        {
            if (orderList == null || !orderList.Any()) return 0;

            double sumOfWeightedPrices = 0, sumOfWeights = 0;
            int max = orderList.Count < depth ? orderList.Count : depth;
            for (int i = 0; i < max; i++)
            {
                double weight = useBtcVolume ?
                    orderList[i].BtcVolume :
                    orderList[i].CoinVolume;
                sumOfWeightedPrices += orderList[i].BtcPrice * weight;
                sumOfWeights += weight;
            }

            if (sumOfWeights == 0) return 0;
            return sumOfWeightedPrices/sumOfWeights;
        }

        private void chkBtc_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk != null)
            {
                _useBtc = chk.Checked;
                UpdateActiveExchanges();
            }
        }

        private void trackDepth_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            if (tb != null)
            {
                _depth = tb.Value;
                UpdateActiveExchanges();
            }
        }

        private void trackBox_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            if (tb != null)
            {
                _boxPercentage = tb.Value;
                UpdateActiveExchanges();
            }
        }

        private void trackWhisker_Scroll(object sender, EventArgs e)
        {
            TrackBar tb = sender as TrackBar;
            if (tb != null)
            {
                _whiskerPercentage = tb.Value;
                UpdateActiveExchanges();
            }
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            if (linkLabel != null) linkLabel.LinkVisited = true;

            string link = e.Link.LinkData as string;
            if (link != null) Process.Start(link);
        }

        private void UpdateActiveExchanges()
        {
            UpdateAllMarketsTab();
            foreach (Coin.Exchange exchange in _usedCoin.Exchanges)
            {
                switch (exchange.ExchangeName)
                {
                    case "Bittrex":
                        UpdateBittrex(exchange);
                        break;
                    case "MintPal":
                        UpdateMintpal(exchange);
                        break;
                    case "Poloniex":
                        UpdatePoloniex(exchange);
                        break;
                    case "Cryptsy":
                        UpdateCryptsy(exchange);
                        break;
                    case "BTer":
                        UpdateBTer(exchange);
                        break;
                    case "C-Cex":
                        UpdateCCex(exchange);
                        break;
                    case "Comkort":
                        UpdateComkort(exchange);
                        break;
                    case "AllCrypt":
                        UpdateAllCrypt(exchange);
                        break;
                    case "AllCoin":
                        UpdateAllcoin(exchange);
                        break;
                    case "Atomic Trade":
                        UpdateAtomic(exchange);
                        break;
                    case "Cryptoine":
                        UpdateCryptoine(exchange);
                        break;
                }
            }
        }

        private void UpdateAllMarketsTab()
        {
            chkAllBtc.Checked = _useBtc;
            lblAllDepth.Text = "Depth: " + _depth + " orders";
            trackAllDepth.Value = _depth;
            lblAllBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackAllBox.Value = _boxPercentage;
            lblAllWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackAllWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                _usedCoin.TotalExchange.BuyOrders, _useBtc);
            chartAllBuy.Series["BoxPlotSeries"].Points.Clear();
            chartAllBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvAllBuy.DataSource = _usedCoin.TotalExchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                _usedCoin.TotalExchange.SellOrders, _useBtc);
            chartAllSell.Series["BoxPlotSeries"].Points.Clear();
            chartAllSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvAllSell.DataSource = _usedCoin.TotalExchange.SellOrders;
        }

        private void UpdateBittrex(Coin.Exchange exchange)
        {
            chkBittrexBtc.Checked = _useBtc;
            lblBittrexDepth.Text = "Depth: " + _depth + " orders";
            trackBittrexDepth.Value = _depth;
            lblBittrexBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackBittrexBox.Value = _boxPercentage;
            lblBittrexWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackBittrexWhisker.Value = _whiskerPercentage;
            
            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth, 
                exchange.BuyOrders, _useBtc);
            chartBittrexBuy.Series["BoxPlotSeries"].Points.Clear();
            chartBittrexBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvBittrexBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartBittrexSell.Series["BoxPlotSeries"].Points.Clear();
            chartBittrexSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvBittrexSell.DataSource = exchange.SellOrders;
        }

        private void UpdateMintpal(Coin.Exchange exchange)
        {
            chkMintpalBtc.Checked = _useBtc;
            lblMintpalDepth.Text = "Depth: " + _depth + " orders";
            trackMintpalDepth.Value = _depth;
            lblMintpalBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackMintpalBox.Value = _boxPercentage;
            lblMintpalWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackMintpalWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartMintpalBuy.Series["BoxPlotSeries"].Points.Clear();
            chartMintpalBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvMintpalBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartMintpalSell.Series["BoxPlotSeries"].Points.Clear();
            chartMintpalSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvMintpalSell.DataSource = exchange.SellOrders;
        }
        
        private void UpdatePoloniex(Coin.Exchange exchange)
        {
            chkPoloniexBtc.Checked = _useBtc;
            lblPoloniexDepth.Text = "Depth: " + _depth + " orders";
            trackPoloniexDepth.Value = _depth;
            lblPoloniexBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackPoloniexBox.Value = _boxPercentage;
            lblPoloniexWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackPoloniexWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartPoloniexBuy.Series["BoxPlotSeries"].Points.Clear();
            chartPoloniexBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvPoloniexBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartPoloniexSell.Series["BoxPlotSeries"].Points.Clear();
            chartPoloniexSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvPoloniexSell.DataSource = exchange.SellOrders;
        }

        private void UpdateCryptsy(Coin.Exchange exchange)
        {
            chkCryptsyBtc.Checked = _useBtc;
            lblCryptsyDepth.Text = "Depth: " + _depth + " orders";
            trackCryptsyDepth.Value = _depth;
            lblCryptsyBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackCryptsyBox.Value = _boxPercentage;
            lblCryptsyWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackCryptsyWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartCryptsyBuy.Series["BoxPlotSeries"].Points.Clear();
            chartCryptsyBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvCryptsyBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartCryptsySell.Series["BoxPlotSeries"].Points.Clear();
            chartCryptsySell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvCryptsySell.DataSource = exchange.SellOrders;
        }

        private void UpdateBTer(Coin.Exchange exchange)
        {
            chkBterBtc.Checked = _useBtc;
            lblBterDepth.Text = "Depth: " + _depth + " orders";
            trackBterDepth.Value = _depth;
            lblBterBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackBterBox.Value = _boxPercentage;
            lblBterWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackBterWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartBterBuy.Series["BoxPlotSeries"].Points.Clear();
            chartBterBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvBterBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartBterSell.Series["BoxPlotSeries"].Points.Clear();
            chartBterSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvBterSell.DataSource = exchange.SellOrders;
        }

        private void UpdateCCex(Coin.Exchange exchange)
        {
            chkCcexBtc.Checked = _useBtc;
            lblCcexDepth.Text = "Depth: " + _depth + " orders";
            trackCcexDepth.Value = _depth;
            lblCcexBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackCcexBox.Value = _boxPercentage;
            lblCcexWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackCcexWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartCcexBuy.Series["BoxPlotSeries"].Points.Clear();
            chartCcexBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvCcexBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartCcexSell.Series["BoxPlotSeries"].Points.Clear();
            chartCcexSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvCcexSell.DataSource = exchange.SellOrders;
        }

        private void UpdateComkort(Coin.Exchange exchange)
        {
            chkComkortBtc.Checked = _useBtc;
            lblComkortDepth.Text = "Depth: " + _depth + " orders";
            trackComkortDepth.Value = _depth;
            lblComkortBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackComkortBox.Value = _boxPercentage;
            lblComkortWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackComkortWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartComkortBuy.Series["BoxPlotSeries"].Points.Clear();
            chartComkortBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvComkortBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartComkortSell.Series["BoxPlotSeries"].Points.Clear();
            chartComkortSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvComkortSell.DataSource = exchange.SellOrders;
        }

        private void UpdateAllCrypt(Coin.Exchange exchange)
        {
            chkAllcryptBtc.Checked = _useBtc;
            lblAllcryptDepth.Text = "Depth: " + _depth + " orders";
            trackAllcryptDepth.Value = _depth;
            lblAllcryptBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackAllcryptBox.Value = _boxPercentage;
            lblAllcryptWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackAllcryptWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartAllcryptBuy.Series["BoxPlotSeries"].Points.Clear();
            chartAllcryptBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvAllcryptBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartAllcryptSell.Series["BoxPlotSeries"].Points.Clear();
            chartAllcryptSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvAllcryptSell.DataSource = exchange.SellOrders;
        }

        private void UpdateAllcoin(Coin.Exchange exchange)
        {
            chkAllcoinBtc.Checked = _useBtc;
            lblAllcoinDepth.Text = "Depth: " + _depth + " orders";
            trackAllcoinDepth.Value = _depth;
            lblAllcoinBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackAllcoinBox.Value = _boxPercentage;
            lblAllcoinWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackAllcoinWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartAllcoinBuy.Series["BoxPlotSeries"].Points.Clear();
            chartAllcoinBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvAllcoinBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartAllcoinSell.Series["BoxPlotSeries"].Points.Clear();
            chartAllcoinSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvAllcoinSell.DataSource = exchange.SellOrders;
        }
        
        private void UpdateAtomic(Coin.Exchange exchange)
        {
            chkAtomicBtc.Checked = _useBtc;
            lblAtomicDepth.Text = "Depth: " + _depth + " orders";
            trackAtomicDepth.Value = _depth;
            lblAtomicBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackAtomicBox.Value = _boxPercentage;
            lblAtomicWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackAtomicWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartAtomicBuy.Series["BoxPlotSeries"].Points.Clear();
            chartAtomicBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvAtomicBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartAtomicSell.Series["BoxPlotSeries"].Points.Clear();
            chartAtomicSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvAtomicSell.DataSource = exchange.SellOrders;
        }

        private void UpdateCryptoine(Coin.Exchange exchange)
        {
            chkCryptoineBtc.Checked = _useBtc;
            lblCryptoineDepth.Text = "Depth: " + _depth + " orders";
            trackCryptoineDepth.Value = _depth;
            lblCryptoineBox.Text = "Box percentage: " + _boxPercentage + "% - " + (100 - _boxPercentage) + "%";
            trackCryptoineBox.Value = _boxPercentage;
            lblCryptoineWhisker.Text = "Whisker percentage: " + _whiskerPercentage + "% - " + (100 - _whiskerPercentage) + "%";
            trackCryptoineWhisker.Value = _whiskerPercentage;

            double[] yBuyValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.BuyOrders, _useBtc);
            chartCryptoineBuy.Series["BoxPlotSeries"].Points.Clear();
            chartCryptoineBuy.Series["BoxPlotSeries"].Points.Add(yBuyValues);
            dgvCryptoineBuy.DataSource = exchange.BuyOrders;

            double[] ySellValues = GetBoxPlotValues(_boxPercentage, _whiskerPercentage, _depth,
                exchange.SellOrders, _useBtc);
            chartCryptoineSell.Series["BoxPlotSeries"].Points.Clear();
            chartCryptoineSell.Series["BoxPlotSeries"].Points.Add(ySellValues);
            dgvCryptoineSell.DataSource = exchange.SellOrders;
        }
    }
}
