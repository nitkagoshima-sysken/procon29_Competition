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

namespace Procon29_Visualizer
{
    /// <summary>
    /// メインフォームです。
    /// </summary>
    public partial class MainForm : Form
    {
        Team selectedTeam;
        Agent selectedAgent;

        Calc calc;
        Show show;
        Logger log;
        TeamDesign[] teamDesigns;

        CreateNewForm createNewForm = new CreateNewForm();


        /// <summary>
        /// MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            this.FieldDisplay.MouseMove += new MouseEventHandler(FieldDisplay_MouseMove);
            this.Resize += new System.EventHandler(this.MainForm_Resize);

            log = new Logger(messageBox);
            log.WriteLine(Color.LightGray, "Procon29 Visualizer (ver. 3.0)");

            var pqr = "8 11:-2 1 0 1 2 0 2 1 0 1 -2:1 3 2 -2 0 1 0 -2 2 3 1:1 3 2 1 0 -2 0 1 2 3 1:2 1 1 2 2 3 2 2 1 1 2:2 1 1 2 2 3 2 2 1 1 2:1 3 2 1 0 -2 0 1 2 3 1:1 3 2 -2 0 1 0 -2 2 3 1:-2 1 0 1 2 0 2 1 0 1 -2:2 2:7 10:";
            log.WriteLine(Color.LightGray, pqr);
            var pqr_data = DataConverter.ToPQRData(pqr);
            calc = new Calc(pqr_data.Fields, new Point[2] { pqr_data.One, pqr_data.Two });

            teamDesigns = new TeamDesign[2] {
                new TeamDesign(name:"Orange", agentColor:Color.DarkOrange, areaColor:Color.DarkOrange),
                new TeamDesign(name:"Lime", agentColor:Color.LimeGreen, areaColor:Color.LimeGreen),
            };
            show = new Show(calc, teamDesigns);
            show.Showing(FieldDisplay);
            calc.PointMapCheck();

            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "Team A");
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "name: " + teamDesigns[(int)Team.A].Name);
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "agent: " + calc.AgentPosition[0, 0]);
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "agent: " + calc.AgentPosition[0, 1]);
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "Team B");
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "name: " + teamDesigns[(int)Team.B].Name);
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "agent: " + calc.AgentPosition[1, 0]);
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "agent: " + calc.AgentPosition[1, 1]);

            messageBox.Select(messageBox.Text.Length, 0);
        }

        /// <summary>
        /// MainFormの画面の大きさを変更したときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            show.Showing(FieldDisplay);
        }

        /// <summary>
        /// FieldDisplay内でクリックしたときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            show.ClickedShow(FieldDisplay);
            messageBox.Select(messageBox.Text.Length, 0);
        }

        /// <summary>
        /// FieldDisplay内でダブルクリックしたときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            show.DoubleClickedShow();
            
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "agent: " + calc.AgentPosition[0, 0]);
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "agent: " + calc.AgentPosition[0, 1]);
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
            if (delta.TotalMilliseconds >= 1.0)
            {
                show.Showing(FieldDisplay);
                // フィールド内にいるときは、フィールドの情報を表示する。
                if (0 <= show.CursorPosition(FieldDisplay).X &&
                    show.CursorPosition(FieldDisplay).X < calc.Field.Width() &&
                    0 <= show.CursorPosition(FieldDisplay).Y &&
                    show.CursorPosition(FieldDisplay).Y < calc.Field.Height())
                {
                    var f = calc.Field[show.CursorPosition(FieldDisplay).X, show.CursorPosition(FieldDisplay).Y];
                    // 情報を表示
                    toolStripStatusLabel1.Text = (show.CursorPosition(FieldDisplay) + " Point: " + f.Point);
                    // 囲まれているか判定
                    if (f.IsEnclosed[0] && f.IsEnclosed[1]) toolStripStatusLabel1.Text += " (Surrounded by both)";
                    else if (f.IsEnclosed[0])
                    {
                        toolStripStatusLabel1.Text += " (Surrounded by " + teamDesigns[0].Name + ")";
                        toolStripStatusLabel1.ForeColor = teamDesigns[0].AgentColor;
                    }
                    else if (f.IsEnclosed[1])
                    {
                        toolStripStatusLabel1.Text += " (Surrounded by " + teamDesigns[1].Name + ")";
                        toolStripStatusLabel1.ForeColor = teamDesigns[1].AgentColor;
                    }
                    // タイルがおかれているか判定
                    if (f.IsTileOn[0]) toolStripStatusLabel1.ForeColor = teamDesigns[0].AgentColor;
                    else if (f.IsTileOn[1]) toolStripStatusLabel1.ForeColor = teamDesigns[1].AgentColor;
                    else if ((!f.IsEnclosed[0] && !f.IsEnclosed[1])) toolStripStatusLabel1.ForeColor = Color.LightGray;
                }
            }
            time = DateTime.Now;
        }

        /// <summary>
        /// エージェントを動かします。
        /// </summary>
        /// <param name="team"></param>
        /// <param name="agent"></param>
        /// <param name="where"></param>
        private void MoveAgent(Team team, Agent agent, Point where)
        {
            calc.MoveAgent(team, agent, where);
            //log.WriteLine(teamDesigns[(int)agent].AreaColor, Procon29_Calc.ShortTeamAgentName[(int)team, (int)agent] + " moved to " + where);
        }

        /// <summary>
        /// Form1内でキーを押したときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            var isAct = true;
            e.SuppressKeyPress = true;
            Console.WriteLine(e.KeyCode);
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Q:
                        isAct = false;
                        show.SelectedTeamAndAgent = (Team.A, Agent.One);
                        break;
                    case Keys.W:
                        isAct = false;
                        show.SelectedTeamAndAgent = (Team.A, Agent.Two);
                        break;
                    case Keys.E:
                        isAct = false;
                        show.SelectedTeamAndAgent = (Team.B, Agent.One);
                        break;
                    case Keys.R:
                        isAct = false;
                        show.SelectedTeamAndAgent = (Team.B, Agent.Two);
                        break;
                    case Keys.NumPad1:
                        calc.MoveAgent(show.SelectedTeamAndAgent.Item1, show.SelectedTeamAndAgent.Item2, Arrow10Key.DownLeft);
                        break;
                    case Keys.NumPad2:
                        calc.MoveAgent(show.SelectedTeamAndAgent.Item1, show.SelectedTeamAndAgent.Item2, Arrow10Key.Down);
                        break;
                    case Keys.NumPad3:
                        calc.MoveAgent(show.SelectedTeamAndAgent.Item1, show.SelectedTeamAndAgent.Item2, Arrow10Key.DownRight);
                        break;
                    case Keys.NumPad4:
                        calc.MoveAgent(show.SelectedTeamAndAgent.Item1, show.SelectedTeamAndAgent.Item2, Arrow10Key.Left);
                        break;
                    case Keys.NumPad6:
                        calc.MoveAgent(show.SelectedTeamAndAgent.Item1, show.SelectedTeamAndAgent.Item2, Arrow10Key.Right);
                        break;
                    case Keys.NumPad7:
                        calc.MoveAgent(show.SelectedTeamAndAgent.Item1, show.SelectedTeamAndAgent.Item2, Arrow10Key.UpLeft);
                        break;
                    case Keys.NumPad8:
                        calc.MoveAgent(show.SelectedTeamAndAgent.Item1, show.SelectedTeamAndAgent.Item2, Arrow10Key.Up);
                        break;
                    case Keys.NumPad9:
                        calc.MoveAgent(show.SelectedTeamAndAgent.Item1, show.SelectedTeamAndAgent.Item2, Arrow10Key.UpRight);
                        break;
                    default:
                        isAct = false;
                        e.SuppressKeyPress = false;
                        break;
                }
                show.ClickedField =
                    new Point(
                        calc.AgentPosition[
                            (int)show.SelectedTeamAndAgent.Item1,
                            (int)show.SelectedTeamAndAgent.Item2]
                            .X,
                        calc.AgentPosition[
                            (int)show.SelectedTeamAndAgent.Item1,
                            (int)show.SelectedTeamAndAgent.Item2]
                            .Y);
                if (isAct)
                {
                    log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A   Direct Point: " + calc.AreaPoint(Team.A).ToString());
                    log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A Indirect Point: " + calc.ClosedPoint(Team.A).ToString());
                    log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A    Total Point: " + calc.TotalPoint(Team.A).ToString());
                    log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B   Direct Point: " + calc.AreaPoint(Team.B).ToString());
                    log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B Indirect Point: " + calc.ClosedPoint(Team.B).ToString());
                    log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B    Total Point: " + calc.TotalPoint(Team.B).ToString());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("不正なキー入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            show.Showing(FieldDisplay);
        }

        /// <summary>
        /// 「ファイル」の「開く」を押したときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            openFileDialog.Filter = "PQRファイル(*.pqr)|*.pqr|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //1番目の「PQRファイル」が選択されているようにする
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
                    try
                    {
                        var pqr = sr.ReadToEnd();
                        log.WriteLine(Color.LightGray, pqr);
                        var pqr_data = DataConverter.ToPQRData(pqr);
                        if (pqr_data.One.X < 0 || pqr_data.One.Y < 0)
                        {
                            MessageBox.Show("1人目のエージェントの位置" + pqr_data.One + "が不正です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            pqr_data.One = new Point(0, 0);
                        }
                        if (pqr_data.Two.X < 0 || pqr_data.Two.Y < 0)
                        {
                            MessageBox.Show("2人目のエージェントの位置" + pqr_data.One + "が不正です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            pqr_data.Two = new Point(0, 0);
                        }

                        calc = new Calc(pqr_data.Fields, new Point[2] { pqr_data.One, pqr_data.Two });

                        show = new Show(calc, teamDesigns);
                        show.Showing(FieldDisplay);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("不正なPQR形式です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CreateNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createNewForm.ShowDialog(this);
        }

        private void TurnEndButton_Click(object sender, EventArgs e)
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (Agent agent in Enum.GetValues(typeof(Agent)))
                {
                    var isIndependence = true;
                    foreach (Team otherteam in Enum.GetValues(typeof(Team)))
                    {
                        foreach (Agent otheragent in Enum.GetValues(typeof(Agent)))
                        {
                            if (team == otherteam && agent == otheragent)
                            {
                                continue;
                            }
                            if (show.wanttogo[(int)team, (int)agent] == show.wanttogo[(int)otherteam, (int)otheragent])
                            {
                                isIndependence = false;
                                break;
                            }

                        }
                    }
                    if (isIndependence) MoveAgent(team, agent, show.wanttogo[(int)team, (int)agent]);
                }
            }
            show.Showing(FieldDisplay);
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A   Direct Point: " + calc.AreaPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A Indirect Point: " + calc.ClosedPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A    Total Point: " + calc.TotalPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B   Direct Point: " + calc.AreaPoint(Team.B).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B Indirect Point: " + calc.ClosedPoint(Team.B).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B    Total Point: " + calc.TotalPoint(Team.B).ToString());
        }
    }
}
