using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.SatsumaimoBot
{
    public class SatsumaimoBot : KaraimoBot.KaraimoBot
    {
        public override AgentActivityData[] Answer()
        {
            var ret = base.Answer();
            foreach (var agent in Calc.Agents[OurTeam])
            {
                if (ret[(int)agent.AgentNumber].AgentStatusData == AgentStatusCode.RequestNotToDoAnything)
                {
                    System.Random r = new System.Random();
                    Arrow arrow = (Arrow)r.Next(0, 7);

                    while (true)
                    {
                        if (!Calc.Field.CellExist(agent + arrow)) continue;
                        Arrow arrow = (Arrow)r.Next(0, 7);
                        if (Calc.Field.IsMovable(agent, arrow))
                        {
                            ret[(int)agent.AgentNumber].AgentStatusData = AgentStatusCode.RequestMovement;
                            ret[(int)agent.AgentNumber].Destination = agent.Position + arrow;
                            break;
                        }
                        else if (Calc.Field.IsRemovableOpponentTile(agent, arrow))
                        {
                            ret[(int)agent.AgentNumber].AgentStatusData = AgentStatusCode.RequestRemovementOpponentTile;
                            ret[(int)agent.AgentNumber].Destination = agent.Position + arrow;
                            break;
                        }
                    }
                }
            }
            return ret;
        }
    }
}
