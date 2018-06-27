using System;
using System.Drawing;

namespace Procon29_Visualizer
{
    /// <summary>
    /// エージェントの行動の状態を表します
    /// </summary>
    public enum AgentStatusData
    {
        /// <summary>
        /// 何もしていない
        /// </summary>
        NotDoneAnything,
        /// <summary>
        /// その場でとどまり、何もしないことを要請します
        /// </summary>
        RequestNotToDoAnything,
        /// <summary>
        /// 移動を要請する
        /// </summary>
        RequestMovement,
        /// <summary>
        /// 自分のチームからタイルを取り除くことを要請します
        /// </summary>
        RequestRemovementOurTile,
        /// <summary>
        /// 相手のチームからタイルを取り除くことを要請します
        /// </summary>
        RequestRemovementOpponentTile,
        /// <summary>
        /// 自分にエージェントを行動させる権限ないため、リクエストを禁止されています
        /// </summary>
        RequestForbidden,
        /// <summary>
        /// その場でとどまり、何もしないことに成功しました
        /// </summary>
        SucceededNotToDoAnything,
        /// <summary>
        /// 移動に成功し、タイルを置きました
        /// </summary>
        SucceededInMoving,
        /// <summary>
        /// 自分のチームからタイルを取り除くことに成功しました
        /// </summary>
        SucceededInRemoveingOurTile,
        /// <summary>
        /// 相手のチームからタイルを取り除くことに成功しました
        /// </summary>
        SucceededInRemoveingOpponentTile,
        /// <summary>
        /// 相手のチームとコリジョンが発生し、移動に失敗しました
        /// </summary>
        FailedInMovingByCollisionWithEachOther,
        /// <summary>
        /// 相手のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileByCollisionWithEachOther,
        /// <summary>
        /// 相手のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileByCollisionWithEachOther,
        /// <summary>
        /// 自分のチームとコリジョンが発生し、移動に失敗しました
        /// </summary>
        FailedInMovingBySelfCollision,
        /// <summary>
        /// 自分のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileBySelfCollision,
        /// <summary>
        /// 自分のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileBySelfCollision,
        /// <summary>
        /// 目標物がフィールド外のため、移動に失敗しました
        /// </summary>
        FailedInMovingByTryingToGoOutOfTheFieldWithEachOther,
        /// <summary>
        /// 目標物がフィールド外のため、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileByTryingToGoOutOfTheField,
        /// <summary>
        /// 目標物がフィールド外のため、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileByTryingToGoOutOfTheField,
        /// <summary>
        /// エージェントのムーア近傍に目標部がないため、移動に失敗しました
        /// </summary>
        FailedInMovingByTryingAgentToJump,
        /// <summary>
        /// エージェントのムーア近傍に目標部がないため、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileByTryingAgentToJump,
        /// <summary>
        /// エージェントのムーア近傍に目標部がないため、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileByTryingAgentToJump,
        /// <summary>
        /// 自分自身とコリジョンが発生し、移動に失敗しました
        /// </summary>
        YouHadCollisionsWithYourselfAndYouFailedToMoveBecauseYouAreThereAlready,
        /// <summary>
        /// 取り除くタイルの上に自分がいるため、タイルを取り除くことに失敗しました
        /// </summary>
        YouHadCollisionsWithYourselfAndYouFailedToRemoveTilesFromYourTeamBecauseYouAreThere,
        /// <summary>
        /// 動かない自分のチームとコリジョンが発生し、移動に失敗しました
        /// </summary>
        FailedInMovingByCollisionWithTheLazyOurTeam,
        /// <summary>
        /// 動かない自分のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileWithTheLazyOurTeam,
        /// <summary>
        /// 動かない相手のチームとコリジョンが発生し、移動に失敗しました
        /// </summary>
        FailedInMovingByCollisionWithTheLazyOpponent,
        /// <summary>
        /// 動かない相手のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileWithTheLazyOpponent,
        /// <summary>
        /// 取り除くタイルが存在しないため、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileByDoingTileNotExist,
        /// <summary>
        /// 取り除くタイルが存在しないため、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileByDoingTileNotExist,
        /// <summary>
        /// 不明なエラーによって、移動に失敗しました
        /// </summary>
        FailedInMovingByUnkownError,
        /// <summary>
        /// 不明なエラーによって、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileByUnkownError,
        /// <summary>
        /// 不明なエラーによって、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileByUnkownError,
        /// <summary>
        /// ティーポットでコーヒーを淹れようとしたため、エラーが発生しました
        /// </summary>
        ImATeaPot = 418,
    }

    /// <summary>
    /// エージェントの行動データを表します
    /// </summary>
    public class AgentActivityData
    {
        AgentStatusData agentStatusData;
        Point destination;

        /// <summary>
        /// 初期化を行います
        /// </summary>
        /// <param name="agentStatusData">エージェントが行動をどこに起こしたかを表します</param>
        /// <param name="destination">エージェントが行動した結果の状態を表します</param>
        public AgentActivityData(AgentStatusData agentStatusData, Point destination)
        {
            AgentStatusData = agentStatusData;
            Destination = destination;
        }

        /// <summary>
        /// 初期化を行います
        /// </summary>
        public AgentActivityData()
        {
            AgentStatusData = AgentStatusData.NotDoneAnything;
            Destination = new Point();
        }

        /// <summary>
        /// エージェントが行動をどこに起こしたかを設定または取得します
        /// </summary>
        public Point Destination { get => destination; set => destination = value; }
        /// <summary>
        /// エージェントが行動した結果の状態を設定または取得します
        /// </summary>
        internal AgentStatusData AgentStatusData { get => agentStatusData; set => agentStatusData = value; }

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
        public static bool IsRequest(this AgentStatusData agentStatusData) =>
            agentStatusData == AgentStatusData.RequestNotToDoAnything ||
            agentStatusData == AgentStatusData.RequestMovement ||
            agentStatusData == AgentStatusData.RequestRemovementOpponentTile ||
            agentStatusData == AgentStatusData.RequestRemovementOurTile;

        /// <summary>
        /// エージェントの行動が成功したことを判定します
        /// </summary>
        /// <param name="agentStatusData">対象となるエージェントの行動の状態</param>
        /// <returns>状態が成功なら真、そうでなければ偽</returns>
        public static bool IsSucceeded(this AgentStatusData agentStatusData) =>
            agentStatusData == AgentStatusData.SucceededNotToDoAnything ||
            agentStatusData == AgentStatusData.SucceededInMoving ||
            agentStatusData == AgentStatusData.SucceededInRemoveingOpponentTile ||
            agentStatusData == AgentStatusData.SucceededInRemoveingOurTile;

        /// <summary>
        /// エージェントの行動が失敗したことを判定します
        /// </summary>
        /// <param name="agentStatusData">対象となるエージェントの行動の状態</param>
        /// <returns>状態が失敗なら真、そうでなければ偽</returns>
        public static bool IsFailed(this AgentStatusData agentStatusData) =>
            !(agentStatusData.IsRequest() || agentStatusData.IsSucceeded() ||
            agentStatusData == AgentStatusData.NotDoneAnything ||
            agentStatusData == AgentStatusData.RequestForbidden);

        /// <summary>
        /// リクエストが自分のチームとコリジョンが発生し、失敗したとして処理します
        /// </summary>
        /// <param name="agentActivityData">対象となるエージェントの行動データ</param>
        public static void ToFailBySelfCollision(this AgentActivityData agentActivityData)
        {
            switch (agentActivityData.AgentStatusData)
            {
                case AgentStatusData.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInMovingBySelfCollision;
                    return;
                case AgentStatusData.RequestRemovementOurTile:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInRemovingOurTileBySelfCollision;
                    return;
                case AgentStatusData.RequestRemovementOpponentTile:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInRemovingOpponentTileBySelfCollision;
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
                case AgentStatusData.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInMovingByCollisionWithEachOther;
                    return;
                case AgentStatusData.RequestRemovementOurTile:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInRemovingOurTileByCollisionWithEachOther;
                    return;
                case AgentStatusData.RequestRemovementOpponentTile:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInRemovingOpponentTileByCollisionWithEachOther;
                    return;
                default:
                    return;
            }
        }

        public static void ToFailByCollisionWithTheLazyOurTeam(this AgentActivityData agentActivityData)
        {
            switch (agentActivityData.AgentStatusData)
            {
                case AgentStatusData.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInMovingByCollisionWithTheLazyOurTeam;
                    return;
                case AgentStatusData.RequestRemovementOurTile:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInRemovingOurTileWithTheLazyOurTeam;
                    return;
                default:
                    return;
            }
        }

        public static void ToFailByCollisionWithTheLazyOpponent(this AgentActivityData agentActivityData)
        {
            switch (agentActivityData.AgentStatusData)
            {
                case AgentStatusData.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInMovingByCollisionWithTheLazyOpponent;
                    return;
                case AgentStatusData.RequestRemovementOpponentTile:
                    agentActivityData.AgentStatusData = AgentStatusData.FailedInRemovingOpponentTileWithTheLazyOpponent;
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
                case AgentStatusData.NotDoneAnything:
                    agentActivityData.AgentStatusData = AgentStatusData.SucceededNotToDoAnything;
                    return;
                case AgentStatusData.RequestMovement:
                    agentActivityData.AgentStatusData = AgentStatusData.SucceededInMoving;
                    return;
                case AgentStatusData.RequestRemovementOurTile:
                    agentActivityData.AgentStatusData = AgentStatusData.SucceededInRemoveingOurTile;
                    return;
                case AgentStatusData.RequestRemovementOpponentTile:
                    agentActivityData.AgentStatusData = AgentStatusData.SucceededInRemoveingOpponentTile;
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
