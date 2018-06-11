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
        Procon29_Logger log;
        TeamDesign[] teamDesigns;

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

            teamDesigns = new TeamDesign[2] {
                new TeamDesign(name:"Orange", agentColor:Color.DarkOrange, areaColor:Color.DarkOrange),
                new TeamDesign(name:"Lime", agentColor:Color.LimeGreen, areaColor:Color.LimeGreen),
            };
            show = new Procon29_Show(procon, teamDesigns);
            log = new Procon29_Logger(messageBox);
            log.WriteLine(Color.LightGray, "Test");
            log.WriteLine(Color.LightGray, procon.Sum().ToString());
            log.WriteLine(Color.LightGray, procon.SumAbs().ToString());
            log.WriteLine(Color.LightGray, procon.SumDirectArea(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "Team A");
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "name: " + teamDesigns[(int)Team.A].Name);
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "Team B");
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "name: " + teamDesigns[(int)Team.B].Name);

            messageBox.Select(messageBox.Text.Length, 0);

            this.MoveAgent(Team.A, Agent.One, new Point(10, 3));
            log.WriteLine(Color.LightGray, procon.SumDirectArea(Team.A).ToString());
            show.Show(FieldDisplay);
        }

        //Resizeイベントハンドラ
        private void Form1_Resize(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            show.Show(FieldDisplay);
        }

        private void FieldDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            show.ClickedShow(FieldDisplay);
            messageBox.Select(messageBox.Text.Length, 0);
        }

        private void FieldDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            show.DoubleClickedShow();
        }

        /// <summary>
        /// FieldDisplay_MouseMoveのための変数
        /// </summary>
        System.DateTime time = System.DateTime.Now;

        /// <summary>
        /// マウスが動いたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            // 最後にイベントが実行された時刻から何ミリ秒たったかを計算し、それが1ミリ秒以上だった場合は、画面を更新する。
            // こうすることによって、イベントの渋滞を防ぐ。
            var delta = DateTime.Now - time;
            if (delta.TotalMilliseconds >= 1.0) show.Show(FieldDisplay);
            time = DateTime.Now;
        }

        private void MoveAgent(Team team, Agent agent, Point where)
        {
            procon.MoveAgent(team, agent, where);
            log.WriteLine(teamDesigns[(int)agent].AreaColor, Procon29_Calc.ShortTeamAgentName[(int)team, (int)agent] + " moved to " + where);
        }
    }
}
