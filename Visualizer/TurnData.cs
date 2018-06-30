

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 1ターンのデータを表します
    /// </summary>
    class TurnData
    {
        /// <summary>
        /// フィールドを設定または取得します
        /// </summary>
        public Cell[,] Field { get; set; }

        /// <summary>
        /// エージェントの位置を設定または取得します
        /// </summary>
        public Coordinate[,] AgentPosition { get; set; }

        /// <summary>
        /// エージェントの行動データを設定または取得します
        /// </summary>
        internal AgentActivityData[,] AgentActivityData { get; set; }

        /// <summary>
        /// 初期化します
        /// </summary>
        /// <param name="field">フィールドを表します</param>
        /// <param name="agentPosition">エージェントの位置を表します</param>
        /// <param name="agentActivityData">エージェントの行動データを表します</param>
        public TurnData(Cell[,] field, Coordinate[,] agentPosition, AgentActivityData[,] agentActivityData)
        {
            Field = field;
            AgentPosition = agentPosition;
            AgentActivityData = agentActivityData;
        }

        /// <summary>
        /// 初期化します
        /// </summary>
        public TurnData(Cell[,] field, Coordinate[,] agentPosition)
        {
            Field = field;
            AgentPosition = agentPosition;
            AgentActivityData = new AgentActivityData[AgentPosition.GetLength(0), AgentPosition.GetLength(1)];
            for (int team = 0; team < AgentPosition.GetLength(0); team++)
            {
                for (int agent = 0; agent < AgentPosition.GetLength(1); agent++)
                {
                    AgentActivityData[team, agent] = new AgentActivityData();
                }
            }
        }

        /// <summary>
        /// 現在のobjectのディープコピーを行います。
        /// </summary>
        /// <returns>objectのディープコピー</returns>
        public object DeepCopy()
        {
            var cloned =
                new TurnData(
                    (Cell[,])Field.Clone(),
                    new Coordinate[AgentPosition.GetLength(0), AgentPosition.GetLength(1)],
                    new AgentActivityData[AgentActivityData.GetLength(0), AgentActivityData.GetLength(1)]);
            for (int team = 0; team < AgentPosition.GetLength(0); team++)
            {
                for (int agent = 0; agent < AgentPosition.GetLength(1); agent++)
                {
                    cloned.AgentPosition[team, agent] = AgentPosition[team, agent];
                    cloned.AgentActivityData[team, agent] = (AgentActivityData)AgentActivityData[team, agent].DeepCopy;
                }
            }
            return cloned;
        }
    }
}
