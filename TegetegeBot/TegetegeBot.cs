using System.Drawing;
using Procon29_Visualizer;

namespace TegetegeBot
{
    /// <summary>
    /// テスト用プログラム
    /// ちなみにこのボットは、いかなるときも(0,0)へ移動しようとするボットだった。
    /// </summary>
    class TegetegeBot : Bot
    {
        /// <summary>
        /// 初期化するところです
        /// </summary>
        /// <param name="team"></param>
        public TegetegeBot() : base() { }

        /// <summary>
        /// あなたが、どこにエージェントを行かせるかを、
        /// この関数に書きます。
        /// </summary>
        /// <returns>どこにエージェントに行かせるか</returns>
        public override AgentActivityData[] Answer()
        {
            var result = new AgentActivityData[2];
            var maxp = -100;
            var maxp1 = -100;
            Point point = new Point();
            Point point1 = new Point();
            for (int x = 0; x < Calc.Field.Width(); x++)
            {
                for (int y = 0; y < Calc.Field.Height(); y++)
                {
                    var p = GetDiffPoint0(new Point(x, y));

                    if (Calc.AgentPosition[(int)Team, 0].ChebyshevDistance(new Point(x, y)) != 1) continue;
                    if (maxp < p)
                    {
                        maxp = p;
                        point = new Point(x, y);
                    }
                }
            }
            for (int x = 0; x < Calc.Field.Width(); x++)
            {
                for (int y = 0; y < Calc.Field.Height(); y++)
                {
                    var p = GetDiffPoint1(new Point(x, y));

                    if (Calc.AgentPosition[(int)Team, 1].ChebyshevDistance(new Point(x, y)) != 1) continue;
                    if (maxp1 < p)
                    {
                        maxp1 = p;
                        point1 = new Point(x, y);
                    }
                }
            }
            result[0] = new AgentActivityData(AgentStatusData.RequestMovement, point);
            result[1] = new AgentActivityData(AgentStatusData.RequestMovement, point1);
            return result;
        }

        protected int GetDiffPoint0(Point point)
        {
            //var p = Calc.TotalPoint(Team.B);
            var go = new AgentActivityData[2, 2];
            var otherteam = (Team == Team.A) ? Team.B : Team.A;

            go[(int)otherteam, 0] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[(int)otherteam, 1] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[(int)Team, 0] = new AgentActivityData(AgentStatusData.RequestMovement, point);
            go[(int)Team, 1] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            return SimulateAndTake(go, Calc.TotalPoint);
        }

        protected int GetDiffPoint1(Point point)
        {
            //var p = Calc.TotalPoint(Team.B);
            var go = new AgentActivityData[2, 2];
            var otherteam = (Team == Team.A) ? Team.B : Team.A;

            go[(int)otherteam, 0] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[(int)otherteam, 1] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[(int)Team, 0] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[(int)Team, 1] = new AgentActivityData(AgentStatusData.RequestMovement, point);
            return SimulateAndTake(go, Calc.TotalPoint);
        }
    }
}
