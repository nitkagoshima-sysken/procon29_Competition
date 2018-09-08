namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// フィールドの拡張メソッドを定義するためのクラスです。
    /// </summary>
    public static class FieldExtension
    {
        /// <summary>
        /// フィールドの幅を取得します。
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int Width(this Cell[,] field) => field.GetLength(0);

        /// <summary>
        /// フィールドの高さを取得します。
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int Height(this Cell[,] field) => field.GetLength(1);

        /// <summary>
        /// フィールド上で上下反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="field">対象となるフィールド</param>
        /// <param name="point">対象となるマス</param>
        /// <returns></returns>
        public static Coordinate FlipVertical(this Cell[,] field, Coordinate point) => new Coordinate(point.X, field.Height() - 1 - point.Y);

        /// <summary>
        /// フィールド上で左右反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="field">対象となるフィールド</param>
        /// <param name="point">対象となるマス</param>
        /// <returns></returns>
        public static Coordinate FlipHorizontal(this Cell[,] field, Coordinate point) => new Coordinate(field.Width() - 1 - point.X, point.Y);

        /// <summary>
        /// フィールド上で上下左右反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="field">対称となるフィールド</param>
        /// <param name="point">対称となるマス</param>
        /// <returns></returns>
        public static Coordinate FlipHorizontalAndVertical(this Cell[,] field, Coordinate point) => new Coordinate(field.Width() - 1 - point.X, field.Height() - 1 - point.Y);

        /// <summary>
        /// 上下対称か判定します。
        /// </summary>
        /// <returns>上下対称なら真、そうでなければ偽が返ってきます。</returns>
        public static bool VerticallySymmetricalCheck(this Cell[,] field)
        {
            for (int x = 0; x < field.Width(); x++)
            {
                for (int y = 0; y < field.Height() / 2; y++)
                {
                    if (field[x, y].Point != field[x, (field.Height() - 1) - y].Point) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 左右対称か判定します。
        /// </summary>
        /// <returns>左右対称なら真、そうでなければ偽が返ってきます。</returns>
        public static bool HorizontallySymmetricalCheck(this Cell[,] field)
        {
            for (int x = 0; x < field.Width() / 2; x++)
            {
                for (int y = 0; y < field.Height(); y++)    
                {
                    if (field[x, y].Point != field[(field.Width() - 1) - x, y].Point) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// フィールド上のセルをPoint型で取得します。
        /// </summary>
        /// <param name="field">対称となるフィールド</param>
        /// <param name="point">対称となるマス</param>
        /// <returns></returns>
        public static Cell Take(this Cell[,] field, Coordinate point) => field[point.X, point.Y];

        /// <summary>
        /// 現在のobjectのディープコピーを行います。
        /// </summary>
        /// <returns>objectのディープコピー</returns>
        public static object DeepCopy(this Cell[,] field)
        {
            var cloned = new Cell[field.Width(), field.Height()];
            for (int x = 0; x < cloned.Width(); x++)
            {
                for (int y = 0; y < cloned.Height(); y++)
                {
                    cloned[x, y] = (Cell)field[x, y].DeepCopy();
                }
            }
            return cloned;
        }
    }
}
