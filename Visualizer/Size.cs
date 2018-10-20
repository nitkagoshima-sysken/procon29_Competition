using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// サイズを表します。
    /// </summary>
    struct Size
    {
        /// <summary>
        /// 高さを表します。
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 幅を表します。
        /// </summary>
        public int Width { get; set; }

        public Size(int height, int width) { Height = height; Width = width; }

        /// <summary>
        /// 人間が判読できる文字列に変換します
        /// </summary>
        /// <returns></returns>
        public override string ToString() => String.Format("{{Width:{0}, Height: {1}}}", Width, Height);
    }
}
