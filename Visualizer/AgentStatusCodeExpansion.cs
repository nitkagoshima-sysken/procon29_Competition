using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    static class AgentStatusCodeExpansion
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
        /// リクエストが自分のチームとコリジョンが発生し、失敗したとして処理します
        /// </summary>
        /// <param name="agentActivityData">対象となるエージェントの行動データ</param>
        public static void ToFailBySelfCollision(this AgentActivityData agentActivityData)
        {
            switch (agentActivityData.AgentStatusData)
            {
                case AgentStatusCode.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInMovingBySelfCollision;
                    return;
                case AgentStatusCode.RequestRemovementOurTile:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInRemovingOurTileBySelfCollision;
                    return;
                case AgentStatusCode.RequestRemovementOpponentTile:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInRemovingOpponentTileBySelfCollision;
                    return;
                default:
                    return;
            }
        }
    }
}
