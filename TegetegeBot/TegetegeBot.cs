using System.Drawing;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken
{
    namespace Procon29
    {
        namespace TegetegeBot
        {
            /// <summary>
            /// テスト用プログラム
            /// ちなみにこのボットは、いかなるときも(0,0)へ移動しようとするボットだった。
            /// </summary>
            class TegetegeBot : Bot.Bot
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
                    var a1mp = -100;
                    var a2mp = -100;
                    Point ap1 = new Point();
                    Point ap2 = new Point();
                    for (int x = 0; x < Calc.Field.Width(); x++)
                    {
                        for (int y = 0; y < Calc.Field.Height(); y++)
                        {
                            var trying = new AgentActivityData[2, 2];
                            var otherteam = (Team == Team.A) ? Team.B : Team.A;

                            // Bot側のチーム、1人目のエージェントが(x,y)に移動する
                            trying[(int)Team, (int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestMovement, new Point(x, y));
                            // Bot側のチーム、2人目のエージェントは何もしない
                            trying[(int)Team, (int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything, new Point());
                            // 敵側のチーム、1人目のエージェントは何もしない
                            trying[(int)otherteam, (int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything, new Point());
                            // 敵側のチーム、2人目のエージェントは何もしない
                            trying[(int)otherteam, (int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything, new Point());
                            var p = Simulate(action: trying, take: Calc.TotalPoint);

                            if (Calc.AgentPosition[(int)Team, 0].ChebyshevDistance(new Point(x, y)) != 1) continue;
                            // 今までの中で一番、得点が高かったら、ap1の座標を更新する
                            if (a1mp < p)
                            {
                                a1mp = p;
                                ap1 = new Point(x, y);
                            }
                        }
                    }
                    // Bot側のチーム、1人目のエージェントがap1に行くことが確定する
                    result[0] = new AgentActivityData(AgentStatusCode.RequestMovement, ap1);
                    for (int x = 0; x < Calc.Field.Width(); x++)
                    {
                        for (int y = 0; y < Calc.Field.Height(); y++)
                        {
                            var trying = new AgentActivityData[2, 2];
                            var otherteam = (Team == Team.A) ? Team.B : Team.A;

                            trying[(int)Team, (int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything, new Point());
                            trying[(int)Team, (int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestMovement, new Point(x, y));
                            trying[(int)otherteam, (int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything, new Point());
                            trying[(int)otherteam, (int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything, new Point());
                            var p = Simulate(action: trying, take: Calc.TotalPoint);

                            if (Calc.AgentPosition[(int)Team, 1].ChebyshevDistance(new Point(x, y)) != 1) continue;
                            if (a2mp < p)
                            {
                                a2mp = p;
                                ap2 = new Point(x, y);
                            }
                        }
                    }
                    result[1] = new AgentActivityData(AgentStatusCode.RequestMovement, ap2);
                    return result;
                }
            }
        }
    }
}