namespace TegetegeBot
{
    /// <summary>
    /// 処理時間のデータを表します。
    /// </summary>
    public struct TimeData
    {
        /// <summary>
        /// 処理内容を設定または取得します。
        /// </summary>
        public string InspectionItem { get; set; }

        /// <summary>
        /// 処理時間を設定または取得します。
        /// </summary>
        public long ProcessingTime { get; set; }

        /// <summary>
        /// 処理時間のデータを初期化します。
        /// </summary>
        /// <param name="inspectionItem">処理内容を初期化します。</param>
        /// <param name="processingTime">処理時間を初期化します。</param>
        public TimeData(string inspectionItem, long processingTime)
        {
            InspectionItem = inspectionItem;
            ProcessingTime = processingTime;
        }
    }
}
