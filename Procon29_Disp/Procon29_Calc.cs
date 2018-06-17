using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;

namespace procon29_disp
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
    /// procon29におけるフィールドの管理、ポイント計算などの全般を行います。
    /// </summary>
    class Procon29_Calc
    {
        private int turn;
        private Cell[,] fields = new Cell[12, 12];
        private const string pointFamilyName = "Impact";
        private Point[,] agentPosition = new Point[2, 2];
        private static readonly string[,] shortTeamAgentName = new string[2, 2] { { "A1", "A2", }, { "B1", "B2", }, };
        private bool isVerticallySymmetrical, isHorizontallySymmetrical;
        private List<TurnData> turnDatas = new List<TurnData>();

        /// <summary>
        /// Procon29_Calcを初期化します。
        /// </summary>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Procon29_Calc(int[,] field, Point[,] initPosition)
        {
            Fields = new Cell[field.GetLength(1), field.GetLength(0)];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    this.Fields[y, x] = new Cell { Point = field[x, y] };
                    //Fields[y, x].IsDirectArea[0] = false;
                    //Fields[y, x].IsDirectArea[1] = false;
                    //Fields[y, x].IsIndirectArea[0] = true;
                    //Fields[y, x].IsIndirectArea[1] = true;
                }
            }
            Turn = 1;
            for (int t = 0; t < 2; t++)
            {
                for (int a = 0; a < 2; a++)
                {
                    AgentPosition[t, a] = initPosition[t, a];
                    MakeArea(team: t, agent: a);
                }
            }            
        }

        /// <summary>
        /// フィールドのポイントの状態を設定または、取得します。
        /// </summary>
        public Cell[,] Map
        {
            set
            {
                Fields = value;
                PointMapCheck();
            }
            get { return Fields; }
        }

        /// <summary>
        /// 現在のターンが先攻か後攻かを取得します。
        /// </summary>
        public Team TurnTeam
        {
            get { return (Turn % 2 == 1) ? Team.A : Team.B; }
        }

        /// <summary>
        /// フォントの名前を設定または取得します。
        /// </summary>
        public static string PointFamilyName => pointFamilyName;

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
        public Cell[,] Fields { get => fields; set => fields = value; }

        /// <summary>
        /// フィールドの幅を取得します。
        /// </summary>
        public int Width { get => Map.GetLength(0); }

        /// <summary>
        /// フィールドの高さを取得します。
        /// </summary>
        public int Height { get => Map.GetLength(1); }

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
                foreach (var item in Fields)
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
        public int DirectPoint(Team team) => FieldList.Sum(x => ((x.IsTileOn[(int)team] == true) ? x.Point : 0));

        /// <summary>
        /// 指定したチームが囲んだエリアのポイントの絶対値の合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームが囲んだエリアのポイントの絶対値の合計</returns>
        public int IndirectPoint(Team team) => FieldList.Sum(x => ((x.IsIndirectArea[(int)team] == true) ? Math.Abs(x.Point) : 0));

        /// <summary>
        /// 指定したチームの合計ポイントを計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームの合計ポイント</returns>
        public int TotalPoint(Team team) => DirectPoint(team) + IndirectPoint(team);

        /// <summary>
        /// マップが対称であるか規定内の大きさか判定します。
        /// </summary>
        public void PointMapCheck()
        {
            if (Fields.GetLength(0) > 12 || Fields.GetLength(1) > 12) ;
            //message += "[Error] 'field' was not declare array smaller than 12 * 12" + "\n";
            IsHorizontallySymmetrical = HorizontallySymmetricalCheck();
            IsVerticallySymmetrical = VerticallySymmetricalCheck();
        }

        /// <summary>
        /// 上下対称か判定します。
        /// </summary>
        /// <returns>上下対称なら真、そうでなければ偽が返ってきます。</returns>
        private bool VerticallySymmetricalCheck()
        {
            for (int i = 0; i < Fields.GetLength(1); i++)
            {
                for (int j = 0; j < Fields.GetLength(0) / 2; j++)
                {
                    if (Fields[i, j].Point != Fields[(Fields.GetLength(0) - 1) - i, j].Point) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 左右対称か判定します。
        /// </summary>
        /// <returns>左右対称なら真、そうでなければ偽が返ってきます。</returns>
        private bool HorizontallySymmetricalCheck()
        {
            for (int j = 0; j < Fields.GetLength(0); j++)
            {
                for (int i = 0; i < Fields.GetLength(1) / 2; i++)
                {
                    if (Fields[i, j].Point != Fields[i, (Fields.GetLength(1) - 1) - j].Point) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// そのフィールドが塗れるか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">対象となるフィールド</param>
        /// <returns>そのフィールドが塗れるなら真、そうでなければ偽が返ってきます。</returns>
        bool IsFillable(int team, Point point) => 0 <= point.X && point.X < Width && 0 <= point.Y && point.Y < Height && !Map[point.X, point.Y].IsTileOn[team];

        /// <summary>
        /// 指定したフィールドを基準にIsIndirectAreaをfalseで塗りつぶします。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">始点にするフィールド</param>
        private void FillFalse(Team team, Point point) => FillFalse((int)team, point);

        /// <summary>
        /// 指定したフィールドを基準にIsIndirectAreaをfalseで塗りつぶします。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">始点にするフィールド</param>
        private void FillFalse(int team, Point point)
        {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point);

            while (stack.Count > 0)
            {
                point = stack.Pop();
                if (IsFillable(team, point)) //make sure we stay within bounds
                {
                    if (Map[point.X, point.Y].IsIndirectArea[team] == true)
                    {
                        Map[point.X, point.Y].IsIndirectArea[team] = false;
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
            foreach (var item in Map)
            {
                item.IsIndirectArea[(int)team] = true;
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                        FillFalse(team, new Point(x, y));
                }
            }
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
        public void MakeArea(Team team, Agent agent) => Fields[AgentPosition[(int)team, (int)agent].X, AgentPosition[(int)team, (int)agent].Y].IsTileOn[(int)team] = true;

        /// <summary>
        /// 自分のフィールドにタイルを置きます。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        public void MakeArea(int team, int agent) => MakeArea((Team)team, (Agent)agent);

        /// <summary>
        /// 相手のフィールドに置いてあるタイルを破壊します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">対象となるエージェント</param>
        public void DestroyArea(Team team, Point point) => Fields[point.X, point.Y].IsTileOn[(int)team] = false;

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
                    movable = Map[where.X, where.Y].IsTileOn[otherteam];
                    if (movable)
                    {
                        DestroyArea(team: (Team)otherteam, point: where);
                        CheckIndirectArea((Team)otherteam);
                        break;
                    }
                }
            }
            if (!movable)
            {
                AgentPosition[(int)team, (int)agent] = where;
                MakeArea(team: team, agent: agent);
            }

            CheckIndirectArea(team);

            Turn++;
            foreach (var item in Map)
            {
                if (item.IsTileOn[0]) item.IsIndirectArea[0] = false;
                if (item.IsTileOn[1]) item.IsIndirectArea[1] = false;
            }
        }
    }
}