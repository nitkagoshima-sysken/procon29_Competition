using System;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// Point型の拡張メソッドを定義するためのクラスです。
    /// </summary>
    public static class PointExtension
    {
        /// <summary>
        /// 二点間のチェビシェフ距離を求めます。
        /// </summary>
        /// <param name="c1">対象となる点</param>
        /// <param name="c2">対象となる点</param>
        /// <returns></returns>
        public static int ChebyshevDistance(this Coordinate c1, Coordinate c2) => Math.Max(Math.Abs(c1.X - c2.X), Math.Abs(c1.Y - c2.Y));
    }
}
