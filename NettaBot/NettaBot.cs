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

            foreach(Agent agent in Calc.Agents)
            {
                if(agent.Team == Team)
                {
                    if(((agent.Position.X + agent.Position.Y)%2 != 0) == true)
                    {
                        var maxpoint = int.MinValue;
                        Coordinate coordinate = new Coordinate();
                        
                        foreach(Arrow arrow in Enum.GetValues(typeof(Arrow)))
                        {
                            var slanting = new Coordinate() + arrow;
                            if (slanting.X * slanting.Y == 0)
                            {
                                var c = Simulate(
                                    Team,
                                    agent.AgentNumber,
                                    MoveOrRemoveTile( agent.Position + arrow));
                                if (maxpoint < c.TotalPoint(Team))
                                {
                                    maxpoint = c.TotalPoint(Team);
                                    coordinate = new Coordinate(agent.Position + arrow);
                                }
                                result[(int)agent.AgentNumber] = MoveOrRemoveTile(coordinate);
                            }
                        }
                    }
                    else
                    {
                        var maxpoint = int.MinValue;
                        Coordinate coordinate = new Coordinate();
                        
                        foreach(Arrow arrow in Enum.GetValues(typeof(Arrow)))
                        {
                            var slanting = new Coordinate() + arrow;
                            if (slanting.X * slanting.Y != 0)
                            {
                                var c = Simulate(
                                    Team,
                                    agent.AgentNumber,
                                   MoveOrRemoveTile(agent.Position + arrow));
                                if (maxpoint < c.TotalPoint(Team))
                                {
                                    maxpoint = c.TotalPoint(Team);
                                    coordinate = new Coordinate(agent.Position + arrow);
                                }
                                result[(int)agent.AgentNumber] = MoveOrRemoveTile(coordinate);
                            }
                        }

                    }
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
            if (!(Calc.Field[coordinate].IsTileOn[Team]) && (Calc.Field[coordinate].IsTileOn[Team.A] || Calc.Field[coordinate].IsTileOn[Team.B]))
            {
                return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            }
            return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);

        }
    }
}
