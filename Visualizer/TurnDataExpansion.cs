using System;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// TurnData関連の拡張メソッドを定義するためのクラスです。
    /// </summary>
    static class TurnDataExpansion
    {

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

        /// <summary>
        /// リクエストが相手のチームとコリジョンが発生し、失敗したとして処理します
        /// </summary>
        /// <param name="agentActivityData">対象となるエージェントの行動データ</param>
        public static void ToFailByCollisionWithEachOther(this AgentActivityData agentActivityData)
        {
            switch (agentActivityData.AgentStatusData)
            {
                case AgentStatusCode.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInMovingByCollisionWithEachOther;
                    return;
                case AgentStatusCode.RequestRemovementOurTile:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInRemovingOurTileByCollisionWithEachOther;
                    return;
                case AgentStatusCode.RequestRemovementOpponentTile:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInRemovingOpponentTileByCollisionWithEachOther;
                    return;
                default:
                    return;
            }
        }

        public static void ToFailByCollisionWithTheLazyOurTeam(this AgentActivityData agentActivityData)
        {
            switch (agentActivityData.AgentStatusData)
            {
                case AgentStatusCode.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInMovingByCollisionWithTheLazyOurTeam;
                    return;
                case AgentStatusCode.RequestRemovementOurTile:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInRemovingOurTileWithTheLazyOurTeam;
                    return;
                default:
                    return;
            }
        }

        public static void ToFailByCollisionWithTheLazyOpponent(this AgentActivityData agentActivityData)
        {
            switch (agentActivityData.AgentStatusData)
            {
                case AgentStatusCode.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInMovingByCollisionWithTheLazyOpponent;
                    return;
                case AgentStatusCode.RequestRemovementOpponentTile:
                    agentActivityData.AgentStatusData = AgentStatusCode.FailedInRemovingOpponentTileWithTheLazyOpponent;
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// リクエストが成功したとして処理します
        /// </summary>
        /// <param name="agentActivityData">対象となるエージェントの行動データ</param>
        public static void ToSuccess(this AgentActivityData agentActivityData)
        {
            switch (agentActivityData.AgentStatusData)
            {
                case AgentStatusCode.NotDoneAnything:
                    agentActivityData.AgentStatusData = AgentStatusCode.SucceededNotToDoAnything;
                    return;
                case AgentStatusCode.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusCode.SucceededInMoving;
                    return;
                case AgentStatusCode.RequestRemovementOurTile:
                    agentActivityData.AgentStatusData = AgentStatusCode.SucceededInRemovingOurTile;
                    return;
                case AgentStatusCode.RequestRemovementOpponentTile:
                    agentActivityData.AgentStatusData = AgentStatusCode.SucceededInRemovingOpponentTile;
                    return;
                default:
                    return;
            }
        }

        public static void CheckCollision(this AgentActivityData[,] agentActivityData)
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (Agent agent in Enum.GetValues(typeof(Agent)))
                {
                    var item = agentActivityData[(int)team, (int)agent];
                    if (item.AgentStatusData.IsRequest())
                    {
                        foreach (Team otherteam in Enum.GetValues(typeof(Team)))
                        {
                            foreach (Agent otheragent in Enum.GetValues(typeof(Agent)))
                            {
                                if (team == otherteam && agent == otheragent) continue;
                                var otheritem = agentActivityData[(int)otherteam, (int)otheragent];
                                if (item.Destination == otheritem.Destination)
                                {
                                    if (team == otherteam)
                                    {
                                        item.ToFailBySelfCollision();
                                        otheritem.ToFailBySelfCollision();
                                    }
                                    else
                                    {
                                        item.ToFailByCollisionWithEachOther();
                                        otheritem.ToFailByCollisionWithEachOther();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
