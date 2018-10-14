namespace nitkagoshima_sysken.Procon29.Visualizer
{
    partial class TimeMeasurementForm
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
            this.TimeMeasurementGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.TimeMeasurementGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // TimeMeasurementGridView
            // 
            this.TimeMeasurementGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TimeMeasurementGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TimeMeasurementGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeMeasurementGridView.Location = new System.Drawing.Point(0, 0);
            this.TimeMeasurementGridView.Name = "TimeMeasurementGridView";
            this.TimeMeasurementGridView.RowTemplate.Height = 24;
            this.TimeMeasurementGridView.Size = new System.Drawing.Size(800, 450);
            this.TimeMeasurementGridView.TabIndex = 0;
            // 
            // TimeMeasurementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TimeMeasurementGridView);
            this.Name = "TimeMeasurementForm";
            this.Text = "TimeMeasurementForm";
            ((System.ComponentModel.ISupportInitialize)(this.TimeMeasurementGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TimeMeasurementGridView;
    }
}