using System;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// 1ターンのデータを表します
    /// </summary>
    [Serializable]
    public class TurnData
    {
        /// <summary>
        /// フィールドを設定または取得します
        /// </summary>
        public Field Field { get; set; }

        /// <summary>
        /// エージェントの位置を設定または取得します
        /// </summary>
        public Agents Agents { get; set; }

        /// <summary>
        /// エージェントの行動データを設定または取得します
        /// </summary>
        public AgentsActivityData AgentsActivityData { get; set; }

        /// <summary>
        /// TurnDataを初期化します。
        /// </summary>
        public TurnData(TurnData turnData)
        {
            Agents = new Agents(turnData.Agents);
            AgentsActivityData = new AgentsActivityData(turnData.AgentsActivityData);
            Field = new Field(turnData.Field.Width, turnData.Field.Height);
            for (int x = 0; x < Field.Width; x++)
            {
                for (int y = 0; y < Field.Height; y++)
                {
                    Field[x, y] = new Cell(turnData.Field[x, y]);
                }
            }
        }

        /// <summary>
        /// XML化するために宣言します
        /// </summary>
        /// <param name="xmlTurnData">対象のXmlTurnData</param>
        public TurnData(XmlTurnData xmlTurnData)
        {
            Agents = new Agents();
            for (int i = 0; i < 4; i++)
            {
                Agents[(Team)(i / 2), (AgentNumber)(i % 2)] = xmlTurnData.Agents[i];
            }
            AgentsActivityData = xmlTurnData.AgentActivityDatas;
            Field = new Field(xmlTurnData.Width, xmlTurnData.Height);
            for (int x = 0; x < xmlTurnData.Width; x++)
            {
                for (int y = 0; y < xmlTurnData.Height; y++)
                {
                    Field[x, y] = new Cell(xmlTurnData.Field[y * xmlTurnData.Width + x]);
                }
            }
        }

        /// <summary>
        /// 初期化します
        /// </summary>
        /// <param name="field">フィールドを表します</param>
        /// <param name="agents">エージェントたちを表します</param>
        /// <param name="agentActivityDatas">エージェントの行動データを表します</param>
        public TurnData(Field field, Agents agents, AgentsActivityData agentActivityDatas)
        {
            Field = field;
            Agents = agents;
            AgentsActivityData = agentActivityDatas;
        }

        /// <summary>
        /// 初期化します
        /// </summary>
        /// <param name="field">フィールドを表します</param>
        /// <param name="agents">エージェントたちを表します</param>
        public TurnData(Field field, Agents agents)
        {
            Field = field;
            Agents = agents;
            AgentsActivityData = new AgentsActivityData();
        }
    }
}
