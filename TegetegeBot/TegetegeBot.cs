using System.Drawing;
using Procon29_Visualizer;

namespace TegetegeBot
{
    /// <summary>
    /// テスト用プログラム
    /// ちなみにこのボットは、いかなるときも(0,0)へ移動しようとする。
    /// </summary>
    class TegetegeBot : Bot
    {
        /// <summary>
        /// 
        /// </summary>
        public TegetegeBot() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

                    if (Calc.AgentPosition[1, 0].ChebyshevDistance(new Point(x, y)) != 1) continue;
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

                    if (Calc.AgentPosition[1, 1].ChebyshevDistance(new Point(x, y)) != 1) continue;
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
            var p = Calc.TotalPoint(Team.B);
            var go = new AgentActivityData[2, 2];
            go[0, 0] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[0, 1] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[1, 0] = new AgentActivityData(AgentStatusData.RequestMovement, point);
            go[1, 1] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            Calc.MoveAgent(go);
            var diff = Calc.TotalPoint(Team.B) - p;
            Calc.Undo();
            return diff;
        }

        protected int GetDiffPoint1(Point point)
        {
            var p = Calc.TotalPoint(Team.B);
            var go = new AgentActivityData[2, 2];
            go[0, 0] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[0, 1] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[1, 0] = new AgentActivityData(AgentStatusData.RequestNotToDoAnything, new Point());
            go[1, 1] = new AgentActivityData(AgentStatusData.RequestMovement, point);
            //Calc.MoveAgent(go);
            //var diff = Calc.TotalPoint(Team.B) - p;
            //Calc.Undo();
            //return diff;
            return Hoge(go, Team.B);
        }
    }
}
