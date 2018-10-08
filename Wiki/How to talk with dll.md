# ボットの仕様

## ボットの命名規則

- 必ず、最後は`Bot`で終わらせましょう
- 鹿児島弁か鹿児島に関係する単語を名前に含みましょう

例. `TegeTegeBot`

せっかく徳島に行って全国大会するのだから、  
ここは遠慮せずに、鹿児島をアピールしましょう！

## とりあえずプロジェクトを作成しよう

1. ソリューションエクスプローラーで`ソリューション 'Procon29'`の項目を右クリック！
1. `追加(D)`をクリック
1. `新しいプロジェクト(N)`をクリック
1. `クラスライブラリ (.NET Framework)`をクリック
1. ボットの名前を決めよう（ボットの名前には規則があります。詳しくはボットの命名規則を見て）
1. 今作ったプロジェクト内の「参照」を右クリック
1. `参照の追加(R)`をクリック
1. Procon29_Visualizerにチェックを入れる

## とりあえず空のボットを作成しよう

試しに、`CustodomBot`を作成します。  
ちなみに`CustodomBot`は「custom（習慣）」と「かすたどん」の合成語です。  
みなさんも`CustodomBot`のような慣習的な`C#`のコードを書きましょう。  

あなたがまずボットを作るには次のように書きましょう。  

```cs
// CustodomBot.cs

using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.CustodomBot
{
    class CustodomBot: Bot.Bot
    {
        public CustodomBot() : base() { }

        public override AgentActivityData[] Answer()
        {
        }
    }
}
```

よく分からない人は、「おまじない」だと思ってください。  
この`CustodomBot`というクラスは`Bot`というクラスを継承しています。  
（つまり、`CustodomBot`は`Bot`の子ども）  
`Bot`クラスは、Visualizerとあなたのボットとの仲介をしてくれるクラスです。  
ですので、すべてのボットは`Bot`クラスを継承しなければなりません。  
もし、継承しなければ、Visualizerは

ここで、名前空間とクラス名とファイル名をすべて`CustodomBot`にしてください。  
あなたが作るボットの名前も、名前空間とクラス名とファイル名をすべて統一してください。  
これはVisualizerが`CustodomBot.dll`を開くときに、  
名前空間`CustodomBot`のクラス`CustodomBot`を探そうとするからです。  
この3つをすべて同じにしないと、Visualizerが例外を投げます。
ですので、必ずボットの名前は、名前空間とクラス名とファイル名をすべて同じにしてください。  

```cs
// CustodomBot のコンストラクタ
public CustodomBot() : base() { }
```

これは`CustodomBot`のコンストラクタです。  
よく分からない人はおまじないだと思いましょう。

```cs
// ここでBotの中身を書く
// C言語で言うmain関数みたいなもの
public override AgentActivityData[] Answer()
{
    // 処理
    return ...;
}
```

Visualizerはあなたのボットの中の`Answer()`というメソッドを探して、それを実行します。  
ですので、`Answer()`の中にボットの中身を書きましょう。

## 何もしないボットを作成しよう

```cs
// 何もしないボットを作る
public override AgentActivityData[] Answer()
{
    var result = new AgentActivityData[2];
    result[0] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
    result[1] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
    return result;
}
```

さて、返り値には`int`や`string`の型を返すメソッドがありますが、  
あなたがこれから作るボットの`Answer()`の返り値の型は`AgentActivityData[]`です。  
`AgentActivityData[]`は`AgentActivityData`型の配列を意味します。

```cs
var result = new AgentActivityData[2];
result[0] = // あなたのチームの1人目のエージェントの Agent Activity Data が入ります
result[1] = // あなたのチームの2人目のエージェントの Agent Activity Data が入ります
```

Agent Activity Dataでは、エージェントが次のターンで何をしたいかを記述します。  
`var result = new AgentActivityData[2];`で`AgentActivityData`型の要素数2の`result`という変数が宣言されます。  
`result[0]`: 配列の0番目にはボット側のチームの1人目のエージェントの Agent Activity Data が入り、  
`result[1]`: 配列の1番目にはボット側のチームの2人目のエージェントの Agent Activity Data が入ります。

```cs
result[0] =
    new AgentActivityData(
      AgentStatusCode.RequestNotToDoAnything,
      new Point(0, 0)
      );
```

AADの中には2つの要素があります。
それは`AgentStatusCode`と`Destination`です。  
`AgentStatusCode`で「エージェントが何をするか」を決め、
`Destination`で「それをエージェントがどこへするか」を決めます。

|AgentStatusCode|Destination|
|:-:|:-:|
|`AgentStatusCode.RequestNotToDoAnything`|`new Point(0, 0)`|
|何もしない|（何もしないので今回は意味をなさない）|

今回の場合はこのような意味になります。  
ちなみに、`RequestNotToDoAnything`のときは、  
第二引数を省略できるので、このように書けます。

```cs
result[0] =
    new AgentActivityData(
      AgentStatusCode.RequestNotToDoAnything,
      );
```

他のパターンを下に示します。

```cs
result[0] =
    new AgentActivityData(
      AgentStatusCode.RequestMovement,
      new Point(2, 8)
      );
```

|AgentStatusCode|Destination|
|:-:|:-:|
|`AgentStatusCode.RequestMovement`|`new Point(2, 8)`|
|移動する|縦2行目、横8列目のマス|

この場合は、「あなたのチームの1人目のエージェント」は「縦2行目、横8列目のマス」へ「移動する」という意味になります。

`AgentStatusCode`が取りうる値を**ASC** (Agent Status Code)といいます。  
例えば`ASC001`は"Request Not To Do Anything"（その場でとどまり、何もしないことを要請します）です。  
詳しくは[About Agent Status Data](https://github.com/nitkagoshima-sysken/procon29_Competition/wiki/About-Agent-Status-Code)を参照してください。

```cs
return result;
```

これで「何もしないこと」を返すボットが完成しました。