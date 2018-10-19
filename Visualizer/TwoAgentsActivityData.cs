using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントたちの行動データを表します
    /// </summary>
    [Serializable]
    public class TwoAgentsActivityData : IEnumerable<AgentActivityData>
    {
        AgentActivityData[] Array { get; set; } = new AgentActivityData[Enum.GetValues(typeof(AgentNumber)).Length];

        /// <summary>
        /// 列挙します。
        /// </summary>
        /// <returns>列挙されたエージェント</returns>
        public IEnumerator<AgentActivityData> GetEnumerator()
        {
            foreach (AgentActivityData item in Array)
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
            foreach (AgentActivityData item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator<AgentActivityData> IEnumerable<AgentActivityData>.GetEnumerator()
        {
            foreach (AgentActivityData item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// TwoAgentsActivityData を初期化します。
        /// </summary>
        public TwoAgentsActivityData()
        {
            Array[0] = new AgentActivityData();
            Array[1] = new AgentActivityData();
        }

        /// <summary>
        /// TwoAgentsActivityData を初期化します。
        /// </summary>
        public TwoAgentsActivityData(FourAgentsActivityData agentsActivityData)
        {
            Array[0] = new AgentActivityData(agentsActivityData[Team.A, AgentNumber.One]);
            Array[1] = new AgentActivityData(agentsActivityData[Team.A, AgentNumber.Two]);
        }

        /// <summary>
        /// TwoAgentsActivityData を初期化します。
        /// </summary>
        /// <param name="agentStatusCode">指定した AgentStatusCode で初期化します。</param>
        public TwoAgentsActivityData(AgentStatusCode agentStatusCode)
        {
            Array[0] = new AgentActivityData(agentStatusCode);
            Array[1] = new AgentActivityData(agentStatusCode);
        }

        /// <summary>
        /// エージェントアクティビティデータを取得または設定します
        /// </summary>
        /// <param name="agent">対象となるエージェント番号</param>
        /// <returns></returns>
        public AgentActivityData this[AgentNumber agent]
        {
            set { Array[(int)agent] = value; }
            get { return Array[(int)agent]; }
        }

        /// <summary>
        /// XML化するために宣言します
        /// </summary>
        /// <param name="obj"></param>
        public void Add(System.Object obj)
        {
        }
    }
}
