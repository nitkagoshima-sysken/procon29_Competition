namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントの行動の状態を表します
    /// </summary>
    public enum AgentStatusCode
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
        SucceededInRemovingOurTile,
        /// <summary>
        /// 相手のチームからタイルを取り除くことに成功しました
        /// </summary>
        SucceededInRemovingOpponentTile,
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
        FailedInMovingByTryingToGoOutOfTheField,
        /// <summary>
        /// 目標物がフィールド外のため、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileByTryingToGoOutOfTheField,
        /// <summary>
        /// 目標物がフィールド外のため、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileByTryingToGoOutOfTheField,
        /// <summary>
        /// エージェントのムーア近傍に目標物がないため、移動に失敗しました
        /// </summary>
        FailedInMovingByBeingNotMooreNeighborhood,
        /// <summary>
        /// エージェントのムーア近傍に目標物がないため、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileByBeingNotMooreNeighborhood,
        /// <summary>
        /// エージェントのムーア近傍に目標物がないため、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileByBeingNotMooreNeighborhood,
        /// <summary>
        /// 移動する先のタイルの上に既に自分がいるのにもかかわらず、そこに移動しようとしたため、自分自身でコリジョンが発生し、移動に失敗しました
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
        /// 移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、移動に失敗しました
        /// </summary>
        FailedInMovingByInvolvedInOtherCollisions,
        /// <summary>
        /// 移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、自分のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOpponentTileByInvolvedInOtherCollisions,
        /// <summary>
        /// 移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、相手のチームからタイルを取り除くことに失敗しました
        /// </summary>
        FailedInRemovingOurTileByInvolvedInOtherCollisions,
        /// <summary>
        /// 相手のタイルが置いてあるマスを取り外さずに、移動をリクエストしたため、移動に失敗しました
        /// </summary>
        FailedInMovingByTryingItWithoutRemovingTheOpponentTile,
        /// <summary>
        /// 不明なエラーによって、移動に失敗しました
        /// </summary>
        FailedInMovingByUnkownError = 992,
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
}
