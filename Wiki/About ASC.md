# Agent Status Code について

## ASC000 Not Done Anything

意味は「**何もしていない**」です

このコードは初期化された`AgentStatusCode`の値に使われます。  
それ以外のことに使わないでください。

|項目|概要|
|:-:|:-:|
|意味|まだ何もしていない|
|種類|Undefined|
|C#でのコード|`AgentStatusCode.NotDoneAnything`|
|`Destination`|いかなる値も無効になります|

## ASC001 Request Not To Do Anything

意味は「**その場でとどまり、何もしないことを要請します**」です

|項目|概要|
|:-:|:-:|
|意味|その場でとどまり、何もしないことを要請します|
|種類|Request :bow:|
|C#でのコード|`AgentStatusCode.RequestNotToDoAnything`|
|`Destination`|いかなる値も無効になります|

### For example ASC001

```cs
new AgentActivityData(
    AgentStatusCode.RequestMovement,
    new Point(2, 8)
);
```

これで「何もしない」という意味になります。  
`new Point(2, 8)`は無効になります。

## ASC002 Request Movement

意味は「**移動を要請する**」です

|項目|概要|
|:-:|:-:|
|意味|移動を要請する|
|種類|Request :bow:|
|C#でのコード|`AgentStatusCode.RequestMovement`|
|`Destination`|エージェントが行きたい場所を指定してください|

### For example ASC002

```cs
new AgentActivityData(
    AgentStatusCode.RequestMovement,
    new Point(2, 8)
);
```

これで「縦2行目、横8列目のマス」へ「移動を要請する」という意味になります。

## ASC003 Request Removement Our Tile

意味は「**自分のチームからタイルを取り除くことを要請します**」です

|項目|概要|
|:-:|:-:|
|意味|自分のチームからタイルを取り除くことを要請|
|種類|Request :bow:|
|C#でのコード|`AgentStatusCode.RequestRemovementOurTile`|
|`Destination`|タイルを取り除きたい場所を指定してください|

### For example ASC003

```cs
new AgentActivityData(
    AgentStatusCode.RequestRemovementOurTile,
    new Point(
        Calc.AgentPosition[(int)Agent.One].X + 1,
        Calc.AgentPosition[(int)Agent.One].Y)
);
```

これで「1人目のエージェントのいる場所の右のマス」にある「自分のチームからタイルを取り除くことを要請します」という意味になります。

## ASC004 Request Removement Opponent Tile

意味は「**相手のチームからタイルを取り除くことを要請します**」です

|項目|概要|
|:-:|:-:|
|意味|相手のチームからタイルを取り除くことを要請|
|種類|Request :bow:|
|C#でのコード|`AgentStatusCode.RequestRemovementOpponentTile`|
|`Destination`|タイルを取り除きたい場所を指定してください|

### For example ASC004

```cs
new AgentActivityData(
    AgentStatusCode.RequestRemovementOpponentTile,
    new Point(
        Calc.AgentPosition[(int)Agent.Two].X,
        Calc.AgentPosition[(int)Agent.Two].Y - 1)
);
```

これで「2人目のエージェントのいる場所の上のマス」にある「相手のチームからタイルを取り除くことを要請します」という意味になります。

## ASC005 Request Forbidden

意味は「**自分にエージェントを行動させる権限がないため、リクエストを禁止されています**」です

|項目|概要|
|:-:|:-:|
|意味|リクエストの禁止|
|種類|Request :bow:|
|C#でのコード|`AgentStatusCode.RequestForbidden`|
|`Destination`|いかなる値も無効になります|

## ASC006 Succeeded Not To Do Anything

意味は「**その場でとどまり、何もしないことに成功しました**」です

|項目|概要|
|:-:|:-:|
|意味|何もしないことに成功|
|種類|Succeeded :grinning:|
|C#でのコード|`AgentStatusCode.SucceededNotToDoAnything`|
|発生媒体|`AgentStatusCode.RequestNotToDoAnything`|
|発生条件|いかなるときも成功する|

## ASC007 Succeeded In Moving

意味は「**移動に成功し、タイルを置きました**」です

|項目|概要|
|:-:|:-:|
|意味|移動に成功し、タイルを置いた|
|種類|Succeeded :grinning:|
|C#でのコード|`AgentStatusCode.SucceededInMoving`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|`AgentStatusCode.RequestMovement`が実行可能なとき|

## ASC008 Succeeded In Removing Our Tile

意味は「**自分のチームからタイルを取り除くことに成功しました**」です

|項目|概要|
|:-:|:-:|
|意味|自分のチームからタイルを取り除くことに成功|
|種類|Succeeded :grinning:|
|C#でのコード|`AgentStatusCode.SucceededInRemovingOurTile`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|`AgentStatusCode.RequestRemovementOurTile`が実行可能なとき|

## ASC009 Succeeded In Removing Opponent Tile

意味は「**相手のチームからタイルを取り除くことに成功しました**」です

|項目|概要|
|:-:|:-:|
|意味|相手のチームからタイルを取り除くことに成功|
|種類|Succeeded :grinning:|
|C#でのコード|`AgentStatusCode.SucceededInRemovingOpponentTile`|
|発生媒体|`AgentStatusCode.RequestRemovementOpponentTile`|
|発生条件|`AgentStatusCode.RequestRemovementOpponentTile`が実行可能なとき|

## ASC010 Failed In Moving By Collision With Each Other

意味は「**相手のチームとコリジョンが発生し、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|相手のチームとコリジョンが発生し、移動に失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInMovingByCollisionWithEachOther`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|相手のチームとDestinationが重複したとき|

## ASC011 Failed In Removing Our Tile By Collision With Each Other

意味は「**相手のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|相手のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOurTileByCollisionWithEachOther`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|相手のチームとDestinationが重複したとき|

## ASC012 Failed In Removing Opponent Tile By Collision With Each Other

意味は「**相手のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|相手のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOpponentTileByCollisionWithEachOther`|
|発生媒体|`AgentStatusCode.RequestRemovementOpponentTile`|
|発生条件|相手のチームとDestinationが重複したとき|

## ASC013 Failed In Moving By Self Collision

意味は「**自分のチームとコリジョンが発生し、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|自分のチームとコリジョンが発生し、移動に失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInMovingBySelfCollision`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|自分のチームと`Destination`が重複したとき|

## ASC014 Failed In Removing Our Tile By Self Collision

意味は「**自分のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|自分のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOurTileBySelfCollision`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|自分のチームと`Destination`が重複したとき|

## ASC015 Failed In Removing Opponent Tile By Self Collision

意味は「**自分のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|自分のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOpponentTileBySelfCollision`|
|発生媒体|`AgentStatusCode.RequestRemovementOpponentTile`|
|発生条件|自分のチームと`Destination`が重複したとき|

## ASC016 Failed In Moving By Trying To Go Out Of The Field With Each Other

意味は「**目標物がフィールド外のため、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|目標物がフィールド外のため、移動に失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInMovingByTryingToGoOutOfTheFieldWithEachOther`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|`Destination`がフィールド外のとき|

## ASC017 Failed In Removing Our Tile By Trying To Go Out Of The Field

意味は「**目標物がフィールド外のため、自分のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|目標物がフィールド外のため、自分のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOurTileByTryingToGoOutOfTheField`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|`Destination`がフィールド外のとき|

## ASC018 Failed In Removing Opponent Tile By Trying To Go Out Of The Field

意味は「**目標物がフィールド外のため、相手のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|目標物がフィールド外のため、相手のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOpponentTileByTryingToGoOutOfTheField`|
|発生媒体|`AgentStatusCode.RequestRemovementOpponentTile`|
|発生条件|`Destination`がフィールド外のとき|

## ASC019 Failed In Moving By Being Not Moore Neighborhood

意味は「**エージェントのムーア近傍に目標物がないため、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|エージェントのムーア近傍に目標物がないため、移動に失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInMovingByBeingNotMooreNeighborhood`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|`Destination`がエージェントのいるマスに接していないとき|

## ASC020 Failed In Removing Our Tile By Being Not Moore Neighborhood

意味は「**エージェントのムーア近傍に目標物がないため、自分のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|エージェントのムーア近傍に目標物がないため、自分のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOurTileByBeingNotMooreNeighborhood`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|`Destination`がフィールド外のとき|

## ASC021 Failed In Removing Opponent Tile By Being Not Moore Neighborhood

意味は「**エージェントのムーア近傍に目標物がないため、相手のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|エージェントのムーア近傍に目標物がないため、相手のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOpponentTileByBeingNotMooreNeighborhood`|
|発生媒体|`AgentStatusCode.RequestRemovementOpponentTile`|
|発生条件|`Destination`がフィールド外のとき|

## ASC022 You Had Collisions With Yourself And You Failed To Move Because You Are There Already

意味は「**移動する先のタイルの上に既に自分がいるのにもかかわらず、そこに移動しようとしたため、自分自身でコリジョンが発生し、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|自分自身でコリジョンが発生し、移動に失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.YouHadCollisionsWithYourselfAndYouFailedToMoveBecauseYouAreThereAlready`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|`Destination`が自分のエージェントのいるマスのとき|

## ASC023 You Had Collisions With Yourself And You Failed To Remove Tiles From Your Team Because You Are There

意味は「**取り除くタイルの上に自分がいるため、タイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|自分自身でコリジョンが発生し、タイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.YouHadCollisionsWithYourselfAndYouFailedToRemoveTilesFromYourTeamBecauseYouAreThere`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|`Destination`が自分のエージェントのいるマスのとき|

## ASC024 Failed In Moving By Collision With The Lazy Our Team

意味は「**動かない自分のチームとコリジョンが発生し、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|動かない自分のチームとコリジョンが発生し、移動に失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInMovingByCollisionWithTheLazyOurTeam`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|`Destination`が次のターンで動かない同じチームのエージェントのいるマスのとき|

## ASC025 Failed In Removing Our Tile With The Lazy Our Team

意味は「**動かない自分のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|動かない自分のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOurTileWithTheLazyOurTeam`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|`Destination`が次のターンで動かない同じチームのエージェントのいるマスのとき|

## ASC026 Failed In Moving By Collision With The Lazy Opponent

意味は「**動かない相手のチームとコリジョンが発生し、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|動かない相手のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInMovingByCollisionWithTheLazyOpponent`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|`Destination`が次のターンで動かない相手のチームのエージェントのいるマスのとき|

## ASC027 Failed In Removing Opponent Tile With The Lazy Opponent

意味は「**動かない相手のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|動かない相手のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOpponentTileWithTheLazyOpponent`|
|発生媒体|`AgentStatusCode.RequestRemovementOpponentTile`|
|発生条件|`Destination`が次のターンで動かない相手のチームのエージェントのいるマスのとき|

## ASC028 Failed In Removing Our Tile By Doing Tile Not Exist

意味は「**取り除くタイルが存在しないため、自分のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|動かない自分のチームとコリジョンが発生し、自分のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOurTileByDoingTileNotExist`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|`Destination`がタイルのないマスのとき|

## ASC029 Failed In Removing Opponent Tile By Doing Tile Not Exist

意味は「**取り除くタイルが存在しないため、相手のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|動かない相手のチームとコリジョンが発生し、相手のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOpponentTileByDoingTileNotExist`|
|発生媒体|`AgentStatusCode.RequestRemovementOpponentTile`|
|発生条件|`Destination`がタイルのないマスのとき|

## ASC030 Failed In Moving By Involved In Other Collisions

意味は「**移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|コリジョンに巻き込まれて、移動に失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInMovingByInvolvedInOtherCollisions`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|`Destination`がコリジョンで動けなくなったエージェントのいるマスのとき|

## ASC031 Failed In Removing Opponent Tile By Involved In Other Collisions

意味は「**移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、自分のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|コリジョンに巻き込まれて、自分のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOpponentTileByInvolvedInOtherCollisions`|
|発生媒体|`AgentStatusCode.RequestRemovementOurTile`|
|発生条件|`Destination`がコリジョンで動けなくなったエージェントのいるマスのとき|

## ASC032 Failed In Removing Our Tile By Involved In Other Collisions

意味は「**移動先のマスにいるエージェントのリクエストがコリジョンによって失敗し、それに巻き込まれたため、相手のチームからタイルを取り除くことに失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|コリジョンに巻き込まれて、相手のチームからタイルを取り除くことに失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInRemovingOurTileByInvolvedInOtherCollisions`|
|発生媒体|`AgentStatusCode.RequestRemovementOpponentTile`|
|発生条件|`Destination`がコリジョンで動けなくなったエージェントのいるマスのとき|

## ASC033 Failed In Moving By Trying It Without Removing The OpponentTile

意味は「**相手のタイルが置いてあるマスを取り外さずに、移動をリクエストしたため、移動に失敗しました**」です

|項目|概要|
|:-:|:-:|
|意味|相手のタイルが置いてあるマスを取り外さずに、移動をリクエストしたため、移動に失敗|
|種類|Failed :scream:|
|C#でのコード|`AgentStatusCode.FailedInMovingByTryingItWithoutRemovingTheOpponentTile`|
|発生媒体|`AgentStatusCode.RequestMovement`|
|発生条件|`Destination`が相手のタイルが置いてあるマスのとき|

## ASC992 Failed In Moving By Unkown Error

意味は「**不明なエラーによって、移動に失敗しました**」です

現在、Visualizerはいかなるときもこのコードを返すことはありません。  
何か新たなASCを作る必要がある場合、次のバージョンに上がるまでのしのぎとして使用される可能性があります。  

## ASC993 Failed In Removing Our Tile By Unkown Error

意味は「**不明なエラーによって、自分のチームからタイルを取り除くことに失敗しました**」です

現在、Visualizerはいかなるときもこのコードを返すことはありません。  
何か新たなASCを作る必要がある場合、次のバージョンに上がるまでのしのぎとして使用される可能性があります。  

## ASC994 Failed In Removing Opponent Tile By Unkown Error

意味は「**不明なエラーによって、相手のチームからタイルを取り除くことに失敗しました**」です

現在、Visualizerはいかなるときもこのコードを返すことはありません。  
何か新たなASCを作る必要がある場合、次のバージョンに上がるまでのしのぎとして使用される可能性があります。  

## 更新情報

最終更新時のVisualizerバージョンは **8.0**