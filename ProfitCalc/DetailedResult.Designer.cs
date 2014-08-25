namespace ProfitCalc
{
    sealed partial class DetailedResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailedResult));
            this.spcDetailedResult = new System.Windows.Forms.SplitContainer();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtCoinsPerDay = new System.Windows.Forms.TextBox();
            this.lbCoinsPerDay = new System.Windows.Forms.Label();
            this.txtRetrieved = new System.Windows.Forms.TextBox();
            this.lblRetrieved = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtCnyPerDay = new System.Windows.Forms.TextBox();
            this.lblCnyPerDay = new System.Windows.Forms.Label();
            this.txtGbpPerDay = new System.Windows.Forms.TextBox();
            this.lblGbpPerDay = new System.Windows.Forms.Label();
            this.txtEurPerDay = new System.Windows.Forms.TextBox();
            this.lblEurPerDay = new System.Windows.Forms.Label();
            this.txtUsdPerDay = new System.Windows.Forms.TextBox();
            this.lblUsdPerDay = new System.Windows.Forms.Label();
            this.txtBtcPerDay = new System.Windows.Forms.TextBox();
            this.lblBtcPerDay = new System.Windows.Forms.Label();
            this.txtBlockReward = new System.Windows.Forms.TextBox();
            this.lblNetHashrate = new System.Windows.Forms.Label();
            this.txtBlockTime = new System.Windows.Forms.TextBox();
            this.lblBlockTime = new System.Windows.Forms.Label();
            this.txtNetHashrate = new System.Windows.Forms.TextBox();
            this.lblBlockReward = new System.Windows.Forms.Label();
            this.txt24HAvgDiff = new System.Windows.Forms.TextBox();
            this.lbl24HAvgDiff = new System.Windows.Forms.Label();
            this.txtDiff = new System.Windows.Forms.TextBox();
            this.lblDiff = new System.Windows.Forms.Label();
            this.txtAlgo = new System.Windows.Forms.TextBox();
            this.lblAlgo = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.lblTag = new System.Windows.Forms.Label();
            this.grpMarkets = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAllMarkets = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.spcDetailedResult)).BeginInit();
            this.spcDetailedResult.Panel1.SuspendLayout();
            this.spcDetailedResult.Panel2.SuspendLayout();
            this.spcDetailedResult.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.grpMarkets.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabAllMarkets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // spcDetailedResult
            // 
            this.spcDetailedResult.BackColor = System.Drawing.Color.Gainsboro;
            this.spcDetailedResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcDetailedResult.Location = new System.Drawing.Point(0, 0);
            this.spcDetailedResult.Name = "spcDetailedResult";
            // 
            // spcDetailedResult.Panel1
            // 
            this.spcDetailedResult.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.spcDetailedResult.Panel1.Controls.Add(this.grpGeneral);
            // 
            // spcDetailedResult.Panel2
            // 
            this.spcDetailedResult.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.spcDetailedResult.Panel2.Controls.Add(this.grpMarkets);
            this.spcDetailedResult.Size = new System.Drawing.Size(834, 515);
            this.spcDetailedResult.SplitterDistance = 199;
            this.spcDetailedResult.SplitterWidth = 5;
            this.spcDetailedResult.TabIndex = 0;
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.txtHeight);
            this.grpGeneral.Controls.Add(this.lblHeight);
            this.grpGeneral.Controls.Add(this.txtCoinsPerDay);
            this.grpGeneral.Controls.Add(this.lbCoinsPerDay);
            this.grpGeneral.Controls.Add(this.txtRetrieved);
            this.grpGeneral.Controls.Add(this.lblRetrieved);
            this.grpGeneral.Controls.Add(this.txtSource);
            this.grpGeneral.Controls.Add(this.lblSource);
            this.grpGeneral.Controls.Add(this.txtCnyPerDay);
            this.grpGeneral.Controls.Add(this.lblCnyPerDay);
            this.grpGeneral.Controls.Add(this.txtGbpPerDay);
            this.grpGeneral.Controls.Add(this.lblGbpPerDay);
            this.grpGeneral.Controls.Add(this.txtEurPerDay);
            this.grpGeneral.Controls.Add(this.lblEurPerDay);
            this.grpGeneral.Controls.Add(this.txtUsdPerDay);
            this.grpGeneral.Controls.Add(this.lblUsdPerDay);
            this.grpGeneral.Controls.Add(this.txtBtcPerDay);
            this.grpGeneral.Controls.Add(this.lblBtcPerDay);
            this.grpGeneral.Controls.Add(this.txtBlockReward);
            this.grpGeneral.Controls.Add(this.lblNetHashrate);
            this.grpGeneral.Controls.Add(this.txtBlockTime);
            this.grpGeneral.Controls.Add(this.lblBlockTime);
            this.grpGeneral.Controls.Add(this.txtNetHashrate);
            this.grpGeneral.Controls.Add(this.lblBlockReward);
            this.grpGeneral.Controls.Add(this.txt24HAvgDiff);
            this.grpGeneral.Controls.Add(this.lbl24HAvgDiff);
            this.grpGeneral.Controls.Add(this.txtDiff);
            this.grpGeneral.Controls.Add(this.lblDiff);
            this.grpGeneral.Controls.Add(this.txtAlgo);
            this.grpGeneral.Controls.Add(this.lblAlgo);
            this.grpGeneral.Controls.Add(this.txtName);
            this.grpGeneral.Controls.Add(this.lblName);
            this.grpGeneral.Controls.Add(this.txtTag);
            this.grpGeneral.Controls.Add(this.lblTag);
            this.grpGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGeneral.Location = new System.Drawing.Point(0, 0);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(199, 515);
            this.grpGeneral.TabIndex = 0;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(88, 175);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.ReadOnly = true;
            this.txtHeight.Size = new System.Drawing.Size(100, 20);
            this.txtHeight.TabIndex = 47;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(10, 178);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(41, 13);
            this.lblHeight.TabIndex = 46;
            this.lblHeight.Text = "Height:";
            // 
            // txtCoinsPerDay
            // 
            this.txtCoinsPerDay.Location = new System.Drawing.Point(88, 357);
            this.txtCoinsPerDay.Name = "txtCoinsPerDay";
            this.txtCoinsPerDay.ReadOnly = true;
            this.txtCoinsPerDay.Size = new System.Drawing.Size(100, 20);
            this.txtCoinsPerDay.TabIndex = 43;
            // 
            // lbCoinsPerDay
            // 
            this.lbCoinsPerDay.AutoSize = true;
            this.lbCoinsPerDay.Location = new System.Drawing.Point(10, 360);
            this.lbCoinsPerDay.Name = "lbCoinsPerDay";
            this.lbCoinsPerDay.Size = new System.Drawing.Size(58, 13);
            this.lbCoinsPerDay.TabIndex = 42;
            this.lbCoinsPerDay.Text = "Coins/day:";
            // 
            // txtRetrieved
            // 
            this.txtRetrieved.Location = new System.Drawing.Point(88, 123);
            this.txtRetrieved.Name = "txtRetrieved";
            this.txtRetrieved.ReadOnly = true;
            this.txtRetrieved.Size = new System.Drawing.Size(100, 20);
            this.txtRetrieved.TabIndex = 39;
            // 
            // lblRetrieved
            // 
            this.lblRetrieved.AutoSize = true;
            this.lblRetrieved.Location = new System.Drawing.Point(10, 126);
            this.lblRetrieved.Name = "lblRetrieved";
            this.lblRetrieved.Size = new System.Drawing.Size(56, 13);
            this.lblRetrieved.TabIndex = 38;
            this.lblRetrieved.Text = "Retrieved:";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(88, 97);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(100, 20);
            this.txtSource.TabIndex = 37;
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(10, 100);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(44, 13);
            this.lblSource.TabIndex = 36;
            this.lblSource.Text = "Source:";
            // 
            // txtCnyPerDay
            // 
            this.txtCnyPerDay.Location = new System.Drawing.Point(88, 487);
            this.txtCnyPerDay.Name = "txtCnyPerDay";
            this.txtCnyPerDay.ReadOnly = true;
            this.txtCnyPerDay.Size = new System.Drawing.Size(100, 20);
            this.txtCnyPerDay.TabIndex = 35;
            // 
            // lblCnyPerDay
            // 
            this.lblCnyPerDay.AutoSize = true;
            this.lblCnyPerDay.Location = new System.Drawing.Point(10, 490);
            this.lblCnyPerDay.Name = "lblCnyPerDay";
            this.lblCnyPerDay.Size = new System.Drawing.Size(54, 13);
            this.lblCnyPerDay.TabIndex = 34;
            this.lblCnyPerDay.Text = "CNY/day:";
            // 
            // txtGbpPerDay
            // 
            this.txtGbpPerDay.Location = new System.Drawing.Point(88, 461);
            this.txtGbpPerDay.Name = "txtGbpPerDay";
            this.txtGbpPerDay.ReadOnly = true;
            this.txtGbpPerDay.Size = new System.Drawing.Size(100, 20);
            this.txtGbpPerDay.TabIndex = 33;
            // 
            // lblGbpPerDay
            // 
            this.lblGbpPerDay.AutoSize = true;
            this.lblGbpPerDay.Location = new System.Drawing.Point(10, 464);
            this.lblGbpPerDay.Name = "lblGbpPerDay";
            this.lblGbpPerDay.Size = new System.Drawing.Size(54, 13);
            this.lblGbpPerDay.TabIndex = 32;
            this.lblGbpPerDay.Text = "GBP/day:";
            // 
            // txtEurPerDay
            // 
            this.txtEurPerDay.Location = new System.Drawing.Point(88, 435);
            this.txtEurPerDay.Name = "txtEurPerDay";
            this.txtEurPerDay.ReadOnly = true;
            this.txtEurPerDay.Size = new System.Drawing.Size(100, 20);
            this.txtEurPerDay.TabIndex = 31;
            // 
            // lblEurPerDay
            // 
            this.lblEurPerDay.AutoSize = true;
            this.lblEurPerDay.Location = new System.Drawing.Point(10, 438);
            this.lblEurPerDay.Name = "lblEurPerDay";
            this.lblEurPerDay.Size = new System.Drawing.Size(55, 13);
            this.lblEurPerDay.TabIndex = 30;
            this.lblEurPerDay.Text = "EUR/day:";
            // 
            // txtUsdPerDay
            // 
            this.txtUsdPerDay.Location = new System.Drawing.Point(88, 409);
            this.txtUsdPerDay.Name = "txtUsdPerDay";
            this.txtUsdPerDay.ReadOnly = true;
            this.txtUsdPerDay.Size = new System.Drawing.Size(100, 20);
            this.txtUsdPerDay.TabIndex = 29;
            // 
            // lblUsdPerDay
            // 
            this.lblUsdPerDay.AutoSize = true;
            this.lblUsdPerDay.Location = new System.Drawing.Point(10, 412);
            this.lblUsdPerDay.Name = "lblUsdPerDay";
            this.lblUsdPerDay.Size = new System.Drawing.Size(55, 13);
            this.lblUsdPerDay.TabIndex = 28;
            this.lblUsdPerDay.Text = "USD/day:";
            // 
            // txtBtcPerDay
            // 
            this.txtBtcPerDay.Location = new System.Drawing.Point(88, 383);
            this.txtBtcPerDay.Name = "txtBtcPerDay";
            this.txtBtcPerDay.ReadOnly = true;
            this.txtBtcPerDay.Size = new System.Drawing.Size(100, 20);
            this.txtBtcPerDay.TabIndex = 27;
            // 
            // lblBtcPerDay
            // 
            this.lblBtcPerDay.AutoSize = true;
            this.lblBtcPerDay.Location = new System.Drawing.Point(10, 386);
            this.lblBtcPerDay.Name = "lblBtcPerDay";
            this.lblBtcPerDay.Size = new System.Drawing.Size(53, 13);
            this.lblBtcPerDay.TabIndex = 26;
            this.lblBtcPerDay.Text = "BTC/day:";
            // 
            // txtBlockReward
            // 
            this.txtBlockReward.Location = new System.Drawing.Point(88, 305);
            this.txtBlockReward.Name = "txtBlockReward";
            this.txtBlockReward.ReadOnly = true;
            this.txtBlockReward.Size = new System.Drawing.Size(100, 20);
            this.txtBlockReward.TabIndex = 17;
            // 
            // lblNetHashrate
            // 
            this.lblNetHashrate.AutoSize = true;
            this.lblNetHashrate.Location = new System.Drawing.Point(10, 256);
            this.lblNetHashrate.Name = "lblNetHashrate";
            this.lblNetHashrate.Size = new System.Drawing.Size(71, 13);
            this.lblNetHashrate.TabIndex = 16;
            this.lblNetHashrate.Text = "Net hashrate:";
            // 
            // txtBlockTime
            // 
            this.txtBlockTime.Location = new System.Drawing.Point(88, 279);
            this.txtBlockTime.Name = "txtBlockTime";
            this.txtBlockTime.ReadOnly = true;
            this.txtBlockTime.Size = new System.Drawing.Size(100, 20);
            this.txtBlockTime.TabIndex = 15;
            // 
            // lblBlockTime
            // 
            this.lblBlockTime.AutoSize = true;
            this.lblBlockTime.Location = new System.Drawing.Point(10, 282);
            this.lblBlockTime.Name = "lblBlockTime";
            this.lblBlockTime.Size = new System.Drawing.Size(59, 13);
            this.lblBlockTime.TabIndex = 14;
            this.lblBlockTime.Text = "Block time:";
            // 
            // txtNetHashrate
            // 
            this.txtNetHashrate.Location = new System.Drawing.Point(88, 253);
            this.txtNetHashrate.Name = "txtNetHashrate";
            this.txtNetHashrate.ReadOnly = true;
            this.txtNetHashrate.Size = new System.Drawing.Size(100, 20);
            this.txtNetHashrate.TabIndex = 13;
            // 
            // lblBlockReward
            // 
            this.lblBlockReward.AutoSize = true;
            this.lblBlockReward.Location = new System.Drawing.Point(10, 308);
            this.lblBlockReward.Name = "lblBlockReward";
            this.lblBlockReward.Size = new System.Drawing.Size(72, 13);
            this.lblBlockReward.TabIndex = 12;
            this.lblBlockReward.Text = "Block reward:";
            // 
            // txt24HAvgDiff
            // 
            this.txt24HAvgDiff.Location = new System.Drawing.Point(88, 227);
            this.txt24HAvgDiff.Name = "txt24HAvgDiff";
            this.txt24HAvgDiff.ReadOnly = true;
            this.txt24HAvgDiff.Size = new System.Drawing.Size(100, 20);
            this.txt24HAvgDiff.TabIndex = 11;
            // 
            // lbl24HAvgDiff
            // 
            this.lbl24HAvgDiff.AutoSize = true;
            this.lbl24HAvgDiff.Location = new System.Drawing.Point(10, 230);
            this.lbl24HAvgDiff.Name = "lbl24HAvgDiff";
            this.lbl24HAvgDiff.Size = new System.Drawing.Size(66, 13);
            this.lbl24HAvgDiff.TabIndex = 10;
            this.lbl24HAvgDiff.Text = "24h avg diff:";
            // 
            // txtDiff
            // 
            this.txtDiff.Location = new System.Drawing.Point(88, 201);
            this.txtDiff.Name = "txtDiff";
            this.txtDiff.ReadOnly = true;
            this.txtDiff.Size = new System.Drawing.Size(100, 20);
            this.txtDiff.TabIndex = 9;
            // 
            // lblDiff
            // 
            this.lblDiff.AutoSize = true;
            this.lblDiff.Location = new System.Drawing.Point(10, 204);
            this.lblDiff.Name = "lblDiff";
            this.lblDiff.Size = new System.Drawing.Size(50, 13);
            this.lblDiff.TabIndex = 8;
            this.lblDiff.Text = "Difficulty:";
            // 
            // txtAlgo
            // 
            this.txtAlgo.Location = new System.Drawing.Point(88, 71);
            this.txtAlgo.Name = "txtAlgo";
            this.txtAlgo.ReadOnly = true;
            this.txtAlgo.Size = new System.Drawing.Size(100, 20);
            this.txtAlgo.TabIndex = 5;
            // 
            // lblAlgo
            // 
            this.lblAlgo.AutoSize = true;
            this.lblAlgo.Location = new System.Drawing.Point(10, 74);
            this.lblAlgo.Name = "lblAlgo";
            this.lblAlgo.Size = new System.Drawing.Size(53, 13);
            this.lblAlgo.TabIndex = 4;
            this.lblAlgo.Text = "Algorithm:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(88, 45);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(10, 48);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(55, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Full name:";
            // 
            // txtTag
            // 
            this.txtTag.Location = new System.Drawing.Point(88, 19);
            this.txtTag.Name = "txtTag";
            this.txtTag.ReadOnly = true;
            this.txtTag.Size = new System.Drawing.Size(100, 20);
            this.txtTag.TabIndex = 1;
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Location = new System.Drawing.Point(10, 22);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(29, 13);
            this.lblTag.TabIndex = 0;
            this.lblTag.Text = "Tag:";
            // 
            // grpMarkets
            // 
            this.grpMarkets.Controls.Add(this.tabControl1);
            this.grpMarkets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMarkets.Location = new System.Drawing.Point(0, 0);
            this.grpMarkets.Name = "grpMarkets";
            this.grpMarkets.Size = new System.Drawing.Size(630, 515);
            this.grpMarkets.TabIndex = 1;
            this.grpMarkets.TabStop = false;
            this.grpMarkets.Text = "Markets";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAllMarkets);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 496);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAllMarkets
            // 
            this.tabAllMarkets.Controls.Add(this.dataGridView1);
            this.tabAllMarkets.Location = new System.Drawing.Point(4, 22);
            this.tabAllMarkets.Name = "tabAllMarkets";
            this.tabAllMarkets.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllMarkets.Size = new System.Drawing.Size(616, 470);
            this.tabAllMarkets.TabIndex = 0;
            this.tabAllMarkets.Text = "All markets";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 133);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(154, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // DetailedResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 515);
            this.Controls.Add(this.spcDetailedResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DetailedResult";
            this.Text = "DetailedResult";
            this.spcDetailedResult.Panel1.ResumeLayout(false);
            this.spcDetailedResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcDetailedResult)).EndInit();
            this.spcDetailedResult.ResumeLayout(false);
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.grpMarkets.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabAllMarkets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcDetailedResult;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.GroupBox grpMarkets;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAllMarkets;
        private System.Windows.Forms.TextBox txtCnyPerDay;
        private System.Windows.Forms.Label lblCnyPerDay;
        private System.Windows.Forms.TextBox txtGbpPerDay;
        private System.Windows.Forms.Label lblGbpPerDay;
        private System.Windows.Forms.TextBox txtEurPerDay;
        private System.Windows.Forms.Label lblEurPerDay;
        private System.Windows.Forms.TextBox txtUsdPerDay;
        private System.Windows.Forms.Label lblUsdPerDay;
        private System.Windows.Forms.TextBox txtBtcPerDay;
        private System.Windows.Forms.Label lblBtcPerDay;
        private System.Windows.Forms.TextBox txtBlockReward;
        private System.Windows.Forms.Label lblNetHashrate;
        private System.Windows.Forms.TextBox txtBlockTime;
        private System.Windows.Forms.Label lblBlockTime;
        private System.Windows.Forms.TextBox txtNetHashrate;
        private System.Windows.Forms.Label lblBlockReward;
        private System.Windows.Forms.TextBox txt24HAvgDiff;
        private System.Windows.Forms.Label lbl24HAvgDiff;
        private System.Windows.Forms.TextBox txtDiff;
        private System.Windows.Forms.Label lblDiff;
        private System.Windows.Forms.TextBox txtAlgo;
        private System.Windows.Forms.Label lblAlgo;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.Label lblTag;
        private System.Windows.Forms.TextBox txtCoinsPerDay;
        private System.Windows.Forms.Label lbCoinsPerDay;
        private System.Windows.Forms.TextBox txtRetrieved;
        private System.Windows.Forms.Label lblRetrieved;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.DataGridView dataGridView1;

    }
}