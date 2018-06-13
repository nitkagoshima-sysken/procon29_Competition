using System.Drawing;

namespace procon29_disp
{
    class TeamDesign
    {
        private string name;
        private Color agentColor, areaColor;

        public string Name { get => name; set => name = value; }
        public Color AgentColor { get => agentColor; set => agentColor = value; }
        public Color AreaColor { get => areaColor; set => areaColor = value; }

        public TeamDesign(string name, Color agentColor, Color areaColor)
        {
            Name = name;
            AgentColor = agentColor;
            AreaColor = areaColor;
        }
    }
}
