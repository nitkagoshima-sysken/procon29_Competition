namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントステータスコードの行動の種類を表します。
    /// </summary>
    public enum AgentStatusCodeAction
    {
        /// <summary>
        /// 移動を表します。
        /// </summary>
        Movement,
        /// <summary>
        /// 自分のチームからタイルを取り除くことを表します。
        /// </summary>
        RemovementOurTile,
        /// <summary>
        /// 相手のチームからタイルを取り除くことを表します。
        /// </summary>
        RemovementOpponentTile,
        /// <summary>
        /// その他の場合を表します。
        /// </summary>
        Other,
    }
}
