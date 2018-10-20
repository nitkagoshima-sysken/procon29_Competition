namespace nitkagoshima_sysken.Procon29.Visualizer
{
    partial class CreateNewForm2
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
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.MaxTurnLabel = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.UsedFieldLabel = new System.Windows.Forms.Label();
            this.SlecetPQRFileButton = new System.Windows.Forms.Button();
            this.SelectedPQRFileNameLabel = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.SlecetPQRFileButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.UsedFieldLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.MaxTurnLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.OKButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.maskedTextBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.CancelButton, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.SelectedPQRFileNameLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBox1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(582, 302);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.OKButton.Location = new System.Drawing.Point(3, 228);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(285, 71);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "はじめる";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
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
            this.CancelButton.Location = new System.Drawing.Point(294, 228);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(285, 71);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "やめておく";
            this.CancelButton.UseVisualStyleBackColor = false;
            // 
            // MaxTurnLabel
            // 
            this.MaxTurnLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxTurnLabel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MaxTurnLabel.ForeColor = System.Drawing.Color.LightGray;
            this.MaxTurnLabel.Location = new System.Drawing.Point(3, 0);
            this.MaxTurnLabel.Name = "MaxTurnLabel";
            this.MaxTurnLabel.Size = new System.Drawing.Size(285, 75);
            this.MaxTurnLabel.TabIndex = 3;
            this.MaxTurnLabel.Text = "ターン数";
            this.MaxTurnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.maskedTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maskedTextBox1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.maskedTextBox1.Location = new System.Drawing.Point(294, 19);
            this.maskedTextBox1.Mask = "99";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(285, 37);
            this.maskedTextBox1.TabIndex = 4;
            this.maskedTextBox1.Text = "40";
            // 
            // UsedFieldLabel
            // 
            this.UsedFieldLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UsedFieldLabel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UsedFieldLabel.ForeColor = System.Drawing.Color.LightGray;
            this.UsedFieldLabel.Location = new System.Drawing.Point(3, 75);
            this.UsedFieldLabel.Name = "UsedFieldLabel";
            this.UsedFieldLabel.Size = new System.Drawing.Size(285, 75);
            this.UsedFieldLabel.TabIndex = 5;
            this.UsedFieldLabel.Text = "PQRファイル";
            this.UsedFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SlecetPQRFileButton
            // 
            this.SlecetPQRFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SlecetPQRFileButton.BackColor = System.Drawing.Color.DimGray;
            this.SlecetPQRFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SlecetPQRFileButton.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SlecetPQRFileButton.ForeColor = System.Drawing.Color.LightGray;
            this.SlecetPQRFileButton.Location = new System.Drawing.Point(294, 78);
            this.SlecetPQRFileButton.Name = "SlecetPQRFileButton";
            this.SlecetPQRFileButton.Size = new System.Drawing.Size(285, 69);
            this.SlecetPQRFileButton.TabIndex = 7;
            this.SlecetPQRFileButton.Text = "開く";
            this.SlecetPQRFileButton.UseVisualStyleBackColor = false;
            // 
            // SelectedPQRFileNameLabel
            // 
            this.SelectedPQRFileNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedPQRFileNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.SelectedPQRFileNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SelectedPQRFileNameLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedPQRFileNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.SelectedPQRFileNameLabel.Location = new System.Drawing.Point(294, 168);
            this.SelectedPQRFileNameLabel.Multiline = true;
            this.SelectedPQRFileNameLabel.Name = "SelectedPQRFileNameLabel";
            this.SelectedPQRFileNameLabel.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.SelectedPQRFileNameLabel.Size = new System.Drawing.Size(285, 38);
            this.SelectedPQRFileNameLabel.TabIndex = 8;
            this.SelectedPQRFileNameLabel.Text = "HVA001";
            this.SelectedPQRFileNameLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.comboBox1.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "デフォルト（既定値）",
            "Visualizer 共通フィールド",
            "ファイルから直接開く"});
            this.comboBox1.Location = new System.Drawing.Point(3, 171);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(285, 33);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.Text = "Visualizer 共通フィールド";
            // 
            // CreateNewForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(582, 303);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CreateNewForm2";
            this.Text = "新規作成";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label MaxTurnLabel;
        public System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.Label UsedFieldLabel;
        private System.Windows.Forms.Button SlecetPQRFileButton;
        private System.Windows.Forms.TextBox SelectedPQRFileNameLabel;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}