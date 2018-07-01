using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// フィールド上の座標を表します。
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// 横の座標を取得または設定します。
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 縦の座標を取得または設定します。
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Coordinateを初期化します。
        /// </summary>
        public Coordinate() { X = 0; Y = 0; }

        /// <summary>
        /// Coordinateを初期化します。
        /// </summary>
        /// <param name="x">x軸を表します。</param>
        /// <param name="y">y軸を表します。</param>
        public Coordinate(int x, int y) { X = x; Y = y; }


        /// <summary>
        /// 座標に座標を加算します。
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Coordinate operator +(Coordinate c1, Coordinate c2) => new Coordinate(c1.X + c2.X, c1.Y + c2.Y);

        /// <summary>
        /// 座標と座標の差を求めます。
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Coordinate operator -(Coordinate c1, Coordinate c2) => new Coordinate(c1.X - c2.X, c1.Y - c2.Y);

        /// <summary>
        /// 座標に方向を加算します。
        /// </summary>
        /// <param name="c">座標</param>
        /// <param name="a">Coordinate に変換します</param>
        /// <returns></returns>
        public static Coordinate operator +(Coordinate c, Arrow a)
        {
            switch (a)
            {
                case Arrow.Up:
                    return new Coordinate(c.X, c.Y - 1);
                case Arrow.UpRight:
                    return new Coordinate(c.X + 1, c.Y - 1);
                case Arrow.Right:
                    return new Coordinate(c.X + 1, c.Y);
                case Arrow.DownRight:
                    return new Coordinate(c.X + 1, c.Y + 1);
                case Arrow.Down:
                    return new Coordinate(c.X, c.Y + 1);
                case Arrow.DownLeft:
                    return new Coordinate(c.X - 1, c.Y + 1);
                case Arrow.Left:
                    return new Coordinate(c.X - 1, c.Y);
                case Arrow.UpLeft:
                    return new Coordinate(c.X - 1, c.Y - 1);
                default:
                    return new Coordinate(c.X, c.Y);
            }
        }

        /// <summary>
        /// Coordinate を System.Drawing.Point に暗黙的に変換します。
        /// </summary>
        /// <param name="point"></param>
        public static implicit operator System.Drawing.Point(Coordinate point) { return new System.Drawing.Point(point.X, point.Y); }

        /// <summary>
        /// System.Drawing.Point を Coordinate に暗黙的に変換します。
        /// </summary>
        /// <param name="point"></param>
        public static implicit operator Coordinate(System.Drawing.Point point) { return new Coordinate(point.X, point.Y); }

        /// <summary>
        /// 人間が判読できる文字列に変換します
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("({0, 2}, {1, 2})", X, Y);
        }
    }
}
