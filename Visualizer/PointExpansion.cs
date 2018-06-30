using System;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// Point型の拡張メソッドを定義するためのクラスです。
    /// </summary>
    public static class PointExpansion
    {
        /// <summary>
        /// 二点間のチェビシェフ距離を求めます。
        /// </summary>
        /// <param name="p1">対象となる点</param>
        /// <param name="p2">対象となる点</param>
        /// <returns></returns>
        public static int ChebyshevDistance(this Coordinate p1, Coordinate p2) => Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
    }
}
