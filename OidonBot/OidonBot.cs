using System;
using System.Linq;
using nitkagoshima_sysken.Procon29.Bot;
using nitkagoshima_sysken.Procon29.Visualizer;

using System.Collections.Generic;

namespace nitkagoshima_sysken.Procon29.OidonBot
{
    class OidonBot :Bot.Bot
    {
        public OidonBot() : base() { }
        AgentActivityData[] result = new AgentActivityData[2];
        //一人目のエージェントがどの方向に行くかを保存する
        Arrow agent1dir = Arrow.Up;
        //二人目のエージェントがどの方向に行くかを保存する
        Arrow agent2dir = Arrow.Up;
        //エージェント1が理想的にとれる最高値
        int agent1pmax = int.MinValue;
        //エージェント2が理想的にとれる最高値
        int agent2pmax = int.MinValue;
        Coordinate coodinate;
        int sum = 0;
        int count = 0;
        int nowpoint = 0;
        Coordinate pastposition;
        int enowpoint = 0;
        Calc calc;
        int[,] prob = new int[,]{
                { 3, 2, 2, 4, 8, 4, 2, 2 },
                { 2, 1, 2, 3, 4, 8, 4, 3 },
                { 2, 2, 3, 2, 2, 4, 8, 4 },
                { 4, 3, 2, 1, 2, 3, 4, 8 },
                { 8, 4, 2, 2, 3, 2, 2, 4 },
                { 4, 8, 4, 3, 2, 1, 2, 3 },
                { 2, 4, 8, 4, 2, 2, 3, 2 },
                { 2, 3, 4, 8, 4, 3, 2, 1 }
            };
        public override AgentActivityData[] Answer()
        {
            coodinate = Calc.Agents[OurTeam, AgentNumber.One].Position;
            foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
            {
                sum = 0;
                count = 0;
                nowpoint = Calc.Field.TotalPoint(OurTeam);
                enowpoint = Calc.Field.TotalPoint(OurTeam.Opponent());
                foreach (Arrow arrow2 in Enum.GetValues(typeof(Arrow)))
                {
                    try
                    {
                        calc = Simulate(OurTeam, AgentNumber.One, new AgentActivityData(AgentStatusCode.RequestMovement, coodinate + arrow));
                        calc = calc.Simulate(OurTeam, AgentNumber.One, new AgentActivityData(AgentStatusCode.RequestMovement, coodinate + arrow + arrow2));
                        if (Calc.Field[coodinate + arrow].IsTileOn[OurTeam.Opponent()])
                        {
                            sum += Calc.Field[coodinate + arrow].Point + 27 * (enowpoint - calc.Field.TotalPoint(OurTeam.Opponent()));
                        }
                        if (Calc.Field[coodinate + arrow].IsTileOn[OurTeam.Opponent()])
                        {
                            sum += 0;
                        }
                        else
                        {
                            sum += prob[(int)arrow, (int)arrow2] * (calc.Field.TotalPoint(OurTeam) - nowpoint);
                        }
                    } catch (Exception e)
                    {
                        count++;
                        continue;
                    }
                }
                if ((agent1pmax < sum)&&(count!=8))
                {
                    agent1pmax = sum;
                    agent1dir = arrow;
                }
                pastposition = coodinate;
            }
            try {
                if (Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + agent1dir].IsTileOn[OurTeam.Opponent()])
                {
                    result[(int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, Calc.Agents[OurTeam, AgentNumber.One].Position + agent1dir);
                }
                result[(int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[OurTeam, AgentNumber.One].Position + agent1dir);
            }
            catch (Exception e)
            {
            }
    coodinate = Calc.Agents[OurTeam, AgentNumber.Two].Position;
            foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
            {
                sum = 0;
                count = 0;
                nowpoint = Calc.Field.TotalPoint(OurTeam);
                enowpoint = Calc.Field.TotalPoint(OurTeam.Opponent());
                foreach (Arrow arrow2 in Enum.GetValues(typeof(Arrow)))
                {
                    try
                    {
                        calc = Simulate(OurTeam, AgentNumber.Two, new AgentActivityData(AgentStatusCode.RequestMovement, coodinate + arrow));
                        calc = calc.Simulate(OurTeam, AgentNumber.Two, new AgentActivityData(AgentStatusCode.RequestMovement, coodinate + arrow + arrow2));
                        if (Calc.Field[coodinate + arrow].IsTileOn[OurTeam.Opponent()])
                        {
                            sum += Calc.Field[coodinate + arrow].Point + 27 * (enowpoint - calc.Field.TotalPoint(OurTeam.Opponent()));
                        }
                        if (Calc.Field[coodinate + arrow].IsTileOn[OurTeam.Opponent()])
                        {
                            sum += 0;
                        }
                        else
                        {
                            sum += prob[(int)arrow, (int)arrow2] * (calc.Field.TotalPoint(OurTeam) - nowpoint);
                        }
                    }
                    catch (Exception e)
                    {
                        count++;
                        continue;
                    }
                }

                if ((agent2pmax < sum)&&(count!=8))
                {
                    agent2pmax = sum;
                    agent2dir = arrow;
                }
                pastposition = coodinate;
            }
            try
            {
                if (Calc.Field[Calc.Agents[OurTeam, AgentNumber.Two].Position + agent2dir].IsTileOn[OurTeam.Opponent()])
                {
                    result[(int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, Calc.Agents[OurTeam, AgentNumber.Two].Position + agent2dir);
                }
                result[(int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[OurTeam, AgentNumber.Two].Position + agent2dir);
            }
            catch (Exception e)
            {
            }
            return result;
        }
    }
}
