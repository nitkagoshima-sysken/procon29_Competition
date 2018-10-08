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
        public static bool IsRequest(this AgentStatusCode agentStatusData) => agentStatusData.ToAttribute() == AgentStatusCodeAttribute.Request;

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
        /// エージェントステータスコードの属性を可能な限り成功に変更します。
        /// </summary>
        /// <param name="agentStatusCode"></param>
        /// <returns></returns>
        public static AgentStatusCode ToSucceeded(this AgentStatusCode agentStatusCode)
        {
            if (agentStatusCode == AgentStatusCode.RequestNotToDoAnything)
            {
                return AgentStatusCode.SucceededNotToDoAnything;
            }
            switch (agentStatusCode.ToAction())
            {
                case AgentStatusCodeAction.Movement:
                    return AgentStatusCode.SucceededInMoving;
                case AgentStatusCodeAction.RemovementOurTile:
                    return AgentStatusCode.SucceededInRemovingOurTile;
                case AgentStatusCodeAction.RemovementOpponentTile:
                    return AgentStatusCode.SucceededInRemovingOpponentTile;
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// エージェントステータスコードの属性を可能な限りリクエストに変更します。
        /// </summary>
        /// <param name="agentStatusCode"></param>
        /// <returns></returns>
        public static AgentStatusCode ToRequest(this AgentStatusCode agentStatusCode)
        {
            if (agentStatusCode == AgentStatusCode.SucceededNotToDoAnything)
            {
                return AgentStatusCode.RequestNotToDoAnything;
            }
            switch (agentStatusCode.ToAction())
            {
                case AgentStatusCodeAction.Movement:
                    return AgentStatusCode.RequestMovement;
                case AgentStatusCodeAction.RemovementOurTile:
                    return AgentStatusCode.RequestRemovementOurTile;
                case AgentStatusCodeAction.RemovementOpponentTile:
                    return AgentStatusCode.RequestRemovementOpponentTile;
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// エージェントステータスコードのアクションを求めます。
        /// </summary>
        /// <param name="agentStatusCode">対象のエージェントステータスコード</param>
        /// <returns>エージェントステータスコードのアクション</returns>
        public static AgentStatusCodeAction ToAction(this AgentStatusCode agentStatusCode)
        {
            if (Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("InMoving") >= 0 ||
                Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("Movement") >= 0)
            {
                return AgentStatusCodeAction.Movement;
            }
            if (Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("InRemovingOurTile") >= 0 ||
                Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("RemovementOurTile") >= 0)
            {
                return AgentStatusCodeAction.RemovementOurTile;
            }
            if (Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("InRemovingOpponentTile") >= 0 ||
                Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("RemovementOpponentTile") >= 0)
            {
                return AgentStatusCodeAction.RemovementOpponentTile;
            }
            return AgentStatusCodeAction.Other;
        }

        /// <summary>
        /// エージェントステータスコードの属性を求めます。
        /// </summary>
        /// <param name="agentStatusCode">対象のエージェントステータスコード</param>
        /// <returns>エージェントステータスコードの属性</returns>
        public static AgentStatusCodeAttribute ToAttribute(this AgentStatusCode agentStatusCode)
        {
            if (Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("Request") == 0)
            {
                return AgentStatusCodeAttribute.Request;
            }
            if (Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("SucceededIn") == 0)
            {
                return AgentStatusCodeAttribute.Succeeded;
            }
            if (Enum.GetName(typeof(AgentStatusCode), agentStatusCode).IndexOf("FailedIn") == 0)
            {
                return AgentStatusCodeAttribute.Failed;
            }
            return AgentStatusCodeAttribute.Undefined;
        }
    }
}