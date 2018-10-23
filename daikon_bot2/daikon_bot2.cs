using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.daikon_bot2
{
    public class daikon_bot2 : Bot.Bot
    {
        public daikon_bot2() : base() { }

        public override AgentActivityData[] Answer()
        {
            var result = new AgentActivityData[2] { new AgentActivityData(), new AgentActivityData() };
            var go = new Coordinate[2];

            Arrow num1 = Arrow.Down;
            Arrow num2 = Arrow.Down;
            Arrow ori1 = (Arrow)8;
            Arrow ori2 = (Arrow)8;
            int[] point = new int[8];
            int maxpoint1 = -15;
            int maxpoint2 = -15;
            int j1 = 0;
            int j2 = 0;
            int i = 0;
            int count1 = 0;
            int count2 = 0;

            //=======================================================================================================================================================================================================

            //追いかけ
            //敵のタイルがあるか
            for (i = 0; i <= 7; i++)
            {
                ori1 = (Arrow)i;

                if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.One].Position + ori1) && Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].Point < 0 && Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].IsTileOn[OurTeam.Opponent()])
                {
                    Log.WriteLine("there are opponent");
                    num1 = ori1;
                    j1++;
                    break;
                }
            }
            //敵のタイルがなければ、最大値を入れる
            if (j1 == 0)
            {
                Log.WriteLine("no opponent");
                //8方位の評価
                //ポイントの取得
                for (i = 0; i <= 7; i++)
                {
                    ori1 = (Arrow)i;

                    if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.One].Position + ori1) && !Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].IsTileOn[OurTeam])
                    {
                        point[i] = Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].Point;
                        Log.WriteLine("get point");
                    }
                }

                //最大値を求める→行き先を決める
                //自分のマスには行かない
                for (i = 0; i <= 7; i++)
                {
                    ori1 = (Arrow)i;
                    if ((Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.One].Position + ori1) && !Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].IsTileOn[OurTeam]))
                    {
                        Log.WriteLine("dainyuu point[i]");
                        maxpoint1 = (maxpoint1 > point[i]) ? maxpoint1 : point[i];
                    }
                }

                for (i = 0; i <= 7; i++)
                {
                    ori1 = (Arrow)i;
                    if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.One].Position + ori1) && !Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].IsTileOn[OurTeam])
                    {
                        if (maxpoint1 == point[i])
                        {
                            count1++;
                            Log.WriteLine("max point");
                            num1 = (Arrow)i;
                            break;
                        }
                    }
                }
            }

            //動けなかった時
            //bot_1
            if (Calc.Turn > 2 && Calc.History[Calc.Turn - 1].AgentsActivityData[OurTeam, AgentNumber.One].AgentStatusData.IsFailed() && Calc.History[Calc.Turn - 2].AgentsActivityData[OurTeam, AgentNumber.One].AgentStatusData.IsFailed())
            {
                num1++;
            }

            go[0] = Calc.Agents[OurTeam, AgentNumber.One].Position + num1;

            //===============================================================================================================================================================================================================

            //No.2
            //敵のタイルがあるか
            for (i = 0; i <= 7; i++)
            {
                ori2 = (Arrow)i;

                if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2) && Calc.Field[Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2].Point >= 0 && Calc.Field[Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2].IsTileOn[OurTeam.Opponent()])
                {
                    Log.WriteLine("2there are opponent");
                    num2 = ori2;
                    j2++;
                    break;
                }
            }
            //敵のタイルがなければ、最大値を入れる
            if (j2 == 0)
            {
                Log.WriteLine("2no opponent");
                //8方位の評価
                //ポイントの取得
                for (i = 0; i <= 7; i++)
                {
                    ori2 = (Arrow)i;
                    if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2) && !Calc.Field[Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2].IsTileOn[OurTeam])
                    {
                        Log.WriteLine("2get point");
                        point[i] = Calc.Field[Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2].Point;
                    }
                }

                //最大値を求める→行き先を決める
                //自分のマスには行かない
                for (i = 0; i <= 7; i++)
                {
                    ori2 = (Arrow)i;

                    if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2) && !Calc.Field[Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2].IsTileOn[OurTeam])
                    {
                        Log.WriteLine("2dainyuu point");
                        maxpoint2 = (maxpoint2 > point[i]) ? maxpoint2 : point[i];
                    }
                }

                for (i = 0; i <= 7; i++)
                {
                    ori2 = (Arrow)i;
                    if (Calc.Field.CellExist(Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2) && !Calc.Field[Calc.Agents[OurTeam, AgentNumber.Two].Position + ori2].IsTileOn[OurTeam])
                    {
                        if (maxpoint2 == point[i])
                        {
                            count2++;
                            Log.WriteLine("max point");
                            num2 = (Arrow)i;
                            break;
                        }
                    }
                }
            }

            //動けなかったとき
            //bot_2
            if (Calc.Turn > 2 && Calc.History[Calc.Turn - 1].AgentsActivityData[OurTeam, AgentNumber.Two].AgentStatusData.IsFailed() && Calc.History[Calc.Turn - 2].AgentsActivityData[OurTeam, AgentNumber.Two].AgentStatusData.IsFailed())
            {
                num2++;
            }

            go[1] = Calc.Agents[OurTeam, AgentNumber.Two].Position + num2;



            //=============最後の色々===========================================================================================================================================================================================
            
            //同じ場所に行こうとした時は bot_1 の動きを優先する。
            if (go[0] == go[1])
            {
                go[1] = Calc.Agents[OurTeam, AgentNumber.Two].Position;
            }

            //フィールドの外に行こうとしたとき
            if (0 <= go[0].X && go[0].X < Calc.Field.Width && 0 <= go[0].Y && go[0].Y < Calc.Field.Height)
            {
                result[(int)AgentNumber.One] = MoveOrRemoveTile(go[0]);
            }

            if (0 <= go[1].X && go[1].X < Calc.Field.Width && 0 <= go[1].Y && go[1].Y < Calc.Field.Height)
            {
                result[(int)AgentNumber.Two] = MoveOrRemoveTile(go[1]);
            }
            return result;


            AgentActivityData MoveOrRemoveTile(Coordinate coordinate)
            {
                if (Calc.Field[coordinate].IsTileOn[OurTeam.Opponent()])
                {
                    return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
                }
                return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);
            }
        }
    }
}