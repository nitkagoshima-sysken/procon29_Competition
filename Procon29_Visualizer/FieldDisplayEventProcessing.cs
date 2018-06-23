using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Procon29_Visualizer
{
    class FieldDisplayEventProcessing
    {
        Calc calc;
        Team selectedTeam;
        Agent selectedAgent;
        Point clickedField;
        AgentActivityData[,] agentActivityData = new AgentActivityData[2,2];

        internal Calc Calc { get => calc; set => calc = value; }
        internal Team SelectedTeam { get => selectedTeam; set => selectedTeam = value; }
        internal Agent SelectedAgent { get => selectedAgent; set => selectedAgent = value; }
        public Point ClickedField { get => clickedField; set => clickedField = value; }
        internal AgentActivityData[,] AgentActivityData { get => agentActivityData; set => agentActivityData = value; }

        FieldDisplayEventProcessing(Calc calc)
        {
            Calc = calc;
        }

        /// <summary>
        /// 特定のエージェントに座標を加えます。
        /// </summary>
        /// <param name="team">対称となるチーム</param>
        /// <param name="agent">対称となるエージェント</param>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point AddAgentPosition(Team team, Agent agent, Point point)
        {
            return new Point(calc.AgentPosition[(int)team, (int)agent].X + point.X, calc.AgentPosition[(int)team, (int)agent].Y + point.Y);
        }

        /// <summary>
        /// Form1内でキーを押したときに実行されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            Console.WriteLine(e.KeyCode);
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Q:
                        SelectedTeam = Team.A;
                        SelectedAgent = Agent.One;
                        break;
                    case Keys.W:
                        SelectedTeam = Team.A;
                        SelectedAgent = Agent.Two;
                        break;
                    case Keys.E:
                        SelectedTeam = Team.B;
                        SelectedAgent = Agent.One;
                        break;
                    case Keys.R:
                        SelectedTeam = Team.B;
                        SelectedAgent = Agent.Two;
                        break;
                    case Keys.NumPad1:
                        AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination =
                            AddAgentPosition(SelectedTeam, SelectedAgent, new Point(-1, 1));
                        break;
                    case Keys.NumPad2:
                        AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination =
                            AddAgentPosition(SelectedTeam, SelectedAgent, new Point(0, 1));
                        break;
                    case Keys.NumPad3:
                        AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination =
                            AddAgentPosition(SelectedTeam, SelectedAgent, new Point(1, 1));
                        break;
                    case Keys.NumPad4:
                        AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination =
                            AddAgentPosition(SelectedTeam, SelectedAgent, new Point(-1, 0));
                        break;
                    case Keys.NumPad6:
                        AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination =
                            AddAgentPosition(SelectedTeam, SelectedAgent, new Point(1, 0));
                        break;
                    case Keys.NumPad7:
                        AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination =
                            AddAgentPosition(SelectedTeam, SelectedAgent, new Point(-1, -1));
                        break;
                    case Keys.NumPad8:
                        AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination =
                            AddAgentPosition(SelectedTeam, SelectedAgent, new Point(0, -1));
                        break;
                    case Keys.NumPad9:
                        AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination =
                            AddAgentPosition(SelectedTeam, SelectedAgent, new Point(1, -1));
                        break;
                    default:
                        e.SuppressKeyPress = false;
                        break;
                }
                ClickedField = Calc.AgentPosition[(int)SelectedTeam, (int)SelectedAgent];
                if (AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination.X < 0 ||
                    AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination.Y < 0 ||
                    AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination.X >= Calc.Field.Width() ||
                    AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination.Y >= Calc.Field.Height())
                {
                    AgentActivityData[(int)SelectedTeam, (int)SelectedAgent].Destination = Calc.AgentPosition[(int)SelectedTeam, (int)SelectedAgent];
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("不正なキー入力です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
