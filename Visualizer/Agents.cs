using System;
using System.Collections;
using System.Collections.Generic;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// エージェントたちを表します。
    /// </summary>
    public class Agents : IEnumerable<Agent>
    {
        Agent[,] Array { get; set; } = new Agent[Enum.GetValues(typeof(Team)).Length, Enum.GetValues(typeof(AgentNumber)).Length];

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Agent item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator<Agent> IEnumerable<Agent>.GetEnumerator()
        {
            foreach (Agent item in Array)
            {
                yield return item;
            }
        }

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
        /// Agentsを初期化します。
        /// </summary>
        public Agents()
        {
            Array[0, 0] = new Agent("Strawberry", Team.A, AgentNumber.One);
            Array[0, 1] = new Agent("Apple", Team.A, AgentNumber.Two);
            Array[1, 0] = new Agent("Kiwi", Team.B, AgentNumber.One);
            Array[1, 1] = new Agent("Muscat", Team.B, AgentNumber.Two);
        }

        /// <summary>
        /// Agentsを初期化します。
        /// </summary>
        public Agents(Agents agents)
        {
            Array[0, 0] = new Agent(agents[Team.A, AgentNumber.One]);
            Array[0, 1] = new Agent(agents[Team.A, AgentNumber.Two]);
            Array[1, 0] = new Agent(agents[Team.B, AgentNumber.One]);
            Array[1, 1] = new Agent(agents[Team.B, AgentNumber.Two]);
        }

        /// <summary>
        /// エージェントを取得または設定します
        /// </summary>
        /// <param name="team">対象となる所属チーム</param>
        /// <param name="agent">対象となるエージェント番号</param>
        /// <returns></returns>
        public Agent this[Team team, AgentNumber agent]
        {
            set { Array[(int)team, (int)agent] = value; }
            get { return Array[(int)team, (int)agent]; }
        }

        /// <summary>
        /// エージェントたちを取得または設定します
        /// </summary>
        /// <param name="team">対象となる所属チーム</param>
        /// <returns></returns>
        public List<Agent> this[Team team]
        {
            get
            {
                return new List<Agent> {
                    this[team, AgentNumber.One],
                    this[team, AgentNumber.Two]
                };
            }
        }

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
