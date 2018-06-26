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
        /// <param name="calc"></param>
        public override void Grasp(Calc calc)
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override AgentActivityData[] FinalAnswer()
        {
            var result = new AgentActivityData[2];
            result[0] = new AgentActivityData(AgentStatusData.RequestMovement, new Point(0, 0));
            result[1] = new AgentActivityData(AgentStatusData.RequestMovement, new Point(0, 0));
            return result;
        }

    }
}
