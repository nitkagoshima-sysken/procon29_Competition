namespace nitkagoshima_sysken.Procon29.Visualizer
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FieldDisplay = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.TrumpPictureBox = new System.Windows.Forms.PictureBox();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.TurnEndButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateNew2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.OpenQRCodeReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFieldDataGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SwapAgentPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.EndButtleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BotConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeMeasurementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BotWarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PracticeModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProductionModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.CellInformationToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AreaToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.ChangeAgentNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FieldDisplay)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrumpPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.FieldDisplay, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(757, 394);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // FieldDisplay
            // 
            this.FieldDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldDisplay.Location = new System.Drawing.Point(4, 4);
            this.FieldDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.FieldDisplay.Name = "FieldDisplay";
            this.FieldDisplay.Size = new System.Drawing.Size(521, 386);
            this.FieldDisplay.TabIndex = 0;
            this.FieldDisplay.TabStop = false;
            this.FieldDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FieldDisplay_MouseClick);
            this.FieldDisplay.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FieldDisplay_MouseDoubleClick);
            this.FieldDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FieldDisplay_MouseDown);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.TrumpPictureBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.messageBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.TurnEndButton, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(532, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(222, 388);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // TrumpPictureBox
            // 
            this.TrumpPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrumpPictureBox.Location = new System.Drawing.Point(3, 3);
            this.TrumpPictureBox.Name = "TrumpPictureBox";
            this.TrumpPictureBox.Size = new System.Drawing.Size(216, 1);
            this.TrumpPictureBox.TabIndex = 2;
            this.TrumpPictureBox.TabStop = false;
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.messageBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageBox.ForeColor = System.Drawing.Color.LightGray;
            this.messageBox.Location = new System.Drawing.Point(3, 3);
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = true;
            this.messageBox.Size = new System.Drawing.Size(216, 304);
            this.messageBox.TabIndex = 1;
            this.messageBox.Text = "";
            // 
            // TurnEndButton
            // 
            this.TurnEndButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TurnEndButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.TurnEndButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TurnEndButton.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TurnEndButton.ForeColor = System.Drawing.Color.LightGray;
            this.TurnEndButton.Location = new System.Drawing.Point(3, 313);
            this.TurnEndButton.Name = "TurnEndButton";
            this.TurnEndButton.Size = new System.Drawing.Size(216, 72);
            this.TurnEndButton.TabIndex = 3;
            this.TurnEndButton.Text = "ターンエンド";
            this.TurnEndButton.UseVisualStyleBackColor = false;
            this.TurnEndButton.Click += new System.EventHandler(this.TurnEndButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.ModeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(782, 31);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateNew2ToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.SaveAsSToolStripMenuItem,
            this.toolStripSeparator1,
            this.OpenQRCodeReaderToolStripMenuItem,
            this.OpenFieldDataGeneratorToolStripMenuItem});
            this.FileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(105, 27);
            this.FileToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // CreateNew2ToolStripMenuItem
            // 
            this.CreateNew2ToolStripMenuItem.Name = "CreateNew2ToolStripMenuItem";
            this.CreateNew2ToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.CreateNew2ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.CreateNew2ToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.CreateNew2ToolStripMenuItem.Text = "新規作成(&N)";
            this.CreateNew2ToolStripMenuItem.Click += new System.EventHandler(this.CreateNewToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.OpenToolStripMenuItem.Text = "開く(&O)";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.SaveToolStripMenuItem.Text = "上書き保存(&S)";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // SaveAsSToolStripMenuItem
            // 
            this.SaveAsSToolStripMenuItem.Name = "SaveAsSToolStripMenuItem";
            this.SaveAsSToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.SaveAsSToolStripMenuItem.Text = "名前をつけて保存(&A)";
            this.SaveAsSToolStripMenuItem.Click += new System.EventHandler(this.SaveAsSToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(305, 6);
            // 
            // OpenQRCodeReaderToolStripMenuItem
            // 
            this.OpenQRCodeReaderToolStripMenuItem.Name = "OpenQRCodeReaderToolStripMenuItem";
            this.OpenQRCodeReaderToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.OpenQRCodeReaderToolStripMenuItem.Text = "QR Code Readerを開く(&Q)";
            this.OpenQRCodeReaderToolStripMenuItem.Click += new System.EventHandler(this.OpenQRCodeReaderToolStripMenuItem_Click);
            // 
            // OpenFieldDataGeneratorToolStripMenuItem
            // 
            this.OpenFieldDataGeneratorToolStripMenuItem.Name = "OpenFieldDataGeneratorToolStripMenuItem";
            this.OpenFieldDataGeneratorToolStripMenuItem.Size = new System.Drawing.Size(308, 28);
            this.OpenFieldDataGeneratorToolStripMenuItem.Text = "Field Data Generatorを開く(&F)";
            this.OpenFieldDataGeneratorToolStripMenuItem.Click += new System.EventHandler(this.OpenFieldDataGeneratorToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoToolStripMenuItem,
            this.RedoToolStripMenuItem,
            this.toolStripSeparator3,
            this.SelectBotToolStripMenuItem,
            this.SwapAgentPositionToolStripMenuItem,
            this.ChangeAgentNameToolStripMenuItem,
            this.toolStripSeparator2,
            this.EndButtleToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(75, 27);
            this.EditToolStripMenuItem.Text = "編集(&E)";
            // 
            // UndoToolStripMenuItem
            // 
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.UndoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(296, 28);
            this.UndoToolStripMenuItem.Text = "元に戻す(&U)";
            this.UndoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
            // 
            // RedoToolStripMenuItem
            // 
            this.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem";
            this.RedoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Y";
            this.RedoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.RedoToolStripMenuItem.Size = new System.Drawing.Size(296, 28);
            this.RedoToolStripMenuItem.Text = "やり直し(&R)";
            this.RedoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(293, 6);
            // 
            // SelectBotToolStripMenuItem
            // 
            this.SelectBotToolStripMenuItem.Name = "SelectBotToolStripMenuItem";
            this.SelectBotToolStripMenuItem.Size = new System.Drawing.Size(296, 28);
            this.SelectBotToolStripMenuItem.Text = "ボットを選択する";
            this.SelectBotToolStripMenuItem.Click += new System.EventHandler(this.SelectBotToolStripMenuItem_Click);
            // 
            // SwapAgentPositionToolStripMenuItem
            // 
            this.SwapAgentPositionToolStripMenuItem.Name = "SwapAgentPositionToolStripMenuItem";
            this.SwapAgentPositionToolStripMenuItem.Size = new System.Drawing.Size(296, 28);
            this.SwapAgentPositionToolStripMenuItem.Text = "エージェントの位置を交換する";
            this.SwapAgentPositionToolStripMenuItem.Click += new System.EventHandler(this.SwapAgentPositionToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(293, 6);
            // 
            // EndButtleToolStripMenuItem
            // 
            this.EndButtleToolStripMenuItem.Name = "EndButtleToolStripMenuItem";
            this.EndButtleToolStripMenuItem.Size = new System.Drawing.Size(296, 28);
            this.EndButtleToolStripMenuItem.Text = "試合を終わらせる";
            this.EndButtleToolStripMenuItem.Click += new System.EventHandler(this.EndButtleToolStripMenuItem_Click);
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BotConsoleToolStripMenuItem,
            this.TimeMeasurementToolStripMenuItem,
            this.TrumpToolStripMenuItem,
            this.BotWarsToolStripMenuItem});
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(76, 27);
            this.ViewToolStripMenuItem.Text = "表示(&V)";
            // 
            // BotConsoleToolStripMenuItem
            // 
            this.BotConsoleToolStripMenuItem.Name = "BotConsoleToolStripMenuItem";
            this.BotConsoleToolStripMenuItem.Size = new System.Drawing.Size(206, 28);
            this.BotConsoleToolStripMenuItem.Text = "ボットコンソール";
            this.BotConsoleToolStripMenuItem.Click += new System.EventHandler(this.BotConsoleToolStripMenuItem_Click);
            // 
            // TimeMeasurementToolStripMenuItem
            // 
            this.TimeMeasurementToolStripMenuItem.Name = "TimeMeasurementToolStripMenuItem";
            this.TimeMeasurementToolStripMenuItem.Size = new System.Drawing.Size(206, 28);
            this.TimeMeasurementToolStripMenuItem.Text = "処理時間計測";
            this.TimeMeasurementToolStripMenuItem.Click += new System.EventHandler(this.TimeMeasurementToolStripMenuItem_Click);
            // 
            // TrumpToolStripMenuItem
            // 
            this.TrumpToolStripMenuItem.Name = "TrumpToolStripMenuItem";
            this.TrumpToolStripMenuItem.Size = new System.Drawing.Size(206, 28);
            this.TrumpToolStripMenuItem.Text = "トランプ";
            this.TrumpToolStripMenuItem.Click += new System.EventHandler(this.TrumpToolStripMenuItem_Click);
            // 
            // BotWarsToolStripMenuItem
            // 
            this.BotWarsToolStripMenuItem.Name = "BotWarsToolStripMenuItem";
            this.BotWarsToolStripMenuItem.Size = new System.Drawing.Size(206, 28);
            this.BotWarsToolStripMenuItem.Text = "Bot Wars";
            this.BotWarsToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // ModeToolStripMenuItem
            // 
            this.ModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PracticeModeToolStripMenuItem,
            this.ProductionModeToolStripMenuItem});
            this.ModeToolStripMenuItem.Name = "ModeToolStripMenuItem";
            this.ModeToolStripMenuItem.Size = new System.Drawing.Size(93, 27);
            this.ModeToolStripMenuItem.Text = "モード(&M)";
            // 
            // PracticeModeToolStripMenuItem
            // 
            this.PracticeModeToolStripMenuItem.Name = "PracticeModeToolStripMenuItem";
            this.PracticeModeToolStripMenuItem.Size = new System.Drawing.Size(161, 28);
            this.PracticeModeToolStripMenuItem.Text = "練習モード";
            this.PracticeModeToolStripMenuItem.Click += new System.EventHandler(this.PracticeModeToolStripMenuItem_Click);
            // 
            // ProductionModeToolStripMenuItem
            // 
            this.ProductionModeToolStripMenuItem.Name = "ProductionModeToolStripMenuItem";
            this.ProductionModeToolStripMenuItem.Size = new System.Drawing.Size(161, 28);
            this.ProductionModeToolStripMenuItem.Text = "本番モード";
            this.ProductionModeToolStripMenuItem.Click += new System.EventHandler(this.ProductionModeToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.CellInformationToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 429);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(782, 24);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 18);
            // 
            // CellInformationToolStripStatusLabel
            // 
            this.CellInformationToolStripStatusLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CellInformationToolStripStatusLabel.ForeColor = System.Drawing.Color.LightGray;
            this.CellInformationToolStripStatusLabel.Name = "CellInformationToolStripStatusLabel";
            this.CellInformationToolStripStatusLabel.Size = new System.Drawing.Size(80, 19);
            this.CellInformationToolStripStatusLabel.Text = "{} Point:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ContextMenuStrip
            // 
            this.ContextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AreaToolStripComboBox});
            this.ContextMenuStrip.Name = "ContextMenuStrip";
            this.ContextMenuStrip.Size = new System.Drawing.Size(182, 39);
            // 
            // AreaToolStripComboBox
            // 
            this.AreaToolStripComboBox.Items.AddRange(new object[] {
            "自分のチーム",
            "敵のチーム",
            "未開拓"});
            this.AreaToolStripComboBox.Name = "AreaToolStripComboBox";
            this.AreaToolStripComboBox.Size = new System.Drawing.Size(121, 31);
            this.AreaToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.AreaToolStripComboBox_SelectedIndexChanged);
            // 
            // ChangeAgentNameToolStripMenuItem
            // 
            this.ChangeAgentNameToolStripMenuItem.Name = "ChangeAgentNameToolStripMenuItem";
            this.ChangeAgentNameToolStripMenuItem.Size = new System.Drawing.Size(296, 28);
            this.ChangeAgentNameToolStripMenuItem.Text = "エージェントの名前を変更する";
            this.ChangeAgentNameToolStripMenuItem.Click += new System.EventHandler(this.ChangeAgentNameToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.Opacity = 0.95D;
            this.Text = "Procon29 Visualizar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldDisplay_MouseMove);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FieldDisplay)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TrumpPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox FieldDisplay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.PictureBox TrumpPictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel CellInformationToolStripStatusLabel;
        private System.Windows.Forms.Button TurnEndButton;
        private System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RedoToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsSToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem ModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PracticeModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProductionModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem OpenQRCodeReaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFieldDataGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TimeMeasurementToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip;
        private System.Windows.Forms.ToolStripComboBox AreaToolStripComboBox;
        private System.Windows.Forms.ToolStripMenuItem TrumpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem EndButtleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BotConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateNew2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem SelectBotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BotWarsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SwapAgentPositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeAgentNameToolStripMenuItem;
    }
}
