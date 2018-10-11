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

エージェントの座標を表示したいなら、

```cs
var posi = Calc.Agents[OurTeam, AgentNumber.One].Position;
Log.WriteLine("一人目のエージェントは、");
Log.WriteLine("左から" + (posi.X + 1) + "マス目に、");
Log.WriteLine("上から" + (posi.Y + 1) + "マス目にいます。");
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
foreach (var agent in Calc.Agents)
{
    // 何らかの処理をここに書きましょう。
    Log.Write(agent.Name); // エージェントの名前を表示
    Log.Write("は、現在、");
    Log.Write(agent.Position.ToString()); // エージェントの場所を表示
    Log.WriteLine("のところにいます。");
    // こんな感じにね
}
```

ちなみに表示結果はこうなります。（例です。）

```表示結果
Strawberryは、現在、{11, 6}のところにいます。
Appleは、現在、{9, 10}のところにいます。
Kiwiは、現在、{11, 5}のところにいます。
Muscatは、現在、{9, 1}のところにいます。
```

## 敵チームのタイルが置いてあるマスをすべて取得したい

```cs
var list =
    from cell in Calc.Field // フィールドからマス(cell)をひとつずつ取り出して、
    where cell.IsTileOn[OurTeam.Opponent()] == true // もし、そのマスに敵のタイルが置いてあったら、
    select cell.Coordinate; // そのマスの座標をリストに追加する。

foreach (var item in list) // リストからひとつずつ取り出して、
{
    Log.WriteLine(item.ToString()); // 座標を表示する。
}
```

ちなみに表示結果はこうなります。（例です。）

```表示結果
{7, 2}
{8, 0}
{8, 1}
{9, 0}
{9, 1}
{9, 3}
{10, 2}
{11, 3}
{11, 4}
{11, 5}
```

クエリ式を詳しく知りたい方は、How to use Queryを見ましょう。