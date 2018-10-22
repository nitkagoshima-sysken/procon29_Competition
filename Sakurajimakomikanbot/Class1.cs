using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.SakurajimaKomikanBot
{
    public class SakurajimaKomikanBot : Bot.Bot
    {
        public SakurajimaKomikanBot() : base() { }

        public override AgentActivityData[] Answer()
        {
            var result = new AgentActivityData[2];
            var agnt1maxp = -16;
            var agnt2maxp = -16;
            Coordinate agnt1pos = new Coordinate();
            Coordinate agnt2pos = new Coordinate();

            // 1人目
            //もし5ターン以下なら
            if (Calc.Turn <= 2)
            {
                Log.WriteLine(Calc.Turn + " if");
                foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                {
                    var c = Simulate(
                        OurTeam,
                        AgentNumber.One,
                        new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[OurTeam, AgentNumber.One].Position + arrow));

                    if (agnt1maxp < c.Field.TotalPoint(OurTeam))
                    {
                        agnt1maxp = c.Field.TotalPoint(OurTeam);
                        agnt1pos = new Coordinate(Calc.Agents[OurTeam, AgentNumber.One].Position + arrow);
                    }
                }
                result[(int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestMovement, agnt1pos);
            }
            //
            else
            {
                Log.WriteLine(Calc.Turn + " else");
                foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                {
                    if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.One].Position + arrow))
                    {
                        if (Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + arrow].IsTileOn[OurTeam.Opponent()])
                        {
                            result[(int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, Calc.Agents[OurTeam, AgentNumber.One].Position + arrow);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                if (result[(int)AgentNumber.One] == null)
                {
                    foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                    {
                        var c = Simulate(
                            OurTeam,
                            AgentNumber.One,
                            new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[OurTeam, AgentNumber.One].Position + arrow));

                        if (agnt1maxp < c.Field.TotalPoint(OurTeam))
                        {
                            agnt1maxp = c.Field.TotalPoint(OurTeam);
                            agnt1pos = new Coordinate(Calc.Agents[OurTeam, AgentNumber.One].Position + arrow);
                        }
                    }
                    result[(int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestMovement, agnt1pos);
                }

            }

            // 2人目
            //もし5ターン以下なら
            /*if (Calc.Turn <= 5)
              {
                  foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
                  {
                      var c = Simulate(
                          OurTeam,

                          AgentNumber.Two,
                          new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow));

                      if (agnt2maxp < c.Field.TotalPoint(OurTeam))
                      {
                          agnt2maxp = c.Field.TotalPoint(OurTeam);
                          agnt2pos = new Coordinate(Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow);
                      }
                  }
                  result[(int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestMovement, agnt2pos);
              }
              //
              else
              {*/
            foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
            {
                if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow))
                {
                    if (Calc.Field[Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow].IsTileOn[OurTeam.Opponent()])
                    {
                        result[(int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (result[(int)AgentNumber.Two] == null)
            {
                foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))

                {
                    var c = Simulate(
                        OurTeam,
                        AgentNumber.Two,
                        new AgentActivityData(AgentStatusCode.RequestMovement, Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow));

                    if (agnt2maxp < c.Field.TotalPoint(OurTeam))
                    {
                        agnt2maxp = c.Field.TotalPoint(OurTeam);
                        agnt2pos = new Coordinate(Calc.Agents[OurTeam, AgentNumber.Two].Position + arrow);
                    }
                }
                result[(int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestMovement, agnt2pos);
            }

            //}

            return result;
        }
    }
}