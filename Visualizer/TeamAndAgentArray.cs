using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    class TeamAndAgentArray
    {
        public bool[,] Array { get; set; } = new bool[Enum.GetValues(typeof(Team)).Length, Enum.GetValues(typeof(Agent)).Length];

        public TeamAndAgentArray() { }

        public bool this[Team team, Agent agent]
        {
            set { Array[(int)team, (int)agent] = value; }
            get { return Array[(int)team, (int)agent]; }
        }
    }
}
