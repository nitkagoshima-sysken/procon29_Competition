using nitkagoshima_sysken.Procon29.Visualizer;
using System;

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
                /// ログを取ります。
                /// </summary>
                public Logger Log { get; set; }

                /// <summary>
                /// ログを取ります。
                /// </summary>
                public PlayMode Mode { get; set; }

                /// <summary>
                /// Bot側のチームを表します
                /// </summary>
                public Team OurTeam { get; set; }

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
                public Calc Simulate(AgentsActivityData action)
                {
                    if (Mode == PlayMode.PracticeMode)
                    {
                        foreach (Team team in Enum.GetValues(typeof(Team)))
                        {
                            foreach (AgentNumber agent in Enum.GetValues(typeof(AgentNumber)))
                            {
                                Log.WriteLine(Calc.Agents[team, agent].Name);
                                Log.WriteLine("   {" + action[team, agent].AgentStatusData + "," + action[team, agent].Destination + "}");
                                Log.WriteLine("=> {" + action[team, agent].AgentStatusData + "," + action[team, agent].Destination + "}");
                            }
                        }
                    }
                    Calc.MoveAgent(action.DeepClone());
                    var c = new Calc(new XmlCalc(Calc).DeepClone());
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
                    if (Mode == PlayMode.PracticeMode)
                    {
                        foreach (AgentNumber agent in Enum.GetValues(typeof(AgentNumber)))
                        {
                            Log.WriteLine(Calc.Agents[team, agent].Name);
                            Log.WriteLine("   {" + action[(int)agent].AgentStatusData + "," + action[(int)agent].Destination + "}");
                            Log.WriteLine("=> {" + action[(int)agent].AgentStatusData + "," + action[(int)agent].Destination + "}");
                        }
                    }
                    Calc.MoveAgent(team, action.DeepClone());
                    var c = new Calc(new XmlCalc(Calc).DeepClone());
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
                    if (Mode == PlayMode.PracticeMode)
                    {
                        Log.WriteLine(Calc.Agents[team, agentNumber].Name);
                        Log.WriteLine("   {" + action.AgentStatusData + "," + action.Destination + "}");
                        Log.WriteLine("=> {" + action.AgentStatusData + "," + action.Destination + "}");
                    }
                    Calc.MoveAgent(team, agentNumber, action.DeepClone());
                    var c = new Calc(new XmlCalc(Calc).DeepClone());
                    Calc.Undo();
                    return c;
                }
            }
        }
    }
}