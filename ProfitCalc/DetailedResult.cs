using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }
    }
}
