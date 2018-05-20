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
        public Field[,] field = new Field[12, 12];
        private int turn;
        private SolidBrush backGroundSolidBrush = new SolidBrush(Color.FromArgb(48, 48, 48));
        private SolidBrush selectSolidBrush = new SolidBrush(Color.FromArgb(50, Color.DarkGray));
        private int dx, dy;

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
            turn = 1;
        }


        /// <summary>
        /// <code>Procon29_Display</code>を初期化します。
        /// <code>turn</code>は<code>1</code>にセットされます。
        /// </summary>
        /// <param name="field">競技フィールドにおける各マスのポイントのデータを格納します。</param>
        public Procon29_Display(Field[,] field)
        {
            this.field = field;
            turn = 1;
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
            turn = 1;
        }

        public bool PointMapCheck()
        {
            if (field.GetLength(0) > 12 || field.GetLength(1) > 12)
                throw new ArgumentException("[ERR2] 'field' was not declare array smaller than 12 * 12");
            else if (!VerticallySymmetricalCheck())
                throw new ArgumentException("[ERR3] 'field' was not declare vertically symmetric array");
            else if (!HorizontallySymmetricalCheck())
                throw new ArgumentException("[ERR4] 'field' was not declare horizontally symmetric array");
            return true;
        }

        private bool VerticallySymmetricalCheck()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1) / 2; j++)
                {
                    if (field[i, j].Point != field[i, (field.GetLength(1) - 1) - j].Point) return false;
                }
            }
            return true;
        }

        private bool HorizontallySymmetricalCheck()
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                for (int i = 0; i < field.GetLength(0) / 2; i++)
                {
                    if (field[i, j].Point != field[(field.GetLength(0) - 1) - i, j].Point) return false;
                }
            }
            return true;
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
            get { return (turn % 2 == 1) ? Team.FilstTeam : Team.LastTeam; }
        }

        /// <summary>
        /// フィールドの背景色を取得します。
        /// </summary>
        public SolidBrush BackGroundSolidBrush { get => backGroundSolidBrush; }
        /// <summary>
        /// フィールドがマウス上に乗っているときの背景色を取得します。
        /// </summary>
        public SolidBrush SelectSolidBrush { get => selectSolidBrush; }
        public int Dx { get => dx; set => dx = value; }
        public int Dy { get => dy; set => dy = value; }

        /// <summary>
        /// 現在のターンをパスします。
        /// </summary>
        public void Pass()
        {
            turn++;
        }

        public void Show(PictureBox pictureBox)
        {
            int ws = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / field.GetLength(0);
            int hs = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / field.GetLength(1);

            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(((pictureBox.Width <= 0) ? 1 : pictureBox.Width), ((pictureBox.Height <= 0) ? 1 : pictureBox.Height));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    g.FillRectangle(BackGroundSolidBrush, i * ws, j * hs, ws, hs);
                }
            }

            //Penオブジェクトの作成(幅1の黒色)
            //(この場合はPenを作成せずに、Pens.Blackを使っても良い)
            Pen p = new Pen(Color.Black, 1);
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    g.DrawRectangle(p, i * ws, j * hs, ws, hs);
                }
            }

            Font fnt = new Font("Impact", ((float)hs <= 0) ? 1 : (float)hs / 4 * 3 / 1.5f);
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    g.DrawString(Map[i, j].Point.ToString(), fnt, Brushes.DarkGray, (float)(i + 0.1) * ws, j * hs);
                }
            }

            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            System.Drawing.Point fieldcursor = pictureBox.PointToClient(sp);

            int fieldcursorx = fieldcursor.X / ((ws <= 0) ? 1 : ws);
            int fieldcursory = fieldcursor.Y / ((hs <= 0) ? 1 : hs);
            g.FillRectangle(SelectSolidBrush, fieldcursorx * ws, fieldcursory * hs, ws, hs);
            Console.WriteLine("mouse : {0} x {1}", fieldcursorx, fieldcursory);

            SolidBrush destinationSolidBrush = new SolidBrush(Color.FromArgb(100, Color.SkyBlue));
            g.FillRectangle(destinationSolidBrush, Dx * ws, Dy * hs, ws, hs);

            //リソースを解放する
            p.Dispose();
            fnt.Dispose();
            g.Dispose();

            //PictureBox1に表示する
            pictureBox.Image = canvas;
        }

    }
}
