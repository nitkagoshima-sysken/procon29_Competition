using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// チームデザインを配列にします。
    /// </summary>
    public class TeamDesigns : IEnumerable<TeamDesign>
    {
        TeamDesign[] Array { get; set; } = new TeamDesign[Enum.GetValues(typeof(Team)).Length];

        /// <summary>
        /// 列挙します。
        /// </summary>
        /// <returns>列挙されたエージェント</returns>
        public IEnumerator<TeamDesign> GetEnumerator()
        {
            foreach (var item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (TeamDesign item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator<TeamDesign> IEnumerable<TeamDesign>.GetEnumerator()
        {
            foreach (TeamDesign item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// TeamDesignsを初期化します。
        /// </summary>
        public TeamDesigns()
        {
            Array[0] = new TeamDesign("Orange", Color.DarkOrange, Color.DarkOrange);
            Array[1] = new TeamDesign("Lime", Color.LimeGreen, Color.LimeGreen);
        }

        /// <summary>
        /// エージェントを取得または設定します
        /// </summary>
        /// <param name="team">対象となる所属チーム</param>
        /// <returns></returns>
        public TeamDesign this[Team team]
        {
            set { Array[(int)team] = value; }
            get { return Array[(int)team]; }
        }
    }
}
