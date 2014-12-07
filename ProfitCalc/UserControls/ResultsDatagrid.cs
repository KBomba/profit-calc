using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProfitCalc
{
    public partial class ResultsDatagrid : UserControl
    {
        private List<Coin> _listOfCoins;
        private bool _paintColors;

        public ResultsDatagrid()
        {
            InitializeComponent();
            dgvResults.AutoGenerateColumns = false;
            dgvResults.DataSource = _listOfCoins;
        }

        public void UpdateCoinList(List<Coin> listOfCoins, bool paintColors)
        {
            dgvResults.SuspendLayout();
            _listOfCoins = listOfCoins;
            _paintColors = paintColors;
            dgvResults.DataSource = null;
            dgvResults.DataSource = _listOfCoins;
            PaintColorsAndRowNumber();
            dgvResults.ResumeLayout();
        }

        public DataGridView GetDataGridView()
        {
            return dgvResults;
        }

        private void PaintColorsAndRowNumber()
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
                if (_paintColors)
                {
                    row.DefaultCellStyle.BackColor = GetRowColor(_listOfCoins[row.Index]);
                }
            }
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

            if (coin.TotalExchange.BtcVolume == 0)
            {
                return Color.BurlyWood;
            }

            if (coin.TotalExchange.BtcVolume < coin.BtcPerDay)
            {
                return Color.PaleTurquoise;
            }

            return Color.GreenYellow;
        }

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                new DetailedResult(_listOfCoins[e.RowIndex]) { Visible = true };
            }
        }

        private void ResultsDatagrid_Paint(object sender, PaintEventArgs e)
        {
            dgvResults.SuspendLayout();
            PaintColorsAndRowNumber();
            dgvResults.ResumeLayout();
        }
    }
}
