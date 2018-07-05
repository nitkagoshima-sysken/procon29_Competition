using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary> 
    /// 方向を表します。 
    /// </summary> 
    public enum Arrow
    {
        /// <summary> 
        /// 上を表します。 
        /// </summary> 
        Up,
        /// <summary> 
        /// 右上を表します。 
        /// </summary> 
        UpRight,
        /// <summary> 
        /// 右を表します。 
        /// </summary> 
        Right,
        /// <summary> 
        /// 右下を表します 
        /// </summary> 
        DownRight,
        /// <summary> 
        /// 下を表します。 
        /// </summary> 
        Down,
        /// <summary> 
        /// 左下を示します。 
        /// </summary> 
        DownLeft,
        /// <summary> 
        /// 左を示します。 
        /// </summary> 
        Left,
        /// <summary> 
        /// 左上を示します。 
        /// </summary> 
        UpLeft,
    }
}
