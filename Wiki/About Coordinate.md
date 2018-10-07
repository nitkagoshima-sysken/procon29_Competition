# Coordinateについて

フィールド上の座標を表します

## プロパティ

|型|名前|説明|
|:-:|:-:|:-:|
|`int`|`X`|横の座標を取得または設定します|
|`int`|`Y`|縦の座標を取得または設定します|

## 演算子

|型|説明|
|:-:|:-:|
|`Coordinate`|`Coordinate + Arrow`|
|`Coordinate`|`Coordinate + Coordinate`|
|`Coordinate`|`Coordinate - Coordinate`|
|`Coordinate`|`Coordinate == Coordinate`|
|`Coordinate`|`Coordinate != Coordinate`|
|`Coordinate`|(暗黙的型変換) `System.Drawing.Point`|
|`System.Drawing.Point`|(暗黙的型変換) `Coordinate`|

## 更新情報

最終更新時のVisualizerバージョンは **8.0**