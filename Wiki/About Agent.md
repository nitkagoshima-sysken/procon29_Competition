# Agentについて

<img src="https://github.com/nitkagoshima-sysken/procon29_Competition/blob/skytomo/Wiki/About%20Agent.png?raw=true" width="640px">

## プロパティ

|型|名前|説明|
|:-:|:-:|:-:|
|`string`|`Name`|名前を取得または設定します|
|`Team`|`Team`|所属チームを取得または設定します|
|`AgentNumber`|`AgentNumber`|エージェント番号を取得または設定します|
|`Coordinate`|`Position`|エージェントのいる座標を取得または設定します|

## 使い方や風習

```cs
System.Console.WriteLine(agent.Name); // 表示結果: Muscat
System.Console.WriteLine(agent.Team); // 表示結果: Team.B
System.Console.WriteLine(agent.AgentNumber); // 表示結果: AgentNumber.Two
System.Console.WriteLine(agent.Position); // 表示結果: {9, 1}
```

例えば、上の`agent`というエージェントは、  
「ポイントが12点」  
「チームBに所属している」  
「エージェント番号は`Two`」  
「左から9マス目、上から1マス目のところにある」  
ということがわかります。

## 更新情報

最終更新時のVisualizerバージョンは **8.0**