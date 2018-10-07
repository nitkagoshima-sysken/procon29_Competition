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
    }
}
