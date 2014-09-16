using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;
using ProfitCalc.ApiControl;

namespace ProfitCalc
{
    public partial class ProfitCalc : Form
    {
        private Dictionary<string,List<Coin>> _coinLists;
        private CoinList _sourceList; 
        private Dictionary<string,Profile> _profileList;
        private BindingList<CustomCoin> _customCoins;
        private AutoCompleteStringCollection _historicAlgoList;


        public ProfitCalc()
        {
            InitializeComponent();
            InitializeOtherComponents();

            LoadSettings();
            UpdateFilterColors();

            InitCustomAlgos();
            InitCustomCoins();
            InitJsonRpcSettings();

            foreach (KeyValuePair<string, Profile> profile in _profileList)
            {
                UpdateHistoricAlgo(profile.Value.CustomAlgoList);
            }
        }

        private void InitializeOtherComponents()
        {
            MakeTabPagesMenuGrey(tbcControlSettings);
            MakeTabPagesMenuGrey(tbcInnerCoinMultipool);

            txtLog.BackColor = SystemColors.Window;
            txtReadme.BackColor = SystemColors.Window;
            AppendToLog("Loading settings");

            cbbFiat.SelectedIndex = 0;
           
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

        private void MakeTabPagesMenuGrey(TabControl tbc)
        {
            foreach (TabPage page in tbc.TabPages)
            {
                page.BackColor = SystemColors.Menu;
            }
        }

        private void InitCustomAlgos()
        {
            dgvCustomAlgos.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Use",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            };
            checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn synonymsColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SynonymsCsv",
                HeaderText = "Synonyms",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            DataGridViewComboBoxColumn styleColumn = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Style",
                HeaderText = "Calc style",
            };
            styleColumn.Items.Add("Classic");
            styleColumn.Items.Add("NetHashRate");
            styleColumn.Items.Add("CryptoNight");

            DataGridViewTextBoxColumn hashrateColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HashRate",
                HeaderText = "Hashrate (MH/s)",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                DefaultCellStyle = { Format = "#0.#########" }
            };


            DataGridViewTextBoxColumn wattageColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Wattage",
                HeaderText = "Wattage (W)",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
            };

            DataGridViewTextBoxColumn targetColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Target",
                HeaderText = "Target",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
            };

            dgvCustomAlgos.Columns.Add(checkColumn);
            dgvCustomAlgos.Columns.Add(nameColumn);
            dgvCustomAlgos.Columns.Add(synonymsColumn);
            dgvCustomAlgos.Columns.Add(styleColumn);
            dgvCustomAlgos.Columns.Add(hashrateColumn);
            dgvCustomAlgos.Columns.Add(wattageColumn);
            dgvCustomAlgos.Columns.Add(targetColumn);

            dgvCustomAlgos.DataSource = _profileList.First().Value.CustomAlgoList;
        }

        private void InitCustomCoins()
        {
            _customCoins =
                File.Exists("customcoins.txt")
                    ? JsonControl.GetSerializedApiFile<BindingList<CustomCoin>>("customcoins.txt")
                    : new BindingList<CustomCoin>();

            dgvCustomCoins.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Use",
                HeaderText = "Include",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            };
            checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn tagColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Tag",
                HeaderText = "Tag",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Full Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };


            DataGridViewTextBoxColumn algoColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Algo",
                HeaderText = "Algo",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn heightColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Height",
                HeaderText = "Height",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn diffColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Difficulty",
                HeaderText = "Difficulty",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                DefaultCellStyle = { Format = "#0.####" }
            };

            DataGridViewTextBoxColumn rewardColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BlockReward",
                HeaderText = "Block Reward",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            };

            DataGridViewTextBoxColumn nethashColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NetHashRate",
                HeaderText = "Net Hashrate (MH/s)",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                DefaultCellStyle = { Format = "#0.####" }
            };

            DataGridViewTextBoxColumn timeColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BlockTime",
                HeaderText = "Block Time (s)",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            };

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomPrice",
                HeaderText = "Price",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = {Format = "N8"}
            };

            dgvCustomCoins.Columns.Add(checkColumn);
            dgvCustomCoins.Columns.Add(tagColumn);
            dgvCustomCoins.Columns.Add(nameColumn);
            dgvCustomCoins.Columns.Add(algoColumn);
            dgvCustomCoins.Columns.Add(heightColumn);
            dgvCustomCoins.Columns.Add(diffColumn);
            dgvCustomCoins.Columns.Add(rewardColumn);
            dgvCustomCoins.Columns.Add(timeColumn);
            dgvCustomCoins.Columns.Add(nethashColumn);
            dgvCustomCoins.Columns.Add(priceColumn);

            dgvCustomCoins.DataSource = _customCoins;
        }

        private void InitJsonRpcSettings()
        {
            dgvJsonRpc.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "Use",
                HeaderText = "Include",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            };
            checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn tagColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Tag",
                HeaderText = "Tag",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Full Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            DataGridViewCheckBoxColumn useRpcColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "UseRpc",
                HeaderText = "Use RPC",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            };
            checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewCheckBoxColumn lockDiffColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "GetDiff",
                HeaderText = "Get Difficulty",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            };
            checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewCheckBoxColumn lockRewardColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "GetReward",
                HeaderText = "Get Block Reward",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            };
            checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewCheckBoxColumn lockNetHashColumn = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "GetNetHash",
                HeaderText = "Get NetHashRate",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            };
            checkColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn ipColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RpcIp",
                HeaderText = "IP Address",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn portColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RpcPort",
                HeaderText = "RPC Port",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn userColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RpcUser",
                HeaderText = "Username",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            DataGridViewTextBoxColumn passColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RpcPass",
                HeaderText = "Password",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            dgvJsonRpc.Columns.Add(checkColumn);
            dgvJsonRpc.Columns.Add(tagColumn);
            dgvJsonRpc.Columns.Add(nameColumn);
            dgvJsonRpc.Columns.Add(useRpcColumn);
            dgvJsonRpc.Columns.Add(lockDiffColumn);
            dgvJsonRpc.Columns.Add(lockRewardColumn);
            dgvJsonRpc.Columns.Add(lockNetHashColumn);
            dgvJsonRpc.Columns.Add(ipColumn);
            dgvJsonRpc.Columns.Add(portColumn);
            dgvJsonRpc.Columns.Add(userColumn);
            dgvJsonRpc.Columns.Add(passColumn);

            dgvJsonRpc.DataSource = _customCoins;
        }

        private void LoadSettings()
        {
            if (File.Exists("algos.txt"))
            {
                _profileList = JsonControl.GetSerializedApiFile<Dictionary<string, Profile>>("algos.txt");
                foreach (KeyValuePair<string, Profile> hashRateJson in _profileList)
                {
                    cbbProfiles.Items.Add(hashRateJson.Key);
                }
                
            } 
            else if (File.Exists("profiles.txt"))
            {
                _profileList = new Dictionary<string, Profile>();
                Dictionary<string, OldHashrates> oldProfileList = 
                    JsonControl.GetSerializedApiFile<Dictionary<string, OldHashrates>>("profiles.txt");
                foreach (KeyValuePair<string, OldHashrates> oldHashrates in oldProfileList)
                {
                    _profileList.Add(oldHashrates.Key, GetProfileFromOldHashrates(oldHashrates.Value));
                    cbbProfiles.Items.Add(oldHashrates.Key);
                }
            }
            else if (File.Exists("hashrates.txt"))
            {
                _profileList = new Dictionary<string, Profile>
                {
                    {"hashrates.txt",GetProfileFromOldHashrates(JsonControl.GetSerializedApiFile<OldHashrates>("hashrates.txt"))}
                };
                cbbProfiles.Items.Add("hashrates.txt");
            }
            else
            {
                _profileList = GetDefaultProfileList();
                cbbProfiles.Items.Add("Default");
            }
            cbbProfiles.SelectedIndex = 0;
            nudAmount.Value = _profileList[cbbProfiles.Text].Multiplier;
            cbbFiat.SelectedIndex = _profileList[cbbProfiles.Text].FiatOfChoice;
            txtFiatElectricityCost.Text = _profileList[cbbProfiles.Text].FiatPerKwh.ToString(CultureInfo.InvariantCulture);

            foreach (KeyValuePair<string, Profile> profile in _profileList)
            {
                AddToResultsTabControl(profile.Key);
            }
            
            
            if (File.Exists("apisettings.txt"))
            {
                try
                {
                    ApiSettingsJson apiSettings = JsonControl.GetSerializedApiFile<ApiSettingsJson>("apisettings.txt");

                    List<Action> settingsToLoad = new List<Action>
                    {
                        () => _historicAlgoList = apiSettings.AllAlgoList,
                        () => txtCointweakApiKey.Text = apiSettings.ApiSettings["CoinTweak"],
                        () => txtCoinwarzApiKey.Text = apiSettings.ApiSettings["CoinWarz"],
                        () => txtCcexApiKey.Text = apiSettings.ApiSettings["C-Cex"],
                        () => nudCryptoday.Text = apiSettings.ApiSettings["CrypToday"],
                        () => nudPoolpicker.Text = apiSettings.ApiSettings["PoolPicker"],
                        () => nudTimeout.Text = apiSettings.ApiSettings["Timeout"],
                        () => txtProxy.Text = apiSettings.ApiSettings["ProxyURL"],
                        () => chkBittrex.Checked = apiSettings.CheckedApis["Bittrex"],
                        () => chkMintpal.Checked = apiSettings.CheckedApis["Mintpal"],
                        () => chkCryptsy.Checked = apiSettings.CheckedApis["Cryptsy"],
                        () => chkPoloniex.Checked = apiSettings.CheckedApis["Poloniex"],
                        () => chkBTer.Checked = apiSettings.CheckedApis["BTer"],
                        () => chkAllcoin.Checked = apiSettings.CheckedApis["AllCoin"],
                        () => chkAllcrypt.Checked = apiSettings.CheckedApis["AllCrypt"],
                        () => chkCCex.Checked = apiSettings.CheckedApis["C-Cex"],
                        () => chkComkort.Checked = apiSettings.CheckedApis["Comkort"],
                        () => chkAtomictrade.Checked = apiSettings.CheckedApis["AtomicTrade"],
                        () => chkCryptoine.Checked = apiSettings.CheckedApis["Cryptoine"],
                        () => chkCoindesk.Checked = apiSettings.CheckedApis["CoinDesk"],
                        () => chkNiceHash.Checked = apiSettings.CheckedApis["Nicehash"],
                        () => chkWhattomine.Checked = apiSettings.CheckedApis["WhatToMine"],
                        () => chkCointweak.Checked = apiSettings.CheckedApis["CoinTweak"],
                        () => chkCoinwarz.Checked = apiSettings.CheckedApis["CoinWarz"],
                        () => chkCryptoday.Checked = apiSettings.CheckedApis["CrypToday"],
                        () => chkPoolpicker.Checked = apiSettings.CheckedApis["PoolPicker"],
                        () => radHighestBid.Checked = apiSettings.CheckedMisc["HighestBid"],
                        () => radMostRecentTrade.Checked = apiSettings.CheckedMisc["MostRecentTrade"],
                        () => radLowestAsk.Checked = apiSettings.CheckedMisc["LowestAsk"],
                        () => radWeighted.Checked = apiSettings.CheckedMisc["WeightedCalculations"],
                        () => radFallThroughExchange.Checked = apiSettings.CheckedMisc["UseBestFallThrough"],
                        () => radVolumeExchange.Checked = apiSettings.CheckedMisc["UseMostVolume"],
                        () => chkRemoveUnlisted.Checked = apiSettings.CheckedMisc["RemoveUnlisted"],
                        () => chkRemoveFrozenCoins.Checked = apiSettings.CheckedMisc["RemoveFrozen"],
                        () => chkRemoveTooGoodToBeTrue.Checked = apiSettings.CheckedMisc["RemoveTooGoodToBeTrue"],
                        () => chkRemoveZeroVolume.Checked = apiSettings.CheckedMisc["RemoveZeroVolume"],
                        () => chkRemoveNegative.Checked = apiSettings.CheckedMisc["RemoveNegative"],
                        () => chk24hDiff.Checked = apiSettings.CheckedMisc["Use24hDiff"],
                        () => chkColor.Checked = apiSettings.CheckedMisc["ColoredTable"],
                        () => chkProxy.Checked = apiSettings.CheckedMisc["Proxy"],
                        () => chkProxy.Checked = apiSettings.CheckedMisc["GetOrderDepths"]
                    };

                    foreach (Action action in settingsToLoad)
                    {
                        try
                        {
                            action.Invoke();
                        }
                        catch (KeyNotFoundException knfException)
                        {
                            AppendToLog("KeyNotFoundException in apisettings.txt, " +
                                        "probably due to upgrade to a newer version.", knfException);
                        }
                    }
                }
                catch (Exception exception)
                {
                    AppendToLog("Error in apisettings.txt", exception);
                }
            }
        }

        private void AddToResultsTabControl(string name)
        {
            ResultsDatagrid results = new ResultsDatagrid()
            {
                Dock = DockStyle.Fill
            };

            TabPage tabProfile = new TabPage
            {
                UseVisualStyleBackColor = false,
                Text = name,
            };

            tabProfile.Controls.Add(results);
            tbcResults.TabPages.Add(tabProfile);
        }

        private void UpdateHistoricAlgo(IEnumerable<CustomAlgo> customAlgoList)
        {
            if (_historicAlgoList == null)
            {
                _historicAlgoList = new AutoCompleteStringCollection();
            }

            foreach (CustomAlgo customAlgo in customAlgoList)
            {
                if (!_historicAlgoList.Contains(customAlgo.Name))
                {
                    _historicAlgoList.Add(customAlgo.Name);
                }
            }
        }

        private Profile GetProfileFromOldHashrates(OldHashrates hashratesTxt)
        {
            Profile profile = new Profile();
            foreach (KeyValuePair<string, double> hashrate in hashratesTxt.HashRateList)
            {
                CustomAlgo algo = new CustomAlgo
                {
                    Name = hashrate.Key.ToUpperInvariant(),
                    HashRate = hashrate.Value,
                    Wattage = hashratesTxt.WattageList[hashrate.Key],
                };

                // I was a fool for naming the keys differently :D
                switch (hashrate.Key)
                {
                    case "MyriadGroestl":
                        algo.Use = hashratesTxt.CheckedHashRates["MyrGroestl"];
                        break;
                    case "Nist5":
                        algo.Use = hashratesTxt.CheckedHashRates["NIST5"];
                        break;
                    case "Heavy":
                        algo.Use = hashratesTxt.CheckedHashRates["Hefty"];
                        break;
                    case "ScryptJane15":
                        algo.Use = hashratesTxt.CheckedHashRates["Jane15"];
                        break;
                    case "ScryptJane14":
                        algo.Use = hashratesTxt.CheckedHashRates["Jane14"];
                        break;
                    case "ScryptJane13":
                        algo.Use = hashratesTxt.CheckedHashRates["Jane13"];
                        break;
                    default:
                        algo.Use = hashratesTxt.CheckedHashRates[hashrate.Key];
                        break;
                }
                
                // Grabbing these new items from the default list
                foreach (CustomAlgo defaultAlgo in GetDefaultProfileList()["Default"].CustomAlgoList)
                {
                    if (defaultAlgo.Name == algo.Name)
                    {
                        algo.SynonymsCsv = defaultAlgo.SynonymsCsv;
                        algo.Style = defaultAlgo.Style;
                        algo.Target = defaultAlgo.Target;
                    }
                }

                profile.CustomAlgoList.Add(algo);
            }

            return profile;
        }

        private void SaveSettings()
        {
            try
            {
                if (_profileList == null)
                {
                    _profileList = GetDefaultProfileList();
                }

                File.WriteAllText(@"algos.txt",
                    JsonConvert.SerializeObject(_profileList, Formatting.Indented));

                ApiSettingsJson apiSettings = new ApiSettingsJson
                {
                    ApiSettings = new Dictionary<string, string>(),
                    CheckedApis = new Dictionary<string, bool>(),
                    CheckedMisc = new Dictionary<string, bool>(),
                    AllAlgoList = _historicAlgoList
                };

                apiSettings.ApiSettings.Add("CoinTweak", txtCointweakApiKey.Text);
                apiSettings.ApiSettings.Add("CoinWarz", txtCoinwarzApiKey.Text);
                apiSettings.ApiSettings.Add("C-Cex", txtCcexApiKey.Text);
                apiSettings.ApiSettings.Add("CrypToday", nudCryptoday.Text);
                apiSettings.ApiSettings.Add("PoolPicker", nudPoolpicker.Text);
                apiSettings.ApiSettings.Add("Timeout", nudTimeout.Text);
                apiSettings.ApiSettings.Add("ProxyURL", txtProxy.Text);

                apiSettings.CheckedApis.Add("Bittrex", chkBittrex.Checked);
                apiSettings.CheckedApis.Add("Mintpal", chkMintpal.Checked);
                apiSettings.CheckedApis.Add("Cryptsy", chkCryptsy.Checked);
                apiSettings.CheckedApis.Add("Poloniex", chkPoloniex.Checked);
                apiSettings.CheckedApis.Add("BTer", chkBTer.Checked);
                apiSettings.CheckedApis.Add("AllCoin", chkAllcoin.Checked);
                apiSettings.CheckedApis.Add("AllCrypt", chkAllcrypt.Checked);
                apiSettings.CheckedApis.Add("C-Cex", chkCCex.Checked);
                apiSettings.CheckedApis.Add("Comkort", chkComkort.Checked);
                apiSettings.CheckedApis.Add("AtomicTrade", chkAtomictrade.Checked);
                apiSettings.CheckedApis.Add("Cryptoine", chkCryptoine.Checked);

                apiSettings.CheckedApis.Add("CoinDesk", chkCoindesk.Checked);
                apiSettings.CheckedApis.Add("Nicehash", chkNiceHash.Checked);
                apiSettings.CheckedApis.Add("WhatToMine", chkWhattomine.Checked);
                apiSettings.CheckedApis.Add("CoinTweak", chkCointweak.Checked);
                apiSettings.CheckedApis.Add("CoinWarz", chkCoinwarz.Checked);
                apiSettings.CheckedApis.Add("CrypToday", chkCryptoday.Checked);
                apiSettings.CheckedApis.Add("PoolPicker", chkPoolpicker.Checked);

                apiSettings.CheckedMisc.Add("WeightedCalculations", radWeighted.Checked);
                apiSettings.CheckedMisc.Add("UseBestFallThrough", radFallThroughExchange.Checked);
                apiSettings.CheckedMisc.Add("UseMostVolume", radVolumeExchange.Checked);
                apiSettings.CheckedMisc.Add("HighestBid", radHighestBid.Checked);
                apiSettings.CheckedMisc.Add("MostRecentTrade", radMostRecentTrade.Checked);
                apiSettings.CheckedMisc.Add("LowestAsk", radLowestAsk.Checked);

                apiSettings.CheckedMisc.Add("RemoveUnlisted", chkRemoveUnlisted.Checked);
                apiSettings.CheckedMisc.Add("RemoveFrozen", chkRemoveUnlisted.Checked);
                apiSettings.CheckedMisc.Add("RemoveTooGoodToBeTrue", chkRemoveTooGoodToBeTrue.Checked);
                apiSettings.CheckedMisc.Add("RemoveZeroVolume", chkRemoveZeroVolume.Checked);
                apiSettings.CheckedMisc.Add("RemoveNegative", chkRemoveNegative.Checked);
                apiSettings.CheckedMisc.Add("Use24hDiff", chk24hDiff.Checked);
                apiSettings.CheckedMisc.Add("ColoredTable", chkColor.Checked);
                apiSettings.CheckedMisc.Add("Proxy", chkProxy.Checked);
                apiSettings.CheckedMisc.Add("GetOrderDepths", chkProxy.Checked);

                File.WriteAllText(@"apisettings.txt",
                    JsonConvert.SerializeObject(apiSettings, Formatting.Indented));

                File.WriteAllText(@"customcoins.txt",
                    JsonConvert.SerializeObject(_customCoins, Formatting.Indented));

                AppendToLog("Settings saved");
                File.WriteAllText(@"log.txt", txtLog.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Oops, something went wrong while saving your setting. Try running as admin."
                    + Environment.NewLine + e);
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            // Actual process starts here ^^"
            DateTime start = DateTime.Now;
            Cursor = Cursors.AppStarting;

            tsStatus.Text = "Starting new profit calculation...";
            tsErrors.Text = "0 errors";
            tsErrors.ForeColor = Color.Green;
            tsProgress.Value = 0;

            tsStatus.Text = "Parsing given hashrates...";

            const int i = 5;
            GetCoinList(i);
            tsProgress.Value += i;

            tsStatus.Text = "Calculating profits and sorting the list...";
            _coinLists = new Dictionary<string, List<Coin>>();
            UpdateResults(true);

            tsProgress.Value = 100;
            TimeSpan end = DateTime.Now.Subtract(start);
            tsStatus.Text = "Completed in " + end.TotalSeconds.ToString("0.##") + " seconds";

            File.WriteAllText(@"log.txt", txtLog.Text);
            Cursor = Cursors.Default;
        }

        private List<Coin> CalculatePrices(KeyValuePair<string, Profile> profile)
        {
            List<Coin> coinList = new List<Coin>();
            try
            {
                coinList = _sourceList.CalculatePrices(radWeighted.Checked, radFallThroughExchange.Checked,
                    chkCoindesk.Checked, chk24hDiff.Checked, profile.Value);
            }
            catch (Exception exception)
            {
                AppendToLog("Error while calculating your profits for profile " + profile.Key,
                    exception);
            }

            return coinList;
        }

        private List<Coin> GetCleanedCoinList(IEnumerable<Coin> listOfCoins)
        {
            ParallelQuery<Coin> tempCoinList = listOfCoins.AsParallel();

            if (chkRemoveUnlisted.Checked)
            {
                tsStatus.Text = "Removing coins that aren't listed on supported exchanges...";
                tempCoinList = tempCoinList.Where(coin =>
                    coin.HasImplementedMarketApi);
            }

            if (chkRemoveFrozenCoins.Checked)
            {
                tsStatus.Text = "Removing coins that are frozen on supported exchanges...";
                tempCoinList = tempCoinList.Where(coin => 
                    coin.Exchanges.Count(exchange => exchange.IsFrozen) == 0);
            }

            if (chkRemoveTooGoodToBeTrue.Checked)
            {
                tsStatus.Text = "Removing coins with a volume lower than you can earn..";
                tempCoinList = tempCoinList.Where(coin =>
                    coin.TotalExchange.BtcVolume > coin.BtcPerDay || coin.IsMultiPool);
            }

            if (chkRemoveZeroVolume.Checked)
            {
                tsStatus.Text = "Removing coins with zero volume..";
                tempCoinList = tempCoinList.Where(coin =>
                    (coin.TotalExchange.BtcVolume > 0 || coin.IsMultiPool) && !Double.IsNaN(coin.TotalExchange.BtcVolume));
            }

            if (chkRemoveNegative.Checked)
            {
                tsStatus.Text = "Removing results with a negative profit..";
                tempCoinList = tempCoinList.Where(coin =>
                    coin.BtcPerDay > 0);
            }

            return new List<Coin>(tempCoinList.OrderByDescending(o => o.BtcPerDay));
        }

        private void UpdateResults(bool init)
        {
            foreach (KeyValuePair<string, Profile> profile in _profileList)
            {
                if (init)
                {
                    _coinLists.Add(profile.Key, CalculatePrices(profile));
                }

                foreach (TabPage tabPage in tbcResults.TabPages)
                {
                    if (tabPage.Text == profile.Key)
                    {
                        foreach (Control control in tabPage.Controls)
                        {
                            ResultsDatagrid results = control as ResultsDatagrid;
                            if (results != null)
                            {
                                results.UpdateCoinList(
                                    GetCleanedCoinList(_coinLists[profile.Key]), 
                                    chkColor.Checked);
                            }
                        }
                    }
                }
            }
        }

        

        private void GetCoinList(int progress)
        {
            HttpClientHandler hch = new HttpClientHandler();
            if (chkProxy.Checked)
            {
                hch.UseProxy = true;
                hch.Proxy = String.IsNullOrEmpty(txtProxy.Text) 
                    ? WebRequest.GetSystemWebProxy() 
                    : new WebProxy(txtProxy.Text);
            }
            else
            {
                hch.UseProxy = false;
                hch.Proxy = null;
            }

            HttpClient client = new HttpClient(hch, false)
            {
                Timeout = TimeSpan.FromSeconds((double) nudTimeout.Value)
            };

            int bidRecentAsk = radLowestAsk.Checked ? 2 : (radMostRecentTrade.Checked ? 1 : 0);
            _sourceList = new CoinList(client, bidRecentAsk, chkOrderDepth.Checked);

            if (_customCoins.Count > 0)
            {
                try
                {
                    tsStatus.Text = "Adding custom coins...";
                    _sourceList.AddCustomCoins(_customCoins);
                    // Refresh so it shows updated custom coin params through rpc
                    dgvCustomCoins.Refresh();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while adding custom coins",exception);
                }
            }

            tsProgress.Value += progress;
            if (chkNiceHash.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting actual NiceHash prices...";
                    _sourceList.UpdateNiceHash();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from NiceHash.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkPoolpicker.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting multipool prices from PoolPicker...";
                    _sourceList.UpdatePoolPicker(nudPoolpicker.Value, chkReviewCalc.Checked);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from PoolPicker.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkCryptoday.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting multipool prices from CrypToday...";
                    _sourceList.UpdateCrypToday(nudCryptoday.Value);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from CrypToday.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkCointweak.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting coin info from CoinTweak...";
                    _sourceList.UpdateCoinTweak(txtCointweakApiKey.Text);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from CoinTweak.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkCoinwarz.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting coin info from CoinWarz...";
                    _sourceList.UpdateCoinWarz(txtCoinwarzApiKey.Text);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from CoinWarz.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkWhattomine.Checked)
            {
                try
                {
                    tsStatus.Text = "Getting coin info from WhatToMine...";
                    _sourceList.UpdateWhatToMine();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from WhatToMine.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkBittrex.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Bittrex prices...";
                    _sourceList.UpdateBittrex();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Bittrex.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkMintpal.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with MintPal prices...";
                    _sourceList.UpdateMintPal();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Mintpal.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkCryptsy.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Cryptsy prices...";
                    _sourceList.UpdateCryptsy();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Cryptsy.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkPoloniex.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Poloniex prices...";
                    _sourceList.UpdatePoloniex();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Poloniex.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkBTer.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with BTer prices...";
                    _sourceList.UpdateBTer();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from BTer.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkAllcoin.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with AllCoin prices...";
                    _sourceList.UpdateAllCoin();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from AllCoin.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkAllcrypt.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with AllCrypt prices...";
                    _sourceList.UpdateAllCrypt();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from AllCrypt.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkCCex.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with C-Cex prices...";
                    _sourceList.UpdateCCex(txtCcexApiKey.Text);
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from C-Cex.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkComkort.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Comkort prices...";
                    _sourceList.UpdateComkort();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Comkort.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkAtomictrade.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Atomic Trade prices...";
                    _sourceList.UpdateAtomicTrade();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Atomic Trade.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkCryptoine.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with Cryptoine prices...";
                    _sourceList.UpdateCryptoine();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Cryptoine.",
                        exception);
                }
            }

            tsProgress.Value += progress;
            if (chkCoindesk.Checked)
            {
                try
                {
                    tsStatus.Text = "Updating with fiat prices...";
                    _sourceList.UpdateFiatPrices();
                }
                catch (Exception exception)
                {
                    AppendToLog("Error while getting data from Coindesk, used to calculate your " + cbbFiat.Text + "/day.",
                        exception);
                }
            }
        }

        private void AppendToLog(string s)
        {
            txtLog.AppendText("[" + DateTime.Now + "] " + s + Environment.NewLine);
        }

        private void AppendToLog(string s, Exception e)
        {
            txtLog.AppendText("[" + DateTime.Now + "] [ERROR] " + s + Environment.NewLine + e + Environment.NewLine);
            UpdateErrorCounter();
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

        private Dictionary<string, Profile> GetDefaultProfileList()
        {
            CustomAlgo groestl = new CustomAlgo
            {
                Use = true,
                Name = "GROESTL",
                SynonymsCsv = "",
                HashRate = 7.7,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo myrgroestl = new CustomAlgo
            {
                Use = true,
                Name = "MYR-GROESTL",
                SynonymsCsv = "MYRIADGROESTL",
                HashRate = 12.9,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo fugue = new CustomAlgo
            {
                Use = true,
                Name = "FUGUE",
                SynonymsCsv = "FUGUE256",
                HashRate = 93.9,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo jha = new CustomAlgo
            {
                Use = true,
                Name = "JHA",
                SynonymsCsv = "JACKPOT",
                HashRate = 5.6,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo nist5 = new CustomAlgo
            {
                Use = true,
                Name = "NIST5",
                SynonymsCsv = "",
                HashRate = 8.4,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo heavy = new CustomAlgo
            {
                Use = true,
                Name = "HEAVY",
                SynonymsCsv = "HEFTY,HEFTY1,HEAVYCOIN",
                HashRate = 13.1,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo x11 = new CustomAlgo
            {
                Use = true,
                Name = "X11",
                SynonymsCsv = "",
                HashRate = 2.6,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo x13 = new CustomAlgo
            {
                Use = true,
                Name = "X13",
                SynonymsCsv = "",
                HashRate = 2.0,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo x15 = new CustomAlgo
            {
                Use = true,
                Name = "X15",
                SynonymsCsv = "",
                HashRate = 1.5,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo quark = new CustomAlgo
            {
                Use = true,
                Name = "QUARK",
                SynonymsCsv = "",
                HashRate = 4.5,
                Wattage = 0,
                Style = "Classic",
                Target = 24
            };

            CustomAlgo qubit = new CustomAlgo
            {
                Use = true,
                Name = "QUBIT",
                SynonymsCsv = "",
                HashRate = 3.9,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo keccak = new CustomAlgo
            {
                Use = true,
                Name = "KECCAK",
                SynonymsCsv = "",
                HashRate = 163.1,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo scrypt = new CustomAlgo
            {
                Use = true,
                Name = "SCRYPT",
                SynonymsCsv = "",
                HashRate = 0.28,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo scryptn = new CustomAlgo
            {
                Use = true,
                Name = "SCRYPTN",
                SynonymsCsv = "SCRYPT-N,SCRYPT-ADAPTIVE-NFACTOR",
                HashRate = 0.14,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo scryptjane15 = new CustomAlgo
            {
                Use = true,
                Name = "SCRYPTJANE15",
                SynonymsCsv = "CHACHA (NF15)",
                HashRate = 0.0009,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo scryptjane14 = new CustomAlgo
            {
                Use = true,
                Name = "SCRYPTJANE14",
                SynonymsCsv = "CHACHA (NF14)",
                HashRate = 0.0034,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo scryptjane13 = new CustomAlgo
            {
                Use = true,
                Name = "SCRYPTJANE13",
                SynonymsCsv = "CHACHA (NF13)",
                HashRate = 0.0095,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            CustomAlgo cryptonight = new CustomAlgo
            {
                Use = true,
                Name = "CRYPTONIGHT",
                SynonymsCsv = "",
                HashRate = 0.00022,
                Wattage = 0,
                Style = "CryptoNight",
                Target = 0
            };

            CustomAlgo sha256 = new CustomAlgo
            {
                Use = false,
                Name = "SHA256",
                SynonymsCsv = "SHA-256",
                HashRate = 0,
                Wattage = 0,
                Style = "Classic",
                Target = 32
            };

            BindingList<CustomAlgo> algoList = new BindingList<CustomAlgo>
            {
                groestl,
                myrgroestl,
                fugue,
                jha,
                nist5,
                heavy,
                x11,
                x13,
                x15,
                quark,
                qubit,
                keccak,
                scrypt,
                scryptn,
                scryptjane15,
                scryptjane14,
                scryptjane13,
                cryptonight,
                sha256
            };

            Profile defaultProfile = new Profile
            {
                FiatOfChoice = 0,
                FiatPerKwh = 0.1,
                Multiplier = 1,
                CustomAlgoList = algoList
            };

            Dictionary<string, Profile> defaultProfileList = new Dictionary<string, Profile>
            {
                {"Default", defaultProfile}
            };

            return defaultProfileList;
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
            if (_sourceList != null && _sourceList.ListOfCoins != null)
            {
                UpdateResults(false);
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

            if (_profileList != null && _profileList.ContainsKey(cbbProfiles.Text))
            {
                _profileList[cbbProfiles.Text].FiatOfChoice = cbbFiat.SelectedIndex;
            }
        }

        private void SetVisibleFiatColumn()
        {
            foreach (TabPage tabPage in tbcResults.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    ResultsDatagrid results = control as ResultsDatagrid;
                    if (results != null)
                    {
                        DataGridView dgvResults = results.GetDataGridView();
                        if (chkCoindesk.Checked)
                        {
                            switch (cbbFiat.SelectedIndex)
                            {
                                case 0:
                                    lblElectricityCost.Text = "USD/kWh";
                                    dgvResults.Columns[3].Visible = true;
                                    dgvResults.Columns[4].Visible = false;
                                    dgvResults.Columns[5].Visible = false;
                                    dgvResults.Columns[6].Visible = false;
                                    break;
                                case 1:
                                    lblElectricityCost.Text = "EUR/kWh";
                                    dgvResults.Columns[3].Visible = false;
                                    dgvResults.Columns[4].Visible = true;
                                    dgvResults.Columns[5].Visible = false;
                                    dgvResults.Columns[6].Visible = false;
                                    break;
                                case 2:
                                    lblElectricityCost.Text = "GBP/kWh";
                                    dgvResults.Columns[3].Visible = false;
                                    dgvResults.Columns[4].Visible = false;
                                    dgvResults.Columns[5].Visible = true;
                                    dgvResults.Columns[6].Visible = false;
                                    break;
                                case 3:
                                    lblElectricityCost.Text = "CNY/kWh";
                                    dgvResults.Columns[3].Visible = false;
                                    dgvResults.Columns[4].Visible = false;
                                    dgvResults.Columns[5].Visible = false;
                                    dgvResults.Columns[6].Visible = true;
                                    break;
                                default:
                                    lblElectricityCost.Text = "USD/kWh";
                                    dgvResults.Columns[3].Visible = true;
                                    dgvResults.Columns[4].Visible = true;
                                    dgvResults.Columns[5].Visible = true;
                                    dgvResults.Columns[6].Visible = true;
                                    break;
                            }
                        }
                        else
                        {
                            lblElectricityCost.Text = "Disabled";
                            dgvResults.Columns[3].Visible = false;
                            dgvResults.Columns[4].Visible = false;
                            dgvResults.Columns[5].Visible = false;
                            dgvResults.Columns[6].Visible = false;
                        }
                    }
                }
            }

            int x = txtFiatElectricityCost.Location.X - 6 - lblElectricityCost.Size.Width;
            lblElectricityCost.Location = new Point(x, lblElectricityCost.Location.Y);
        }

        private void tsStatus_TextChanged(object sender, EventArgs e)
        {
            AppendToLog(tsStatus.Text);
        }

        private void chkColor_CheckStateChanged(object sender, EventArgs e)
        {
             UpdateFilterColors();
        }

        private void UpdateFilterColors()
        {
            if (chkColor.Checked)
            {
                chkRemoveUnlisted.BackColor = Color.Plum;
                chkRemoveFrozenCoins.BackColor = Color.DeepSkyBlue;
                chkRemoveTooGoodToBeTrue.BackColor = Color.PaleTurquoise;
                chkRemoveZeroVolume.BackColor = Color.BurlyWood;
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
                chkRemoveZeroVolume.BackColor = Color.Transparent;
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
                _profileList.Remove(cbbProfiles.Text);
                foreach (TabPage tabPage in tbcResults.TabPages)
                {
                    if (tabPage.Text == cbbProfiles.Text)
                    {
                        tabPage.Dispose();
                        break;
                    }
                }

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
            else if (!_profileList.ContainsKey(cbbProfiles.Text))
            {
                _profileList.Add(cbbProfiles.Text, GetDefaultProfileList()["Default"]);
                dgvCustomAlgos.DataSource = null;
                dgvCustomAlgos.DataSource = _profileList[cbbProfiles.Text].CustomAlgoList;
                AddToResultsTabControl(cbbProfiles.Text);
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
            dgvCustomAlgos.AutoGenerateColumns = false;
            
            if (_profileList != null)
            {
                dgvCustomAlgos.DataSource = _profileList[cbbProfiles.Text].CustomAlgoList;
                nudAmount.Value = _profileList[cbbProfiles.Text].Multiplier;
                cbbFiat.SelectedIndex = _profileList[cbbProfiles.Text].FiatOfChoice;
                txtFiatElectricityCost.Text = _profileList[cbbProfiles.Text].FiatPerKwh.ToString(CultureInfo.InvariantCulture);
                UpdateHistoricAlgo(_profileList[cbbProfiles.Text].CustomAlgoList);
            }
        }

        private void dgvNeedsEdit_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (e.Button == MouseButtons.Left && dgv != null)
            {
                if (dgv.HitTest(e.X, e.Y).Type
                    == DataGridViewHitTestType.Cell)
                {
                    dgv.BeginEdit(true);
                }
                else
                {
                    dgv.EndEdit();
                }
            }
        }

        private void dgvCustomAlgos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= NumberColumn_KeyPress;
            e.Control.KeyPress -= NameOrSynonymColumn_KeyPress;

            TextBox tb = e.Control as TextBox;
            if (tb != null)
            {
                switch (dgvCustomAlgos.CurrentCell.ColumnIndex)
                {
                    case 1:
                    case 2:
                        tb.KeyPress += NameOrSynonymColumn_KeyPress;
                        break;
                    case 4:
                    case 5:
                        tb.KeyPress += NumberColumn_KeyPress;
                        break;
                }
            }
        }

        private void dgvCustomCoins_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= NumberColumn_KeyPress;
            e.Control.KeyPress -= AlgoColumn_KeyPress;
            TextBox tb = e.Control as TextBox;
            if (tb != null)
            {
                switch (dgvCustomCoins.CurrentCell.ColumnIndex)
                {
                    case 3:
                        tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        tb.AutoCompleteCustomSource = _historicAlgoList;
                        tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        tb.KeyPress += AlgoColumn_KeyPress;
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        tb.KeyPress += NumberColumn_KeyPress;
                        break;
                }
            }
        }

        private void dgvJsonRpc_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= PortColumn_KeyPress;

            TextBox tb = e.Control as TextBox;
            if (tb != null && dgvJsonRpc.CurrentCell.ColumnIndex == 8)
            {
                tb.KeyPress += PortColumn_KeyPress;
            }
        }

        private void PortColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AlgoColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsLetterOrDigit(e.KeyChar))
            {
                e.KeyChar = char.ToUpperInvariant(e.KeyChar);
            }
        }

        private void NameOrSynonymColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                if (char.IsLetterOrDigit(e.KeyChar))
                {
                    e.KeyChar = char.ToUpperInvariant(e.KeyChar);
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void NumberColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void dgvCustomCoins_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = true;
            e.Row.Cells[3].Value = _profileList[cbbProfiles.Text].CustomAlgoList[_profileList[cbbProfiles.Text].CustomAlgoList.Count - 1].Name;
        }


        private void dgvCustomAlgos_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = true;
            e.Row.Cells[3].Value = "Classic";
            e.Row.Cells[6].Value = 32;
        }

        private void dgvJsonRpc_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = true;
            e.Row.Cells[3].Value = true;
            e.Row.Cells[4].Value = true;
            e.Row.Cells[5].Value = true;
            e.Row.Cells[6].Value = true;
            e.Row.Cells[7].Value = "127.0.0.1";
        }

        private void dgvCustomAlgos_Validated(object sender, EventArgs e)
        {
            UpdateHistoricAlgo(_profileList[cbbProfiles.Text].CustomAlgoList);
        }

        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            if (_profileList != null && _profileList.ContainsKey(cbbProfiles.Text))
            {
                _profileList[cbbProfiles.Text].Multiplier = (int) nudAmount.Value;
            }
        }

        private void txtFiatElectricityCost_TextChanged(object sender, EventArgs e)
        {
            if (_profileList != null && _profileList.ContainsKey(cbbProfiles.Text))
            {
                double fiatPerKwh;
                if (double.TryParse(txtFiatElectricityCost.Text, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out fiatPerKwh))
                {
                    _profileList[cbbProfiles.Text].FiatPerKwh = fiatPerKwh;
                }
                else
                {
                    txtFiatElectricityCost.Text = "0";
                }
            }
        }

        private void chkAllHashrates_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CustomAlgo algo in _profileList[cbbProfiles.Text].CustomAlgoList)
            {
                algo.Use = chkAllHashrates.Checked;
            }
            dgvCustomAlgos.Refresh();
        }

        private void picDonate_Click(object sender, EventArgs e)
        {
        }

        private void chkCCex_CheckedChanged(object sender, EventArgs e)
        {
            txtCcexApiKey.Enabled = chkCCex.Checked;
        }

        private void radFallThroughExchange_CheckedChanged(object sender, EventArgs e)
        {
            grpExchangePrice.Enabled = !radFallThroughExchange.Checked;
            if(radFallThroughExchange.Checked) radHighestBid.Checked = true;
        }
    }
}