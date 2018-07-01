using System;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントたちを表します。
    /// </summary>
    public class Agents
    {
        Agent[,] Array { get; set; } = new Agent[Enum.GetValues(typeof(Team)).Length, Enum.GetValues(typeof(AgentNumber)).Length];

        /// <summary>
        /// Agentsを初期化します。
        /// </summary>
        public Agents()
        {
            Array[0, 0] = new Agent("Strawberry", Team.A, AgentNumber.One);
            Array[0, 1] = new Agent("Apple", Team.A, AgentNumber.Two);
            Array[1, 0] = new Agent("Kiwi", Team.B, AgentNumber.One);
            Array[1, 1] = new Agent("Muscat", Team.B, AgentNumber.Two);
        }

        /// <summary>
        /// エージェントを取得または設定します
        /// </summary>
        /// <param name="team">対象となる所属チーム</param>
        /// <param name="agent">対象となるエージェント番号</param>
        /// <returns></returns>
        public Agent this[Team team, AgentNumber agent]
        {
            set { Array[(int)team, (int)agent] = value; }
            get { return Array[(int)team, (int)agent]; }
        }
    }
}
