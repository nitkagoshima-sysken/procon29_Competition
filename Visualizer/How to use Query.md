# クエリ式の使い方

```cs
var リスト
    from 変数名 in データテーブル
    where 条件
    select 取り出すもの;
```

基本的なクエリ式はこのように使います。  
さすがに、これでは分かりにくいので、具体的を。

例えば、  
フィールドに存在するマスの中から、敵のタイルが置いてあるマスだけを集めたリストがほしいとします。

これをクエリ式風で書くと

```日本語
from句: フィールドからひとつずつマス(cell)を取り出します。
where句: もし取り出したマス(cell)に敵のタイルが置いてあったら、
select句: そのマスをリストに追加します。
```

という感じになります。では実際にC#で書きましょう。

```cs
var list
    from cell in Calc.Field
    where cell.IsTileOn[OurTeam.Opponent()] == true
    select cell;
```

もし、「敵のタイルが置いてあるマス」のリストではなく、「敵のタイルが置いてあるマスの**座標**」のリストがほしいときは、

```cs
var list
    from cell in Calc.Field
    where cell.IsTileOn[OurTeam.Opponent()] == true
    select cell.Coordinate; // ここを「マスの座標」を取り出すように変更
```

としましょう。