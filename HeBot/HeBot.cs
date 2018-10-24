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
                    agentActivityData[(int)AgentNumber.One] = MoveOrRemoveTile(destinationOne);
                    agentActivityData[(int)AgentNumber.Two] = MoveOrRemoveTile(destinationTwo);

                    //Simulate
                    calc = Calc.Simulate(OurTeam, agentActivityData);

                    //AgentStatusCode transform RequestCode
                    foreach (var item in agentActivityData) item.AgentStatusData.ToRequest();
                    nowpoint = (double)calc.Field.TotalPoint(OurTeam) - (double)calc.Field.TotalPoint(OurTeam.Opponent());
                    if (maxpoint < nowpoint)
                    {
                        maxpoint = nowpoint;
                        result = agentActivityData.DeepClone();
                    }
                }
            }

            return result;
        }
        private AgentActivityData MoveOrRemoveTile(Coordinate coordinate)
        {
            if (Calc.Field[coordinate].IsTileOn[OurTeam.Opponent()])return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);
        }
    }
}
