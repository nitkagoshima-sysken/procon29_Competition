using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// Teamの拡張メソッドを定義するためのクラスです。
    /// </summary>
    public static class TeamExtension
    {
        /// <summary>
        /// 指定したチームの相手のチームを表します。
        /// </summary>
        /// <param name="team">指定したチーム</param>
        /// <returns>相手のチーム</returns>
        public static Team Opponent(this Team team) => (team == Team.A) ? Team.B : Team.A;
    }
}
