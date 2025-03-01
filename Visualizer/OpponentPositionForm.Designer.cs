﻿namespace nitkagoshima_sysken.Procon29.Visualizer
{
    partial class OpponentPositionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.OpponentPosition1X = new System.Windows.Forms.MaskedTextBox();
            this.OpponentPosition1Y = new System.Windows.Forms.MaskedTextBox();
            this.OpponentPosition2X = new System.Windows.Forms.MaskedTextBox();
            this.OpponentPosition2Y = new System.Windows.Forms.MaskedTextBox();
            this.OurTeamPositionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "1人目の位置(X, Y)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "2人目の位置(X, Y)";
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OKButton.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OKButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OKButton.Location = new System.Drawing.Point(12, 181);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(204, 47);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // OpponentPosition1X
            // 
            this.OpponentPosition1X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OpponentPosition1X.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OpponentPosition1X.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpponentPosition1X.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OpponentPosition1X.Location = new System.Drawing.Point(12, 42);
            this.OpponentPosition1X.Mask = "99";
            this.OpponentPosition1X.Name = "OpponentPosition1X";
            this.OpponentPosition1X.Size = new System.Drawing.Size(100, 30);
            this.OpponentPosition1X.TabIndex = 7;
            // 
            // OpponentPosition1Y
            // 
            this.OpponentPosition1Y.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OpponentPosition1Y.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OpponentPosition1Y.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpponentPosition1Y.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OpponentPosition1Y.Location = new System.Drawing.Point(118, 42);
            this.OpponentPosition1Y.Mask = "99";
            this.OpponentPosition1Y.Name = "OpponentPosition1Y";
            this.OpponentPosition1Y.Size = new System.Drawing.Size(100, 30);
            this.OpponentPosition1Y.TabIndex = 8;
            // 
            // OpponentPosition2X
            // 
            this.OpponentPosition2X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OpponentPosition2X.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OpponentPosition2X.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpponentPosition2X.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OpponentPosition2X.Location = new System.Drawing.Point(12, 115);
            this.OpponentPosition2X.Mask = "99";
            this.OpponentPosition2X.Name = "OpponentPosition2X";
            this.OpponentPosition2X.Size = new System.Drawing.Size(100, 30);
            this.OpponentPosition2X.TabIndex = 9;
            // 
            // OpponentPosition2Y
            // 
            this.OpponentPosition2Y.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OpponentPosition2Y.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OpponentPosition2Y.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpponentPosition2Y.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OpponentPosition2Y.Location = new System.Drawing.Point(118, 115);
            this.OpponentPosition2Y.Mask = "99";
            this.OpponentPosition2Y.Name = "OpponentPosition2Y";
            this.OpponentPosition2Y.Size = new System.Drawing.Size(100, 30);
            this.OpponentPosition2Y.TabIndex = 10;
            // 
            // OurTeamPositionLabel
            // 
            this.OurTeamPositionLabel.AutoSize = true;
            this.OurTeamPositionLabel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OurTeamPositionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OurTeamPositionLabel.Location = new System.Drawing.Point(12, 148);
            this.OurTeamPositionLabel.Name = "OurTeamPositionLabel";
            this.OurTeamPositionLabel.Size = new System.Drawing.Size(182, 30);
            this.OurTeamPositionLabel.TabIndex = 11;
            this.OurTeamPositionLabel.Text = "自分：(X,Y) (X,Y)";
            // 
            // OpponentPositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(235, 238);
            this.ControlBox = false;
            this.Controls.Add(this.OurTeamPositionLabel);
            this.Controls.Add(this.OpponentPosition2Y);
            this.Controls.Add(this.OpponentPosition2X);
            this.Controls.Add(this.OpponentPosition1Y);
            this.Controls.Add(this.OpponentPosition1X);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OpponentPositionForm";
            this.ShowIcon = false;
            this.Text = "敵の位置は？";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OKButton;
        public System.Windows.Forms.MaskedTextBox OpponentPosition1X;
        public System.Windows.Forms.MaskedTextBox OpponentPosition1Y;
        public System.Windows.Forms.MaskedTextBox OpponentPosition2X;
        public System.Windows.Forms.MaskedTextBox OpponentPosition2Y;
        public System.Windows.Forms.Label OurTeamPositionLabel;
    }
}