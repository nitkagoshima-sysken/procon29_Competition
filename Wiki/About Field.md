# Fieldについて

競技フィールドを表します

## プロパティ

|型|名前|説明|
|:-:|:-:|:-:|
|`int`|`Width`|フィールドの幅を取得します|
|`int`|`Height`|フィールドの高さを取得します|
|`Cell`|`this[x, y]`|指定した座標のマスを取得します|
|`Cell`|`this[coordinate]`|指定した座標のマスを取得します|
|`bool`|`IsVerticallySymmetrical`|上下対称なら真、<br>そうでなければ偽が返ってきます|
|`bool`|`IsHorizontallySymmetrical`|左右対称なら真、<br>そうでなければ偽が返ってきます|

## メソッド

|型|名前|説明|
|:-:|:-:|:-:|
|`Coordinate`|`FlipVertical`|フィールド上で上下反転したときの<br>マスの座標を取得します|
|`Coordinate`|`FlipHorizontal`|フィールド上で左右反転したときの<br>マスの座標を取得します|
|`Coordinate`|`FlipHorizontalAndVertical`|フィールド上で上下左右反転<br>したときのマスの座標を取得します|

## 関連記事

FieldExpansion

## 更新情報

最終更新時のVisualizerバージョンは **8.0**