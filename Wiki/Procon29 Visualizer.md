# test

## 用語集

|プロコン用語|Visualizerで対応する用語|
|:-:|:-:|
|フィールド|`Field`|
|マス|`Cell`|
|タイル|`Tile`|
|チーム|`Team`|
|エージェント|`Agent`|
|トランプ|`Trump`|
|ポイント|`Point`|
|領域ポイント|`AreaPoint`|
|囲みポイント|`SurroundingPoint`|
|合計ポイント|`TotalPoint`|
|取り除く|Remove|
|置く|Put|
|動く|Move|

## フィールドについて

フィールドはマスがたくさん集まってできています。  
もう少し厳密に言えば、フィールドは「**マスの2次元配列**」でできています。

```cs
var Field = new Cell[12,12];
```

## 概要

通信は基本的に「ターンデータ」を用いてやり取りする予定です。

## ターンデータ

```cs
    /// <summary>
    /// 1ターンのデータを表します
    /// </summary>
    class TurnData
    {
        /// <summary>
        /// フィールドを設定または取得します
        /// </summary>
        public Cell[,] Field;
        /// <summary>
        /// エージェントの行動データを設定または取得します
        /// </summary>
        internal AgentActivityData[,] AgentActivityData;
    }
```

## フィールド

フィールドはマスの集まりです。

## マス

```cs
    /// <summary>
    /// 競技フィールドにおける任意の1マスのデータ構造を表します。
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// そのマスにおける、ポイントを表します。
        /// ポイントには、-16以上16以下の整数値の点数が付与されます。
        /// ただし、0以下の点数のポイントは、少数しか存在しません。
        /// </summary>
        private int Point;
        /// <summary>
        /// そのマスにタイルが置かれているかを表します。
        /// </summary>
        public bool[] IsTileOn;
        /// <summary>
        /// そのマスがタイルに囲まれているかを表します。
        /// </summary>
        public bool[] IsEnclosed;
        /// <summary>
        /// そのマスの囲み判定が行われたかを表します。
        /// </summary>
        public bool[] IsAreaCheck;
    }
```

## エージェントアクティビティデータ

```cs
    /// <summary>
    /// エージェントの行動データを表します
    /// </summary>
    class AgentActivityData
    {
       /// <summary>
        /// エージェントが行動をどこに起こしたかを設定または取得します
        /// </summary>
        public Point Destination;
        /// <summary>
        /// エージェントが行動した結果の状態を設定または取得します
        /// </summary>
        internal AgentStatusData AgentStatusData;
    }
```

## エージェントステータスコード

```cs
    /// <summary>
    /// エージェントの行動の状態を表します
    /// </summary>
    enum AgentStatusData
    {
        /// <summary>
        /// 何もしていない
        /// </summary>
        NotDoneAnything,
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
    }
```
