using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.SatsumaimoBot
{
    public class SatsumaimoBot : Bot.Bot
    {
        public override AgentActivityData[] Answer()
        {
            var AgentActivityData = new AgentActivityData[2]
            {
                new AgentActivityData (AgentStatusCode.RequestNotToDoAnything),
                new AgentActivityData (AgentStatusCode.RequestNotToDoAnything),
            };
            int maxpoint = int.MinValue; // 最大値

            foreach (Arrow arrow1 in Enum.GetValues(typeof(Arrow)))
            {
                foreach (Arrow arrow2 in Enum.GetValues(typeof(Arrow)))
                {
                    var test = new AgentActivityData[2]
                    {
                        new AgentActivityData() { },
                        new AgentActivityData() { }
                    };
                    AgentActivityData[0].AgentStatusData = MoveOrRemove(Calc.Agents[OurTeam, AgentNumber.One], arrow1);
                    AgentActivityData[0].Destination = Calc.Agents[OurTeam, AgentNumber.One].Position + arrow1;
                    AgentActivityData[1].AgentStatusData = MoveOrRemove(Calc.Agents[OurTeam, AgentNumber.Two], arrow2);
                    AgentActivityData[1].Destination = Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow2;
                    var c = Simulate(OurTeam, AgentActivityData);
                    var point = c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent());
                    if (maxpoint < point)
                    {
                        maxpoint = point;
                    }
                }
            }
            return AgentActivityData;
        }

        AgentStatusCode MoveOrRemove(Agent agent, Arrow arrow)
        {
            if (Calc.Field.IsMovable(agent, arrow))
            {
                return AgentStatusCode.RequestMovement;
            }
            else
            {
                return AgentStatusCode.RequestRemovementOpponentTile;
            }
        }
    }
}
