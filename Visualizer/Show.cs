using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    class Show
    {
        public AgentsActivityData AgentsActivityData = new AgentsActivityData();

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

        public ClickField ClickField { get; set; }

        /// <summary>
        /// 描画する対象となるPictureBoxを設定または取得します。
        /// </summary>
        public PictureBox PictureBox { get; set; }

        /// <summary>
        /// クリックしたときのフィールドの場所の設定または取得します。
        /// </summary>
        public Point ClickedField { get; set; }

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
        /// <param name="procon29_Calc">表示する計算機</param>
        /// <param name="pictureBox">表示するピクチャボックス</param>
        public Show(Calc procon29_Calc, PictureBox pictureBox)
        {
            Calc = procon29_Calc;
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
                    AgentsActivityData[team, agent] = new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[team, agent].Position);
                }
            }

            ClickField = new ClickField(Calc, PictureBox);
        }

        /// <summary>
        /// Procon29_Showの初期化を行います。
        /// </summary>
        /// <param name="procon29_Calc">表示するProcon29_Calc</param>
        /// <param name="teamDesigns">表示するときのチームデザイン</param>
        public Show(Calc procon29_Calc, TeamDesign[] teamDesigns, PictureBox pictureBox)
        {
            Calc = procon29_Calc;
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
                    AgentsActivityData[team, agent] = new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[team, agent].Position);
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
            PictureBox = pictureBox;
        }

        /// <summary>
        /// 前のカーソルの状態を設定または取得します。
        /// </summary>
        public Coordinate precursor { get; set; }

        /// <summary>
        /// フィールドを描画するオブジェクトを設定または取得します。
        /// </summary>
        public DrawField DrawField { get; set; }

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

            DrawField = new DrawField(Calc, canvas)
            {
                AgentsActivityData = AgentsActivityData
            };
            DrawField.Draw(cursor);
            precursor = cursor;
            try
            {
                foreach (var agent in Calc.Agents)
                {
                    if (ClickField.ClickedAgent == agent && cursor.ChebyshevDistance(agent.Position) == 1)
                    {
                        DrawField.DrawArrow(agent.Team, agent.Position, cursor);
                        break;
                    }
                    else if (cursor.ChebyshevDistance(agent.Position) == 1 && cursor.ChebyshevDistance(ClickField.ClickedAgent.Position) != 1)
                    {
                        DrawField.DrawArrow(agent.Team, agent.Position, cursor);
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
        /// PictureBoxを新たに生成します。
        /// </summary>
        /// <param name="pictureBox">表示するPictureBox</param>
        /// <param name="canvas">表示するBitmap</param>
        /// <param name="graphics">表示するGraphics</param>
        /// <param name="turn">表示するターン</param>
        /// <returns></returns>
        public Bitmap MakePictureBox(PictureBox pictureBox, Bitmap canvas, Graphics graphics, int turn)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Calc.Field.Width;
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Calc.Field.Height;

            Bitmap = canvas;

            var cursor = CursorPosition(PictureBox);

            DrawField = new DrawField(Calc, canvas)
            {
                AgentsActivityData = AgentsActivityData
            };
            DrawField.Draw(turn, cursor);
            precursor = cursor;
            try
            {
                foreach (var agent in Calc.Agents)
                {
                    if (ClickField.ClickedAgent == agent && cursor.ChebyshevDistance(agent.Position) == 1)
                    {
                        DrawField.DrawArrow(agent.Team, agent.Position, cursor);
                        break;
                    }
                    else if (cursor.ChebyshevDistance(agent.Position) == 1 && cursor.ChebyshevDistance(ClickField.ClickedAgent.Position) != 1)
                    {
                        DrawField.DrawArrow(agent.Team, agent.Position, cursor);
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
        public void Showing()
        {
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(((PictureBox.Width <= 0) ? 1 : PictureBox.Width), ((PictureBox.Height <= 0) ? 1 : PictureBox.Height));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(canvas);

            MakePictureBox(PictureBox, canvas, graphics);

            //リソースを開放
            graphics.Dispose();
            //前のImageオブジェクトのリソースを開放してから
            if (PictureBox.Image != null) PictureBox.Image.Dispose();
            //pictureBoxに表示する
            PictureBox.Image = canvas;
        }

        /// <summary>
        /// 表示を行います。
        /// </summary>
        /// <param name="turn">表示するターンを指定します</param>
        public void Showing(int turn)
        {
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(((PictureBox.Width <= 0) ? 1 : PictureBox.Width), ((PictureBox.Height <= 0) ? 1 : PictureBox.Height));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(canvas);

            MakePictureBox(PictureBox, canvas, graphics, turn);

            //リソースを開放
            graphics.Dispose();
            //前のImageオブジェクトのリソースを開放してから
            if (PictureBox.Image != null) PictureBox.Image.Dispose();
            //pictureBoxに表示する
            PictureBox.Image = canvas;
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
            AgentsActivityData = ClickField.AgentsActivityData;
        }

        public void KeyDownShow()
        {
            var next = AgentsActivityData[SelectedTeam, SelecetedAgent];

            if (Calc.Field[next.Destination.X, next.Destination.Y].IsTileOn[((SelectedTeam == Team.A) ? Team.B : Team.A)])
                AgentsActivityData[SelectedTeam, SelecetedAgent].AgentStatusData = AgentStatusCode.RequestRemovementOpponentTile;
            else if (Calc.Field[next.Destination.X, next.Destination.Y].IsTileOn[SelectedTeam])
            {
                //メッセージボックスを表示する
                DialogResult result = MessageBox.Show("タイルを取り除きますか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    Console.WriteLine("「はい」が選択されました");
                    AgentsActivityData[SelectedTeam, SelecetedAgent].AgentStatusData = AgentStatusCode.RequestRemovementOurTile;
                }
                else if (result == DialogResult.No)
                {
                    //「いいえ」が選択された時
                    Console.WriteLine("「いいえ」が選択されました");
                    AgentsActivityData[SelectedTeam, SelecetedAgent].AgentStatusData = AgentStatusCode.RequestMovement;
                }
            }
            else
                AgentsActivityData[SelectedTeam, SelecetedAgent].AgentStatusData = AgentStatusCode.RequestMovement;
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
            ClickField.PushKey(e.KeyCode);
            AgentsActivityData = ClickField.AgentsActivityData;
            Showing();
        }
    }
}
