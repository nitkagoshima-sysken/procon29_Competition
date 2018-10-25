using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// 描画処理をします。
        /// </summary>
        private new Show Show { get; set; }

        /// <summary>
        /// ログを取ります。
        /// </summary>
        public static Logger Log { get; set; }

        /// <summary>
        /// ボットのログを取ります。
        /// </summary>
        public static Logger BotLog { get; set; }

        TeamDesign[] TeamDesign;

        /// <summary>
        /// 新たな戦いを始めるためのフォームです。
        /// </summary>
        public CreateNewForm CreateNewForm { get; set; } = new CreateNewForm();

        /// <summary>
        /// ボットを選択するためのフォームです。
        /// </summary>
        BotForm BotForm { get; set; } = new BotForm();

        /// <summary>
        /// ボットのログを表示します。
        /// </summary>
        public BotLogForm BotLogForm { get; set; } = new BotLogForm();

        /// <summary>
        /// 敵の位置の入力補助を表示します。
        /// </summary>
        public OpponentPositionForm OpponentPositionForm { get; set; } = new OpponentPositionForm();

        /// <summary>
        /// ボットを設定または取得します。
        /// </summary>
        public static Bot[] Bot { get; set; } = new Bot[2];

        /// <summary>
        /// 最大ターンのデフォルト値を設定または取得します。
        /// </summary>
        public static int MaxTurn = VisualizerDefaultStyle.MaxTurn;

        /// <summary>
        /// デバッグモードを設定または取得します。
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// ゲームのモードを設定または取得します。
        /// </summary>
        private PlayMode Mode { get; set; } = PlayMode.PracticeMode;

        /// <summary>
        /// QRコードリーダーのファイルパスを設定または取得します。
        /// </summary>
        public string QRCodeReader_FilePath;

        /// <summary>
        /// FieldDataGeneratorのファイルパスを設定または取得します。
        /// </summary>
        public string FieldDataGenerator_FilePath;

        /// <summary>
        /// HydroGoBotのファイルパスを設定または取得します。
        /// </summary>
        public string HydroGoBot_FilePath;

        /// <summary>
        /// 処理時間のデータのリストを表します。
        /// </summary>
        public static List<TimeData> TimeDataList { get; set; } = new List<TimeData>();

        private TrumpShow TrumpShow = new TrumpShow();

        /// <summary>
        /// MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            FieldDisplay.MouseMove += new MouseEventHandler(FieldDisplay_MouseMove);
            Resize += new System.EventHandler(MainForm_Resize);
            BotLogForm.FormClosing += BotLogForm_Closing;
            CreateNewForm.OKButton.Click += CreateNewForm_OKButton_Click;
            BotForm.OKButton.Click += BotForm_OKButton_Click;

            Log = new Logger(messageBox);
            var version = System.Diagnostics.FileVersionInfo.GetVersionInfo(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
            Log.WriteLine("Procon29 Visualizer (ver. " + version.FileMinorPart + "." + version.FileBuildPart + ")");

            BotLog = new Logger(BotLogForm.BotLogRichText);

            // PQRファイルを直接読み込む
            // ちなみにQR_code_sample.pdfで登場したQRコード
            var reader = new PqrReader
            {
                Stream =
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
                "7 10:"
            };
            Log.WriteLine(reader.Stream);
            var pqr = reader.ConvertToPqrData();

            var agents = new Agents();
            agents[Team.A, AgentNumber.One].Position = pqr.One;
            agents[Team.A, AgentNumber.Two].Position = pqr.Two;

            var field = new Field(pqr.Fields.GetLength(1), pqr.Fields.GetLength(0), pqr.Fields);
            OpponentPositionForm.OurTeamPositionLabel.Text = "自分:" + pqr.One + pqr.Two;
            OpponentPositionForm.OpponentPosition1X.Text = ComplementEnemysPosition(field, pqr.One).X.ToString();
            OpponentPositionForm.OpponentPosition1Y.Text = ComplementEnemysPosition(field, pqr.One).Y.ToString();
            OpponentPositionForm.OpponentPosition2X.Text = ComplementEnemysPosition(field, pqr.Two).X.ToString();
            OpponentPositionForm.OpponentPosition2Y.Text = ComplementEnemysPosition(field, pqr.Two).Y.ToString();

            OpponentPositionForm.ShowDialog(this);
            agents[Team.B, AgentNumber.One].Position =
                new Coordinate(
                    int.Parse(OpponentPositionForm.OpponentPosition1X.Text),
                    int.Parse(OpponentPositionForm.OpponentPosition1Y.Text));
            agents[Team.B, AgentNumber.Two].Position =
                new Coordinate(
                    int.Parse(OpponentPositionForm.OpponentPosition2X.Text),
                    int.Parse(OpponentPositionForm.OpponentPosition2Y.Text));

            Calc = new Calc(MaxTurn, field, agents);

            PrefetchingFileReader.BotsTsv(this);
            PrefetchingFileReader.CalcTsv(this);
            PrefetchingFileReader.FilePathTsv(this);

            TeamDesign =
                new TeamDesign[2] {
                    new TeamDesign(name: "Orange", agentColor: Color.DarkOrange, areaColor: Color.DarkOrange),
                    new TeamDesign(name: "Lime", agentColor: Color.LimeGreen, areaColor: Color.LimeGreen),
                };
            Show = new Show(Calc, TeamDesign, FieldDisplay);
            Show.Showing();
            Calc.PointMapCheck();

            KeyDown += new KeyEventHandler(Show.KeyDown);
            KeyDown += new KeyEventHandler(MainForm_KeyDown);

            WriteLog();
            TurnProgressCheck();
            Log.CursorScroll();
        }

        private void WriteLog()
        {
            Log.WriteLine("\n" + "Turn : " + Calc.Turn);
            Log.WriteLine("A   Area Point: " + Calc.Field.AreaPoint(Team.A).ToString(), TeamDesign[(int)Team.A].AreaColor);
            Log.WriteLine("Enclosed Point: " + Calc.Field.EnclosedPoint(Team.A).ToString(), TeamDesign[(int)Team.A].AreaColor);
            Log.WriteLine("   Total Point: " + Calc.Field.TotalPoint(Team.A).ToString(), TeamDesign[(int)Team.A].AreaColor);
            Log.WriteLine("agent: " + Calc.Agents[Team.A, AgentNumber.One].Position, TeamDesign[(int)Team.A].AreaColor);
            Log.WriteLine("agent: " + Calc.Agents[Team.A, AgentNumber.Two].Position, TeamDesign[(int)Team.A].AreaColor);
            Log.WriteLine("B   Area Point: " + Calc.Field.AreaPoint(Team.B).ToString(), TeamDesign[(int)Team.B].AreaColor);
            Log.WriteLine("Enclosed Point: " + Calc.Field.EnclosedPoint(Team.B).ToString(), TeamDesign[(int)Team.B].AreaColor);
            Log.WriteLine("   Total Point: " + Calc.Field.TotalPoint(Team.B).ToString(), TeamDesign[(int)Team.B].AreaColor);
            Log.WriteLine("agent: " + Calc.Agents[Team.B, AgentNumber.One].Position, TeamDesign[(int)Team.B].AreaColor);
            Log.WriteLine("agent: " + Calc.Agents[Team.B, AgentNumber.Two].Position, TeamDesign[(int)Team.B].AreaColor);
        }

        void TurnProgressCheck()
        {
            toolStripProgressBar1.Maximum = Calc.MaxTurn;
            toolStripProgressBar1.Value = Calc.Turn;
            if (Calc.IsEnd)
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
            Show.Showing();
            TrumpShowing();
        }

        /// <summary>
        /// FieldDisplay内でクリックしたときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            Show.ClickedShow(FieldDisplay);
            Show.ClickShow();
            messageBox.Select(messageBox.Text.Length, 0);
            try
            {
                TrumpShowing();
            }
            catch (Exception)
            {
                MessageBox.Show("なぜかここでエラーになります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        Coordinate precursor = new Coordinate(-1, -1);

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
            if (precursor == ((Coordinate)e.Location).ToCellCordinate(FieldDisplay, Calc.Field))
            {
                if (delta.TotalMilliseconds >= 5.0)
                {
                    CellInformationToolStripStatusLabel_Review(e.Location);
                }
                time = DateTime.Now;
            }
            precursor = ((Coordinate)e.Location).ToCellCordinate(FieldDisplay, Calc.Field);
        }

        /// <summary>
        /// CellInformationToolStripStatusLabel を更新します。
        /// </summary>
        /// <param name="mouse"></param>
        private void CellInformationToolStripStatusLabel_Review(Coordinate mouse)
        {
            var cell_position = mouse.ToCellCordinate(FieldDisplay, Calc.Field);
            CellInformationToolStripStatusLabel.Text = "[Turn: " + Calc.Turn + "/" + Calc.MaxTurn + "]";
            if (Calc.Field.CellExist(cell_position))
            {
                var cell = Calc.Field[cell_position];
                // 情報を表示
                CellInformationToolStripStatusLabel.Text += ("[Coordinate: " + cell_position + " Point: " + cell.Point);
                // 囲まれているか判定
                if (cell.IsEnclosed[Team.A] && cell.IsEnclosed[Team.B]) CellInformationToolStripStatusLabel.Text += " (Surrounded by both)";
                else if (cell.IsEnclosed[Team.A])
                {
                    CellInformationToolStripStatusLabel.Text += " (Surrounded by " + TeamDesign[0].Name + ")";
                    CellInformationToolStripStatusLabel.ForeColor = TeamDesign[0].AgentColor;
                }
                else if (cell.IsEnclosed[Team.B])
                {
                    CellInformationToolStripStatusLabel.Text += " (Surrounded by " + TeamDesign[1].Name + ")";
                    CellInformationToolStripStatusLabel.ForeColor = TeamDesign[1].AgentColor;
                }
                CellInformationToolStripStatusLabel.Text += "]";
                // タイルがおかれているか判定
                if (cell.IsTileOn[Team.A]) CellInformationToolStripStatusLabel.ForeColor = TeamDesign[0].AgentColor;
                else if (cell.IsTileOn[Team.B]) CellInformationToolStripStatusLabel.ForeColor = TeamDesign[1].AgentColor;
                else if ((!cell.IsEnclosed[Team.A] && !cell.IsEnclosed[Team.B])) CellInformationToolStripStatusLabel.ForeColor = Color.LightGray;
            }
            Show.Showing();
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
            if (e.KeyCode == Keys.Enter)
            {
                TurnEnd();
                Show.Showing();
            }
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
                Show.Calc = Calc;
                Show.ClickField = new ClickField(Calc, Show.PictureBox);
                Show.Showing();
            }
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
                Log.WriteLine(reader.Stream);
                var pqr = reader.ConvertToPqrData();
                pqr.IsRegular();

                var agents = new Agents();
                agents[Team.A, AgentNumber.One].Position = pqr.One;
                agents[Team.A, AgentNumber.Two].Position = pqr.Two;

                var field = new Field(pqr.Fields.GetLength(1), pqr.Fields.GetLength(0), pqr.Fields);
                OpponentPositionForm.OurTeamPositionLabel.Text = "自分:" + pqr.One + pqr.Two;
                OpponentPositionForm.OpponentPosition1X.Text = ComplementEnemysPosition(field, pqr.One).X.ToString();
                OpponentPositionForm.OpponentPosition1Y.Text = ComplementEnemysPosition(field, pqr.One).Y.ToString();
                OpponentPositionForm.OpponentPosition2X.Text = ComplementEnemysPosition(field, pqr.Two).X.ToString();
                OpponentPositionForm.OpponentPosition2Y.Text = ComplementEnemysPosition(field, pqr.Two).Y.ToString();

                OpponentPositionForm.ShowDialog(this);
                agents[Team.B, AgentNumber.One].Position =
                    new Coordinate(
                        int.Parse(OpponentPositionForm.OpponentPosition1X.Text),
                        int.Parse(OpponentPositionForm.OpponentPosition1Y.Text));
                agents[Team.B, AgentNumber.Two].Position =
                    new Coordinate(
                        int.Parse(OpponentPositionForm.OpponentPosition2X.Text),
                        int.Parse(OpponentPositionForm.OpponentPosition2Y.Text));

                Calc = new Calc(MaxTurn, field, agents);

                Show = new Show(Calc, TeamDesign, FieldDisplay);
                Show.Showing();
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(
                    "そんなディレクトリは存在しません。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(
                    "そんなファイルは存在しません。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void TurnEndButton_Click(object sender, EventArgs e)
        {
            TurnEnd();
        }

        private void TurnEnd()
        {
            var action = new AgentsActivityData();
            var stopwatch_all = new Stopwatch();
            stopwatch_all.Start();
            if (TurnEndButton.Text == "ターンエンド")
            {
                if (Mode == PlayMode.ProductionMode)
                {
                    if (Bot[0].Body != null)
                    {
                        Show.AgentsActivityData[Team.A, AgentNumber.One] = Show.AgentsActivityData[Team.A, AgentNumber.One];
                        Show.AgentsActivityData[Team.A, AgentNumber.Two] = Show.AgentsActivityData[Team.A, AgentNumber.Two];
                    }
                    if (Bot[1].Body != null)
                    {
                        Show.AgentsActivityData[Team.B, AgentNumber.One] = Show.AgentsActivityData[Team.B, AgentNumber.One];
                        Show.AgentsActivityData[Team.B, AgentNumber.Two] = Show.AgentsActivityData[Team.B, AgentNumber.Two];
                    }
                    TurnEndButton.Text = "ボットで選択";
                    TurnEndButton.BackColor = Color.DarkGray;
                    TurnEndButton.ForeColor = Color.White;
                }
                else
                {
                    if (Bot[0].Body != null)
                    {
                        var stopwatch = new Stopwatch();
                        stopwatch.Start();
                        BotAnswer((int)Team.A);
                        stopwatch.Stop();
                        TimeDataList.Add(new TimeData(Bot[0].AssemblyName.Name + " (Our Team) of " + Calc.Turn, stopwatch.ElapsedMilliseconds));
                    }
                    if (Bot[1].Body != null)
                    {
                        var stopwatch = new Stopwatch();
                        stopwatch.Start();
                        BotAnswer((int)Team.B);
                        stopwatch.Stop();
                        TimeDataList.Add(new TimeData(Bot[1].AssemblyName.Name + " (Opponent Team) of " + Calc.Turn, stopwatch.ElapsedMilliseconds));
                    }
                    if (Bot[0].AssemblyName.Name == "hydro_go_bot")
                    {
                        using (var sw = new StreamWriter("object.in", false))
                        {
                            sw.Write(CalcExtension.HydroGoBotAdapter(Calc, Team.A));
                        }
                        Log.WriteLine("[HGbot] Calling now...");
                        using (var p = new Process())
                        {
                            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.RedirectStandardInput = false;
                            p.StartInfo.Arguments = @"/c "+ HydroGoBot_FilePath;
                            p.Start();
                            p.WaitForExit();
                            string results = p.StandardOutput.ReadToEnd();
                            BotLog.WriteLine(results);
                            BotLog.CursorScroll();
                        }
                        var data = string.Empty;
                        using (var sr = new StreamReader("object.out"))
                        {
                            data= sr.ReadToEnd();
                        }
                        var re = new Regex(@"\[\[(?<onex>\d+), (?<oney>\d+)\], \[(?<twox>\d+), (?<twoy>\d+)\]\]");
                        var m = re.Match(data);
                        Show.AgentsActivityData[Team.A, AgentNumber.One].Destination = new Coordinate(int.Parse(m.Groups["onex"].Value), int.Parse(m.Groups["oney"].Value));
                        Show.AgentsActivityData[Team.A, AgentNumber.Two].Destination = new Coordinate(int.Parse(m.Groups["twox"].Value), int.Parse(m.Groups["twoy"].Value));
                        var d = Show.AgentsActivityData[Team.A, AgentNumber.One].Destination;
                        if (Calc.Field.CellExist(d) && Calc.Field[d].IsTileOn[Team.A.Opponent()])
                        {
                            Show.AgentsActivityData[Team.A, AgentNumber.One].AgentStatusData = AgentStatusCode.RequestRemovementOpponentTile;
                        }
                        else
                        {
                            Show.AgentsActivityData[Team.A, AgentNumber.One].AgentStatusData = AgentStatusCode.RequestMovement;
                        }
                        d = Show.AgentsActivityData[Team.A, AgentNumber.Two].Destination;
                        if (Calc.Field.CellExist(d) && Calc.Field[d].IsTileOn[Team.A.Opponent()])
                        {
                            Show.AgentsActivityData[Team.A, AgentNumber.Two].AgentStatusData = AgentStatusCode.RequestRemovementOpponentTile;
                        }
                        else
                        {
                            Show.AgentsActivityData[Team.A, AgentNumber.Two].AgentStatusData = AgentStatusCode.RequestMovement;
                        }
                    }
                    if (Bot[1].AssemblyName.Name == "hydro_go_bot")
                    {
                        using (var sw = new StreamWriter("object.in", false))
                        {
                            sw.Write(CalcExtension.HydroGoBotAdapter(Calc, Team.B));
                        }
                        Log.WriteLine("[HGbot] Calling now...");
                        using (var p = new Process())
                        {
                            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                            p.StartInfo.CreateNoWindow = true;
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.RedirectStandardOutput = true;
                            p.StartInfo.RedirectStandardInput = false;
                            p.StartInfo.Arguments = @"/c " + HydroGoBot_FilePath;
                            p.Start();
                            p.WaitForExit();
                            string results = p.StandardOutput.ReadToEnd();
                            BotLog.WriteLine(results);
                            BotLog.CursorScroll();
                        }
                        var data = string.Empty;
                        using (var sr = new StreamReader("object.out"))
                        {
                            data = sr.ReadToEnd();
                        }
                        var re = new Regex(@"\[\[(?<onex>\d+), (?<oney>\d+)\], \[(?<twox>\d+), (?<twoy>\d+)\]\]");
                        var m = re.Match(data);
                        Show.AgentsActivityData[Team.B, AgentNumber.One].Destination = new Coordinate(int.Parse(m.Groups["onex"].Value), int.Parse(m.Groups["oney"].Value));
                        Show.AgentsActivityData[Team.B, AgentNumber.Two].Destination = new Coordinate(int.Parse(m.Groups["twox"].Value), int.Parse(m.Groups["twoy"].Value));
                        var d = Show.AgentsActivityData[Team.B, AgentNumber.One].Destination;
                        if (Calc.Field.CellExist(d) && Calc.Field[d].IsTileOn[Team.B.Opponent()])
                        {
                            Show.AgentsActivityData[Team.B, AgentNumber.One].AgentStatusData = AgentStatusCode.RequestRemovementOpponentTile;
                        }
                        else
                        {
                            Show.AgentsActivityData[Team.B, AgentNumber.One].AgentStatusData = AgentStatusCode.RequestMovement;
                        }
                        d = Show.AgentsActivityData[Team.B, AgentNumber.Two].Destination;
                        if (Calc.Field.CellExist(d) && Calc.Field[d].IsTileOn[Team.B.Opponent()])
                        {
                            Show.AgentsActivityData[Team.B, AgentNumber.Two].AgentStatusData = AgentStatusCode.RequestRemovementOpponentTile;
                        }
                        else
                        {
                            Show.AgentsActivityData[Team.B, AgentNumber.Two].AgentStatusData = AgentStatusCode.RequestMovement;
                        }
                    }
                }
                stopwatch_all.Stop();
                TimeDataList.Add(new TimeData("Turn End of " + Calc.Turn, stopwatch_all.ElapsedMilliseconds));
            }
            else
            {
                if (Bot[0].Body != null)
                {
                    Bot[0].Body.OurTeam = (Team)0;
                    Bot[0].Body.Log = BotLog;
                    Bot[0].Body.Question(new Calc(Calc));
                    var answer = Bot[0].Body.Answer();
                    Show.AgentsActivityData[(Team)0, AgentNumber.One] = answer[0];
                    Show.AgentsActivityData[(Team)0, AgentNumber.Two] = answer[1];
                }
                if (Bot[1].Body != null)
                {
                    Bot[1].Body.OurTeam = (Team)1;
                    Bot[1].Body.Log = BotLog;
                    Bot[1].Body.Question(new Calc(Calc));
                    var answer = Bot[1].Body.Answer();
                    Show.AgentsActivityData[(Team)1, AgentNumber.One] = answer[0];
                    Show.AgentsActivityData[(Team)1, AgentNumber.Two] = answer[1];
                }
                TurnEndButton.Text = "ターンエンド";
                TurnEndButton.BackColor = Color.RoyalBlue;
                TurnEndButton.ForeColor = Color.LightGray;
                Show.Showing();
                TrumpShowing();
                return;
            }

            Calc.MoveAgent(Show.AgentsActivityData);

            foreach (AgentActivityData item in Show.AgentsActivityData)
            {
                item.AgentStatusData = AgentStatusCode.NotDoneAnything;
            }

            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            Show.Showing();
            stopwatch2.Stop();
            TimeDataList.Add(new TimeData("Draw", stopwatch2.ElapsedMilliseconds));

            WriteLog();
            TurnProgressCheck();

            if (Bot[0].Body != null)
            {
                Log.WriteLine("[" + Bot[0].AssemblyName.Name + "]", Color.SkyBlue);
                var d = Calc.History[Calc.Turn - 1].AgentsActivityData[Team.A, AgentNumber.One];
                Log.WriteLine("A1 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString(), Color.SkyBlue);
                d = Calc.History[Calc.Turn - 1].AgentsActivityData[Team.A, AgentNumber.Two];
                Log.WriteLine("A2 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString(), Color.SkyBlue);
            }
            if (Bot[1].Body != null)
            {
                Log.WriteLine("[" + Bot[1].AssemblyName.Name + "]", Color.SkyBlue);
                var d = Calc.History[Calc.Turn - 1].AgentsActivityData[Team.B, AgentNumber.One];
                Log.WriteLine("B1 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString(), Color.SkyBlue);
                d = Calc.History[Calc.Turn - 1].AgentsActivityData[Team.B, AgentNumber.Two];
                Log.WriteLine("B2 => " + d.Destination.ToString() + " " + d.AgentStatusData.ToString(), Color.SkyBlue);
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

            CellInformationToolStripStatusLabel_Review(new Coordinate());
            Log.CursorScroll();
        }

        private void TrumpShowing()
        {
            if (TrumpToolStripMenuItem.Checked)
            {
                TrumpShow.PictureBox = TrumpPictureBox;
                TrumpShow.Showing(Calc, Team.A, new AgentActivityData[] { Show.AgentsActivityData[Team.A, AgentNumber.One], Show.AgentsActivityData[Team.A, AgentNumber.Two] });
            }
        }

        private void BotAnswer()
        {
            for (int i = 0; i < 2; i++)
            {
                BotAnswer(i);
            }
        }

        private void BotAnswer(int i)
        {
            if (Bot[i].Body != null)
            {
                Bot[i].Body.OurTeam = (Team)i;
                Bot[i].Body.Log = BotLog;
                Bot[i].Body.Question(new Calc(Calc));
                var answer = Bot[i].Body.Answer();
                Show.AgentsActivityData[(Team)i, AgentNumber.One] = answer[0];
                Show.AgentsActivityData[(Team)i, AgentNumber.Two] = answer[1];
            }
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calc.Undo();
            Show.Showing();
            WriteLog();
            TurnProgressCheck();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calc.Redo();
            Show.Showing();
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
            BotLogForm.Show();
        }

        private void ProductionModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mode = PlayMode.ProductionMode;
            TurnEndButton.Text = "ボットで選択";
            TurnEndButton.BackColor = Color.DarkGray;
            TurnEndButton.ForeColor = Color.White;
            BotLogForm.Hide();
        }

        private void OpenQRCodeReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(QRCodeReader_FilePath))
            {
                System.Diagnostics.Process.Start(QRCodeReader_FilePath);
            }
            else
            {
                MessageBox.Show("QR Code Readerのファイルパスが分かりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenFieldDataGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(FieldDataGenerator_FilePath))
            {
                System.Diagnostics.Process.Start(FieldDataGenerator_FilePath);
            }
            else
            {
                MessageBox.Show("Field Data Generatorのファイルパスが分かりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BotConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BotConsoleToolStripMenuItem.Checked)
            {
                BotConsoleToolStripMenuItem.Checked = false;
                BotLogForm.Hide();
            }
            else
            {
                BotConsoleToolStripMenuItem.Checked = true;
                BotLogForm.Show();
            }
        }

        /// <summary>
        /// QRコードには自分のチームの位置情報しか分からないため、
        /// 敵の位置情報を自分の位置から補完します。
        /// </summary>
        /// <param name="field">対象のフィールド</param>
        /// <param name="coordinate">対象の座標</param>
        /// <returns></returns>
        public static Coordinate ComplementEnemysPosition(Field field, Coordinate coordinate)
        {
            if (field.IsVerticallySymmetrical)
            {
                return field.FlipVertical(coordinate);
            }
            else if (field.IsHorizontallySymmetrical)
            {
                return field.FlipHorizontal(coordinate);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private void TimeMeasurementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TimeMeasurementForm().ShowDialog(this);
        }

        static Coordinate RightClickedCellCoordinate;

        private void FieldDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    ContextMenuStrip.Show((Coordinate)Location + e.Location);
                    if (Calc.Field.CellExist(((Coordinate)e.Location).ToCellCordinate(FieldDisplay, Calc.Field)))
                    {
                        RightClickedCellCoordinate = ((Coordinate)e.Location).ToCellCordinate(FieldDisplay, Calc.Field);
                        if (Calc.Field[RightClickedCellCoordinate].IsTileOn[Team.A])
                        {
                            AreaToolStripComboBox.SelectedIndex = 0;
                        }
                        else if (Calc.Field[RightClickedCellCoordinate].IsTileOn[Team.B])
                        {
                            AreaToolStripComboBox.SelectedIndex = 1;
                        }
                        else
                        {
                            AreaToolStripComboBox.SelectedIndex = 2;
                        }
                    }
                    break;
            }
        }

        private void AreaToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (AreaToolStripComboBox.SelectedIndex)
            {
                case 0:
                    Calc.Field[RightClickedCellCoordinate].IsTileOn[Team.A] = true;
                    Calc.Field[RightClickedCellCoordinate].IsTileOn[Team.B] = false;
                    break;
                case 1:
                    Calc.Field[RightClickedCellCoordinate].IsTileOn[Team.A] = false;
                    Calc.Field[RightClickedCellCoordinate].IsTileOn[Team.B] = true;
                    break;
                case 2:
                    Calc.Field[RightClickedCellCoordinate].IsTileOn[Team.A] = false;
                    Calc.Field[RightClickedCellCoordinate].IsTileOn[Team.B] = false;
                    break;
            }
            Calc.Recalculation();
            Show.Showing();
        }

        private void TrumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrumpToolStripMenuItem.Checked)
            {
                TrumpToolStripMenuItem.Checked = false;
                tableLayoutPanel2.RowStyles[0] = new RowStyle(SizeType.Percent, 0F);
                tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Percent, 80F);
                tableLayoutPanel2.RowStyles[2] = new RowStyle(SizeType.Percent, 20F);
            }
            else
            {
                TrumpToolStripMenuItem.Checked = true;
                tableLayoutPanel2.RowStyles[0] = new RowStyle(SizeType.Percent, 30F);
                tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Percent, 50F);
                tableLayoutPanel2.RowStyles[2] = new RowStyle(SizeType.Percent, 20F);
            }
        }

        private void EndButtleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case PlayMode.PracticeMode:
                    if (MessageBox.Show("試合を終わらせるのに、時間がかかる場合があります。よろしいですか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        while (Calc.Turn < Calc.MaxTurn)
                        {
                            TurnEnd();
                        }
                    }
                    break;
                case PlayMode.ProductionMode:
                    MessageBox.Show("安全のため、本番モードでは使用できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    break;
            }
        }

        private void BotLogForm_Closing(object sender, FormClosingEventArgs e)
        {
            BotConsoleToolStripMenuItem.Checked = false;
        }

        private void CreateNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case PlayMode.PracticeMode:
                    CreateNewForm.FieldKindComboBox.SelectedIndex = 1;
                    break;
                case PlayMode.ProductionMode:
                    CreateNewForm.FieldKindComboBox.SelectedIndex = 2;
                    break;
            }
            CreateNewForm.ShowDialog();
        }

        private void CreateNewForm_OKButton_Click(object sender, EventArgs e)
        {
            CreateNewForm.Hide();
            MaxTurn = int.Parse(CreateNewForm.MaxTurnMaskedTextBox.Text);
            switch (CreateNewForm.FieldKindComboBox.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    Load_FieldGenerator(CreateNewForm.SelectedPQRFileNameLabel.Text);
                    break;
                case 2:
                    OpenPQRFile(CreateNewForm.SelectedPQRFileNameLabel.Text);
                    break;
            }
            TurnProgressCheck();
        }

        void Load_FieldGenerator(string id)
        {
            var field_generator = new FieldGenerator(id);
            var agents = new Agents();
            var field = field_generator.Generate();
            var coordinates = field_generator.AgentPositionGenerate(field);
            agents[Team.A, AgentNumber.One].Position = coordinates[0];
            agents[Team.A, AgentNumber.Two].Position = coordinates[1];
            OpponentPositionForm.OurTeamPositionLabel.Text = "自分:" + coordinates[0] + coordinates[1];
            OpponentPositionForm.OpponentPosition1X.Text = ComplementEnemysPosition(field, coordinates[0]).X.ToString();
            OpponentPositionForm.OpponentPosition1Y.Text = ComplementEnemysPosition(field, coordinates[0]).Y.ToString();
            OpponentPositionForm.OpponentPosition2X.Text = ComplementEnemysPosition(field, coordinates[1]).X.ToString();
            OpponentPositionForm.OpponentPosition2Y.Text = ComplementEnemysPosition(field, coordinates[1]).Y.ToString();
            OpponentPositionForm.ShowDialog(this);
            agents[Team.B, AgentNumber.One].Position =
                new Coordinate(
                    int.Parse(OpponentPositionForm.OpponentPosition1X.Text),
                    int.Parse(OpponentPositionForm.OpponentPosition1Y.Text));
            agents[Team.B, AgentNumber.Two].Position =
                new Coordinate(
                    int.Parse(OpponentPositionForm.OpponentPosition2X.Text),
                    int.Parse(OpponentPositionForm.OpponentPosition2Y.Text));
            Calc = new Calc(MaxTurn, field, agents);
            Show = new Show(Calc, TeamDesign, FieldDisplay);
            Show.Showing();
            Log.WriteLine("[Create New] Visualizer Common Field Code is " + id, Color.SkyBlue);
        }

        private void SelectBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BotForm.ShowDialog();
        }

        private void BotForm_OKButton_Click(object sender, EventArgs e)
        {
            BotForm.Hide();
            switch (BotForm.OrangeBotKindComboBox.SelectedIndex)
            {
                case 0: // 人間
                    Bot[0].Body = null;
                    Bot[0].AssemblyName = new AssemblyName();
                    Bot[0].AssemblyName.Name = "Human";
                    Log.WriteLine("[Bot] Changed to Human on orange team.", Color.SkyBlue);
                    break;
                case 1: // ボット
                    try
                    {
                        Bot[0] = Visualizer.Bot.Connect(BotForm.SelectedOrangeBotNameLabel.Text);
                        Log.WriteLine("[Bot] \"" + Bot[0].AssemblyName.Name + "\" was read on lime team", Color.SkyBlue);
                        Log.WriteLine("[" + Bot[0].AssemblyName.Name + "]", Color.SkyBlue);
                        Log.WriteLine("Code Base: " + Bot[0].AssemblyName.CodeBase.ToString(), Color.SkyBlue);
                        Log.WriteLine("Flags: " + Bot[0].AssemblyName.Flags.ToString(), Color.SkyBlue);
                        Log.WriteLine("Hash Algorithm: " + Bot[0].AssemblyName.HashAlgorithm.ToString(), Color.SkyBlue);
                        Log.WriteLine("Processor Architecture: " + Bot[0].AssemblyName.ProcessorArchitecture.ToString(), Color.SkyBlue);
                        Log.WriteLine("Version: " + Bot[0].AssemblyName.Version.ToString(), Color.SkyBlue);
                        Log.WriteLine("Version Compatibility: " + Bot[0].AssemblyName.VersionCompatibility.ToString(), Color.SkyBlue);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Caught Exception");
                    }
                    break;
                case 2: // Hydro Go Bot
                    Bot[0].Body = null;
                    Bot[0].AssemblyName.Name = "hydro_go_bot";
                    break;
            }
            switch (BotForm.LimeBotKindComboBox.SelectedIndex)
            {
                case 0: // 人間
                    Bot[1].Body = null;
                    Bot[1].AssemblyName = new AssemblyName();
                    Bot[1].AssemblyName.Name = "Human";
                    Log.WriteLine("[Bot] Changed to Human on lime team.", Color.SkyBlue);
                    break;
                case 1: // ボット
                    try
                    {
                        Bot[1] = Visualizer.Bot.Connect(BotForm.SelectedLimeBotNameLabel.Text);
                        Log.WriteLine("[Bot] \"" + Bot[1].AssemblyName.Name + "\" was read on lime team", Color.SkyBlue);
                        Log.WriteLine("[" + Bot[1].AssemblyName.Name + "]", Color.SkyBlue);
                        Log.WriteLine("Code Base: " + Bot[1].AssemblyName.CodeBase.ToString(), Color.SkyBlue);
                        Log.WriteLine("Flags: " + Bot[1].AssemblyName.Flags.ToString(), Color.SkyBlue);
                        Log.WriteLine("Hash Algorithm: " + Bot[1].AssemblyName.HashAlgorithm.ToString(), Color.SkyBlue);
                        Log.WriteLine("Processor Architecture: " + Bot[1].AssemblyName.ProcessorArchitecture.ToString(), Color.SkyBlue);
                        Log.WriteLine("Version: " + Bot[1].AssemblyName.Version.ToString(), Color.SkyBlue);
                        Log.WriteLine("Version Compatibility: " + Bot[1].AssemblyName.VersionCompatibility.ToString(), Color.SkyBlue);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Caught Exception");
                    }
                    break;
                case 2: // Hydro Go Bot
                    Bot[1].Body = null;
                    Bot[1].AssemblyName.Name = "hydro_go_bot";
                    break;
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new BotWarsForm(this);
            form.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Mode == PlayMode.ProductionMode)
            {
                MessageBox.Show("本番中です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void SwapAgentPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = Calc.Agents[Team.A, AgentNumber.One].Position;
            Calc.Agents[Team.A, AgentNumber.One].Position = Calc.Agents[Team.A, AgentNumber.Two].Position;
            Calc.Agents[Team.A, AgentNumber.Two].Position = temp;
        }

        NameForm NameForm;
        private void ChangeAgentNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NameForm = new NameForm(Calc);
            NameForm.OKButton.Click += NameForm_OKButton_Click;
            NameForm.ShowDialog();
        }

        private void NameForm_OKButton_Click(object sender, EventArgs e)
        {
            Calc.Agents[Team.A, AgentNumber.One].Name = NameForm.NameTextBox1.Text;
            Calc.Agents[Team.A, AgentNumber.Two].Name = NameForm.NameTextBox2.Text;
            NameForm.Close();
        }
    }
}
