using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// フルーツフェアリーを表します。
    /// </summary>
    public class FruitFairy
    {
        /// <summary>
        /// 名前を取得または設定します
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属チームを取得または設定します
        /// </summary>
        public Team Team { get; set; }

        /// <summary>
        /// 所属するエージェントのエージェント番号を取得または設定します
        /// </summary>
        public AgentNumber AgentNumber { get; set; }

        /// <summary>
        /// フルーツフェアリーのいる番号を取得または設定します
        /// </summary>
        public Coordinate Position { get; set; }

        /// <summary>
        /// フルーツフェアリーを初期化します。
        /// </summary>
        public FruitFairy()
        {
        }

        /// <summary>
        /// フルーツフェアリーを初期化します。
        /// </summary>
        /// <param name="name">名前を設定します</param>
        /// <param name="team">チーム名を設定します</param>
        /// <param name="agentNumber">エージェント番号を設定します</param>
        public FruitFairy(string name, Team team, AgentNumber agentNumber)
        {
            Name = name;
            Team = team;
            AgentNumber = agentNumber;
        }        
    }
}
