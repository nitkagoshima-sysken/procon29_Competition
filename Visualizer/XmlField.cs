using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public class XmlField : IEnumerable<XmlCell>
    {
        XmlCell[,] Cells { get; set; }

        /// <summary>
        /// Cellを列挙します。
        /// </summary>
        /// <returns>列挙されたセル</returns>
        public IEnumerable<XmlCell> GetEnumerator()
        {
            foreach (XmlCell item in Cells)
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
            foreach (XmlCell item in Cells)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator<XmlCell> IEnumerable<XmlCell>.GetEnumerator()
        {
            foreach (XmlCell item in Cells)
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
        public XmlField(int width, int height)
        {
            Cells = new XmlCell[width, height];
        }

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="field">コピーしたいフィールド</param>
        public XmlField(Field field)
        {
            Cells = new XmlCell[field.Width, field.Height];
            foreach (var item in field.GetEnumerator())
                this[item.Coordinate] = new XmlCell(item);
        }

        /// <summary>
        /// フィールドの任意のマスを取得または設定します。
        /// </summary>
        /// <param name="x">横の座標</param>
        /// <param name="y">縦の座標</param>
        /// <returns></returns>
        public XmlCell this[int x, int y]
        {
            set { Cells[x, y] = value; }
            get { return Cells[x, y]; }
        }

        /// <summary>
        /// フィールドの任意のマスを取得または設定します。
        /// </summary>
        /// <param name="coordinate">座標</param>
        /// <returns></returns>
        public XmlCell this[Coordinate coordinate]
        {
            set { Cells[coordinate.X, coordinate.Y] = value; }
            get { return Cells[coordinate.X, coordinate.Y]; }
        }
    }
}
