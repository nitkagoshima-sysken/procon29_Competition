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
        /// <param name="calc"></param>
        public Bot() { }

        /// <summary>
        /// 
        /// </summary>
        public abstract void Grasp(Calc calc);

        /// <summary>
        /// 
        /// </summary>
        public abstract AgentActivityData[] FinalAnswer();
    }    
}
