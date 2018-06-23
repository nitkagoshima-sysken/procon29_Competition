﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Procon29_Visualizer
{
    class Show
    {
        private Calc procon29_Calc;
        private TeamDesign[] teamDesign;
        private PictureBox pictureBox;
        private Logger procon29_Logger;
        private SolidBrush backGroundSolidBrush = new SolidBrush(Color.FromArgb(48, 48, 48));
        private SolidBrush selectSolidBrush = new SolidBrush(Color.FromArgb(50, Color.DarkGray));
        private SolidBrush clickedSolidBrush = new SolidBrush(Color.FromArgb(100, Color.SkyBlue));
        private Font pointFont;
        private Point clickedField;
        private const string pointFamilyName = "Impact";
        private (Team, Agent) selectedTeamAndAgent;

        private Bitmap[] agentBitmap;

        public AgentActivityData[,] agentActivityData = new AgentActivityData[2, 2];

        /// <summary>
        /// 描画する対象となるProcon29_Calcを設定または取得します。
        /// </summary>
        internal Calc Procon29_Calc { get => procon29_Calc; set => procon29_Calc = value; }

        /// <summary>
        /// 描画するときの色を設定または取得します。
        /// </summary>
        internal TeamDesign[] TeamDesign { get => teamDesign; set => teamDesign = value; }

        /// <summary>
        /// 描画する対象となるPictureBoxを設定または取得します。
        /// </summary>
        public PictureBox PictureBox { get => pictureBox; set => pictureBox = value; }

        /// <summary>
        /// 背景をどのように塗りつぶすか設定または取得します。
        /// </summary>
        public SolidBrush BackGroundSolidBrush { get => backGroundSolidBrush; set => backGroundSolidBrush = value; }

        /// <summary>
        /// 選択したフィールドをどのように塗りつぶすか設定または取得します。
        /// </summary>
        public SolidBrush SelectSolidBrush { get => selectSolidBrush; set => selectSolidBrush = value; }

        /// <summary>
        /// クリックしたフィールドをどのように塗りつぶすか設定または取得します。
        /// </summary>
        public SolidBrush ClickedSolidBrush { get => clickedSolidBrush; set => clickedSolidBrush = value; }

        /// <summary>
        /// フォントの設定または取得します。
        /// </summary>
        public Font PointFont { get => pointFont; set => pointFont = value; }

        /// <summary>
        /// フォントの設定または取得します。
        /// </summary>
        public static string PointFamilyName => pointFamilyName;

        /// <summary>
        /// クリックしたときのフィールドの場所の設定または取得します。
        /// </summary>
        public Point ClickedField { get => clickedField; set => clickedField = value; }

        /// <summary>
        /// ログを書き込むためのProcon29_Loggerを設定または取得します。
        /// </summary>
        internal Logger Procon29_Logger { get => procon29_Logger; set => procon29_Logger = value; }

        /// <summary>
        /// 選択したチームとエージェントの設定または取得します。
        /// </summary>
        public (Team, Agent) SelectedTeamAndAgent { get => selectedTeamAndAgent; set => selectedTeamAndAgent = value; }

        /// <summary>
        /// エージェントの画像を設定または取得します。
        /// </summary>
        public Bitmap[] AgentBitmap { get => agentBitmap; set => agentBitmap = value; }

        /// <summary>
        /// Procon29_Showの初期化を行います。
        /// </summary>
        /// <param name="procon29_Calc">表示するProcon29_Calc</param>
        /// <param name="teamDesigns">表示するときのチームデザイン</param>
        public Show(Calc procon29_Calc, TeamDesign[] teamDesigns, PictureBox pictureBox)
        {
            Procon29_Calc = procon29_Calc;
            TeamDesign = teamDesigns;
            PictureBox = pictureBox;

            // ResourceManagerを取得する
            System.Resources.ResourceManager resource = Properties.Resources.ResourceManager;

            //画像ファイルを読み込んで、Imageオブジェクトとして取得する
            AgentBitmap = new Bitmap[2];
            AgentBitmap[0] = (Bitmap)resource.GetObject("A");
            AgentBitmap[1] = (Bitmap)resource.GetObject("B");

            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (Agent agent in Enum.GetValues(typeof(Agent)))
                {
                    agentActivityData[(int)team, (int)agent] = new AgentActivityData(AgentStatusData.RequestMovement, Procon29_Calc.GetAgentPosition(team, agent));
                }
            }
        }

        /// <summary>
        /// Procon29_Showの初期化を行います。
        /// </summary>
        /// <param name="procon29_Calc">表示するProcon29_Calc</param>
        /// <param name="teamDesigns">表示するときのチームデザイン</param>
        /// <param name="pictureBox">表示する場所</param>
        public Show(Calc procon29_Calc, TeamDesign[] teamDesigns, PictureBox pictureBox, Logger procon29_Logger)
        {
            Procon29_Calc = procon29_Calc;
            TeamDesign = teamDesigns;
            PictureBox = pictureBox;
            Procon29_Logger = procon29_Logger;
        }

        /// <summary>
        /// PictureBoxを新たに生成します。
        /// </summary>
        /// <param name="pictureBox">表示するPictureBox</param>
        /// <param name="canvas">表示するBitmap</param>
        /// <param name="graphics">表示するGraphics</param>
        /// <returns></returns>
        public Bitmap MakePictureBox(PictureBox pictureBox, Bitmap canvas, Graphics graphics)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Procon29_Calc.Field.Width();
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Procon29_Calc.Field.Height();

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 2.0f);
            for (int x = 0; x < Procon29_Calc.Field.Width(); x++)
            {
                for (int y = 0; y < Procon29_Calc.Field.Height(); y++)
                {
                    //背景色の表示
                    if (!Procon29_Calc.Field[x, y].IsTileOn[(int)Team.A] && !Procon29_Calc.Field[x, y].IsTileOn[(int)Team.B])
                        graphics.FillRectangle(
                        brush: BackGroundSolidBrush,
                        x: x * fieldWidth,
                        y: y * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                    //囲み領域の表示
                    if (Procon29_Calc.Field[x, y].IsEnclosed[(int)Team.A] && Procon29_Calc.Field[x, y].IsEnclosed[(int)Team.B])
                    {
                        graphics.FillRectangle(
                            brush: new HatchBrush(HatchStyle.LargeConfetti, TeamDesign[1].AgentColor),
                            x: x * fieldWidth,
                            y: y * fieldHeight,
                            width: fieldWidth / 2,
                            height: fieldHeight);
                        graphics.FillRectangle(
                            brush: new HatchBrush(HatchStyle.LargeConfetti, TeamDesign[0].AgentColor),
                            x: x * fieldWidth + fieldWidth / 2,
                            y: y * fieldHeight,
                            width: fieldWidth / 2,
                            height: fieldHeight);
                    }
                    else if (Procon29_Calc.Field[x, y].IsEnclosed[(int)Team.A])
                        graphics.FillRectangle(
                            brush: new HatchBrush(HatchStyle.LargeConfetti, TeamDesign[0].AgentColor),
                            x: x * fieldWidth,
                            y: y * fieldHeight,
                            width: fieldWidth,
                            height: fieldHeight);
                    else if (Procon29_Calc.Field[x, y].IsEnclosed[(int)Team.B])
                        graphics.FillRectangle(
                            brush: new HatchBrush(HatchStyle.LargeConfetti, TeamDesign[1].AgentColor),
                            x: x * fieldWidth,
                            y: y * fieldHeight,
                            width: fieldWidth,
                            height: fieldHeight);
                    // タイルの色表示
                    for (int i = 0; i < 2; i++)
                    {
                        if (Procon29_Calc.Field[x, y].IsTileOn[i])
                            graphics.FillRectangle(
                            brush: new SolidBrush(TeamDesign[i].AgentColor),
                            x: x * fieldWidth,
                            y: y * fieldHeight,
                            width: fieldWidth,
                            height: fieldHeight);
                    }
                    // 枠の表示
                    graphics.DrawRectangle(
                        pen: Pens.Black,
                        x: x * fieldWidth,
                        y: y * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                    // 得点表示
                    graphics.DrawString(
                    s: Procon29_Calc.Field[x, y].Point.ToString(),
                    font: PointFont,
                    brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                    x: (float)(x + 0.1) * fieldWidth,
                    y: (float)(y + 0.2) * fieldHeight);
                }
            }
            pointFont.Dispose();

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);
            for (int team = 0; team < 2; team++)
            {
                for (int agent = 0; agent < 2; agent++)
                {
                    graphics.DrawString(
                        s: Calc.ShortTeamAgentName[team, agent],
                        font: PointFont,
                        brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                        x: (float)(Procon29_Calc.AgentPosition[team, agent].X + 0.7) * fieldWidth,
                        y: Procon29_Calc.AgentPosition[team, agent].Y * fieldHeight);
                }
            }
            pointFont.Dispose();

            System.Drawing.Point systemCursorPosition = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            Point selectedFieldPoint = new Point(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
            if ((selectedFieldPoint.X < Procon29_Calc.Field.Width()) && (selectedFieldPoint.Y < Procon29_Calc.Field.Height()))
                graphics.FillRectangle(
                    brush: SelectSolidBrush,
                    x: selectedFieldPoint.X * fieldWidth,
                    y: selectedFieldPoint.Y * fieldHeight,
                    width: fieldWidth,
                    height: fieldHeight);
            graphics.DrawRectangle(
                pen: new Pen(color: Color.LightSkyBlue, width: 5),
                x: ClickedField.X * fieldWidth,
                y: ClickedField.Y * fieldHeight,
                width: fieldWidth,
                height: fieldHeight);

            //エージェントを女の子にするところ
            for (int x = 0; x < Procon29_Calc.Field.Width(); x++)
            {
                for (int y = 0; y < Procon29_Calc.Field.Height(); y++)
                {
                    Graphics g = Graphics.FromImage(canvas);

                    float f = canvas.Height / 3000.0f;

                    if (Procon29_Calc.AgentPosition[0, 0].X == x && Procon29_Calc.AgentPosition[0, 0].Y == y ||
                        Procon29_Calc.AgentPosition[0, 1].X == x && Procon29_Calc.AgentPosition[0, 1].Y == y)
                    {
                        graphics.DrawImage(
                            image: AgentBitmap[(int)Team.A],
                            x: x * fieldWidth,
                            y: y * fieldHeight - (AgentBitmap[(int)Team.A].Height * f * 0.5f),
                            width: AgentBitmap[(int)Team.A].Width * f,
                            height: AgentBitmap[(int)Team.A].Height * f);
                    }
                    if (Procon29_Calc.AgentPosition[1, 0].X == x && Procon29_Calc.AgentPosition[1, 0].Y == y ||
                        Procon29_Calc.AgentPosition[1, 1].X == x && Procon29_Calc.AgentPosition[1, 1].Y == y)
                    {
                        graphics.DrawImage(
                            image: AgentBitmap[(int)Team.B],
                            x: x * fieldWidth,
                            y: y * fieldHeight - (AgentBitmap[(int)Team.B].Height * f * 0.5f),
                            width: AgentBitmap[(int)Team.B].Width * f,
                            height: AgentBitmap[(int)Team.B].Height * f);
                    }
                }
            }

            //次のターンで行くところのマークの表示
            for (int t = 0; t < agentActivityData.GetLength(0); t++)
            {
                for (int a = 0; a < agentActivityData.GetLength(1); a++)
                {
                    if (agentActivityData[t, a] != null)
                    {
                        graphics.FillEllipse(
                            brush: new SolidBrush(color: Color.RoyalBlue),
                            x: (agentActivityData[t, a].Destination.X + 0.5f) * fieldWidth,
                            y: (agentActivityData[t, a].Destination.Y + 0.5f) * fieldHeight,
                            width: fieldWidth / 4,
                            height: fieldWidth / 4);
                    }
                }
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
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Procon29_Calc.Field.Width();
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Procon29_Calc.Field.Height();

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(((pictureBox.Width <= 0) ? 1 : pictureBox.Width), ((pictureBox.Height <= 0) ? 1 : pictureBox.Height));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(canvas);

            System.Drawing.Point systemCursorPosition = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            Point clickedFieldPoint = new Point(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
            if (Procon29_Calc.IsAgentInNeighborhood(clickedFieldPoint))
            {
                if ((clickedFieldPoint.X < Procon29_Calc.Field.Width()) && (clickedFieldPoint.Y < Procon29_Calc.Field.Height()))
                {
                    ClickedField = new Point(
                        x: clickedFieldPoint.X,
                        y: clickedFieldPoint.Y);
                }
            }

            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (Agent agent in Enum.GetValues(typeof(Agent)))
                {
                    if (ClickedField == Procon29_Calc.AgentPosition[(int)team, (int)agent])
                    {
                        SelectedTeamAndAgent = (team, agent);
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

        /// <summary>
        /// ダブルクリックした際の表示を行います。
        /// </summary>
        public void DoubleClickedShow()
        {
            if (Procon29_Calc.Field[CursorPosition(PictureBox).X, CursorPosition(PictureBox).Y].IsTileOn[(int)((SelectedTeamAndAgent.Item1 == Team.A) ? Team.B : Team.A)])
                agentActivityData[(int)SelectedTeamAndAgent.Item1, (int)SelectedTeamAndAgent.Item2].AgentStatusData = AgentStatusData.RequestRemovementOpponentTile;
            else if (Procon29_Calc.Field[CursorPosition(PictureBox).X, CursorPosition(PictureBox).Y].IsTileOn[(int)SelectedTeamAndAgent.Item1])
            {
                //メッセージボックスを表示する
                DialogResult result = MessageBox.Show("タイルを取り除きますか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                //何が選択されたか調べる
                if (result == DialogResult.Yes)
                {
                    //「はい」が選択された時
                    Console.WriteLine("「はい」が選択されました");
                    agentActivityData[(int)SelectedTeamAndAgent.Item1, (int)SelectedTeamAndAgent.Item2].AgentStatusData = AgentStatusData.RequestRemovementOurTile;
                }
                else if (result == DialogResult.No)
                {
                    //「いいえ」が選択された時
                    Console.WriteLine("「いいえ」が選択されました");
                    agentActivityData[(int)SelectedTeamAndAgent.Item1, (int)SelectedTeamAndAgent.Item2].AgentStatusData = AgentStatusData.RequestMovement;
                }
            }
            else
                agentActivityData[(int)SelectedTeamAndAgent.Item1, (int)SelectedTeamAndAgent.Item2].AgentStatusData = AgentStatusData.RequestMovement;
            agentActivityData[(int)SelectedTeamAndAgent.Item1, (int)SelectedTeamAndAgent.Item2].Destination = ClickedField;
        }

        /// <summary>
        /// カーソルがどのフィールドの上にいるかを計算します。
        /// </summary>
        /// <returns></returns>
        public Point CursorPosition(PictureBox pictureBox)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Procon29_Calc.Field.Width();
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Procon29_Calc.Field.Height();
            System.Drawing.Point systemCursorPosition = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            return new Point(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
        }
    }
}
