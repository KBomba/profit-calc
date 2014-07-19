using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ProfitCalc
{
    public partial class ProfitCalc : Form
    {
        private CoinList _coinList;
        private HashRateJson _hashList;
        private decimal _hashRateMultiplier;

        public ProfitCalc()
        {
            InitializeComponent();
            InitializeOtherComponents();
            LoadSettings();
            UpdateChkAllState();
        }

        private void InitializeOtherComponents()
        {
            foreach (TabPage page in tabControlSettings.TabPages)
            {
                page.BackColor = SystemColors.Menu;
            }
            txtLog.BackColor = SystemColors.Window;
            AppendToLog("Loading settings");

            cbbFiat.SelectedIndex = 0;
            cbbBidRecentAsk.SelectedIndex = 0;
        }

        private void LoadSettings()
        {
            if (File.Exists("hashrates.txt"))
            {
                try
                {

                    HashRateJson rates = JsonControl.GetSerializedApiFile<HashRateJson>("hashrates.txt");
                    txtGroestl.Text = rates.ListHashRate[HashAlgo.Algo.Groestl].ToString(CultureInfo.InvariantCulture);
                    txtMyrGroestl.Text =
                        rates.ListHashRate[HashAlgo.Algo.MyriadGroestl].ToString(CultureInfo.InvariantCulture);
                    txtFugue.Text = rates.ListHashRate[HashAlgo.Algo.Fugue256].ToString(CultureInfo.InvariantCulture);
                    txtKeccak.Text = rates.ListHashRate[HashAlgo.Algo.Keccak].ToString(CultureInfo.InvariantCulture);
                    txtJackpot.Text = rates.ListHashRate[HashAlgo.Algo.JHA].ToString(CultureInfo.InvariantCulture);
                    txtNist5.Text = rates.ListHashRate[HashAlgo.Algo.Nist5].ToString(CultureInfo.InvariantCulture);
                    txtQuark.Text = rates.ListHashRate[HashAlgo.Algo.Quark].ToString(CultureInfo.InvariantCulture);
                    txtQubit.Text = rates.ListHashRate[HashAlgo.Algo.Qubit].ToString(CultureInfo.InvariantCulture);
                    txtScrypt.Text = rates.ListHashRate[HashAlgo.Algo.Scrypt].ToString(CultureInfo.InvariantCulture);
                    txtX11.Text = rates.ListHashRate[HashAlgo.Algo.X11].ToString(CultureInfo.InvariantCulture);
                    txtX13.Text = rates.ListHashRate[HashAlgo.Algo.X13].ToString(CultureInfo.InvariantCulture);
                    txtX15.Text = rates.ListHashRate[HashAlgo.Algo.X15].ToString(CultureInfo.InvariantCulture);
                    txtHefty.Text = rates.ListHashRate[HashAlgo.Algo.Heavy].ToString(CultureInfo.InvariantCulture);
                    txtScryptN.Text = rates.ListHashRate[HashAlgo.Algo.ScryptN].ToString(CultureInfo.InvariantCulture);
                    txtJane15.Text =
                        rates.ListHashRate[HashAlgo.Algo.ScryptJane15].ToString(CultureInfo.InvariantCulture);
                    txtJane14.Text =
                        rates.ListHashRate[HashAlgo.Algo.ScryptJane14].ToString(CultureInfo.InvariantCulture);
                    txtJane13.Text =
                        rates.ListHashRate[HashAlgo.Algo.ScryptJane13].ToString(CultureInfo.InvariantCulture);
                    txtCryptonight.Text =
                        rates.ListHashRate[HashAlgo.Algo.CryptoNight].ToString(CultureInfo.InvariantCulture);

                    txtGroestlWattage.Text =
                        rates.ListWattage[HashAlgo.Algo.Groestl].ToString(CultureInfo.InvariantCulture);
                    txtMyrGroestlWattage.Text =
                        rates.ListWattage[HashAlgo.Algo.MyriadGroestl].ToString(CultureInfo.InvariantCulture);
                    txtFugueWattage.Text =
                        rates.ListWattage[HashAlgo.Algo.Fugue256].ToString(CultureInfo.InvariantCulture);
                    txtKeccakWattage.Text =
                        rates.ListWattage[HashAlgo.Algo.Keccak].ToString(CultureInfo.InvariantCulture);
                    txtJhaWattage.Text = rates.ListWattage[HashAlgo.Algo.JHA].ToString(CultureInfo.InvariantCulture);
                    txtNist5Wattage.Text = rates.ListWattage[HashAlgo.Algo.Nist5].ToString(CultureInfo.InvariantCulture);
                    txtQuarkWattage.Text = rates.ListWattage[HashAlgo.Algo.Quark].ToString(CultureInfo.InvariantCulture);
                    txtQubitWattage.Text = rates.ListWattage[HashAlgo.Algo.Qubit].ToString(CultureInfo.InvariantCulture);
                    txtScryptWattage.Text =
                        rates.ListWattage[HashAlgo.Algo.Scrypt].ToString(CultureInfo.InvariantCulture);
                    txtX11Wattage.Text = rates.ListWattage[HashAlgo.Algo.X11].ToString(CultureInfo.InvariantCulture);
                    txtX13Wattage.Text = rates.ListWattage[HashAlgo.Algo.X13].ToString(CultureInfo.InvariantCulture);
                    txtX15Wattage.Text = rates.ListWattage[HashAlgo.Algo.X15].ToString(CultureInfo.InvariantCulture);
                    txtHeftyWattage.Text = rates.ListWattage[HashAlgo.Algo.Heavy].ToString(CultureInfo.InvariantCulture);
                    txtScryptNWattage.Text =
                        rates.ListWattage[HashAlgo.Algo.ScryptN].ToString(CultureInfo.InvariantCulture);
                    txtJane15Wattage.Text =
                        rates.ListWattage[HashAlgo.Algo.ScryptJane15].ToString(CultureInfo.InvariantCulture);
                    txtJane14Wattage.Text =
                        rates.ListWattage[HashAlgo.Algo.ScryptJane14].ToString(CultureInfo.InvariantCulture);
                    txtJane13Wattage.Text =
                        rates.ListWattage[HashAlgo.Algo.ScryptJane13].ToString(CultureInfo.InvariantCulture);
                    txtCryptonightWattage.Text =
                        rates.ListWattage[HashAlgo.Algo.CryptoNight].ToString(CultureInfo.InvariantCulture);

                    chkGroestl.Checked = rates.CheckedHashRates["Groestl"];
                    chkMyrGroestl.Checked = rates.CheckedHashRates["MyrGroestl"];
                    chkFugue.Checked = rates.CheckedHashRates["Fugue256"];
                    chkJha.Checked = rates.CheckedHashRates["JHA"];
                    chkNist5.Checked = rates.CheckedHashRates["NIST5"];
                    chkHefty.Checked = rates.CheckedHashRates["Hefty"];
                    chkX11.Checked = rates.CheckedHashRates["X11"];
                    chkX13.Checked = rates.CheckedHashRates["X13"];
                    chkX15.Checked = rates.CheckedHashRates["X15"];
                    chkQuark.Checked = rates.CheckedHashRates["Quark"];
                    chkQubit.Checked = rates.CheckedHashRates["Qubit"];
                    chkKeccak.Checked = rates.CheckedHashRates["Keccak"];
                    chkScrypt.Checked = rates.CheckedHashRates["Scrypt"];
                    chkScryptN.Checked = rates.CheckedHashRates["ScryptN"];
                    chkJane15.Checked = rates.CheckedHashRates["Jane15"];
                    chkJane14.Checked = rates.CheckedHashRates["Jane14"];
                    chkJane13.Checked = rates.CheckedHashRates["Jane13"];
                    chkCryptonight.Checked = rates.CheckedHashRates["CryptoNight"];

                    cbbFiat.SelectedIndex = rates.FiatOfChoice;
                    txtFiatElectricityCost.Text = rates.FiatPerKwh.ToString(CultureInfo.InvariantCulture);
                }
                catch (KeyNotFoundException) 
                {
                    AppendToLog("KeyNotFoundException in hashrates.txt, probably due to upgrade to a newer version.");
                }
                catch (Exception exception)
                {
                    AppendToLog("Error in hashrates.txt", exception);
                }
            }
            
            if (File.Exists("apisettings.txt"))
            {
                try
                {

                    ApiSettingsJson apiSettings = JsonControl.GetSerializedApiFile<ApiSettingsJson>("apisettings.txt");
                    txtCointweakApiKey.Text = apiSettings.ApiSettings["CoinTweak"];
                    txtCoinwarzApiKey.Text = apiSettings.ApiSettings["CoinWarz"];
                    nudCryptoday.Text = apiSettings.ApiSettings["CrypToday"];
                    nudPoolpicker.Text = apiSettings.ApiSettings["PoolPicker"];
                    nudAmount.Text = apiSettings.ApiSettings["Multiplier"];
                    _hashRateMultiplier = nudAmount.Value;
                    txtProxy.Text = apiSettings.ApiSettings["ProxyURL"];
                    int savedIndex; 
                    int.TryParse(apiSettings.ApiSettings["BidRecentAsk"], out savedIndex);
                    cbbBidRecentAsk.SelectedIndex = savedIndex;

                    chkBittrex.Checked = apiSettings.CheckedApis["Bittrex"];
                    chkMintpal.Checked = apiSettings.CheckedApis["Mintpal"];
                    chkCryptsy.Checked = apiSettings.CheckedApis["Cryptsy"];
                    chkPoloniex.Checked = apiSettings.CheckedApis["Poloniex"];
                    chkAllcoin.Checked = apiSettings.CheckedApis["AllCoin"];
                    chkAllcrypt.Checked = apiSettings.CheckedApis["AllCrypt"];
                    chkCoindesk.Checked = apiSettings.CheckedApis["CoinDesk"];
                    chkNiceHash.Checked = apiSettings.CheckedApis["Nicehash"];
                    chkWhattomine.Checked = apiSettings.CheckedApis["WhatToMine"];
                    chkCointweak.Checked = apiSettings.CheckedApis["CoinTweak"];
                    chkCoinwarz.Checked = apiSettings.CheckedApis["CoinWarz"];
                    chkCryptoday.Checked = apiSettings.CheckedApis["CrypToday"];
                    chkPoolpicker.Checked = apiSettings.CheckedApis["PoolPicker"];

                    chkRemoveUnlisted.Checked = apiSettings.CheckedMisc["RemoveUnlisted"];
                    chkRemoveTooGoodToBeTrue.Checked = apiSettings.CheckedMisc["RemoveTooGoodToBeTrue"];
                    chkRemoveNegative.Checked = apiSettings.CheckedMisc["RemoveNegative"];
                    chkWeight.Checked = apiSettings.CheckedMisc["WeightedCalculations"];
                    chkColor.Checked = apiSettings.CheckedMisc["ColoredTable"];
                    chkProxy.Checked = apiSettings.CheckedMisc["Proxy"];
                }
                catch (KeyNotFoundException)
                {
                    AppendToLog("KeyNotFoundException in apisettings.txt, probably due to upgrade to a newer version.");
                    SaveSettings();
                    LoadSettings();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error in apisettings.txt", exception);
                }
            }
        }

        private void SaveSettings()
        {
            HashRateJson parsed = ParseGuiHashrates(1, false);
            _hashList = new HashRateJson
            {
                ListHashRate = parsed.ListHashRate,
                ListWattage = parsed.ListWattage,
                FiatOfChoice = parsed.FiatOfChoice,
                FiatPerKwh = parsed.FiatPerKwh,
                CheckedHashRates = new Dictionary<string, bool>()
            };

            _hashList.CheckedHashRates.Add("Groestl", chkGroestl.Checked);
            _hashList.CheckedHashRates.Add("MyrGroestl", chkMyrGroestl.Checked);
            _hashList.CheckedHashRates.Add("Fugue256", chkFugue.Checked);
            _hashList.CheckedHashRates.Add("JHA", chkJha.Checked);
            _hashList.CheckedHashRates.Add("NIST5", chkNist5.Checked);
            _hashList.CheckedHashRates.Add("Hefty", chkHefty.Checked);
            _hashList.CheckedHashRates.Add("X11", chkX11.Checked);
            _hashList.CheckedHashRates.Add("X13", chkX13.Checked);
            _hashList.CheckedHashRates.Add("X15", chkX15.Checked);
            _hashList.CheckedHashRates.Add("Quark", chkQuark.Checked);
            _hashList.CheckedHashRates.Add("Qubit", chkQubit.Checked);
            _hashList.CheckedHashRates.Add("Keccak", chkKeccak.Checked);
            _hashList.CheckedHashRates.Add("Scrypt", chkScrypt.Checked);
            _hashList.CheckedHashRates.Add("ScryptN", chkScryptN.Checked);
            _hashList.CheckedHashRates.Add("Jane15", chkJane15.Checked);
            _hashList.CheckedHashRates.Add("Jane14", chkJane14.Checked);
            _hashList.CheckedHashRates.Add("Jane13", chkJane13.Checked);
            _hashList.CheckedHashRates.Add("CryptoNight", chkCryptonight.Checked);

            string jsonHashlist = JsonConvert.SerializeObject(_hashList, Formatting.Indented);
            File.WriteAllText(@"hashrates.txt", jsonHashlist);

            ApiSettingsJson apiSettings = new ApiSettingsJson
            {
                ApiSettings = new Dictionary<string, string>(),
                CheckedApis = new Dictionary<string, bool>(),
                CheckedMisc = new Dictionary<string, bool>(),
            };

            apiSettings.ApiSettings.Add("CoinTweak", txtCointweakApiKey.Text);
            apiSettings.ApiSettings.Add("CoinWarz", txtCoinwarzApiKey.Text);
            apiSettings.ApiSettings.Add("CrypToday", nudCryptoday.Text);
            apiSettings.ApiSettings.Add("PoolPicker", nudPoolpicker.Text);
            apiSettings.ApiSettings.Add("Multiplier", nudAmount.Text);
            apiSettings.ApiSettings.Add("ProxyURL", txtProxy.Text);
            apiSettings.ApiSettings.Add("BidRecentAsk", cbbBidRecentAsk.SelectedIndex.ToString(CultureInfo.InvariantCulture));

            apiSettings.CheckedApis.Add("Bittrex", chkBittrex.Checked);
            apiSettings.CheckedApis.Add("Mintpal", chkMintpal.Checked);
            apiSettings.CheckedApis.Add("Cryptsy", chkCryptsy.Checked);
            apiSettings.CheckedApis.Add("Poloniex", chkPoloniex.Checked);
            apiSettings.CheckedApis.Add("AllCoin", chkAllcoin.Checked);
            apiSettings.CheckedApis.Add("AllCrypt", chkAllcrypt.Checked);

            apiSettings.CheckedApis.Add("CoinDesk", chkCoindesk.Checked);
            apiSettings.CheckedApis.Add("Nicehash", chkNiceHash.Checked);
            apiSettings.CheckedApis.Add("WhatToMine", chkWhattomine.Checked);
            apiSettings.CheckedApis.Add("CoinTweak", chkCointweak.Checked);
            apiSettings.CheckedApis.Add("CoinWarz", chkCoinwarz.Checked);
            apiSettings.CheckedApis.Add("CrypToday", chkCryptoday.Checked);
            apiSettings.CheckedApis.Add("PoolPicker", chkPoolpicker.Checked);

            apiSettings.CheckedMisc.Add("RemoveUnlisted", chkRemoveUnlisted.Checked);
            apiSettings.CheckedMisc.Add("RemoveTooGoodToBeTrue", chkRemoveTooGoodToBeTrue.Checked);
            apiSettings.CheckedMisc.Add("RemoveNegative", chkRemoveNegative.Checked);
            apiSettings.CheckedMisc.Add("WeightedCalculations", chkWeight.Checked);
            apiSettings.CheckedMisc.Add("ColoredTable", chkColor.Checked);
            apiSettings.CheckedMisc.Add("Proxy", chkProxy.Checked);

            string jsonApiList = JsonConvert.SerializeObject(apiSettings, Formatting.Indented);
            File.WriteAllText(@"apisettings.txt", jsonApiList);

            AppendToLog("Settings saved");
            File.WriteAllText(@"log.txt", txtLog.Text);
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            // Actual process starts here ^^"
            DateTime start = DateTime.Now;

            tsStatus.Text = "Starting new profit calculation...";
            tsErrors.Text = "0 errors";
            tsErrors.ForeColor = Color.Green;
            tsProgress.Value = 0;

            tsStatus.Text = "Parsing given hashrates...";
            _hashList = ParseGuiHashrates((double) nudAmount.Value, true);

            const int i = 7;
            GetCoinList(i);
            tsProgress.Value += i;

            tsStatus.Text = "Calculating profits and sorting the list...";
            CalculatePrices();

            tsProgress.Value += i;
            UpdateDataGridView(GetCleanedCoinList());

            tsProgress.Value = 100;
            TimeSpan end = DateTime.Now.Subtract(start);
            tsStatus.Text = "Completed in " + end.TotalSeconds.ToString("0.##") + " seconds";

            File.WriteAllText(@"log.txt", txtLog.Text);
        }

        private void CalculatePrices()
        {
            if (chkCoindesk.Checked)
            {
                try
                {
                    _coinList.CalculatePrices(_hashList, chkWeight.Checked,
                        "https://api.coindesk.com/v1/bpi/currentprice.json",
                        "https://api.coindesk.com/v1/bpi/currentprice/CNY.json");
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Coindesk, used to calculate your " + cbbFiat.Text + "/day.",
                        exception);
                }
            }
            else
            {
                _coinList.CalculatePrices(_hashList, chkWeight.Checked);
            }
        }

        private List<Coin> GetCleanedCoinList()
        {
            ParallelQuery<Coin> tempCoinList = _coinList.List.AsParallel();

            if (chkRemoveUnlisted.Checked)
            {
                tsStatus.Text = "Removing coins that aren't listed on supported exchanges...";
                tempCoinList = tempCoinList.Where(coin =>
                    coin.HasImplementedMarketApi);
            }

            if (chkRemoveFrozenCoins.Checked)
            {
                tsStatus.Text = "Removing coins that are frozen on supported exchanges...";
                tempCoinList =
                    tempCoinList.Where(coin => coin.Exchanges.Count(exchange => exchange.IsFrozen) == 0);
            }

            if (chkRemoveTooGoodToBeTrue.Checked)
            {
                tsStatus.Text = "Removing coins with a volume lower than you can earn..";
                tempCoinList = tempCoinList.Where(coin =>
                    coin.TotalVolume > coin.BtcPerDay || coin.IsMultiPool);
            }

            if (chkRemoveNegative.Checked)
            {
                tsStatus.Text = "Removing results with a negative profit..";
                tempCoinList = tempCoinList.Where(coin =>
                    coin.BtcPerDay >= 0);
            }

            return new List<Coin>(tempCoinList.OrderByDescending(o => o.BtcPerDay));
        }

        private void UpdateDataGridView(List<Coin> listCoins)
        {
            tsStatus.Text = "Writing data to table...";

            dgView.Rows.Clear();
            DataGridViewRow[] arrCoinRows = new DataGridViewRow[listCoins.Count];

            Parallel.For(0, listCoins.Count, index =>
            {
                Coin coin = listCoins[index];
                
                arrCoinRows[index] = new DataGridViewRow {HeaderCell = {Value = String.Format("{0}", index + 1)}};
                arrCoinRows[index].CreateCells(dgView, coin.TagName, coin.FullName, coin.Algo,
                    coin.UsdPerDay.ToString("0.000"), coin.EurPerDay.ToString("0.000"),
                    coin.GbpPerDay.ToString("0.000"), coin.CnyPerDay.ToString("0.000"),
                    coin.BtcPerDay.ToString("0.00000000"), coin.CoinsPerDay.ToString("0.00000"),
                    coin.Exchanges[0].ExchangeName, coin.Exchanges[0].BtcPrice.ToString("0.00000000"),
                    coin.Exchanges[0].BtcVolume.ToString("0.000"), coin.WeightedBtcPrice.ToString("0.00000000"),
                    coin.TotalVolume.ToString("0.000"), coin.Difficulty, coin.BlockReward
                    );

                if (chkColor.Checked)
                {
                    arrCoinRows[index].DefaultCellStyle.BackColor = GetRowColor(coin);
                }
            });

            dgView.Rows.AddRange(arrCoinRows);
        }

        private Color GetRowColor(Coin coin)
        {
            if (!coin.HasImplementedMarketApi) 
            {
                return Color.Plum;
            }

            if (coin.Exchanges.Count(exchange => exchange.IsFrozen) != 0)
            {
                return Color.DeepSkyBlue;
            }

            if (coin.BtcPerDay <= 0)
            {
                return Color.Red;
            }

            if (coin.TotalVolume < coin.BtcPerDay && !coin.IsMultiPool)
            {
                return Color.PaleTurquoise;
            }

            if (coin.IsMultiPool)
            {
                return Color.YellowGreen;
            }

            return Color.GreenYellow;
        }

        private void GetCoinList(int progress)
        {
            List<Action> erroredActions = new List<Action>();

            HttpClientHandler hch = new HttpClientHandler();
            if (chkProxy.Checked)
            {
                hch.UseProxy = true;
                hch.Proxy = String.IsNullOrEmpty(txtProxy.Text) ? WebRequest.GetSystemWebProxy() : new WebProxy(txtProxy.Text);
            }
            else
            {
                hch.UseProxy = false;
                hch.Proxy = null;
            }

            _coinList = new CoinList(new HttpClient(hch, true));

            if (chkCryptonight.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting XMR diff, reward, ...";
                    _coinList.AddMoneroWorkAround();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from MoneroChain. Will be retried.",
                        exception);
                    erroredActions.Add(_coinList.AddMoneroWorkAround);
                }
            }

            tsProgress.Value += progress;
            if (chkNiceHash.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting actual NiceHash prices...";
                    _coinList.UpdateNiceHash();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from NiceHash. Will be retried.",
                        exception);
                    erroredActions.Add(_coinList.UpdateNiceHash);
                }
            }

            tsProgress.Value += progress;
            if (chkPoolpicker.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting multipool prices from PoolPicker...";
                    _coinList.UpdatePoolPicker(nudPoolpicker.Value, chkReviewCalc.Checked);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from PoolPicker. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdatePoolPicker(nudPoolpicker.Value, chkReviewCalc.Checked));
                }
            }

            tsProgress.Value += progress;
            if (chkCryptoday.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting multipool prices from CrypToday...";
                    _coinList.UpdateCrypToday(nudCryptoday.Value);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from CrypToday. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdateCrypToday(nudCryptoday.Value));
                }
            }

            tsProgress.Value += progress;
            if (chkWhattomine.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting coin info from WhatToMine...";
                    _coinList.UpdateWhatToMine();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from WhatToMine. Will be retried.",
                        exception);
                    erroredActions.Add(_coinList.UpdateWhatToMine);
                }
            }

            tsProgress.Value += progress;
            if (chkCointweak.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting coin info from CoinTweak...";
                    _coinList.UpdateCoinTweak(txtCointweakApiKey.Text);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from CoinTweak. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdateCoinTweak(txtCointweakApiKey.Text));
                }
            }

            tsProgress.Value += progress;
            if (chkCoinwarz.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting coin info from CoinWarz...";
                    _coinList.UpdateCoinWarz(txtCoinwarzApiKey.Text);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from CoinWarz. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdateCoinWarz(txtCoinwarzApiKey.Text));
                }
            }

            if (erroredActions.Count > 0)
            {
                tsStatus.Text = "Retrying errored coin info and multipool API's";
                int errors = 0;

                foreach (Action erroredAction in erroredActions)
                {
                    try
                    {
                        erroredAction.Invoke();
                    }
                    catch (Exception exception)
                    {
                        errors++;
                        AppendToLog("Error", exception);
                    }
                }

                if (errors != 0)
                {
                    UpdateErrorCounter(errors);
                }

                erroredActions.Clear();
            }

            tsProgress.Value += progress;
            if (chkBittrex.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Bittrex prices...";
                    _coinList.UpdateBittrex(cbbBidRecentAsk.SelectedIndex);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Bittrex. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdateBittrex(cbbBidRecentAsk.SelectedIndex));
                }
            }

            tsProgress.Value += progress;
            if (chkMintpal.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with MintPal prices...";
                    _coinList.UpdateMintPal(cbbBidRecentAsk.SelectedIndex);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Mintpal. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdateMintPal(cbbBidRecentAsk.SelectedIndex));
                }
            }

            tsProgress.Value += progress;
            if (chkCryptsy.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Cryptsy prices...";
                    _coinList.UpdateCryptsy(cbbBidRecentAsk.SelectedIndex);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Cryptsy. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdateCryptsy(cbbBidRecentAsk.SelectedIndex));
                }
            }

            tsProgress.Value += progress;
            if (chkPoloniex.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Poloniex prices...";
                    _coinList.UpdatePoloniex(cbbBidRecentAsk.SelectedIndex);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Poloniex. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdatePoloniex(cbbBidRecentAsk.SelectedIndex));
                }
            }

            tsProgress.Value += progress;
            if (chkAllcoin.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with AllCoin prices...";
                    _coinList.UpdateAllCoin(cbbBidRecentAsk.SelectedIndex);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from AllCoin. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdateAllCoin(cbbBidRecentAsk.SelectedIndex));
                }
            }

            tsProgress.Value += progress;
            if (chkAllcrypt.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with AllCrypt prices...";
                    _coinList.UpdateAllCrypt(cbbBidRecentAsk.SelectedIndex);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from AllCrypt. Will be retried.",
                        exception);
                    erroredActions.Add(() => _coinList.UpdateAllCrypt(cbbBidRecentAsk.SelectedIndex));
                }
            }

            if (erroredActions.Count > 0)
            {
                tsStatus.Text = "Retrying errored market API's";
                int errors = 0;

                foreach (Action erroredAction in erroredActions)
                {
                    try
                    {
                        erroredAction.Invoke();
                    }
                    catch (Exception exception)
                    {
                        errors++;
                        AppendToLog("Error", exception);
                    }
                }

                if (errors != 0)
                {
                    UpdateErrorCounter(errors);
                }

                erroredActions.Clear();
            }
        }
        
        private HashRateJson ParseGuiHashrates(double multiplier, bool checkChecked)
        {
            // checkChecked is false whenever all hashrates need to be saved to file
            Dictionary<HashAlgo.Algo, double> hashList = new Dictionary<HashAlgo.Algo, double>();
            Dictionary<HashAlgo.Algo, double> wattageList = new Dictionary<HashAlgo.Algo, double>();
            double dHashRate, dWattage, fiatElectricityCost;
            int fiatOfChoice = cbbFiat.SelectedIndex;

            if (!Double.TryParse(txtFiatElectricityCost.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                out fiatElectricityCost)) MessageBox.Show("Something wrong with your fiat/khw");

            if (!checkChecked || chkGroestl.Checked)
            {
                if (Double.TryParse(txtGroestl.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtGroestlWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.Groestl, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.Groestl, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Groestl hashrate");
                }
            }

            if (!checkChecked || chkMyrGroestl.Checked)
            {
                if (
                    Double.TryParse(txtMyrGroestl.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtMyrGroestlWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.MyriadGroestl, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.MyriadGroestl, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your MyrGroestl hashrate");
                }
            }

            if (!checkChecked || chkFugue.Checked)
            {
                if (Double.TryParse(txtFugue.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtFugueWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.Fugue256, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.Fugue256, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Fugue hashrate");
                }
            }

            if (!checkChecked || chkJha.Checked)
            {
                if (Double.TryParse(txtJackpot.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtJhaWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.JHA, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.JHA, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your JHA hashrate");
                }
            }

            if (!checkChecked || chkNist5.Checked)
            {
                if (Double.TryParse(txtNist5.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtNist5Wattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.Nist5, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.Nist5, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Nist5 hashrate");
                }
            }

            if (!checkChecked || chkHefty.Checked)
            {
                if (Double.TryParse(txtHefty.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtHeftyWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.Heavy, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.Heavy, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Hefty hashrate");
                }
            }

            if (!checkChecked || chkX11.Checked)
            {
                if (Double.TryParse(txtX11.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtX11Wattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.X11, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.X11, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your X11 hashrate");
                }
            }

            if (!checkChecked || chkX13.Checked)
            {
                if (Double.TryParse(txtX13.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtX13Wattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.X13, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.X13, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your X13 hashrate");
                }
            }

            if (!checkChecked || chkX15.Checked)
            {
                if (Double.TryParse(txtX15.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtX15Wattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.X15, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.X15, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your X15 hashrate");
                }
            }

            if (!checkChecked || chkQuark.Checked)
            {
                if (Double.TryParse(txtQuark.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtQuarkWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.Quark, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.Quark, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Quark hashrate");
                }
            }

            if (!checkChecked || chkQubit.Checked)
            {
                if (Double.TryParse(txtQubit.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtQubitWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.Qubit, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.Qubit, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Qubit hashrate");
                }
            }

            if (!checkChecked || chkKeccak.Checked)
            {
                if (Double.TryParse(txtKeccak.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtKeccakWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.Keccak, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.Keccak, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Keccak hashrate");
                }
            }

            if (!checkChecked || chkScrypt.Checked)
            {
                if (Double.TryParse(txtScrypt.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtScryptWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.Scrypt, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.Scrypt, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Scrypt hashrate");
                }
            }

            if (!checkChecked || chkScryptN.Checked)
            {
                if (Double.TryParse(txtScryptN.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtScryptNWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.ScryptN, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.ScryptN, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your ScryptN hashrate");
                }
            }

            if (!checkChecked || chkJane15.Checked)
            {
                if (Double.TryParse(txtJane15.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtJane15Wattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.ScryptJane15, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.ScryptJane15, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Jane15 hashrate");
                }
            }

            if (!checkChecked || chkJane14.Checked)
            {
                if (Double.TryParse(txtJane14.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtJane14Wattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.ScryptJane14, dHashRate * multiplier);
                    wattageList.Add(HashAlgo.Algo.ScryptJane14, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Jane14 hashrate");
                }
            }

            if (!checkChecked || chkJane13.Checked)
            {
                if (Double.TryParse(txtJane13.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtJane13Wattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.ScryptJane13, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.ScryptJane13, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your Jane13 hashrate");
                }
            }

            if (!checkChecked || chkCryptonight.Checked)
            {
                if (
                    Double.TryParse(txtCryptonight.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out dHashRate) &&
                    Double.TryParse(txtCryptonightWattage.Text, NumberStyles.Float, CultureInfo.InvariantCulture,
                        out dWattage))
                {
                    hashList.Add(HashAlgo.Algo.CryptoNight, dHashRate*multiplier);
                    wattageList.Add(HashAlgo.Algo.CryptoNight, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your CryptoNight hashrate");
                }
            }

            HashRateJson hashRateJson = new HashRateJson
            {
                ListHashRate = hashList,
                ListWattage = wattageList,
                FiatPerKwh = fiatElectricityCost,
                FiatOfChoice = fiatOfChoice
            };
            return hashRateJson;
        }

        private void AppendToLog(string s)
        {
            txtLog.AppendText("[" + DateTime.Now + "] " + s + Environment.NewLine);
        }

        private void AppendToLog(string s, Exception e)
        {
            txtLog.AppendText("[" + DateTime.Now + "] [ERROR] " + s + Environment.NewLine + e + Environment.NewLine);
        }

        private void UpdateErrorCounter(int iErrors = 1)
        {
            tsErrors.ForeColor = Color.Red;

            iErrors += int.Parse(tsErrors.Text.Split(' ')[0]);

            tsErrors.Text = iErrors == 1
                ? iErrors + " error"
                : iErrors + " errors";
        }

        private void tsmResultsToClipboard_Click(object sender, EventArgs e)
        {
            if (_coinList != null) Clipboard.SetText(_coinList.ToString());
        }

        private void tsmHashratesToClipboard_Click(object sender, EventArgs e)
        {
            if (_hashList != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<HashAlgo.Algo, double> algo in _hashList.ListHashRate)
                {
                    sb.Append(algo.Key + ": " + algo.Value + " MH/s" + Environment.NewLine);
                }

                Clipboard.SetText(sb.ToString());
            }
        }

        private void CudaProfitCalc_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void chkCointweak_CheckedChanged(object sender, EventArgs e)
        {
            txtCointweakApiKey.Enabled = chkCointweak.Checked;
        }

        private void chkCoinwarz_CheckedChanged(object sender, EventArgs e)
        {
            txtCoinwarzApiKey.Enabled = chkCoinwarz.Checked;
        }

        private void chkProxy_CheckedChanged(object sender, EventArgs e)
        {
            txtProxy.Enabled = chkProxy.Checked;
        }

        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            _hashRateMultiplier = nudAmount.Value;
        }

        private void chkPoolpicker_CheckedChanged(object sender, EventArgs e)
        {
            nudPoolpicker.Enabled = chkPoolpicker.Checked;
            chkReviewCalc.Enabled = chkPoolpicker.Checked;
        }

        private void chkCryptoday_CheckedChanged(object sender, EventArgs e)
        {
            nudCryptoday.Enabled = chkCryptoday.Checked;
        }

        private void reasonToUpdateDgv_CheckedChanged(object sender, EventArgs e)
        {
            if (_coinList != null && _coinList.List != null)
            {
                UpdateDataGridView(GetCleanedCoinList());
            }
        }

        private void chkCoindesk_CheckedChanged(object sender, EventArgs e)
        {
            SetVisibleFiatColumn();
            cbbFiat.Enabled = chkCoindesk.Checked;
            txtFiatElectricityCost.Enabled = chkCoindesk.Checked;
        }

        private void cbbFiat_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVisibleFiatColumn();
        }

        private void SetVisibleFiatColumn()
        {
            if (chkCoindesk.Checked)
            {
                switch (cbbFiat.SelectedIndex)
                {
                    case 0:
                        lblElectricityCost.Text = "USD/kWh";
                        dgView.Columns[3].Visible = true;
                        dgView.Columns[4].Visible = false;
                        dgView.Columns[5].Visible = false;
                        dgView.Columns[6].Visible = false;
                        break;
                    case 1:
                        lblElectricityCost.Text = "EUR/kWh";
                        dgView.Columns[3].Visible = false;
                        dgView.Columns[4].Visible = true;
                        dgView.Columns[5].Visible = false;
                        dgView.Columns[6].Visible = false;
                        break;
                    case 2:
                        lblElectricityCost.Text = "GBP/kWh";
                        dgView.Columns[3].Visible = false;
                        dgView.Columns[4].Visible = false;
                        dgView.Columns[5].Visible = true;
                        dgView.Columns[6].Visible = false;
                        break;
                    case 3:
                        lblElectricityCost.Text = "CNY/kWh";
                        dgView.Columns[3].Visible = false;
                        dgView.Columns[4].Visible = false;
                        dgView.Columns[5].Visible = false;
                        dgView.Columns[6].Visible = true;
                        break;
                    default:
                        lblElectricityCost.Text = "USD/kWh";
                        dgView.Columns[3].Visible = true;
                        dgView.Columns[4].Visible = true;
                        dgView.Columns[5].Visible = true;
                        dgView.Columns[6].Visible = true;
                        break;
                }
            }
            else
            {
                lblElectricityCost.Text = "Disabled";
                dgView.Columns[3].Visible = false;
                dgView.Columns[4].Visible = false;
                dgView.Columns[5].Visible = false;
                dgView.Columns[6].Visible = false;
            }

            int x = txtFiatElectricityCost.Location.X - 6 - lblElectricityCost.Size.Width;
            lblElectricityCost.Location = new Point(x, lblElectricityCost.Location.Y);
        }

        private void tsStatus_TextChanged(object sender, EventArgs e)
        {
            AppendToLog(tsStatus.Text);
        }

        private void checkAll_Click(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grpHashrates.Controls.OfType<CheckBox>().AsParallel()
                .Where(chkBox => chkAll != chkBox && chkCoindesk != chkBox))
            {
                chkBox.Checked = chkAll.Checked;
            }
        }

        private void UpdateChkAllState()
        {
            int totalChkBoxes = 0, checkedChkBoxes = 0;
            foreach (CheckBox chkBox in grpHashrates.Controls.OfType<CheckBox>().AsParallel()
                .Where(chkBox => chkAll != chkBox && chkCoindesk != chkBox))
            {
                totalChkBoxes++;
                if (chkBox.Checked)
                {
                    checkedChkBoxes++;
                }
            }

            if (checkedChkBoxes == 0)
            {
                chkAll.CheckState = CheckState.Unchecked;
            }
            else if (checkedChkBoxes == totalChkBoxes)
            {
                chkAll.CheckState = CheckState.Checked;
            }
            else
            {
                chkAll.CheckState = CheckState.Indeterminate;
            }
        }

        private void chkHashingalgo_Click(object sender, EventArgs e)
        {
            UpdateChkAllState();
        }

        private void chkColor_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkColor.Checked)
            {
                chkRemoveUnlisted.BackColor = Color.Plum;
                chkRemoveFrozenCoins.BackColor = Color.DeepSkyBlue;
                chkRemoveTooGoodToBeTrue.BackColor = Color.PaleTurquoise;
                chkRemoveNegative.BackColor = Color.Red;
                tabMultipool.BackColor = Color.YellowGreen;
                tabCoinInfo.BackColor = Color.GreenYellow;
                tabMarketApi.BackColor = Color.GreenYellow;
            }
            else
            {
                chkRemoveUnlisted.BackColor = Color.Transparent;
                chkRemoveFrozenCoins.BackColor = Color.Transparent;
                chkRemoveTooGoodToBeTrue.BackColor = Color.Transparent;
                chkRemoveNegative.BackColor = Color.Transparent;
                tabMultipool.BackColor = Color.Transparent;
                tabCoinInfo.BackColor = Color.Transparent;
                tabMarketApi.BackColor = Color.Transparent;
            }   
        }
    }
}