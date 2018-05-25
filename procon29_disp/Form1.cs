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
                { -6, 15,  0,  7,  0, -1, 13, -8, -7, -7,  2, -3, },
                {  8,  1, -5,  0, -2, -8, 10, -3,-15, 14, -4, -3, },
                {-15, 14, -4, -3, -8, -3, -7, -5,-11,-16, -9, -1, },
                { 15,  1, 14, 10,-11, 11, -2,  8,  6, 11,  9, -5, },
                {-11,-16,  0,  8,-10, -8,-10,  5,  4,  5,  7,-14, },
                {-13, -3, 16, -5,  6, 12, -3, -5,  0,  1, 16,-16, },
                {-13, -3, 16, -5,  6, 12, -3, -5,  0,  1, 16,-16, },
                {-11,-16,  0,  8,-10, -8,-10,  5,  4,  5,  7,-14, },
                { 15,  1, 14, 10,-11, 11, -2,  8,  6, 11,  9, -5, },
                {-15, 14, -4, -3, -8, -3, -7, -5,-11,-16, -9, -1, },
                {  8,  1, -5,  0, -2, -8, 10, -3,-15, 14, -4, -3, },
                { -6, 15,  0,  7,  0, -1, 13, -8, -7, -7,  2, -3, },
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
