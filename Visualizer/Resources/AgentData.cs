namespace nitkagoshima_sysken.Procon29.Visualizer.Resources
{
    /// <summary>
    /// エージェントを表します
    /// </summary>
    public class AgentData
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
        public Agent Agent { get; set; }

        /// <summary>
        /// エージェントのいる番号を取得または設定します
        /// </summary>
        public Coordinate Position { get; set; }

        /// <summary>
        /// AgentDataの初期化します。
        /// </summary>
        /// <param name="name">名前を設定します</param>
        /// <param name="team">チーム名を設定します</param>
        /// <param name="agent">エージェント番号を設定します</param>
        public AgentData(string name, Team team, Agent agent)
        {
            Name = name;
            Team = team;
            Agent = agent;
        }
    }
}
