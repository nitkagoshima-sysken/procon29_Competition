using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    static class FourAgentsActivityDataExtension
    {
        public static void CheckCollision(this FourAgentsActivityData action)
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                foreach (AgentNumber agent in Enum.GetValues(typeof(AgentNumber)))
                {
                    var item = action[team, agent];
                    if (item.AgentStatusData.IsRequest())
                    {
                        foreach (Team otherteam in Enum.GetValues(typeof(Team)))
                        {
                            foreach (AgentNumber otheragent in Enum.GetValues(typeof(AgentNumber)))
                            {
                                if (team == otherteam && agent == otheragent) continue;
                                var otheritem = action[otherteam, otheragent];
                                if (item.Destination == otheritem.Destination)
                                {
                                    if (team == otherteam)
                                    {
                                        item.ToFailBySelfCollision();
                                        otheritem.ToFailBySelfCollision();
                                    }
                                    else
                                    {
                                        item.ToFailByCollisionWithEachOther();
                                        otheritem.ToFailByCollisionWithEachOther();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
