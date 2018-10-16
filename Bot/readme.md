# Procon29 Bot 9.54.1

## What's new (ver. 9.54.1)

### `Bot.Simulate(AgentsActivityData)`のコード最適化（SeitaHigashi氏によるデバッグ）

### `Bot.Simulate(Team, AgentActivityData[])`のコード最適化（SeitaHigashi氏によるデバッグ）

### `Bot.Simulate(Team, AgentNumber, AgentActivityData)`のコード最適化（SeitaHigashi氏によるデバッグ）

## What's new

### ver. 0.7.0

- ボットを作れるようになった

### ver. 1.7.1

- `SucceededInRemoveingOurTile`が`SucceededInRemovingOurTile`に変更された  
- `SucceededInRemoveingOpponentTile`が`SucceededInRemovingOpponentTile`に変更された
- `FailedInMovingByUnkownError`がASC992に変更された
- `FailedInRemovingOurTileByUnkownError`がASC993に変更された
- `FailedInRemovingOpponentTileByUnkownError`がASC994に変更された
- `FailedInMovingByBeingNotChebyshevNeighborhood`が`FailedInMovingByBeingNotMooreNeighborhood`に変更された  
- `FailedInRemovingOurTileByBeingNotChebyshevNeighborhood`が`FailedInRemovingOurTileByBeingNotMooreNeighborhood`に変更された  
- `FailedInRemovingOpponentTileByBeingNotChebyshevNeighborhood`が`FailedInRemovingOpponentTileByBeingNotMooreNeighborhood`に変更された  

### ver. 2.8.0

- Visualizer 8.0 に対応した

### ver. 3.10.0

- Visualizer 10.0 に対応した

### ver. 4.17.1

- Visualizer 17.1 に対応した

### ver. 5.25.0

- `Team`が`OurTeam`に変更された
- readmeの微調整

### ver. 6.32.0

- Simulate内のAgentActivityDataをDeepCloneするように変更

### ver. 7.34.0

- ボットがVisualizerのコンソール画面にログを残せるようになった

### ver. 8.35.0

- 自動でログを表示する機能を廃止した

### ver. 9.54.1

- `Bot.Simulate(AgentsActivityData)`のコード最適化（SeitaHigashi氏によるデバッグ）
- `Bot.Simulate(Team, AgentActivityData[])`のコード最適化（SeitaHigashi氏によるデバッグ）
- `Bot.Simulate(Team, AgentNumber, AgentActivityData)`のコード最適化（SeitaHigashi氏によるデバッグ）