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
            this.OpponentPosition1X = new System.Windows.Forms.TextBox();
            this.OpponentPosition2X = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OpponentPosition1Y = new System.Windows.Forms.TextBox();
            this.OpponentPosition2Y = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpponentPosition1X
            // 
            this.OpponentPosition1X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OpponentPosition1X.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpponentPosition1X.Location = new System.Drawing.Point(12, 42);
            this.OpponentPosition1X.Name = "OpponentPosition1X";
            this.OpponentPosition1X.Size = new System.Drawing.Size(100, 37);
            this.OpponentPosition1X.TabIndex = 0;
            // 
            // OpponentPosition2X
            // 
            this.OpponentPosition2X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OpponentPosition2X.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpponentPosition2X.Location = new System.Drawing.Point(12, 115);
            this.OpponentPosition2X.Name = "OpponentPosition2X";
            this.OpponentPosition2X.Size = new System.Drawing.Size(100, 37);
            this.OpponentPosition2X.TabIndex = 1;
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
            // OpponentPosition1Y
            // 
            this.OpponentPosition1Y.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OpponentPosition1Y.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpponentPosition1Y.Location = new System.Drawing.Point(118, 42);
            this.OpponentPosition1Y.Name = "OpponentPosition1Y";
            this.OpponentPosition1Y.Size = new System.Drawing.Size(100, 37);
            this.OpponentPosition1Y.TabIndex = 4;
            // 
            // OpponentPosition2Y
            // 
            this.OpponentPosition2Y.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.OpponentPosition2Y.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OpponentPosition2Y.Location = new System.Drawing.Point(118, 115);
            this.OpponentPosition2Y.Name = "OpponentPosition2Y";
            this.OpponentPosition2Y.Size = new System.Drawing.Size(100, 37);
            this.OpponentPosition2Y.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.Location = new System.Drawing.Point(12, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 47);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // OpponentPositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(235, 221);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OpponentPosition2Y);
            this.Controls.Add(this.OpponentPosition1Y);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpponentPosition2X);
            this.Controls.Add(this.OpponentPosition1X);
            this.Name = "OpponentPositionForm";
            this.Text = "敵の位置は？";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox OpponentPosition1X;
        public System.Windows.Forms.TextBox OpponentPosition2X;
        public System.Windows.Forms.TextBox OpponentPosition1Y;
        public System.Windows.Forms.TextBox OpponentPosition2Y;
    }
}