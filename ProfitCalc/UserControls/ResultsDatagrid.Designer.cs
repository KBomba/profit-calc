namespace ProfitCalc
{
    partial class ResultsDatagrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.TagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Algo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsdPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EurPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GbpPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CnyPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtcPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoinsPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestExchangeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestExchangePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestExchangeVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeightedBtcPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Difficulty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BlockReward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TagName,
            this.FullName,
            this.Algo,
            this.UsdPerDay,
            this.EurPerDay,
            this.GbpPerDay,
            this.CnyPerDay,
            this.BtcPerDay,
            this.CoinsPerDay,
            this.BestExchangeName,
            this.BestExchangePrice,
            this.BestExchangeVolume,
            this.WeightedBtcPrice,
            this.TotalVolume,
            this.Difficulty,
            this.BlockReward});
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvResults.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.Size = new System.Drawing.Size(847, 363);
            this.dgvResults.TabIndex = 2;
            this.dgvResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellDoubleClick);
            // 
            // TagName
            // 
            this.TagName.DataPropertyName = "TagName";
            this.TagName.HeaderText = "Tag";
            this.TagName.Name = "TagName";
            this.TagName.ReadOnly = true;
            this.TagName.Width = 51;
            // 
            // FullName
            // 
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "Name";
            this.FullName.MinimumWidth = 200;
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.Width = 200;
            // 
            // Algo
            // 
            this.Algo.DataPropertyName = "Algo";
            this.Algo.HeaderText = "Algo";
            this.Algo.Name = "Algo";
            this.Algo.ReadOnly = true;
            this.Algo.Width = 53;
            // 
            // UsdPerDay
            // 
            this.UsdPerDay.DataPropertyName = "UsdPerDay";
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = null;
            this.UsdPerDay.DefaultCellStyle = dataGridViewCellStyle2;
            this.UsdPerDay.HeaderText = "USD per day";
            this.UsdPerDay.Name = "UsdPerDay";
            this.UsdPerDay.ReadOnly = true;
            this.UsdPerDay.Width = 70;
            // 
            // EurPerDay
            // 
            this.EurPerDay.DataPropertyName = "EurPerDay";
            dataGridViewCellStyle3.Format = "N3";
            this.EurPerDay.DefaultCellStyle = dataGridViewCellStyle3;
            this.EurPerDay.HeaderText = "EUR per day";
            this.EurPerDay.Name = "EurPerDay";
            this.EurPerDay.ReadOnly = true;
            this.EurPerDay.Visible = false;
            this.EurPerDay.Width = 93;
            // 
            // GbpPerDay
            // 
            this.GbpPerDay.DataPropertyName = "GbpPerDay";
            dataGridViewCellStyle4.Format = "N3";
            this.GbpPerDay.DefaultCellStyle = dataGridViewCellStyle4;
            this.GbpPerDay.HeaderText = "GBP per day";
            this.GbpPerDay.Name = "GbpPerDay";
            this.GbpPerDay.ReadOnly = true;
            this.GbpPerDay.Visible = false;
            this.GbpPerDay.Width = 92;
            // 
            // CnyPerDay
            // 
            this.CnyPerDay.DataPropertyName = "CnyPerDay";
            dataGridViewCellStyle5.Format = "N3";
            this.CnyPerDay.DefaultCellStyle = dataGridViewCellStyle5;
            this.CnyPerDay.HeaderText = "CNY per day";
            this.CnyPerDay.Name = "CnyPerDay";
            this.CnyPerDay.ReadOnly = true;
            this.CnyPerDay.Visible = false;
            this.CnyPerDay.Width = 92;
            // 
            // BtcPerDay
            // 
            this.BtcPerDay.DataPropertyName = "BtcPerDay";
            dataGridViewCellStyle6.Format = "N8";
            this.BtcPerDay.DefaultCellStyle = dataGridViewCellStyle6;
            this.BtcPerDay.HeaderText = "BTC per day";
            this.BtcPerDay.Name = "BtcPerDay";
            this.BtcPerDay.ReadOnly = true;
            this.BtcPerDay.Width = 69;
            // 
            // CoinsPerDay
            // 
            this.CoinsPerDay.DataPropertyName = "CoinsPerDay";
            dataGridViewCellStyle7.Format = "N5";
            dataGridViewCellStyle7.NullValue = null;
            this.CoinsPerDay.DefaultCellStyle = dataGridViewCellStyle7;
            this.CoinsPerDay.HeaderText = "Coins per day";
            this.CoinsPerDay.Name = "CoinsPerDay";
            this.CoinsPerDay.ReadOnly = true;
            this.CoinsPerDay.Width = 73;
            // 
            // BestExchangeName
            // 
            this.BestExchangeName.DataPropertyName = "BestExchangeName";
            this.BestExchangeName.HeaderText = "Best exchange";
            this.BestExchangeName.Name = "BestExchangeName";
            this.BestExchangeName.ReadOnly = true;
            this.BestExchangeName.Width = 95;
            // 
            // BestExchangePrice
            // 
            this.BestExchangePrice.DataPropertyName = "BestExchangePrice";
            dataGridViewCellStyle8.Format = "N8";
            this.BestExchangePrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.BestExchangePrice.HeaderText = "Best exchange\'s price";
            this.BestExchangePrice.Name = "BestExchangePrice";
            this.BestExchangePrice.ReadOnly = true;
            this.BestExchangePrice.Width = 124;
            // 
            // BestExchangeVolume
            // 
            this.BestExchangeVolume.DataPropertyName = "BestExchangeVolume";
            dataGridViewCellStyle9.Format = "N3";
            this.BestExchangeVolume.DefaultCellStyle = dataGridViewCellStyle9;
            this.BestExchangeVolume.HeaderText = "Best exchange\'s volume";
            this.BestExchangeVolume.Name = "BestExchangeVolume";
            this.BestExchangeVolume.ReadOnly = true;
            this.BestExchangeVolume.Width = 134;
            // 
            // WeightedBtcPrice
            // 
            this.WeightedBtcPrice.DataPropertyName = "WeightedBtcPrice";
            dataGridViewCellStyle10.Format = "N8";
            this.WeightedBtcPrice.DefaultCellStyle = dataGridViewCellStyle10;
            this.WeightedBtcPrice.HeaderText = "Weighted price";
            this.WeightedBtcPrice.Name = "WeightedBtcPrice";
            this.WeightedBtcPrice.ReadOnly = true;
            this.WeightedBtcPrice.Width = 96;
            // 
            // TotalVolume
            // 
            this.TotalVolume.DataPropertyName = "TotalVolume";
            dataGridViewCellStyle11.Format = "N3";
            this.TotalVolume.DefaultCellStyle = dataGridViewCellStyle11;
            this.TotalVolume.HeaderText = "Total Volume";
            this.TotalVolume.Name = "TotalVolume";
            this.TotalVolume.ReadOnly = true;
            this.TotalVolume.Width = 87;
            // 
            // Difficulty
            // 
            this.Difficulty.DataPropertyName = "Difficulty";
            dataGridViewCellStyle12.Format = "N4";
            this.Difficulty.DefaultCellStyle = dataGridViewCellStyle12;
            this.Difficulty.HeaderText = "Difficulty";
            this.Difficulty.Name = "Difficulty";
            this.Difficulty.ReadOnly = true;
            this.Difficulty.Width = 72;
            // 
            // BlockReward
            // 
            this.BlockReward.DataPropertyName = "BlockReward";
            dataGridViewCellStyle13.Format = "N4";
            this.BlockReward.DefaultCellStyle = dataGridViewCellStyle13;
            this.BlockReward.HeaderText = "Block reward";
            this.BlockReward.Name = "BlockReward";
            this.BlockReward.ReadOnly = true;
            this.BlockReward.Width = 87;
            // 
            // ResultsDatagrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvResults);
            this.Name = "ResultsDatagrid";
            this.Size = new System.Drawing.Size(847, 363);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ResultsDatagrid_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Algo;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsdPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn EurPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn GbpPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn CnyPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn BtcPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoinsPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestExchangeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestExchangePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestExchangeVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeightedBtcPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn Difficulty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BlockReward;
    }
}
