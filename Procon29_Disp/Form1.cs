﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "Team A");
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "name: " + teamDesigns[(int)Team.A].Name);
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "Team B");
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "name: " + teamDesigns[(int)Team.B].Name);

            messageBox.Select(messageBox.Text.Length, 0);

            this.MoveAgent(Team.A, Agent.One, new Point(10, 3));
            //log.WriteLine(Color.LightGray, procon.SumDirectArea(Team.A).ToString());
            show.Show(FieldDisplay);



            //foreach (var item in Field.CSVToFields("4, 2, 1, 2, 3, 4, 5, 6, 7, 8,"))
            //{
            //    Console.WriteLine(item.Point);
            //}

            //foreach (var item in Procon29_CSV.SpaceCSVToList("4 2 1 2 3 4 5 6 7 8 "))
            //{
            //    Console.WriteLine(item);
            //}
            Procon29_CSV.ToPQRData("12 12:6 -13 -2 -16 0 16 16 0 -16 -2 -13 6:-12 -15 10 14 0 5 5 0 14 10 -15 -12:6 11 13 -14 -12 12 12 -12 -14 13 11 6:12 3 -11 10 7 0 0 7 10 -11 3 12:1 -10 3 -4 12 14 14 12 -4 3 -10 1:12 6 9 5 4 -9 -9 4 5 9 6 12:12 6 9 5 4 -9 -9 4 5 9 6 12:1 -10 3 -4 12 14 14 12 -4 3 -10 1:12 3 -11 10 7 0 0 7 10 -11 3 12:6 11 13 -14 -12 12 12 -12 -14 13 11 6:-12 -15 10 14 0 5 5 0 14 10 -15 -12:6 -13 -2 -16 0 16 16 0 -16 -2 -13 6:-1 2:1 2:");
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
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A   Direct Point: " + procon.DirectPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A Indirect Point: " + procon.IndirectPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A    Total Point: " + procon.TotalPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B   Direct Point: " + procon.DirectPoint(Team.B).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B Indirect Point: " + procon.IndirectPoint(Team.B).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B    Total Point: " + procon.TotalPoint(Team.A).ToString());
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
            //log.WriteLine(teamDesigns[(int)agent].AreaColor, Procon29_Calc.ShortTeamAgentName[(int)team, (int)agent] + " moved to " + where);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            openFileDialog.FileName = "";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            openFileDialog.InitialDirectory = "";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            openFileDialog.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //1番目の「CSVファイル」が選択されているようにする
            openFileDialog.FilterIndex = 1;
            //タイトルを設定する
            openFileDialog.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            openFileDialog.RestoreDirectory = true;

            //ダイアログを表示する
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を開き、データを読み込む                
                using (StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding("Shift_JIS")))
                {
                    var text = sr.ReadToEnd();
                    Console.WriteLine(text);

                }
            }
        }
    }
}
