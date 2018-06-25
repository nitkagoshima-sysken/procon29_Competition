using System.Drawing;

namespace Procon29_Visualizer
{
    /// <summary>
    /// チームのデザインを表現します。
    /// </summary>
    public class TeamDesign
    {
        private string name;
        private Color agentColor;
        private Color areaColor;

        /// <summary>
        /// チームの名前を設定または取得します。
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// エージェントの色を設定または取得します。
        /// </summary>
        public Color AgentColor { get => agentColor; set => agentColor = value; }

        /// <summary>
        /// 領域の色を設定または取得します。
        /// </summary>
        public Color AreaColor { get => areaColor; set => areaColor = value; }

        /// <summary>
        /// TeamDesignを初期化します。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="agentColor"></param>
        /// <param name="areaColor"></param>
        public TeamDesign(string name, Color agentColor, Color areaColor)
        {
            Name = name;
            AgentColor = agentColor;
            AreaColor = areaColor;
        }
    }
}
