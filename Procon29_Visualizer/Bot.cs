using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Procon29_Visualizer
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
        /// <param name="agentActivityDatas">どうエージェントが動くか指定します。</param>
        /// <param name="func">ほしい情報を指定します。</param>
        /// <returns></returns>
        public int SimulateAndTake(AgentActivityData[,] agentActivityDatas, Func<Team, int> func)
        {
            Calc.MoveAgent(agentActivityDatas);
            int p = func(Team);
            Calc.Undo();
            return p;
        }
    }
}
