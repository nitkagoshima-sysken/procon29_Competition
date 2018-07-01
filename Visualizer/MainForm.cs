using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken
{
    namespace Procon29
    {
        namespace Visualizer
        {
            /// <summary>
            /// メインフォームです。
            /// </summary>
            public partial class MainForm : Form
            {
                public static Calc calc;
                Show show;
                Logger log;
                TeamDesign[] teamDesigns;

                CreateNewForm createNewForm = new CreateNewForm();
                public static dynamic[] bot = new dynamic[2];
                public static string[] botName = new string[2];

                /// <summary>
                /// MainForm
                /// </summary>
                public MainForm()
                {
                    InitializeComponent();

                    this.FieldDisplay.MouseMove += new MouseEventHandler(FieldDisplay_MouseMove);
                    this.Resize += new System.EventHandler(this.MainForm_Resize);

                    log = new Logger(messageBox);
                    log.WriteLine(Color.LightGray, "Procon29 Visualizer (ver. 7.0)");

                    // PQRファイルを直接読み込む
                    // ちなみにQR_code_sample.pdfで登場したQRコード
                    var pqr = "8 11:-2 1 0 1 2 0 2 1 0 1 -2:1 3 2 -2 0 1 0 -2 2 3 1:1 3 2 1 0 -2 0 1 2 3 1:2 1 1 2 2 3 2 2 1 1 2:2 1 1 2 2 3 2 2 1 1 2:1 3 2 1 0 -2 0 1 2 3 1:1 3 2 -2 0 1 0 -2 2 3 1:-2 1 0 1 2 0 2 1 0 1 -2:2 2:7 10:";
                    log.WriteLine(Color.LightGray, pqr);
                    var pqr_data = DataConverter.ToPQRData(pqr);
                    calc = new Calc(pqr_data.Fields, new Coordinate[2] { pqr_data.One, pqr_data.Two });

                    teamDesigns = new TeamDesign[2] {
                new TeamDesign(name:"Orange", agentColor:Color.DarkOrange, areaColor:Color.DarkOrange),
                new TeamDesign(name:"Lime", agentColor:Color.LimeGreen, areaColor:Color.LimeGreen),
            };
                    show = new Show(calc, teamDesigns, FieldDisplay);
                    show.Showing(FieldDisplay);
                    calc.PointMapCheck();

                    KeyDown += new KeyEventHandler(show.KeyDown);
                    KeyDown += new KeyEventHandler(MainForm_KeyDown);

                    WriteLog();
                }


                private void WriteLog()
                {
                    log.WriteLine(Color.LightGray, "\n" + "Turn : " + calc.Turn);
                    log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A   Area Point: " + calc.AreaPoint(Team.A).ToString());
                    log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "Enclosed Point: " + calc.EnclosedPoint(Team.A).ToString());
                    log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "   Total Point: " + calc.TotalPoint(Team.A).ToString());
                    log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "agent: " + calc.Agents[Team.A, AgentNumber.One].Position);
                    log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "agent: " + calc.Agents[Team.A, AgentNumber.Two].Position);
                    log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B   Area Point: " + calc.AreaPoint(Team.B).ToString());
                    log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "Enclosed Point: " + calc.EnclosedPoint(Team.B).ToString());
                    log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "   Total Point: " + calc.TotalPoint(Team.B).ToString());
                    log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "agent: " + calc.Agents[Team.B, AgentNumber.One].Position);
                    log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "agent: " + calc.Agents[Team.B, AgentNumber.Two].Position);
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
                    if (delta.TotalMilliseconds >= 5.0)
                    {
                        show.Showing(FieldDisplay);
                        // フィールド内にいるときは、フィールドの情報を表示する。
                        if (0 <= show.CursorPosition(FieldDisplay).X &&
                            show.CursorPosition(FieldDisplay).X < calc.Field.Width &&
                            0 <= show.CursorPosition(FieldDisplay).Y &&
                            show.CursorPosition(FieldDisplay).Y < calc.Field.Height)
                        {
                            var f = calc.Field[show.CursorPosition(FieldDisplay).X, show.CursorPosition(FieldDisplay).Y];
                            // 情報を表示
                            toolStripStatusLabel1.Text = (show.CursorPosition(FieldDisplay) + " Point: " + f.Point);
                            // 囲まれているか判定
                            if (f.IsEnclosed[Team.A] && f.IsEnclosed[Team.B]) toolStripStatusLabel1.Text += " (Surrounded by both)";
                            else if (f.IsEnclosed[Team.A])
                            {
                                toolStripStatusLabel1.Text += " (Surrounded by " + teamDesigns[0].Name + ")";
                                toolStripStatusLabel1.ForeColor = teamDesigns[0].AgentColor;
                            }
                            else if (f.IsEnclosed[Team.B])
                            {
                                toolStripStatusLabel1.Text += " (Surrounded by " + teamDesigns[1].Name + ")";
                                toolStripStatusLabel1.ForeColor = teamDesigns[1].AgentColor;
                            }
                            // タイルがおかれているか判定
                            if (f.IsTileOn[Team.A]) toolStripStatusLabel1.ForeColor = teamDesigns[0].AgentColor;
                            else if (f.IsTileOn[Team.B]) toolStripStatusLabel1.ForeColor = teamDesigns[1].AgentColor;
                            else if ((!f.IsEnclosed[Team.A] && !f.IsEnclosed[Team.B])) toolStripStatusLabel1.ForeColor = Color.LightGray;
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
                private void MoveAgent(Team team, AgentNumber agent, Coordinate where)
                {
                    calc.MoveAgent(team, agent, where);
                }
                
                /// <summary>
                /// Form1内でキーを押したときに実行されます。
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                private void MainForm_KeyDown(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter) TurnEnd();
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
                                    pqr_data.One = new Coordinate(0, 0);
                                }
                                if (pqr_data.Two.X < 0 || pqr_data.Two.Y < 0)
                                {
                                    MessageBox.Show("2人目のエージェントの位置" + pqr_data.One + "が不正です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    pqr_data.Two = new Coordinate(0, 0);
                                }

                                calc = new Calc(pqr_data.Fields, new Coordinate[2] { pqr_data.One, pqr_data.Two });

                                show = new Show(calc, teamDesigns, FieldDisplay);
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
                    TurnEnd();
                }

                private void TurnEnd()
                {
                    if (bot[0] != null)
                    {
                        bot[0].Team = Team.A;
                        bot[0].Question(calc);
                        var a = bot[0].Answer();
                        show.agentActivityData[0, 0] = a[0];
                        show.agentActivityData[0, 1] = a[1];
                    }
                    if (bot[1] != null)
                    {
                        bot[1].Team = Team.B;
                        bot[1].Question(calc);
                        var a = bot[1].Answer();
                        show.agentActivityData[1, 0] = a[0];
                        show.agentActivityData[1, 1] = a[1];
                    }

                    calc.MoveAgent(show.agentActivityData);

                    foreach (var item in show.agentActivityData)
                    {
                        item.AgentStatusData = AgentStatusCode.NotDoneAnything;
                    }

                    show.Showing(FieldDisplay);
                    WriteLog();

                    if (bot[0] != null)
                    {
                        log.WriteLine(Color.SkyBlue, "[" + botName[0] + "]");
                        var d = calc.FieldHistory[calc.Turn - 1].AgentActivityDatas[Team.A, AgentNumber.One];
                        log.WriteLine(Color.SkyBlue, "A1 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString());
                        d = calc.FieldHistory[calc.Turn - 1].AgentActivityDatas[Team.A, AgentNumber.Two];
                        log.WriteLine(Color.SkyBlue, "A2 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString());
                    }
                    if (bot[1] != null)
                    {
                        log.WriteLine(Color.SkyBlue, "[" + botName[1] + "]");
                        var d = calc.FieldHistory[calc.Turn - 1].AgentActivityDatas[Team.B, AgentNumber.One];
                        log.WriteLine(Color.SkyBlue, "B1 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString());
                        d = calc.FieldHistory[calc.Turn - 1].AgentActivityDatas[Team.B, AgentNumber.Two];
                        log.WriteLine(Color.SkyBlue, "B2 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString());
                    }
                }

                private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    calc.Undo();
                    show.Showing(FieldDisplay);
                    WriteLog();

                }

                private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    calc.Redo();
                    show.Showing(FieldDisplay);
                    WriteLog();
                }
            }
        }
    }
}