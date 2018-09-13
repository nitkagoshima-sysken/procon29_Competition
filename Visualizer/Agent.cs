using System;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントを表します
    /// </summary>
    [Serializable]
    public class Agent
    {
        /// <summary>
        /// 名前を取得または設定します
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属チームを取得または設定します
        /// </summary>
        public Team Team { get; set; }

        /// <summary>
        /// エージェント番号を取得または設定します
        /// </summary>
        public AgentNumber AgentNumber { get; set; }

        /// <summary>
        /// エージェントのいる番号を取得または設定します
        /// </summary>
        public Coordinate Position { get; set; }

        public Agent()
        {
        }

        /// <summary>
        /// AgentDataの初期化します。
        /// </summary>
        /// <param name="name">名前を設定します</param>
        /// <param name="team">チーム名を設定します</param>
        /// <param name="agentNumber">エージェント番号を設定します</param>
        public Agent(string name, Team team, AgentNumber agentNumber)
        {
            Name = name;
            Team = team;
            AgentNumber = agentNumber;
        }

        /// <summary>
        /// AgentDataの初期化します。
        /// </summary>
        /// <param name="agent">コピーするエージェントを指定します。</param>
        public Agent(Agent agent)
        {
            Name = agent.Name;
            Team = agent.Team;
            AgentNumber = agent.AgentNumber;
            Position = new Coordinate(agent.Position);
        }
    }
}
