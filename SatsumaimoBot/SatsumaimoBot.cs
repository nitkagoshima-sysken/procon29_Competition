using System;
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
                    System.Random r = new System.Random(100);
                    Arrow arrow = (Arrow)r.Next(Enum.GetNames(typeof(Arrow)).Length);

                    while (true)
                    {
                        if (!Calc.Field.CellExist(agent.Position + arrow)) continue;
                        arrow = (Arrow)r.Next(Enum.GetNames(typeof(Arrow)).Length);
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
