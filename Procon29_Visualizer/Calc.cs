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
            for (int x = 0; x < field.Width(); x++)
            {
                for (int y = 0; y < field.Height() / 2; y++)
                {
                    if (field[x, y].Point != field[x, (field.Height() - 1) - y].Point) return false;
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
            for (int x = 0; x < field.Width() / 2; x++)
            {
                for (int y = 0; y < field.Height(); y++)
                {
                    if (field[x, y].Point != field[(field.Height() - 1) - x, y].Point) return false;
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
        public static Cell Take(this Cell[,] field, Point point) => field[point.X, point.Y];
    }

    /// <summary>
    /// procon29におけるフィールドの管理、ポイント計算などの全般を行います。
    /// </summary>
    class Calc
    {
        private int turn = 0;
        private Cell[,] field;
        private Point[,] agentPosition = new Point[2, 2];
        private static readonly string[,] shortTeamAgentName = new string[2, 2] { { "A1", "A2", }, { "B1", "B2", }, };
        private bool isVerticallySymmetrical, isHorizontallySymmetrical;
        private List<TurnData> turnDatas = new List<TurnData>();

        /// <summary>
        /// Procon29_Calcを初期化します。
        /// </summary>
        /// <param name="point">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Calc(int[,] point, Point[] initPosition)
        {
            InitializationOfField(point);
            foreach (Agent agent in Enum.GetValues(typeof(Agent)))
            {
                AgentPosition[0, (int)agent] = initPosition[(int)agent];
                PutTile(team: 0, agent: agent);
            }
            ComplementEnemysPosition();
        }

        /// <summary>
        /// QRコードには自分のチームの位置情報しか分からないため、
        /// 敵の位置情報を自分の位置から補完します。
        /// </summary>
        private void ComplementEnemysPosition()
        {
            PointMapCheck();
            if (IsVerticallySymmetrical)
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
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="point"></param>
        private void InitializationOfField(int[,] point)
        {
            Field = new Cell[point.GetLength(1), point.GetLength(0)];
            for (int x = 0; x < Field.Width(); x++)
            {
                for (int y = 0; y < Field.Height(); y++)
                {
                    Field[x, y] = new Cell { Point = point[y, x] };
                }
            }
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
        public int AreaPoint(int team) => FieldList.Sum(x => ((x.IsTileOn[team] == true) ? x.Point : 0));

        /// <summary>
        /// 指定したチームの直接的なエリアのポイントの合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームの直接的なエリアのポイントの合計</returns>
        public int AreaPoint(Team team) => AreaPoint((int)team);

        /// <summary>
        /// 指定したチームが囲んだエリアのポイントの絶対値の合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームが囲んだエリアのポイントの絶対値の合計</returns>
        public int ClosedPoint(int team) => FieldList.Sum(x => ((x.IsEnclosed[team] == true) ? Math.Abs(x.Point) : 0));

        /// <summary>
        /// 指定したチームが囲んだエリアのポイントの絶対値の合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームが囲んだエリアのポイントの絶対値の合計</returns>
        public int ClosedPoint(Team team) => ClosedPoint((int)team);

        /// <summary>
        /// 指定したチームの合計ポイントを計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームの合計ポイント</returns>
        public int TotalPoint(int team) => AreaPoint(team) + ClosedPoint(team);

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
            if (Field.Width() > 12 || Field.Height() > 12) throw new IndexOutOfRangeException();
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
        /// 指定したフィールドを基準にIsEnclosedをfalseで塗りつぶします。
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
        /// 指定したフィールドを基準にIsEnclosedをfalseで塗りつぶします。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">始点にするフィールド</param>
        private void FillFalseInIsEnclosed(Team team, Point point) => FillFalseInIsEnclosed((int)team, point);

        /// <summary>
        /// 対象となるチームのIsEnclosedをTrueで初期化します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void ResetTrueInIsEnclosed(int team)
        {
            foreach (var cell in Field)
            {
                cell.IsEnclosed[team] = true;
            }
        }

        /// <summary>
        /// 対象となるチームのIsEnclosedをTrueで初期化します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void ResetTrueInIsEnclosed(Team team) => ResetTrueInIsEnclosed((int)team);

        /// <summary>
        /// すべてのチームのIsEnclosedをTrueで初期化します。
        /// </summary>
        private void ResetTrueInIsEnclosed()
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                ResetTrueInIsEnclosed(team);
            }
        }

        /// <summary>
        /// 対象となるチームにおいてフィールドが囲まれているか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void CheckEnclosedArea(int team)
        {
            ResetTrueInIsEnclosed(team);
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
        /// 対象となるチームにおいてフィールドが囲まれているか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void CheckEnclosedArea(Team team) => CheckEnclosedArea((int)team);

        /// <summary>
        /// すべてのチームにおいてフィールドが囲まれているか判定します。
        /// </summary>
        private void CheckEnclosedArea()
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                CheckEnclosedArea(team);
            }
        }

        /// <summary>
        /// 対象となるマスに対象となるエージェントがいるか、またはムーア近傍にいるかを判定します
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        /// <param name="point">対象となるマス</param>
        /// <returns>対象となるマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns>
        public bool IsAgentInNeighborhood(int team, int agent, Point point)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (point.X == AgentPosition[team, agent].X + x && point.Y == AgentPosition[team, agent].Y + y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 対象となるマスに対象となるエージェントがいるか、またはムーア近傍にいるかを判定します
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        /// <param name="point">対象となるマス</param>
        /// <returns>対象となるマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns>
        public bool IsAgentInNeighborhood(Team team, Agent agent, Point point) => IsAgentInNeighborhood((int)team, (int)agent, point);

        /// <summary>
        /// 対象となるマスにエージェントがいるか、またはムーア近傍にいるかを判定します
        /// </summary>
        /// <param name="point">対象となるマス</param>
        /// <returns>そのマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns>
        public bool IsAgentInNeighborhood(Point point)
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (Agent agent in Enum.GetValues(typeof(Agent)))
                {
                    if (IsAgentInNeighborhood(team, agent, point)) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ターンエンドします。
        /// </summary>
        public void TurnEnd() => Turn++;

        /// <summary>
        /// 自分のフィールドにタイルを置きます。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        public void PutTile(int team, int agent) => Field.Take(AgentPosition[team, agent]).IsTileOn[team] = true;

        /// <summary>
        /// 自分のフィールドにタイルを置きます。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        public void PutTile(Team team, Agent agent) => PutTile((int)team, (int)agent);

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
                        CheckEnclosedArea((Team)otherteam);
                        break;
                    }
                }
            }
            if (!movable)
            {
                AgentPosition[(int)team, (int)agent] = where;
                PutTile(team: team, agent: agent);
            }

            CheckEnclosedArea(team);

            foreach (var item in Field)
            {
                if (item.IsTileOn[0]) item.IsEnclosed[0] = false;
                if (item.IsTileOn[1]) item.IsEnclosed[1] = false;
            }
        }

        /// <summary>
        /// 指定したところにエージェントが移動します。
        /// </summary>
        /// <param name="distination">移動する場所</param>
        public void MoveAgent(Point[,] distination, bool[,] isRemove)
        {
            for (int team = 0; team < 2; team++)
            {
                for (int agent = 0; agent < 2; agent++)
                {
                    MoveAgent((Team)team, (Agent)agent, distination[team, agent]);
                }
            }
        }
    }
}