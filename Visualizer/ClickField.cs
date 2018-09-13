using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// フィールドをクリックするときに反応するクラスです。
    /// </summary>
    public class ClickField
    {
        /// <summary>
        /// エージェントアクティビティデータを設定または取得します。
        /// </summary>
        public AgentsActivityData AgentsActivityData { get; set; } = new AgentsActivityData();

        /// <summary>
        /// 描画する対象となる計算機を設定または取得します。
        /// </summary>
        public Calc Calc { get; set; }

        /// <summary>
        /// クリックされたエージェントを設定または取得します。
        /// </summary>
        public Agent ClickedAgent { get; set; }

        /// <summary>
        /// 描画するピクチャーボックスを設定または取得します。
        /// </summary>
        public PictureBox PictureBox { get; set; }

        public ClickField()
        {
        }

        public ClickField(Calc calc, PictureBox pictureBox)
        {
            Calc = calc;
            ClickedAgent = calc.Agents[Team.A, AgentNumber.One];
            PictureBox = pictureBox;
            foreach (var agent in Calc.Agents)
            {
                AgentsActivityData[agent.Team, agent.AgentNumber].Destination = agent.Position;
            }
        }

        /// <summary>
        /// クリックしたときに反応します。
        /// </summary>
        /// <returns></returns>
        public void Click()
        {
            var coordinate = CursorPosition(PictureBox);
            var agentsActivityData = new AgentsActivityData();
            try
            {
                foreach (var agent in Calc.Agents)
                {
                    if (coordinate == agent.Position)
                    {
                        ClickedAgent = agent;
                        return;
                    }
                }
                foreach (var agent in Calc.Agents)
                {
                    if (ClickedAgent == agent && coordinate.ChebyshevDistance(agent.Position) == 1)
                    {
                        MakeRequest(agent, coordinate);
                        return;
                    }
                    else if (coordinate.ChebyshevDistance(agent.Position) == 1 && coordinate.ChebyshevDistance(ClickedAgent.Position) != 1)
                    {
                        MakeRequest(agent, coordinate);
                        return;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// クリックによってリクエストを作成します。
        /// </summary>
        /// <param name="agent">対象のエージェント</param>
        /// <param name="coordinate">移動先</param>
        /// <returns></returns>
        public void MakeRequest(Agent agent, Coordinate coordinate)
        {
            if (Calc.Field[coordinate].IsTileOn[agent.Team])
            {
                DialogResult result = MessageBox.Show("タイルを取り除きますか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                switch (result)
                {
                    case DialogResult.Yes:
                        AgentsActivityData[agent.Team, agent.AgentNumber].AgentStatusData = AgentStatusCode.RequestRemovementOurTile;
                        break;
                    case DialogResult.No:
                        AgentsActivityData[agent.Team, agent.AgentNumber].AgentStatusData = AgentStatusCode.RequestMovement;
                        break;
                }
            }
            else if (Calc.Field[coordinate].IsTileOn[agent.Team.Opponent()])
            {
                AgentsActivityData[agent.Team, agent.AgentNumber].AgentStatusData = AgentStatusCode.RequestRemovementOpponentTile;
            }
            else
            {
                AgentsActivityData[agent.Team, agent.AgentNumber].AgentStatusData = AgentStatusCode.RequestMovement;
            }
            AgentsActivityData[agent.Team, agent.AgentNumber].Destination = coordinate;
        }

        /// <summary>
        /// カーソルがどのフィールドの上にいるかを計算します。
        /// </summary>
        /// <returns></returns>
        public Coordinate CursorPosition(PictureBox pictureBox)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / Calc.Field.Width;
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / Calc.Field.Height;
            Point systemCursorPosition = Cursor.Position;
            Point pictureBoxCursorPosition = pictureBox.PointToClient(systemCursorPosition);
            return new Coordinate(
                x: pictureBoxCursorPosition.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: pictureBoxCursorPosition.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
        }
    }
}
