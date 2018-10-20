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
            this.SlecetPQRFileButton = new System.Windows.Forms.Button();
            this.UsedFieldLabel = new System.Windows.Forms.Label();
            this.MaxTurnLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.MaxTurnMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SelectedPQRFileNameLabel = new System.Windows.Forms.TextBox();
            this.FieldKindComboBox = new System.Windows.Forms.ComboBox();
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
            this.tableLayoutPanel1.Controls.Add(this.MaxTurnMaskedTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.CancelButton, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.SelectedPQRFileNameLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.FieldKindComboBox, 0, 2);
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
            this.SlecetPQRFileButton.Click += new System.EventHandler(this.SlecetPQRFileButton_Click);
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
            // 
            // MaxTurnMaskedTextBox
            // 
            this.MaxTurnMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxTurnMaskedTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MaxTurnMaskedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxTurnMaskedTextBox.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MaxTurnMaskedTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MaxTurnMaskedTextBox.Location = new System.Drawing.Point(294, 19);
            this.MaxTurnMaskedTextBox.Mask = "99";
            this.MaxTurnMaskedTextBox.Name = "MaxTurnMaskedTextBox";
            this.MaxTurnMaskedTextBox.Size = new System.Drawing.Size(285, 37);
            this.MaxTurnMaskedTextBox.TabIndex = 4;
            this.MaxTurnMaskedTextBox.Text = "40";
            this.MaxTurnMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
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
            // FieldKindComboBox
            // 
            this.FieldKindComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldKindComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.FieldKindComboBox.Font = new System.Drawing.Font("メイリオ", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FieldKindComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FieldKindComboBox.FormattingEnabled = true;
            this.FieldKindComboBox.Items.AddRange(new object[] {
            "デフォルト（既定値）",
            "Visualizer 共通フィールド",
            "ファイルから直接開く"});
            this.FieldKindComboBox.Location = new System.Drawing.Point(3, 171);
            this.FieldKindComboBox.Name = "FieldKindComboBox";
            this.FieldKindComboBox.Size = new System.Drawing.Size(285, 33);
            this.FieldKindComboBox.TabIndex = 9;
            this.FieldKindComboBox.Text = "Visualizer 共通フィールド";
            this.FieldKindComboBox.SelectedIndexChanged += new System.EventHandler(this.FieldKindComboBox_SelectedIndexChanged);
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
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label MaxTurnLabel;
        public System.Windows.Forms.MaskedTextBox MaxTurnMaskedTextBox;
        private System.Windows.Forms.Label UsedFieldLabel;
        private System.Windows.Forms.Button SlecetPQRFileButton;
        public System.Windows.Forms.Button OKButton;
        public System.Windows.Forms.TextBox SelectedPQRFileNameLabel;
        public System.Windows.Forms.ComboBox FieldKindComboBox;
    }
}