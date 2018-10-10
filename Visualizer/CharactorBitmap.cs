using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 
    /// </summary>
    public class CharactorBitmap : IEnumerable<Bitmap>
    {
        Bitmap[,] Array { get; set; }

        /// <summary>
        /// 列挙します。
        /// </summary>
        /// <returns>列挙されたエージェント</returns>
        public IEnumerator<Bitmap> GetEnumerator()
        {
            foreach (var item in Array)
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
            foreach (Bitmap item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator<Bitmap> IEnumerable<Bitmap>.GetEnumerator()
        {
            foreach (Bitmap item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// CharactorBitmapを初期化します。
        /// </summary>
        public CharactorBitmap()
        {
        }

        /// <summary>
        /// CharactorBitmapを初期化します。
        /// </summary>
        /// <param name="n">配列の数</param>
        public CharactorBitmap(int n)
        {
            Array = new Bitmap[n, 2];
        }

        /// <summary>
        /// エージェントを取得または設定します
        /// </summary>
        /// <param name="n">番号</param>
        /// <param name="direction">向き</param>
        /// <returns></returns>
        public Bitmap this[int n, Direction direction]
        {
            set { Array[n, (int)direction] = value; }
            get { return Array[n, (int)direction]; }
        }

        /// <summary>
        /// XML化するために宣言します
        /// </summary>
        /// <param name="obj"></param>
        public void Add(System.Object obj)
        {
        }
    }
}
