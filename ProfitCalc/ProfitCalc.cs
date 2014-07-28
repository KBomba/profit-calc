using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ProfitCalc
{
    public partial class ProfitCalc : Form
    {
        private CoinList _coinList;
        private Dictionary<string,HashRateJson> _hashList;
        private BindingList<CustomCoin> _customCoins; 

        public ProfitCalc()
        {
            InitializeComponent();
            InitializeOtherComponents();

            LoadSettings();
            UpdateChkAllState();

            InitCustomCoins();
        }

        private void InitializeOtherComponents()
        {
            foreach (TabPage page in tabControlSettings.TabPages)
            {
                page.BackColor = SystemColors.Menu;
                //They're white by default, and VS keeps on reverting my changes in the designer file
                //Screw you Visual Studio :D
            }
            txtLog.BackColor = SystemColors.Window;
            txtReadme.BackColor = SystemColors.Window;
            AppendToLog("Loading settings");

            cbbFiat.SelectedIndex = 0;
            cbbBidRecentAsk.SelectedIndex = 0;
            
            try
            {
                if (File.Exists("README.txt"))
                {
                    using (TextReader tr = File.OpenText("README.txt"))
                    {
                        txtReadme.Text = tr.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                AppendToLog("Unable to read README.txt", e);
            }
        }

        private void InitCustomCoins()
        {
            dgvCustomCoins.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn tagColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Tag",
                HeaderText = "Tag"
            };

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Full Name"
            };

            DataGridViewComboBoxColumn algoColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Algo",
                HeaderText = "Algo",
                DataSource = Enum.GetValues(typeof(HashAlgo.Algo))
            };

            DataGridViewTextBoxColumn diffColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Difficulty",
                HeaderText = "Difficulty"
            };

            DataGridViewTextBoxColumn rewardColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BlockReward",
                HeaderText = "Block Reward"
            };

            dgvCustomCoins.Columns.Add(tagColumn);
            dgvCustomCoins.Columns.Add(nameColumn);
            dgvCustomCoins.Columns.Add(algoColumn);
            dgvCustomCoins.Columns.Add(diffColumn);
            dgvCustomCoins.Columns.Add(rewardColumn);

            CustomCoin test = new CustomCoin
            {
                Tag = "MYR",
                FullName = "Myriad",
                Algo = HashAlgo.Algo.MyriadGroestl,
                Difficulty = 666,
                BlockReward = 1000
            };
            _customCoins = new BindingList<CustomCoin> { test };
            dgvCustomCoins.DataSource = _customCoins;
        }

        private void LoadSettings()
        {
            if (File.Exists("profiles.txt"))
            {
                _hashList = JsonControl.GetSerializedApiFile<Dictionary<string, HashRateJson>>("profiles.txt");
                foreach (KeyValuePair<string, HashRateJson> hashRateJson in _hashList)
                {
                    cbbProfiles.Items.Add(hashRateJson.Key);
                }
                cbbProfiles.SelectedIndex = 0;
                UpdateGuiHashrates(_hashList.First().Value);
            } 
            else if (File.Exists("hashrates.txt"))
            {
                HashRateJson rates = JsonControl.GetSerializedApiFile<HashRateJson>("hashrates.txt");
                _hashList = new Dictionary<string, HashRateJson> {{"hashrates.txt", rates}};
                cbbProfiles.Items.Add("hashrates.txt");
                cbbProfiles.SelectedIndex = 0;
                UpdateGuiHashrates(rates);
            }
            else
            {
                _hashList = new Dictionary<string, HashRateJson> { { "Default", ParseGuiHashrates(false)} };
                cbbProfiles.Items.Add("Default");
                cbbProfiles.SelectedIndex = 0;
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
                    chkRemoveFrozenCoins.Checked = apiSettings.CheckedMisc["RemoveFrozen"];
                    chkRemoveTooGoodToBeTrue.Checked = apiSettings.CheckedMisc["RemoveTooGoodToBeTrue"];
                    chkRemoveNegative.Checked = apiSettings.CheckedMisc["RemoveNegative"];
                    chkWeight.Checked = apiSettings.CheckedMisc["WeightedCalculations"];
                    chkColor.Checked = apiSettings.CheckedMisc["ColoredTable"];
                    chkProxy.Checked = apiSettings.CheckedMisc["Proxy"];
                }
                catch (KeyNotFoundException)
                {
                    AppendToLog("KeyNotFoundException in apisettings.txt, probably due to upgrade to a newer version.");
                }
                catch (Exception exception)
                {
                    AppendToLog("Error in apisettings.txt", exception);
                }
            }
        }

        private void SaveSettings()
        {
            if (_hashList != null)
            {
                if (_hashList.ContainsKey(cbbProfiles.Text))
                {
                    _hashList[cbbProfiles.Text] = ParseGuiHashrates(false);
                }
            }
            else
            {
                _hashList = new Dictionary<string, HashRateJson> {{"Default", ParseGuiHashrates(false)}};
            }
            
            string jsonHashlist = JsonConvert.SerializeObject(_hashList, Formatting.Indented);
            File.WriteAllText(@"profiles.txt", jsonHashlist);

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
            apiSettings.CheckedMisc.Add("RemoveFrozen", chkRemoveUnlisted.Checked);
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
            _hashList[cbbProfiles.Text] = ParseGuiHashrates(true);

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
            try
            {
                _coinList.CalculatePrices(_hashList[cbbProfiles.Text], chkWeight.Checked, chkCoindesk.Checked);
            }
            catch (Exception exception)
            {
                AppendToLog("Error while getting data from Coindesk, used to calculate your " + cbbFiat.Text + "/day.",
                    exception);
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
                    coin.TotalVolume > coin.BtcPerDay || coin.IsMultiPool || Double.IsNaN(coin.BtcPerDay));
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
                return coin.IsMultiPool ? Color.OrangeRed : Color.Red;
            }

            if (coin.IsMultiPool)
            {
                return Color.YellowGreen;
            }

            if (coin.TotalVolume < coin.BtcPerDay)
            {
                return Color.PaleTurquoise;
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

        private void UpdateGuiHashrates(HashRateJson rates)
        {
            try
            {
                txtGroestl.Text = rates.ListHashRate[HashAlgo.Algo.Groestl].ToString(CultureInfo.InvariantCulture);
                txtGroestlWattage.Text = rates.ListWattage[HashAlgo.Algo.Groestl].ToString(CultureInfo.InvariantCulture);
                chkGroestl.Checked = rates.CheckedHashRates["Groestl"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Groestl settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Groestl settings.",
                    exception);
            }

            try
            {
                txtMyrGroestl.Text = rates.ListHashRate[HashAlgo.Algo.MyriadGroestl].ToString(CultureInfo.InvariantCulture);
                txtMyrGroestlWattage.Text = rates.ListWattage[HashAlgo.Algo.MyriadGroestl].ToString(CultureInfo.InvariantCulture);
                chkMyrGroestl.Checked = rates.CheckedHashRates["MyrGroestl"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Myriad-Groestl settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Myriad-Groestl settings.",
                    exception);
            }

            try
            {
                txtFugue.Text = rates.ListHashRate[HashAlgo.Algo.Fugue256].ToString(CultureInfo.InvariantCulture);
                txtFugueWattage.Text = rates.ListWattage[HashAlgo.Algo.Fugue256].ToString(CultureInfo.InvariantCulture);
                chkFugue.Checked = rates.CheckedHashRates["Fugue256"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Fugue256 settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Fugue256 settings.",
                    exception);
            }

            try
            {
                txtKeccak.Text = rates.ListHashRate[HashAlgo.Algo.Keccak].ToString(CultureInfo.InvariantCulture);
                txtKeccakWattage.Text = rates.ListWattage[HashAlgo.Algo.Keccak].ToString(CultureInfo.InvariantCulture);
                chkKeccak.Checked = rates.CheckedHashRates["Keccak"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Keccak settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Keccak settings.",
                    exception);
            }

            try
            {
                txtJackpot.Text = rates.ListHashRate[HashAlgo.Algo.JHA].ToString(CultureInfo.InvariantCulture);
                txtJhaWattage.Text = rates.ListWattage[HashAlgo.Algo.JHA].ToString(CultureInfo.InvariantCulture);
                chkJha.Checked = rates.CheckedHashRates["JHA"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved JackpotHash settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved JackpotHash settings.",
                    exception);
            }

            try
            {
                txtNist5.Text = rates.ListHashRate[HashAlgo.Algo.Nist5].ToString(CultureInfo.InvariantCulture);
                txtNist5Wattage.Text = rates.ListWattage[HashAlgo.Algo.Nist5].ToString(CultureInfo.InvariantCulture);
                chkNist5.Checked = rates.CheckedHashRates["NIST5"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved NIST5 settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved NIST5 settings.",
                    exception);
            }

            try
            {
                txtQuark.Text = rates.ListHashRate[HashAlgo.Algo.Quark].ToString(CultureInfo.InvariantCulture);
                txtQuarkWattage.Text = rates.ListWattage[HashAlgo.Algo.Quark].ToString(CultureInfo.InvariantCulture);
                chkQuark.Checked = rates.CheckedHashRates["Quark"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Quark settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Quark settings.",
                    exception);
            }

            try
            {
                txtQubit.Text = rates.ListHashRate[HashAlgo.Algo.Qubit].ToString(CultureInfo.InvariantCulture);
                txtQubitWattage.Text = rates.ListWattage[HashAlgo.Algo.Qubit].ToString(CultureInfo.InvariantCulture);
                chkQubit.Checked = rates.CheckedHashRates["Qubit"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Qubit settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Qubit settings.",
                    exception);
            }

            try
            {
                txtScrypt.Text = rates.ListHashRate[HashAlgo.Algo.Scrypt].ToString(CultureInfo.InvariantCulture);
                txtScryptWattage.Text = rates.ListWattage[HashAlgo.Algo.Scrypt].ToString(CultureInfo.InvariantCulture);
                chkScrypt.Checked = rates.CheckedHashRates["Scrypt"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Scrypt settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Scrypt settings.",
                    exception);
            }

            try
            {
                txtScryptN.Text = rates.ListHashRate[HashAlgo.Algo.ScryptN].ToString(CultureInfo.InvariantCulture);
                txtScryptNWattage.Text = rates.ListWattage[HashAlgo.Algo.ScryptN].ToString(CultureInfo.InvariantCulture);
                chkScryptN.Checked = rates.CheckedHashRates["ScryptN"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved ScryptN settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved ScryptN settings.",
                    exception);
            }

            try
            {
                txtHefty.Text = rates.ListHashRate[HashAlgo.Algo.Heavy].ToString(CultureInfo.InvariantCulture);
                txtHeftyWattage.Text = rates.ListWattage[HashAlgo.Algo.Heavy].ToString(CultureInfo.InvariantCulture);
                chkHefty.Checked = rates.CheckedHashRates["Hefty"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Hefty settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Hefty settings.",
                    exception);
            }

            try
            {
                txtX11.Text = rates.ListHashRate[HashAlgo.Algo.X11].ToString(CultureInfo.InvariantCulture);
                txtX11Wattage.Text = rates.ListWattage[HashAlgo.Algo.X11].ToString(CultureInfo.InvariantCulture);
                chkX11.Checked = rates.CheckedHashRates["X11"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved X11 settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved X11 settings.",
                    exception);
            }

            try
            {
                txtX13.Text = rates.ListHashRate[HashAlgo.Algo.X13].ToString(CultureInfo.InvariantCulture);
                txtX13Wattage.Text = rates.ListWattage[HashAlgo.Algo.X13].ToString(CultureInfo.InvariantCulture);
                chkX13.Checked = rates.CheckedHashRates["X13"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved X13 settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved X13 settings.",
                    exception);
            }

            try
            {
                txtX15.Text = rates.ListHashRate[HashAlgo.Algo.X15].ToString(CultureInfo.InvariantCulture);
                txtX15Wattage.Text = rates.ListWattage[HashAlgo.Algo.X15].ToString(CultureInfo.InvariantCulture);
                chkX15.Checked = rates.CheckedHashRates["X15"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved X15 settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved X15 settings.",
                    exception);
            }

            try
            {
                txtJane15.Text = rates.ListHashRate[HashAlgo.Algo.ScryptJane15].ToString(CultureInfo.InvariantCulture);
                txtJane15Wattage.Text = rates.ListWattage[HashAlgo.Algo.ScryptJane15].ToString(CultureInfo.InvariantCulture);
                chkJane15.Checked = rates.CheckedHashRates["Jane15"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved ScryptJane15 settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved ScryptJane15 settings.",
                    exception);
            }

            try
            {
                txtJane14.Text = rates.ListHashRate[HashAlgo.Algo.ScryptJane14].ToString(CultureInfo.InvariantCulture);
                txtJane14Wattage.Text = rates.ListWattage[HashAlgo.Algo.ScryptJane14].ToString(CultureInfo.InvariantCulture);
                chkJane14.Checked = rates.CheckedHashRates["Jane14"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved ScryptJane14 settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved ScryptJane14 settings.",
                    exception);
            }

            try
            {
                txtJane13.Text = rates.ListHashRate[HashAlgo.Algo.ScryptJane13].ToString(CultureInfo.InvariantCulture);
                txtJane13Wattage.Text = rates.ListWattage[HashAlgo.Algo.ScryptJane13].ToString(CultureInfo.InvariantCulture);
                chkJane13.Checked = rates.CheckedHashRates["Jane13"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Jane13 settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved Jane13 settings.",
                    exception);
            }

            try
            {
                txtCryptonight.Text = rates.ListHashRate[HashAlgo.Algo.CryptoNight].ToString(CultureInfo.InvariantCulture);
                txtCryptonightWattage.Text = rates.ListWattage[HashAlgo.Algo.CryptoNight].ToString(CultureInfo.InvariantCulture);
                chkCryptonight.Checked = rates.CheckedHashRates["CryptoNight"];
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved CryptoNight settings. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved CryptoNight settings.",
                    exception);
            }

            try
            {
                cbbFiat.SelectedIndex = rates.FiatOfChoice;
                txtFiatElectricityCost.Text = rates.FiatPerKwh.ToString(CultureInfo.InvariantCulture);
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved electricity cost and fiat of choice. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved electricity cost and fiat of choice.",
                    exception);
            }

            try
            {
                nudAmount.Value = rates.Multiplier;
            }
            catch (KeyNotFoundException knfException)
            {
                AppendToLog(
                    "Something went wrong with loading your saved multiplier. Ignore this after an update.",
                    knfException);
            }
            catch (Exception exception)
            {
                AppendToLog(
                    "Something went wrong with loading your saved multiplier.",
                    exception);
            }
        }
        
        private HashRateJson ParseGuiHashrates(bool checkChecked)
        {
            // checkChecked is false whenever all hashrates need to be saved to file
            Dictionary<HashAlgo.Algo, double> hashList = new Dictionary<HashAlgo.Algo, double>();
            Dictionary<HashAlgo.Algo, double> wattageList = new Dictionary<HashAlgo.Algo, double>();
            Dictionary<string, bool> checkedHashRates = new Dictionary<string, bool>();
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
                    hashList.Add(HashAlgo.Algo.Groestl, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.MyriadGroestl, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.Fugue256, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.JHA, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.Nist5, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.Heavy, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.X11, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.X13, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.X15, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.Quark, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.Qubit, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.Keccak, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.Scrypt, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.ScryptN, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.ScryptJane15, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.ScryptJane14, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.ScryptJane13, dHashRate);
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
                    hashList.Add(HashAlgo.Algo.CryptoNight, dHashRate);
                    wattageList.Add(HashAlgo.Algo.CryptoNight, dWattage);
                }
                else
                {
                    UpdateErrorCounter();
                    AppendToLog("[ERROR] Something wrong with your CryptoNight hashrate");
                }
            }

            checkedHashRates.Add("Groestl", chkGroestl.Checked);
            checkedHashRates.Add("MyrGroestl", chkMyrGroestl.Checked);
            checkedHashRates.Add("Fugue256", chkFugue.Checked);
            checkedHashRates.Add("JHA", chkJha.Checked);
            checkedHashRates.Add("NIST5", chkNist5.Checked);
            checkedHashRates.Add("Hefty", chkHefty.Checked);
            checkedHashRates.Add("X11", chkX11.Checked);
            checkedHashRates.Add("X13", chkX13.Checked);
            checkedHashRates.Add("X15", chkX15.Checked);
            checkedHashRates.Add("Quark", chkQuark.Checked);
            checkedHashRates.Add("Qubit", chkQubit.Checked);
            checkedHashRates.Add("Keccak", chkKeccak.Checked);
            checkedHashRates.Add("Scrypt", chkScrypt.Checked);
            checkedHashRates.Add("ScryptN", chkScryptN.Checked);
            checkedHashRates.Add("Jane15", chkJane15.Checked);
            checkedHashRates.Add("Jane14", chkJane14.Checked);
            checkedHashRates.Add("Jane13", chkJane13.Checked);
            checkedHashRates.Add("CryptoNight", chkCryptonight.Checked);


            HashRateJson hashRateJson = new HashRateJson
            {
                ListHashRate = hashList,
                ListWattage = wattageList,
                FiatPerKwh = fiatElectricityCost,
                FiatOfChoice = fiatOfChoice,
                CheckedHashRates = checkedHashRates,
                Multiplier = (int) nudAmount.Value
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
            if (tsErrors.Text.Any())
            {
                tsErrors.ForeColor = Color.Red;

                iErrors += int.Parse(tsErrors.Text.Split(' ')[0]);

                tsErrors.Text = iErrors == 1
                    ? iErrors + " error"
                    : iErrors + " errors";
            }
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
                foreach (KeyValuePair<HashAlgo.Algo, double> algo in _hashList[cbbProfiles.Text].ListHashRate)
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

        private void btnAddDeleteProfile_Click(object sender, EventArgs e)
        {
            if (btnAddDeleteProfile.Text.Contains("Remove"))
            {
                _hashList.Remove(cbbProfiles.Text);
                foreach (var item in cbbProfiles.Items)
                {
                    if (item.ToString() == cbbProfiles.Text)
                    {
                        cbbProfiles.Items.Remove(item);
                        break;
                    }
                }
                cbbProfiles.SelectedItem = cbbProfiles.Items[0];
            }
            else if (!_hashList.ContainsKey(cbbProfiles.Text))
            {
                _hashList.Add(cbbProfiles.Text, ParseGuiHashrates(true));
                cbbProfiles.Items.Add(cbbProfiles.Text);

                btnAddDeleteProfile.Text = "Remove profile";
                btnAddDeleteProfile.Enabled = cbbProfiles.Items.Count != 1;
            }
        }

        private void cbbProfiles_TextChanged(object sender, EventArgs e)
        {
            if (cbbProfiles.Items.Cast<string>().All(item => item != cbbProfiles.Text))
            {
                btnAddDeleteProfile.Text = "Add profile";
                btnAddDeleteProfile.Enabled = true;
            }
            else
            {
                btnAddDeleteProfile.Text = "Remove profile";
                btnAddDeleteProfile.Enabled = cbbProfiles.Items.Count != 1;
            }
        }

        private void cbbProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            UpdateGuiHashrates(_hashList[cbbProfiles.Text]);
        }

        private void txtHashrateOrWattage_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                double result;
                if (double.TryParse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                {
                    _hashList[cbbProfiles.Text] = ParseGuiHashrates(false);
                }
                else
                {
                    MessageBox.Show(textBox.Text + " isn't a valid option");
                    textBox.Text = "";
                }
            }
        }

        private void txtHashrateOrWattage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }
        }
    }
}