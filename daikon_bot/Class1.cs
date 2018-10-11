using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.daikon_bot
{
    public class daikon_bot : Bot.Bot
    {
        public daikon_bot() : base() { }
        public override AgentActivityData[] Answer()
        {
            var result = new AgentActivityData[2] { new AgentActivityData(), new AgentActivityData() };
            var go = new Coordinate[2];
            Arrow num = Arrow.Down;
            int c1 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate(0, 0));
            int c2 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate(Calc.Field.Width, 0));
            int c3 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate(Calc.Field.Width, Calc.Field.Height));
            int c4 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate(0, Calc.Field.Height));
            int dis = Math.Min(Math.Min(c1, c2), Math.Min(c3, c4));

            //code is here.
            


    go[0] = Calc.Agents[OurTeam, AgentNumber.One].Position + num;
            go[1] = Calc.Agents[OurTeam, AgentNumber.Two].Position + num;

            if (0 <= go[0].X && go[0].X < Calc.Field.Width && 0 <= go[0].Y && go[0].Y < Calc.Field.Height)
            {
                result[(int)AgentNumber.One] = MoveOrRemoveTile(go[0]);
            }
            if (0 <= go[1].X && go[1].X < Calc.Field.Width && 0 <= go[1].Y && go[1].Y < Calc.Field.Height)
            {
                result[(int)AgentNumber.Two] = MoveOrRemoveTile(go[1]);
            }
            return result;

        }


        private AgentActivityData MoveOrRemoveTile(Coordinate coordinate)
        {
            if (Calc.Field[coordinate].IsTileOn[OurTeam.Opponent()])
            {
                return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            }
            return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);

        }

    }
}