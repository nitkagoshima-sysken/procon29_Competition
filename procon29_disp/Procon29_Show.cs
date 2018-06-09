using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace procon29_disp
{
    class TeamDesign
    {
        private string name;
        private Color agentColor, areaColor;

        public string Name { get => name; set => name = value; }
        public Color AgentColor { get => agentColor; set => agentColor = value; }
        public Color AreaColor { get => areaColor; set => areaColor = value; }

        public TeamDesign(string name, Color agentColor, Color areaColor)
        {
            Name = name;
            AgentColor = agentColor;
            AreaColor = areaColor;
        }
    }

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

        public Procon29_Show(Procon29_Calc procon29_Calc, TeamDesign[] teamDesigns)
        {
            Procon29_Calc = procon29_Calc;
            TeamDesign = teamDesign;
        }

        public Procon29_Show(Procon29_Calc procon29_Calc, TeamDesign[] teamDesigns, PictureBox pictureBox)
        {
            Procon29_Calc = procon29_Calc;
            TeamDesign = teamDesign;
            PictureBox = pictureBox;
        }

        public Bitmap MakePictureBox(PictureBox pictureBox, Bitmap canvas, Graphics graphics)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Procon29_Calc.Fields.GetLength(0);
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Procon29_Calc.Fields.GetLength(1);

            for (int y = 0; y < Procon29_Calc.Fields.GetLength(0); y++)
            {
                for (int x = 0; x < Procon29_Calc.Fields.GetLength(1); x++)
                {
                    if (Procon29_Calc.Fields[x, y].IsArea[(int)Team.A])
                        graphics.FillRectangle(
                        brush: new SolidBrush(Color.DarkOrange),
                        x: x * fieldWidth,
                        y: y * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                    else if (Procon29_Calc.Fields[x, y].IsArea[(int)Team.B])
                        graphics.FillRectangle(
                        brush: new SolidBrush(Color.LimeGreen),
                        x: x * fieldWidth,
                        y: y * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                    else
                        graphics.FillRectangle(
                        brush: BackGroundSolidBrush,
                        x: x * fieldWidth,
                        y: y * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                }
            }

            for (int y = 0; y < Procon29_Calc.Fields.GetLength(0); y++)
            {
                for (int x = 0; x < Procon29_Calc.Fields.GetLength(1); x++)
                {
                    graphics.DrawRectangle(
                        pen: Pens.Black,
                        x: x * fieldWidth,
                        y: y * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                }
            }

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 2.0f);
            for (int y = 0; y < Procon29_Calc.Fields.GetLength(0); y++)
            {
                for (int x = 0; x < Procon29_Calc.Fields.GetLength(1); x++)
                {
                    graphics.DrawString(
                    s: Procon29_Calc.Map[x, y].Point.ToString(),
                    font: PointFont,
                    brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                    x: (float)(x + 0.1) * fieldWidth,
                    y: (float)(y + 0.2) * fieldHeight);
                }
            }

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);
            graphics.DrawString(
                s: "F1",
                font: PointFont,
                brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                x: (float)(Procon29_Calc.AgentPosition[(int)Team.A, (int)Agent.One].X + 0.7) * fieldWidth,
                y: Procon29_Calc.AgentPosition[(int)Team.A, (int)Agent.One].Y * fieldHeight);

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);
            graphics.DrawString(
                s: "F2",
                font: PointFont,
                brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                x: (float)(Procon29_Calc.AgentPosition[(int)Team.A, (int)Agent.Two].X + 0.7) * fieldWidth,
                y: Procon29_Calc.AgentPosition[(int)Team.A, (int)Agent.Two].Y * fieldHeight);
            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);

            graphics.DrawString(
                s: "L1",
                font: PointFont,
                brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                x: (float)(Procon29_Calc.AgentPosition[(int)Team.B, (int)Agent.One].X + 0.7) * fieldWidth,
                y: Procon29_Calc.AgentPosition[(int)Team.B, (int)Agent.One].Y * fieldHeight);
            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);

            graphics.DrawString(
                s: "L2",
                font: PointFont,
                brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                x: (float)(Procon29_Calc.AgentPosition[(int)Team.B, (int)Agent.Two].X + 0.7) * fieldWidth,
                y: Procon29_Calc.AgentPosition[(int)Team.B, (int)Agent.Two].Y * fieldHeight);

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

            PointFont.Dispose();
            return canvas;
        }

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

            if (ClickedField == Procon29_Calc.GetAgentPosition(Team.A, Agent.One))
            {
                selectedTeam = Team.A;
                selectedAgent = Agent.One;
            }
            else if (ClickedField == Procon29_Calc.GetAgentPosition(Team.A, Agent.Two))
            {
                selectedTeam = Team.A;
                selectedAgent = Agent.Two;
            }
            else if (ClickedField == Procon29_Calc.GetAgentPosition(Team.B, Agent.One))
            {
                selectedTeam = Team.B;
                selectedAgent = Agent.One;
            }
            else if (ClickedField == Procon29_Calc.GetAgentPosition(Team.B, Agent.Two))
            {
                selectedTeam = Team.B;
                selectedAgent = Agent.Two;
            }

            MakePictureBox(pictureBox, canvas, graphics);

            //リソースを開放
            graphics.Dispose();
            //前のImageオブジェクトのリソースを開放してから
            if (pictureBox.Image != null) pictureBox.Image.Dispose();
            //pictureBoxに表示する
            pictureBox.Image = canvas;
        }

        public void DoubleClickedShow()
        {
            Procon29_Calc.MoveAgent(selectedTeam, selectedAgent, ClickedField);
        }

    }
}
