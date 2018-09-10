using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    [Serializable]
    [XmlRoot("TurnData")]
    public class XmlTurnData
    {
        /// <summary>
        /// フィールドの高さを設定または取得します。
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// フィールドの幅を設定または取得します。
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// フィールドを設定または取得します
        /// </summary>
        [XmlArrayItem("Cell")]
        public XmlCell[] Field { get; set; }

        /// <summary>
        /// エージェントの位置を設定または取得します
        /// </summary>
        public Agent[] Agents { get; set; }

        /// <summary>
        /// エージェントの行動データを設定または取得します
        /// </summary>
        public AgentsActivityData AgentActivityDatas { get; set; }

        /// <summary>
        /// XML化するために宣言します
        /// </summary>
        public XmlTurnData()
        {
        }

        public XmlTurnData(TurnData turnData)
        {
            Agents = new Agent[4];
            for (int i = 0; i < 4; i++)
            {
                Agents[i] = turnData.Agents[(Team)(i/2), (AgentNumber)(i%2)];
            }
            AgentActivityDatas = turnData.AgentActivityDatas;
            Height = turnData.Field.Height;
            Width = turnData.Field.Width;
            var max = Height * Width;
            Field = new XmlCell[max];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Field[y * Width + x] = new XmlCell(turnData.Field[x, y]);
                }
            }
        }
    }
}
