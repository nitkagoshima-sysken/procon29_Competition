using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.NettaBot
{
    class NettaBot : Bot.Bot
    {
        public NettaBot() : base() { }

        public override AgentActivityData[] Answer()
        {
            var result = new AgentActivityData[2];

            result[0] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
            result[1] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);

            return result;
        }
    }
}
