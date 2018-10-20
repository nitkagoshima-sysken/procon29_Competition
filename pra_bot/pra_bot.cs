using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.pra_bot
{
    public class pra_bot : Bot.Bot
    {
        public pra_bot() : base() { }

        public override AgentActivityData[] Answer()
        {
            System.Random r = new System.Random();
            var result = new AgentActivityData[2] { new AgentActivityData(), new AgentActivityData() };
            Arrow num;
            num = (Arrow)r.Next(0, 8);
            var go = new Coordinate[2];

            go[0] = Calc.Agents[Team, AgentNumber.One].Position + num;
            go[1] = Calc.Agents[Team, AgentNumber.Two].Position + num;

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
            if (Calc.Field[coordinate].IsTileOn[Team.Opponent()])
            {
                return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            }
            return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);

        }



    }
}
