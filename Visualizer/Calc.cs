﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// procon29におけるフィールドの管理、ポイント計算などの全般を行います。
    /// </summary>
    public class Calc
    {
        int maxTurn;

        /// <summary>
        /// エージェントたちを表します
        /// </summary>
        public Agents Agents { get => FieldHistory[Turn].Agents; set => FieldHistory[Turn].Agents = value; }

        /// <summary>
        /// フィールドを設定または取得します。
        /// </summary>
        public Field Field { get => FieldHistory[Turn].Field; set => FieldHistory[Turn].Field = value; }

        /// <summary>
        /// ターンを設定または取得します。
        /// </summary>
        public int Turn { get; private set; }

        /// <summary> 
        /// ターンの終わりを設定または取得します。 
        /// </summary> 
        public int MaxTurn
        {
            get => maxTurn;
            set => maxTurn = (value <= 0) ? 1 : value;
        }

        /// <summary>
        /// フィールドの歴史を設定または取得します。
        /// </summary>
        public List<TurnData> FieldHistory { get; private set; } = new List<TurnData>();

        /// <summary> 
        /// エージェントの略称を返します。 
        /// </summary> 
        public static string[,] ShortTeamAgentName => new string[2, 2] { { "Strawberry", "Apple", }, { "Kiwi", "Muscat", }, };

        /// <summary> 
        /// Team列挙体のすべての要素を配列にします 
        /// </summary> 
        public static Array TeamArray => Enum.GetValues(typeof(Team));

        /// <summary> 
        /// Agent列挙体のすべての要素を配列にします 
        /// </summary> 
        public static Array AgentArray => Enum.GetValues(typeof(AgentNumber));

        /// <summary>
        /// 現在のobjectのディープコピーを行います。
        /// </summary>
        /// <returns>objectのディープコピー</returns>
        public object DeepCopy()
        {
            return new Calc((BaseCalc)(new BaseCalc(this)).DeepCopy());
        }

        /// <summary>
        /// Calcを初期化します。
        /// </summary>
        /// <param name="calc"></param>
        public Calc(BaseCalc calc)
        {
            MaxTurn = calc.MaxTurn;
            Turn = calc.Turn;
            FieldHistory = calc.FieldHistory;
        }

        /// <summary>
        /// Calcを初期化します。
        /// </summary>
        /// <param name="maxTurn">最大ターン数を設定します。</param>
        /// <param name="point">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Calc(int maxTurn, int[,] point, Coordinate[] initPosition)
        {
            MaxTurn = maxTurn;
            // Turn -> 0
            Turn = 0;
            // TurnData作成
            FieldHistory.Add(new TurnData(new Field(point.GetLength(1), point.GetLength(0)), new Agents()));

            InitializationOfField(point);
            foreach (AgentNumber agent in AgentArray)
            {
                Agents[Team.A, agent].Position = initPosition[(int)agent];
                PutTile(team: 0, agent: agent);
            }
            ComplementEnemysPosition();

            // Turn -> 1
            TurnEnd();
        }

        /// <summary>
        /// Calcを初期化します。
        /// </summary>
        /// <param name="maxTurn">最大ターン数を設定します。</param>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Calc(int maxTurn, Field field, Coordinate[] initPosition)
        {
            MaxTurn = maxTurn;
            // Turn -> 0
            Turn = 0;
            // TurnData作成
            FieldHistory.Add(new TurnData(field, new Agents()));

            foreach (AgentNumber agent in AgentArray)
            {
                Agents[Team.A, agent].Position = initPosition[(int)agent];
                PutTile(team: 0, agent: agent);
            }
            ComplementEnemysPosition();

            // Turn -> 1
            TurnEnd();
        }

        /// <summary>
        /// QRコードには自分のチームの位置情報しか分からないため、
        /// 敵の位置情報を自分の位置から補完します。
        /// </summary>
        private void ComplementEnemysPosition()
        {
            PointMapCheck();
            if (Field.IsVerticallySymmetrical)
            {
                Agents[Team.B, AgentNumber.One].Position = Field.FlipVertical(Agents[Team.A, AgentNumber.One].Position);
                Agents[Team.B, AgentNumber.Two].Position = Field.FlipVertical(Agents[Team.A, AgentNumber.Two].Position);
            }
            else if (Field.IsHorizontallySymmetrical)
            {
                Agents[Team.B, AgentNumber.One].Position = Field.FlipHorizontal(Agents[Team.A, AgentNumber.One].Position);
                Agents[Team.B, AgentNumber.Two].Position = Field.FlipHorizontal(Agents[Team.A, AgentNumber.Two].Position);
            }
            PutTile(Team.B, AgentNumber.One);
            PutTile(Team.B, AgentNumber.Two);
        }

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="point"></param>
        private void InitializationOfField(int[,] point)
        {
            Field = new Field(point.GetLength(1), point.GetLength(0));
            for (int x = 0; x < Field.Width; x++)
            {
                for (int y = 0; y < Field.Height; y++)
                {
                    Field[x, y] = new Cell { Point = point[y, x], Coordinate = new Coordinate(x, y) };
                }
            }
        }

        /// <summary> 
        /// フィールドのリストを返します。 
        /// </summary> 
        [Obsolete("わざわざFieldListを使わなくてもFieldでリストのように扱えるようになりました")]
        public List<Cell> FieldList
        {
            get
            {
                var list = new List<Cell>();
                foreach (Cell cell in Field.GetEnumerator())
                {
                    list.Add(cell);
                }
                return list;
            }
        }

        public int CalcPoint(Func<Cell, bool> func) => FieldList.Sum(x => (func(x)) ? x.Point : 0);

        /// <summary> 
        /// すべてのフィールドのポイントの和を計算します。 
        /// </summary> 
        /// <returns>すべてのフィールドのポイントの和</returns> 
        public int Sum() => FieldList.Sum(x => x.Point);

        /// <summary> 
        /// すべてのフィールドのポイントの絶対値の和を計算します。 
        /// </summary> 
        /// <returns>すべてのフィールドのポイントの絶対値の和</returns> 
        public int SumAbs() => FieldList.Sum(x => ((x.Point > 0) ? x.Point : -x.Point));

        /// <summary> 
        /// 指定したチームの直接的なエリアのポイントの合計を計算します。 
        /// </summary> 
        /// <param name="team">計算するチーム</param> 
        /// <returns>指定したチームの直接的なエリアのポイントの合計</returns> 
        public int AreaPoint(Team team) => FieldList.Sum(x => ((x.IsTileOn[team] == true) ? x.Point : 0));

        /// <summary> 
        /// 指定したチームが囲んだエリアのポイントの絶対値の合計を計算します。 
        /// </summary> 
        /// <param name="team">計算するチーム</param> 
        /// <returns>指定したチームが囲んだエリアのポイントの絶対値の合計</returns> 
        public int EnclosedPoint(Team team) => FieldList.Sum(x => ((x.IsEnclosed[team] == true) ? Math.Abs(x.Point) : 0));

        /// <summary> 
        /// 指定したチームの合計ポイントを計算します。 
        /// </summary> 
        /// <param name="team">計算するチーム</param> 
        /// <returns>指定したチームの合計ポイント</returns> 
        public int TotalPoint(Team team) => AreaPoint(team) + EnclosedPoint(team);

        /// <summary> 
        /// マップが対称であるか規定内の大きさか判定します。 
        /// </summary> 
        public void PointMapCheck()
        {
            if (Field.Width > 12 || Field.Height > 12) throw new IndexOutOfRangeException();
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
            foreach (Cell cell in Field.GetEnumerator())
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
            foreach (Cell item in Field.GetEnumerator())
            {
                if (item.IsTileOn[team]) item.IsEnclosed[team] = false;
            }
        }

        /// <summary>
        /// すべてのチームにおいてフィールドが囲まれているか判定します。
        /// </summary>
        private void CheckEnclosedArea()
        {
            foreach (Team team in TeamArray)
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
                // TurnData作成 
                var a = new Agents();
                for (Team team = 0; (int)team < TeamArray.Length; team++)
                {
                    for (AgentNumber agent = 0; (int)agent < AgentArray.Length; agent++)
                    {
                        a[team, agent] = new Agent(Agents[team, agent]);
                    }
                }
                FieldHistory.Add(new TurnData(new Field(Field), a));
                Turn++;
            }
        }

        /// <summary> 
        /// ターンを元に戻します 
        /// </summary> 
        public void Undo()
        {
            if (Turn == 1) return;
            Turn--;
        }

        /// <summary> 
        /// ターンをやり直します 
        /// </summary> 
        public void Redo()
        {
            if (Turn == FieldHistory.Count - 1) return;
            Turn++;
        }

        /// <summary> 
        /// 自分のフィールドにタイルを置きます。 
        /// </summary> 
        /// <param name="team">対象となるチーム</param> 
        /// <param name="agent">対象となるエージェント</param> 
        public void PutTile(Team team, AgentNumber agent) => Field[Agents[team, agent].Position].IsTileOn[team] = true;

        /// <summary> 
        /// フィールドに置いてあるタイルを剥がします。 
        /// </summary> 
        /// <param name="point">対象となるエージェント</param> 
        public void RemoveTile(Coordinate point)
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                Field[point.X, point.Y].IsTileOn[team] = false;
            }
        }

        /// <summary> 
        /// 指定したところにエージェントが移動します。 
        /// </summary> 
        /// <param name="team">移動するチーム</param> 
        /// <param name="agent">移動するエージェント</param> 
        /// <param name="where">移動する場所</param> 
        public void MoveAgent(Team team, AgentNumber agent, Coordinate where)
        {

            bool movable = false;
            foreach (Team otherteam in TeamArray)
            {
                if (otherteam != team)
                {
                    movable = Field[where.X, where.Y].IsTileOn[otherteam];
                    if (movable)
                    {
                        RemoveTile(point: where);
                        CheckEnclosedArea(otherteam);
                        break;
                    }
                }
            }
            if (!movable)
            {
                Agents[team, agent].Position = where;
                PutTile(team: team, agent: agent);
            }

            CheckEnclosedArea(team);

            foreach (Cell item in Field.GetEnumerator())
            {
                if (item.IsTileOn[Team.A]) item.IsEnclosed[Team.A] = false;
                if (item.IsTileOn[Team.B]) item.IsEnclosed[Team.B] = false;
            }
        }

        void CheckAgentActivityData(AgentsActivityData agentActivityData)
        {
            foreach (Team team in TeamArray)
            {
                foreach (AgentNumber agent in AgentArray)
                {
                    var item = agentActivityData[team, agent];
                    // 何もしないのは、無条件で成功する(1)
                    // SucceededNotToDoAnything
                    if (item.AgentStatusData == AgentStatusCode.RequestNotToDoAnything)
                    {
                        item.ToSuccess();
                        continue;
                    }
                    // リクエストの禁止や、何も命令がないときは無条件でスキップ(2)
                    if (item.AgentStatusData == AgentStatusCode.NotDoneAnything ||
                        item.AgentStatusData == AgentStatusCode.RequestForbidden)
                        continue;
                    // 自分自身の衝突をチェック(2)
                    // YouHadCollisionsWithYourselfAndYouFailedToMoveBecauseYouAreThereAlready
                    // YouHadCollisionsWithYourselfAndYouFailedToRemoveTilesFromYourTeam;
                    if (item.Destination == Agents[team, agent].Position)
                        switch (item.AgentStatusData)
                        {
                            case AgentStatusCode.RequestMovement:
                                item.AgentStatusData = AgentStatusCode.YouHadCollisionsWithYourselfAndYouFailedToMoveBecauseYouAreThereAlready;
                                continue;
                            case AgentStatusCode.RequestRemovementOurTile:
                                item.AgentStatusData = AgentStatusCode.YouHadCollisionsWithYourselfAndYouFailedToRemoveTilesFromYourTeamBecauseYouAreThere;
                                continue;
                            default:
                                break;
                        }
                    // 目標部が自分から遠い場所にないかチェック(3)
                    // FailedInMovingByBeingNotChebyshevNeighborhood
                    // FailedInRemovingOurTileByBeingNotChebyshevNeighborhood
                    // FailedInRemovingOpponentTileByBeingNotChebyshevNeighborhood
                    if (item.Destination.ChebyshevDistance(Agents[team, agent].Position) != 1)
                        switch (item.AgentStatusData)
                        {
                            case AgentStatusCode.RequestMovement:
                                item.AgentStatusData = AgentStatusCode.FailedInMovingByBeingNotMooreNeighborhood;
                                continue;
                            case AgentStatusCode.RequestRemovementOurTile:
                                item.AgentStatusData = AgentStatusCode.FailedInRemovingOurTileByBeingNotMooreNeighborhood;
                                continue;
                            case AgentStatusCode.RequestRemovementOpponentTile:
                                item.AgentStatusData = AgentStatusCode.FailedInRemovingOpponentTileByBeingNotMooreNeighborhood;
                                continue;
                            default:
                                break;
                        }
                    // 目標部がフィールドの外にないかチェック(3)
                    // FailedInMovingByTryingToGoOutOfTheFieldWithEachOther
                    // FailedInRemovingOurTileByTryingToGoOutOfTheField
                    // FailedInRemovingOpponentTileByTryingToGoOutOfTheField
                    if (item.Destination.X < 0 || Field.Width <= item.Destination.X ||
                        item.Destination.Y < 0 || Field.Height <= item.Destination.Y)
                        switch (item.AgentStatusData)
                        {
                            case AgentStatusCode.RequestMovement:
                                item.AgentStatusData = AgentStatusCode.FailedInMovingByTryingToGoOutOfTheField;
                                continue;
                            case AgentStatusCode.RequestRemovementOurTile:
                                item.AgentStatusData = AgentStatusCode.FailedInRemovingOurTileByTryingToGoOutOfTheField;
                                continue;
                            case AgentStatusCode.RequestRemovementOpponentTile:
                                item.AgentStatusData = AgentStatusCode.FailedInRemovingOpponentTileByTryingToGoOutOfTheField;
                                continue;
                            default:
                                break;
                        }
                    // 剥がそうとしていたタイルが存在していないかチェック(2)
                    // FailedInRemovingOurTileByDoingTileNotExist
                    // FailedInRemovingOpponentTileByDoingTileNotExist
                    if (item.AgentStatusData == AgentStatusCode.RequestRemovementOurTile &&
                        Field[item.Destination.X, item.Destination.Y].IsTileOn[team] == false)
                    {
                        item.AgentStatusData = AgentStatusCode.FailedInRemovingOurTileByDoingTileNotExist;
                        continue;
                    }
                    if (item.AgentStatusData == AgentStatusCode.RequestRemovementOpponentTile &&
                        Field[item.Destination.X, item.Destination.Y].IsTileOn[team.Opponent()] == false)
                    {
                        item.AgentStatusData = AgentStatusCode.FailedInRemovingOpponentTileByDoingTileNotExist;
                        continue;
                    }
                    // 移動先に相手のタイルが置いてないかチェック(1)
                    // FailedInMovingByTryingItWithoutRemovingTheOpponentTile
                    if (item.AgentStatusData == AgentStatusCode.RequestMovement &&
                        Field[item.Destination].IsTileOn[team.Opponent()])
                    {
                        item.AgentStatusData = AgentStatusCode.FailedInMovingByTryingItWithoutRemovingTheOpponentTile;
                        continue;
                    }
                    // 動かないエージェントに衝突していないかチェック(4)
                    // FailedInMovingByCollisionWithTheLazyOurTeam
                    // FailedInRemovingOurTileWithTheLazyOurTeam
                    // FailedInMovingByCollisionWithTheLazyOpponent
                    // FailedInRemovingOpponentTileWithTheLazyOpponent
                    foreach (Team otherteam in TeamArray)
                    {
                        foreach (AgentNumber otheragent in AgentArray)
                        {
                            if (team == otherteam && agent == otheragent) continue;
                            var otheritem = agentActivityData[otherteam, otheragent];
                            var otherposition = Agents[otherteam, otheragent].Position;
                            if ((otheritem.AgentStatusData == AgentStatusCode.RequestNotToDoAnything ||
                                otheritem.AgentStatusData == AgentStatusCode.NotDoneAnything) &&
                                item.Destination == otherposition)
                            {
                                if (team == otherteam)
                                {
                                    item.ToFailByCollisionWithTheLazyOurTeam();
                                }
                                else
                                {
                                    item.ToFailByCollisionWithTheLazyOpponent();
                                }
                            }
                        }
                    }
                }
            }
            // 自分または相手のチームと衝突していないかチェック(6)
            // FailedInMovingBySelfCollision;
            // FailedInRemovingOurTileBySelfCollision;
            // FailedInRemovingOpponentTileBySelfCollision;
            // FailedInMovingByCollisionWithEachOther;
            // FailedInRemovingOurTileByCollisionWithEachOther;
            // FailedInRemovingOpponentTileByCollisionWithEachOther;
            agentActivityData.CheckCollision();
            // 移動先のエージェントがコリジョンを起こし、
            // 自分もそのコリジョンに巻き込まれたかチェック(3)
            // FailedInMovingByInvolvedInOtherCollisions
            // FailedInRemovingOpponentTileByInvolvedInOtherCollisions
            // FailedInRemovingOurTileByInvolvedInOtherCollisions
            foreach (Team team in TeamArray)
            {
                foreach (AgentNumber agent in AgentArray)
                {
                    var item = agentActivityData[team, agent];
                    foreach (Team otherteam in TeamArray)
                    {
                        foreach (AgentNumber otheragent in AgentArray)
                        {
                            if (team == otherteam && agent == otheragent) continue;
                            var otheritem = agentActivityData[otherteam, otheragent];
                            var otherposition = Agents[otherteam, otheragent].Position;
                            if ((otheritem.AgentStatusData == AgentStatusCode.RequestNotToDoAnything ||
                                otheritem.AgentStatusData == AgentStatusCode.NotDoneAnything ||
                                otheritem.AgentStatusData.IsFailed()) &&
                                item.Destination == otherposition)
                            {
                                switch (item.AgentStatusData)
                                {
                                    case AgentStatusCode.RequestMovement:
                                        item.AgentStatusData = AgentStatusCode.FailedInMovingByInvolvedInOtherCollisions;
                                        break;
                                    case AgentStatusCode.RequestRemovementOurTile:
                                        item.AgentStatusData = AgentStatusCode.FailedInRemovingOpponentTileByInvolvedInOtherCollisions;
                                        break;
                                    case AgentStatusCode.RequestRemovementOpponentTile:
                                        item.AgentStatusData = AgentStatusCode.FailedInRemovingOurTileByInvolvedInOtherCollisions;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            // 全チェック後に残ったリクエストは、成功したとみなす(3)
            foreach (var item in agentActivityData)
            {
                item.ToSuccess();
            }
        }

        /// <summary> 
        /// 指定したところにエージェントが移動します。 
        /// </summary> 
        /// <param name="agentActivityData"></param> 
        public void MoveAgent(AgentsActivityData agentActivityData)
        {
            if (Turn < MaxTurn)
            {
                // Undoしたときに 
                // FieldHistory.Count != Turn 
                // になる 
                // TurnEndしたときにやり直す前の未来を消す 
                if (FieldHistory.Count - 1 != Turn)
                    FieldHistory.RemoveRange(Turn + 1, FieldHistory.Count - 1 - Turn);

                // 不正な動きをしていないかチェック 
                CheckAgentActivityData(agentActivityData);

                // ターンエンドの処理 
                TurnEnd();
                // ログを取る 
                FieldHistory[Turn - 1].AgentActivityDatas[Team.A, AgentNumber.One].AgentStatusData = agentActivityData[Team.A, AgentNumber.One].AgentStatusData;
                FieldHistory[Turn - 1].AgentActivityDatas[Team.A, AgentNumber.Two].AgentStatusData = agentActivityData[Team.A, AgentNumber.Two].AgentStatusData;
                FieldHistory[Turn - 1].AgentActivityDatas[Team.B, AgentNumber.One].AgentStatusData = agentActivityData[Team.B, AgentNumber.One].AgentStatusData;
                FieldHistory[Turn - 1].AgentActivityDatas[Team.B, AgentNumber.Two].AgentStatusData = agentActivityData[Team.B, AgentNumber.Two].AgentStatusData;

                foreach (Team team in TeamArray)
                {
                    foreach (AgentNumber agent in AgentArray)
                    {
                        switch (agentActivityData[team, agent].AgentStatusData)
                        {
                            case AgentStatusCode.SucceededInMoving:
                                Agents[team, agent].Position = agentActivityData[team, agent].Destination;
                                PutTile(team: team, agent: agent);
                                break;
                            case AgentStatusCode.SucceededInRemovingOurTile:
                                RemoveTile(agentActivityData[team, agent].Destination);
                                break;
                            case AgentStatusCode.SucceededInRemovingOpponentTile:
                                RemoveTile(agentActivityData[team, agent].Destination);
                                break;
                            default:
                                break;
                        }
                        agentActivityData[team, agent].ToSuccess();
                    }
                }
                CheckEnclosedArea();
            }
        }

        /// <summary> 
        /// 指定したところにエージェントが移動します。 
        /// </summary> 
        /// <param name="team">移動させるチーム</param> 
        /// <param name="agentActivityData"></param> 
        public void MoveAgent(Team team, AgentActivityData[] agentActivityData)
        {
            var agentActivityDatas = new AgentsActivityData();
            switch (team)
            {
                case Team.A:
                    agentActivityDatas[Team.A, AgentNumber.One] = agentActivityData[0];
                    agentActivityDatas[Team.A, AgentNumber.Two] = agentActivityData[1];
                    agentActivityDatas[Team.B, AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                    agentActivityDatas[Team.B, AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                    break;
                case Team.B:
                    agentActivityDatas[Team.A, AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                    agentActivityDatas[Team.A, AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                    agentActivityDatas[Team.B, AgentNumber.One] = agentActivityData[0];
                    agentActivityDatas[Team.B, AgentNumber.Two] = agentActivityData[1];
                    break;
            }
            MoveAgent(agentActivityDatas);
        }

        /// <summary> 
        /// 指定したところにエージェントが移動します。 
        /// </summary> 
        /// <param name="team">移動させるチーム</param> 
        /// <param name="agent">移動させるエージェント</param> 
        /// <param name="agentActivityData"></param> 
        public void MoveAgent(Team team, AgentNumber agent, AgentActivityData agentActivityData)
        {
            var agentActivityDatas = new AgentsActivityData();
            switch (team)
            {
                case Team.A:
                    switch (agent)
                    {
                        case AgentNumber.One:
                            agentActivityDatas[Team.A, AgentNumber.One] = agentActivityData;
                            agentActivityDatas[Team.A, AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.B, AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.B, AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            break;
                        case AgentNumber.Two:
                            agentActivityDatas[Team.A, AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.A, AgentNumber.Two] = agentActivityData;
                            agentActivityDatas[Team.B, AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.B, AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            break;
                    }
                    break;
                case Team.B:
                    switch (agent)
                    {
                        case AgentNumber.One:
                            agentActivityDatas[Team.A, AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.A, AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.B, AgentNumber.One] = agentActivityData;
                            agentActivityDatas[Team.B, AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            break;
                        case AgentNumber.Two:
                            agentActivityDatas[Team.A, AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.A, AgentNumber.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.B, AgentNumber.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[Team.B, AgentNumber.Two] = agentActivityData;
                            break;
                    }
                    break;
            }
            MoveAgent(agentActivityDatas);
        }
    }
}
