using System;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 座標の拡張メソッドを定義するためのクラスです。
    /// </summary>
    public static class CoordinateExtension
    {
        /// <summary>
        /// 二点間のチェビシェフ距離を求めます。
        /// </summary>
        /// <param name="c1">対象となる点</param>
        /// <param name="c2">対象となる点</param>
        /// <returns></returns>
        public static int ChebyshevDistance(this Coordinate c1, Coordinate c2) => Math.Max(Math.Abs(c1.X - c2.X), Math.Abs(c1.Y - c2.Y));

        /// <summary>
        /// マウスの座標からマスの座標に変換します。
        /// </summary>
        /// <param name="mouse">マウスの座標</param>
        /// <param name="pictureBox">対象のピクチャーボックス</param>
        /// <param name="field">対象のフィールド</param>
        /// <returns></returns>
        public static Coordinate ToCellCordinate(this Coordinate mouse, PictureBox pictureBox, Field field)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / field.Width;
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / field.Height;
            return new Coordinate(
                x: mouse.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: mouse.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
        }
    }
}
