namespace nitkagoshima_sysken
{
    namespace Procon29
    {
        namespace Visualizer
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
                    this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
                    this.FieldDisplay = new System.Windows.Forms.PictureBox();
                    this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
                    this.pictureBox2 = new System.Windows.Forms.PictureBox();
                    this.messageBox = new System.Windows.Forms.RichTextBox();
                    this.TurnEndButton = new System.Windows.Forms.Button();
                    this.menuStrip1 = new System.Windows.Forms.MenuStrip();
                    this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    this.CreateNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    this.RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                    this.statusStrip1 = new System.Windows.Forms.StatusStrip();
                    this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
                    this.tableLayoutPanel1.SuspendLayout();
                    ((System.ComponentModel.ISupportInitialize)(this.FieldDisplay)).BeginInit();
                    this.tableLayoutPanel2.SuspendLayout();
                    ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
                    this.menuStrip1.SuspendLayout();
                    this.statusStrip1.SuspendLayout();
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
                    // 
                    // tableLayoutPanel2
                    // 
                    this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                    this.tableLayoutPanel2.ColumnCount = 1;
                    this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
                    this.tableLayoutPanel2.Controls.Add(this.pictureBox2, 0, 0);
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
                    // pictureBox2
                    // 
                    this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                    this.pictureBox2.Location = new System.Drawing.Point(153, 3);
                    this.pictureBox2.Name = "pictureBox2";
                    this.pictureBox2.Size = new System.Drawing.Size(66, 1);
                    this.pictureBox2.TabIndex = 2;
                    this.pictureBox2.TabStop = false;
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
            this.ViewToolStripMenuItem});
                    this.menuStrip1.Location = new System.Drawing.Point(0, 0);
                    this.menuStrip1.Name = "menuStrip1";
                    this.menuStrip1.Size = new System.Drawing.Size(782, 31);
                    this.menuStrip1.TabIndex = 4;
                    this.menuStrip1.Text = "menuStrip1";
                    // 
                    // FileToolStripMenuItem
                    // 
                    this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateNewToolStripMenuItem,
            this.OpenToolStripMenuItem});
                    this.FileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
                    this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
                    this.FileToolStripMenuItem.Size = new System.Drawing.Size(105, 27);
                    this.FileToolStripMenuItem.Text = "ファイル(&F)";
                    // 
                    // CreateNewToolStripMenuItem
                    // 
                    this.CreateNewToolStripMenuItem.Name = "CreateNewToolStripMenuItem";
                    this.CreateNewToolStripMenuItem.Size = new System.Drawing.Size(171, 28);
                    this.CreateNewToolStripMenuItem.Text = "新規作成(&N)";
                    this.CreateNewToolStripMenuItem.Click += new System.EventHandler(this.CreateNewToolStripMenuItem_Click);
                    // 
                    // OpenToolStripMenuItem
                    // 
                    this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
                    this.OpenToolStripMenuItem.Size = new System.Drawing.Size(171, 28);
                    this.OpenToolStripMenuItem.Text = "開く(&O)";
                    this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
                    // 
                    // EditToolStripMenuItem
                    // 
                    this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoToolStripMenuItem,
            this.RedoToolStripMenuItem});
                    this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
                    this.EditToolStripMenuItem.Size = new System.Drawing.Size(75, 27);
                    this.EditToolStripMenuItem.Text = "編集(&E)";
                    // 
                    // UndoToolStripMenuItem
                    // 
                    this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
                    this.UndoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
                    this.UndoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
                    this.UndoToolStripMenuItem.Size = new System.Drawing.Size(229, 28);
                    this.UndoToolStripMenuItem.Text = "元に戻す(&U)";
                    this.UndoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
                    // 
                    // RedoToolStripMenuItem
                    // 
                    this.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem";
                    this.RedoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Y";
                    this.RedoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
                    this.RedoToolStripMenuItem.Size = new System.Drawing.Size(229, 28);
                    this.RedoToolStripMenuItem.Text = "やり直し(&R)";
                    this.RedoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
                    // 
                    // ViewToolStripMenuItem
                    // 
                    this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
                    this.ViewToolStripMenuItem.Size = new System.Drawing.Size(76, 27);
                    this.ViewToolStripMenuItem.Text = "表示(&V)";
                    // 
                    // statusStrip1
                    // 
                    this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
                    this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
                    this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
                    this.statusStrip1.Location = new System.Drawing.Point(0, 430);
                    this.statusStrip1.Name = "statusStrip1";
                    this.statusStrip1.Size = new System.Drawing.Size(782, 23);
                    this.statusStrip1.TabIndex = 5;
                    this.statusStrip1.Text = "statusStrip1";
                    // 
                    // toolStripStatusLabel1
                    // 
                    this.toolStripStatusLabel1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.LightGray;
                    this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
                    this.toolStripStatusLabel1.Size = new System.Drawing.Size(80, 18);
                    this.toolStripStatusLabel1.Text = "{} Point:";
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
                    this.Name = "MainForm";
                    this.Text = "Procon29 Visualizar";
                    this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
                    this.tableLayoutPanel1.ResumeLayout(false);
                    ((System.ComponentModel.ISupportInitialize)(this.FieldDisplay)).EndInit();
                    this.tableLayoutPanel2.ResumeLayout(false);
                    ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
                    this.menuStrip1.ResumeLayout(false);
                    this.menuStrip1.PerformLayout();
                    this.statusStrip1.ResumeLayout(false);
                    this.statusStrip1.PerformLayout();
                    this.ResumeLayout(false);
                    this.PerformLayout();

                }

                #endregion

                private System.Windows.Forms.PictureBox FieldDisplay;
                private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
                private System.Windows.Forms.RichTextBox messageBox;
                private System.Windows.Forms.PictureBox pictureBox2;
                private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
                private System.Windows.Forms.MenuStrip menuStrip1;
                private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
                private System.Windows.Forms.StatusStrip statusStrip1;
                private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
                private System.Windows.Forms.ToolStripMenuItem CreateNewToolStripMenuItem;
                private System.Windows.Forms.Button TurnEndButton;
                private System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem;
                private System.Windows.Forms.ToolStripMenuItem RedoToolStripMenuItem;
            }
        }
    }
}