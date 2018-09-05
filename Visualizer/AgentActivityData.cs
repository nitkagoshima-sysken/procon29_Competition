using System.Drawing;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントの行動データを表します
    /// </summary>
    public class AgentActivityData
    {
        /// <summary>
        /// エージェントが行動をどこに起こしたかを設定または取得します
        /// </summary>
        public Coordinate Destination { get; set; }

        /// <summary>
        /// エージェントが行動した結果の状態を設定または取得します
        /// </summary>
        public AgentStatusCode AgentStatusData { get; set; }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        public AgentActivityData()
        {
            AgentStatusData = AgentStatusCode.NotDoneAnything;
            Destination = new Coordinate();
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        public AgentActivityData(AgentActivityData agentActivityData)
        {
            AgentStatusData = agentActivityData.AgentStatusData;
            Destination = new Coordinate(agentActivityData.Destination);
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        /// <param name="agentStatusData">エージェントが行動をどこに起こしたかを表します</param>
        public AgentActivityData(AgentStatusCode agentStatusData)
        {
            AgentStatusData = agentStatusData;
            Destination = new Coordinate();
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        /// <param name="agentStatusData">エージェントが行動をどこに起こしたかを表します</param>
        /// <param name="destination">エージェントが行動した結果の状態を表します</param>
        public AgentActivityData(AgentStatusCode agentStatusData, Coordinate destination)
        {
            AgentStatusData = agentStatusData;
            Destination = destination;
        }

        /// <summary>
        /// 現在のobjectのディープコピーを行います。
        /// </summary>
        /// <returns>objectのディープコピー</returns>
        public object DeepCopy => new AgentActivityData(AgentStatusData, Destination);
    }
}
