﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    /// <summary>
    /// procon29におけるフィールドの管理、ポイント計算などの全般を行います。
    /// </summary>
    public class Calc
    {
        /// <summary>
        /// Procon29_Calcを初期化します。
        /// </summary>
        /// <param name="point">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Calc(int[,] point, Point[] initPosition)
        {
            // Turn -> 0
            Turn = 0;
            // TurnData作成
            FieldHistory.Add(new TurnData(new Cell[point.GetLength(1), point.GetLength(0)], new Point[TeamArray.Length, AgentArray.Length]));

            InitializationOfField(point);
            foreach (Agent agent in AgentArray)
            {
                AgentPosition[0, (int)agent] = initPosition[(int)agent];
                PutTile(team: 0, agent: agent);
            }
            ComplementEnemysPosition();

            // Turn -> 1
            TurnEnd();
        }

        /// <summary>
        /// Procon29_Calcを初期化します。
        /// </summary>
        /// <param name="field">フィールドのポイントを設定します。</param>
        /// <param name="initPosition">エージェントの初期位置を設定します。</param>
        public Calc(Cell[,] field, Point[] initPosition)
        {
            // Turn -> 0
            Turn = 0;
            // TurnData作成
            FieldHistory.Add(new TurnData(field, new Point[TeamArray.Length, AgentArray.Length]));

            foreach (Agent agent in AgentArray)
            {
                AgentPosition[0, (int)agent] = initPosition[(int)agent];
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
            if (IsVerticallySymmetrical)
            {
                AgentPosition[1, 0] = Field.FlipVertical(AgentPosition[0, 0]);
                AgentPosition[1, 1] = Field.FlipVertical(AgentPosition[0, 1]);
            }
            else if (IsHorizontallySymmetrical)
            {
                AgentPosition[1, 0] = Field.FlipHorizontal(AgentPosition[0, 0]);
                AgentPosition[1, 1] = Field.FlipHorizontal(AgentPosition[0, 1]);
            }
            PutTile(team: 1, agent: 0);
            PutTile(team: 1, agent: 1);
        }

        /// <summary>
        /// フィールドの初期化をします。
        /// </summary>
        /// <param name="point"></param>
        private void InitializationOfField(int[,] point)
        {
            Field = new Cell[point.GetLength(1), point.GetLength(0)];
            for (int x = 0; x < Field.Width(); x++)
            {
                for (int y = 0; y < Field.Height(); y++)
                {
                    Field[x, y] = new Cell { Score = point[y, x] };
                }
            }
        }

        /// <summary>
        /// エージェントの位置をを設定または取得します。
        /// </summary>
        public Point[,] AgentPosition { get => FieldHistory[Turn].AgentPosition; set => FieldHistory[Turn].AgentPosition = value; }

        /// <summary>
        /// ターンを設定または取得します。
        /// </summary>
        public int Turn { get; private set; }

        /// <summary>
        /// フィールドを設定または取得します。
        /// </summary>
        public Cell[,] Field { get => FieldHistory[Turn].Field; set => FieldHistory[Turn].Field = value; }

        /// <summary>
        /// フィールドのリストを返します。
        /// </summary>
        public List<Cell> FieldList
        {
            get
            {
                var list = new List<Cell>();
                foreach (var cell in Field)
                {
                    list.Add(cell);
                }
                return list;
            }
        }

        /// <summary>
        /// 上下対称なら真、そうでなければ偽が返ってきます。
        /// </summary>
        public bool IsVerticallySymmetrical => Field.VerticallySymmetricalCheck();

        /// <summary>
        /// 左右対称なら真、そうでなければ偽が返ってきます。
        /// </summary>
        public bool IsHorizontallySymmetrical => Field.HorizontallySymmetricalCheck();

        /// <summary>
        /// エージェントの略称を返します。
        /// </summary>
        public static string[,] ShortTeamAgentName => new string[2, 2] { { "Strawberry", "Apple", }, { "Kiwi", "Muscat", }, };

        /// <summary>
        /// フィールドの歴史を設定または取得します。
        /// </summary>
        internal List<TurnData> FieldHistory { get; private set; } = new List<TurnData>();

        /// <summary>
        /// Team列挙体のすべての要素を配列にします
        /// </summary>
        public static Array TeamArray => Enum.GetValues(typeof(Team));

        /// <summary>
        /// Agent列挙体のすべての要素を配列にします
        /// </summary>
        public static Array AgentArray => Enum.GetValues(typeof(Agent));

        /// <summary>
        /// 現在のobjectのディープコピーを行います。
        /// </summary>
        /// <returns>objectのディープコピー</returns>
        public object DeepCopy()
        {
            return new Calc((Cell[,])Field.DeepCopy(), new Point[] { AgentPosition[0, 0], AgentPosition[0, 1] });
        }

        /// <summary>
        /// すべてのフィールドのポイントの和を計算します。
        /// </summary>
        /// <returns>すべてのフィールドのポイントの和</returns>
        public int Sum() => FieldList.Sum(x => x.Score);

        /// <summary>
        /// すべてのフィールドのポイントの絶対値の和を計算します。
        /// </summary>
        /// <returns>すべてのフィールドのポイントの絶対値の和</returns>
        public int SumAbs() => FieldList.Sum(x => ((x.Score > 0) ? x.Score : -x.Score));

        /// <summary>
        /// 指定したチームの直接的なエリアのポイントの合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームの直接的なエリアのポイントの合計</returns>
        public int AreaPoint(int team) => FieldList.Sum(x => ((x.IsTileOn[team] == true) ? x.Score : 0));

        /// <summary>
        /// 指定したチームの直接的なエリアのポイントの合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームの直接的なエリアのポイントの合計</returns>
        public int AreaPoint(Team team) => AreaPoint((int)team);

        /// <summary>
        /// 指定したチームが囲んだエリアのポイントの絶対値の合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームが囲んだエリアのポイントの絶対値の合計</returns>
        public int EnclosedPoint(int team) => FieldList.Sum(x => ((x.IsEnclosed[team] == true) ? Math.Abs(x.Score) : 0));

        /// <summary>
        /// 指定したチームが囲んだエリアのポイントの絶対値の合計を計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームが囲んだエリアのポイントの絶対値の合計</returns>
        public int EnclosedPoint(Team team) => EnclosedPoint((int)team);

        /// <summary>
        /// 指定したチームの合計ポイントを計算します。
        /// </summary>
        /// <param name="team">計算するチーム</param>
        /// <returns>指定したチームの合計ポイント</returns>
        public int TotalPoint(int team) => AreaPoint(team) + EnclosedPoint(team);

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
            if (Field.Width() > 12 || Field.Height() > 12) throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// そのフィールドが塗れるか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">対象となるフィールド</param>
        /// <returns>そのフィールドが塗れるなら真、そうでなければ偽が返ってきます。</returns>
        bool IsFillable(int team, Point point) => 0 <= point.X && point.X < Field.Width() && 0 <= point.Y && point.Y < Field.Height() && !Field[point.X, point.Y].IsTileOn[team];

        /// <summary>
        /// 指定したフィールドを基準にIsEnclosedをfalseで塗りつぶします。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">始点にするフィールド</param>
        private void FillFalseInIsEnclosed(int team, Point point)
        {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(point);

            while (stack.Count > 0)
            {
                point = stack.Pop();
                if (IsFillable(team, point)) //make sure we stay within bounds
                {
                    if (Field[point.X, point.Y].IsEnclosed[team] == true)
                    {
                        Field[point.X, point.Y].IsEnclosed[team] = false;
                        stack.Push(new Point(point.X - 1, point.Y));
                        stack.Push(new Point(point.X + 1, point.Y));
                        stack.Push(new Point(point.X, point.Y - 1));
                        stack.Push(new Point(point.X, point.Y + 1));
                    }
                }
            }
            return;
        }

        /// <summary>
        /// 指定したフィールドを基準にIsEnclosedをfalseで塗りつぶします。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="point">始点にするフィールド</param>
        private void FillFalseInIsEnclosed(Team team, Point point) => FillFalseInIsEnclosed((int)team, point);

        /// <summary>
        /// 対象となるチームのIsEnclosedをTrueで初期化します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void ResetTrueInIsEnclosed(int team)
        {
            foreach (var cell in Field)
            {
                cell.IsEnclosed[team] = true;
            }
        }

        /// <summary>
        /// 対象となるチームのIsEnclosedをTrueで初期化します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void ResetTrueInIsEnclosed(Team team) => ResetTrueInIsEnclosed((int)team);

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
        private void CheckEnclosedArea(int team)
        {
            ResetTrueInIsEnclosed(team);
            for (int x = 0; x < Field.Width(); x++)
            {
                for (int y = 0; y < Field.Height(); y++)
                {
                    if (x == 0 || x == Field.Width() - 1 || y == 0 || y == Field.Height() - 1)
                        FillFalseInIsEnclosed(team, new Point(x, y));
                }
            }
            foreach (var item in Field)
            {
                if (item.IsTileOn[team]) item.IsEnclosed[team] = false;
            }
        }

        /// <summary>
        /// 対象となるチームにおいてフィールドが囲まれているか判定します。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        private void CheckEnclosedArea(Team team) => CheckEnclosedArea((int)team);

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
        /// <param name="agent">対象となるエージェント</param>
        /// <param name="point">対象となるマス</param>
        /// <returns>対象となるマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns>
        public bool IsAgentHereOrInMooreNeighborhood(int team, int agent, Point point) => (AgentPosition[team, agent].ChebyshevDistance(point) <= 1) ? true : false;

        /// <summary>
        /// 対象となるマスに対象となるエージェントがいるか、またはムーア近傍にいるかを判定します
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        /// <param name="point">対象となるマス</param>
        /// <returns>対象となるマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns>
        public bool IsAgentHereOrInMooreNeighborhood(Team team, Agent agent, Point point) => IsAgentHereOrInMooreNeighborhood((int)team, (int)agent, point);

        /// <summary>
        /// 対象となるマスにエージェントがいるか、またはムーア近傍にいるかを判定します
        /// </summary>
        /// <param name="point">対象となるマス</param>
        /// <returns>そのマスにエージェントがいるか、またはムーア近傍にいたら真、そうでなければ偽</returns>
        public bool IsAgentHereOrInMooreNeighborhood(Point point)
        {
            foreach (var agent in AgentPosition)
                if (agent.ChebyshevDistance(point) <= 1)
                    return true;
            return false;
        }

        /// <summary>
        /// 対象となるマスに一人だけ、エージェントがいるか、またはムーア近傍にいるかを判定します
        /// </summary>
        /// <param name="point">対象になるマス</param>
        /// <returns></returns>
        public bool IsOneAgentHereOrInMooreNeighborhood(Point point)
        {
            var result = false;
            foreach (var agent in AgentPosition)
                if (agent.ChebyshevDistance(point) <= 1)
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
        public bool IsAgentHere(Point point)
        {
            foreach (var agent in AgentPosition)
                if (agent.ChebyshevDistance(point) == 0)
                    return true;
            return false;
        }

        /// <summary>
        /// 対象になるマスのムーア近傍にエージェントがいるか判定します。
        /// </summary>
        /// <param name="point">対象になるマス</param>
        /// <returns></returns>
        public bool IsAgentInMooreNeighborhood(Point point)
        {
            foreach (var agent in AgentPosition)
                if (agent.ChebyshevDistance(point) == 1)
                    return true;
            return false;
        }

        /// <summary>
        /// 対象になるマスのムーア近傍に一人だけエージェントがいるか判定します。
        /// </summary>
        /// <param name="point">対象になるマス</param>
        /// <returns></returns>
        public bool IsOneAgentInMooreNeighborhood(Point point)
        {
            var result = false;
            foreach (var agent in AgentPosition)
                if (agent.ChebyshevDistance(point) == 1)
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
            // TurnData作成
            var a = new Point[TeamArray.Length, AgentArray.Length];
            for (int team = 0; team < TeamArray.Length; team++)
            {
                for (int agent = 0; agent < AgentArray.Length; agent++)
                {
                    a[team, agent] = new Point(AgentPosition[team, agent].X, AgentPosition[team, agent].Y);
                }
            }
            FieldHistory.Add(new TurnData((Cell[,])Field.DeepCopy(), a));
            Turn++;
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
        public void PutTile(int team, int agent) => Field.Take(AgentPosition[team, agent]).IsTileOn[team] = true;

        /// <summary>
        /// 自分のフィールドにタイルを置きます。
        /// </summary>
        /// <param name="team">対象となるチーム</param>
        /// <param name="agent">対象となるエージェント</param>
        public void PutTile(Team team, Agent agent) => PutTile((int)team, (int)agent);

        /// <summary>
        /// フィールドに置いてあるタイルを剥がします。
        /// </summary>
        /// <param name="point">対象となるエージェント</param>
        public void RemoveTile(Point point)
        {
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                Field[point.X, point.Y].IsTileOn[(int)team] = false;
            }
        }

        /// <summary>
        /// 指定したところにエージェントが移動します。
        /// </summary>
        /// <param name="team">移動するチーム</param>
        /// <param name="agent">移動するエージェント</param>
        /// <param name="where">移動する場所</param>
        public void MoveAgent(Team team, Agent agent, Point where)
        {
            bool movable = false;
            foreach (int otherteam in TeamArray)
            {
                if (otherteam != (int)team)
                {
                    movable = Field[where.X, where.Y].IsTileOn[otherteam];
                    if (movable)
                    {
                        RemoveTile(point: where);
                        CheckEnclosedArea((Team)otherteam);
                        break;
                    }
                }
            }
            if (!movable)
            {
                AgentPosition[(int)team, (int)agent] = where;
                PutTile(team: team, agent: agent);
            }

            CheckEnclosedArea(team);

            foreach (var item in Field)
            {
                if (item.IsTileOn[0]) item.IsEnclosed[0] = false;
                if (item.IsTileOn[1]) item.IsEnclosed[1] = false;
            }
        }

        void CheckAgentActivityData(AgentActivityData[,] agentActivityData)
        {
            foreach (int team in TeamArray)
            {
                foreach (int agent in AgentArray)
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
                    if (item.Destination == AgentPosition[team, agent])
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
                    if (item.Destination.ChebyshevDistance(AgentPosition[team, agent]) != 1)
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
                    if (item.Destination.X < 0 || Field.Width() <= item.Destination.X ||
                        item.Destination.Y < 0 || Field.Height() <= item.Destination.Y)
                        switch (item.AgentStatusData)
                        {
                            case AgentStatusCode.RequestMovement:
                                item.AgentStatusData = AgentStatusCode.FailedInMovingByTryingToGoOutOfTheFieldWithEachOther;
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
                        Field[item.Destination.X, item.Destination.Y].IsTileOn[(team == 0) ? 0 : 1] == false)
                    {
                        item.AgentStatusData = AgentStatusCode.FailedInRemovingOurTileByDoingTileNotExist;
                        continue;
                    }
                    if (item.AgentStatusData == AgentStatusCode.RequestRemovementOpponentTile &&
                        Field[item.Destination.X, item.Destination.Y].IsTileOn[(team == 0) ? 1 : 0] == false)
                    {
                        item.AgentStatusData = AgentStatusCode.FailedInRemovingOpponentTileByDoingTileNotExist;
                        continue;
                    }
                    // 移動先に相手のタイルが置いてないかチェック(1)
                    // FailedInMovingByTryingItWithoutRemovingTheOpponentTile
                    if (Field.Take(item.Destination).IsTileOn[(team == (int)Team.A) ? (int)Team.B : (int)Team.A])
                    {
                        item.AgentStatusData = AgentStatusCode.FailedInMovingByTryingItWithoutRemovingTheOpponentTile;
                        continue;
                    }
                    // 動かないエージェントに衝突していないかチェック(4)
                    // FailedInMovingByCollisionWithTheLazyOurTeam
                    // FailedInRemovingOurTileWithTheLazyOurTeam
                    // FailedInMovingByCollisionWithTheLazyOpponent
                    // FailedInRemovingOpponentTileWithTheLazyOpponent
                    foreach (int otherteam in TeamArray)
                    {
                        foreach (int otheragent in AgentArray)
                        {
                            if (team == otherteam && agent == otheragent) continue;
                            var otheritem = agentActivityData[otherteam, otheragent];
                            var otherposition = AgentPosition[otherteam, otheragent];
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
            foreach (int team in TeamArray)
            {
                foreach (int agent in AgentArray)
                {
                    var item = agentActivityData[team, agent];
                    foreach (int otherteam in TeamArray)
                    {
                        foreach (int otheragent in AgentArray)
                        {
                            if (team == otherteam && agent == otheragent) continue;
                            var otheritem = agentActivityData[otherteam, otheragent];
                            var otherposition = AgentPosition[otherteam, otheragent];
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
        public void MoveAgent(AgentActivityData[,] agentActivityData)
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
            FieldHistory[Turn - 1].AgentActivityData[0, 0].AgentStatusData = agentActivityData[0, 0].AgentStatusData;
            FieldHistory[Turn - 1].AgentActivityData[0, 1].AgentStatusData = agentActivityData[0, 1].AgentStatusData;
            FieldHistory[Turn - 1].AgentActivityData[1, 0].AgentStatusData = agentActivityData[1, 0].AgentStatusData;
            FieldHistory[Turn - 1].AgentActivityData[1, 1].AgentStatusData = agentActivityData[1, 1].AgentStatusData;

            foreach (Team team in TeamArray)
            {
                foreach (Agent agent in AgentArray)
                {
                    switch (agentActivityData[(int)team, (int)agent].AgentStatusData)
                    {
                        case AgentStatusCode.SucceededInMoving:
                            AgentPosition[(int)team, (int)agent] = agentActivityData[(int)team, (int)agent].Destination;
                            PutTile(team: team, agent: agent);
                            break;
                        case AgentStatusCode.SucceededInRemovingOurTile:
                            RemoveTile(agentActivityData[(int)team, (int)agent].Destination);
                            break;
                        case AgentStatusCode.SucceededInRemovingOpponentTile:
                            RemoveTile(agentActivityData[(int)team, (int)agent].Destination);
                            break;
                        default:
                            break;
                    }
                    agentActivityData[(int)team, (int)agent].ToSuccess();
                }
            }
            CheckEnclosedArea();
        }

        /// <summary>
        /// 指定したところにエージェントが移動します。
        /// </summary>
        /// <param name="team">移動させるチーム</param>
        /// <param name="agentActivityData"></param>
        public void MoveAgent(Team team, AgentActivityData[] agentActivityData)
        {
            var agentActivityDatas = new AgentActivityData[2, 2];
            switch (team)
            {
                case Team.A:
                    agentActivityDatas[(int)Team.A, (int)Agent.One] = agentActivityData[0];
                    agentActivityDatas[(int)Team.A, (int)Agent.Two] = agentActivityData[1];
                    agentActivityDatas[(int)Team.B, (int)Agent.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                    agentActivityDatas[(int)Team.B, (int)Agent.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                    break;
                case Team.B:
                    agentActivityDatas[(int)Team.A, (int)Agent.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                    agentActivityDatas[(int)Team.A, (int)Agent.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                    agentActivityDatas[(int)Team.B, (int)Agent.One] = agentActivityData[0];
                    agentActivityDatas[(int)Team.B, (int)Agent.Two] = agentActivityData[1];
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
        public void MoveAgent(Team team, Agent agent, AgentActivityData agentActivityData)
        {
            var agentActivityDatas = new AgentActivityData[2, 2];
            switch (team)
            {
                case Team.A:
                    switch (agent)
                    {
                        case Agent.One:
                            agentActivityDatas[(int)Team.A, (int)Agent.One] = agentActivityData;
                            agentActivityDatas[(int)Team.A, (int)Agent.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.B, (int)Agent.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.B, (int)Agent.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            break;
                        case Agent.Two:
                            agentActivityDatas[(int)Team.A, (int)Agent.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.A, (int)Agent.Two] = agentActivityData;
                            agentActivityDatas[(int)Team.B, (int)Agent.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.B, (int)Agent.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            break;
                    }
                    break;
                case Team.B:
                    switch (agent)
                    {
                        case Agent.One:
                            agentActivityDatas[(int)Team.A, (int)Agent.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.A, (int)Agent.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.B, (int)Agent.One] = agentActivityData;
                            agentActivityDatas[(int)Team.B, (int)Agent.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            break;
                        case Agent.Two:
                            agentActivityDatas[(int)Team.A, (int)Agent.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.A, (int)Agent.Two] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.B, (int)Agent.One] = new AgentActivityData(AgentStatusCode.RequestNotToDoAnything);
                            agentActivityDatas[(int)Team.B, (int)Agent.Two] = agentActivityData;
                            break;
                    }
                    break;
            }
            MoveAgent(agentActivityDatas);
        }
    }
}
