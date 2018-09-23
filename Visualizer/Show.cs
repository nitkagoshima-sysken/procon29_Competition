using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    class Show
    {
        public AgentsActivityData agentActivityData = new AgentsActivityData();

        /// <summary>
        /// 描画するビットマップを指定します。
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// 描画する対象となる計算機を設定または取得します。
        /// </summary>
        public Calc Calc { get; set; }

        /// <summary>
        /// マスの高さを設定または取得します。
        /// </summary>
        protected int CellHeight { get; set; }

        /// <summary>
        /// マスの幅を設定または取得します。
        /// </summary>
        protected int CellWidth { get; set; }

        /// <summary>
        /// 描画するときの色を設定または取得します。
        /// </summary>
        public TeamDesigns TeamDesigns { get; set; } = new TeamDesigns();

        public ClickField ClickField { get; set; }


        // not under

        /// <summary>
        /// 描画するときの色を設定または取得します。
        /// </summary>
        public TeamDesign[] TeamDesign { get; set; }

        /// <summary>
        /// 描画する対象となるPictureBoxを設定または取得します。
        /// </summary>
        public PictureBox PictureBox { get; set; }

        /// <summary>
        /// 背景をどのように塗りつぶすか設定または取得します。
        /// </summary>
        public SolidBrush BackGroundSolidBrush { get; set; } = new SolidBrush(Color.FromArgb(48, 48, 48));

        /// <summary>
        /// 選択したフィールドをどのように塗りつぶすか設定または取得します。
        /// </summary>
        public SolidBrush SelectSolidBrush { get; set; } = new SolidBrush(Color.FromArgb(50, Color.DarkGray));

        /// <summary>
        /// クリックしたフィールドをどのように塗りつぶすか設定または取得します。
        /// </summary>
        public SolidBrush ClickedSolidBrush { get; set; } = new SolidBrush(Color.FromArgb(100, Color.SkyBlue));

        /// <summary>
        /// フォントの設定または取得します。
        /// </summary>
        public Font PointFont { get; set; }

        /// <summary>
        /// クリックしたときのフィールドの場所の設定または取得します。
        /// </summary>
        public Point ClickedField { get; set; }

        /// <summary>
        /// ログを書き込むためのProcon29_Loggerを設定または取得します。
        /// </summary>
        internal Logger Procon29_Logger { get; set; }

        public Team SelectedTeam { get; set; }
        public AgentNumber SelecetedAgent { get; set; }

        /// <summary>
        /// エージェントの画像を設定または取得します。
        /// </summary>
        public Bitmap[] AgentBitmap { get; set; }

        /// <summary>
        /// フルーツフェアリーの画像を設定または取得します。
        /// </summary>
        public Bitmap[] FairyBitmap { get; set; }

        /// <summary>
        /// Procon29_Showの初期化を行います。
        /// </summary>
        /// <param name="procon29_Calc">表示するProcon29_Calc</param>
        /// <param name="teamDesigns">表示するときのチームデザイン</param>
        public Show(Calc procon29_Calc, TeamDesign[] teamDesigns, PictureBox pictureBox)
        {
            Calc = procon29_Calc;
            TeamDesign = teamDesigns;
            PictureBox = pictureBox;

            // ResourceManagerを取得する
            System.Resources.ResourceManager resource = Properties.Resources.ResourceManager;

            //画像ファイルを読み込んで、Imageオブジェクトとして取得する
            AgentBitmap = new Bitmap[2];
            AgentBitmap[0] = (Bitmap)resource.GetObject("Orange");
            AgentBitmap[1] = (Bitmap)resource.GetObject("Lime");
            FairyBitmap = new Bitmap[4];
            FairyBitmap[0] = (Bitmap)resource.GetObject("Strawberry");
            FairyBitmap[1] = (Bitmap)resource.GetObject("Apple");
            FairyBitmap[2] = (Bitmap)resource.GetObject("Kiwi");
            FairyBitmap[3] = (Bitmap)resource.GetObject("Muscat");

            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (AgentNumber agent in Enum.GetValues(typeof(AgentNumber)))
                {
                    agentActivityData[team, agent] = new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[team, agent].Position);
                }
            }

            ClickField = new ClickField(Calc, PictureBox);
        }

        /// <summary>
        /// Procon29_Showの初期化を行います。
        /// </summary>
        /// <param name="procon29_Calc">表示するProcon29_Calc</param>
        /// <param name="teamDesigns">表示するときのチームデザイン</param>
        /// <param name="pictureBox">表示する場所</param>
        public Show(Calc procon29_Calc, TeamDesign[] teamDesigns, PictureBox pictureBox, Logger procon29_Logger)
        {
            Calc = procon29_Calc;
            TeamDesign = teamDesigns;
            PictureBox = pictureBox;
            Procon29_Logger = procon29_Logger;
        }

        /// <summary>
        /// 前のカーソルの状態を設定または取得します。
        /// </summary>
        public Coordinate precursor { get; set; }

        /// <summary>
        /// フィールドを描画するオブジェクトを設定または取得します。
        /// </summary>
        public DrawField drawField { get; set; }

        /// <summary>
        /// PictureBoxを新たに生成します。
        /// </summary>
        /// <param name="pictureBox">表示するPictureBox</param>
        /// <param name="canvas">表示するBitmap</param>
        /// <param name="graphics">表示するGraphics</param>
        /// <returns></returns>
        public Bitmap MakePictureBox(PictureBox pictureBox, Bitmap canvas, Graphics graphics)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Calc.Field.Width;
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Calc.Field.Height;

            Bitmap = canvas;

            var cursor = CursorPosition(PictureBox);
           
                drawField = new DrawField(Calc, canvas);
                drawField.AgentsActivityData = agentActivityData;
                drawField.Draw(cursor);
            precursor = cursor;
            try
            {
                foreach (var agent in Calc.Agents)
                {
                    if (ClickField.ClickedAgent == agent && cursor.ChebyshevDistance(agent.Position) == 1)
                    {
                        drawField.DrawArrow(agent.Team, agent.Position, cursor);
                        break;
                    }
                    else if (cursor.ChebyshevDistance(agent.Position) == 1 && cursor.ChebyshevDistance(ClickField.ClickedAgent.Position) != 1)
                    {
                        drawField.DrawArrow(agent.Team, agent.Position, cursor);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("フィールドの外です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return canvas;
        }

        /// <summary>
        /// 表示を行います。
        /// </summary>
        /// <param name="pictureBox">表示するPictureBoxを指定します</param>
        public void Showing(PictureBox pictureBox)
        {
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(((pictureBox.Width <= 0) ? 1 : pictureBox.Width), ((pictureBox.Height <= 0) ? 1 : pictureBox.Height));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(canvas);

            MakePictureBox(pictureBox, canvas, graphics);

            //リソースを開放
            graphics.Dispose();
            //前のImageオブジェクトのリソースを開放してから
            if (pictureBox.Image != null) pictureBox.Image.Dispose();
            //pictureBoxに表示する
            pictureBox.Image = canvas;
        }

        /// <summary>
        /// クリックした際の表示を行います。
        /// </summary>
        /// <param name="pictureBox">表示するPictureBoxを指定します</param>
        public void ClickedShow(PictureBox pictureBox)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Calc.Field.Width;
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Calc.Field.Height;

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(((pictureBox.Width <= 0) ? 1 : pictureBox.Width), ((pictureBox.Height <= 0) ? 1 : pictureBox.Height));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(canvas);

            Point systemCursorPosition = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            System.Drawing.Point clickedFieldPoint = new System.Drawing.Point(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));

            if (Calc.IsAgentHereOrInMooreNeighborhood(clickedFieldPoint))
            {
                if ((clickedFieldPoint.X < Calc.Field.Width) && (clickedFieldPoint.Y < Calc.Field.Height))
                {
                    ClickedField = new System.Drawing.Point(
                        x: clickedFieldPoint.X,
                        y: clickedFieldPoint.Y);
                }
            }

            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (AgentNumber agent in Enum.GetValues(typeof(AgentNumber)))
                {
                    if (ClickedField == (Point)Calc.Agents[team, agent].Position)
                    {
                        //SelectedTeamAndAgent = (team, agent);
                        SelectedTeam = team;
                        SelecetedAgent = agent;
                    }
                }
            }

            MakePictureBox(pictureBox, canvas, graphics);

            //リソースを開放
            graphics.Dispose();
            //前のImageオブジェクトのリソースを開放してから
            if (pictureBox.Image != null) pictureBox.Image.Dispose();
            //pictureBoxに表示する
            pictureBox.Image = canvas;
        }

        public void ClickShow()
        {
            ClickField.Click();
            agentActivityData = ClickField.AgentsActivityData;
        }

        public void KeyDownShow()
        {
            var next = agentActivityData[SelectedTeam, SelecetedAgent];

            if (Calc.Field[next.Destination.X, next.Destination.Y].IsTileOn[((SelectedTeam == Team.A) ? Team.B : Team.A)])
                agentActivityData[SelectedTeam, SelecetedAgent].AgentStatusData = AgentStatusCode.RequestRemovementOpponentTile;
            else if (Calc.Field[next.Destination.X, next.Destination.Y].IsTileOn[SelectedTeam])
            {
                //メッセージボックスを表示する
                DialogResult result = MessageBox.Show("タイルを取り除きますか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    Console.WriteLine("「はい」が選択されました");
                    agentActivityData[SelectedTeam, SelecetedAgent].AgentStatusData = AgentStatusCode.RequestRemovementOurTile;
                }
                else if (result == DialogResult.No)
                {
                    //「いいえ」が選択された時
                    Console.WriteLine("「いいえ」が選択されました");
                    agentActivityData[SelectedTeam, SelecetedAgent].AgentStatusData = AgentStatusCode.RequestMovement;
                }
            }
            else
                agentActivityData[SelectedTeam, SelecetedAgent].AgentStatusData = AgentStatusCode.RequestMovement;
        }

        /// <summary>
        /// カーソルがどのフィールドの上にいるかを計算します。
        /// </summary>
        /// <returns></returns>
        public Coordinate CursorPosition(PictureBox pictureBox)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Calc.Field.Width;
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Calc.Field.Height;
            System.Drawing.Point systemCursorPosition = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            return new System.Drawing.Point(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
        }


        /// <summary>
        /// Form1内でキーを押したときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            var current = Calc.Agents[SelectedTeam, SelecetedAgent].Position;
            var next = agentActivityData[SelectedTeam, SelecetedAgent];

            Console.WriteLine(e.KeyCode);
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Q:
                        SelectedTeam = Team.A;
                        SelecetedAgent = AgentNumber.One;
                        break;
                    case Keys.W:
                        SelectedTeam = Team.A;
                        SelecetedAgent = AgentNumber.Two;
                        break;
                    case Keys.E:
                        SelectedTeam = Team.B;
                        SelecetedAgent = AgentNumber.One;
                        break;
                    case Keys.R:
                        SelectedTeam = Team.B;
                        SelecetedAgent = AgentNumber.Two;
                        break;
                    case Keys.NumPad1:
                        next.Destination = current + Arrow.DownLeft;
                        break;
                    case Keys.NumPad2:
                        next.Destination = current + Arrow.Down;
                        break;
                    case Keys.NumPad3:
                        next.Destination = current + Arrow.DownRight;
                        break;
                    case Keys.NumPad4:
                        next.Destination = current + Arrow.Left;
                        break;
                    case Keys.NumPad6:
                        next.Destination = current + Arrow.Right;
                        break;
                    case Keys.NumPad7:
                        next.Destination = current + Arrow.UpLeft;
                        break;
                    case Keys.NumPad8:
                        next.Destination = current + Arrow.Up;
                        break;
                    case Keys.NumPad9:
                        next.Destination = current + Arrow.UpRight;
                        break;
                }

                current = Calc.Agents[SelectedTeam, SelecetedAgent].Position;
                next = agentActivityData[SelectedTeam, SelecetedAgent];

                ClickedField = current;
                if (next.Destination.X < 0 ||
                    next.Destination.Y < 0 ||
                    next.Destination.X >= Calc.Field.Width ||
                    next.Destination.Y >= Calc.Field.Height)
                {
                    next.Destination = current;
                    throw new Exception();
                }
                else if (current != next.Destination)
                {
                    KeyDownShow();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("不正なキー入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Showing(PictureBox);
        }


    }
}
