﻿using System;
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
    public class AgentsActivityData : IEnumerable<AgentActivityData>
    {
        AgentActivityData[,] Array { get; set; } = new AgentActivityData[Enum.GetValues(typeof(Team)).Length, Enum.GetValues(typeof(AgentNumber)).Length];

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
        /// AgentActivityDatas を初期化します。
        /// </summary>
        public AgentsActivityData()
        {
            Array[0, 0] = new AgentActivityData();
            Array[0, 1] = new AgentActivityData();
            Array[1, 0] = new AgentActivityData();
            Array[1, 1] = new AgentActivityData();
        }

        /// <summary>
        /// AgentActivityDatas を初期化します。
        /// </summary>
        public AgentsActivityData(AgentsActivityData agentActivityDatas)
        {
            Array[0, 0] = new AgentActivityData(agentActivityDatas[Team.A, AgentNumber.One]);
            Array[0, 1] = new AgentActivityData(agentActivityDatas[Team.A, AgentNumber.Two]);
            Array[1, 0] = new AgentActivityData(agentActivityDatas[Team.B, AgentNumber.One]);
            Array[1, 1] = new AgentActivityData(agentActivityDatas[Team.B, AgentNumber.Two]);
        }

        /// <summary>
        /// エージェントアクティビティデータを取得または設定します
        /// </summary>
        /// <param name="team">対象となる所属チーム</param>
        /// <param name="agent">対象となるエージェント番号</param>
        /// <returns></returns>
        public AgentActivityData this[Team team, AgentNumber agent]
        {
            set { Array[(int)team, (int)agent] = value; }
            get { return Array[(int)team, (int)agent]; }
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