# Program Contest Competition #29

## Bot

Visualizerにボットを入れることで、多種多様なアルゴリズムでエージェントを動かすことができます。  
ボットを製作したい方はWikiへ行くことをおすすめします。

## FieldDataGenerator

24時間365日対応でいつでもお好きな条件でフィールドをカスタマイズできます。

## NettaBot

SeitaHigashi氏によるボット。

## Procon29_Algo

C++を使ってVisualizerと通信してエージェントの行動アルゴリズムを書くプロジェクトでしたが、  
現在は廃止されたプロジェクトです。

## Procon29_disp_Python

Pythonで試合を可視化します。

## QRcode_Reader

試合で使われるQRコードをいとも簡単に解析します。  
Pythonなのでソースコードもいとも簡単です。

### QRcode_Readerをビルドに必要なライブラリ

- pyzbarを `python3 -m pip install pyzbar` または `python -m pip install pyzbar` でダウンロード＆インストールする必要があります。
- PILを `python3 -m pip install pillow` または `python -m pip install pillow` でダウンロード＆インストールする必要があります。
- zbarを `brew install zbar`でダウンロード＆インストールする必要があります。 もしあなたのOSがmacOSじゃないのなら、 http://zbar.sourceforge.net/download.html からダウンロードしてくる必要があります。
- もし、実行ファイルを使いたいだけなら、上記3つをわざわざインストールする必要はありません。

### QRcode_Readerの使い方

- QRコードの写真をドロップ＆ドラッグするだけ！超簡単でしょ！！

## TegetegeBot

skytomo氏によるお手本ボット。  
ここで、ボットのソースコードをだいたいどのようにして書けばいいか見えてくる。

## Visualizer

C#で試合を可視化します。

### Visualizerの操作方法

 1. クリックで対象のエージェントを選択しよう。
 2. クリックで行きたい場所、またはタイルを取り除く場所を選択しよう。
 3. キミが操作可能なすべてのエージェントに対して上の操作を繰り返す。
 4. 最後にターンエンドを押す。

### Visualizerのショートカットキー一覧

現在(Visualizer 25.0)、謎のバグが発生して、エージェントの選択ができない状態です。  
`Ctrl`を使ったショートカットは使えるので、そこは安心してお使いしてください。

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
|`Ctrl`+`S`| 試合を保存する |
|`Ctrl`+`Y`| ターンをやり直す |
|`Ctrl`+`Z`| ターンを元に戻す |

## Wiki

Wikiのページなどを管理するディレクトリです。