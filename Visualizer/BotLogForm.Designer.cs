namespace nitkagoshima_sysken.Procon29.Visualizer
{
    partial class BotLogForm
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
            this.BotLogRichText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // BotLogRichText
            // 
            this.BotLogRichText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BotLogRichText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BotLogRichText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotLogRichText.Location = new System.Drawing.Point(0, 0);
            this.BotLogRichText.Name = "BotLogRichText";
            this.BotLogRichText.Size = new System.Drawing.Size(800, 450);
            this.BotLogRichText.TabIndex = 0;
            this.BotLogRichText.Text = "";
            // 
            // BotLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BotLogRichText);
            this.Name = "BotLogForm";
            this.Text = "Bot Log";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox BotLogRichText;
    }
}