using System;
using System.Collections;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// チームごとの真偽を表します。
    /// </summary>
    public class TeamBool : IEnumerable
    {
        bool[] Array { get; set; } = new bool[Enum.GetValues(typeof(Team)).Length];

        /// <summary>
        /// 列挙します。
        /// </summary>
        /// <returns>列挙されたエージェント</returns>
        public IEnumerator GetEnumerator()
        {
            foreach (var item in Array)
            {
                yield return item;
            }
        }

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

        /// <summary>
        /// 人間が判読できる文字列に変換します
        /// </summary>
        /// <returns></returns>
        public override string ToString() => String.Format("{{{0}, {1}}}", this[Team.A], this[Team.B]);
        
        /// <summary>
        /// XML化するために宣言します
        /// </summary>
        /// <param name="obj"></param>
        public void Add(System.Object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
