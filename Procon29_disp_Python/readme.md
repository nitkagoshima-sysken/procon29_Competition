# PPAP -Procon29 Python Application Project-
## Need library
    numpy, pillow, joblib, pyzbar
    多分これくらい

## PPAP
    本体。学習済みデータは基本的にgene/params100.pklを読み込む。

## HLGSCore
    GUIに対応している場合はLearn.pyを実行してもらえれば十分。
    CUIだけで動かしたい場合はhglscore_cui.pyを実行する。
    Field Dataは外部のファイルに一行ずつ記述する。
    また、起動時に引数'-n'を指定することでField Dataが存在するディレクトリから読み込む。

## HGLS
    GUIで起動するが、計算中は応答なしになるので注意。
    HGLSCoreのみで起動を推奨