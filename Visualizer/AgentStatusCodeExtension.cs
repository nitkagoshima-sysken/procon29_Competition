using System;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    static class AgentStatusCodeExtension
    {
        /// <summary>
        /// エージェントの行動の状態がリクエストであることを判定します
        /// </summary>
        /// <param name="agentStatusData">対象となるエージェントの行動の状態</param>
        /// <returns>状態がリクエストなら真、そうでなければ偽</returns>
        public static bool IsRequest(this AgentStatusCode agentStatusData) =>
            agentStatusData == AgentStatusCode.RequestNotToDoAnything ||
            agentStatusData == AgentStatusCode.RequestMovement ||
            agentStatusData == AgentStatusCode.RequestRemovementOpponentTile ||
            agentStatusData == AgentStatusCode.RequestRemovementOurTile;

        /// <summary>
        /// エージェントの行動が成功したことを判定します
        /// </summary>
        /// <param name="agentStatusData">対象となるエージェントの行動の状態</param>
        /// <returns>状態が成功なら真、そうでなければ偽</returns>
        public static bool IsSucceeded(this AgentStatusCode agentStatusData) =>
            agentStatusData == AgentStatusCode.SucceededNotToDoAnything ||
            agentStatusData == AgentStatusCode.SucceededInMoving ||
            agentStatusData == AgentStatusCode.SucceededInRemovingOpponentTile ||
            agentStatusData == AgentStatusCode.SucceededInRemovingOurTile;

        /// <summary>
        /// エージェントの行動が失敗したことを判定します
        /// </summary>
        /// <param name="agentStatusData">対象となるエージェントの行動の状態</param>
        /// <returns>状態が失敗なら真、そうでなければ偽</returns>
        public static bool IsFailed(this AgentStatusCode agentStatusData) =>
            !(agentStatusData.IsRequest() || agentStatusData.IsSucceeded() ||
            agentStatusData == AgentStatusCode.NotDoneAnything ||
            agentStatusData == AgentStatusCode.RequestForbidden);

        /// <summary>
        /// エージェントステータスコードの属性をリクエストから成功に変更します。
        /// </summary>
        /// <param name="agentStatusCode"></param>
        /// <returns></returns>
        public static AgentStatusCode ToSucceeded(this AgentStatusCode agentStatusCode)
        {
            switch (agentStatusCode)
            {
                case AgentStatusCode.RequestNotToDoAnything:
                    return AgentStatusCode.SucceededNotToDoAnything;
                case AgentStatusCode.RequestMovement:
                    return AgentStatusCode.SucceededInMoving;
                case AgentStatusCode.RequestRemovementOurTile:
                    return AgentStatusCode.SucceededInRemovingOurTile;
                case AgentStatusCode.RequestRemovementOpponentTile:
                    return AgentStatusCode.SucceededInRemovingOpponentTile;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
