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
        //private string message;
        private Field[,] fields = new Field[12, 12];
        private const string pointFamilyName = "Impact";
        //private Point clickedField;
        //private Font pointFont;
        private Point[,] agentPosition = new Point[2, 2];
        public static readonly string[,] ShortTeamAgentName = new string[2, 2] { { "A1", "A2", }, { "B1", "B2", }, };

        /// <summary>
        /// Procon29_Calcを初期化します。
        /// </summary>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Procon29_Calc(int[,] field, Point[,] initPosition)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    this.Fields[i, j] = new Field
                    {
                        Point = field[i, j]
                    };
                }
            }
            Turn = 1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    AgentPosition[i, j] = initPosition[i, j];
                    MakeArea(team: i, agent: j);
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

        //public string Message { get => message; set => message = value; }
        //public Point ClickedField { get => clickedField; set => clickedField = value; }
        //public Font PointFont { get => pointFont; set => pointFont = value; }
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

        public int AllPoint { get => FieldList.Sum(x => x.Point); }
        
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

        public void MakeArea(Team team, Agent agent) => Fields[AgentPosition[(int)team, (int)agent].X, AgentPosition[(int)team, (int)agent].Y].IsArea[(int)team] = true;
        public void MakeArea(int team, int agent) => MakeArea((Team)team, (Agent)agent);

        public void RemoveArea(Team team, Agent agent) => Fields[AgentPosition[(int)team, (int)agent].X, AgentPosition[(int)team, (int)agent].Y].IsArea[(int)team] = false;

        public void MoveAgent(Team team, Agent agent, Point where)
        {
            AgentPosition[(int)team, (int)agent] = where;
            MakeArea(team: team, agent: agent);
            //Message += "[Info][MoveAgent] AgentPosition[" + team.ToString() + "][" + agent.ToString() + "] = " + AgentPosition[(int)team, (int)agent] + "\n";
            Turn++;
        }
    }
}
