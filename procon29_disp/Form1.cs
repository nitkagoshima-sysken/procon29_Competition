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
        Team selectedTeam;
        Agent selectedAgent;
        
        Procon29_Calc procon;
        Procon29_Show show;
        /// <summary>
        /// Form1
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            this.FieldDisplay.MouseMove += new MouseEventHandler(FieldDisplay_MouseMove);
            this.Resize += new System.EventHandler(this.Form1_Resize);

            procon = new Procon29_Calc(
                field: new int[,] {
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
                },
                initPosition: new Point[,]
                {
                    { new Point(11, 3), new Point(11, 9) },
                    { new Point(0, 3), new Point(0, 9) },
                });

            procon.PointMapCheck();
            //procon.Show(FieldDisplay);

            TeamDesign[] teamDesigns = new TeamDesign[2] {
                new TeamDesign(name:"A", agentColor:Color.DarkGreen, areaColor:Color.DarkGreen),
                new TeamDesign(name:"B", agentColor:Color.LimeGreen, areaColor:Color.LimeGreen),
            };
            show = new Procon29_Show(procon, teamDesigns);
            messageBox.Select(messageBox.Text.Length, 0);

            procon.MoveAgent(Team.A, Agent.One, new Point(10, 3));
        }

        //Resizeイベントハンドラ
        private void Form1_Resize(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            //Console.WriteLine("フォームのサイズが{0}x{1}に変更されました", c.Width, c.Height);
            //procon.Show(FieldDisplay);
            show.Show(FieldDisplay);
        }

        private void FieldDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            show.ClickedShow(FieldDisplay);
            //procon.ClickedShow(FieldDisplay);
            //messageBox.Text = procon.Message;
            messageBox.Select(messageBox.Text.Length, 0);
        }

        private void FieldDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            show.DoubleClickedShow();
            //procon.DoubleClickedShow();
        }

        private void FieldDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            show.Show(FieldDisplay);
            //procon.Show(FieldDisplay);
        }
    }
}
