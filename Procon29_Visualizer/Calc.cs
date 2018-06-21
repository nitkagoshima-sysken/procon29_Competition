using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;

namespace Procon29_Visualizer
{
    /// <summary>
    /// 方向を表します。
    /// </summary>
    enum Arrow10Key
    {
        /// <summary>
        /// 上を表します。
        /// </summary>
        Up = 8,
        /// <summary>
        /// 右上を表します。
        /// </summary>
        UpRight = 9,
        /// <summary>
        /// 右を表します。
        /// </summary>
        Right = 6,
        /// <summary>
        /// 右下を表します
        /// </summary>
        DownRight = 3,
        /// <summary>
        /// 下を表します。
        /// </summary>
        Down = 2,
        /// <summary>
        /// 左下を示します。
        /// </summary>
        DownLeft = 1,
        /// <summary>
        /// 左を示します。
        /// </summary>
        Left = 4,
        /// <summary>
        /// 左上を示します。
        /// </summary>
        UpLeft = 7,
    }

    /// <summary>
    /// Arrow10Keyの拡張メソッドを使うためのクラス
    /// </summary>
    static class Arrow10KeyExtensions
    {
        /// <summary>
        /// Arrow10KeyをPoint型に変換します。
        /// </summary>
        /// <param name="arrow10Key">変換するArrow10Key</param>
        /// <returns></returns>
        public static Point ToPoint(this Arrow10Key arrow10Key)
        {
            switch (arrow10Key)
            {
                case Arrow10Key.Up:
                    return new Point(0, -1);
                case Arrow10Key.UpRight:
                    return new Point(1, -1);
                case Arrow10Key.Right:
                    return new Point(1, 0);
                case Arrow10Key.DownRight:
                    return new Point(1, 1);
                case Arrow10Key.Down:
                    return new Point(0, 1);
                case Arrow10Key.DownLeft:
                    return new Point(-1, 1);
                case Arrow10Key.Left:
                    return new Point(-1, 0);
                case Arrow10Key.UpLeft:
                    return new Point(-1, -1);
                default:
                    return new Point(0, 0);
            }
        }
    }

    /// <summary>
    /// どのエージェントがどの方向に動いたかをデータにするための構造体です。
    /// </summary>
    public struct AgentActiveData
    {
        private bool isMove;
        private bool isDestory;
        private Arrow10Key arrow;
        private string comment;

        /// <summary>
        /// そのエージェントが動いたか設定または取得します。
        /// </summary>
        public bool IsMove { get => isMove; set => isMove = value; }

        /// <summary>
        /// そのエージェントがタイルを破壊したか設定または取得します。
        /// </summary>
        public bool IsDestory { get => isDestory; set => isDestory = value; }

        /// <summary>
        /// コメントを設定または取得します。
        /// </summary>
        public string Comment { get => comment; set => comment = value; }

        /// <summary>
        /// そのエージェントがどの方向に行動を起こしたか設定または取得します。
        /// </summary>
        internal Arrow10Key Arrow { get => arrow; set => arrow = value; }
    }

    /// <summary>
    /// 1ターンに起きた出来事をデータにします。
    /// </summary>
    struct TurnData
    {
        private AgentActiveData[,] agentActiveData;
        private Cell[,] mapHistory;
        private string comment;

        /// <summary>
        /// どのエージェントがどの方向に動いたかを配列で設定または取得します。
        /// </summary>
        public AgentActiveData[,] AgentActiveData { get => agentActiveData; set => agentActiveData = value; }

        /// <summary>
        /// ターン終了時のマップを設定または取得します。
        /// </summary>
        public Cell[,] MapHistory { get => mapHistory; set => mapHistory = value; }

        /// <summary>
        /// コメントを設定または取得します。
        /// </summary>
        public string Comment { get => comment; set => comment = value; }
    }

    /// <summary>
    /// フィールドの拡張メソッドを定義するためのクラスです。
    /// </summary>
    static class FieldExpansion
    {
        /// <summary>
        /// フィールドの幅を取得します。
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int Width(this Cell[,] field) => field.GetLength(0);

        /// <summary>
        /// フィールドの高さを取得します。
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int Height(this Cell[,] field) => field.GetLength(1);

        /// <summary>
        /// フィールド上で上下反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="field">対象となるフィールド</param>
        /// <param name="point">対象となるマス</param>
        /// <returns></returns>
        public static Point FlipVertical(this Cell[,] field, Point point) => new Point(point.X, field.Height() - 1 - point.Y);

        /// <summary>
        /// フィールド上で左右反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="field">対象となるフィールド</param>
        /// <param name="point">対象となるマス</param>
        /// <returns></returns>
        public static Point FlipHorizontal(this Cell[,] field, Point point) => new Point(field.Width() - 1 - point.X, point.Y);

        /// <summary>
        /// フィールド上で上下左右反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="field">対称となるフィールド</param>
        /// <param name="point">対称となるマス</param>
        /// <returns></returns>
        public static Point FlipHorizontalAndVertical(this Cell[,] field, Point point) => new Point(field.Width() - 1 - point.X, field.Height() - 1 - point.Y);

        /// <summary>
        /// 上下対称か判定します。
        /// </summary>
        /// <returns>上下対称なら真、そうでなければ偽が返ってきます。</returns>
        public static bool VerticallySymmetricalCheck(this Cell[,] field)
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

        /// <summary>
        /// 左右対称か判定します。
        /// </summary>
        /// <returns>左右対称なら真、そうでなければ偽が返ってきます。</returns>
        public static bool HorizontallySymmetricalCheck(this Cell[,] field)
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
        /// フィールド上のセルをPoint型で取得します。
        /// </summary>
        /// <param name="field">対称となるフィールド</param>
        /// <param name="point">対称となるマス</param>
        /// <returns></returns>
        public static Cell At(this Cell[,] field, Point point) => field[point.X, point.Y];
    }

    /// <summary>
    /// procon29におけるフィールドの管理、ポイント計算などの全般を行います。
    /// </summary>
    class Calc
    {
        private int turn;
        private Cell[,] field = new Cell[12, 12];
        private Point[,] agentPosition = new Point[2, 2];
        private static readonly string[,] shortTeamAgentName = new string[2, 2] { { "A1", "A2", }, { "B1", "B2", }, };
        private bool isVerticallySymmetrical, isHorizontallySymmetrical;
        private List<TurnData> turnDatas = new List<TurnData>();

        /// <summary>
        /// Procon29_Calcを初期化します。
        /// </summary>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Calc(int[,] field, Point[,] initPosition)
        {
            Field = new Cell[field.GetLength(1), field.GetLength(0)];
            for (int y = 0; y < Field.Height(); y++)
            {
                for (int x = 0; x < Field.Width(); x++)
                {
                    Field[y, x] = new Cell { Point = field[x, y] };
                }
            }
            Turn = 1;
            for (int t = 0; t < 2; t++)
            {
                for (int a = 0; a < 2; a++)
                {
                    AgentPosition[t, a] = initPosition[t, a];
                    PutTile(team: t, agent: a);
                }
            }
        }

        /// <summary>
        /// Procon29_Calcを初期化します。
        /// </summary>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Calc(int[,] field, Point[] initPosition)
        {
            Field = new Cell[field.GetLength(1), field.GetLength(0)];
            for (int y = 0; y < Field.Height(); y++)
            {
                for (int x = 0; x < Field.Width(); x++)
                {
                    Field[y, x] = new Cell { Point = field[x, y] };
                }
            }
            Turn = 1;
            for (int a = 0; a < 2; a++)
            {
                AgentPosition[0, a] = initPosition[a];
                PutTile(team: 0, agent: a);
            }
            PointMapCheck();
            if (IsHorizontallySymmetrical && IsVerticallySymmetrical)
            {
                AgentPosition[1, 0] = Field.FlipHorizontalAndVertical(AgentPosition[0, 0]);
                AgentPosition[1, 1] = Field.FlipHorizontalAndVertical(AgentPosition[0, 1]);
            }
            else if (IsVerticallySymmetrical)
            {
                AgentPosition[1, 0] = Field.FlipVertical(AgentPosition[0, 0]);
                AgentPosition[1, 1] = Field.FlipVertical(AgentPosition[0, 1]);
            }
            else if (IsHorizontallySymmetrical)
            {
                AgentPosition[1, 0] = Field.FlipHorizontal(AgentPosition[0, 0]);
                AgentPosition[1, 1] = Field.FlipHorizontal(AgentPosition[0, 1]);
            }
            PutTile(team: 1, agent: 0);
            PutTile(team: 1, agent: 1);
        }

        /// <summary>
        /// 現在のターンが先攻か後攻かを取得します。
        /// </summary>
        public Team TurnTeam
        {
            get { return (Turn % 2 == 1) ? Team.A : Team.B; }
        }

        /// <summary>
        /// エージェントの位置をを設定または取得します。
        /// </summary>
        public Point[,] AgentPosition { get => agentPosition; set => agentPosition = value; }

        /// <summary>
        /// ターンを設定または取得します。
        /// </summary>
        public int Turn { get { return turn; } private set { turn = value; } }

        /// <summary>
        /// フィールドを設定または取得します。
        /// </summary>
        public Cell[,] Field { get => field; set => field = value; }

        /// <summary>
        /// エージェントの位置を設定または取得します。
        /// </summary>
        /// <param name="team"></param>
        /// <param name="agent"></param>
        /// <returns></returns>
        public Point GetAgentPosition(Team team, Agent agent) => AgentPosition[(int)team, (int)agent];

        /// <summary>
        /// フィールドのリストを返します。
        /// </summary>
        public List<Cell> FieldList
        {
            get
            {
                var list = new List<Cell>();
                foreach (var item in Field)
                {
                    list.Add(item);
                }
                return list;
            }
        }

        /// <summary>
        /// 上下対称なら真、そうでなければ偽が返ってきます。
        /// </summary>
        public bool IsVerticallySymmetrical { get => isVerticallySymmetrical; private set => isVerticallySymmetrical = value; }

        /// <summary>
        /// 左右対称なら真、そうでなければ偽が返ってきます。
        /// </summary>
        public bool IsHorizontallySymmetrical { get => isHorizontallySymmetrical; private set => isHorizontallySymmetrical = value; }

        /// <summary>
        /// エージェントの略称を返します。
        /// </summary>
        public static string[,] ShortTeamAgentName => shortTeamAgentName;

        /// <summary>
        /// フィールドの歴史を設定または取得します。
        /// </summary>
        internal List<TurnData> TurnDatas { get => turnDatas; set => turnDatas = value; }

        /// <summary>
        /// すべてのフィールドのポイントの和を計算します。
        /// </summary>
        /// <returns>すべてのフィールドのポイントの和</returns>
        public int Sum() => FieldList.Sum(x => x.Point);

        /// <summary>
        /// すべてのフィールドのポイントの絶対値の和を計算します。
        /// </summary>
        /// <returns>すべてのフィールドのポイントの絶対値の和</returns>
        public int SumAbs() => FieldList.Sum(x => ((x.Point > 0) ? x.Point : -x.Point));

        /// <summary>
        /// 指定したチームの直接的なエリアのポイントの合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームの直接的なエリアのポイントの合計</returns>
        public int AreaPoint(Team team) => FieldList.Sum(x => ((x.IsTileOn[(int)team] == true) ? x.Point : 0));

        /// <summary>
        /// 指定したチームが囲んだエリアのポイントの絶対値の合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームが囲んだエリアのポイントの絶対値の合計</returns>
        public int ClosedPoint(Team team) => FieldList.Sum(x => ((x.IsEnclosed[(int)team] == true) ? Math.Abs(x.Point) : 0));

        /// <summary>
        /// 指定したチームの合計ポイントを計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームの合計ポイント</returns>
        public int TotalPoint(Team team) => AreaPoint(team) + ClosedPoint(team);

        /// <summary>
        /// マップが対称であるか規定内の大きさか判定します。
        /// </summary>
        public void PointMapCheck()
        {
            if (Field.GetLength(0) > 12 || Field.GetLength(1) > 12) ;
            //message += "[Error] 'field' was not declare array smaller than 12 * 12" + "\n";
            IsHorizontallySymmetrical = Field.HorizontallySymmetricalCheck();
            IsVerticallySymmetrical = Field.VerticallySymmetricalCheck();
        }

        /// <summary>
        /// そのフィールドが塗れるか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">対象となるフィールド</param>
        /// <returns>そのフィールドが塗れるなら真、そうでなければ偽が返ってきます。</returns>
        bool IsFillable(int team, Point point) => 0 <= point.X && point.X < Field.Width() && 0 <= point.Y && point.Y < Field.Height() && !Field[point.X, point.Y].IsTileOn[team];

        /// <summary>
        /// 指定したフィールドを基準にIsIndirectAreaをfalseで塗りつぶします。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">始点にするフィールド</param>
        private void FillFalseInIsEnclosed(Team team, Point point) => FillFalseInIsEnclosed((int)team, point);

        /// <summary>
        /// 指定したフィールドを基準にIsIndirectAreaをfalseで塗りつぶします。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">始点にするフィールド</param>
        private void FillFalseInIsEnclosed(int team, Point point)
        {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point);

            while (stack.Count > 0)
            {
                point = stack.Pop();
                if (IsFillable(team, point)) //make sure we stay within bounds
                {
                    if (Field[point.X, point.Y].IsEnclosed[team] == true)
                    {
                        Field[point.X, point.Y].IsEnclosed[team] = false;
                        stack.Push(new Point(point.X - 1, point.Y));
                        stack.Push(new Point(point.X + 1, point.Y));
                        stack.Push(new Point(point.X, point.Y - 1));
                        stack.Push(new Point(point.X, point.Y + 1));
                    }
                }
            }
            return;
        }

        /// <summary>
        /// あるフィールドが囲まれているか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void CheckIndirectArea(Team team)
        {
            foreach (var item in Field)
            {
                item.IsEnclosed[(int)team] = true;
            }

            for (int x = 0; x < Field.Width(); x++)
            {
                for (int y = 0; y < Field.Height(); y++)
                {
                    if (x == 0 || x == Field.Width() - 1 || y == 0 || y == Field.Height() - 1)
                        FillFalseInIsEnclosed(team, new Point(x, y));
                }
            }
        }

        /// <summary>
        /// そのマスにエージェントがいるか、またはムーア近傍にいるかを判定します
        /// </summary>
        /// <param name="point">対象となるマス</param>
        /// <returns>そのマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns>
        public bool IsAgentInNeighborhood(Point point)
        {
            foreach (var item in AgentPosition)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (point.X == item.X + x && point.Y == item.Y + y)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 現在のターンをパスします。
        /// </summary>
        public void Pass()
        {
            Turn++;
        }

        /// <summary>
        /// 自分のフィールドにタイルを置きます。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        public void PutTile(Team team, Agent agent) => Field[AgentPosition[(int)team, (int)agent].X, AgentPosition[(int)team, (int)agent].Y].IsTileOn[(int)team] = true;

        /// <summary>
        /// 自分のフィールドにタイルを置きます。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        public void PutTile(int team, int agent) => PutTile((Team)team, (Agent)agent);

        /// <summary>
        /// 相手のフィールドに置いてあるタイルを破壊します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">対象となるエージェント</param>
        public void RemoveTile(Team team, Point point) => Field[point.X, point.Y].IsTileOn[(int)team] = false;

        /// <summary>
        /// 指定したところにエージェントが移動します。
        /// </summary>
        /// <param name="team">移動するチーム</param>
        /// <param name="agent">移動するエージェント</param>
        /// <param name="arrow10Key">移動する方向</param>
        public void MoveAgent(Team team, Agent agent, Arrow10Key arrow10Key)
        {
            MoveAgent(
                team,
                agent,
                new Point(
                    AgentPosition[(int)team, (int)agent].X + arrow10Key.ToPoint().X,
                    AgentPosition[(int)team, (int)agent].Y + arrow10Key.ToPoint().Y));
        }

        /// <summary>
        /// 指定したところにエージェントが移動します。
        /// </summary>
        /// <param name="team">移動するチーム</param>
        /// <param name="agent">移動するエージェント</param>
        /// <param name="where">移動する場所</param>
        public void MoveAgent(Team team, Agent agent, Point where)
        {
            bool movable = false;
            foreach (int otherteam in Enum.GetValues(typeof(Team)))
            {
                if (otherteam != (int)team)
                {
                    movable = Field[where.X, where.Y].IsTileOn[otherteam];
                    if (movable)
                    {
                        RemoveTile(team: (Team)otherteam, point: where);
                        CheckIndirectArea((Team)otherteam);
                        break;
                    }
                }
            }
            if (!movable)
            {
                AgentPosition[(int)team, (int)agent] = where;
                PutTile(team: team, agent: agent);
            }

            CheckIndirectArea(team);

            Turn++;
            foreach (var item in Field)
            {
                if (item.IsTileOn[0]) item.IsEnclosed[0] = false;
                if (item.IsTileOn[1]) item.IsEnclosed[1] = false;
            }
        }
    }
}