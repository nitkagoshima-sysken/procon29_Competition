using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace procon29_disp
{
    class Procon29_Show
    {
        private Procon29_Calc procon29_Calc;
        private TeamDesign[] teamDesign;
        private PictureBox pictureBox;
        private SolidBrush backGroundSolidBrush = new SolidBrush(Color.FromArgb(48, 48, 48));
        private SolidBrush selectSolidBrush = new SolidBrush(Color.FromArgb(50, Color.DarkGray));
        private SolidBrush clickedSolidBrush = new SolidBrush(Color.FromArgb(100, Color.SkyBlue));
        private Font pointFont;
        private Point clickedField;
        private const string pointFamilyName = "Impact";
        private Team selectedTeam;
        private Agent selectedAgent;

        internal Procon29_Calc Procon29_Calc { get => procon29_Calc; set => procon29_Calc = value; }
        internal TeamDesign[] TeamDesign { get => teamDesign; set => teamDesign = value; }
        public PictureBox PictureBox { get => pictureBox; set => pictureBox = value; }
        public SolidBrush BackGroundSolidBrush { get => backGroundSolidBrush; set => backGroundSolidBrush = value; }
        public SolidBrush SelectSolidBrush { get => selectSolidBrush; set => selectSolidBrush = value; }
        public SolidBrush ClickedSolidBrush { get => clickedSolidBrush; set => clickedSolidBrush = value; }
        public Font PointFont { get => pointFont; set => pointFont = value; }
        public static string PointFamilyName => pointFamilyName;
        public Point ClickedField { get => clickedField; set => clickedField = value; }

        /// <summary>
        /// Procon29_Showの初期化を行います。
        /// </summary>
        /// <param name="procon29_Calc">表示するProcon29_Calc</param>
        /// <param name="teamDesigns">表示するときのチームデザイン</param>
        public Procon29_Show(Procon29_Calc procon29_Calc, TeamDesign[] teamDesigns)
        {
            Procon29_Calc = procon29_Calc;
            TeamDesign = teamDesigns;
        }

        /// <summary>
        /// Procon29_Showの初期化を行います。
        /// </summary>
        /// <param name="procon29_Calc">表示するProcon29_Calc</param>
        /// <param name="teamDesigns">表示するときのチームデザイン</param>
        /// <param name="pictureBox">表示する場所</param>
        public Procon29_Show(Procon29_Calc procon29_Calc, TeamDesign[] teamDesigns, PictureBox pictureBox)
        {
            Procon29_Calc = procon29_Calc;
            TeamDesign = teamDesigns;
            PictureBox = pictureBox;
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
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Procon29_Calc.Fields.GetLength(0);
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Procon29_Calc.Fields.GetLength(1);

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 2.0f);
            for (int y = 0; y < Procon29_Calc.Fields.GetLength(0); y++)
            {
                for (int x = 0; x < Procon29_Calc.Fields.GetLength(1); x++)
                {
                    // タイルの色表示
                    for (int i = 0; i < 2; i++)
                    {
                        if (Procon29_Calc.Fields[x, y].IsArea[i])
                            graphics.FillRectangle(
                            brush: new SolidBrush(TeamDesign[i].AgentColor),
                            x: x * fieldWidth,
                            y: y * fieldHeight,
                            width: fieldWidth,
                            height: fieldHeight);
                    }
                    if (!Procon29_Calc.Fields[x, y].IsArea[(int)Team.A] && !Procon29_Calc.Fields[x, y].IsArea[(int)Team.B])
                        graphics.FillRectangle(
                        brush: BackGroundSolidBrush,
                        x: x * fieldWidth,
                        y: y * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                    // 枠の表示
                    graphics.DrawRectangle(
                        pen: Pens.Black,
                        x: x * fieldWidth,
                        y: y * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                    // 得点表示
                    graphics.DrawString(
                    s: Procon29_Calc.Map[x, y].Point.ToString(),
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
                        s: Procon29_Calc.ShortTeamAgentName[team, agent],
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
            if ((selectedFieldPoint.X < Procon29_Calc.Fields.GetLength(1)) && (selectedFieldPoint.Y < Procon29_Calc.Fields.GetLength(0)))
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

            return canvas;
        }

        /// <summary>
        /// 表示を行います。
        /// </summary>
        /// <param name="pictureBox">表示するPictureBoxを指定します</param>
        public void Show(PictureBox pictureBox)
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
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Procon29_Calc.Fields.GetLength(0);
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Procon29_Calc.Fields.GetLength(1);

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(((pictureBox.Width <= 0) ? 1 : pictureBox.Width), ((pictureBox.Height <= 0) ? 1 : pictureBox.Height));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(canvas);

            System.Drawing.Point systemCursorPosition = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            Point clickedFieldPoint = new Point(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
            if ((clickedFieldPoint.X < Procon29_Calc.Fields.GetLength(1)) && (clickedFieldPoint.Y < Procon29_Calc.Fields.GetLength(0)))
                ClickedField = new Point(
                    x: clickedFieldPoint.X,
                    y: clickedFieldPoint.Y);

            for (int team = 0; team < 2; team++)
            {
                for (int agent = 0; agent < 2; agent++)
                {
                    if (ClickedField == Procon29_Calc.AgentPosition[team, agent])
                    {
                        selectedTeam = (Team)team;
                        selectedAgent = (Agent)agent;
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
            Procon29_Calc.MoveAgent(selectedTeam, selectedAgent, ClickedField);
        }

    }
}
