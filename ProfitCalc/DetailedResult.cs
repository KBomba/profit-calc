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
    public partial class DetailedResult : Form
    {
        public Coin UsedCoin { get; set; }

        public DetailedResult(Coin usedCoin)
        {
            InitializeComponent();
            UsedCoin = usedCoin;
            FillGeneralInfo();
        }

        private void FillGeneralInfo()
        {
            txtTag.Text = UsedCoin.TagName;
            txtName.Text = UsedCoin.FullName;
            txtAlgo.Text = UsedCoin.Algo;
            txtSource.Text = UsedCoin.Source;
            txtRetrieved.Text = UsedCoin.Retrieved.ToString("dd MMMM HH:mm");

            txtHeight.Text = UsedCoin.Height.ToString(CultureInfo.InvariantCulture);
            txtDiff.Text = UsedCoin.Difficulty.ToString("0.########");
            txt24HAvgDiff.Text = UsedCoin.Avg24HDifficulty.ToString("0.########");
            txtNetHashrate.Text = (UsedCoin.NetHashRate / 1000000).ToString("0.####");
            txtBlockTime.Text = UsedCoin.BlockTime.ToString("0.####");
            txtBlockReward.Text = UsedCoin.BlockReward.ToString("0.####");

            txtCoinsPerDay.Text = UsedCoin.CoinsPerDay.ToString("0.####");
            txtBtcPerDay.Text = UsedCoin.BtcPerDay.ToString("0.########");
            txtUsdPerDay.Text = UsedCoin.UsdPerDay.ToString("0.####");
            txtEurPerDay.Text = UsedCoin.EurPerDay.ToString("0.####");
            txtGbpPerDay.Text = UsedCoin.GbpPerDay.ToString("0.####");
            txtCnyPerDay.Text = UsedCoin.CnyPerDay.ToString("0.####");
        }
    }
}
