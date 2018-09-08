# Procon29 Visualizer 15.0

## What's new

### BaseCalcからXmlCalcに変更された（開発者向け）

Visualizer 15.0で、`BaseCalc`が`XmlCalc`に変更され、
`Calc`をXMLに変換する際に`XmlCalc`に変換してから行うためにのみ使用されるようになりました。  
ですので、Visualizer 15.0以降では、それ以外の用途のときは必ず`Calc`を使用してください。  
また、`XmlCalc`では`Calc`にはあった大半のメソッドが取り除かれました。

### ExpansionがExtensionに変更された（開発者向け）

拡張メソッドを実装されたクラスにはExpansionとExtensionが混在していましたが、  
Visualizer 15.0で、Extensionに統一されました。  
ボット開発者は一度、リビルドしてから実行してください。

## 操作方法

 1. クリックで対象のエージェントを選択しよう。
 2. ダブルクリックで行きたい場所、またはタイルを取り除く場所を選択しよう。
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
|`Ctrl`+`Z`| ターンを元に戻す |
|`Ctrl`+`Y`| ターンをやり直す |

## ボットのプリフェッチング機能の使い方

`Bot.tsv`を開きます。
オレンジチームにボットをあらかじめ追加したい場合は文頭から`A`+[Tab]+[ボットのパス名]を書きます。
ライムチームにボットをあらかじめ追加したい場合は文頭から`B`+[Tab]+[ボットのパス名]を書きます。

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

- AgentArrayが廃止された（開発者向け）
- TeamArrayが廃止された（開発者向け）

#### Version 14.1

- readme.mdの微調整

### Version 15.0

- BaseCalcからXmlCalcに変更された（開発者向け）
- ExpansionがExtensionに変更された（開発者向け）

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

commit: `21be2795a19b75c90228285b8734ed9c7781f989`

|保守容易性指数|サイクロマティック複雑度|継承の深さ|クラス結合|コード行|
|:-:|:-:|:-:|:-:|:-:|  
|80|571|7|125|1546|
