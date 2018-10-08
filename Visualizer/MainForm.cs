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

namespace nitkagoshima_sysken.Procon29.Visualizer
{

    /// <summary>
    /// メインフォームです。
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// 試合で使う計算機
        /// </summary>
        public static Calc Calc { get; set; }

        Show show;
        Logger log;
        TeamDesign[] teamDesigns;

        CreateNewForm createNewForm = new CreateNewForm();

        /// <summary>
        /// ボットを設定または取得します。
        /// </summary>
        public static dynamic[] Bot { get; set; } = new dynamic[2];

        /// <summary>
        /// ボットの名前を設定または取得します。
        /// </summary>
        public static string[] BotName { get; set; } = new string[2];

        /// <summary>
        /// 最大ターンのデフォルト値を設定または取得します。
        /// </summary>
        public static int MaxTurn = 10;

        /// <summary>
        /// デバッグモードを設定または取得します。
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// ゲームのモードを設定または取得します。
        /// </summary>
        private PlayMode Mode { get; set; } = PlayMode.PracticeMode;

        /// <summary>
        /// MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            FieldDisplay.MouseMove += new MouseEventHandler(FieldDisplay_MouseMove);
            Resize += new System.EventHandler(MainForm_Resize);

            log = new Logger(messageBox);
            var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
            log.WriteLine(Color.LightGray, "Procon29 Visualizer (ver. " + version.FileMinorPart + "." + version.FileBuildPart + ")");

            // PQRファイルを直接読み込む
            // ちなみにQR_code_sample.pdfで登場したQRコード
            var reader = new PqrReader();
            reader.Stream =
                "8 11:" +
                "-2 1 0 1 2 0 2 1 0 1 -2:" +
                "1 3 2 -2 0 1 0 -2 2 3 1:" +
                "1 3 2 1 0 -2 0 1 2 3 1:" +
                "2 1 1 2 2 3 2 2 1 1 2:" +
                "2 1 1 2 2 3 2 2 1 1 2:" +
                "1 3 2 1 0 -2 0 1 2 3 1:" +
                "1 3 2 -2 0 1 0 -2 2 3 1:" +
                "-2 1 0 1 2 0 2 1 0 1 -2:" +
                "2 2:" +
                "7 10:";
            log.WriteLine(Color.LightGray, reader.Stream);
            var pqr = reader.ConvertToPqrData();
            Calc = new Calc(10, pqr.Fields, new Coordinate[2] { pqr.One, pqr.Two });

            ReadBotsTxt();
            ReadCalcTsv();

            teamDesigns =
                new TeamDesign[2] {
                    new TeamDesign(name: "Orange", agentColor: Color.DarkOrange, areaColor: Color.DarkOrange),
                    new TeamDesign(name: "Lime", agentColor: Color.LimeGreen, areaColor: Color.LimeGreen),
                };
            show = new Show(Calc, teamDesigns, FieldDisplay);
            show.Showing();
            Calc.PointMapCheck();

            KeyDown += new KeyEventHandler(show.KeyDown);
            KeyDown += new KeyEventHandler(MainForm_KeyDown);

            WriteLog();
            TurnProgressCheck();
        }


        private void WriteLog()
        {
            log.WriteLine(Color.LightGray, "\n" + "Turn : " + Calc.Turn);
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "A   Area Point: " + Calc.AreaPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "Enclosed Point: " + Calc.EnclosedPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "   Total Point: " + Calc.TotalPoint(Team.A).ToString());
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "agent: " + Calc.Agents[Team.A, AgentNumber.One].Position);
            log.WriteLine(teamDesigns[(int)Team.A].AreaColor, "agent: " + Calc.Agents[Team.A, AgentNumber.Two].Position);
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "B   Area Point: " + Calc.AreaPoint(Team.B).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "Enclosed Point: " + Calc.EnclosedPoint(Team.B).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "   Total Point: " + Calc.TotalPoint(Team.B).ToString());
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "agent: " + Calc.Agents[Team.B, AgentNumber.One].Position);
            log.WriteLine(teamDesigns[(int)Team.B].AreaColor, "agent: " + Calc.Agents[Team.B, AgentNumber.Two].Position);
        }

        void TurnProgressCheck()
        {
            toolStripProgressBar1.Maximum = Calc.MaxTurn;
            toolStripProgressBar1.Value = Calc.Turn;
            if (Calc.MaxTurn <= Calc.Turn)
            {
                TurnEndButton.Visible = false;
                tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
                tableLayoutPanel2.RowStyles[2] = new RowStyle(SizeType.Absolute, 0);
                MessageBox.Show("試合が終了しました", "お知らせ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var form = new HistoryForm(Calc);
                form.ShowDialog();
            }
            else
            {
                TurnEndButton.Visible = true;
                tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Percent, 80);
                tableLayoutPanel2.RowStyles[2] = new RowStyle(SizeType.Percent, 20);
            }
        }

        /// <summary>
        /// MainFormの画面の大きさを変更したときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            show.Showing();
        }

        /// <summary>
        /// FieldDisplay内でクリックしたときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            show.ClickedShow(FieldDisplay);
            show.ClickShow();
            messageBox.Select(messageBox.Text.Length, 0);
        }

        /// <summary>
        /// FieldDisplay内でダブルクリックしたときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldDisplay_MouseDoubleClick(object sender, MouseEventArgs e)
        {
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
                show.Showing();
                // フィールド内にいるときは、フィールドの情報を表示する。
                if (0 <= show.CursorPosition(FieldDisplay).X &&
                    show.CursorPosition(FieldDisplay).X < Calc.Field.Width &&
                    0 <= show.CursorPosition(FieldDisplay).Y &&
                    show.CursorPosition(FieldDisplay).Y < Calc.Field.Height)
                {
                    toolStripStatusLabel1.Text = "[Turn: " + Calc.Turn + "/" + Calc.MaxTurn + "]";
                    var f = Calc.Field[show.CursorPosition(FieldDisplay).X, show.CursorPosition(FieldDisplay).Y];
                    // 情報を表示
                    toolStripStatusLabel1.Text += ("[Coordinate: " + show.CursorPosition(FieldDisplay) + " Point: " + f.Point);
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
                    toolStripStatusLabel1.Text += "]";
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
            Calc.MoveAgent(team, agent, where);
        }

        /// <summary>
        /// Form1内でキーを押したときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TurnEnd();
            show.Showing();
        }

        /// <summary>
        /// 「ファイル」の「開く」を押したときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
            //タイトルを設定する
            openFileDialog1.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            openFileDialog1.RestoreDirectory = true;

            //ダイアログを表示する
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //XmlSerializerオブジェクトを作成
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(XmlCalc));
                //読み込むファイルを開く
                System.IO.StreamReader sr = new System.IO.StreamReader(
                    openFileDialog1.FileName, new System.Text.UTF8Encoding(false));
                //XMLファイルから読み込み、逆シリアル化する
                Calc = new Calc((XmlCalc)serializer.Deserialize(sr));
                //ファイルを閉じる
                sr.Close();
                show.Calc = Calc;
                show.Showing();
            }
        }

        /// <summary>
        /// ここで新しい試合を作成します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createNewForm.ShowDialog(this);
            Calc.MaxTurn = MaxTurn;
            //OKボタンがクリックされたとき、選択されたファイル名を開き、データを読み込む    
            if (createNewForm.SelectPQRFile != ".pqr" && createNewForm.SelectPQRFile != null)
                OpenPQRFile(createNewForm.SelectPQRFile);
            TurnProgressCheck();
        }

        /// <summary>
        /// PQRファイルを開く処理
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        public void OpenPQRFile(string path)
        {
            try
            {
                var reader = new PqrReader(path);
                reader.ReadPqrFile();
                log.WriteLine(Color.LightGray, reader.Stream);
                var pqr = reader.ConvertToPqrData();
                pqr.IsRegular();
                Calc = new Calc(MaxTurn, pqr.Fields, new Coordinate[2] { pqr.One, pqr.Two });
                show = new Show(Calc, teamDesigns, FieldDisplay);
                show.Showing();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "VisualizerはPQRファイルが確かに存在することを確認しました。\n" +
                    "そして、ファイルを開き、読み込むことに成功しました。\n" +
                    "しかし、解析の結果、正規なPQRデータではないことが判明しました。\n" +
                    "そのため、これらのデータは破棄され、Visualizerに反映されません。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void ReadBotsTxt()
        {
            // "Prefetching" というディレクトリが存在しない場合、作成する
            if (!Directory.Exists("Prefetching"))
            {
                Directory.CreateDirectory("Prefetching");
            }
            if (System.IO.File.Exists(@".\Prefetching\Bots.tsv"))
            {
                var reader = new TsvReader(@".\Prefetching\Bots.tsv");
                reader.ReadTsvFile();
                reader.ConvertToTsvData();
                var result = reader.TsvData;

                if (result.ContainsKey("A"))
                {
                    log.WriteLine(Color.SkyBlue, "Bot \"" + result["A"][0].Trim() + "\" was read on my team by Bot.tsv" + BotName[0] + "]");
                    ConnectBot(0, result["A"][0]);
                }
                if (result.ContainsKey("B"))
                {
                    log.WriteLine(Color.SkyBlue, "Bot \"" + result["B"][0].Trim() + "\" was read on opponent team by Bot.tsv" + BotName[0] + "]");
                    ConnectBot(0, result["B"][0]);
                }
            }
            // "Bots.tsv" というディレクトリが存在しない場合、作成する
            else
            {
                using (var file = System.IO.File.Create(@".\Prefetching\Bots.tsv"))
                {
                    if (file != null)
                    {
                        file.Close();
                    }
                }
            }
        }

        public void ReadCalcTsv()
        {
            if (System.IO.File.Exists(@".\Prefetching\Calc.tsv"))
            {
                var reader = new TsvReader(@".\Prefetching\Calc.tsv");
                reader.ReadTsvFile();
                reader.ConvertToTsvData();
                var result = reader.TsvData;

                if (result.ContainsKey("Pqr"))
                {
                    log.WriteLine(Color.SkyBlue, "PQR File \"" + result["Pqr"][0].Trim() + "\" was read by Calc.tsv");
                    OpenPQRFile(result["Pqr"][0].Trim());
                    Console.WriteLine("\"" + result["Pqr"][0].Trim() + "\"");
                }
                if (result.ContainsKey("MaxTurn"))
                {
                    log.WriteLine(Color.SkyBlue, "Max Turn " + result["MaxTurn"][0].Trim() + " was read by Calc.tsv");
                    Calc.MaxTurn = int.Parse(result["MaxTurn"][0]);
                }
            }
            // "Calc.tsv" というディレクトリが存在しない場合、作成する
            else
            {
                using (var file = System.IO.File.Create(@".\Prefetching\Calc.tsv"))
                {
                    if (file != null)
                    {
                        file.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void ConnectBot(int n, string path)
        {
            try
            {
                Assembly m = Assembly.LoadFrom(path);

                System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(path, @"^[A-Z]:\\(.*\\)+(?<file>.*).dll$");

                foreach (System.Text.RegularExpressions.Match match in mc)
                {
                    Bot[n] = Activator.CreateInstance(m.GetType("nitkagoshima_sysken.Procon29." + match.Groups["file"].Value + "." + match.Groups["file"].Value));
                    MainForm.BotName[n] = match.Groups["file"].Value;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("不正なdllです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TurnEndButton_Click(object sender, EventArgs e)
        {
            TurnEnd();
        }

        private void TurnEnd()
        {
            if (TurnEndButton.Text == "ターンエンド")
            {
                if (Bot[0] != null)
                {
                    Bot[0].Team = Team.A;
                    Bot[0].Question(Calc);
                    var a = Bot[0].Answer();
                    show.agentActivityData[Team.A, AgentNumber.One] = a[0];
                    show.agentActivityData[Team.A, AgentNumber.Two] = a[1];
                }
                if (Bot[1] != null)
                {
                    Bot[1].Team = Team.B;
                    Bot[1].Question(Calc);
                    var a = Bot[1].Answer();
                    show.agentActivityData[Team.B, AgentNumber.One] = a[0];
                    show.agentActivityData[Team.B, AgentNumber.Two] = a[1];
                }

                Calc.MoveAgent(show.agentActivityData);

                foreach (AgentActivityData item in show.agentActivityData)
                {
                    item.AgentStatusData = AgentStatusCode.NotDoneAnything;
                }

                show.Showing();
                WriteLog();
                TurnProgressCheck();

                if (Bot[0] != null)
                {
                    log.WriteLine(Color.SkyBlue, "[" + BotName[0] + "]");
                    var d = Calc.History[Calc.Turn - 1].AgentActivityDatas[Team.A, AgentNumber.One];
                    log.WriteLine(Color.SkyBlue, "A1 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString());
                    d = Calc.History[Calc.Turn - 1].AgentActivityDatas[Team.A, AgentNumber.Two];
                    log.WriteLine(Color.SkyBlue, "A2 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString());
                }
                if (Bot[1] != null)
                {
                    log.WriteLine(Color.SkyBlue, "[" + BotName[1] + "]");
                    var d = Calc.History[Calc.Turn - 1].AgentActivityDatas[Team.B, AgentNumber.One];
                    log.WriteLine(Color.SkyBlue, "B1 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString());
                    d = Calc.History[Calc.Turn - 1].AgentActivityDatas[Team.B, AgentNumber.Two];
                    log.WriteLine(Color.SkyBlue, "B2 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString());
                }

                if (Debug)
                {
                    //XmlSerializerオブジェクトを作成
                    //オブジェクトの型を指定する
                    System.Xml.Serialization.XmlSerializer serializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(XmlCalc));
                    //書き込むファイルを開く（UTF-8 BOM無し）
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(
                        "log.xml", false, new System.Text.UTF8Encoding(false));
                    //シリアル化し、XMLファイルに保存する
                    serializer.Serialize(sw, new XmlCalc(Calc));
                    //ファイルを閉じる
                    sw.Close();
                }
                if (Mode == PlayMode.ProductionMode)
                {
                    TurnEndButton.Text = "ボットで選択";
                    TurnEndButton.BackColor = Color.DarkGray;
                    TurnEndButton.ForeColor = Color.White;
                }
            }
            else if (TurnEndButton.Text == "ボットで選択")
            {
                if (Bot[0] != null)
                {
                    Bot[0].Team = Team.A;
                    Bot[0].Question(Calc);
                    var a = Bot[0].Answer();
                    show.agentActivityData[Team.A, AgentNumber.One] = a[0];
                    show.agentActivityData[Team.A, AgentNumber.Two] = a[1];
                }
                if (Bot[1] != null)
                {
                    Bot[1].Team = Team.B;
                    Bot[1].Question(Calc);
                    var a = Bot[1].Answer();
                    show.agentActivityData[Team.B, AgentNumber.One] = a[0];
                    show.agentActivityData[Team.B, AgentNumber.Two] = a[1];
                }
                TurnEndButton.Text = "ターンエンド";
                TurnEndButton.BackColor = Color.RoyalBlue;
                TurnEndButton.ForeColor = Color.LightGray;
                show.Showing();
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calc.Undo();
            show.Showing();
            WriteLog();
            TurnProgressCheck();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calc.Redo();
            show.Showing();
            WriteLog();
            TurnProgressCheck();
        }

        private void SaveAsSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
            saveFileDialog1.Title = "保存先のファイルを選択してください";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(saveFileDialog1.FileName);

                //XmlSerializerオブジェクトを作成
                //オブジェクトの型を指定する
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(XmlCalc));
                //書き込むファイルを開く（UTF-8 BOM無し）
                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                    saveFileDialog1.FileName, false, new System.Text.UTF8Encoding(false));
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, new XmlCalc(Calc));
                //ファイルを閉じる
                sw.Close();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.Filter = "XMLファイル(*.xml)|*.xml|すべてのファイル(*.*)|*.*";
                saveFileDialog1.Title = "保存先のファイルを選択してください";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            //OKボタンがクリックされたとき、選択されたファイル名を表示する
            Console.WriteLine(saveFileDialog1.FileName);

            //XmlSerializerオブジェクトを作成
            //オブジェクトの型を指定する
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(XmlCalc));
            //書き込むファイルを開く（UTF-8 BOM無し）
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                saveFileDialog1.FileName, false, new System.Text.UTF8Encoding(false));
            //シリアル化し、XMLファイルに保存する
            serializer.Serialize(sw, new XmlCalc(Calc));
            //ファイルを閉じる
            sw.Close();
        }

        private void PracticeModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = PlayMode.PracticeMode;
            TurnEndButton.Text = "ターンエンド";
            TurnEndButton.BackColor = Color.RoyalBlue;
            TurnEndButton.ForeColor = Color.LightGray;
        }

        private void ProductionModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = PlayMode.ProductionMode;
            TurnEndButton.Text = "ボットで選択";
            TurnEndButton.BackColor = Color.DarkGray;
            TurnEndButton.ForeColor = Color.White;
        }
    }
}
