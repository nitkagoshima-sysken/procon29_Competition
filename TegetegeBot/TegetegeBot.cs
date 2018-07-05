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

                    // 一人目のエージェントの最高得点を保存するために使う変数
                    var agnt1maxp = -16;
                    // 二人目のエージェントの最高得点を保存するために使う変数
                    var agnt2maxp = -16;
                    // 一人目のエージェントがどこに行くかを保存するために使う変数
                    Coordinate agnt1pos = new Coordinate();
                    // 二人目のエージェントがどこに行くかを保存するために使う変数
                    Coordinate agnt2pos = new Coordinate();

                    for (int x = 0; x < Calc.Field.Width; x++)
                    {
                        for (int y = 0; y < Calc.Field.Height; y++)
                        {
                            var trying = new AgentActivityData[2];

                            // Bot側のチーム、1人目のエージェントが(x,y)に移動する
                            trying[(int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestMovement, new Point(x, y));
                            // Bot側のチーム、2人目のエージェントは何もしない
                            trying[(int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything, new Point());
                            var p = Simulate(action: trying, take: Calc.TotalPoint);

                            if (Calc.Agents[Team, AgentNumber.One].Position.ChebyshevDistance(new Point(x, y)) != 1) continue;
                            // 今までの中で一番、得点が高かったら、ap1の座標を更新する
                            if (agnt1maxp < p)
                            {
                                agnt1maxp = p;
                                agnt1pos = new Point(x, y);
                            }
                        }
                    }

                    // Bot側のチーム、1人目のエージェントがap1に行くことが確定する
                    result[0] = new AgentActivityData(AgentStatusCode.RequestMovement, agnt1pos);

                    for (int x = 0; x < Calc.Field.Width; x++)
                    {
                        for (int y = 0; y < Calc.Field.Height; y++)
                        {
                            var trying = new AgentActivityData[2];

                            trying[(int)AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything, new Point());
                            trying[(int)AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestMovement, new Point(x, y));
                            var p = Simulate(action: trying, take: Calc.TotalPoint);

                            if (Calc.Agents[Team, AgentNumber.Two].Position.ChebyshevDistance(new Point(x, y)) != 1) continue;
                            if (agnt2maxp < p)
                            {
                                agnt2maxp = p;
                                agnt2pos = new Point(x, y);
                            }
                        }
                    }
                    result[1] = new AgentActivityData(AgentStatusCode.RequestMovement, agnt2pos);
                    return result;
                }
            }
        }
    }
}