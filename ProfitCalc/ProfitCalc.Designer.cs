namespace ProfitCalc
{
    partial class ProfitCalc
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfitCalc));
            this.btnAddDeleteProfile = new System.Windows.Forms.Button();
            this.cbbProfiles = new System.Windows.Forms.ComboBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.cbbFiat = new System.Windows.Forms.ComboBox();
            this.lblElectricityCost = new System.Windows.Forms.Label();
            this.txtFiatElectricityCost = new System.Windows.Forms.TextBox();
            this.chkCoindesk = new System.Windows.Forms.CheckBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoinName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Algo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsdPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EurPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GbpPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CnyPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtcPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoinsPerDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestExchange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestExchangePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExchangeVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeightedPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Difficulty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BlockReward = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxtStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmResultsToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHashratesToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.stStatusStrip = new System.Windows.Forms.StatusStrip();
            this.tsProgressText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsErrors = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.chkProxy = new System.Windows.Forms.CheckBox();
            this.chkColor = new System.Windows.Forms.CheckBox();
            this.tabMultipool = new System.Windows.Forms.TabPage();
            this.nudCryptoday = new System.Windows.Forms.NumericUpDown();
            this.chkCryptoday = new System.Windows.Forms.CheckBox();
            this.chkReviewCalc = new System.Windows.Forms.CheckBox();
            this.chkNiceHash = new System.Windows.Forms.CheckBox();
            this.nudPoolpicker = new System.Windows.Forms.NumericUpDown();
            this.chkPoolpicker = new System.Windows.Forms.CheckBox();
            this.tabPriceCalc = new System.Windows.Forms.TabPage();
            this.cbbBidRecentAsk = new System.Windows.Forms.ComboBox();
            this.lblBidRecentAsk = new System.Windows.Forms.Label();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.chkWeight = new System.Windows.Forms.CheckBox();
            this.lblAmountOfGpu = new System.Windows.Forms.Label();
            this.chkRemoveTooGoodToBeTrue = new System.Windows.Forms.CheckBox();
            this.chkRemoveNegative = new System.Windows.Forms.CheckBox();
            this.chkRemoveUnlisted = new System.Windows.Forms.CheckBox();
            this.tabCoinInfo = new System.Windows.Forms.TabPage();
            this.txtCointweakApiKey = new System.Windows.Forms.TextBox();
            this.txtCoinwarzApiKey = new System.Windows.Forms.TextBox();
            this.chkWhattomine = new System.Windows.Forms.CheckBox();
            this.chkCoinwarz = new System.Windows.Forms.CheckBox();
            this.chkCointweak = new System.Windows.Forms.CheckBox();
            this.tabMarketApi = new System.Windows.Forms.TabPage();
            this.chkCCex = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.chkCryptoine = new System.Windows.Forms.CheckBox();
            this.chkAtomictrade = new System.Windows.Forms.CheckBox();
            this.chkComkort = new System.Windows.Forms.CheckBox();
            this.chkAllcrypt = new System.Windows.Forms.CheckBox();
            this.chkAllcoin = new System.Windows.Forms.CheckBox();
            this.chkPoloniex = new System.Windows.Forms.CheckBox();
            this.chkCryptsy = new System.Windows.Forms.CheckBox();
            this.chkMintpal = new System.Windows.Forms.CheckBox();
            this.chkBittrex = new System.Windows.Forms.CheckBox();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabHashrates = new System.Windows.Forms.TabPage();
            this.chkAllHashrates = new System.Windows.Forms.CheckBox();
            this.dgvCustomAlgos = new System.Windows.Forms.DataGridView();
            this.tabCustomCoins = new System.Windows.Forms.TabPage();
            this.dgvCustomCoins = new System.Windows.Forms.DataGridView();
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.chkRemoveFrozenCoins = new System.Windows.Forms.CheckBox();
            this.tabReadme = new System.Windows.Forms.TabPage();
            this.txtReadme = new System.Windows.Forms.TextBox();
            this.chkAllCustomCoins = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.ctxtStrip.SuspendLayout();
            this.stStatusStrip.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
            this.tabMultipool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCryptoday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoolpicker)).BeginInit();
            this.tabPriceCalc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.tabCoinInfo.SuspendLayout();
            this.tabMarketApi.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabHashrates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomAlgos)).BeginInit();
            this.tabCustomCoins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomCoins)).BeginInit();
            this.tabFilters.SuspendLayout();
            this.tabReadme.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddDeleteProfile
            // 
            this.btnAddDeleteProfile.Enabled = false;
            this.btnAddDeleteProfile.Location = new System.Drawing.Point(105, 199);
            this.btnAddDeleteProfile.Name = "btnAddDeleteProfile";
            this.btnAddDeleteProfile.Size = new System.Drawing.Size(87, 23);
            this.btnAddDeleteProfile.TabIndex = 80;
            this.btnAddDeleteProfile.Text = "Remove profile";
            this.btnAddDeleteProfile.UseVisualStyleBackColor = true;
            this.btnAddDeleteProfile.Click += new System.EventHandler(this.btnAddDeleteProfile_Click);
            // 
            // cbbProfiles
            // 
            this.cbbProfiles.FormattingEnabled = true;
            this.cbbProfiles.Location = new System.Drawing.Point(12, 200);
            this.cbbProfiles.Name = "cbbProfiles";
            this.cbbProfiles.Size = new System.Drawing.Size(87, 21);
            this.cbbProfiles.TabIndex = 79;
            this.cbbProfiles.SelectedIndexChanged += new System.EventHandler(this.cbbProfiles_SelectedIndexChanged);
            this.cbbProfiles.TextChanged += new System.EventHandler(this.cbbProfiles_TextChanged);
            // 
            // btnCalc
            // 
            this.btnCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalc.Location = new System.Drawing.Point(521, 199);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(80, 23);
            this.btnCalc.TabIndex = 26;
            this.btnCalc.Text = "Calculate";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // cbbFiat
            // 
            this.cbbFiat.AllowDrop = true;
            this.cbbFiat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFiat.FormattingEnabled = true;
            this.cbbFiat.Items.AddRange(new object[] {
            "USD",
            "EUR",
            "GBP",
            "CNY",
            "ALL"});
            this.cbbFiat.Location = new System.Drawing.Point(49, 56);
            this.cbbFiat.Name = "cbbFiat";
            this.cbbFiat.Size = new System.Drawing.Size(48, 21);
            this.cbbFiat.TabIndex = 35;
            this.cbbFiat.SelectedIndexChanged += new System.EventHandler(this.cbbFiat_SelectedIndexChanged);
            // 
            // lblElectricityCost
            // 
            this.lblElectricityCost.AutoSize = true;
            this.lblElectricityCost.Location = new System.Drawing.Point(4, 86);
            this.lblElectricityCost.Name = "lblElectricityCost";
            this.lblElectricityCost.Size = new System.Drawing.Size(58, 13);
            this.lblElectricityCost.TabIndex = 68;
            this.lblElectricityCost.Text = "USD/kWh";
            // 
            // txtFiatElectricityCost
            // 
            this.txtFiatElectricityCost.Location = new System.Drawing.Point(68, 83);
            this.txtFiatElectricityCost.Name = "txtFiatElectricityCost";
            this.txtFiatElectricityCost.Size = new System.Drawing.Size(50, 20);
            this.txtFiatElectricityCost.TabIndex = 67;
            this.txtFiatElectricityCost.Text = "0.1";
            this.txtFiatElectricityCost.TextChanged += new System.EventHandler(this.txtFiatElectricityCost_TextChanged);
            // 
            // chkCoindesk
            // 
            this.chkCoindesk.AutoSize = true;
            this.chkCoindesk.Checked = true;
            this.chkCoindesk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCoindesk.Location = new System.Drawing.Point(6, 58);
            this.chkCoindesk.Name = "chkCoindesk";
            this.chkCoindesk.Size = new System.Drawing.Size(126, 17);
            this.chkCoindesk.TabIndex = 39;
            this.chkCoindesk.Text = "Calc                  /day.";
            this.chkCoindesk.UseVisualStyleBackColor = true;
            this.chkCoindesk.CheckedChanged += new System.EventHandler(this.chkCoindesk_CheckedChanged);
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tag,
            this.CoinName,
            this.Algo,
            this.UsdPerDay,
            this.EurPerDay,
            this.GbpPerDay,
            this.CnyPerDay,
            this.BtcPerDay,
            this.CoinsPerDay,
            this.BestExchange,
            this.BestExchangePrice,
            this.ExchangeVolume,
            this.WeightedPrice,
            this.TotalVolume,
            this.Difficulty,
            this.BlockReward});
            this.dgView.ContextMenuStrip = this.ctxtStrip;
            this.dgView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgView.Location = new System.Drawing.Point(0, 228);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.Size = new System.Drawing.Size(614, 375);
            this.dgView.TabIndex = 1;
            // 
            // Tag
            // 
            this.Tag.HeaderText = "Tag";
            this.Tag.Name = "Tag";
            this.Tag.ReadOnly = true;
            this.Tag.Width = 51;
            // 
            // CoinName
            // 
            this.CoinName.HeaderText = "Name";
            this.CoinName.Name = "CoinName";
            this.CoinName.ReadOnly = true;
            this.CoinName.Width = 60;
            // 
            // Algo
            // 
            this.Algo.HeaderText = "Algo";
            this.Algo.Name = "Algo";
            this.Algo.ReadOnly = true;
            this.Algo.Width = 53;
            // 
            // UsdPerDay
            // 
            this.UsdPerDay.HeaderText = "USD per day";
            this.UsdPerDay.Name = "UsdPerDay";
            this.UsdPerDay.ReadOnly = true;
            this.UsdPerDay.Width = 70;
            // 
            // EurPerDay
            // 
            this.EurPerDay.HeaderText = "EUR per day";
            this.EurPerDay.Name = "EurPerDay";
            this.EurPerDay.ReadOnly = true;
            this.EurPerDay.Visible = false;
            this.EurPerDay.Width = 93;
            // 
            // GbpPerDay
            // 
            this.GbpPerDay.HeaderText = "GBP per day";
            this.GbpPerDay.Name = "GbpPerDay";
            this.GbpPerDay.ReadOnly = true;
            this.GbpPerDay.Visible = false;
            this.GbpPerDay.Width = 92;
            // 
            // CnyPerDay
            // 
            this.CnyPerDay.HeaderText = "CNY per day";
            this.CnyPerDay.Name = "CnyPerDay";
            this.CnyPerDay.ReadOnly = true;
            this.CnyPerDay.Visible = false;
            this.CnyPerDay.Width = 92;
            // 
            // BtcPerDay
            // 
            this.BtcPerDay.HeaderText = "BTC per day";
            this.BtcPerDay.Name = "BtcPerDay";
            this.BtcPerDay.ReadOnly = true;
            this.BtcPerDay.Width = 69;
            // 
            // CoinsPerDay
            // 
            this.CoinsPerDay.HeaderText = "Coins per day";
            this.CoinsPerDay.Name = "CoinsPerDay";
            this.CoinsPerDay.ReadOnly = true;
            this.CoinsPerDay.Width = 73;
            // 
            // BestExchange
            // 
            this.BestExchange.HeaderText = "Best exchange";
            this.BestExchange.Name = "BestExchange";
            this.BestExchange.ReadOnly = true;
            this.BestExchange.Width = 95;
            // 
            // BestExchangePrice
            // 
            this.BestExchangePrice.HeaderText = "Best exchange\'s price";
            this.BestExchangePrice.Name = "BestExchangePrice";
            this.BestExchangePrice.ReadOnly = true;
            this.BestExchangePrice.Width = 124;
            // 
            // ExchangeVolume
            // 
            this.ExchangeVolume.HeaderText = "Best exchange\'s volume";
            this.ExchangeVolume.Name = "ExchangeVolume";
            this.ExchangeVolume.ReadOnly = true;
            this.ExchangeVolume.Width = 134;
            // 
            // WeightedPrice
            // 
            this.WeightedPrice.HeaderText = "Weighted price";
            this.WeightedPrice.Name = "WeightedPrice";
            this.WeightedPrice.ReadOnly = true;
            this.WeightedPrice.Width = 96;
            // 
            // TotalVolume
            // 
            this.TotalVolume.HeaderText = "Total Volume";
            this.TotalVolume.Name = "TotalVolume";
            this.TotalVolume.ReadOnly = true;
            this.TotalVolume.Width = 87;
            // 
            // Difficulty
            // 
            this.Difficulty.HeaderText = "Difficulty";
            this.Difficulty.Name = "Difficulty";
            this.Difficulty.ReadOnly = true;
            this.Difficulty.Width = 72;
            // 
            // BlockReward
            // 
            this.BlockReward.HeaderText = "Block reward";
            this.BlockReward.Name = "BlockReward";
            this.BlockReward.ReadOnly = true;
            this.BlockReward.Width = 87;
            // 
            // ctxtStrip
            // 
            this.ctxtStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmResultsToClipboard,
            this.tsmHashratesToClipboard});
            this.ctxtStrip.Name = "ctxtStrip";
            this.ctxtStrip.Size = new System.Drawing.Size(156, 48);
            // 
            // tsmResultsToClipboard
            // 
            this.tsmResultsToClipboard.Name = "tsmResultsToClipboard";
            this.tsmResultsToClipboard.Size = new System.Drawing.Size(155, 22);
            this.tsmResultsToClipboard.Text = "Copy all results";
            this.tsmResultsToClipboard.Click += new System.EventHandler(this.tsmResultsToClipboard_Click);
            // 
            // tsmHashratesToClipboard
            // 
            this.tsmHashratesToClipboard.Name = "tsmHashratesToClipboard";
            this.tsmHashratesToClipboard.Size = new System.Drawing.Size(155, 22);
            this.tsmHashratesToClipboard.Text = "Copy hashrates";
            this.tsmHashratesToClipboard.Click += new System.EventHandler(this.tsmHashratesToClipboard_Click);
            // 
            // stStatusStrip
            // 
            this.stStatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.stStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressText,
            this.tsProgress,
            this.tsStatus,
            this.tsSpace,
            this.tsErrors});
            this.stStatusStrip.Location = new System.Drawing.Point(0, 604);
            this.stStatusStrip.Name = "stStatusStrip";
            this.stStatusStrip.Size = new System.Drawing.Size(613, 22);
            this.stStatusStrip.TabIndex = 3;
            this.stStatusStrip.Text = "Status Striper";
            // 
            // tsProgressText
            // 
            this.tsProgressText.Name = "tsProgressText";
            this.tsProgressText.Size = new System.Drawing.Size(55, 17);
            this.tsProgressText.Text = "Progress:";
            // 
            // tsProgress
            // 
            this.tsProgress.Name = "tsProgress";
            this.tsProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // tsStatus
            // 
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(96, 17);
            this.tsStatus.Text = "Ready for launch";
            this.tsStatus.TextChanged += new System.EventHandler(this.tsStatus_TextChanged);
            // 
            // tsSpace
            // 
            this.tsSpace.Name = "tsSpace";
            this.tsSpace.Size = new System.Drawing.Size(345, 17);
            this.tsSpace.Spring = true;
            // 
            // tsErrors
            // 
            this.tsErrors.Name = "tsErrors";
            this.tsErrors.Size = new System.Drawing.Size(0, 17);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(606, 168);
            this.tabLog.TabIndex = 5;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(600, 162);
            this.txtLog.TabIndex = 0;
            this.txtLog.WordWrap = false;
            // 
            // tabMisc
            // 
            this.tabMisc.Controls.Add(this.lblTimeout);
            this.tabMisc.Controls.Add(this.nudTimeout);
            this.tabMisc.Controls.Add(this.txtProxy);
            this.tabMisc.Controls.Add(this.chkProxy);
            this.tabMisc.Location = new System.Drawing.Point(4, 22);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.Size = new System.Drawing.Size(606, 168);
            this.tabMisc.TabIndex = 3;
            this.tabMisc.Text = "Misc Settings";
            this.tabMisc.UseVisualStyleBackColor = true;
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(6, 32);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(45, 13);
            this.lblTimeout.TabIndex = 35;
            this.lblTimeout.Text = "Timeout";
            // 
            // nudTimeout
            // 
            this.nudTimeout.Location = new System.Drawing.Point(57, 30);
            this.nudTimeout.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(58, 20);
            this.nudTimeout.TabIndex = 34;
            this.nudTimeout.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // txtProxy
            // 
            this.txtProxy.Enabled = false;
            this.txtProxy.Location = new System.Drawing.Point(64, 4);
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.Size = new System.Drawing.Size(282, 20);
            this.txtProxy.TabIndex = 32;
            // 
            // chkProxy
            // 
            this.chkProxy.AutoSize = true;
            this.chkProxy.Location = new System.Drawing.Point(6, 6);
            this.chkProxy.Name = "chkProxy";
            this.chkProxy.Size = new System.Drawing.Size(52, 17);
            this.chkProxy.TabIndex = 31;
            this.chkProxy.Text = "Proxy";
            this.chkProxy.UseVisualStyleBackColor = true;
            this.chkProxy.CheckedChanged += new System.EventHandler(this.chkProxy_CheckedChanged);
            // 
            // chkColor
            // 
            this.chkColor.AutoSize = true;
            this.chkColor.Checked = true;
            this.chkColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColor.Location = new System.Drawing.Point(6, 98);
            this.chkColor.Name = "chkColor";
            this.chkColor.Size = new System.Drawing.Size(118, 17);
            this.chkColor.TabIndex = 33;
            this.chkColor.Text = "Use a colored table";
            this.chkColor.UseVisualStyleBackColor = true;
            this.chkColor.CheckedChanged += new System.EventHandler(this.chkColor_CheckStateChanged);
            this.chkColor.CheckStateChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // tabMultipool
            // 
            this.tabMultipool.Controls.Add(this.nudCryptoday);
            this.tabMultipool.Controls.Add(this.chkCryptoday);
            this.tabMultipool.Controls.Add(this.chkReviewCalc);
            this.tabMultipool.Controls.Add(this.chkNiceHash);
            this.tabMultipool.Controls.Add(this.nudPoolpicker);
            this.tabMultipool.Controls.Add(this.chkPoolpicker);
            this.tabMultipool.Location = new System.Drawing.Point(4, 22);
            this.tabMultipool.Name = "tabMultipool";
            this.tabMultipool.Padding = new System.Windows.Forms.Padding(3);
            this.tabMultipool.Size = new System.Drawing.Size(606, 168);
            this.tabMultipool.TabIndex = 2;
            this.tabMultipool.Text = "Multipools";
            this.tabMultipool.UseVisualStyleBackColor = true;
            // 
            // nudCryptoday
            // 
            this.nudCryptoday.Location = new System.Drawing.Point(178, 27);
            this.nudCryptoday.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCryptoday.Name = "nudCryptoday";
            this.nudCryptoday.Size = new System.Drawing.Size(38, 20);
            this.nudCryptoday.TabIndex = 36;
            this.nudCryptoday.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // chkCryptoday
            // 
            this.chkCryptoday.AutoSize = true;
            this.chkCryptoday.Checked = true;
            this.chkCryptoday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCryptoday.Location = new System.Drawing.Point(6, 29);
            this.chkCryptoday.Name = "chkCryptoday";
            this.chkCryptoday.Size = new System.Drawing.Size(246, 17);
            this.chkCryptoday.TabIndex = 37;
            this.chkCryptoday.Text = "Use CrypToday, averaging over               days.";
            this.chkCryptoday.UseVisualStyleBackColor = true;
            this.chkCryptoday.CheckedChanged += new System.EventHandler(this.chkCryptoday_CheckedChanged);
            // 
            // chkReviewCalc
            // 
            this.chkReviewCalc.AutoSize = true;
            this.chkReviewCalc.Checked = true;
            this.chkReviewCalc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReviewCalc.Location = new System.Drawing.Point(6, 75);
            this.chkReviewCalc.Name = "chkReviewCalc";
            this.chkReviewCalc.Size = new System.Drawing.Size(221, 17);
            this.chkReviewCalc.TabIndex = 35;
            this.chkReviewCalc.Text = "Calculate PP reviews into multipool prices";
            this.chkReviewCalc.UseVisualStyleBackColor = true;
            // 
            // chkNiceHash
            // 
            this.chkNiceHash.AutoSize = true;
            this.chkNiceHash.Checked = true;
            this.chkNiceHash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNiceHash.Location = new System.Drawing.Point(6, 6);
            this.chkNiceHash.Name = "chkNiceHash";
            this.chkNiceHash.Size = new System.Drawing.Size(156, 17);
            this.chkNiceHash.TabIndex = 31;
            this.chkNiceHash.Text = "Get actual NiceHash prices";
            this.chkNiceHash.UseVisualStyleBackColor = true;
            // 
            // nudPoolpicker
            // 
            this.nudPoolpicker.Location = new System.Drawing.Point(178, 50);
            this.nudPoolpicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPoolpicker.Name = "nudPoolpicker";
            this.nudPoolpicker.Size = new System.Drawing.Size(38, 20);
            this.nudPoolpicker.TabIndex = 33;
            this.nudPoolpicker.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // chkPoolpicker
            // 
            this.chkPoolpicker.AutoSize = true;
            this.chkPoolpicker.Checked = true;
            this.chkPoolpicker.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPoolpicker.Location = new System.Drawing.Point(6, 52);
            this.chkPoolpicker.Name = "chkPoolpicker";
            this.chkPoolpicker.Size = new System.Drawing.Size(246, 17);
            this.chkPoolpicker.TabIndex = 33;
            this.chkPoolpicker.Text = "Use PoolPicker, averaging over               days.";
            this.chkPoolpicker.UseVisualStyleBackColor = true;
            this.chkPoolpicker.CheckedChanged += new System.EventHandler(this.chkPoolpicker_CheckedChanged);
            // 
            // tabPriceCalc
            // 
            this.tabPriceCalc.Controls.Add(this.cbbBidRecentAsk);
            this.tabPriceCalc.Controls.Add(this.lblBidRecentAsk);
            this.tabPriceCalc.Controls.Add(this.nudAmount);
            this.tabPriceCalc.Controls.Add(this.chkWeight);
            this.tabPriceCalc.Controls.Add(this.cbbFiat);
            this.tabPriceCalc.Controls.Add(this.lblAmountOfGpu);
            this.tabPriceCalc.Controls.Add(this.chkCoindesk);
            this.tabPriceCalc.Controls.Add(this.txtFiatElectricityCost);
            this.tabPriceCalc.Controls.Add(this.lblElectricityCost);
            this.tabPriceCalc.Location = new System.Drawing.Point(4, 22);
            this.tabPriceCalc.Name = "tabPriceCalc";
            this.tabPriceCalc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPriceCalc.Size = new System.Drawing.Size(606, 168);
            this.tabPriceCalc.TabIndex = 4;
            this.tabPriceCalc.Text = "Coin Price Calc";
            this.tabPriceCalc.UseVisualStyleBackColor = true;
            // 
            // cbbBidRecentAsk
            // 
            this.cbbBidRecentAsk.FormattingEnabled = true;
            this.cbbBidRecentAsk.Items.AddRange(new object[] {
            " highest bid",
            " recent trade",
            " lowest ask"});
            this.cbbBidRecentAsk.Location = new System.Drawing.Point(31, 29);
            this.cbbBidRecentAsk.Name = "cbbBidRecentAsk";
            this.cbbBidRecentAsk.Size = new System.Drawing.Size(87, 21);
            this.cbbBidRecentAsk.TabIndex = 39;
            // 
            // lblBidRecentAsk
            // 
            this.lblBidRecentAsk.AutoSize = true;
            this.lblBidRecentAsk.Location = new System.Drawing.Point(4, 32);
            this.lblBidRecentAsk.Name = "lblBidRecentAsk";
            this.lblBidRecentAsk.Size = new System.Drawing.Size(235, 13);
            this.lblBidRecentAsk.TabIndex = 38;
            this.lblBidRecentAsk.Text = "Use                                 price for all calculations";
            // 
            // nudAmount
            // 
            this.nudAmount.Location = new System.Drawing.Point(205, 109);
            this.nudAmount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(50, 20);
            this.nudAmount.TabIndex = 31;
            this.nudAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAmount.ValueChanged += new System.EventHandler(this.nudAmount_ValueChanged);
            // 
            // chkWeight
            // 
            this.chkWeight.AutoSize = true;
            this.chkWeight.Checked = true;
            this.chkWeight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWeight.Location = new System.Drawing.Point(6, 6);
            this.chkWeight.Name = "chkWeight";
            this.chkWeight.Size = new System.Drawing.Size(237, 17);
            this.chkWeight.TabIndex = 37;
            this.chkWeight.Text = "Use the weighted price across all exchanges";
            this.chkWeight.UseVisualStyleBackColor = true;
            // 
            // lblAmountOfGpu
            // 
            this.lblAmountOfGpu.AutoSize = true;
            this.lblAmountOfGpu.Location = new System.Drawing.Point(3, 111);
            this.lblAmountOfGpu.Name = "lblAmountOfGpu";
            this.lblAmountOfGpu.Size = new System.Drawing.Size(196, 13);
            this.lblAmountOfGpu.TabIndex = 32;
            this.lblAmountOfGpu.Text = "Amount of GPUs, multiplier for hashrates";
            // 
            // chkRemoveTooGoodToBeTrue
            // 
            this.chkRemoveTooGoodToBeTrue.AutoSize = true;
            this.chkRemoveTooGoodToBeTrue.Checked = true;
            this.chkRemoveTooGoodToBeTrue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveTooGoodToBeTrue.Location = new System.Drawing.Point(6, 52);
            this.chkRemoveTooGoodToBeTrue.Name = "chkRemoveTooGoodToBeTrue";
            this.chkRemoveTooGoodToBeTrue.Size = new System.Drawing.Size(287, 17);
            this.chkRemoveTooGoodToBeTrue.TabIndex = 41;
            this.chkRemoveTooGoodToBeTrue.Text = "Remove coins with less daily volume than you can earn";
            this.chkRemoveTooGoodToBeTrue.UseVisualStyleBackColor = true;
            this.chkRemoveTooGoodToBeTrue.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // chkRemoveNegative
            // 
            this.chkRemoveNegative.AutoSize = true;
            this.chkRemoveNegative.Location = new System.Drawing.Point(6, 75);
            this.chkRemoveNegative.Name = "chkRemoveNegative";
            this.chkRemoveNegative.Size = new System.Drawing.Size(200, 17);
            this.chkRemoveNegative.TabIndex = 40;
            this.chkRemoveNegative.Text = "Remove results with a negative price";
            this.chkRemoveNegative.UseVisualStyleBackColor = true;
            this.chkRemoveNegative.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // chkRemoveUnlisted
            // 
            this.chkRemoveUnlisted.AutoSize = true;
            this.chkRemoveUnlisted.BackColor = System.Drawing.Color.Transparent;
            this.chkRemoveUnlisted.Checked = true;
            this.chkRemoveUnlisted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveUnlisted.Location = new System.Drawing.Point(6, 6);
            this.chkRemoveUnlisted.Name = "chkRemoveUnlisted";
            this.chkRemoveUnlisted.Size = new System.Drawing.Size(291, 17);
            this.chkRemoveUnlisted.TabIndex = 36;
            this.chkRemoveUnlisted.Text = "Remove coins that aren\'t listed on supported exchanges";
            this.chkRemoveUnlisted.UseVisualStyleBackColor = false;
            this.chkRemoveUnlisted.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // tabCoinInfo
            // 
            this.tabCoinInfo.Controls.Add(this.txtCointweakApiKey);
            this.tabCoinInfo.Controls.Add(this.txtCoinwarzApiKey);
            this.tabCoinInfo.Controls.Add(this.chkWhattomine);
            this.tabCoinInfo.Controls.Add(this.chkCoinwarz);
            this.tabCoinInfo.Controls.Add(this.chkCointweak);
            this.tabCoinInfo.Location = new System.Drawing.Point(4, 22);
            this.tabCoinInfo.Name = "tabCoinInfo";
            this.tabCoinInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabCoinInfo.Size = new System.Drawing.Size(606, 168);
            this.tabCoinInfo.TabIndex = 1;
            this.tabCoinInfo.Text = "Coin Info";
            this.tabCoinInfo.UseVisualStyleBackColor = true;
            // 
            // txtCointweakApiKey
            // 
            this.txtCointweakApiKey.Enabled = false;
            this.txtCointweakApiKey.Location = new System.Drawing.Point(114, 30);
            this.txtCointweakApiKey.Name = "txtCointweakApiKey";
            this.txtCointweakApiKey.Size = new System.Drawing.Size(371, 20);
            this.txtCointweakApiKey.TabIndex = 30;
            this.txtCointweakApiKey.Text = "Enter your CoinTweak API key here.";
            // 
            // txtCoinwarzApiKey
            // 
            this.txtCoinwarzApiKey.Enabled = false;
            this.txtCoinwarzApiKey.Location = new System.Drawing.Point(114, 51);
            this.txtCoinwarzApiKey.Name = "txtCoinwarzApiKey";
            this.txtCoinwarzApiKey.Size = new System.Drawing.Size(371, 20);
            this.txtCoinwarzApiKey.TabIndex = 27;
            this.txtCoinwarzApiKey.Text = "Enter your CoinWarz API key here.";
            // 
            // chkWhattomine
            // 
            this.chkWhattomine.AutoSize = true;
            this.chkWhattomine.Checked = true;
            this.chkWhattomine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWhattomine.Location = new System.Drawing.Point(6, 6);
            this.chkWhattomine.Name = "chkWhattomine";
            this.chkWhattomine.Size = new System.Drawing.Size(110, 17);
            this.chkWhattomine.TabIndex = 3;
            this.chkWhattomine.Text = "Use WhatToMine";
            this.chkWhattomine.UseVisualStyleBackColor = true;
            // 
            // chkCoinwarz
            // 
            this.chkCoinwarz.AutoSize = true;
            this.chkCoinwarz.Location = new System.Drawing.Point(6, 52);
            this.chkCoinwarz.Name = "chkCoinwarz";
            this.chkCoinwarz.Size = new System.Drawing.Size(94, 17);
            this.chkCoinwarz.TabIndex = 29;
            this.chkCoinwarz.Text = "Use CoinWarz";
            this.chkCoinwarz.UseVisualStyleBackColor = true;
            this.chkCoinwarz.CheckedChanged += new System.EventHandler(this.chkCoinwarz_CheckedChanged);
            // 
            // chkCointweak
            // 
            this.chkCointweak.AutoSize = true;
            this.chkCointweak.Location = new System.Drawing.Point(6, 29);
            this.chkCointweak.Name = "chkCointweak";
            this.chkCointweak.Size = new System.Drawing.Size(102, 17);
            this.chkCointweak.TabIndex = 28;
            this.chkCointweak.Text = "Use CoinTweak";
            this.chkCointweak.UseVisualStyleBackColor = true;
            this.chkCointweak.CheckedChanged += new System.EventHandler(this.chkCointweak_CheckedChanged);
            // 
            // tabMarketApi
            // 
            this.tabMarketApi.Controls.Add(this.chkCCex);
            this.tabMarketApi.Controls.Add(this.checkBox2);
            this.tabMarketApi.Controls.Add(this.chkCryptoine);
            this.tabMarketApi.Controls.Add(this.chkAtomictrade);
            this.tabMarketApi.Controls.Add(this.chkComkort);
            this.tabMarketApi.Controls.Add(this.chkAllcrypt);
            this.tabMarketApi.Controls.Add(this.chkAllcoin);
            this.tabMarketApi.Controls.Add(this.chkPoloniex);
            this.tabMarketApi.Controls.Add(this.chkCryptsy);
            this.tabMarketApi.Controls.Add(this.chkMintpal);
            this.tabMarketApi.Controls.Add(this.chkBittrex);
            this.tabMarketApi.Location = new System.Drawing.Point(4, 22);
            this.tabMarketApi.Name = "tabMarketApi";
            this.tabMarketApi.Padding = new System.Windows.Forms.Padding(3);
            this.tabMarketApi.Size = new System.Drawing.Size(606, 168);
            this.tabMarketApi.TabIndex = 0;
            this.tabMarketApi.Text = "Market API";
            this.tabMarketApi.UseVisualStyleBackColor = true;
            // 
            // chkCCex
            // 
            this.chkCCex.AutoSize = true;
            this.chkCCex.Location = new System.Drawing.Point(6, 98);
            this.chkCCex.Name = "chkCCex";
            this.chkCCex.Size = new System.Drawing.Size(54, 17);
            this.chkCCex.TabIndex = 46;
            this.chkCCex.Text = "C-Cex";
            this.chkCCex.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(272, 121);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(61, 17);
            this.checkBox2.TabIndex = 45;
            this.checkBox2.Text = "Alcurex";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // chkCryptoine
            // 
            this.chkCryptoine.AutoSize = true;
            this.chkCryptoine.Location = new System.Drawing.Point(101, 75);
            this.chkCryptoine.Name = "chkCryptoine";
            this.chkCryptoine.Size = new System.Drawing.Size(70, 17);
            this.chkCryptoine.TabIndex = 44;
            this.chkCryptoine.Text = "Cryptoine";
            this.chkCryptoine.UseVisualStyleBackColor = true;
            // 
            // chkAtomictrade
            // 
            this.chkAtomictrade.AutoSize = true;
            this.chkAtomictrade.Location = new System.Drawing.Point(177, 121);
            this.chkAtomictrade.Name = "chkAtomictrade";
            this.chkAtomictrade.Size = new System.Drawing.Size(89, 17);
            this.chkAtomictrade.TabIndex = 43;
            this.chkAtomictrade.Text = "Atomic Trade";
            this.chkAtomictrade.UseVisualStyleBackColor = true;
            this.chkAtomictrade.Visible = false;
            // 
            // chkComkort
            // 
            this.chkComkort.AutoSize = true;
            this.chkComkort.Location = new System.Drawing.Point(101, 52);
            this.chkComkort.Name = "chkComkort";
            this.chkComkort.Size = new System.Drawing.Size(65, 17);
            this.chkComkort.TabIndex = 42;
            this.chkComkort.Text = "Comkort";
            this.chkComkort.UseVisualStyleBackColor = true;
            // 
            // chkAllcrypt
            // 
            this.chkAllcrypt.AutoSize = true;
            this.chkAllcrypt.Location = new System.Drawing.Point(101, 29);
            this.chkAllcrypt.Name = "chkAllcrypt";
            this.chkAllcrypt.Size = new System.Drawing.Size(61, 17);
            this.chkAllcrypt.TabIndex = 41;
            this.chkAllcrypt.Text = "AllCrypt";
            this.chkAllcrypt.UseVisualStyleBackColor = true;
            // 
            // chkAllcoin
            // 
            this.chkAllcoin.AutoSize = true;
            this.chkAllcoin.Location = new System.Drawing.Point(101, 6);
            this.chkAllcoin.Name = "chkAllcoin";
            this.chkAllcoin.Size = new System.Drawing.Size(58, 17);
            this.chkAllcoin.TabIndex = 40;
            this.chkAllcoin.Text = "AllCoin";
            this.chkAllcoin.UseVisualStyleBackColor = true;
            // 
            // chkPoloniex
            // 
            this.chkPoloniex.AutoSize = true;
            this.chkPoloniex.Checked = true;
            this.chkPoloniex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPoloniex.Location = new System.Drawing.Point(6, 52);
            this.chkPoloniex.Name = "chkPoloniex";
            this.chkPoloniex.Size = new System.Drawing.Size(66, 17);
            this.chkPoloniex.TabIndex = 39;
            this.chkPoloniex.Text = "Poloniex";
            this.chkPoloniex.UseVisualStyleBackColor = true;
            // 
            // chkCryptsy
            // 
            this.chkCryptsy.AutoSize = true;
            this.chkCryptsy.Location = new System.Drawing.Point(6, 75);
            this.chkCryptsy.Name = "chkCryptsy";
            this.chkCryptsy.Size = new System.Drawing.Size(60, 17);
            this.chkCryptsy.TabIndex = 38;
            this.chkCryptsy.Text = "Cryptsy";
            this.chkCryptsy.UseVisualStyleBackColor = true;
            // 
            // chkMintpal
            // 
            this.chkMintpal.AutoSize = true;
            this.chkMintpal.Checked = true;
            this.chkMintpal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMintpal.Location = new System.Drawing.Point(6, 29);
            this.chkMintpal.Name = "chkMintpal";
            this.chkMintpal.Size = new System.Drawing.Size(60, 17);
            this.chkMintpal.TabIndex = 37;
            this.chkMintpal.Text = "Mintpal";
            this.chkMintpal.UseVisualStyleBackColor = true;
            // 
            // chkBittrex
            // 
            this.chkBittrex.AutoSize = true;
            this.chkBittrex.Checked = true;
            this.chkBittrex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBittrex.Location = new System.Drawing.Point(6, 6);
            this.chkBittrex.Name = "chkBittrex";
            this.chkBittrex.Size = new System.Drawing.Size(55, 17);
            this.chkBittrex.TabIndex = 36;
            this.chkBittrex.Text = "Bittrex";
            this.chkBittrex.UseVisualStyleBackColor = true;
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSettings.Controls.Add(this.tabHashrates);
            this.tabControlSettings.Controls.Add(this.tabMarketApi);
            this.tabControlSettings.Controls.Add(this.tabCoinInfo);
            this.tabControlSettings.Controls.Add(this.tabCustomCoins);
            this.tabControlSettings.Controls.Add(this.tabMultipool);
            this.tabControlSettings.Controls.Add(this.tabPriceCalc);
            this.tabControlSettings.Controls.Add(this.tabFilters);
            this.tabControlSettings.Controls.Add(this.tabMisc);
            this.tabControlSettings.Controls.Add(this.tabLog);
            this.tabControlSettings.Controls.Add(this.tabReadme);
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(614, 194);
            this.tabControlSettings.TabIndex = 6;
            this.tabControlSettings.TabStop = false;
            // 
            // tabHashrates
            // 
            this.tabHashrates.Controls.Add(this.chkAllHashrates);
            this.tabHashrates.Controls.Add(this.dgvCustomAlgos);
            this.tabHashrates.Location = new System.Drawing.Point(4, 22);
            this.tabHashrates.Name = "tabHashrates";
            this.tabHashrates.Size = new System.Drawing.Size(606, 168);
            this.tabHashrates.TabIndex = 9;
            this.tabHashrates.Text = "Hashrates";
            this.tabHashrates.UseVisualStyleBackColor = true;
            // 
            // chkAllHashrates
            // 
            this.chkAllHashrates.AutoSize = true;
            this.chkAllHashrates.Checked = true;
            this.chkAllHashrates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllHashrates.Location = new System.Drawing.Point(45, 6);
            this.chkAllHashrates.Name = "chkAllHashrates";
            this.chkAllHashrates.Size = new System.Drawing.Size(15, 14);
            this.chkAllHashrates.TabIndex = 2;
            this.chkAllHashrates.UseVisualStyleBackColor = true;
            this.chkAllHashrates.CheckedChanged += new System.EventHandler(this.chkAllHashrates_CheckedChanged);
            // 
            // dgvCustomAlgos
            // 
            this.dgvCustomAlgos.AllowUserToOrderColumns = true;
            this.dgvCustomAlgos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCustomAlgos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomAlgos.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomAlgos.Name = "dgvCustomAlgos";
            this.dgvCustomAlgos.Size = new System.Drawing.Size(606, 168);
            this.dgvCustomAlgos.TabIndex = 1;
            this.dgvCustomAlgos.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvCustomAlgos_DefaultValuesNeeded);
            this.dgvCustomAlgos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCustomAlgos_EditingControlShowing);
            this.dgvCustomAlgos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvCustomAlgos_MouseUp);
            this.dgvCustomAlgos.Validated += new System.EventHandler(this.dgvCustomAlgos_Validated);
            // 
            // tabCustomCoins
            // 
            this.tabCustomCoins.Controls.Add(this.chkAllCustomCoins);
            this.tabCustomCoins.Controls.Add(this.dgvCustomCoins);
            this.tabCustomCoins.Location = new System.Drawing.Point(4, 22);
            this.tabCustomCoins.Name = "tabCustomCoins";
            this.tabCustomCoins.Size = new System.Drawing.Size(606, 168);
            this.tabCustomCoins.TabIndex = 8;
            this.tabCustomCoins.Text = "Custom Coins";
            this.tabCustomCoins.UseVisualStyleBackColor = true;
            // 
            // dgvCustomCoins
            // 
            this.dgvCustomCoins.AllowUserToOrderColumns = true;
            this.dgvCustomCoins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = "0";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomCoins.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCustomCoins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomCoins.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomCoins.Name = "dgvCustomCoins";
            this.dgvCustomCoins.Size = new System.Drawing.Size(606, 168);
            this.dgvCustomCoins.TabIndex = 0;
            this.dgvCustomCoins.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvCustomCoins_DefaultValuesNeeded);
            this.dgvCustomCoins.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCustomCoins_EditingControlShowing);
            this.dgvCustomCoins.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvCustomCoins_MouseUp);
            // 
            // tabFilters
            // 
            this.tabFilters.Controls.Add(this.chkRemoveFrozenCoins);
            this.tabFilters.Controls.Add(this.chkColor);
            this.tabFilters.Controls.Add(this.chkRemoveTooGoodToBeTrue);
            this.tabFilters.Controls.Add(this.chkRemoveUnlisted);
            this.tabFilters.Controls.Add(this.chkRemoveNegative);
            this.tabFilters.Location = new System.Drawing.Point(4, 22);
            this.tabFilters.Name = "tabFilters";
            this.tabFilters.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilters.Size = new System.Drawing.Size(606, 168);
            this.tabFilters.TabIndex = 6;
            this.tabFilters.Text = "Filters";
            this.tabFilters.UseVisualStyleBackColor = true;
            // 
            // chkRemoveFrozenCoins
            // 
            this.chkRemoveFrozenCoins.AutoSize = true;
            this.chkRemoveFrozenCoins.BackColor = System.Drawing.Color.Transparent;
            this.chkRemoveFrozenCoins.Checked = true;
            this.chkRemoveFrozenCoins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveFrozenCoins.Location = new System.Drawing.Point(6, 29);
            this.chkRemoveFrozenCoins.Name = "chkRemoveFrozenCoins";
            this.chkRemoveFrozenCoins.Size = new System.Drawing.Size(285, 17);
            this.chkRemoveFrozenCoins.TabIndex = 42;
            this.chkRemoveFrozenCoins.Text = "Remove coins that are frozen on supported exchanges";
            this.chkRemoveFrozenCoins.UseVisualStyleBackColor = false;
            this.chkRemoveFrozenCoins.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // tabReadme
            // 
            this.tabReadme.Controls.Add(this.txtReadme);
            this.tabReadme.Location = new System.Drawing.Point(4, 22);
            this.tabReadme.Name = "tabReadme";
            this.tabReadme.Padding = new System.Windows.Forms.Padding(3);
            this.tabReadme.Size = new System.Drawing.Size(606, 168);
            this.tabReadme.TabIndex = 7;
            this.tabReadme.Text = "Readme";
            this.tabReadme.UseVisualStyleBackColor = true;
            // 
            // txtReadme
            // 
            this.txtReadme.BackColor = System.Drawing.SystemColors.Menu;
            this.txtReadme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReadme.Location = new System.Drawing.Point(3, 3);
            this.txtReadme.Multiline = true;
            this.txtReadme.Name = "txtReadme";
            this.txtReadme.ReadOnly = true;
            this.txtReadme.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReadme.Size = new System.Drawing.Size(600, 162);
            this.txtReadme.TabIndex = 0;
            this.txtReadme.Text = "Seems like README.txt is missing :) But you can still donate @ 1MVBPhMaeuj5daZtaK" +
    "aVu8BZL5K44CCq7E ";
            // 
            // chkAllCustomCoins
            // 
            this.chkAllCustomCoins.AutoSize = true;
            this.chkAllCustomCoins.Checked = true;
            this.chkAllCustomCoins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllCustomCoins.Location = new System.Drawing.Point(45, 6);
            this.chkAllCustomCoins.Name = "chkAllCustomCoins";
            this.chkAllCustomCoins.Size = new System.Drawing.Size(15, 14);
            this.chkAllCustomCoins.TabIndex = 81;
            this.chkAllCustomCoins.UseVisualStyleBackColor = true;
            this.chkAllCustomCoins.CheckedChanged += new System.EventHandler(this.chkAllCustomCoins_CheckedChanged);
            // 
            // ProfitCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 626);
            this.Controls.Add(this.btnAddDeleteProfile);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.cbbProfiles);
            this.Controls.Add(this.stStatusStrip);
            this.Controls.Add(this.tabControlSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfitCalc";
            this.Text = "Profit Calculator~ By KBomba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CudaProfitCalc_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ctxtStrip.ResumeLayout(false);
            this.stStatusStrip.ResumeLayout(false);
            this.stStatusStrip.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabMisc.ResumeLayout(false);
            this.tabMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
            this.tabMultipool.ResumeLayout(false);
            this.tabMultipool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCryptoday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoolpicker)).EndInit();
            this.tabPriceCalc.ResumeLayout(false);
            this.tabPriceCalc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.tabCoinInfo.ResumeLayout(false);
            this.tabCoinInfo.PerformLayout();
            this.tabMarketApi.ResumeLayout(false);
            this.tabMarketApi.PerformLayout();
            this.tabControlSettings.ResumeLayout(false);
            this.tabHashrates.ResumeLayout(false);
            this.tabHashrates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomAlgos)).EndInit();
            this.tabCustomCoins.ResumeLayout(false);
            this.tabCustomCoins.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomCoins)).EndInit();
            this.tabFilters.ResumeLayout(false);
            this.tabFilters.PerformLayout();
            this.tabReadme.ResumeLayout(false);
            this.tabReadme.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.StatusStrip stStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar tsProgress;
        private System.Windows.Forms.ToolStripStatusLabel tsProgressText;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.ContextMenuStrip ctxtStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmResultsToClipboard;
        private System.Windows.Forms.ToolStripMenuItem tsmHashratesToClipboard;
        private System.Windows.Forms.CheckBox chkCoindesk;
        private System.Windows.Forms.ComboBox cbbFiat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tag;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoinName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Algo;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsdPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn EurPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn GbpPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn CnyPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn BtcPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoinsPerDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestExchange;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestExchangePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExchangeVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeightedPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalVolume;
        private System.Windows.Forms.DataGridViewTextBoxColumn Difficulty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BlockReward;
        private System.Windows.Forms.Label lblElectricityCost;
        private System.Windows.Forms.TextBox txtFiatElectricityCost;
        private System.Windows.Forms.ToolStripStatusLabel tsSpace;
        private System.Windows.Forms.ToolStripStatusLabel tsErrors;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TabPage tabMisc;
        private System.Windows.Forms.TextBox txtProxy;
        private System.Windows.Forms.CheckBox chkProxy;
        private System.Windows.Forms.TabPage tabMultipool;
        private System.Windows.Forms.NumericUpDown nudCryptoday;
        private System.Windows.Forms.CheckBox chkCryptoday;
        private System.Windows.Forms.CheckBox chkReviewCalc;
        private System.Windows.Forms.CheckBox chkNiceHash;
        private System.Windows.Forms.NumericUpDown nudPoolpicker;
        private System.Windows.Forms.CheckBox chkPoolpicker;
        private System.Windows.Forms.TabPage tabPriceCalc;
        private System.Windows.Forms.CheckBox chkRemoveNegative;
        private System.Windows.Forms.ComboBox cbbBidRecentAsk;
        private System.Windows.Forms.Label lblBidRecentAsk;
        private System.Windows.Forms.CheckBox chkRemoveUnlisted;
        private System.Windows.Forms.CheckBox chkWeight;
        private System.Windows.Forms.TabPage tabCoinInfo;
        private System.Windows.Forms.TextBox txtCointweakApiKey;
        private System.Windows.Forms.TextBox txtCoinwarzApiKey;
        private System.Windows.Forms.CheckBox chkWhattomine;
        private System.Windows.Forms.CheckBox chkCoinwarz;
        private System.Windows.Forms.CheckBox chkCointweak;
        private System.Windows.Forms.TabPage tabMarketApi;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox chkCryptoine;
        private System.Windows.Forms.CheckBox chkAtomictrade;
        private System.Windows.Forms.CheckBox chkComkort;
        private System.Windows.Forms.CheckBox chkAllcrypt;
        private System.Windows.Forms.CheckBox chkAllcoin;
        private System.Windows.Forms.CheckBox chkPoloniex;
        private System.Windows.Forms.CheckBox chkCryptsy;
        private System.Windows.Forms.CheckBox chkMintpal;
        private System.Windows.Forms.CheckBox chkBittrex;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.CheckBox chkRemoveTooGoodToBeTrue;
        private System.Windows.Forms.CheckBox chkColor;
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.CheckBox chkRemoveFrozenCoins;
        private System.Windows.Forms.ComboBox cbbProfiles;
        private System.Windows.Forms.Button btnAddDeleteProfile;
        private System.Windows.Forms.TabPage tabReadme;
        private System.Windows.Forms.TextBox txtReadme;
        private System.Windows.Forms.TabPage tabCustomCoins;
        private System.Windows.Forms.DataGridView dgvCustomCoins;
        private System.Windows.Forms.CheckBox chkCCex;
        private System.Windows.Forms.TabPage tabHashrates;
        private System.Windows.Forms.DataGridView dgvCustomAlgos;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.Label lblAmountOfGpu;
        private System.Windows.Forms.NumericUpDown nudTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.CheckBox chkAllHashrates;
        private System.Windows.Forms.CheckBox chkAllCustomCoins;
    }
}

