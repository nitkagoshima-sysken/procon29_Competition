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
    /// procon29におけるフィールドの管理、ポイント計算などの全般を行います。
    /// </summary>
    class Procon29_Calc
    {
        private int turn;
        private Field[,] fields = new Field[12, 12];
        private const string pointFamilyName = "Impact";
        private Point[,] agentPosition = new Point[2, 2];
        public static readonly string[,] ShortTeamAgentName = new string[2, 2] { { "A1", "A2", }, { "B1", "B2", }, };

        /// <summary>
        /// Procon29_Calcを初期化します。
        /// </summary>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Procon29_Calc(int[,] field, Point[,] initPosition)
        {
            Fields = new Field[field.GetLength(1), field.GetLength(0)];
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    this.Fields[y, x] = new Field { Point = field[x, y] };
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
        public Field[,] Map
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

        public static string PointFamilyName => pointFamilyName;
        public Point[,] AgentPosition { get => agentPosition; set => agentPosition = value; }
        public int Turn { get { return turn; } private set { turn = value; } }
        public Field[,] Fields { get => fields; set => fields = value; }

        public Point GetAgentPosition(Team team, Agent agent) => AgentPosition[(int)team, (int)agent];

        public List<Field> FieldList
        {
            get
            {
                var list = new List<Field>();
                foreach (var item in Fields)
                {
                    list.Add(item);
                }
                return list;
            }
        }

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
        public int SumDirectArea(Team team) => FieldList.Sum(x => ((x.IsDirectArea[(int)team] == true) ? x.Point : 0));

        /// <summary>
        /// 指定したチームが囲んだエリアのポイントの絶対値の合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームが囲んだエリアのポイントの絶対値の合計</returns>
        public int SumIndirectArea(Team team) { throw new NotImplementedException(); }


        public void PointMapCheck()
        {
            if (Fields.GetLength(0) > 12 || Fields.GetLength(1) > 12) ;
            //message += "[Error] 'field' was not declare array smaller than 12 * 12" + "\n";
            if (!HorizontallySymmetricalCheck()) ;
            //message += "[Error] 'field' was not declare horizontally symmetric array" + "\n";
            if (!VerticallySymmetricalCheck()) ;
            //message += "[Error] 'field' was not declare vertically symmetric array" + "\n";
        }

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
        /// 現在のターンをパスします。
        /// </summary>
        public void Pass()
        {
            Turn++;
        }

        public void MakeArea(Team team, Agent agent) => Fields[AgentPosition[(int)team, (int)agent].X, AgentPosition[(int)team, (int)agent].Y].IsDirectArea[(int)team] = true;
        public void MakeArea(int team, int agent) => MakeArea((Team)team, (Agent)agent);

        public void RemoveArea(Team team, Agent agent) => Fields[AgentPosition[(int)team, (int)agent].X, AgentPosition[(int)team, (int)agent].Y].IsDirectArea[(int)team] = false;

        public void MoveAgent(Team team, Agent agent, Point where)
        {
            AgentPosition[(int)team, (int)agent] = where;
            MakeArea(team: team, agent: agent);
            Turn++;
        }
    }
}
