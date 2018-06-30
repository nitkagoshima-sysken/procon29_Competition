using System;
using System.Drawing;

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
        public Point[,] AgentPosition { get; set; }

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
        public TurnData(Cell[,] field, Point[,] agentPosition, AgentActivityData[,] agentActivityData)
        {
            Field = field;
            AgentPosition = agentPosition;
            AgentActivityData = agentActivityData;
        }

        /// <summary>
        /// 初期化します
        /// </summary>
        public TurnData(Cell[,] field, Point[,] agentPosition)
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
                    new Point[AgentPosition.GetLength(0), AgentPosition.GetLength(1)],
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

    /// <summary>
    /// TurnData関連の拡張メソッドを定義するためのクラスです。
    /// </summary>
    static class TurnDataExpansion
    {
        

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
