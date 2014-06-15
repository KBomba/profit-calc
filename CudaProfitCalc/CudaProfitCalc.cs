using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CudaProfitCalc
{
    public partial class CudaProfitCalc : Form
    {
        private CoinList _coinList;
        private Dictionary<HashAlgo.Algo, double> _hashList;

        public CudaProfitCalc()
        {
            InitializeComponent();
        }

        private bool CheckApiBox()
        {
            if (chkWhattomine.Checked || chkCointweak.Checked || chkCoinwarz.Checked)
            {
                return true;
            }

            MessageBox.Show("Please leave at least WhatToMine, CoinTweak or CoinWarz checked");
            return false;
        }

        private void chkWhattomine_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckApiBox()) chkWhattomine.Checked = true;
        }

        private void chkCointweak_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckApiBox()) chkCointweak.Checked = true;
            txtCointweakApiKey.Enabled = chkCointweak.Checked;
        }

        private void chkCoinwarz_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckApiBox()) chkCoinwarz.Checked = true;
            txtCoinwarzApiKey.Enabled = chkCoinwarz.Checked;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            tsStatus.Text = "Busy...";
            tsProgress.Value = 0;

            _hashList = ParseHashrates();
            CoinList coinList = GetCoinList();
            tsProgress.Value += 11;

            coinList.Sort(_hashList);
            tsProgress.Value += 11;

            UpdateDataGridView();
            tsProgress.Value = 100;
            tsStatus.Text = "Completed";
        }

        private void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear();
            DataGridViewRow[] arrCoinRows = new DataGridViewRow[_coinList.List.Count];

            for (int index = 0; index < _coinList.List.Count; index++)
            {
                Coin coin = _coinList.List[index];
                arrCoinRows[index] = new DataGridViewRow();
                arrCoinRows[index].HeaderCell.Value = String.Format("{0}", index + 1);
                arrCoinRows[index].CreateCells(dataGridView1, coin.TagName, coin.CoinName, coin.Algo, coin.BtcPerDay.ToString("0.000000000"), coin.CoinsPerDay,
                    coin.BestExchange.ExchangeName, coin.BestExchange.BtcPrice.ToString("0.000000000"), coin.BestExchange.BtcVolume, coin.Difficulty, coin.BlockReward);
            }

            dataGridView1.Rows.AddRange(arrCoinRows);
        }

        private CoinList GetCoinList()
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
                
            tsProgress.Value += 11;
            try
            {
                if (chkWhattomine.Checked) _coinList.UpdateWhatToMine("http://www.whattomine.com/coins.json");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the WhatToMine API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }
                
            tsProgress.Value += 11;
            try 
            {
                if (chkCointweak.Checked) _coinList.UpdateCoinTweak("http://cointweak.com/API/getProfitOverview/&key=" + txtCointweakApiKey.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the CoinTweak API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }
                
            tsProgress.Value += 11;
            try 
            {
                if (chkCoinwarz.Checked) _coinList.UpdateCoinWarz("http://www.coinwarz.com/v1/api/profitability/?apikey=" + txtCoinwarzApiKey.Text + "&algo=all");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the CoinWarz API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }
                
            tsProgress.Value += 11;
            try 
            {
                if (chkBittrex.Checked) _coinList.UpdateBittrex("https://bittrex.com/api/v1/public/getmarketsummaries");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the Bittrex API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }
                
            tsProgress.Value += 11;
            try 
            {
                if (chkMintpal.Checked) _coinList.UpdateMintPal("https://api.mintpal.com/v2/market/summary/BTC");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the Mintpal API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }
                
            tsProgress.Value += 11;
            try 
            {
                if (chkCryptsy.Checked) _coinList.UpdateCryptsy("http://pubapi.cryptsy.com/api.php?method=marketdatav2");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Oops, something went wrong with the Cryptsy API." + Environment.NewLine + Environment.NewLine + exception.StackTrace);
            }

            if (chkShowOnlyHealthy.Checked)
            {
                List<Coin> tempList = _coinList.List.Where(coin => coin.HasImplementedMarketApi && !coin.HasMarketErrors).ToList();
                _coinList.List = tempList;
            }

            return _coinList;
        }

        private Dictionary<HashAlgo.Algo, double> ParseHashrates()
        {
            Dictionary<HashAlgo.Algo, double> hashList = new Dictionary<HashAlgo.Algo, double>();
            double d;

            if (Double.TryParse(txtGroestl.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Groestl, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your Groestl hashrate");
            }

            if (Double.TryParse(txtMyrGroestl.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.MyriadGroestl, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your Myr-Groestl hashrate");
            }

            if (Double.TryParse(txtFugue.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Fugue256, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your Fugue hashrate");
            }

            if (Double.TryParse(txtKeccak.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Keccak, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your Keccak hashrate");
            }

            if (Double.TryParse(txtJackpot.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.JHA, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your JHA hashrate");
            }

            if (Double.TryParse(txtNist5.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Nist5, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your NIST5 hashrate");
            }

            if (Double.TryParse(txtQuark.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Quark, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your Quark hashrate");
            }

            if (Double.TryParse(txtScrypt.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Scrypt, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your Scrypt hashrate");
            }

            if (Double.TryParse(txtX11.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.X11, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your X11 hashrate");
            }

            if (Double.TryParse(txtX13.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.X13, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your X13 hashrate");
            }

            if (Double.TryParse(txtHefty.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
            {
                hashList.Add(HashAlgo.Algo.Heavy, d);
            }
            else
            {
                MessageBox.Show("Something wrong with your Hefty/Heavy hashrate");
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
                foreach (KeyValuePair<HashAlgo.Algo, double> algo in _hashList)
                {
                    sb.Append(algo.Key + ": " + algo.Value + " MH/s" + Environment.NewLine);
                }

                Clipboard.SetText(sb.ToString());
            }
        }

    }
}
