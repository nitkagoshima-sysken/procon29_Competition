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
    [Serializable]
    public struct Coordinate : IComparable<Coordinate>
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
        /// <param name="x">x軸を表します。</param>
        /// <param name="y">y軸を表します。</param>
        public Coordinate(int x, int y) { X = x; Y = y; }

        /// <summary>
        /// Coordinateを初期化します。
        /// </summary>
        /// <param name="coordinate">座標を表します。</param>
        public Coordinate(Coordinate coordinate) { X = coordinate.X; Y = coordinate.Y; }

        /// <summary>
        /// 座標に座標を加算します。
        /// </summary>
        /// <param name="left">左辺値の座標</param>
        /// <param name="right">右辺値の座標</param>
        /// <returns></returns>
        public static Coordinate operator +(Coordinate left, Coordinate right) => new Coordinate(left.X + right.X, left.Y + right.Y);

        /// <summary>
        /// 座標と座標の差を求めます。
        /// </summary>
        /// <param name="left">左辺値の座標</param>
        /// <param name="right">右辺値の座標</param>
        /// <returns></returns>
        public static Coordinate operator -(Coordinate left, Coordinate right) => new Coordinate(left.X - right.X, left.Y - right.Y);

        /// <summary>
        /// 座標に方向を加算します。
        /// </summary>
        /// <param name="left">座標</param>
        /// <param name="right">Coordinate に変換します</param>
        /// <returns></returns>
        public static Coordinate operator +(Coordinate left, Arrow right)
        {
            switch (right)
            {
                case Arrow.Up:
                    return new Coordinate(left.X, left.Y - 1);
                case Arrow.UpRight:
                    return new Coordinate(left.X + 1, left.Y - 1);
                case Arrow.Right:
                    return new Coordinate(left.X + 1, left.Y);
                case Arrow.DownRight:
                    return new Coordinate(left.X + 1, left.Y + 1);
                case Arrow.Down:
                    return new Coordinate(left.X, left.Y + 1);
                case Arrow.DownLeft:
                    return new Coordinate(left.X - 1, left.Y + 1);
                case Arrow.Left:
                    return new Coordinate(left.X - 1, left.Y);
                case Arrow.UpLeft:
                    return new Coordinate(left.X - 1, left.Y - 1);
                default:
                    return new Coordinate(left.X, left.Y);
            }
        }

        /// <summary>
        /// すべての数値型と列挙型で "より小さい" 関係演算子 が定義されます。
        /// </summary>
        /// <param name="left">左辺値の座標</param>
        /// <param name="right">右辺値の座標</param>
        /// <returns></returns>
        public static bool operator <(Coordinate left, Coordinate right) => (left.X < right.X) || (left.Y < right.Y);

        /// <summary>
        /// すべての数値型と列挙型で "より大きい" 関係演算子 が定義されます。
        /// </summary>
        /// <param name="left">左辺値の座標</param>
        /// <param name="right">右辺値の座標</param>
        /// <returns></returns>
        public static bool operator >(Coordinate left, Coordinate right) => (left.X > right.X) || (left.Y > right.Y);

        /// <summary>
        /// 座標が等しいか判定します。
        /// </summary>
        /// <param name="left">左辺値の座標</param>
        /// <param name="right">右辺値の座標</param>
        /// <returns></returns>
        public static bool operator ==(Coordinate left, Coordinate right) => (left.X == right.X) && (left.Y == right.Y);

        /// <summary>
        /// 座標が等しくないか判定します
        /// </summary>
        /// <param name="left">左辺値の座標</param>
        /// <param name="right">右辺値の座標</param>
        /// <returns></returns>
        public static bool operator !=(Coordinate left, Coordinate right) => (left.X != right.X) || (left.Y != right.Y);

        /// <summary>
        /// Coordinate を System.Drawing.Point に暗黙的に変換します。
        /// </summary>
        /// <param name="point"></param>
        public static implicit operator System.Drawing.Point(Coordinate point) => new System.Drawing.Point(point.X, point.Y);

        /// <summary>
        /// System.Drawing.Point を Coordinate に暗黙的に変換します。
        /// </summary>
        /// <param name="point"></param>
        public static implicit operator Coordinate(System.Drawing.Point point) => new Coordinate(point.X, point.Y);

        /// <summary>
        /// 人間が判読できる文字列に変換します
        /// </summary>
        /// <returns></returns>
        public override string ToString() => String.Format("{{{0}, {1}}}", X, Y);

        /// <summary>
        /// このインスタンスと指定したオブジェクトが等しいかどうかを示します。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => base.Equals(obj);

        /// <summary>
        /// このインスタンスのハッシュコードを返します。
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// このインスタンスと指定した Object とを比較し、並べ替え順序において、このインスタンスの位置が指定した Object の前、後ろ、または同じのいずれであるかを示します。
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Coordinate other)
        {
            if (this > other) return 1;
            else if (this < other) return -1;
            else return 0;
        }        
    }
}
