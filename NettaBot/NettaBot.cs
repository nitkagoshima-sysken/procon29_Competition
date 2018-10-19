using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.NettaBot
{
    struct ReturnStruct
    {
        public int point { get; set; }
        public TwoAgentsActivityData AgentActivityData {get; set;}
        public ReturnStruct(int p, TwoAgentsActivityData agentActivityData)
        {
            point = p;
            AgentActivityData = agentActivityData;
        }
    }

    class NettaBot : Bot.Bot
    {
        public NettaBot() : base()
        {
        }
        private Boolean isOdd;
        private Agent agentOne;
        private Agent agentTwo;

        public override AgentActivityData[] Answer()
        {
            isOdd = IsOdd();
            agentOne = Calc.Agents[OurTeam, AgentNumber.One];
            agentTwo = Calc.Agents[OurTeam, AgentNumber.Two];
            var buff = new TwoAgentsActivityData();
            var result = new AgentActivityData[2];

            buff = BestHand(Calc, 2).AgentActivityData;

            result[(int)AgentNumber.One] = buff[AgentNumber.One];
            result[(int)AgentNumber.Two] = buff[AgentNumber.Two];

            return result;
        }

        private ReturnStruct BestHand(Calc calc, int depth)
        {
            var maxpoint = int.MinValue;
            var agentActivityData = new TwoAgentsActivityData();
            var result = new TwoAgentsActivityData();
            foreach (Arrow arrowOne in Enum.GetValues(typeof(Arrow)))
            {
                var destinationOne = calc.Agents[OurTeam, AgentNumber.One].Position + arrowOne;
                if (!calc.Field.CellExist(destinationOne)) continue;

                foreach (Arrow arrowTwo in Enum.GetValues(typeof(Arrow)))
                {
                    var destinationTwo = calc.Agents[OurTeam, AgentNumber.Two].Position + arrowTwo;
                    if (!calc.Field.CellExist(destinationTwo)) continue;
                    if (destinationOne == destinationTwo) continue;
                    if ((destinationOne.X + destinationOne.Y) % 2 != 0 == isOdd)
                    {
                        agentActivityData[AgentNumber.One] = MoveOrRemoveTile(destinationOne);
                    }
                    else
                    {
                        agentActivityData[AgentNumber.One] = RemoveTile(destinationOne);
                        if (agentActivityData[AgentNumber.One].AgentStatusData == AgentStatusCode.RequestNotToDoAnything) continue;
                    }

                    if (((destinationTwo.X + destinationTwo.Y) % 2 != 0) == isOdd)
                    {
                        agentActivityData[AgentNumber.Two] = MoveOrRemoveTile(destinationTwo);
                    }
                    else
                    {
                        agentActivityData[AgentNumber.Two] = RemoveTile(destinationTwo);
                        if (agentActivityData[AgentNumber.Two].AgentStatusData == AgentStatusCode.RequestNotToDoAnything) continue;
                    }
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

        private Boolean IsOdd()
        {
            var sum = 0; ;
            foreach(var cell in Calc.Field.ToList())
            {
                sum = sum + (cell.Coordinate.X + cell.Coordinate.Y) % 2 != 0 ? cell.Point : -cell.Point;
            }
            return sum > 0;
        }

        private AgentActivityData MoveOrRemoveTile(Coordinate coordinate)
        {
            if (Calc.Field[coordinate].IsTileOn[OurTeam.Opponent()])
            {
                return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            }
            return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);

        }

        private AgentActivityData RemoveTile(Coordinate coordinate)
        {
            if (Calc.Field[coordinate].IsTileOn[OurTeam.Opponent()])
            {
                return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            }
            return new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
        }
    }
}
