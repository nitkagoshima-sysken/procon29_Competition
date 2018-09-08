using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// CalcをXMLで管理する際に使用されるクラスです。
    /// </summary>
    [System.Xml.Serialization.XmlInclude(typeof(Agent))]
    [System.Xml.Serialization.XmlInclude(typeof(AgentActivityData))]
    [Serializable]
    public class XmlCalc
    {
        int maxTurn;

        /// <summary>
        /// エージェントたちを表します
        /// </summary>
        public Agents Agents { get => FieldHistory[Turn].Agents; set => FieldHistory[Turn].Agents = value; }

        /// <summary>
        /// フィールドを設定または取得します。
        /// </summary>
        public Field Field { get => FieldHistory[Turn].Field; set => FieldHistory[Turn].Field = value; }

        /// <summary>
        /// ターンを設定または取得します。
        /// </summary>
        public int Turn { get; set; }

        /// <summary>
        /// ターンの終わりを設定または取得します。
        /// </summary>
        public int MaxTurn
        {
            get => maxTurn;
            set => maxTurn = (value <= 0) ? 1 : value;
        }

        /// <summary>
        /// フィールドの歴史を設定または取得します。
        /// </summary>
        public List<TurnData> FieldHistory { get; set; } = new List<TurnData>();

        /// <summary>
        /// エージェントの略称を返します。
        /// </summary>
        public static string[,] ShortTeamAgentName => new string[2, 2] { { "Strawberry", "Apple", }, { "Kiwi", "Muscat", }, };

        /// <summary>
        /// 現在のobjectのディープコピーを行います。
        /// </summary>
        /// <returns>objectのディープコピー</returns>
        public object DeepCopy()
        {
            return new Calc(MaxTurn, new Field(Field), new Coordinate[] { Agents[Team.A, AgentNumber.One].Position, Agents[Team.A, AgentNumber.Two].Position });
        }

        /// <summary>
        /// BaseCalcを初期化をします。
        /// </summary>
        public XmlCalc()
        {

        }

        /// <summary>
        /// Calcを初期化します。
        /// </summary>
        /// <param name="calc"></param>
        public XmlCalc(Calc calc)
        {
            MaxTurn = calc.MaxTurn;
            Turn = calc.Turn;
            FieldHistory = calc.FieldHistory;
        }


        /// <summary>
        /// BaseCalcを初期化します。
        /// </summary>
        /// <param name="maxTurn">最大ターン数を設定します。</param>
        /// <param name="point">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public XmlCalc(int maxTurn, int[,] point, Coordinate[] initPosition)
        {
            MaxTurn = maxTurn;
            // Turn -> 0
            Turn = 0;
            // TurnData作成
            FieldHistory.Add(new TurnData(new Field(point.GetLength(1), point.GetLength(0)), new Agents()));

            InitializationOfField(point);
            foreach (AgentNumber agent in Enum.GetValues(typeof(AgentNumber)))
            {
                Agents[Team.A, agent].Position = initPosition[(int)agent];
                PutTile(team: 0, agent: agent);
            }
            ComplementEnemysPosition();

            // Turn -> 1
            TurnEnd();
        }

        /// <summary>
        /// BaseCalcを初期化します。
        /// </summary>
        /// <param name="maxTurn">最大ターン数を設定します。</param>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public XmlCalc(int maxTurn, Field field, Coordinate[] initPosition)
        {
            MaxTurn = maxTurn;
            // Turn -> 0
            Turn = 0;
            // TurnData作成
            FieldHistory.Add(new TurnData(field, new Agents()));

            foreach (AgentNumber agent in Enum.GetValues(typeof(AgentNumber)))
            {
                Agents[Team.A, agent].Position = initPosition[(int)agent];
                PutTile(team: 0, agent: agent);
            }
            ComplementEnemysPosition();

            // Turn -> 1
            TurnEnd();
        }

        /// <summary>
        /// QRコードには自分のチームの位置情報しか分からないため、
        /// 敵の位置情報を自分の位置から補完します。
        /// </summary>
        private void ComplementEnemysPosition()
        {
            if (Field.IsVerticallySymmetrical)
            {
                Agents[Team.B, AgentNumber.One].Position = Field.FlipVertical(Agents[Team.A, AgentNumber.One].Position);
                Agents[Team.B, AgentNumber.Two].Position = Field.FlipVertical(Agents[Team.A, AgentNumber.Two].Position);
            }
            else if (Field.IsHorizontallySymmetrical)
            {
                Agents[Team.B, AgentNumber.One].Position = Field.FlipHorizontal(Agents[Team.A, AgentNumber.One].Position);
                Agents[Team.B, AgentNumber.Two].Position = Field.FlipHorizontal(Agents[Team.A, AgentNumber.Two].Position);
            }
            PutTile(Team.B, AgentNumber.One);
            PutTile(Team.B, AgentNumber.Two);
        }

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="point"></param>
        private void InitializationOfField(int[,] point)
        {
            Field = new Field(point.GetLength(1), point.GetLength(0));
            for (int x = 0; x < Field.Width; x++)
            {
                for (int y = 0; y < Field.Height; y++)
                {
                    Field[x, y] = new Cell { Point = point[y, x], Coordinate = new Coordinate(x, y) };
                }
            }
        }

        /// <summary>
        /// ターンエンドします。
        /// </summary>
        public void TurnEnd()
        {
            if (Turn < MaxTurn)
            {
                // TurnData作成
                var a = new Agents();
                for (Team team = 0; (int)team < Enum.GetValues(typeof(Team)).Length; team++)
                {
                    for (AgentNumber agent = 0; (int)agent < Enum.GetValues(typeof(AgentNumber)).Length; agent++)
                    {
                        a[team, agent] = new Agent(Agents[team, agent]);
                    }
                }
                FieldHistory.Add(new TurnData(new Field(Field), a));
                Turn++;
            }
        }

        /// <summary>
        /// 自分のフィールドにタイルを置きます。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        public void PutTile(Team team, AgentNumber agent) => Field[Agents[team, agent].Position].IsTileOn[team] = true;
    }
}
