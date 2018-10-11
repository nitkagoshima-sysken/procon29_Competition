# FAQ

## 自分のチームはどうすれば把握できますか？

`OurTeam`で表すことができます。

## 敵チームはどうすれば把握できますか？

`OurTeam.Opponent()`で表すことができます。

## 自分のチームの一人目のエージェントはどうすれば把握できますか？

`Calc.Agents[OurTeam, AgentNumber.One]`で表すことができます。

## 相手のチームの二人目のエージェントはどうすれば把握できますか？

`Calc.Agents[OurTeam.Opponent(), AgentNumber.Two]`で表すことができます。  
`Calc.Agents[チーム, エージェントナンバー]`で任意のエージェントを表すことができます。

## エージェントの場所はどうすれば把握できますか？

`エージェント.Position`で表すことができます。  
`Calc.Agents[OurTeam, AgentNumber.One].Position`で自分のチームの一人目のエージェントがどこにいるか分かります。

## 座標のX軸、Y軸の取得、設定はどうすればいいですか？

`座標.X`、`座標.Y`で取得、設定できます。  
例えば、

```cs
var test = new Coordinate(0,0);
test.X = 1; // X軸に1をセット
test.Y = 3; // Y軸に3をセット
Log.WriteLine(test.ToString()); // ボットコンソールに座標を表示
```

## ボットコンソールに「あぁ^～心がぴょんぴょんするんじゃぁ^～」と表示したいのですが、どうすればいいですか？

```cs
Log.WriteLine("あぁ ^～心がぴょんぴょんするんじゃぁ ^～");
```

ただし、日本語を表示するとそれ以降フォントが変わってしまうので、できれば英語でログは表示してください。

## ボットコンソールに改行なしで文字列を表示したいのですが、どうすればいいですか？

```cs
Log.Write("Hello World!");
```

## せっかくなので俺はボットコンソールに赤い文字列を表示したいのですが、どうすればいいですか？

```cs
Log.Write("Hello World!", Color.Red);
```

もしエラーのときは、ソースコードの先頭に、

```cs
using System.Drawing;
```

を追加してください。

## すべてのエージェントに何らかの処理をしたい

```cs
foreach (agent in Calc.Agents)
{
    // 何らかの処理をここに書きましょう。
    Log.Write("Team: ");
    Log.Write(agent.Team.ToString()); // エージェントのチームを表示
    Log.Write(" AgentNumber: ");
    Log.Write(agent.AgentNumber.ToString()); // エージェントのエージェントナンバーを表示
    Log.Write(" Name: ");
    Log.Write(agent.Name); // エージェントの名前を表示
    Log.Write(" Position: ");
    Log.Write(agent.Position.ToString()); // エージェントの場所を表示
    Log.WriteLine("");
    // こんな感じにね
}
```

ちなみに表示結果はこうなります。（例です。）

```表示結果
Team: A AgentNumber: One Name: Strawberry Position: {11, 6}
Team: A AgentNumber: Two Name: Apple Position: {9, 10}
Team: B AgentNumber: One Name: Kiwi Position: {11, 5}
Team: B AgentNumber: Two Name: Muscat Position: {9, 1}
```