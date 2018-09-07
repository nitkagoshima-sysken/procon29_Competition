using System;
using System.Collections;
using System.Collections.Generic;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 競技フィールドを表します。
    /// </summary>
    [Serializable]
    public class Field : IEnumerable<Cell>
    {
        Cell[,] Cells { get; set; }

        /// <summary>
        /// Cellを列挙します。
        /// </summary>
        /// <returns>列挙されたセル</returns>
        public IEnumerable<Cell> GetEnumerator()
        {
            foreach (Cell item in Cells)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Cell item in Cells)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator<Cell> IEnumerable<Cell>.GetEnumerator()
        {
            foreach (Cell item in Cells)
            {
                yield return item;
            }
        }
       
        /// <summary>
        /// フィールドの幅を取得します。
        /// </summary>
        public int Width => Cells.GetLength(0);

        /// <summary>
        /// フィールドの高さを取得します。
        /// </summary>
        public int Height => Cells.GetLength(1);

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="width">フィールドの幅</param>
        /// <param name="height">フィールドの高さ</param>
        public Field(int width, int height)
        {
            Cells = new Cell[width, height];
        }

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="field">コピーしたいフィールド</param>
        public Field(Field field)
        {
            Cells = new Cell[field.Width, field.Height];
            foreach (var item in field.GetEnumerator())
                this[item.Coordinate] = new Cell(item);
        }

        /// <summary>
        /// フィールドの任意のマスを取得または設定します。
        /// </summary>
        /// <param name="x">横の座標</param>
        /// <param name="y">縦の座標</param>
        /// <returns></returns>
        public Cell this[int x, int y]
        {
            set { Cells[x, y] = value; }
            get { return Cells[x, y]; }
        }

        /// <summary>
        /// フィールドの任意のマスを取得または設定します。
        /// </summary>
        /// <param name="coordinate">座標</param>
        /// <returns></returns>
        public Cell this[Coordinate coordinate]
        {
            set { Cells[coordinate.X, coordinate.Y] = value; }
            get { return Cells[coordinate.X, coordinate.Y]; }
        }

        /// <summary>
        /// フィールド上で上下反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="point">対象となるマス</param>
        /// <returns></returns>
        public Coordinate FlipVertical(Coordinate point) => new Coordinate(point.X, Height - 1 - point.Y);

        /// <summary>
        /// フィールド上で左右反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="point">対象となるマス</param>
        /// <returns></returns>
        public Coordinate FlipHorizontal(Coordinate point) => new Coordinate(Width - 1 - point.X, point.Y);

        /// <summary>
        /// フィールド上で上下左右反転したときのマスの座標を取得します。
        /// </summary>
        /// <param name="point">対称となるマス</param>
        /// <returns></returns>
        public Coordinate FlipHorizontalAndVertical(Coordinate point) => new Coordinate(Width - 1 - point.X, Height - 1 - point.Y);
        
        /// <summary>
        /// XML化するために宣言します
        /// </summary>
        /// <param name="obj"></param>
        public void Add(System.Object obj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 上下対称なら真、そうでなければ偽が返ってきます。
        /// </summary>
        public bool IsVerticallySymmetrical => Cells.VerticallySymmetricalCheck();

        /// <summary>
        /// 左右対称なら真、そうでなければ偽が返ってきます。
        /// </summary>
        public bool IsHorizontallySymmetrical => Cells.HorizontallySymmetricalCheck();        
    }
}
