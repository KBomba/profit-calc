using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProfitCalc
{
    public sealed partial class DetailedResult : Form
    {
        private Coin _usedCoin;

        public DetailedResult(Coin usedCoin)
        {
            InitializeComponent();
            _usedCoin = usedCoin;
            Text = "[" + _usedCoin.TagName + "] " + _usedCoin.FullName + " - Detailed results";
            FillGeneralInfo();
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
    }
}
