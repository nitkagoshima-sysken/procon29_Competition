using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.KaraimoBot
{
    public class KaraimoBot : Bot.Bot
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
                    if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.One].Position + arrow1) &&
                        Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow2))
                    {
                        var test = new AgentActivityData[2]
                        {
                            new AgentActivityData()
                            {
                                AgentStatusData = MoveOrRemove(Calc.Agents[OurTeam, AgentNumber.One], arrow1),
                                Destination = Calc.Agents[OurTeam, AgentNumber.One].Position + arrow1
                            },
                            new AgentActivityData()
                            {
                                AgentStatusData = MoveOrRemove(Calc.Agents[OurTeam, AgentNumber.Two], arrow2),
                                Destination = Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow2
                            },
                        };
                        var c = Simulate(OurTeam, test);
                        var point = c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent());
                        if (maxpoint < point)
                        {
                            var pastcalc = Calc.History[(Calc.Turn - 1 > 0) ? Calc.Turn - 1 : 0];
                            if ((pastcalc.AgentsActivityData[OurTeam, AgentNumber.One].AgentStatusData != AgentStatusCode.NotDoneAnything &&
                                pastcalc.AgentsActivityData[OurTeam, AgentNumber.One].AgentStatusData.ToRequest() != test[0].AgentStatusData) ||
                                pastcalc.AgentsActivityData[OurTeam, AgentNumber.One].Destination != test[0].Destination ||
                                (pastcalc.AgentsActivityData[OurTeam, AgentNumber.Two].AgentStatusData != AgentStatusCode.NotDoneAnything &&
                                pastcalc.AgentsActivityData[OurTeam, AgentNumber.Two].AgentStatusData.ToRequest() != test[1].AgentStatusData) ||
                                pastcalc.AgentsActivityData[OurTeam, AgentNumber.Two].Destination != test[1].Destination)
                            {
                                maxpoint = point;
                            }
                            if ((pastcalc.AgentsActivityData[OurTeam, AgentNumber.One].AgentStatusData != AgentStatusCode.NotDoneAnything &&
                                pastcalc.AgentsActivityData[OurTeam, AgentNumber.One].AgentStatusData.ToRequest() != test[0].AgentStatusData) ||
                                pastcalc.AgentsActivityData[OurTeam, AgentNumber.One].Destination != test[0].Destination)
                            {
                                AgentActivityData[0].AgentStatusData = test[0].AgentStatusData;
                                AgentActivityData[0].Destination = test[0].Destination;
                            }
                            else
                            {
                                Log.WriteLine("error yesterday crash one");
                            }
                            if ((pastcalc.AgentsActivityData[OurTeam, AgentNumber.Two].AgentStatusData != AgentStatusCode.NotDoneAnything &&
                                pastcalc.AgentsActivityData[OurTeam, AgentNumber.Two].AgentStatusData.ToRequest() != test[1].AgentStatusData) ||
                                pastcalc.AgentsActivityData[OurTeam, AgentNumber.Two].Destination != test[1].Destination)
                            {
                                AgentActivityData[1].AgentStatusData = test[1].AgentStatusData;
                                AgentActivityData[1].Destination = test[1].Destination;
                            }
                            else
                            {
                                Log.WriteLine("error yesterday crash two");
                            }
                        }
                    }
                }
            }
            //System.Random r = new System.Random(1000);


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
