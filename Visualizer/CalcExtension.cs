using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    static class CalcExtension
    {
        public static string FukumocchanAdapter(this Calc calc, Team ourteam)
        {
            var arg = string.Empty;
            for (int x = 0; x < calc.Field.Width; x++)
            {
                arg += "{";
                for (int y = 0; y < calc.Field.Height; y++)
                {
                    arg += calc.Field[x, y].Point.ToString() + ", ";
                }
                arg += "}, ";
            }
            arg += "\n";
            foreach (var agent in calc.Agents[ourteam])
            {
                arg += agent.Position + ", ";
            }
            arg += "\n";
            foreach (var agent in calc.Agents[ourteam.Opponent()])
            {
                arg += agent.Position + ", ";
            }
            arg += "\n";
            foreach (var coordinate in from cell in calc.Field where cell.IsTileOn[ourteam] select cell.Coordinate)
            {
                arg += coordinate.ToString() + ", ";
            }
            arg += "\n";
            foreach (var coordinate in from cell in calc.Field where cell.IsTileOn[ourteam.Opponent()] select cell.Coordinate)
            {
                arg += coordinate.ToString() + ", ";
            }
            arg += "\n";
            foreach (var agent in calc.Agents[ourteam])
            {
                foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                {
                    if (calc.Field.CellExist(agent.Position + arrow) && calc.Field.IsMovable(agent, arrow))
                    {
                        arg += (agent.Position + arrow).ToString() + ", ";
                    }
                }
            }
            arg += "\n";
            foreach (var agent in calc.Agents[ourteam.Opponent()])
            {
                foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                {
                    if (calc.Field.CellExist(agent.Position + arrow) && calc.Field.IsMovable(agent, arrow))
                    {
                        arg += (agent.Position + arrow).ToString() + ", ";
                    }
                }
            }
            return arg;
        }
    }
}
