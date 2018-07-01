using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    class TeamArray
    {
        bool[] Array { get; set; } = new bool[Enum.GetValues(typeof(Team)).Length];

        public TeamArray() { }

        public bool this[Team team, Agent agent]
        {
            set { Array[(int)team] = value; }
            get { return Array[(int)team]; }
        }
    }
}
