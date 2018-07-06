using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken
{
    namespace Procon29
    {
        namespace Bot
        {
            public abstract class Bot
            {
                /// <summary>
                /// 計算機を格納するところ
                /// </summary>
                protected Calc Calc { get; set; }

                /// <summary>
                /// Bot側のチームを表します
                /// </summary>
                public Team Team { get; set; }

                /// <summary>
                /// 初期化するところです
                /// </summary>
                public Bot() { }

                /// <summary>
                /// 問題が渡されます。
                /// </summary>
                /// <param name="calc"></param>
                public void Question(Calc calc)
                {
                    Calc = calc;
                }

                /// <summary>
                /// 答えを渡します。
                /// </summary>
                public abstract AgentActivityData[] Answer();

                /// <summary>
                /// エージェントを動かしたときに、状態がどう変化するか計算します。
                /// </summary>
                /// <param name="action">どうエージェントが動くか指定します。</param>
                /// <returns>エージェントを動かしたときの計算データが返ってきます。</returns>
                public Calc Simulate(AgentActivityData[,] action)
                {
                    Calc.MoveAgent(action);
                    var c = (Calc)Calc.DeepCopy();
                    Calc.Undo();
                    return c;
                }

                /// <summary>
                /// エージェントを動かしたときに、状態がどう変化するか計算します。
                /// </summary>
                /// <param name="action">どうエージェントが動くか指定します。</param>
                /// <returns>エージェントを動かしたときの計算データが返ってきます。</returns>
                public Calc Simulate(Team team, AgentActivityData[] action)
                {
                    Calc.MoveAgent(team, action);
                    var c = (Calc)Calc.DeepCopy();
                    Calc.Undo();
                    return c;
                }

                /// <summary>
                /// エージェントを動かしたときに、状態がどう変化するか計算します。
                /// </summary>
                /// <param name="action">どうエージェントが動くか指定します。</param>
                /// <returns>エージェントを動かしたときの計算データが返ってきます。</returns>
                public Calc Simulate(Team team, AgentNumber agentNumber, AgentActivityData action)
                {
                    Calc.MoveAgent(team, agentNumber, action);
                    var c = (Calc)Calc.DeepCopy();
                    Calc.Undo();
                    return c;
                }

            }
        }
    }
}