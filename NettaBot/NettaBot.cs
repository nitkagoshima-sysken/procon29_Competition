using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.NettaBot
{
    struct ReturnStruct
    {
        public int point { get; set; }
        public AgentActivityData[] AgentActivityData {get; set;}
        public ReturnStruct(int p, AgentActivityData[] agentActivityData)
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
            var result = new AgentActivityData[2];

            result = BestHand(Calc, 2).AgentActivityData;

            return result;
        }

        private ReturnStruct BestHand(Calc calc, int depth)
        {
            var maxpoint = int.MinValue;
            var agentActivityData = new AgentActivityData[2];
            var result = new AgentActivityData[2];
            foreach (Arrow arrowOne in Enum.GetValues(typeof(Arrow)))
            {
                var destinationOne = agentOne.Position + arrowOne;
                if (!calc.Field.CellExist(destinationOne)) continue;

                foreach (Arrow arrowTwo in Enum.GetValues(typeof(Arrow)))
                {
                    var destinationTwo = agentTwo.Position + arrowTwo;
                    if (!calc.Field.CellExist(destinationTwo)) continue;
                    if (destinationOne == destinationTwo) continue;
                    if (((destinationOne.X + destinationOne.Y) % 2 != 0) == isOdd)
                    {
                        agentActivityData[(int)AgentNumber.One] = MoveOrRemoveTile(destinationOne);
                    }
                    else
                    {
                        agentActivityData[(int)AgentNumber.One] = RemoveTile(destinationOne);
                        if (agentActivityData[(int)AgentNumber.One].AgentStatusData == AgentStatusCode.RequestNotToDoAnything) continue;
                    }

                    if (((destinationTwo.X + destinationTwo.Y) % 2 != 0) == isOdd)
                    {
                        agentActivityData[(int)AgentNumber.Two] = MoveOrRemoveTile(destinationTwo);
                    }
                    else
                    {
                        agentActivityData[(int)AgentNumber.Two] = RemoveTile(destinationOne);
                        if (agentActivityData[(int)AgentNumber.Two].AgentStatusData == AgentStatusCode.RequestNotToDoAnything) continue;
                    }
                    var c = calc.Simulate(OurTeam, agentActivityData.DeepClone());
                    Log.Write((c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent())).ToString() + ",");
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
            Log.WriteLine("Return to " + maxpoint);
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
