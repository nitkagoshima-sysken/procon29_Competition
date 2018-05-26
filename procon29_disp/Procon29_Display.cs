using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace procon29_disp
{
    /// <summary>
    /// チームが先攻か後攻かを表すための列挙体
    /// </summary>
    enum Team
    {
        /// <summary>
        /// 先攻のチームを表します。
        /// </summary>
        FilstTeam,
        /// <summary>
        /// 後攻のチームを表します。
        /// </summary>
        LastTeam
    }

    /// <summary>
    /// チームのエージェントを表すための列挙体
    /// </summary>
    enum Agent
    {
        /// <summary>
        /// 1人目のエージェントを表します。
        /// </summary>
        FilstAgent,
        /// <summary>
        /// 2人目のエージェントを表します。
        /// </summary>
        SecondAgent
    }

    /// <summary>
    /// procon29におけるフィールドの管理、ポイント計算などの全般を行います。
    /// </summary>
    class Procon29_Display
    {
        private int turn;
        private string message;
        public Field[,] field = new Field[12, 12];
        private const string pointFamilyName = "Impact";
        private SolidBrush backGroundSolidBrush = new SolidBrush(Color.FromArgb(48, 48, 48));
        private SolidBrush selectSolidBrush = new SolidBrush(Color.FromArgb(50, Color.DarkGray));
        private SolidBrush clickedSolidBrush = new SolidBrush(Color.FromArgb(100, Color.SkyBlue));
        private Point clickedField;
        private Font pointFont;
        private Point[,] agentPosition = new Point[2, 2];

        /// <summary>
        /// <code>Procon29_Display</code>を初期化します。
        /// <code>turn</code>は<code>1</code>にセットされます。
        /// </summary>
        public Procon29_Display()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    field[i, j] = new Field();
                }
            }
            Turn = 1;
        }


        /// <summary>
        /// <code>Procon29_Display</code>を初期化します。
        /// <code>turn</code>は<code>1</code>にセットされます。
        /// </summary>
        /// <param name="field">競技フィールドにおける各マスのポイントのデータを格納します。</param>
        public Procon29_Display(Field[,] field)
        {
            this.field = field;
            Turn = 1;
        }

        /// <summary>
        /// <code>Procon29_Display</code>を初期化します。
        /// <code>turn</code>は<code>1</code>にセットされます。
        /// </summary>
        /// <param name="field">競技フィールドにおける各マスのポイントのデータを格納します。</param>
        public Procon29_Display(int[,] field)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    this.field[i, j] = new Field
                    {
                        Point = field[i, j]
                    };
                }
            }
            Turn = 1;
        }

        /// <summary>
        /// <code>Procon29_Display</code>を初期化します。
        /// </summary>
        /// <param name="field"><code>turn</code>は<code>1</code>にセットされます。</param>
        /// <param name="initPositionOfFilstAgentOfFirstTeam">先攻チームの1番目のエージェントの初期位置を設定します。</param>
        /// <param name="initPositionOfSecondAgentOfFirstTeam">先攻チームの2番目のエージェントの初期位置を設定します。</param>
        /// <param name="initPositionOfFilstAgentOfLastTeam">後攻チームの1番目のエージェントの初期位置を設定します。</param>
        /// <param name="initPositionOfSecondAgentOfLastTeam">後攻チームの2番目のエージェントの初期位置を設定します。</param>
        public Procon29_Display(int[,] field,
            Point initPositionOfFilstAgentOfFirstTeam,
            Point initPositionOfSecondAgentOfFirstTeam,
            Point initPositionOfFilstAgentOfLastTeam,
            Point initPositionOfSecondAgentOfLastTeam)
        {
            AgentPosition[(int)Team.FilstTeam, (int)Agent.FilstAgent] = initPositionOfFilstAgentOfFirstTeam;
            AgentPosition[(int)Team.FilstTeam, (int)Agent.SecondAgent] = initPositionOfSecondAgentOfFirstTeam;
            AgentPosition[(int)Team.LastTeam, (int)Agent.FilstAgent] = initPositionOfFilstAgentOfLastTeam;
            AgentPosition[(int)Team.LastTeam, (int)Agent.SecondAgent] = initPositionOfSecondAgentOfLastTeam;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    this.field[i, j] = new Field
                    {
                        Point = field[i, j]
                    };
                }
            }
            Turn = 1;
            Message += "[Info] AgentPosition[(int)Team.FilstTeam, (int)Agent.FilstAgent] = " + initPositionOfFilstAgentOfFirstTeam.ToString() + "\n";
            Message += "[Info] AgentPosition[(int)Team.FilstTeam, (int)Agent.SecondAgent] = " + initPositionOfSecondAgentOfFirstTeam.ToString() + "\n";
            Message += "[Info] AgentPosition[(int)Team.LastTeam, (int)Agent.FilstAgent] = " + initPositionOfFilstAgentOfLastTeam.ToString() + "\n";
            Message += "[Info] AgentPosition[(int)Team.LastTeam, (int)Agent.SecondAgent] = " + initPositionOfSecondAgentOfLastTeam.ToString() + "\n";
            Message += "[Info] Turn = " + Turn.ToString() + "\n";

            MakeArea(team: Team.FilstTeam, agent: Agent.FilstAgent);
            MakeArea(team: Team.FilstTeam, agent: Agent.SecondAgent);
            MakeArea(team: Team.LastTeam, agent: Agent.FilstAgent);
            MakeArea(team: Team.LastTeam, agent: Agent.SecondAgent);
        }

        /// <summary>
        /// フィールドのポイントの状態を設定または、取得します。
        /// </summary>
        public Field[,] Map
        {
            set
            {
                field = value;
                PointMapCheck();
            }
            get { return field; }
        }

        /// <summary>
        /// 現在のターンが先攻か後攻かを取得します。
        /// </summary>
        public Team TurnTeam
        {
            get { return (Turn % 2 == 1) ? Team.FilstTeam : Team.LastTeam; }
        }

        /// <summary>
        /// フィールドの背景色を取得します。
        /// </summary>
        public SolidBrush BackGroundSolidBrush { get => backGroundSolidBrush; }
        /// <summary>
        /// フィールドがマウス上に乗っているときの背景色を取得します。
        /// </summary>
        public SolidBrush SelectSolidBrush { get => selectSolidBrush; }
        public string Message { get => message; set => message = value; }
        public Point ClickedField { get => clickedField; set => clickedField = value; }
        public SolidBrush ClickedSolidBrush { get => clickedSolidBrush; set => clickedSolidBrush = value; }
        public Font PointFont { get => pointFont; set => pointFont = value; }

        public static string PointFamilyName => pointFamilyName;
        public Point[,] AgentPosition { get => agentPosition; set => agentPosition = value; }
        public int Turn { get { return turn; } private set { turn = value; } }

        public void PointMapCheck()
        {
            if (field.GetLength(0) > 12 || field.GetLength(1) > 12)
                message += "[Error] 'field' was not declare array smaller than 12 * 12" + "\n";
            if (!HorizontallySymmetricalCheck())
                message += "[Error] 'field' was not declare horizontally symmetric array" + "\n";
            if (!VerticallySymmetricalCheck())
                message += "[Error] 'field' was not declare vertically symmetric array" + "\n";
        }

        private bool VerticallySymmetricalCheck()
        {
            for (int i = 0; i < field.GetLength(1); i++)
            {
                for (int j = 0; j < field.GetLength(0) / 2; j++)
                {
                    if (field[i, j].Point != field[(field.GetLength(0) - 1) - i, j].Point) return false;
                }
            }
            return true;
        }

        private bool HorizontallySymmetricalCheck()
        {
            for (int j = 0; j < field.GetLength(0); j++)
            {
                for (int i = 0; i < field.GetLength(1) / 2; i++)
                {
                    if (field[i, j].Point != field[i, (field.GetLength(1) - 1) - j].Point) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 現在のターンをパスします。
        /// </summary>
        public void Pass()
        {
            Turn++;
        }

        public void MakeArea(Team team, Agent agent) => field[AgentPosition[(int)team, (int)agent].X, AgentPosition[(int)team, (int)agent].Y].IsArea[(int)team] = true;

        public void RemoveArea(Team team, Agent agent) => field[AgentPosition[(int)team, (int)agent].X, AgentPosition[(int)team, (int)agent].Y].IsArea[(int)team] = false;

        public Bitmap MakePictureBox(PictureBox pictureBox, Bitmap canvas, Graphics graphics)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / field.GetLength(0);
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / field.GetLength(1);

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j].IsArea[(int)Team.FilstTeam])
                        graphics.FillRectangle(
                        brush: new SolidBrush(Color.DarkOrange),
                        x: i * fieldWidth,
                        y: j * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                    else if (field[i, j].IsArea[(int)Team.LastTeam])
                        graphics.FillRectangle(
                        brush: new SolidBrush(Color.LimeGreen),
                        x: i * fieldWidth,
                        y: j * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                    else
                        graphics.FillRectangle(
                        brush: BackGroundSolidBrush,
                        x: i * fieldWidth,
                        y: j * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                }
            }

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    graphics.DrawRectangle(
                        pen: Pens.Black,
                        x: j * fieldWidth,
                        y: i * fieldHeight,
                        width: fieldWidth,
                        height: fieldHeight);
                }
            }

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 2.0f);
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    graphics.DrawString(
                    s: Map[j, i].Point.ToString(),
                    font: PointFont,
                    brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                    x: (float)(i + 0.1) * fieldWidth,
                    y: (float)(j + 0.2) * fieldHeight);
                }
            }

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);
            graphics.DrawString(
                s: "F1",
                font: PointFont,
                brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                x: (float)(AgentPosition[(int)Team.FilstTeam, (int)Agent.FilstAgent].X + 0.7) * fieldWidth,
                y: AgentPosition[(int)Team.FilstTeam, (int)Agent.FilstAgent].Y * fieldHeight);

            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);
            graphics.DrawString(
                s: "F2",
                font: PointFont,
                brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                x: (float)(AgentPosition[(int)Team.FilstTeam, (int)Agent.SecondAgent].X + 0.7) * fieldWidth,
                y: AgentPosition[(int)Team.FilstTeam, (int)Agent.SecondAgent].Y * fieldHeight);
            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);

            graphics.DrawString(
                s: "L1",
                font: PointFont,
                brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                x: (float)(AgentPosition[(int)Team.LastTeam, (int)Agent.FilstAgent].X + 0.7) * fieldWidth,
                y: AgentPosition[(int)Team.LastTeam, (int)Agent.FilstAgent].Y * fieldHeight);
            PointFont = new Font(familyName: PointFamilyName, emSize: fieldHeight <= 0 ? 1 : fieldHeight / 4 * 3 / 4.0f);

            graphics.DrawString(
                s: "L2",
                font: PointFont,
                brush: new SolidBrush(color: Color.FromArgb(0x90, Color.White)),
                x: (float)(AgentPosition[(int)Team.LastTeam, (int)Agent.SecondAgent].X + 0.7) * fieldWidth,
                y: AgentPosition[(int)Team.LastTeam, (int)Agent.SecondAgent].Y * fieldHeight);

            System.Drawing.Point systemCursorPosition = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            Point selectedFieldPoint = new Point(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
            if ((selectedFieldPoint.X < field.GetLength(1)) && (selectedFieldPoint.Y < field.GetLength(0)))
                graphics.FillRectangle(
                    brush: SelectSolidBrush,
                    x: selectedFieldPoint.X * fieldWidth,
                    y: selectedFieldPoint.Y * fieldHeight,
                    width: fieldWidth,
                    height: fieldHeight);

            graphics.FillRectangle(
                brush: ClickedSolidBrush,
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
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / field.GetLength(0);
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / field.GetLength(1);

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(((pictureBox.Width <= 0) ? 1 : pictureBox.Width), ((pictureBox.Height <= 0) ? 1 : pictureBox.Height));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics graphics = Graphics.FromImage(canvas);

            System.Drawing.Point systemCursorPosition = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            Point clickedFieldPoint = new Point(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
            if ((clickedFieldPoint.X < field.GetLength(1)) && (clickedFieldPoint.Y < field.GetLength(0)))
                ClickedField = new Point(
                    x: clickedFieldPoint.X,
                    y: clickedFieldPoint.Y);

            Message += "[Info] ClickedField = " + clickedField.ToString() + "\n";

            MakePictureBox(pictureBox, canvas, graphics);

            //リソースを開放
            graphics.Dispose();
            //前のImageオブジェクトのリソースを開放してから
            if (pictureBox.Image != null) pictureBox.Image.Dispose();
            //pictureBoxに表示する
            pictureBox.Image = canvas;
        }

    }
}
