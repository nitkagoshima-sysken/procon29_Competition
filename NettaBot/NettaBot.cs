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

            var agentOne = Calc.Agents[Team, AgentNumber.One];
            var agentTwo = Calc.Agents[Team, AgentNumber.Two];
            var isOdd = IsOdd();
            var agentActivityDatas = new AgentActivityData[2];
            var maxpoint = int.MinValue;
            foreach(Arrow arrowOne in Enum.GetValues(typeof(Arrow)))
            {
                var destinationOne = agentOne.Position + arrowOne;

                foreach (Arrow arrowTwo in Enum.GetValues(typeof(Arrow)))
                {
                    var destinationTwo = agentTwo.Position + arrowTwo;
                    try {

                    if (((destinationOne.X + destinationOne.Y) % 2 != 0) == isOdd)
                    {
                        agentActivityDatas[(int)AgentNumber.One] = MoveOrRemoveTile(destinationOne);
                    }
                    else
                    {
                        continue;
                    }

                    if (((destinationTwo.X + destinationTwo.Y) % 2 != 0) == isOdd)
                    {
                        agentActivityDatas[(int)AgentNumber.Two] = MoveOrRemoveTile(destinationTwo);
                    }
                    else
                    {
                        continue;
                    }
                    Console.WriteLine(destinationOne + ":" + destinationTwo);
                    Console.WriteLine(isOdd +":"+ ((destinationOne.X + destinationOne.Y) % 2 != 0) +":"+ ((destinationTwo.X + destinationTwo.Y) % 2 != 0));
                    Console.WriteLine(agentActivityDatas[0].AgentStatusData + ":" + agentActivityDatas[1].AgentStatusData);
                        var c = Simulate(Team, agentActivityDatas);
                        Console.WriteLine(c.TotalPoint(Team)+":"+c.TotalPoint(Team.Opponent()));
                        if (maxpoint < c.TotalPoint(Team) )
                        {
                            maxpoint = c.TotalPoint(Team);
                            result = agentActivityDatas.DeepClone();
                        }
                    }
                    catch { }
                }
            }

            return result;
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
            if (Calc.Field[coordinate].IsTileOn[Team.Opponent()])
            {
                return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            }
            return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);

        }
    }
}
