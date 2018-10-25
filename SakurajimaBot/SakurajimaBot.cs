using System;
using System.Collections.Generic;
using System.Linq;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.SakurajimaBot
{
    public class SakurajimaBot : SatsumaimoBot.SatsumaimoBot
    {
        readonly List<AgentStatusCode> AgentStatusCodes
            = new List<AgentStatusCode>()
            {
                AgentStatusCode.RequestMovement,
                AgentStatusCode.RequestRemovementOpponentTile,
                AgentStatusCode.RequestRemovementOurTile,
            };

        struct ReturnStruct : IComparable<ReturnStruct>
        {
            public double Evaluation { get; set; }
            public AgentActivityData[] ActivityData { get; set; }

            public int CompareTo(ReturnStruct other)
            {
                return (int)(Evaluation - other.Evaluation);
            }
        }

        public override AgentActivityData[] Answer()
        {
            var list = new List<ReturnStruct>();
            foreach (Arrow arrow1 in Enum.GetValues(typeof(Arrow)))
            {
                foreach (Arrow arrow2 in Enum.GetValues(typeof(Arrow)))
                {
                    var position = new Coordinate[]
                    {
                        Calc.Agents[OurTeam, AgentNumber.One].Position + arrow1,
                        Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow2,
                    };
                    if (position[(int)AgentNumber.One] == position[(int)AgentNumber.Two] ||
                        !Calc.Field.CellExist(position[(int)AgentNumber.One]) ||
                       !Calc.Field.CellExist(position[(int)AgentNumber.Two]))
                    {
                        continue;
                    }
                    foreach (var asc1 in AgentStatusCodes)
                    {
                        foreach (var asc2 in AgentStatusCodes)
                        {
                            var action = new AgentActivityData[] {
                                new AgentActivityData(asc1, position[(int)AgentNumber.One]),
                                new AgentActivityData(asc2, position[(int)AgentNumber.Two]),
                            };
                            var next_calc = Simulate(OurTeam, action);
                            if (next_calc.History[Calc.Turn].AgentsActivityData[OurTeam, AgentNumber.One].AgentStatusData.IsFailed() ||
                                next_calc.History[Calc.Turn].AgentsActivityData[OurTeam, AgentNumber.One].AgentStatusData.IsFailed())
                            {
                                continue;
                            }
                            list.Add(
                                new ReturnStruct
                                {
                                    Evaluation =
                                        EvaluationFunction(position[(int)AgentNumber.One]) +
                                        next_calc.History[Calc.Turn + 1].Field.TotalPoint(OurTeam) - next_calc.History[Calc.Turn + 1].Field.TotalPoint(OurTeam.Opponent())
                                        - (Calc.Field.TotalPoint(OurTeam) - Calc.Field.TotalPoint(OurTeam.Opponent())),
                                    ActivityData = action,
                                });
                        }
                    }
                }
            }
            return list.Max().ActivityData;
        }

        public double EvaluationFunction(Coordinate coordinate)
        {
            return Calc.Field.Sum(cell => cell.Point * Math.Pow(0.125, coordinate.ChebyshevDistance(cell.Coordinate))) - (Calc.Field.CellExist(coordinate) ? Calc.Field[coordinate].Point : 0);
        }
    }
}
