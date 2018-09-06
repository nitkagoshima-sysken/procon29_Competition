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
        int height, width;

        /// <summary>
        /// 高さを表します。
        /// </summary>
        public int Height { get => height; set => height = value; }
        /// <summary>
        /// 幅を表します。
        /// </summary>
        public int Width { get => width; set => width = value; }
        public Size(int height, int width) { this.height = height; this.width = width; }

    }
}
