using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.HeBot
{
    class HeBot : Bot.Bot
    {
        public HeBot() : base(){}

        public override AgentActivityData[] Answer()
        {
            var result = new AgentActivityData[2];
            var maxpoint = double.MinValue;
            var rate = 1.0;
            var agentActivityData = new AgentActivityData[2];
            Calc calc;
            Coordinate destinationOne;
            Coordinate destinationTwo;
            double nowpoint;

            foreach (Arrow arrowOne in Enum.GetValues(typeof(Arrow)))
            {
                destinationOne = Calc.Agents[OurTeam, AgentNumber.One].Position + arrowOne;
                if (!Calc.Field.CellExist(destinationOne)) continue;

                foreach (Arrow arrowTwo in Enum.GetValues(typeof(Arrow)))
                {
                    destinationTwo = Calc.Agents[OurTeam, AgentNumber.Two].Position + arrowTwo;
                    if (!Calc.Field.CellExist(destinationTwo)) continue;
                    if (destinationOne == destinationTwo) continue;

                    //Setting AgentActivityData
                    //AgentNumber.One
                    rate  = 1.0;
                    agentActivityData[(int)AgentNumber.One] = Calc.Field[destinationOne].IsTileOn[OurTeam.Opponent()]?
                        new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, destinationOne):
                        new AgentActivityData(AgentStatusCode.RequestMovement, destinationOne);
                    rate *= 0.1 / (Math.Abs(destinationOne.X + 1 - Calc.Field.Width / 2.0) + 1) + 1.0;
                    rate *= 0.1 / (Math.Abs(destinationOne.Y + 1 - Calc.Field.Height / 2.0) + 1) + 1.0;

                    //AgentNumber.Two
                    agentActivityData[(int)AgentNumber.Two] = Calc.Field[destinationTwo].IsTileOn[OurTeam.Opponent()]?
                        new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, destinationTwo):
                        new AgentActivityData(AgentStatusCode.RequestMovement, destinationTwo);
                    rate *= 0.1 / (Math.Abs(destinationTwo.X + 1 - Calc.Field.Width / 2.0) + 1) + 1.0;
                    rate *= 0.1 / (Math.Abs(destinationTwo.Y + 1 - Calc.Field.Height / 2.0) + 1) + 1.0;

                    //Simulate
                    calc = Calc.Simulate(OurTeam, agentActivityData);

                    //AgentStatusCode transform RequestCode
                    foreach (var item in agentActivityData) item.AgentStatusData.ToRequest();
                    nowpoint = rate * (
                        ((double)calc.Field.TotalPoint(OurTeam) - (double)Calc.Field.TotalPoint(OurTeam))
                        - ((double)calc.Field.TotalPoint(OurTeam.Opponent()) - (double)Calc.Field.TotalPoint(OurTeam.Opponent())));
                    if (maxpoint < nowpoint)
                    {
                        maxpoint = nowpoint;
                        result = agentActivityData.DeepClone();
                    }
                }
            }
            return result;
        }
    }
}
