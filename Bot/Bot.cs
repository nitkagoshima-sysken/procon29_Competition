using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
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
                /// <param name="take">ほしい情報を指定します。</param>
                /// <returns></returns>
                public int Simulate(AgentActivityData[,] action, Func<Team, int> take)
                {
                    Calc.MoveAgent(action);
                    int p = take(Team);
                    Calc.Undo();
                    return p;
                }

                /// <summary>
                /// エージェントを動かしたときに、状態がどう変化するか計算します。
                /// </summary>
                /// <param name="action">どうエージェントが動くか指定します。</param>
                /// <param name="take">ほしい情報を指定します。</param>
                /// <returns></returns>
                public int Simulate(AgentActivityData[] action, Func<Team, int> take)
                {
                    Calc.MoveAgent(Team, action);
                    int p = take(Team);
                    Calc.Undo();
                    return p;
                }
            }
        }
    }
}