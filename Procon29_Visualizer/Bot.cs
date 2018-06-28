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
        /// 
        /// </summary>
        protected Calc Calc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calc"></param>
        public Bot() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="calc"></param>
        public void Question(Calc calc)
        {
            Calc = calc;
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract AgentActivityData[] Answer();

        /// <summary>
        /// 
        /// </summary>
        public int Hoge(AgentActivityData[,] agentActivityDatas, Team team)
        {
            var p = Calc.TotalPoint(team);
            Calc.MoveAgent(agentActivityDatas);
            var diff = Calc.TotalPoint(team) - p;
            Calc.Undo();
            return diff;
        }
    }
}
