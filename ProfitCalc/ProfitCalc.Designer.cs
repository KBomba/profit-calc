﻿namespace ProfitCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfitCalc));
            this.btnAddDeleteProfile = new System.Windows.Forms.Button();
            this.cbbProfiles = new System.Windows.Forms.ComboBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.stStatusStrip = new System.Windows.Forms.StatusStrip();
            this.tsProgressText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsErrors = new System.Windows.Forms.ToolStripStatusLabel();
            this.picDonate = new System.Windows.Forms.PictureBox();
            this.tabReadme = new System.Windows.Forms.TabPage();
            this.txtReadme = new System.Windows.Forms.TextBox();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabPriceCalc = new System.Windows.Forms.TabPage();
            this.grpExchangePrice = new System.Windows.Forms.GroupBox();
            this.radLowestAsk = new System.Windows.Forms.RadioButton();
            this.radMostRecentTrade = new System.Windows.Forms.RadioButton();
            this.radHighestBid = new System.Windows.Forms.RadioButton();
            this.grpPriceSource = new System.Windows.Forms.GroupBox();
            this.radFallThroughExchange = new System.Windows.Forms.RadioButton();
            this.radVolumeExchange = new System.Windows.Forms.RadioButton();
            this.radWeighted = new System.Windows.Forms.RadioButton();
            this.grpFiatElectricityMultiplier = new System.Windows.Forms.GroupBox();
            this.lblElectricityCost = new System.Windows.Forms.Label();
            this.txtFiatElectricityCost = new System.Windows.Forms.TextBox();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.lblAmountOfGpu = new System.Windows.Forms.Label();
            this.cbbFiat = new System.Windows.Forms.ComboBox();
            this.chkCoindesk = new System.Windows.Forms.CheckBox();
            this.chk24hDiff = new System.Windows.Forms.CheckBox();
            this.tabMarketApi = new System.Windows.Forms.TabPage();
            this.txtCcexApiKey = new System.Windows.Forms.TextBox();
            this.chkCCex = new System.Windows.Forms.CheckBox();
            this.chkBTer = new System.Windows.Forms.CheckBox();
            this.chkCryptoine = new System.Windows.Forms.CheckBox();
            this.chkAtomictrade = new System.Windows.Forms.CheckBox();
            this.chkComkort = new System.Windows.Forms.CheckBox();
            this.chkAllcrypt = new System.Windows.Forms.CheckBox();
            this.chkAllcoin = new System.Windows.Forms.CheckBox();
            this.chkPoloniex = new System.Windows.Forms.CheckBox();
            this.chkCryptsy = new System.Windows.Forms.CheckBox();
            this.chkMintpal = new System.Windows.Forms.CheckBox();
            this.chkBittrex = new System.Windows.Forms.CheckBox();
            this.tabOuterCoinMultipool = new System.Windows.Forms.TabPage();
            this.tbcInnerCoinMultipool = new System.Windows.Forms.TabControl();
            this.tabCoinInfo = new System.Windows.Forms.TabPage();
            this.txtCointweakApiKey = new System.Windows.Forms.TextBox();
            this.txtCoinwarzApiKey = new System.Windows.Forms.TextBox();
            this.chkWhattomine = new System.Windows.Forms.CheckBox();
            this.chkCoinwarz = new System.Windows.Forms.CheckBox();
            this.chkCointweak = new System.Windows.Forms.CheckBox();
            this.tabMultipool = new System.Windows.Forms.TabPage();
            this.nudCryptoday = new System.Windows.Forms.NumericUpDown();
            this.chkCryptoday = new System.Windows.Forms.CheckBox();
            this.chkReviewCalc = new System.Windows.Forms.CheckBox();
            this.chkNiceHash = new System.Windows.Forms.CheckBox();
            this.nudPoolpicker = new System.Windows.Forms.NumericUpDown();
            this.chkPoolpicker = new System.Windows.Forms.CheckBox();
            this.tabCustomCoins = new System.Windows.Forms.TabPage();
            this.dgvCustomCoins = new System.Windows.Forms.DataGridView();
            this.tabJsonRpc = new System.Windows.Forms.TabPage();
            this.dgvJsonRpc = new System.Windows.Forms.DataGridView();
            this.tabHashrates = new System.Windows.Forms.TabPage();
            this.chkAllHashrates = new System.Windows.Forms.CheckBox();
            this.dgvCustomAlgos = new System.Windows.Forms.DataGridView();
            this.tbcControlSettings = new System.Windows.Forms.TabControl();
            this.tabFilters = new System.Windows.Forms.TabPage();
            this.chkRemoveZeroVolume = new System.Windows.Forms.CheckBox();
            this.chkRemoveFrozenCoins = new System.Windows.Forms.CheckBox();
            this.chkColor = new System.Windows.Forms.CheckBox();
            this.chkRemoveTooGoodToBeTrue = new System.Windows.Forms.CheckBox();
            this.chkRemoveUnlisted = new System.Windows.Forms.CheckBox();
            this.chkRemoveNegative = new System.Windows.Forms.CheckBox();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.chkOrderDepth = new System.Windows.Forms.CheckBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.chkProxy = new System.Windows.Forms.CheckBox();
            this.spcMain = new System.Windows.Forms.SplitContainer();
            this.tbcResults = new System.Windows.Forms.TabControl();
            this.stStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDonate)).BeginInit();
            this.tabReadme.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabPriceCalc.SuspendLayout();
            this.grpExchangePrice.SuspendLayout();
            this.grpPriceSource.SuspendLayout();
            this.grpFiatElectricityMultiplier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.tabMarketApi.SuspendLayout();
            this.tabOuterCoinMultipool.SuspendLayout();
            this.tbcInnerCoinMultipool.SuspendLayout();
            this.tabCoinInfo.SuspendLayout();
            this.tabMultipool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCryptoday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoolpicker)).BeginInit();
            this.tabCustomCoins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomCoins)).BeginInit();
            this.tabJsonRpc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJsonRpc)).BeginInit();
            this.tabHashrates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomAlgos)).BeginInit();
            this.tbcControlSettings.SuspendLayout();
            this.tabFilters.SuspendLayout();
            this.tabMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).BeginInit();
            this.spcMain.Panel1.SuspendLayout();
            this.spcMain.Panel2.SuspendLayout();
            this.spcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddDeleteProfile
            // 
            this.btnAddDeleteProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDeleteProfile.Enabled = false;
            this.btnAddDeleteProfile.Location = new System.Drawing.Point(105, 252);
            this.btnAddDeleteProfile.Name = "btnAddDeleteProfile";
            this.btnAddDeleteProfile.Size = new System.Drawing.Size(87, 23);
            this.btnAddDeleteProfile.TabIndex = 80;
            this.btnAddDeleteProfile.Text = "Remove profile";
            this.btnAddDeleteProfile.UseVisualStyleBackColor = true;
            this.btnAddDeleteProfile.Click += new System.EventHandler(this.btnAddDeleteProfile_Click);
            // 
            // cbbProfiles
            // 
            this.cbbProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbbProfiles.FormattingEnabled = true;
            this.cbbProfiles.Location = new System.Drawing.Point(12, 252);
            this.cbbProfiles.Name = "cbbProfiles";
            this.cbbProfiles.Size = new System.Drawing.Size(87, 21);
            this.cbbProfiles.TabIndex = 79;
            this.cbbProfiles.SelectedIndexChanged += new System.EventHandler(this.cbbProfiles_SelectedIndexChanged);
            this.cbbProfiles.TextChanged += new System.EventHandler(this.cbbProfiles_TextChanged);
            // 
            // btnCalc
            // 
            this.btnCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalc.Location = new System.Drawing.Point(892, 252);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(80, 23);
            this.btnCalc.TabIndex = 26;
            this.btnCalc.Text = "Calculate";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
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
            this.stStatusStrip.Location = new System.Drawing.Point(0, 640);
            this.stStatusStrip.Name = "stStatusStrip";
            this.stStatusStrip.Size = new System.Drawing.Size(984, 22);
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
            this.tsSpace.Size = new System.Drawing.Size(716, 17);
            this.tsSpace.Spring = true;
            // 
            // tsErrors
            // 
            this.tsErrors.Name = "tsErrors";
            this.tsErrors.Size = new System.Drawing.Size(0, 17);
            // 
            // picDonate
            // 
            this.picDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picDonate.Image = ((System.Drawing.Image)(resources.GetObject("picDonate.Image")));
            this.picDonate.Location = new System.Drawing.Point(822, 255);
            this.picDonate.Name = "picDonate";
            this.picDonate.Size = new System.Drawing.Size(64, 17);
            this.picDonate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDonate.TabIndex = 81;
            this.picDonate.TabStop = false;
            this.picDonate.Visible = false;
            this.picDonate.Click += new System.EventHandler(this.picDonate_Click);
            // 
            // tabReadme
            // 
            this.tabReadme.Controls.Add(this.txtReadme);
            this.tabReadme.Location = new System.Drawing.Point(4, 22);
            this.tabReadme.Name = "tabReadme";
            this.tabReadme.Padding = new System.Windows.Forms.Padding(3);
            this.tabReadme.Size = new System.Drawing.Size(973, 217);
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
            this.txtReadme.Size = new System.Drawing.Size(967, 211);
            this.txtReadme.TabIndex = 0;
            this.txtReadme.Text = "Seems like README.txt is missing :) But you can still donate some BTC @ 1BombaWy4" +
    "6SPqX8NJumFBvSjSpry8hpzr4 ";
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(973, 217);
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
            this.txtLog.Size = new System.Drawing.Size(967, 211);
            this.txtLog.TabIndex = 0;
            this.txtLog.WordWrap = false;
            // 
            // tabPriceCalc
            // 
            this.tabPriceCalc.Controls.Add(this.grpExchangePrice);
            this.tabPriceCalc.Controls.Add(this.grpPriceSource);
            this.tabPriceCalc.Controls.Add(this.grpFiatElectricityMultiplier);
            this.tabPriceCalc.Location = new System.Drawing.Point(4, 22);
            this.tabPriceCalc.Name = "tabPriceCalc";
            this.tabPriceCalc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPriceCalc.Size = new System.Drawing.Size(973, 217);
            this.tabPriceCalc.TabIndex = 12;
            this.tabPriceCalc.Text = "Price Calc Settings";
            this.tabPriceCalc.UseVisualStyleBackColor = true;
            // 
            // grpExchangePrice
            // 
            this.grpExchangePrice.Controls.Add(this.radLowestAsk);
            this.grpExchangePrice.Controls.Add(this.radMostRecentTrade);
            this.grpExchangePrice.Controls.Add(this.radHighestBid);
            this.grpExchangePrice.Location = new System.Drawing.Point(268, 6);
            this.grpExchangePrice.Name = "grpExchangePrice";
            this.grpExchangePrice.Size = new System.Drawing.Size(143, 90);
            this.grpExchangePrice.TabIndex = 70;
            this.grpExchangePrice.TabStop = false;
            this.grpExchangePrice.Text = "Which exchange price?";
            // 
            // radLowestAsk
            // 
            this.radLowestAsk.AutoSize = true;
            this.radLowestAsk.Location = new System.Drawing.Point(6, 65);
            this.radLowestAsk.Name = "radLowestAsk";
            this.radLowestAsk.Size = new System.Drawing.Size(75, 17);
            this.radLowestAsk.TabIndex = 2;
            this.radLowestAsk.Text = "lowest ask";
            this.radLowestAsk.UseVisualStyleBackColor = true;
            // 
            // radMostRecentTrade
            // 
            this.radMostRecentTrade.AutoSize = true;
            this.radMostRecentTrade.Location = new System.Drawing.Point(6, 42);
            this.radMostRecentTrade.Name = "radMostRecentTrade";
            this.radMostRecentTrade.Size = new System.Drawing.Size(107, 17);
            this.radMostRecentTrade.TabIndex = 1;
            this.radMostRecentTrade.Text = "most recent trade";
            this.radMostRecentTrade.UseVisualStyleBackColor = true;
            // 
            // radHighestBid
            // 
            this.radHighestBid.AutoSize = true;
            this.radHighestBid.Checked = true;
            this.radHighestBid.Location = new System.Drawing.Point(6, 19);
            this.radHighestBid.Name = "radHighestBid";
            this.radHighestBid.Size = new System.Drawing.Size(76, 17);
            this.radHighestBid.TabIndex = 0;
            this.radHighestBid.TabStop = true;
            this.radHighestBid.Text = "highest bid";
            this.radHighestBid.UseVisualStyleBackColor = true;
            // 
            // grpPriceSource
            // 
            this.grpPriceSource.Controls.Add(this.radFallThroughExchange);
            this.grpPriceSource.Controls.Add(this.radVolumeExchange);
            this.grpPriceSource.Controls.Add(this.radWeighted);
            this.grpPriceSource.Location = new System.Drawing.Point(7, 6);
            this.grpPriceSource.Name = "grpPriceSource";
            this.grpPriceSource.Size = new System.Drawing.Size(255, 90);
            this.grpPriceSource.TabIndex = 69;
            this.grpPriceSource.TabStop = false;
            this.grpPriceSource.Text = "Price source";
            // 
            // radFallThroughExchange
            // 
            this.radFallThroughExchange.AutoSize = true;
            this.radFallThroughExchange.Location = new System.Drawing.Point(6, 65);
            this.radFallThroughExchange.Name = "radFallThroughExchange";
            this.radFallThroughExchange.Size = new System.Drawing.Size(195, 17);
            this.radFallThroughExchange.TabIndex = 2;
            this.radFallThroughExchange.Text = "exchange with best fallthrough price";
            this.radFallThroughExchange.UseVisualStyleBackColor = true;
            this.radFallThroughExchange.CheckedChanged += new System.EventHandler(this.radFallThroughExchange_CheckedChanged);
            // 
            // radVolumeExchange
            // 
            this.radVolumeExchange.AutoSize = true;
            this.radVolumeExchange.Location = new System.Drawing.Point(6, 42);
            this.radVolumeExchange.Name = "radVolumeExchange";
            this.radVolumeExchange.Size = new System.Drawing.Size(156, 17);
            this.radVolumeExchange.TabIndex = 1;
            this.radVolumeExchange.Text = "exchange with most volume";
            this.radVolumeExchange.UseVisualStyleBackColor = true;
            // 
            // radWeighted
            // 
            this.radWeighted.AutoSize = true;
            this.radWeighted.Checked = true;
            this.radWeighted.Location = new System.Drawing.Point(6, 19);
            this.radWeighted.Name = "radWeighted";
            this.radWeighted.Size = new System.Drawing.Size(244, 17);
            this.radWeighted.TabIndex = 0;
            this.radWeighted.TabStop = true;
            this.radWeighted.Text = "average of all exchanges, weighted by volume";
            this.radWeighted.UseVisualStyleBackColor = true;
            // 
            // grpFiatElectricityMultiplier
            // 
            this.grpFiatElectricityMultiplier.Controls.Add(this.lblElectricityCost);
            this.grpFiatElectricityMultiplier.Controls.Add(this.txtFiatElectricityCost);
            this.grpFiatElectricityMultiplier.Controls.Add(this.nudAmount);
            this.grpFiatElectricityMultiplier.Controls.Add(this.lblAmountOfGpu);
            this.grpFiatElectricityMultiplier.Controls.Add(this.cbbFiat);
            this.grpFiatElectricityMultiplier.Controls.Add(this.chkCoindesk);
            this.grpFiatElectricityMultiplier.Location = new System.Drawing.Point(7, 102);
            this.grpFiatElectricityMultiplier.Name = "grpFiatElectricityMultiplier";
            this.grpFiatElectricityMultiplier.Size = new System.Drawing.Size(404, 97);
            this.grpFiatElectricityMultiplier.TabIndex = 71;
            this.grpFiatElectricityMultiplier.TabStop = false;
            this.grpFiatElectricityMultiplier.Text = "Fiat, electricity and multiplier (profile-bound)";
            // 
            // lblElectricityCost
            // 
            this.lblElectricityCost.AutoSize = true;
            this.lblElectricityCost.Location = new System.Drawing.Point(4, 47);
            this.lblElectricityCost.Name = "lblElectricityCost";
            this.lblElectricityCost.Size = new System.Drawing.Size(58, 13);
            this.lblElectricityCost.TabIndex = 68;
            this.lblElectricityCost.Text = "USD/kWh";
            // 
            // txtFiatElectricityCost
            // 
            this.txtFiatElectricityCost.Location = new System.Drawing.Point(68, 44);
            this.txtFiatElectricityCost.Name = "txtFiatElectricityCost";
            this.txtFiatElectricityCost.Size = new System.Drawing.Size(50, 20);
            this.txtFiatElectricityCost.TabIndex = 67;
            this.txtFiatElectricityCost.Text = "0.1";
            this.txtFiatElectricityCost.TextChanged += new System.EventHandler(this.txtFiatElectricityCost_TextChanged);
            // 
            // nudAmount
            // 
            this.nudAmount.Location = new System.Drawing.Point(205, 70);
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
            // lblAmountOfGpu
            // 
            this.lblAmountOfGpu.AutoSize = true;
            this.lblAmountOfGpu.Location = new System.Drawing.Point(3, 72);
            this.lblAmountOfGpu.Name = "lblAmountOfGpu";
            this.lblAmountOfGpu.Size = new System.Drawing.Size(196, 13);
            this.lblAmountOfGpu.TabIndex = 32;
            this.lblAmountOfGpu.Text = "Amount of GPUs, multiplier for hashrates";
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
            this.cbbFiat.Location = new System.Drawing.Point(49, 17);
            this.cbbFiat.Name = "cbbFiat";
            this.cbbFiat.Size = new System.Drawing.Size(48, 21);
            this.cbbFiat.TabIndex = 35;
            this.cbbFiat.SelectedIndexChanged += new System.EventHandler(this.cbbFiat_SelectedIndexChanged);
            // 
            // chkCoindesk
            // 
            this.chkCoindesk.AutoSize = true;
            this.chkCoindesk.Checked = true;
            this.chkCoindesk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCoindesk.Location = new System.Drawing.Point(6, 19);
            this.chkCoindesk.Name = "chkCoindesk";
            this.chkCoindesk.Size = new System.Drawing.Size(126, 17);
            this.chkCoindesk.TabIndex = 39;
            this.chkCoindesk.Text = "Calc                  /day.";
            this.chkCoindesk.UseVisualStyleBackColor = true;
            this.chkCoindesk.CheckedChanged += new System.EventHandler(this.chkCoindesk_CheckedChanged);
            // 
            // chk24hDiff
            // 
            this.chk24hDiff.AutoSize = true;
            this.chk24hDiff.Checked = true;
            this.chk24hDiff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk24hDiff.Location = new System.Drawing.Point(6, 100);
            this.chk24hDiff.Name = "chk24hDiff";
            this.chk24hDiff.Size = new System.Drawing.Size(211, 17);
            this.chk24hDiff.TabIndex = 69;
            this.chk24hDiff.Text = "Use 24hr difficulty average (if available)";
            this.chk24hDiff.UseVisualStyleBackColor = true;
            // 
            // tabMarketApi
            // 
            this.tabMarketApi.Controls.Add(this.txtCcexApiKey);
            this.tabMarketApi.Controls.Add(this.chkCCex);
            this.tabMarketApi.Controls.Add(this.chkBTer);
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
            this.tabMarketApi.Size = new System.Drawing.Size(973, 217);
            this.tabMarketApi.TabIndex = 0;
            this.tabMarketApi.Text = "Market API";
            this.tabMarketApi.UseVisualStyleBackColor = true;
            // 
            // txtCcexApiKey
            // 
            this.txtCcexApiKey.Enabled = false;
            this.txtCcexApiKey.Location = new System.Drawing.Point(66, 121);
            this.txtCcexApiKey.Name = "txtCcexApiKey";
            this.txtCcexApiKey.Size = new System.Drawing.Size(371, 20);
            this.txtCcexApiKey.TabIndex = 47;
            // 
            // chkCCex
            // 
            this.chkCCex.AutoSize = true;
            this.chkCCex.Location = new System.Drawing.Point(6, 123);
            this.chkCCex.Name = "chkCCex";
            this.chkCCex.Size = new System.Drawing.Size(54, 17);
            this.chkCCex.TabIndex = 46;
            this.chkCCex.Text = "C-Cex";
            this.chkCCex.UseVisualStyleBackColor = true;
            this.chkCCex.CheckedChanged += new System.EventHandler(this.chkCCex_CheckedChanged);
            // 
            // chkBTer
            // 
            this.chkBTer.AutoSize = true;
            this.chkBTer.Location = new System.Drawing.Point(6, 98);
            this.chkBTer.Name = "chkBTer";
            this.chkBTer.Size = new System.Drawing.Size(49, 17);
            this.chkBTer.TabIndex = 45;
            this.chkBTer.Text = "BTer";
            this.chkBTer.UseVisualStyleBackColor = true;
            // 
            // chkCryptoine
            // 
            this.chkCryptoine.AutoSize = true;
            this.chkCryptoine.Location = new System.Drawing.Point(123, 98);
            this.chkCryptoine.Name = "chkCryptoine";
            this.chkCryptoine.Size = new System.Drawing.Size(70, 17);
            this.chkCryptoine.TabIndex = 44;
            this.chkCryptoine.Text = "Cryptoine";
            this.chkCryptoine.UseVisualStyleBackColor = true;
            // 
            // chkAtomictrade
            // 
            this.chkAtomictrade.AutoSize = true;
            this.chkAtomictrade.Location = new System.Drawing.Point(123, 75);
            this.chkAtomictrade.Name = "chkAtomictrade";
            this.chkAtomictrade.Size = new System.Drawing.Size(89, 17);
            this.chkAtomictrade.TabIndex = 43;
            this.chkAtomictrade.Text = "Atomic Trade";
            this.chkAtomictrade.UseVisualStyleBackColor = true;
            // 
            // chkComkort
            // 
            this.chkComkort.AutoSize = true;
            this.chkComkort.Location = new System.Drawing.Point(123, 6);
            this.chkComkort.Name = "chkComkort";
            this.chkComkort.Size = new System.Drawing.Size(65, 17);
            this.chkComkort.TabIndex = 42;
            this.chkComkort.Text = "Comkort";
            this.chkComkort.UseVisualStyleBackColor = true;
            // 
            // chkAllcrypt
            // 
            this.chkAllcrypt.AutoSize = true;
            this.chkAllcrypt.Location = new System.Drawing.Point(123, 52);
            this.chkAllcrypt.Name = "chkAllcrypt";
            this.chkAllcrypt.Size = new System.Drawing.Size(61, 17);
            this.chkAllcrypt.TabIndex = 41;
            this.chkAllcrypt.Text = "AllCrypt";
            this.chkAllcrypt.UseVisualStyleBackColor = true;
            // 
            // chkAllcoin
            // 
            this.chkAllcoin.AutoSize = true;
            this.chkAllcoin.Location = new System.Drawing.Point(123, 29);
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
            this.chkMintpal.Enabled = false;
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
            // tabOuterCoinMultipool
            // 
            this.tabOuterCoinMultipool.Controls.Add(this.tbcInnerCoinMultipool);
            this.tabOuterCoinMultipool.Location = new System.Drawing.Point(4, 22);
            this.tabOuterCoinMultipool.Name = "tabOuterCoinMultipool";
            this.tabOuterCoinMultipool.Padding = new System.Windows.Forms.Padding(3);
            this.tabOuterCoinMultipool.Size = new System.Drawing.Size(973, 217);
            this.tabOuterCoinMultipool.TabIndex = 10;
            this.tabOuterCoinMultipool.Text = "Coin & Multipool Input";
            this.tabOuterCoinMultipool.UseVisualStyleBackColor = true;
            // 
            // tbcInnerCoinMultipool
            // 
            this.tbcInnerCoinMultipool.Controls.Add(this.tabCoinInfo);
            this.tbcInnerCoinMultipool.Controls.Add(this.tabMultipool);
            this.tbcInnerCoinMultipool.Controls.Add(this.tabCustomCoins);
            this.tbcInnerCoinMultipool.Controls.Add(this.tabJsonRpc);
            this.tbcInnerCoinMultipool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcInnerCoinMultipool.Location = new System.Drawing.Point(3, 3);
            this.tbcInnerCoinMultipool.Name = "tbcInnerCoinMultipool";
            this.tbcInnerCoinMultipool.SelectedIndex = 0;
            this.tbcInnerCoinMultipool.Size = new System.Drawing.Size(967, 211);
            this.tbcInnerCoinMultipool.TabIndex = 0;
            // 
            // tabCoinInfo
            // 
            this.tabCoinInfo.Controls.Add(this.txtCointweakApiKey);
            this.tabCoinInfo.Controls.Add(this.chk24hDiff);
            this.tabCoinInfo.Controls.Add(this.txtCoinwarzApiKey);
            this.tabCoinInfo.Controls.Add(this.chkWhattomine);
            this.tabCoinInfo.Controls.Add(this.chkCoinwarz);
            this.tabCoinInfo.Controls.Add(this.chkCointweak);
            this.tabCoinInfo.Location = new System.Drawing.Point(4, 22);
            this.tabCoinInfo.Name = "tabCoinInfo";
            this.tabCoinInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabCoinInfo.Size = new System.Drawing.Size(959, 185);
            this.tabCoinInfo.TabIndex = 2;
            this.tabCoinInfo.Text = "Coin Info APIs";
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
            this.tabMultipool.Size = new System.Drawing.Size(959, 185);
            this.tabMultipool.TabIndex = 10;
            this.tabMultipool.Text = "Multipool APIs";
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
            // tabCustomCoins
            // 
            this.tabCustomCoins.Controls.Add(this.dgvCustomCoins);
            this.tabCustomCoins.Location = new System.Drawing.Point(4, 22);
            this.tabCustomCoins.Name = "tabCustomCoins";
            this.tabCustomCoins.Size = new System.Drawing.Size(959, 185);
            this.tabCustomCoins.TabIndex = 9;
            this.tabCustomCoins.Text = "Custom Coins";
            this.tabCustomCoins.UseVisualStyleBackColor = true;
            // 
            // dgvCustomCoins
            // 
            this.dgvCustomCoins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCustomCoins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomCoins.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomCoins.Name = "dgvCustomCoins";
            this.dgvCustomCoins.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCustomCoins.Size = new System.Drawing.Size(959, 185);
            this.dgvCustomCoins.TabIndex = 0;
            this.dgvCustomCoins.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvCustomCoins_DefaultValuesNeeded);
            this.dgvCustomCoins.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCustomCoins_EditingControlShowing);
            this.dgvCustomCoins.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvNeedsEdit_MouseUp);
            // 
            // tabJsonRpc
            // 
            this.tabJsonRpc.Controls.Add(this.dgvJsonRpc);
            this.tabJsonRpc.Location = new System.Drawing.Point(4, 22);
            this.tabJsonRpc.Name = "tabJsonRpc";
            this.tabJsonRpc.Size = new System.Drawing.Size(959, 185);
            this.tabJsonRpc.TabIndex = 11;
            this.tabJsonRpc.Text = "Live JSON-RPC Update";
            this.tabJsonRpc.UseVisualStyleBackColor = true;
            // 
            // dgvJsonRpc
            // 
            this.dgvJsonRpc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvJsonRpc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvJsonRpc.Location = new System.Drawing.Point(0, 0);
            this.dgvJsonRpc.Name = "dgvJsonRpc";
            this.dgvJsonRpc.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvJsonRpc.Size = new System.Drawing.Size(959, 185);
            this.dgvJsonRpc.TabIndex = 0;
            this.dgvJsonRpc.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvJsonRpc_DefaultValuesNeeded);
            this.dgvJsonRpc.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvJsonRpc_EditingControlShowing);
            this.dgvJsonRpc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvNeedsEdit_MouseUp);
            // 
            // tabHashrates
            // 
            this.tabHashrates.Controls.Add(this.chkAllHashrates);
            this.tabHashrates.Controls.Add(this.dgvCustomAlgos);
            this.tabHashrates.Location = new System.Drawing.Point(4, 22);
            this.tabHashrates.Name = "tabHashrates";
            this.tabHashrates.Size = new System.Drawing.Size(973, 217);
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
            this.dgvCustomAlgos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCustomAlgos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomAlgos.Location = new System.Drawing.Point(0, 0);
            this.dgvCustomAlgos.Name = "dgvCustomAlgos";
            this.dgvCustomAlgos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCustomAlgos.Size = new System.Drawing.Size(973, 217);
            this.dgvCustomAlgos.TabIndex = 1;
            this.dgvCustomAlgos.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvCustomAlgos_DefaultValuesNeeded);
            this.dgvCustomAlgos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCustomAlgos_EditingControlShowing);
            this.dgvCustomAlgos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvNeedsEdit_MouseUp);
            this.dgvCustomAlgos.Validated += new System.EventHandler(this.dgvCustomAlgos_Validated);
            // 
            // tbcControlSettings
            // 
            this.tbcControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcControlSettings.Controls.Add(this.tabHashrates);
            this.tbcControlSettings.Controls.Add(this.tabOuterCoinMultipool);
            this.tbcControlSettings.Controls.Add(this.tabMarketApi);
            this.tbcControlSettings.Controls.Add(this.tabPriceCalc);
            this.tbcControlSettings.Controls.Add(this.tabFilters);
            this.tbcControlSettings.Controls.Add(this.tabMisc);
            this.tbcControlSettings.Controls.Add(this.tabLog);
            this.tbcControlSettings.Controls.Add(this.tabReadme);
            this.tbcControlSettings.Location = new System.Drawing.Point(3, 3);
            this.tbcControlSettings.Name = "tbcControlSettings";
            this.tbcControlSettings.SelectedIndex = 0;
            this.tbcControlSettings.Size = new System.Drawing.Size(981, 243);
            this.tbcControlSettings.TabIndex = 6;
            this.tbcControlSettings.TabStop = false;
            // 
            // tabFilters
            // 
            this.tabFilters.Controls.Add(this.chkRemoveZeroVolume);
            this.tabFilters.Controls.Add(this.chkRemoveFrozenCoins);
            this.tabFilters.Controls.Add(this.chkColor);
            this.tabFilters.Controls.Add(this.chkRemoveTooGoodToBeTrue);
            this.tabFilters.Controls.Add(this.chkRemoveUnlisted);
            this.tabFilters.Controls.Add(this.chkRemoveNegative);
            this.tabFilters.Location = new System.Drawing.Point(4, 22);
            this.tabFilters.Name = "tabFilters";
            this.tabFilters.Padding = new System.Windows.Forms.Padding(3);
            this.tabFilters.Size = new System.Drawing.Size(973, 217);
            this.tabFilters.TabIndex = 13;
            this.tabFilters.Text = "Filters";
            this.tabFilters.UseVisualStyleBackColor = true;
            // 
            // chkRemoveZeroVolume
            // 
            this.chkRemoveZeroVolume.AutoSize = true;
            this.chkRemoveZeroVolume.Location = new System.Drawing.Point(6, 75);
            this.chkRemoveZeroVolume.Name = "chkRemoveZeroVolume";
            this.chkRemoveZeroVolume.Size = new System.Drawing.Size(200, 17);
            this.chkRemoveZeroVolume.TabIndex = 43;
            this.chkRemoveZeroVolume.Text = "Remove coins with zero daily volume";
            this.chkRemoveZeroVolume.UseVisualStyleBackColor = true;
            this.chkRemoveZeroVolume.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // chkRemoveFrozenCoins
            // 
            this.chkRemoveFrozenCoins.AutoSize = true;
            this.chkRemoveFrozenCoins.BackColor = System.Drawing.Color.Transparent;
            this.chkRemoveFrozenCoins.Location = new System.Drawing.Point(6, 29);
            this.chkRemoveFrozenCoins.Name = "chkRemoveFrozenCoins";
            this.chkRemoveFrozenCoins.Size = new System.Drawing.Size(285, 17);
            this.chkRemoveFrozenCoins.TabIndex = 42;
            this.chkRemoveFrozenCoins.Text = "Remove coins that are frozen on supported exchanges";
            this.chkRemoveFrozenCoins.UseVisualStyleBackColor = false;
            this.chkRemoveFrozenCoins.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // chkColor
            // 
            this.chkColor.AutoSize = true;
            this.chkColor.Checked = true;
            this.chkColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColor.Location = new System.Drawing.Point(6, 121);
            this.chkColor.Name = "chkColor";
            this.chkColor.Size = new System.Drawing.Size(118, 17);
            this.chkColor.TabIndex = 33;
            this.chkColor.Text = "Use a colored table";
            this.chkColor.UseVisualStyleBackColor = true;
            this.chkColor.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            this.chkColor.CheckStateChanged += new System.EventHandler(this.chkColor_CheckStateChanged);
            // 
            // chkRemoveTooGoodToBeTrue
            // 
            this.chkRemoveTooGoodToBeTrue.AutoSize = true;
            this.chkRemoveTooGoodToBeTrue.Location = new System.Drawing.Point(6, 52);
            this.chkRemoveTooGoodToBeTrue.Name = "chkRemoveTooGoodToBeTrue";
            this.chkRemoveTooGoodToBeTrue.Size = new System.Drawing.Size(287, 17);
            this.chkRemoveTooGoodToBeTrue.TabIndex = 41;
            this.chkRemoveTooGoodToBeTrue.Text = "Remove coins with less daily volume than you can earn";
            this.chkRemoveTooGoodToBeTrue.UseVisualStyleBackColor = true;
            this.chkRemoveTooGoodToBeTrue.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // chkRemoveUnlisted
            // 
            this.chkRemoveUnlisted.AutoSize = true;
            this.chkRemoveUnlisted.BackColor = System.Drawing.Color.Transparent;
            this.chkRemoveUnlisted.Location = new System.Drawing.Point(6, 6);
            this.chkRemoveUnlisted.Name = "chkRemoveUnlisted";
            this.chkRemoveUnlisted.Size = new System.Drawing.Size(291, 17);
            this.chkRemoveUnlisted.TabIndex = 36;
            this.chkRemoveUnlisted.Text = "Remove coins that aren\'t listed on supported exchanges";
            this.chkRemoveUnlisted.UseVisualStyleBackColor = false;
            this.chkRemoveUnlisted.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // chkRemoveNegative
            // 
            this.chkRemoveNegative.AutoSize = true;
            this.chkRemoveNegative.Location = new System.Drawing.Point(6, 98);
            this.chkRemoveNegative.Name = "chkRemoveNegative";
            this.chkRemoveNegative.Size = new System.Drawing.Size(200, 17);
            this.chkRemoveNegative.TabIndex = 40;
            this.chkRemoveNegative.Text = "Remove results with a negative price";
            this.chkRemoveNegative.UseVisualStyleBackColor = true;
            this.chkRemoveNegative.CheckedChanged += new System.EventHandler(this.reasonToUpdateDgv_CheckedChanged);
            // 
            // tabMisc
            // 
            this.tabMisc.Controls.Add(this.chkOrderDepth);
            this.tabMisc.Controls.Add(this.lblTimeout);
            this.tabMisc.Controls.Add(this.nudTimeout);
            this.tabMisc.Controls.Add(this.txtProxy);
            this.tabMisc.Controls.Add(this.chkProxy);
            this.tabMisc.Location = new System.Drawing.Point(4, 22);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.Size = new System.Drawing.Size(973, 217);
            this.tabMisc.TabIndex = 14;
            this.tabMisc.Text = "Misc Settings";
            this.tabMisc.UseVisualStyleBackColor = true;
            // 
            // chkOrderDepth
            // 
            this.chkOrderDepth.AutoSize = true;
            this.chkOrderDepth.Checked = true;
            this.chkOrderDepth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOrderDepth.Location = new System.Drawing.Point(6, 84);
            this.chkOrderDepth.Name = "chkOrderDepth";
            this.chkOrderDepth.Size = new System.Drawing.Size(105, 17);
            this.chkOrderDepth.TabIndex = 38;
            this.chkOrderDepth.Text = "Get order depths";
            this.chkOrderDepth.UseVisualStyleBackColor = true;
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
            // spcMain
            // 
            this.spcMain.BackColor = System.Drawing.Color.Gainsboro;
            this.spcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMain.Location = new System.Drawing.Point(0, 0);
            this.spcMain.Name = "spcMain";
            this.spcMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcMain.Panel1
            // 
            this.spcMain.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.spcMain.Panel1.Controls.Add(this.tbcControlSettings);
            this.spcMain.Panel1.Controls.Add(this.picDonate);
            this.spcMain.Panel1.Controls.Add(this.cbbProfiles);
            this.spcMain.Panel1.Controls.Add(this.btnAddDeleteProfile);
            this.spcMain.Panel1.Controls.Add(this.btnCalc);
            // 
            // spcMain.Panel2
            // 
            this.spcMain.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.spcMain.Panel2.Controls.Add(this.tbcResults);
            this.spcMain.Size = new System.Drawing.Size(984, 640);
            this.spcMain.SplitterDistance = 278;
            this.spcMain.SplitterWidth = 6;
            this.spcMain.TabIndex = 82;
            // 
            // tbcResults
            // 
            this.tbcResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcResults.Location = new System.Drawing.Point(0, 0);
            this.tbcResults.Name = "tbcResults";
            this.tbcResults.SelectedIndex = 0;
            this.tbcResults.Size = new System.Drawing.Size(984, 356);
            this.tbcResults.TabIndex = 0;
            // 
            // ProfitCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.spcMain);
            this.Controls.Add(this.stStatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfitCalc";
            this.Text = "Profit Calculator~ By KBomba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CudaProfitCalc_FormClosing);
            this.stStatusStrip.ResumeLayout(false);
            this.stStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDonate)).EndInit();
            this.tabReadme.ResumeLayout(false);
            this.tabReadme.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabPriceCalc.ResumeLayout(false);
            this.grpExchangePrice.ResumeLayout(false);
            this.grpExchangePrice.PerformLayout();
            this.grpPriceSource.ResumeLayout(false);
            this.grpPriceSource.PerformLayout();
            this.grpFiatElectricityMultiplier.ResumeLayout(false);
            this.grpFiatElectricityMultiplier.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.tabMarketApi.ResumeLayout(false);
            this.tabMarketApi.PerformLayout();
            this.tabOuterCoinMultipool.ResumeLayout(false);
            this.tbcInnerCoinMultipool.ResumeLayout(false);
            this.tabCoinInfo.ResumeLayout(false);
            this.tabCoinInfo.PerformLayout();
            this.tabMultipool.ResumeLayout(false);
            this.tabMultipool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCryptoday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPoolpicker)).EndInit();
            this.tabCustomCoins.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomCoins)).EndInit();
            this.tabJsonRpc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJsonRpc)).EndInit();
            this.tabHashrates.ResumeLayout(false);
            this.tabHashrates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomAlgos)).EndInit();
            this.tbcControlSettings.ResumeLayout(false);
            this.tabFilters.ResumeLayout(false);
            this.tabFilters.PerformLayout();
            this.tabMisc.ResumeLayout(false);
            this.tabMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
            this.spcMain.Panel1.ResumeLayout(false);
            this.spcMain.Panel1.PerformLayout();
            this.spcMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcMain)).EndInit();
            this.spcMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.StatusStrip stStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar tsProgress;
        private System.Windows.Forms.ToolStripStatusLabel tsProgressText;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsSpace;
        private System.Windows.Forms.ToolStripStatusLabel tsErrors;
        private System.Windows.Forms.ComboBox cbbProfiles;
        private System.Windows.Forms.Button btnAddDeleteProfile;
        private System.Windows.Forms.PictureBox picDonate;
        private System.Windows.Forms.TabPage tabReadme;
        private System.Windows.Forms.TextBox txtReadme;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TabPage tabPriceCalc;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.ComboBox cbbFiat;
        private System.Windows.Forms.Label lblAmountOfGpu;
        private System.Windows.Forms.CheckBox chkCoindesk;
        private System.Windows.Forms.TextBox txtFiatElectricityCost;
        private System.Windows.Forms.Label lblElectricityCost;
        private System.Windows.Forms.TabPage tabMarketApi;
        private System.Windows.Forms.CheckBox chkCCex;
        private System.Windows.Forms.CheckBox chkBTer;
        private System.Windows.Forms.CheckBox chkCryptoine;
        private System.Windows.Forms.CheckBox chkAtomictrade;
        private System.Windows.Forms.CheckBox chkComkort;
        private System.Windows.Forms.CheckBox chkAllcrypt;
        private System.Windows.Forms.CheckBox chkAllcoin;
        private System.Windows.Forms.CheckBox chkPoloniex;
        private System.Windows.Forms.CheckBox chkCryptsy;
        private System.Windows.Forms.CheckBox chkMintpal;
        private System.Windows.Forms.CheckBox chkBittrex;
        private System.Windows.Forms.TabPage tabOuterCoinMultipool;
        private System.Windows.Forms.TabControl tbcInnerCoinMultipool;
        private System.Windows.Forms.TabPage tabCoinInfo;
        private System.Windows.Forms.TextBox txtCointweakApiKey;
        private System.Windows.Forms.TextBox txtCoinwarzApiKey;
        private System.Windows.Forms.CheckBox chkWhattomine;
        private System.Windows.Forms.CheckBox chkCoinwarz;
        private System.Windows.Forms.CheckBox chkCointweak;
        private System.Windows.Forms.TabPage tabMultipool;
        private System.Windows.Forms.NumericUpDown nudCryptoday;
        private System.Windows.Forms.CheckBox chkCryptoday;
        private System.Windows.Forms.CheckBox chkReviewCalc;
        private System.Windows.Forms.CheckBox chkNiceHash;
        private System.Windows.Forms.NumericUpDown nudPoolpicker;
        private System.Windows.Forms.CheckBox chkPoolpicker;
        private System.Windows.Forms.TabPage tabCustomCoins;
        private System.Windows.Forms.DataGridView dgvCustomCoins;
        private System.Windows.Forms.TabPage tabJsonRpc;
        private System.Windows.Forms.DataGridView dgvJsonRpc;
        private System.Windows.Forms.TabPage tabHashrates;
        private System.Windows.Forms.CheckBox chkAllHashrates;
        private System.Windows.Forms.DataGridView dgvCustomAlgos;
        private System.Windows.Forms.TabControl tbcControlSettings;
        private System.Windows.Forms.TabPage tabFilters;
        private System.Windows.Forms.CheckBox chkRemoveZeroVolume;
        private System.Windows.Forms.CheckBox chkRemoveFrozenCoins;
        private System.Windows.Forms.CheckBox chkColor;
        private System.Windows.Forms.CheckBox chkRemoveTooGoodToBeTrue;
        private System.Windows.Forms.CheckBox chkRemoveUnlisted;
        private System.Windows.Forms.CheckBox chkRemoveNegative;
        private System.Windows.Forms.TabPage tabMisc;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.NumericUpDown nudTimeout;
        private System.Windows.Forms.TextBox txtProxy;
        private System.Windows.Forms.CheckBox chkProxy;
        private System.Windows.Forms.CheckBox chk24hDiff;
        private System.Windows.Forms.SplitContainer spcMain;
        private System.Windows.Forms.CheckBox chkOrderDepth;
        private System.Windows.Forms.TextBox txtCcexApiKey;
        private System.Windows.Forms.GroupBox grpExchangePrice;
        private System.Windows.Forms.RadioButton radLowestAsk;
        private System.Windows.Forms.RadioButton radMostRecentTrade;
        private System.Windows.Forms.RadioButton radHighestBid;
        private System.Windows.Forms.GroupBox grpPriceSource;
        private System.Windows.Forms.RadioButton radFallThroughExchange;
        private System.Windows.Forms.RadioButton radVolumeExchange;
        private System.Windows.Forms.RadioButton radWeighted;
        private System.Windows.Forms.GroupBox grpFiatElectricityMultiplier;
        private System.Windows.Forms.TabControl tbcResults;
    }
}

