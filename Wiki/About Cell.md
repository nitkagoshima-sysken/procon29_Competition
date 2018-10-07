# Cellについて

競技フィールドにおける任意の1マスのデータ構造を表します

## プロパティ

|型|名前|意味|
|:-:|:-:|:-:|
|`int`|`Point`|そのマスにおける、ポイントを設定、または取得します|
|`TeamBool`|`IsTileOn`|そのマスにタイルが置かれているかを表します|
|`TeamBool`|`IsEnclosed`|そのマスがタイルに囲まれているかを表します|
|`Coordinate`|`Coordinate`|そのマスがフィールドのどこにあるかを表します|

## 使い方や風習

```cs
System.Console.WriteLine(cell.Point); // 表示結果: 12
System.Console.WriteLine(cell.IsTileOn[Team.A]); // 表示結果: True
System.Console.WriteLine(cell.IsTileOn[Team.B]); // 表示結果: False
System.Console.WriteLine(cell.IsEnclosed[Team.A]); // 表示結果: False
System.Console.WriteLine(cell.IsEnclosed[Team.B]); // 表示結果: True
System.Console.WriteLine(cell.Coordinate); // 表示結果: ( 1, 2)
```

例えば、上の`cell`というマスは、  
「ポイントが12点」  
「チームAのタイルが置かれいる」  
「チームBのタイルが置かれいない」  
「チームAのタイルに囲まれていない」  
「チームBにタイルに囲まれている」  
「左から1マス目、上から2マス目のところにある」  
ということがわかります。

## 更新情報

最終更新時のVisualizerバージョンは **8.0**