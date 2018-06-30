using System.Drawing;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントの行動データを表します
    /// </summary>
    public class AgentActivityData
    {
        AgentStatusCode agentStatusData;
        Point destination;

        /// <summary>
        /// 初期化を行います
        /// </summary>
        /// <param name="agentStatusData">エージェントが行動をどこに起こしたかを表します</param>
        public AgentActivityData(AgentStatusCode agentStatusData)
        {
            AgentStatusData = agentStatusData;
            Destination = new Point();
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        /// <param name="agentStatusData">エージェントが行動をどこに起こしたかを表します</param>
        /// <param name="destination">エージェントが行動した結果の状態を表します</param>
        public AgentActivityData(AgentStatusCode agentStatusData, Point destination)
        {
            AgentStatusData = agentStatusData;
            Destination = destination;
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        public AgentActivityData()
        {
            AgentStatusData = AgentStatusCode.NotDoneAnything;
            Destination = new Point();
        }

        /// <summary>
        /// エージェントが行動をどこに起こしたかを設定または取得します
        /// </summary>
        public Point Destination { get => destination; set => destination = value; }

        /// <summary>
        /// エージェントが行動した結果の状態を設定または取得します
        /// </summary>
        internal AgentStatusCode AgentStatusData { get => agentStatusData; set => agentStatusData = value; }

        /// <summary>
        /// 現在のobjectのディープコピーを行います。
        /// </summary>
        /// <returns>objectのディープコピー</returns>
        public object DeepCopy => new AgentActivityData(AgentStatusData, Destination);
    }
}
