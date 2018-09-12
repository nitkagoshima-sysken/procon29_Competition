using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// フルーツフェアリーたちを表します。
    /// </summary>
    public class FruitFairies : IEnumerable<FruitFairy>
    {
        FruitFairy[,] Array { get; set; } = new FruitFairy[Enum.GetValues(typeof(Team)).Length, Enum.GetValues(typeof(AgentNumber)).Length];

        /// <summary>
        /// 列挙します。
        /// </summary>
        /// <returns>列挙されたエージェント</returns>
        public IEnumerator<FruitFairy> GetEnumerator()
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
            foreach (FruitFairy item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 列挙します
        /// </summary>
        /// <returns>列挙されたセル</returns>
        IEnumerator<FruitFairy> IEnumerable<FruitFairy>.GetEnumerator()
        {
            foreach (FruitFairy item in Array)
            {
                yield return item;
            }
        }

        /// <summary>
        /// FruitFairiesを初期化します。
        /// </summary>
        public FruitFairies()
        {
            Array[0, 0] = new FruitFairy("Strawberry", Team.A, AgentNumber.One);
            Array[0, 1] = new FruitFairy("Apple", Team.A, AgentNumber.Two);
            Array[1, 0] = new FruitFairy("Kiwi", Team.B, AgentNumber.One);
            Array[1, 1] = new FruitFairy("Muscat", Team.B, AgentNumber.Two);
        }

        /// <summary>
        /// フルーツフェアリーを取得または設定します
        /// </summary>
        /// <param name="team">対象となる所属チーム</param>
        /// <param name="agent">対象となるエージェント番号</param>
        /// <returns></returns>
        public FruitFairy this[Team team, AgentNumber agent]
        {
            set { Array[(int)team, (int)agent] = value; }
            get { return Array[(int)team, (int)agent]; }
        }

        /// <summary>
        /// フルーツフェアリーたちを取得または設定します
        /// </summary>
        /// <param name="team">対象となる所属チーム</param>
        /// <returns></returns>
        public List<FruitFairy> this[Team team]
        {
            get
            {
                return new List<FruitFairy> {
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
        }
    }
}
