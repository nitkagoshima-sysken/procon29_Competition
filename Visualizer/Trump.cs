using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// トランプを表します。
    /// </summary>
    class Trump
    {
        /// <summary>
        /// トランプのマークを設定または取得します。
        /// </summary>
        public TrumpMark TrumpMark { get; set; }
        
        /// <summary>
        /// トランプのナンバーを設定または取得します。
        /// </summary>
        public TrumpNumber TrumpNumber { get; set; }
    }
}
