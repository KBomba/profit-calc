using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CudaProfitCalc
{
    public partial class CudaProfitCalc : Form
    {
        private CoinList _coinList;
        private HashRateJson _hashList;
        private decimal _hashRateMultiplier;

        public CudaProfitCalc()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists("hashrates.txt"))
                {
                    HashRateJson rates = JsonControl.GetSerializedApiFile<HashRateJson>("hashrates.txt");
                    txtGroestl.Text = rates.List[HashAlgo.Algo.Groestl].ToString(CultureInfo.InvariantCulture);
                    txtMyrGroestl.Text = rates.List[HashAlgo.Algo.MyriadGroestl].ToString(CultureInfo.InvariantCulture);
                    txtFugue.Text = rates.List[HashAlgo.Algo.Fugue256].ToString(CultureInfo.InvariantCulture);
                    txtKeccak.Text = rates.List[HashAlgo.Algo.Keccak].ToString(CultureInfo.InvariantCulture);
                    txtJackpot.Text = rates.List[HashAlgo.Algo.JHA].ToString(CultureInfo.InvariantCulture);
                    txtNist5.Text = rates.List[HashAlgo.Algo.Nist5].ToString(CultureInfo.InvariantCulture);
                    txtQuark.Text = rates.List[HashAlgo.Algo.Quark].ToString(CultureInfo.InvariantCulture);
                    txtScrypt.Text = rates.List[HashAlgo.Algo.Scrypt].ToString(CultureInfo.InvariantCulture);
                    txtX11.Text = rates.List[HashAlgo.Algo.X11].ToString(CultureInfo.InvariantCulture);
                    txtX13.Text = rates.List[HashAlgo.Algo.X13].ToString(CultureInfo.InvariantCulture);
                    txtHefty.Text = rates.List[HashAlgo.Algo.Heavy].ToString(CultureInfo.InvariantCulture);
                    txtScryptN.Text = rates.List[HashAlgo.Algo.ScryptN].ToString(CultureInfo.InvariantCulture);
                    txtJane15.Text = rates.List[HashAlgo.Algo.ScryptJane15].ToString(CultureInfo.InvariantCulture);
                    txtJane13.Text = rates.List[HashAlgo.Algo.ScryptJane13].ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with loading your hashrates.txt" + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }
            
            try
            {
                if (File.Exists("apisettings.txt"))
                {
                    ApiJson api = JsonControl.GetSerializedApiFile<ApiJson>("apisettings.txt");
                    txtCointweakApiKey.Text = api.ApiSettings["CoinTweak"];
                    txtCoinwarzApiKey.Text = api.ApiSettings["CoinWarz"];
                    nudPoolpicker.Text = api.ApiSettings["PoolPicker"];
                    chkWeight.Checked = bool.Parse(api.ApiSettings["WeightedCalculations"]);
                    nudAmount.Text = api.ApiSettings["Multiplier"];
                    _hashRateMultiplier = nudAmount.Value;

                    chkBittrex.Checked = api.CheckedApis["Bittrex"];
                    chkMintpal.Checked = api.CheckedApis["Mintpal"];
                    chkCryptsy.Checked = api.CheckedApis["Cryptsy"];
                    chkPoloniex.Checked = api.CheckedApis["Poloniex"];
                    chkNiceHash.Checked = api.CheckedApis["Nicehash"];
                    chkWhattomine.Checked = api.CheckedApis["WhatToMine"];
                    chkCointweak.Checked = api.CheckedApis["CoinTweak"];
                    chkCoinwarz.Checked = api.CheckedApis["CoinWarz"];
                    chkPoolpicker.Checked = api.CheckedApis["PoolPicker"];
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with loading your apisettings.txt" + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            _hashList = new HashRateJson { List = ParseHashrates((double) _hashRateMultiplier) };
        }

        private void SaveSettings()
        {
            _hashList = new HashRateJson { List = ParseHashrates(1) };
            string jsonHashlist = JsonConvert.SerializeObject(_hashList, Formatting.Indented);
            File.WriteAllText(@"hashrates.txt", jsonHashlist);

            ApiJson api = new ApiJson
            {
                ApiSettings = new Dictionary<string, string>(),
                CheckedApis = new Dictionary<string, bool>()
            };

            api.ApiSettings.Add("CoinTweak", txtCointweakApiKey.Text);
            api.ApiSettings.Add("CoinWarz", txtCoinwarzApiKey.Text);
            api.ApiSettings.Add("PoolPicker", nudPoolpicker.Text);
            api.ApiSettings.Add("WeightedCalculations", chkWeight.Checked.ToString());
            api.ApiSettings.Add("Multiplier", nudAmount.Text);

            api.CheckedApis.Add("Bittrex", chkBittrex.Checked);
            api.CheckedApis.Add("Mintpal", chkMintpal.Checked);
            api.CheckedApis.Add("Cryptsy", chkCryptsy.Checked);
            api.CheckedApis.Add("Poloniex", chkPoloniex.Checked);
            api.CheckedApis.Add("Nicehash", chkNiceHash.Checked);
            api.CheckedApis.Add("WhatToMine", chkWhattomine.Checked);
            api.CheckedApis.Add("CoinTweak", chkCointweak.Checked);
            api.CheckedApis.Add("CoinWarz", chkCoinwarz.Checked);
            api.CheckedApis.Add("PoolPicker", chkPoolpicker.Checked);

            string jsonApiList = JsonConvert.SerializeObject(api, Formatting.Indented);
            File.WriteAllText(@"apisettings.txt", jsonApiList);
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            tsStatus.Text = "Busy...";
            Thread.Sleep(50);
            tsProgress.Value = 0;

            _hashList.List = ParseHashrates((double) nudAmount.Value);

            const int i = 8;
            GetCoinList(i);
            tsProgress.Value += i;

            _coinList.Sort(_hashList.List, chkWeight.Checked);
            tsProgress.Value += i;

            UpdateDataGridView();

            tsProgress.Value = 100;
            tsStatus.Text = "Completed";
        }



        private void UpdateDataGridView()
        {
            dgView.Rows.Clear();
            DataGridViewRow[] arrCoinRows = new DataGridViewRow[_coinList.List.Count];

            for (int index = 0; index < _coinList.List.Count; index++)
            {
                Coin coin = _coinList.List[index];
                arrCoinRows[index] = new DataGridViewRow();
                arrCoinRows[index].HeaderCell.Value = String.Format("{0}", index + 1);
                arrCoinRows[index].CreateCells(dgView, coin.TagName, coin.CoinName, 
                    coin.Algo, coin.BtcPerDay.ToString("0.00000000"), coin.CoinsPerDay.ToString("0.00000"),
                    coin.Exchanges[0].ExchangeName, coin.Exchanges[0].BtcPrice.ToString("0.00000000"),
                    coin.Exchanges[0].BtcVolume.ToString("0.000"), coin.WeightedBtcPrice.ToString("0.00000000"),
                    coin.TotalVolume.ToString("0.000"), coin.Difficulty, coin.BlockReward);
            }

            dgView.Rows.AddRange(arrCoinRows);
        }

        private void GetCoinList(int progress)
        {
            _coinList = new CoinList();

            try
            {
                if (chkNiceHash.Checked) _coinList.UpdateNiceHash("https://www.nicehash.com/api?method=stats.global.current");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the NiceHash API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            tsProgress.Value += progress;
            try
            {
                if (chkWhattomine.Checked) _coinList.UpdateWhatToMine("http://www.whattomine.com/coins.json");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the WhatToMine API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            tsProgress.Value += progress;
            try 
            {
                if (chkCointweak.Checked) _coinList.UpdateCoinTweak("http://cointweak.com/API/getProfitOverview/&key=" + txtCointweakApiKey.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the CoinTweak API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            tsProgress.Value += progress;
            try 
            {
                if (chkCoinwarz.Checked) _coinList.UpdateCoinWarz("http://www.coinwarz.com/v1/api/profitability/?apikey=" + txtCoinwarzApiKey.Text + "&algo=all");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the CoinWarz API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            tsProgress.Value += progress;
            try 
            {
                if (chkBittrex.Checked) _coinList.UpdateBittrex("https://bittrex.com/api/v1/public/getmarketsummaries");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the Bittrex API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            tsProgress.Value += progress;
            try 
            {
                if (chkMintpal.Checked) _coinList.UpdateMintPal("https://api.mintpal.com/v2/market/summary/BTC");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the Mintpal API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            tsProgress.Value += progress;
            try 
            {
                if (chkCryptsy.Checked) _coinList.UpdateCryptsy("http://pubapi.cryptsy.com/api.php?method=marketdatav2");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the Cryptsy API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            tsProgress.Value += progress;
            try 
            {
                if (chkPoloniex.Checked) _coinList.UpdatePoloniex("https://poloniex.com/public?command=returnTicker");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the Poloniex API."  + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            tsProgress.Value += progress;
            try
            {
                if (chkPoolpicker.Checked) _coinList.UpdatePoolPicker("http://poolpicker.eu/api", nudPoolpicker.Value);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the PoolPicker API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            if (chkShowOnlyHealthy.Checked)
            {
                List<Coin> tempList = _coinList.List.Where(coin => coin.HasImplementedMarketApi && !coin.HasMarketErrors).ToList();
                _coinList.List = tempList;
            }
        }

        private Dictionary<HashAlgo.Algo, double> ParseHashrates(double multiplier)
        {

            Dictionary<HashAlgo.Algo, double> hashList = new Dictionary<HashAlgo.Algo, double>();
            double d;

            if (Double.TryParse(txtGroestl.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Groestl, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Groestl hashrate");
            }

            if (Double.TryParse(txtMyrGroestl.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.MyriadGroestl, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Myr-Groestl hashrate");
            }

            if (Double.TryParse(txtFugue.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Fugue256, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Fugue hashrate");
            }

            if (Double.TryParse(txtKeccak.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Keccak, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Keccak hashrate");
            }

            if (Double.TryParse(txtJackpot.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.JHA, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your JHA hashrate");
            }

            if (Double.TryParse(txtNist5.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Nist5, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your NIST5 hashrate");
            }

            if (Double.TryParse(txtQuark.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Quark, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Quark hashrate");
            }

            if (Double.TryParse(txtScrypt.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Scrypt, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Scrypt hashrate");
            }

            if (Double.TryParse(txtX11.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.X11, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your X11 hashrate");
            }

            if (Double.TryParse(txtX13.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.X13, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your X13 hashrate");
            }

            if (Double.TryParse(txtHefty.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Heavy, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Hefty hashrate");
            }

            if (Double.TryParse(txtScryptN.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.ScryptN, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Scrypt-N hashrate");
            }

            if (Double.TryParse(txtJane15.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.ScryptJane15, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Scrypt-Jane (15) hashrate");
            }

            if (Double.TryParse(txtJane13.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.ScryptJane13, d * multiplier);
            }
            else
            {
                MessageBox.Show("Something wrong with your Scrypt-Jane (13) hashrate");
            }

            return hashList;
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
                foreach (KeyValuePair<HashAlgo.Algo, double> algo in _hashList.List)
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

        private void nudAmount_ValueChanged(object sender, EventArgs e)
        {
            _hashRateMultiplier = nudAmount.Value;
        }

        private void chkPoolpicker_CheckedChanged(object sender, EventArgs e)
        {
            nudPoolpicker.Enabled = chkPoolpicker.Checked;
        }

    }
}
