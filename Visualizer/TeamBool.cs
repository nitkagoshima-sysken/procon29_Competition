using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// チームごとの真偽を表します。
    /// </summary>
    public class TeamBool
    {
        bool[] Array { get; set; } = new bool[Enum.GetValues(typeof(Team)).Length];

        /// <summary>
        /// TeamBoolを設定します
        /// </summary>
        public TeamBool() { }

        /// <summary>
        /// チームごとの真偽を表します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <returns></returns>
        public bool this[Team team]
        {
            set { Array[(int)team] = value; }
            get { return Array[(int)team]; }
        }
    }
}
