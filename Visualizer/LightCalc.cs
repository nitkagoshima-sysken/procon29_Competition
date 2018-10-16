using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// Procon 29における軽い計算機です。フィールドの管理、ポイント計算などの全般を行います。
    /// </summary>
    class LightCalc
    {
        private int maxTurn;

        /// <summary>
        /// エージェントたちを表します
        /// </summary>
        public Agents Agents { get; set; }

        /// <summary>
        /// フィールドを設定または取得します。
        /// </summary>
        public Field Field { get; set; }

        /// <summary>
        /// ターンを設定または取得します。
        /// </summary>
        public int Turn { get; protected set; } = 0;

        /// <summary> 
        /// ターンの終わりを設定または取得します。 
        /// </summary> 
        public int MaxTurn
        {
            get { return maxTurn; }
            set { maxTurn = (value <= 0) ? 1 : value; }
        }

        /// <summary>
        /// ゲームが終了したか判定します。
        /// </summary>
        public bool IsEnd
        {
            get { return Turn >= MaxTurn; }
        }

        /// <summary>
        /// LightCalc を初期化します。
        /// </summary>
        /// <param name="turn">最大ターン数を設定します。</param>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="agents">エージェントを設定します。</param>
        public LightCalc(int turn, Field field, Agents agents)
        {
            Field = field;
            Agents = agents;
            MaxTurn = turn;
            foreach (var agent in Agents)
            {
                PutTile(team: agent.Team, agent: agent.AgentNumber);
            }
        }

        /// <summary>
        /// そのフィールドが塗れるか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">対象となるフィールド</param>
        /// <returns>そのフィールドが塗れるなら真、そうでなければ偽が返ってきます。</returns>
        bool IsFillable(Team team, Coordinate point) => 0 <= point.X && point.X < Field.Width && 0 <= point.Y && point.Y < Field.Height && !Field[point.X, point.Y].IsTileOn[team];

        /// <summary>
        /// 指定したフィールドを基準にIsEnclosedをfalseで塗りつぶします。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">始点にするフィールド</param>
        private void FillFalseInIsEnclosed(Team team, Coordinate point)
        {
            Stack<Coordinate> stack = new Stack<Coordinate>();
            stack.Push(point);

            while (stack.Count > 0)
            {
                point = stack.Pop();
                if (IsFillable(team, point)) //make sure we stay within bounds
                {
                    if (Field[point.X, point.Y].IsEnclosed[team] == true)
                    {
                        Field[point.X, point.Y].IsEnclosed[team] = false;
                        stack.Push(new Coordinate(point.X - 1, point.Y));
                        stack.Push(new Coordinate(point.X + 1, point.Y));
                        stack.Push(new Coordinate(point.X, point.Y - 1));
                        stack.Push(new Coordinate(point.X, point.Y + 1));
                    }
                }
            }
            return;
        }

        /// <summary>
        /// 対象となるチームのIsEnclosedをTrueで初期化します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void ResetTrueInIsEnclosed(Team team)
        {
            foreach (Cell cell in Field)
            {
                cell.IsEnclosed[team] = true;
            }
        }

        /// <summary>
        /// すべてのチームのIsEnclosedをTrueで初期化します。
        /// </summary>
        private void ResetTrueInIsEnclosed()
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                ResetTrueInIsEnclosed(team);
            }
        }

        /// <summary>
        /// 対象となるチームにおいてフィールドが囲まれているか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void CheckEnclosedArea(Team team)
        {
            ResetTrueInIsEnclosed(team);
            for (int x = 0; x < Field.Width; x++)
            {
                for (int y = 0; y < Field.Height; y++)
                {
                    if (x == 0 || x == Field.Width - 1 || y == 0 || y == Field.Height - 1)
                        FillFalseInIsEnclosed(team, new Coordinate(x, y));
                }
            }
            foreach (Cell item in Field)
            {
                if (item.IsTileOn[team]) item.IsEnclosed[team] = false;
            }
        }

        /// <summary>
        /// すべてのチームにおいてフィールドが囲まれているか判定します。
        /// </summary>
        private void CheckEnclosedArea()
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                CheckEnclosedArea(team);
            }
        }

        /// <summary> 
        /// 対象となるマスに対象となるエージェントがいるか、またはムーア近傍にいるかを判定します 
        /// </summary> 
        /// <param name="team">対象となるチーム</param> 
        /// <param name="agentNumber">対象となるエージェント</param> 
        /// <param name="point">対象となるマス</param> 
        /// <returns>対象となるマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns> 
        public bool IsAgentHereOrInMooreNeighborhood(Team team, AgentNumber agentNumber, Coordinate point) => (Agents[team, agentNumber].Position.ChebyshevDistance(point) <= 1) ? true : false;

        /// <summary> 
        /// 対象となるマスにエージェントがいるか、またはムーア近傍にいるかを判定します 
        /// </summary> 
        /// <param name="point">対象となるマス</param> 
        /// <returns>そのマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns> 
        public bool IsAgentHereOrInMooreNeighborhood(Coordinate point)
        {
            foreach (Agent agent in Agents)
                if (agent.Position.ChebyshevDistance(point) <= 1)
                    return true;
            return false;
        }

        /// <summary> 
        /// 対象となるマスに一人だけ、エージェントがいるか、またはムーア近傍にいるかを判定します 
        /// </summary> 
        /// <param name="point">対象になるマス</param> 
        /// <returns></returns> 
        public bool IsOneAgentHereOrInMooreNeighborhood(Coordinate point)
        {
            var result = false;
            foreach (Agent agent in Agents)
                if (agent.Position.ChebyshevDistance(point) <= 1)
                    if (result)
                        return false;
                    else
                        result = true;
            return true;
        }

        /// <summary> 
        /// 対象になるマスにエージェントがいるか判定します。 
        /// </summary> 
        /// <param name="point">対象になるマス</param> 
        /// <returns></returns> 
        public bool IsAgentHere(Coordinate point)
        {
            foreach (Agent agent in Agents)
                if (agent.Position.ChebyshevDistance(point) == 0)
                    return true;
            return false;
        }

        /// <summary> 
        /// 対象になるマスのムーア近傍にエージェントがいるか判定します。 
        /// </summary> 
        /// <param name="point">対象になるマス</param> 
        /// <returns></returns> 
        public bool IsAgentInMooreNeighborhood(Coordinate point)
        {
            foreach (Agent agent in Agents)
                if (agent.Position.ChebyshevDistance(point) == 1)
                    return true;
            return false;
        }

        /// <summary> 
        /// 対象になるマスのムーア近傍に一人だけエージェントがいるか判定します。 
        /// </summary> 
        /// <param name="point">対象になるマス</param> 
        /// <returns></returns> 
        public bool IsOneAgentInMooreNeighborhood(Coordinate point)
        {
            var result = false;
            foreach (Agent agent in Agents)
                if (agent.Position.ChebyshevDistance(point) == 1)
                    if (result)
                        return false;
                    else
                        result = true;
            return true;
        }

        /// <summary> 
        /// ターンエンドします。 
        /// </summary> 
        public void TurnEnd()
        {
            if (Turn < MaxTurn)
            {
                var agents = new Agents();
                foreach (var agent in Agents)
                {
                    agents[agent.Team, agent.AgentNumber] = new Agent(Agents[agent.Team, agent.AgentNumber]);
                }
                Agents = agents;
                Turn++;
            }
        }


    }
}
