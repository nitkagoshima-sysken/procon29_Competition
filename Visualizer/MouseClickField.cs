using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// フィールドをマウスでクリックするときに反応するクラスです。
    /// </summary>
    public class MouseClickField
    {
        /// <summary>
        /// イベントを反映する位置を指定します。
        /// </summary>
        public Point Location { get; set; } = new Point(0, 0);

        /// <summary>
        /// イベントを反映するサイズを指定します。
        /// </summary>
        public System.Drawing.Size Size { get; set; }

        /// <summary>
        /// フィールドの高さを取得または設定します。
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// フィールドの幅を取得または設定します。
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 描画するピクチャーボックスを設定または取得します。
        /// </summary>
        public PictureBox PictureBox { get; set; }

        /// <summary>
        /// マウスでクリックした際に呼び出されます。
        /// </summary>
        /// <param name="coordinate"></param>
        public delegate void MouseClick(Coordinate coordinate);

        /// <summary>
        /// MouseClickField を初期化します。
        /// </summary>
        /// <param name="calc">フィールドの大きさを渡します。</param>
        /// <param name="pictureBox">反映するフォームを指定します。</param>
        public MouseClickField(Calc calc, PictureBox pictureBox)
        {
            Height = calc.Field.Height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseClickEvent(object sender, MouseEventArgs e)
        {

        }
    }
}
