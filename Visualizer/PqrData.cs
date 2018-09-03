using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// PQR形式のデータを表します。
    /// </summary>
    struct PqrData
    {
        /// <summary>
        /// フィールドの大きさを表します。
        /// </summary>
        internal Size Size { get; set; }
        /// <summary>
        /// フィールドを表します。
        /// </summary>
        public int[,] Fields { get; set; }
        /// <summary>
        /// 1人目のエージェントの位置を表します。
        /// </summary>
        public Coordinate One { get; set; }
        /// <summary>
        /// 2人目のエージェントの位置を表します。
        /// </summary>
        public Coordinate Two { get; set; }
    }
}
