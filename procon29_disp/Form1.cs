using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace procon29_disp
{
    public partial class Form1 : Form
    {
        Procon29_Display procon = new Procon29_Display();
        public Form1()
        {
            InitializeComponent();

            this.pictureBox1.MouseMove += new MouseEventHandler(mouseMove);
            this.Resize += new System.EventHandler(this.Form1_Resize);

            var pointarray = new int[,] {
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, },
                //{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }
            };


            procon = new Procon29_Display(pointarray);
            procon.PointMapCheck();
            procon.Show(pictureBox1);
        }

        //Resizeイベントハンドラ
        private void Form1_Resize(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            //Console.WriteLine("フォームのサイズが{0}x{1}に変更されました", c.Width, c.Height);
            procon.Show(pictureBox1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            procon.Show(pictureBox1);
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            procon.Show(pictureBox1);
        }
    }
}
