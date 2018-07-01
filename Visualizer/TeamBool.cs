using System;

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
        /// TeamBoolを設定します
        /// </summary>
        /// <param name="teamBool">コピーするTeamBool</param>
        public TeamBool(TeamBool teamBool)
        {
            this[Team.A] = teamBool[Team.A];
            this[Team.B] = teamBool[Team.B];
        }

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
