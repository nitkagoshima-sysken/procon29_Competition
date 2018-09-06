

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 1ターンのデータを表します
    /// </summary>
    public class TurnData
    {
        /// <summary>
        /// フィールドを設定または取得します
        /// </summary>
        public Field Field { get; set; }

        /// <summary>
        /// エージェントの位置を設定または取得します
        /// </summary>
        public Agents Agents { get; set; }

        /// <summary>
        /// エージェントの行動データを設定または取得します
        /// </summary>
        public AgentsActivityData AgentActivityDatas { get; set; }
        
        /// <summary>
        /// XML化するために宣言します
        /// </summary>
        public TurnData()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 初期化します
        /// </summary>
        /// <param name="field">フィールドを表します</param>
        /// <param name="agents">エージェントたちを表します</param>
        /// <param name="agentActivityDatas">エージェントの行動データを表します</param>
        public TurnData(Field field, Agents agents, AgentsActivityData agentActivityDatas)
        {
            Field = field;
            Agents = agents;
            AgentActivityDatas = agentActivityDatas;
        }

        /// <summary>
        /// 初期化します
        /// </summary>
        /// <param name="field">フィールドを表します</param>
        /// <param name="agents">エージェントたちを表します</param>
        public TurnData(Field field, Agents agents)
        {
            Field = field;
            Agents = agents;
            AgentActivityDatas = new AgentsActivityData();
        }

        /// <summary>
        /// 現在のobjectのディープコピーを行います。
        /// </summary>
        /// <returns>objectのディープコピー</returns>
        public object DeepCopy()
        {
            var cloned =
                new TurnData(
                    new Field(Field),
                    new Agents(Agents),
                    new AgentsActivityData(AgentActivityDatas));
            return cloned;
        }
    }
}
