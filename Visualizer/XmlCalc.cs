using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// CalcをXMLで管理する際に使用されるクラスです。
    /// </summary>
    [XmlInclude(typeof(Agent))]
    [XmlInclude(typeof(AgentActivityData))]
    [Serializable]
    [XmlRoot("Calc")]
    public class XmlCalc
    {
        /// <summary>
        /// ターンを設定または取得します。
        /// </summary>
        public int Turn { get; set; }

        /// <summary>
        /// ターンの終わりを設定または取得します。
        /// </summary>
        public int MaxTurn { get; set; }

        /// <summary>
        /// フィールドの歴史を設定または取得します。
        /// </summary>
        [XmlArrayItem("TurnData")]
        public List<XmlTurnData> History { get; set; } = new List<XmlTurnData>();

        /// <summary>
        /// エージェントの略称を返します。
        /// </summary>
        public static string[,] ShortTeamAgentName => new string[2, 2] { { "Strawberry", "Apple", }, { "Kiwi", "Muscat", }, };

        /// <summary>
        /// BaseCalcを初期化をします。
        /// </summary>
        public XmlCalc()
        {

        }

        /// <summary>
        /// Calcを初期化します。
        /// </summary>
        /// <param name="calc"></param>
        public XmlCalc(Calc calc)
        {
            Turn = calc.Turn;
            MaxTurn = calc.MaxTurn;
            foreach (var item in calc.History)
            {
                History.Add(new XmlTurnData(item));
            }
        }
    }
}
