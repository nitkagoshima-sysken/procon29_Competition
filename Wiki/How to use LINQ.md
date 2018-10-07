# FieldにおけるLINQの有効的な使い方

LINQ とは、 Language Integrated Query の略称で、 C# や VB などの .NET Framework 対応言語に、 リレーショナルデータや XML に対するデータ操作構文を組み込む （＋ データベースや XML 操作用のライブラリ） というものです。

その目的は、 データベース問い合わせとオブジェクト指向プログラミング（OOP: Object-Oriented Programming）の統合です。 これまでに、 SQL などの問い合わせ言語によって、 データベースの構築・問い合わせが容易になりました。 また、 C# や Java などの OOP 言語によって、 さまざまなデータ（文字列・数値はもちろん、画像や音声なども）に対する操作を容易に記述できるようになりました。

## 要素の取得（単一）

|メソッド名|機能|
|:-:|:-:|
|`ElementAt`|指定した位置(インデックス)にある要素を返します。|
|`First`|最初の要素を返します。|
|`Last`|最後の要素を返します。|
|`Single`|唯一の要素を返します。該当する要素が複数ある場合、例外をスローします。|

```cs
Cell cell;
cell = Calc.Field.ElementAt(1);
Console.WriteLine(cell);
cell = Calc.Field.First();
Console.WriteLine(cell);
cell = Calc.Field.Last();
Console.WriteLine(cell);
cell = Calc.Field.Single(); // ここで例外が投げられる。
```

```実行結果
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 1}}
{Point:-2, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 0}}
{Point:-2, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {10, 7}}
'Procon29 Visualizer.exe' (CLR v4.0.30319: Procon29 Visualizer.exe): 'C:\WINDOWS\Microsoft.Net\assembly\GACcellMSIL\System.Core.resources\v4.0cell4.0.0.0celljacellb77a5c561934e089\System.Core.resources.dll' が読み込まれました。モジュールがシンボルなしでビルドされました。
例外がスローされました: 'System.InvalidOperationException' (System.Core.dll の中)
型 'System.InvalidOperationException' のハンドルされていない例外が System.Core.dll で発生しました
シーケンスに複数の要素が含まれています
```

実際、フィールドには必ず、複数のマスが含まれているため、`Single`を使う機会はあまりない。  
ちなみに例外を投げてほしくないときは語尾に`OrDefault`をつけると、例外を投げる代わりに既定値を返すらしいが、試してみたら例外が投げられたのでよく分からない。

## 要素の取得（複数）

|メソッド名|機能|
|:-:|:-:|
|`Where`|条件を満たす要素をすべて返します。|
|`Distinct`|重複を除いたシーケンスを返します。|
|`Skip`|先頭から指定された数の要素をスキップし、残りのシーケンスを返します。|
|`SkipWhile`|先頭から指定された条件を満たさなくなるまで要素をスキップし、残りのシーケンスを返します。|
|`Take`|先頭から指定された数の要素を返します。|
|`TakeWhile`|先頭から指定された条件を満たす要素を返します。|

```cs
Console.WriteLine("[Where]");
var list = Calc.Field.Where(cell => 3 <= cell.Point);
foreach (var cell in list)
    Console.WriteLine(cell);

Console.WriteLine("[Skip]");
list = Calc.Field.Where(cell => 3 <= cell.Point).Skip(6);
foreach (var cell in list)
    Console.WriteLine(cell);

Console.WriteLine("[SkipWhile]");
list = Calc.Field.Where(cell => 3 <= cell.Point).SkipWhile(cell => cell.Coordinate.X <= Calc.Field.Width / 2);
foreach (var cell in list)
    Console.WriteLine(cell);

Console.WriteLine("[Take]");
list = Calc.Field.Where(cell => 3 <= cell.Point).Take(3);
foreach (var cell in list)
    Console.WriteLine(cell);

Console.WriteLine("[TakeWhile]");
list = Calc.Field.Where(cell => 3 <= cell.Point).TakeWhile(cell => cell.Coordinate.X <= Calc.Field.Width / 2);
foreach (var cell in list)
    Console.WriteLine(cell);
```

```実行結果
[Where]
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {1, 1}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 2}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 5}}
{Point:3, IsTileOn: {False, True}, IsEnclosed: {False, False}, Coordinate: {1, 6}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {5, 3}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {5, 4}}
{Point:3, IsTileOn: {False, True}, IsEnclosed: {False, False}, Coordinate: {9, 1}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {9, 2}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {9, 5}}
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {9, 6}}
[Skip]
{Point:3, IsTileOn: {False, True}, IsEnclosed: {False, False}, Coordinate: {9, 1}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {9, 2}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {9, 5}}
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {9, 6}}
[SkipWhile]
{Point:3, IsTileOn: {False, True}, IsEnclosed: {False, False}, Coordinate: {9, 1}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {9, 2}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {9, 5}}
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {9, 6}}
[Take]
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {1, 1}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 2}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 5}}
[TakeWhile]
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {1, 1}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 2}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 5}}
{Point:3, IsTileOn: {False, True}, IsEnclosed: {False, False}, Coordinate: {1, 6}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {5, 3}}
{Point:3, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {5, 4}}
```

## 集計

|メソッド名|機能|
|:-:|:-:|
|`Max`|最大値を返します。|
|`Min`|最小値を返します。|
|`Average`|平均値を返します。|
|`Sum`|合計を返します。|
|`Count`|要素数を返します。|
|`Aggregate`|アキュムレータ関数で処理した結果を返します。|

```cs
Console.WriteLine(Calc.Field.Max(cell => cell.Point));
Console.WriteLine(Calc.Field.Min(cell => cell.Point));
Console.WriteLine(Calc.Field.Average(cell => cell.Point));
Console.WriteLine(Calc.Field.Sum(cell => cell.Point));
Console.WriteLine(Calc.Field.Count());
```

```実行結果
3
-2
1
88
88
```

## 判定

|メソッド名|機能|
|:-:|:-:|
|`All`|すべての要素が条件を満たしてるか判定します。|
|`Any`|条件を満たす要素が含まれているか判定します。|
|`Contains`|指定した要素が含まれているかどうかを判定します。|
|`SequenceEqual`|2つのシーケンスが等しいかどうかを判定します。|

```cs
Console.WriteLine(Calc.Field.All(cell => cell.Point == 3));
Console.WriteLine(Calc.Field.Any(cell => cell.Point == 3));
Console.WriteLine(Calc.Field.Contains(new Cell(new Coordinate(1, 2))));
Console.WriteLine(Calc.Field.SequenceEqual(new Field(Calc.Field)));
```

```実行結果
False
True
False
False
```

## 集合

|メソッド名|機能|
|:-:|:-:|
|`Union`|指定したシーケンスとの和集合を返します。|
|`Except`|指定したシーケンスとの差集合を返します。|
|`Intersect`|指定したシーケンスとの積集合を返します。|

```cs
Console.WriteLine(Calc.Field.Where(cell => 0 <= cell.Point).Union(Calc.Field.Where(cell => cell.Point <= 0)).Count());
Console.WriteLine(Calc.Field.Where(cell => 0 <= cell.Point).Except(Calc.Field.Where(cell => cell.Point <= 1)).Count());
Console.WriteLine(Calc.Field.Where(cell => 0 <= cell.Point).Intersect(Calc.Field.Where(cell => cell.Point <= 0)).Count());
```

```実行結果
88
34
14
```

ちなみに、下のソースコードでも全く同じ結果が得られます。

```cs
Console.WriteLine(Calc.Field.Count());
Console.WriteLine(Calc.Field.Where(cell => 2 <= cell.Point).Count());
Console.WriteLine(Calc.Field.Where(cell => cell.Point == 0).Count());
```

## ソート

|メソッド名|機能|
|:-:|:-:|
|`OrderBy`|昇順にソートしたシーケンスを返します。|
|`OrderByDescending`|降順にソートしたシーケンスを返します。|
|`ThenBy`|ソートしたシーケンスに対し、キーが等しい要素同士を昇順にソートしたシーケンスを返します。|
|`ThenByDescending`|ソートしたシーケンスに対し、キーが等しい要素同士を降順にソートしたシーケンスを返します。|
|`Reverse`|逆順にソートしたシーケンスを返します。|

```cs
var source = Calc.Field.Skip(5).Take(5);

Console.WriteLine("[OrderBy]");
var list = source.OrderBy(cell => cell.Point);
foreach (var cell in list)
    Console.WriteLine(cell);

Console.WriteLine("[OrderByDescending]"); // Visualizer 9.0 で実装予定
list = source.OrderByDescending(cell => cell.Coordinate);
foreach (var cell in list)
    Console.WriteLine(cell);

Console.WriteLine("[Reverse]");
var list2 = source.Reverse();
foreach (var cell in list2)
    Console.WriteLine(cell);
```

```実行結果
[OrderBy]
{Point:-2, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 7}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 5}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 6}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 0}}
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {1, 1}}
[OrderByDescending]
{Point:-2, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 7}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 6}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 5}}
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {1, 1}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 0}}
[Reverse]
{Point:3, IsTileOn: {True, False}, IsEnclosed: {False, False}, Coordinate: {1, 1}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {1, 0}}
{Point:-2, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 7}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 6}}
{Point:1, IsTileOn: {False, False}, IsEnclosed: {False, False}, Coordinate: {0, 5}}
```

`Cell.Coordinate` は Visualizer 8.0では、`IComparable<Coordinate>`がまだ実装されていないので、例外を投げられてしまうので、
`list = Calc.Field.Skip(5).Take(5).OrderByDescending(cell => cell.Coordinate);`みたいなことはできません。

## 結合

|メソッド名|機能|
|:-:|:-:|
|`Join`|内部結合を行ったシーケンスを返します。|
|`GroupJoin`|左外部結合を行って指定のキーでグループ化します。その "キーとグループ" のシーケンスを返します。|
|`Concat`|２つのシーケンスを連結します。<br>（Unionは同じ要素を一つにまとめますが、Concatは元の要素をすべて返します。）|
|`DefaultIfEmpty`|シーケンスを返します。シーケンスが空なら、規定値もしくは任意の要素を返します。|
|`Zip`|指定した関数で、2つのシーケンスを1つのシーケンスにマージします。|

## 変換

|メソッド名|機能|
|:-:|:-:|
|`OfType`|各要素を指定した型に変換します。<br>キャストできない要素は除外します。|
|`Cast`|各要素を指定した型に変換します。<br>キャストできない要素が含まれていた場合、例外をスローします。|
|`ToArray`|配列を作成します。|
|`ToDictionary`|連想配列(ディクショナリ)を作成します。|
|`ToList`|リストを生成します。|
|`ToLookup`|キーコレクション*1を生成します。|
|`AsEnume`|`IEnumerable<T>`を返します。*2|

*1：1対多のディクショナリ。例えば、～.ToLookup()["hoge"] と実行すると、"hoge" に紐付く要素の集合（IEnumerable）が返ってきます。  
*2：IEnumerable と同じ名前のメソッドがクラス内に定義されている場合に使います。そのままだと、クラス内のメソッドが優先的に選択されて、IEnumerable の拡張メソッドが呼びだせないためです。

## Example

### ポイントが正の数のマスのリストがほしい

```cs
var list = Calc.Field.Where(cell => 0 < cell.Point);
```

### ポイントが0のマスのリストがほしい

```cs
var list = Calc.Field.Where(cell => 0 == cell.Point);
```

### ポイントが負の数のマスのリストがほしい

```cs
var list = Calc.Field.Where(cell => 0 > cell.Point);
```

### フィールドの左半分のマスだけのリストがほしい

```cs
var list = Calc.Field.Where(cell => cell.Coordinate.X <= Calc.Field.Width / 2); // 中央のマスを含む
var list2 = Calc.Field.Where(cell => cell.Coordinate.X < Calc.Field.Width / 2); // 中央のマスを含まない
```

### あるチームの一人目のエージェントの周りの1マスのリストがほしい

```cs
var list =
    from cell in Calc.Field
    where cell.Coordinate.ChebyshevDistance(Calc.Agents[Team, AgentNumber.One].Position) == 1
    select cell;
```

## 参考文献

[LINQ - C# によるプログラミング入門 | ++C++; // 未確認飛行 C](https://ufcpp.net/study/csharp/sp3_linq.html)  
[LINQの拡張メソッド一覧と、ほぼ全部のサンプルを作ってみました。](http://d.hatena.ne.jp/chiheisen/20111031/1320068429)