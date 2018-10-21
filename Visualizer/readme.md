# Procon29 Visualizer 80.0

## Visualizer Common Field Libraryの一部にエージェントたちが同じマスに2人存在してしまうフィールドが存在しているバグを修正

具体的に例を示すと、`HK960`や、`HE897`などが挙げられます。

## Visualizer Common Field Library について

**Visualizer 75.0** からすべてのVisualizerに**Visualizer Common Field Library**が実装されました。  
**Visualizer Common Field Library**には、72000種類の様々なフィールドデータが収録されています。  
すべての**Visualizer Common Field**には5桁ないし6桁のIDが割り振られています。  
このIDが同じであれば、他のVisualizerと同じフィールドを読み込むことができるため、  
バグが発生したときに、再現がしやすくなります。  
また、PQRファイルのように、バグを再現するために送ったりする必要がなくなります。
**Visualizer Common Field**のIDにつくアルファベットには次の共通点があります。

|Alphabet|Meaning|
|:--:|:--:|
|`A`|幅7×高さ12のフィールド|
|`B`|幅8×高さ10のフィールド|
|`C`|幅8×高さ11のフィールド|
|`D`|幅8×高さ12のフィールド|
|`E`|幅9×高さ9のフィールド|
|`F`|幅9×高さ10のフィールド|
|`G`|幅9×高さ11のフィールド|
|`H`|左右対称のフィールド|
|`I`|幅9×高さ12のフィールド|
|`J`|幅10×高さ8のフィールド|
|`K`|幅10×高さ9のフィールド|
|`L`|幅10×高さ10のフィールド|
|`M`|幅10×高さ11のフィールド|
|`N`|幅10×高さ12のフィールド|
|`O`|幅11×高さ8のフィールド|
|`P`|幅11×高さ9のフィールド|
|`Q`|幅11×高さ10のフィールド|
|`R`|幅11×高さ11のフィールド|
|`S`|幅11×高さ12のフィールド|
|`T`|幅12×高さ7のフィールド|
|`U`|幅12×高さ8のフィールド|
|`V`|上下対称のフィールド|
|`W`|幅12×高さ9のフィールド|
|`X`|幅12×高さ10のフィールド|
|`Y`|幅12×高さ11のフィールド|
|`Z`|幅12×高さ12のフィールド|

## 操作方法

 1. クリックで対象のエージェントを選択しよう。
 2. クリックで行きたい場所、またはタイルを取り除く場所を選択しよう。
 3. キミが操作可能なすべてのエージェントに対して上の操作を繰り返す。
 4. 最後にターンエンドを押す。

## ショートカットキー一覧

|Key|What will happen?|
|:--:|:--:|
|`Q`| チームエージェントA1が選択される |
|`W`| チームエージェントA2が選択される |
|`R`| チームエージェントB1が選択される |
|`T`| チームエージェントB2が選択される |
|`NumPad1`| 選択されたエージェントが左下に移動する |
|`NumPad2`| 選択されたエージェントが下に移動する |
|`NumPad3`| 選択されたエージェントが右下に移動する |
|`NumPad4`| 選択されたエージェントが左に移動する |
|`NumPad6`| 選択されたエージェントが右に移動する |
|`NumPad7`| 選択されたエージェントが左上に移動する |
|`NumPad8`| 選択されたエージェントが上に移動する |
|`NumPad9`| 選択されたエージェントが右上に移動する |
|`Ctrl`+`N`| 試合を新しく行う |
|`Ctrl`+`S`| 試合を保存する |
|`Ctrl`+`Y`| ターンをやり直す |
|`Ctrl`+`Z`| ターンを元に戻す |

## プリフェッチング機能の使い方

`Visualizer.exe`のあるディレクトリに`Prefetching`というディレクトリが存在するはずです。  
存在しない場合は一度、`Visualizer.exe`を実行すると、生成されます。  
`Prefetching`の中にあるTSVファイルに特別なコマンドを入力すると、  
次回実行時にVisualizerがそのファイルを読み、コマンドに応じた変化が生じます。

### Bots.tsv

|コマンド|引数|機能|
|:-:|:-:|:-:|
|`A`|path|自分チームに指定したボットが読み込まれる|
|`B`|path|敵チームに指定したボットが読み込まれる|

### Calc.tsv

|コマンド|引数|機能|
|:-:|:-:|:-:|
|`Pqr`|path|指定したPQRファイルが読み込まれる|
|`Turn`|digit|指定したターンが読み込まれる|
|`MaxTurn`|digit|指定した最大ターン数が読み込まれる|

## Version

### Version 1.0

- マウスを使ったエージェントの移動が可能
- マウスを使ったエージェントの選択が可能
- タイルを置いた領域の計算
- 領域ポイントの計算
- 囲み領域の計算
- 囲みポイントの計算
- タイルを置いた領域の表示
- 領域ポイントの表示
- 囲み領域の表示
- 囲みポイントの表示
- 合計ポイントの表示

### Version 2.0

- PQRファイルの読み込みが可能になった
- 上にマウスが乗っかっているフィールドの情報が画面下に地味に表示されるようになった

### Version 3.0

- キーボードを使ったエージェントの移動が可能
- キーボードを使ったエージェントの選択が可能
- エージェントが女の子になった
- マウスを使った操作ではエージェントのムーア近傍しかクリックできなくなった
- ターンエンドボタンができた

#### Version 3.1

- 自分のチームが置いたタイルを取り除くことができるようになった
- 内部構造が変わった

#### Version 3.2

- エンターでターンエンドができるようになった

### Version 4.0

- エージェントに友達ができた
- 誰もが透明になれる
- エージェントの足元に名前が表示された
- マスのポイントが整然と並んだ
- アンドゥ機能が付いた
- リドゥ機能が付いた
- ボットがプラグインで追加できるようになった
- ボットが選択できるようになった

#### Version 4.1

- エージェントのマスにいてもエージェントが透明になるようにした

#### Version 4.2

- 描画の順番を変更した
- マスカットの妖精が綺麗になった
- ボットに対応するための準備をした
- その他、細かな修正

### Version 5.0

- ボット対応によるVisualizer側の仕様変更
- ボットの仕様変更
- MessageBoxが常にカーソルが下に移動する

### Version 6.0

- ボット対応によるVisualizer側の仕様変更
- ボットの仕様変更
- オレンジチームもボットが選択できるようになった
- ボットで相手のタイルを取り除いてないのにエージェントが移動できるバグを修正

### Version 7.0

- .NET Framework 4.6.1にダウングレードした
- BotプロジェクトとVisualizerプロジェクトに分かれた

#### Version 7.1

- bug fix

### Version 8.0

- ボット製作者に優しいVisualizerになった
- 最大ターン数が設定できるようになった
- ショートカットキーの機能が追加された

#### Version 8.1

- 新たなる戦いで「PQRファイル」を読み込めるようになった
- `Agents[Team]`で`List<Agent>`が返されるようになった（開発者向け）

#### Version 8.2

- 新たなる戦いで「PQRファイル」を読み込まないと例外が投げられるバグを修正

### Version 9.0

- 試合が終了するとログとして出力される`log.xml`の内容が変更された。
- `AgentActivityData.AgentStatusData`のアクセシビリティを`internal`から`public`に変更した。（開発者向け）
- `TurnData.AgentActivityDatas` のアクセシビリティを`internal`から`public`に変更した。（開発者向け）
- `AgentStatusCode.FailedInMovingByTryingToGoOutOfTheFieldWithEachOther`を`AgentStatusCode.FailedInMovingByTryingToGoOutOfTheField`に変更した。（開発者向け）

#### Version 9.1

- XMLシリアル化のバグを修正

#### Version 9.2

- `Calc.cs`の`CheckAgentActivityData`関数の584行目の`AgentStatusCode.FailedInMovingByTryingItWithoutRemovingTheOpponentTile`の判定がおかしかったので修正（開発者向け）
- `TeamExpansion.cs`の`Opponent`関数の返り値が間違っていたので修正（開発者向け）

#### Version 9.3

- `TurnDataExpansion.cs`の`CheckCollision`関数の引数が`AgentActivityData[,]`から`AgentActivityDatas`に変更された（開発者向け）

### Version 10.0

- Visualizerのウインドウを小さくしすぎると例外を吐いてしまうバグがいつのまにか修正された
- 名前が`AgentActivityDatas`から`AgentsActivityData`に変更された（開発者向け）
- `AgentsActivityData`を列挙するとき、`var`で`AgentActivityData`を型推論してくれるように修正した（開発者向け）
- `Agents`を列挙するとき、`var`で`Agent`を型推論してくれるように修正した（開発者向け）

### Version 11.0

- ボットのプリフェッチング機能が追加された

### Version 12.0

- PQRファイルのプリフェッチング機能が追加された
- Calc.DeepCopyの中身を変更した（開発者向け）

#### Version 12.1

- Visualizerのログ画面にディスティネーションが常に`{0,0}`になるバグを修正

### Version 13.0

- FieldListが廃止された（開発者向け）
- DeepCloneが実装された（開発者向け）
- Calc.DeepCopyが修正された（開発者向け）

### Version 14.0

- `Calc.AgentArray`が廃止された（開発者向け）
- `Calc.TeamArray`が廃止された（開発者向け）

#### Version 14.1

- readme.mdの微調整

### Version 15.0

- BaseCalcからXmlCalcに変更された（開発者向け）
- `XmlCalc.Sum()`が廃止された（開発者向け）
- `XmlCalc.SumAbs()`が廃止された（開発者向け）
- `XmlCalc.AreaPoint(Team)`が廃止された（開発者向け）
- `XmlCalc.EnclosedPoint(Team)`が廃止された（開発者向け）
- `XmlCalc.TotalPoint(Team)`が廃止された（開発者向け）
- `XmlCalc.PointMapCheck()`が廃止された（開発者向け）
- `XmlCalc.IsFillable(Team, Coordinate)`が廃止された（開発者向け）
- `XmlCalc.FillFalseInIsEnclosed(Team, Coordinate)`が廃止された（開発者向け）
- `XmlCalc.ResetTrueInIsEnclosed(Team)`が廃止された（開発者向け）
- `XmlCalc.ResetTrueInIsEnclosed()`が廃止された（開発者向け）
- `XmlCalc.CheckEnclosedArea(Team)`が廃止された（開発者向け）
- `XmlCalc.CheckEnclosedArea()`が廃止された（開発者向け）
- `XmlCalc.IsAgentHereOrInMooreNeighborhood(Team, AgentNumber, Coordinate)`が廃止された（開発者向け）
- `XmlCalc.IsAgentHereOrInMooreNeighborhood(Coordinate)`が廃止された（開発者向け）
- `XmlCalc.IsOneAgentHereOrInMooreNeighborhood(Coordinate)`が廃止された（開発者向け）
- `XmlCalc.IsAgentHere(Coordinate)`が廃止された（開発者向け）
- `XmlCalc.IsAgentInMooreNeighborhood(Coordinate)`が廃止された（開発者向け）
- `XmlCalc.IsOneAgentInMooreNeighborhood(Coordinate)`が廃止された（開発者向け）
- `XmlCalc.Undo()`が廃止された（開発者向け）
- `XmlCalc.Redo()`が廃止された（開発者向け）
- `XmlCalc.RemoveTile()`が廃止された（開発者向け）
- `XmlCalc.MoveAgent(Team, AgentNumber, Coordinate)`が廃止された（開発者向け）
- `XmlCalc.CheckAgentActivityData(AgentsActivityData)`が廃止された（開発者向け）
- `XmlCalc.MoveAgent(AgentsActivityData)`が廃止された（開発者向け）
- `XmlCalc.MoveAgent(Team, AgentActivityData[])`が廃止された（開発者向け）
- `XmlCalc.MoveAgent(Team, AgentNumber, AgentActivityData)`が廃止された（開発者向け）
- `AgentStatusCodeExpansion`が`AgentStatusCodeExtension`に変更された（開発者向け）
- `FieldExpansion`が`FieldExtension`に変更された（開発者向け）
- `PointExpansion`が`PointExtension`に変更された（開発者向け）
- `PqrDataExpansion`が`PqrDataExtension`に変更された（開発者向け）
- `TeamExpansion`が`TeamExtension`に変更された（開発者向け）
- `TurnDataExpansion`が`TurnDataExtension`に変更された（開発者向け）

### Version 16.0

- `AgentActivityData.DeepCopy()`が廃止された（開発者向け）
- `Calc.DeepCopy()`が廃止された（開発者向け）
- `Cell.DeepCopy()`が廃止された（開発者向け）
- `FieldExtension.DeepCopy()`が廃止された（開発者向け）
- `TurnData.DeepCopy()`が廃止された（開発者向け）
- `XmlCalc.DeepCopy()`が廃止された（開発者向け）

### Version 17.0

- 試合のデータを「名前をつけて保存」できるようになった
- 試合のデータを「上書き保存」できるようになった
- PQRファイルを開く機能が試合のデータを開く機能に変更された

#### Version 17.1

- Prefetchingディレクトリが存在しないとき例外が発生するバグを修正

#### Version 17.2

- `Ctrl`+`S`で試合を保存できるようになった
- readme.mdの微調整

### Version 18.0

- 保存した試合を開けないバグを修正
- `Agent`の引数なしコンストラクタを実装（開発者向け）
- `TurnData`の引数なしコンストラクタを実装（開発者向け）
- `Agents.Add()`を実装（開発者向け）
- `AgentsActivityData.Add()`を実装（開発者向け）
- `Field.Add()`を実装（開発者向け）
- `XmlTurnData`を追加（開発者向け）
- `XmlCell`を追加（開発者向け）
- `Calc(XmlCalc)`を実装（開発者向け）
- `XmlCalc(Calc)`を実装（開発者向け）
- `Cell(XmlCell)`を実装（開発者向け）
- `XmlCell(Cell)`を実装（開発者向け）
- `TurnData(XmlTurnData)`を実装（開発者向け）
- `XmlTurnData(TurnData)`を実装（開発者向け）
- `XmlCalc.Agents`を廃止（開発者向け）
- `XmlCalc.Field`を廃止（開発者向け）
- `XmlCalc.Height`を廃止（開発者向け）
- `XmlCalc.Width`を廃止（開発者向け）
- `XmlCalc(int, int[,], Coordinate[])`を廃止（開発者向け）
- `XmlCalc(int, Field, Coordinate[])`を廃止（開発者向け）
- `XmlCalc.ComplementEnemysPosition()`を廃止（開発者向け）
- `XmlCalc.InitializationOfField()`を廃止（開発者向け）
- `XmlCalc.TurnEnd()`を廃止（開発者向け）
- `XmlCalc.PutTile()`を廃止（開発者向け）

#### Version 18.1

- readme.mdの微調整
- `MainForm`におけるソースコードの管理（開発者向け）

#### Version 18.2

- `Calc`におけるソースコードの管理（開発者向け）
- `Show`におけるソースコードの管理（開発者向け）

### Version 19.0

- クリックだけで操作できるようになった
- デバッグモードのみオートセーブされるように変更された
- 矢印が表示されるようになった
- ダブルクリックした直後にマウスをフィールドの外に移動させるとエラーになるバグを修正
- `TeamDesigns`を追加（開発者向け）
- `Direction`を追加（開発者向け）
- `CharactorBitmap`を追加（開発者向け）
- `DrawField`を追加（開発者向け）
- `ClickField`を追加（開発者向け）

### Version 20.0

- Visualizerの起動中にスプラッシュウインドウを表示するにした
- `Show`におけるソースコードの管理（開発者向け）
- `DrawField`におけるソースコードの管理（開発者向け）
- `Show.teamDesign`を廃止（開発者向け）
- `Show.pictureBox`を廃止（開発者向け）
- `Show.procon29_Logger`を廃止（開発者向け）
- `Show.backGroundSolidBrush`を廃止（開発者向け）
- `Show.selectSolidBrush`を廃止（開発者向け）
- `Show.clickedSolidBrush`を廃止（開発者向け）
- `Show.pointFont`を廃止（開発者向け）
- `Show.clickedField`を廃止（開発者向け）
- `Show.agentBitmap`を廃止（開発者向け）
- `Show.fairyBitmap`を廃止（開発者向け）
- `Show.PointFont`を廃止（開発者向け）
- `Show.DrawBackground`を廃止（開発者向け）
- `Show.DrawEnclosedCell`を廃止（開発者向け）
- `DrawField.MoveOrRemove`を廃止（開発者向け）
- `DrawField.PictureBox`を廃止（開発者向け）
- `DrawField.Draw(Coordinate)`を追加（開発者向け）
- `DrawField.DrawMouseOverCell(Coordinate)`を追加（開発者向け）
- `DrawField.DrawAgent(Coordinate)`を追加（開発者向け）
- `DrawField.DrawAgent(Agent, Coordinate)`を追加（開発者向け）
- `DrawField.DrawAgent(Graphics, Agent, Coordinate)`を追加（開発者向け）
- `DrawField.DrawAgent(Graphics, Agent)`を追加（開発者向け）
- `DrawField.DrawAgent(Graphics, Agent, ImageAttributes)`を追加（開発者向け）
- `DrawField.DrawAgentName(Coordinate)`を追加（開発者向け）
- `DrawField.DrawAgentName(Agent)`を追加（開発者向け）
- `DrawField.DrawAgentName(Graphics, Agent)`を追加（開発者向け）
- `DrawField.Draw()`を追加（開発者向け）
- `DrawField.DrawAgent()`を追加（開発者向け）
- `DrawField.DrawEnclosedCell(Cell)`を追加（開発者向け）
- `DrawField.DrawEnclosedCell(Graphics, Cell)`を追加（開発者向け）
- `DrawField.DrawEnclosedCell(int)`を追加（開発者向け）
- `DrawField.DrawTile(int)`を追加（開発者向け）
- `DrawField.DrawAgent(int)`を追加（開発者向け）
- `DrawField.DrawAgent(int, Coordinate)`を追加（開発者向け）
- `DrawField.DrawAgentName(int)`を追加（開発者向け）
- `DrawField.DrawAgentName(int, Coordinate)`を追加（開発者向け）
- `DrawField.Draw(int)`を追加（開発者向け）
- `DrawField.Draw(int, Coordinate)`を追加（開発者向け）
- `DrawField.DrawFruitFairies(int)`を追加（開発者向け）

### Version 21.0

- 試合が終了した際に、リプレイが表示されるようになった
- `TrumpMark`を追加（開発者向け）
- `TrumpNumber`を追加（開発者向け）
- `TsvReader`を追加（開発者向け）

#### Version 21.1

- readme.mdの微調整

### Version 22.0

- 本番モードと練習モードの実装

#### Version 22.1

- 本番モードのときに「ボットで選択」ボタンで再描画するように修正

### Version 23.0

- `Calc.Simulate(AgentsActivityData)`を追加（開発者向け）
- `Calc.Simulate(Team, AgentActivityData[])`を追加（開発者向け）
- `Calc.Simulate(Team, AgentNumber, AgentActivityData)`を追加（開発者向け）

### Version 24.0

- `Field.CellExist(Coordinate)`を追加（開発者向け）
- `Field.CellExist(int, int)`を追加（開発者向け）

### Version 25.0

- 一度試合が終了した後に、ターン数を変更した場合に、ターンエンドのボタンが再び表示されるように修正しました。
- `ClickField.PushKey(Keys)`を追加（開発者向け）

### Version 26.0

- `PointExtension`から`CoordinateExtension`に変更（開発者向け）

### Version 27.0

- C# 6にダウングレード

#### Version 27.1

- `Logger.richTextBox`を廃止（開発者向け）

### Version 28.0

- `AgentStatusCodeExtension.IsMovement(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.IsRemovementOutTile(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.IsRemovementOpponentTile(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.ToSucceeded(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.ToRequest(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.ToAction(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.ToAttribute(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.IsRequest(this AgentStatusCode)`のコード最適化（開発者向け）
- `AgentStatusCodeExtension.IsSucceeded(this AgentStatusCode)`のコード最適化（開発者向け）
- `AgentStatusCodeExtension.IsFailed(this AgentStatusCode)`のコード最適化（開発者向け）

### Version 29.0

- プリフェッチングファイルを読み込んだ際に表示されるダイアログを廃止した
- プリフェッチングファイルを読み込んだ際にログに表示されるようにした
- 1ターン目のときにエージェントの位置の表示がおかしいバグを修正

### Version 30.0

- 本番モードでターンエンドのときもボットを呼び出してしまうバグを修正

### Version 31.0

- Visualizerから外部プログラムの起動が可能になった

#### Version 31.1

- ボットが呼び出せない重大なバグを修正

#### Version 31.2

- 最後のターンエンドすると例外がおきるバグを修正

### Version 32.0

- エージェントのログが二人目以降は`NotDoneAnything`になってしまうバグを修正
- `TsvReader`のバグを修正（開発者向け）
- `Calc`のコード最適化（開発者向け）

### Version 33.0

- 同じタイル上に2人のエージェントが同衾するバグを修正（SeitaHigashi氏によるデバッグ）

#### Version 33.1

- `Logger.Write(Color, string)`を廃止（開発者向け）
- `Logger.WriteLine(Color, string)`を廃止（開発者向け）
- `Logger.DefaultColor`を追加（開発者向け）
- `Logger.Write(string)`を追加（開発者向け）
- `Logger.Write(string, Color)`を追加（開発者向け）
- `Logger.WriteLine(string)`を追加（開発者向け）
- `Logger.WriteLine(string, Color)`を追加（開発者向け）
- `Logger`のアクセシビリティを`public`に変更（開発者向け）

### Version 34.0

- ボットがコンソール画面にログを表示できるようになった
- `PlayMode`のアクセシビリティを`public`に変更（開発者向け）

### Version 35.0

- ボットコンソールのフォントが`Console`に改善された
- ボットコンソールの非透明度が90%に変更された
- ボットコンソールがデフォルトで非表示になった
- ボットコンソールを閉じてもエラーにならないように修正

### Version 36.0

- `MainForm`のコード最適化（開発者向け）
- `AgentStatusCodeExtension.ToFailedByBeingNotMooreNeighborhood(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.ToYouHadCollisionsWithYourselfAndYouFailed(this AgentStatusCode)`を追加（開発者向け）
- `AgentStatusCodeExtension.ToFailedInMovingByTryingToGoOutOfTheField(this AgentStatusCode)`を追加（開発者向け）

### Version 37.0

- ゲーム開始時に敵の位置を入力するように修正
- 敵の位置を入力するフォームを追加

### Version 38.0

- 敵の位置を入力するテキストボックスが2桁の数値しか入れられないように修正された
- 敵の位置を入力するフォームのフォントがメイリオに変更された
- 敵の位置を入力するフォームの色が修正された

### Version 39.0

- 敵の位置を入力するフォームで自分のチームがどこにいるのか表示されるようにした
- `MainForm`のコード最適化（開発者向け）

### Version 40.0

- 試合のターン数のデフォルトが10ターンから40ターンに変更された

### Version 41.0

- 敵の位置を入力するフォームにOKボタンがついた

### Version 42.0

- Visualizerの起動中に表示するスプラッシュウインドウが廃止された
- `Program`のコード最適化（開発者向け）

### Version 43.0

- 敵の位置を入力するフォームのアイコンを非表示にした
- 敵の位置を入力するフォームのコントロールボタンを非表示にした

### Version 44.0

- 敵の位置を入力するフォームで空白のままOKボタンを押すと、エラーダイアログが出るようになった

### Version 45.0

- `MainForm`のコード最適化（開発者向け）
- `Calc.Calc(int, int[,], Coordinate)`を廃止（開発者向け）
- `Calc.Calc(int, Field, Coordinate)`を廃止（開発者向け）

### Version 46.0

- 敵の位置を入力するフォームで敵の位置を自動補完するようになった

### Version 47.0

- `MainForm`のコード最適化（開発者向け）
- `Field.IsMovable(Agent, Arrow)`を追加（開発者向け）
- `Field.IsRemovableOurTile(Agent, Arrow)`を追加（開発者向け）
- `Field.IsRemovableOpponentTile(Agent, Arrow)`を追加（開発者向け）

### Version 48.0

- `MainForm`のコード最適化（開発者向け）
- `Field.Field(int, int, int[,])`を追加（開発者向け）
- `Calc.Calc(int, int[,], Agents)`を廃止（開発者向け）
- `Calc.Calc(int turn, Field field, Agents agents)`を追加（開発者向け）

### Version 49.0

- `Field.Sum()`を追加（開発者向け）
- `Field.SumAbs()`を追加（開発者向け）
- `Field.AreaPoint(Team)`を追加（開発者向け）
- `Field.EnclosedPoint(Team)`を追加（開発者向け）
- `Field.TotalPoint(Team)`を追加（開発者向け）

### Version 50.0

- `Calc.Sum()`を廃止（開発者向け）
- `Calc.SumAbs()`を廃止（開発者向け）
- `Calc.AreaPoint(Team)`を廃止（開発者向け）
- `Calc.EnclosedPoint(Team)`を廃止（開発者向け）
- `Calc.TotalPoint(Team)`を廃止（開発者向け）

### Version 51.0

- 処理時間を計測するボタンを追加した

### Version 52.0

- 本番モードでボットがボットコンソールに書き込もうとしたらエラーになるバグを修正

### Version 53.0

- 処理時間を計測するフォームの色やデザインを修正した

### Version 54.0

- 処理時間を計測するフォームに「ディープクローン」の項目が追加された

#### Version 54.1

- バージョン管理の修正

### Version 55.0

- 処理時間を計測するフォームが1秒以内に表示できるようになった
- 処理時間を計測するフォームに自由に検査項目を追加できるようになった

### Version 56.0

- `hydro_go_bot`との通信方式に対応するための機能が追加された

### Version 57.0

- `Calc.MoveAgent(Team, AgentNumber, Coordinate)`のコード最適化（SeitaHigashi氏によるデバッグ）
- `Calc.Simulate(AgentsActivityData)`のコード最適化（SeitaHigashi氏によるデバッグ）
- `Calc.Simulate(Team, AgentActivityData[])`のコード最適化（SeitaHigashi氏によるデバッグ）
- `Calc.Simulate(Team, AgentNumber, AgentActivityData)`のコード最適化（SeitaHigashi氏によるデバッグ）

### Version 58.0

- 新規作成で試合のターン数のデフォルトが10ターンから40ターンに変更された
- 処理時間を計測するフォームの修正

### Version 59.0

- ボットの処理速度が最大で30倍速くなった

### Version 60.0

- 右クリックでいつでもマスのタイルの情報を変更できるようになった
- `PictureBoxExtension.ToCellCordinate(this PictureBox, Calc, Coordinate)`を追加（開発者向け）

### Version 61.0

- トランプを表示するための領域が表示できるようになった
- `CoordinateExtension.ToCellCordinate(this Coordinate, PictureBox, Field)`を追加（開発者向け）
- `PictureBoxExtension.ToCellCordinate(this PictureBox, Calc, Coordinate)`を廃止（開発者向け）
- `Show.BackGroundSolidBrush`を廃止（開発者向け）
- `Show.SelectSolidBrush`を廃止（開発者向け）
- `Show.ClickedSolidBrush`を廃止（開発者向け）
- `Show.PointFont`を廃止（開発者向け）
- `Show.Procon29_Logger`を廃止（開発者向け）

### Version 62.0

- 右クリックでマスのタイルの情報を書き換えた後、自動的に再計算するようになった
- `Calc.Recalculation()`を追加（開発者向け）

### Version 63.0

- `TegetegeBot`にて2ターン目から試合が始まってしまうバグを修正
- ボットに`Calc`を渡す際に、ディープコピーするように変更（開発者向け）
- このバージョンで、足りないコメントを補完（開発者向け）
- このバージョンで、C#の命名規則違反になっている変数名などを変更（開発者向け）

### Version 64.0

- エージェントがフィールドの中心を見るように修正

### Version 65.0

- ボットの処理速度がさらに少し速くなった

### Version 66.0

- 試合を強制的に終わらせるようにした

### Version 67.0

- テンキーでの移動に関するバグが修正された

#### Version 67.1

- `AgentStatusCodeExtension`のアクセシビリティを`public`に変更（開発者向け）

### Version 68.0

- ボットコンソールを閉じるとチェックが外れるように修正
- `Trump`を追加（開発者向け）
- トランプの画像をリソースに追加（開発者向け）

### Version 69.0

- `AgentsActivityData`を`FourAgentsActivityData`に変更（開発者向け）
- `AgentsActivityDataExtension`を`FourAgentsActivityDataExtension`に変更（開発者向け）
- `TwoAgentsActivityData`を追加（開発者向け）
- `Calc.Simulate(Team, AgentActivityData[])`を廃止（開発者向け）
- `Calc.Simulate(Team, TwoAgentsActivityData)`を追加（開発者向け）

#### Version 69.1

- ボットによる緊急修正

### Version 70.0

- **Version 69.0** の取り消し

### Version 71.0

- `SymmetricalPattern`を追加（開発者向け）
- `Size.ToString()`を追加（開発者向け）
- `FieldGenerator`を追加（開発者向け）

### Version 72.0

- 新規作成をリニューアル

### Version 73.0

- Visualizer 共通フィールドを開くためのフォームを追加
- **Visualizer Common Field Library** を実装

### Version 74.0

- ボットを読み込むためのフォームを追加

### Version 75.0

- Visualizer 共通フィールドを開くためのフォームで「左右および上下対称」が選択できないバグを修正

### Version 76.0

- `CreateNewForm`を廃止（開発者向け）

### Version 77.0

- 本番モードでは新規作成を開くとデフォルトが「直接ファイルを開く」になるように修正
- `CreateNewForm2`を`CreateNewForm`に変更（開発者向け）

### Version 78.0

- ボットを選択のときに「人間」または「Hydro Go Bot」を選択した際に、ファイルを開くボタンを非表示にするように変更
- ボットを読み込む際や、新しく試合を開始するときに読み込んだファイル名をコンソールに表示するように変更

#### Version 78.1

- ボットを読み込む際のバグを修正
- デフォルトが「人間」から「ボット」に変更された

### Version 79.0

- Visualizer Common Field Libraryの一部にエージェントたちが同じマスに2人存在してしまうフィールドが存在しているバグを修正

### Version 80.0

- **Bot Wars** が実装された

## バージョンの上がり方について

Visualizerの正式なバージョンは`1.14.1`のように、`メジャーバージョン.マイナーバージョン.ビルドバージョン`で表されます。  
メジャーバージョンは基本的に`1`のままですので、普段は`14.1`というように省略されます。  
マイナーバージョンは、主に外見の変化、内部の互換性のない仕様の変化などのときに上がります。
ビルドバージョンは、小さなバグの修正や、コードの最適化などのときに上がります。

## 君は開発者のフレンズなんだね！

### ボット開発者のフレンズへ

ボット開発の上で必要なページだよ！読んでおこう！  
[Agent について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Agent)  
[Agent Activity Data について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/Agent-Activity-Data)  
[Agent Status Code について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Agent-Status-Code)  
[Agents について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Agents)  
[Arrow について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Arrow)  
[Calc について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Calc)  
[Cell について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Cell)  
[Coordinate について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Coordinate)  
[Field について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Field)  
[Team について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Team)  
[Team Expansion について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Team-Expansion)  
[How to talk with .dll について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/How-to-talk-with-.dll)

### QRコード解析ソフト開発者のフレンドへ

[PQRファイルの形式について](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-.PQR-file)

## コードメトリックス

|バージョン|保守容易性指数|サイクロマティック複雑度|継承の深さ|クラス結合|コード行|
|:-:|:-:|:-:|:-:|:-:|:-:|  
|3.0 β|83|286|7|97|591|
|3.0|81|349|7|107|1068|
|6.0|80|571|7|125|1546|
|17.1|81|745|7|168|1902|
|18.1|81|763|7|171|1924|
|21.0|82|1026|7|194|2439|
|25.0|83|1064|7|195|2509|
|28.0|83|1079|7|198|2544|
|34.0|83|1108|7|201|2625|
|54.1|82|1145|7|217|2833|
|57.0|81|1162|7|218|2903|
|59.0|81|1167|7|218|2918|
|67.1|81|1166|7|224|2826|
|75.0|80|1266|7|238|3468|
|77.0|80|1234|7|236|3004|
|80.0|81|1304|7|249|3210|