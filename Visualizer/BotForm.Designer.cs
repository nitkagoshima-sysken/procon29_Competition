namespace nitkagoshima_sysken.Procon29.Visualizer
{
    partial class BotForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.OragenBotOpenButton = new System.Windows.Forms.Button();
            this.LimeBotOpenButton = new System.Windows.Forms.Button();
            this.SelectedOrangeBotNameLabel = new System.Windows.Forms.TextBox();
            this.SelectedLimeBotNameLabel = new System.Windows.Forms.TextBox();
            this.OrangeBotKindComboBox = new System.Windows.Forms.ComboBox();
            this.LimeBotKindComboBox = new System.Windows.Forms.ComboBox();
            this.OrangeLabel = new System.Windows.Forms.Label();
            this.LimeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.LimeLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.OrangeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.OrangeBotKindComboBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.OKButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.CancelButton, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.OragenBotOpenButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LimeBotOpenButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.SelectedOrangeBotNameLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.SelectedLimeBotNameLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.LimeBotKindComboBox, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.BackColor = System.Drawing.Color.DimGray;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CancelButton.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CancelButton.ForeColor = System.Drawing.Color.LightGray;
            this.CancelButton.Location = new System.Drawing.Point(403, 363);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(394, 84);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "やめておく";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OKButton.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OKButton.ForeColor = System.Drawing.Color.LightGray;
            this.OKButton.Location = new System.Drawing.Point(3, 363);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(394, 84);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "はじめる";
            this.OKButton.UseVisualStyleBackColor = false;
            // 
            // OragenBotOpenButton
            // 
            this.OragenBotOpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OragenBotOpenButton.BackColor = System.Drawing.Color.DimGray;
            this.OragenBotOpenButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OragenBotOpenButton.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OragenBotOpenButton.ForeColor = System.Drawing.Color.LightGray;
            this.OragenBotOpenButton.Location = new System.Drawing.Point(403, 3);
            this.OragenBotOpenButton.Name = "OragenBotOpenButton";
            this.OragenBotOpenButton.Size = new System.Drawing.Size(394, 84);
            this.OragenBotOpenButton.TabIndex = 5;
            this.OragenBotOpenButton.Text = "開く";
            this.OragenBotOpenButton.UseVisualStyleBackColor = false;
            this.OragenBotOpenButton.Click += new System.EventHandler(this.OragenBotOpenButton_Click);
            // 
            // LimeBotOpenButton
            // 
            this.LimeBotOpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LimeBotOpenButton.BackColor = System.Drawing.Color.DimGray;
            this.LimeBotOpenButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LimeBotOpenButton.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LimeBotOpenButton.ForeColor = System.Drawing.Color.LightGray;
            this.LimeBotOpenButton.Location = new System.Drawing.Point(403, 183);
            this.LimeBotOpenButton.Name = "LimeBotOpenButton";
            this.LimeBotOpenButton.Size = new System.Drawing.Size(394, 84);
            this.LimeBotOpenButton.TabIndex = 6;
            this.LimeBotOpenButton.Text = "開く";
            this.LimeBotOpenButton.UseVisualStyleBackColor = false;
            this.LimeBotOpenButton.Click += new System.EventHandler(this.LimeBotOpenButton_Click);
            // 
            // SelectedOrangeBotNameLabel
            // 
            this.SelectedOrangeBotNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedOrangeBotNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.SelectedOrangeBotNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SelectedOrangeBotNameLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedOrangeBotNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SelectedOrangeBotNameLabel.Location = new System.Drawing.Point(403, 116);
            this.SelectedOrangeBotNameLabel.Multiline = true;
            this.SelectedOrangeBotNameLabel.Name = "SelectedOrangeBotNameLabel";
            this.SelectedOrangeBotNameLabel.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.SelectedOrangeBotNameLabel.Size = new System.Drawing.Size(394, 38);
            this.SelectedOrangeBotNameLabel.TabIndex = 9;
            this.SelectedOrangeBotNameLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SelectedLimeBotNameLabel
            // 
            this.SelectedLimeBotNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedLimeBotNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.SelectedLimeBotNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SelectedLimeBotNameLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedLimeBotNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SelectedLimeBotNameLabel.Location = new System.Drawing.Point(403, 296);
            this.SelectedLimeBotNameLabel.Multiline = true;
            this.SelectedLimeBotNameLabel.Name = "SelectedLimeBotNameLabel";
            this.SelectedLimeBotNameLabel.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.SelectedLimeBotNameLabel.Size = new System.Drawing.Size(394, 38);
            this.SelectedLimeBotNameLabel.TabIndex = 10;
            this.SelectedLimeBotNameLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OrangeBotKindComboBox
            // 
            this.OrangeBotKindComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.OrangeBotKindComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OrangeBotKindComboBox.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OrangeBotKindComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OrangeBotKindComboBox.FormattingEnabled = true;
            this.OrangeBotKindComboBox.Items.AddRange(new object[] {
            "人間",
            "ボット",
            "Hydro Go Bot"});
            this.OrangeBotKindComboBox.Location = new System.Drawing.Point(3, 118);
            this.OrangeBotKindComboBox.Name = "OrangeBotKindComboBox";
            this.OrangeBotKindComboBox.Size = new System.Drawing.Size(394, 33);
            this.OrangeBotKindComboBox.TabIndex = 11;
            this.OrangeBotKindComboBox.Text = "人間";
            // 
            // LimeBotKindComboBox
            // 
            this.LimeBotKindComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LimeBotKindComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.LimeBotKindComboBox.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LimeBotKindComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LimeBotKindComboBox.FormattingEnabled = true;
            this.LimeBotKindComboBox.Items.AddRange(new object[] {
            "人間",
            "ボット",
            "Hydro Go Bot"});
            this.LimeBotKindComboBox.Location = new System.Drawing.Point(3, 298);
            this.LimeBotKindComboBox.Name = "LimeBotKindComboBox";
            this.LimeBotKindComboBox.Size = new System.Drawing.Size(394, 33);
            this.LimeBotKindComboBox.TabIndex = 12;
            this.LimeBotKindComboBox.Text = "人間";
            // 
            // OrangeLabel
            // 
            this.OrangeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrangeLabel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OrangeLabel.ForeColor = System.Drawing.Color.LightGray;
            this.OrangeLabel.Location = new System.Drawing.Point(3, 0);
            this.OrangeLabel.Name = "OrangeLabel";
            this.OrangeLabel.Size = new System.Drawing.Size(394, 90);
            this.OrangeLabel.TabIndex = 13;
            this.OrangeLabel.Text = "オレンジチーム";
            this.OrangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LimeLabel
            // 
            this.LimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LimeLabel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LimeLabel.ForeColor = System.Drawing.Color.LightGray;
            this.LimeLabel.Location = new System.Drawing.Point(3, 180);
            this.LimeLabel.Name = "LimeLabel";
            this.LimeLabel.Size = new System.Drawing.Size(394, 90);
            this.LimeLabel.TabIndex = 14;
            this.LimeLabel.Text = "ライムチーム";
            this.LimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BotForm";
            this.Text = "BotForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button CancelButton;
        public System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button OragenBotOpenButton;
        private System.Windows.Forms.Button LimeBotOpenButton;
        public System.Windows.Forms.TextBox SelectedOrangeBotNameLabel;
        public System.Windows.Forms.TextBox SelectedLimeBotNameLabel;
        public System.Windows.Forms.ComboBox OrangeBotKindComboBox;
        public System.Windows.Forms.ComboBox LimeBotKindComboBox;
        private System.Windows.Forms.Label LimeLabel;
        private System.Windows.Forms.Label OrangeLabel;
    }
}