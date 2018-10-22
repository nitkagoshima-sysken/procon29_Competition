using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.NettaBot
{
    struct ReturnStruct
    {
        public double point { get; set; }
        public AgentActivityData[] AgentActivityData {get; set;}
        public ReturnStruct(double p, AgentActivityData[] agentActivityData)
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
            var maxpoint = double.MinValue;
            var agentActivityData = new AgentActivityData[2];
            var result = new AgentActivityData[2];
            var rate = 1.0;
            var failed = false;
            foreach (Arrow arrowOne in Enum.GetValues(typeof(Arrow)))
            {
                var destinationOne = calc.Agents[OurTeam, AgentNumber.One].Position + arrowOne;
                if (!calc.Field.CellExist(destinationOne)) continue;

                foreach (Arrow arrowTwo in Enum.GetValues(typeof(Arrow)))
                {
                    rate = 1.0;
                    failed = false;
                    var destinationTwo = calc.Agents[OurTeam, AgentNumber.Two].Position + arrowTwo;
                    if (!calc.Field.CellExist(destinationTwo)) continue;
                    if (destinationOne == destinationTwo) continue;
                    if ((destinationOne.X + destinationOne.Y) % 2 != 0 == isOdd)
                    {
                        agentActivityData[(int)AgentNumber.One] = MoveOrRemoveTile(destinationOne);
                        rate *= 1.2;
                    }
                    else
                    {
                        agentActivityData[(int)AgentNumber.One] = RemoveTile(destinationOne);
                        if (agentActivityData[(int)AgentNumber.One].AgentStatusData == AgentStatusCode.RequestNotToDoAnything) continue;
                        rate *= 0.5;
                    }

                    if (((destinationTwo.X + destinationTwo.Y) % 2 != 0) == isOdd)
                    {
                        agentActivityData[(int)AgentNumber.Two] = MoveOrRemoveTile(destinationTwo);
                        rate *= 1.2;
                    }
                    else
                    {
                        agentActivityData[(int)AgentNumber.Two] = RemoveTile(destinationTwo);
                        if (agentActivityData[(int)AgentNumber.Two].AgentStatusData == AgentStatusCode.RequestNotToDoAnything) continue;
                        rate *= 0.5;
                    }
                    var c = calc.Simulate(OurTeam, agentActivityData);
                    foreach (var item in agentActivityData) failed = item.AgentStatusData.IsFailed() ? true : failed;
                    if (failed) continue;
                    foreach (var item in agentActivityData) item.AgentStatusData.ToRequest();
                    if (depth <= 1)
                    {
                        if (maxpoint < (rate * (c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent()))))
                        {
                            maxpoint = rate * (c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent()));
                            result = agentActivityData.DeepClone();
                        }
                    }
                    else
                    {
                        var bestHand = rate*BestHand(c, depth - 1).point;
                        if (maxpoint < bestHand)
                        {
                            maxpoint = bestHand;
                            result = agentActivityData.DeepClone();
                        }
                    }
                    Log.WriteLine("maxpoint=" + maxpoint + " rate=" + rate + " TotalPoint=" + rate * (c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent())) +" originalpoint="+(c.Field.TotalPoint(OurTeam) - c.Field.TotalPoint(OurTeam.Opponent())));
                }
            }
            Log.WriteLine("Return point to " + maxpoint);
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
