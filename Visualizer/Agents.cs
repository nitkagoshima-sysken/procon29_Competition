using System;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントたちを表します。
    /// </summary>
    public class Agents
    {
        AgentData[,] Array { get; set; } = new AgentData[Enum.GetValues(typeof(Team)).Length, Enum.GetValues(typeof(Agent)).Length];

        /// <summary>
        /// Agentsを初期化します。
        /// </summary>
        public Agents()
        {
            Array[0, 0] = new AgentData("Strawberry", Team.A, Agent.One);
            Array[0, 1] = new AgentData("Apple", Team.A, Agent.Two);
            Array[1, 0] = new AgentData("Kiwi", Team.B, Agent.One);
            Array[1, 1] = new AgentData("Muscat", Team.B, Agent.Two);
        }

        /// <summary>
        /// エージェントを取得または設定します
        /// </summary>
        /// <param name="team">対象となる所属チーム</param>
        /// <param name="agent">対象となるエージェント番号</param>
        /// <returns></returns>
        public AgentData this[Team team, Agent agent]
        {
            set { Array[(int)team, (int)agent] = value; }
            get { return Array[(int)team, (int)agent]; }
        }
    }
}
