using System;
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

    /// <summary>
    /// 1ターンのデータを表します
    /// </summary>
    class TurnData
    {
        AgentActivityData[,] agentActivityData;
        Cell[,] field;
        Point[,] agentPosition;

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
        /// フィールドを設定または取得します
        /// </summary>
        public Cell[,] Field { get => field; set => field = value; }

        /// <summary>
        /// エージェントの位置を設定または取得します
        /// </summary>
        public Point[,] AgentPosition { get => agentPosition; set => agentPosition = value; }

        /// <summary>
        /// エージェントの行動データを設定または取得します
        /// </summary>
        internal AgentActivityData[,] AgentActivityData { get => agentActivityData; set => agentActivityData = value; }

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
