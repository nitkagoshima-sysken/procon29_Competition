using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.HeBot
{
    struct ReturnStruct
    {
        public int point { get; set; }
        public AgentActivityData[] AgentActivityData { get; set; }
        public ReturnStruct(int p, AgentActivityData[] agentActivityData)
        {
            point = p;
            AgentActivityData = agentActivityData;
        }
    }

    class HeBot : Bot.Bot
    {
        public HeBot() : base()
        {
        }
        private Agent agentOne;
        private Agent agentTwo;

        public override AgentActivityData[] Answer()
        {
            agentOne = Calc.Agents[OurTeam, AgentNumber.One];
            agentTwo = Calc.Agents[OurTeam, AgentNumber.Two];
            var result = new AgentActivityData[2];

            result = BestHand(Calc, 1).AgentActivityData;


            return result;
        }

        private ReturnStruct BestHand(Calc calc, int depth)
        {
            var maxpoint = int.MinValue;
            var agentActivityData = new AgentActivityData[2];
            var result = new AgentActivityData[2];
            foreach (Arrow arrowOne in Enum.GetValues(typeof(Arrow)))
            {
                var destinationOne = calc.Agents[OurTeam, AgentNumber.One].Position + arrowOne;
                if (!calc.Field.CellExist(destinationOne)) continue;

                foreach (Arrow arrowTwo in Enum.GetValues(typeof(Arrow)))
                {
                    var destinationTwo = calc.Agents[OurTeam, AgentNumber.Two].Position + arrowTwo;
                    if (!calc.Field.CellExist(destinationTwo)) continue;
                    if (destinationOne == destinationTwo) continue;
                    agentActivityData[(int)AgentNumber.One] = MoveOrRemoveTile(destinationOne);
                    agentActivityData[(int)AgentNumber.Two] = MoveOrRemoveTile(destinationTwo);
                    var c = calc.Simulate(OurTeam, agentActivityData);
                    foreach (var item in agentActivityData) item.AgentStatusData.ToRequest();
                    if (depth <= 1)
                    {
                        if (maxpoint < (c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent())))
                        {
                            maxpoint = c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent());
                            result = agentActivityData.DeepClone();
                        }
                    }
                    else
                    {
                        var bestHand = BestHand(c, depth - 1);
                        if (maxpoint < bestHand.point)
                        {
                            maxpoint = bestHand.point;
                            result = agentActivityData.DeepClone();
                        }
                    }
                }
            }
            return new ReturnStruct(maxpoint, result);
        }

        private AgentActivityData MoveOrRemoveTile(Coordinate coordinate)
        {
            if (Calc.Field[coordinate].IsTileOn[OurTeam.Opponent()])
            {
                return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            }
            return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);

        }
    }
}
