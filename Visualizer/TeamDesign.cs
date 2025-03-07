﻿using System.Drawing;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// チームのデザインを表現します。
    /// </summary>
    public class TeamDesign
    {
        /// <summary>
        /// チームの名前を設定または取得します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// エージェントの色を設定または取得します。
        /// </summary>
        public Color AgentColor { get; set; }

        /// <summary>
        /// 領域の色を設定または取得します。
        /// </summary>
        public Color AreaColor { get; set; }

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
