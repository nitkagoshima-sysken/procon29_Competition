using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nitkagoshima_sysken.Procon29.Visualizer;

namespace nitkagoshima_sysken.Procon29.daikon_bot
{
    public class daikon_bot : Bot.Bot
    {
        public daikon_bot() : base() { }

        static int i = 0;
        static int count1 = 0;
        static int count2 = 0;
        static int count3 = 0;//bot_1
        static int count4 = 0;//bot_1
        static int count5 = 0;//bot_2
        static int count6 = 0;//bot_2
        static int turn1 = 0;
        static int turn2 = 0;
        static int edge1 = 0;
        static int edge2 = 0;
        static int ud1 = 0; //up == 偶数::down == 奇数
        static int ud2 = 0; //up == 偶数::down == 奇数
        static int r1 = 1;
        static int r2 = 1;
        static int[] corner1 = new int[2] { 0, 0 };
        static int[] corner2 = new int[2] { 0, 0 };

        public override AgentActivityData[] Answer()
        {
            int decide = Calc.Field.Height % 2;
            var result = new AgentActivityData[2] { new AgentActivityData(), new AgentActivityData() };
            var go = new Coordinate[2];
            Arrow num1 = Arrow.Down;
            Arrow num2 = Arrow.Down;
            Coordinate des1 = new Coordinate(0, 0);
            Coordinate des2 = new Coordinate(0, 0);
            int on = 0;
            int c1 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate(0, 0));
            int c2 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate((Calc.Field.Width - 1), 0));
            int c3 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1)));
            int c4 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate(0, (Calc.Field.Height - 1)));
            int dis1 = Math.Min((Math.Min(c1, c2)), (Math.Min(c3, c4)));

            int c_1 = Calc.Agents[OurTeam, AgentNumber.Two].Position.ChebyshevDistance(new Coordinate(0, 0));
            int c_2 = Calc.Agents[OurTeam, AgentNumber.Two].Position.ChebyshevDistance(new Coordinate((Calc.Field.Width - 1), 0));
            int c_3 = Calc.Agents[OurTeam, AgentNumber.Two].Position.ChebyshevDistance(new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1)));
            int c_4 = Calc.Agents[OurTeam, AgentNumber.Two].Position.ChebyshevDistance(new Coordinate(0, (Calc.Field.Height - 1)));
            int dis2 = Math.Min((Math.Min(c_1, c_2)), (Math.Min(c_3, c_4)));

            int a = 0;
            int b = 0;
            Arrow ori1 = (Arrow)8;
            Arrow ori2 = (Arrow)8;
            int[] point = new int[8];
            var max = (Arrow)1;
            int maxpoint = -15;



            //========================================================================================角にたどり着くまで========================================================================================================

            if ((Calc.Field.Width % 2 == 0 && count3 == 0) || (Calc.Field.Width % 2 == 1 && count4 == 0))
            {
                // No.1
                if (count1 == 0)
                {
                    //目的地を決める
                    if (dis1 == c1)
                    {
                        des1 = new Coordinate(0, 0);
                        corner1[0] = des1.X;
                        corner1[1] = des1.Y;
                    }
                    else if (dis1 == c2)
                    {
                        des1 = new Coordinate((Calc.Field.Width - 1), 0);
                        corner1[0] = des1.X;
                        corner1[1] = des1.Y;
                    }
                    else if (dis1 == c3)
                    {
                        des1 = new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1));
                        corner1[0] = des1.X;
                        corner1[1] = des1.Y;
                    }
                    else if (dis1 == c4)
                    {
                        des1 = new Coordinate(0, (Calc.Field.Height - 1));
                        corner1[0] = des1.X;
                        corner1[1] = des1.Y;
                    }

                    //どう動くか決める
                    if (des1.X < Calc.Agents[OurTeam, AgentNumber.One].Position.X)
                    {
                        if (des1.Y < Calc.Agents[OurTeam, AgentNumber.One].Position.Y)
                        {
                            num1 = Arrow.UpLeft;
                        }
                        else if (des1.Y == Calc.Agents[OurTeam, AgentNumber.One].Position.Y)
                        {
                            num1 = Arrow.Left;
                        }
                        else
                        {
                            num1 = Arrow.DownLeft;
                        }

                    }
                    else if (des1.X > Calc.Agents[OurTeam, AgentNumber.One].Position.X)
                    {
                        if (des1.Y < Calc.Agents[OurTeam, AgentNumber.One].Position.Y)
                        {
                            num1 = Arrow.UpRight;
                        }
                        else if (des1.Y == Calc.Agents[OurTeam, AgentNumber.One].Position.Y)
                        {
                            num1 = Arrow.Right;
                        }
                        else
                        {
                            num1 = Arrow.DownRight;
                        }
                    }
                    else
                    {
                        if (des1.Y < Calc.Agents[OurTeam, AgentNumber.One].Position.Y)
                        {
                            num1 = Arrow.Up;
                        }
                        else
                        {
                            num1 = Arrow.Down;
                        }
                    }
                    //行動を返す
                    go[0] = Calc.Agents[OurTeam, AgentNumber.One].Position + num1;

                    if (go[0] == new Coordinate(0, 0) || go[0] == new Coordinate((Calc.Field.Width - 1), 0) || go[0] == new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1)) || go[0] == new Coordinate(0, (Calc.Field.Height - 1)))
                    {
                        count1++;
                        if (go[0] == new Coordinate(0, 0) || go[0] == new Coordinate(Calc.Field.Width - 1, 0))
                        {
                            ud1++;
                        }
                    }
                }
                //ジグザグ===============================================================================================
                else
                {
                    turn1++;

                    if (decide == 0)
                    {
                        a = 0;
                        b = (int)Calc.Field.Height - 1;
                    }
                    else
                    {
                        a = 1;
                        b = (int)Calc.Field.Height - 2;
                    }

                    //from DownLeft
                    if (corner1[0] == 0 && corner1[1] == (int)Calc.Field.Height - 1)
                    {
                        if (Calc.Agents[OurTeam, AgentNumber.One].Position.Y != a && r1 % 2 == 1)
                        {

                            if (turn1 % 2 == 1)
                            {
                                num1 = Arrow.UpRight;
                            }
                            else
                            {
                                num1 = Arrow.UpLeft;
                            }
                        }
                        else
                        {
                            if (edge1 == 0)
                            {
                                num1 = Arrow.Right;
                                edge1++;
                                ud1++;
                                r1++;
                            }
                            else
                            {
                                if (ud1 % 2 == 1)
                                {
                                    if (turn1 % 2 == 1)
                                    {
                                        num1 = Arrow.DownRight;
                                    }
                                    else
                                    {
                                        num1 = Arrow.DownLeft;
                                    }
                                }
                                else if (ud1 % 2 == 0)
                                {
                                    if (turn1 % 2 == 1)
                                    {
                                        num1 = Arrow.UpRight;
                                    }
                                    else
                                    {
                                        num1 = Arrow.UpLeft;
                                    }
                                }
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.One].Position + num1).Y == Calc.Field.Height - 1)
                            {
                                edge1--;
                            }
                            if (Calc.Agents[OurTeam, AgentNumber.One].Position + num1 == new Coordinate(Calc.Field.Width - 1, 0))
                            {
                                count3++;
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.One].Position + num1).X == Calc.Field.Width - 1)
                            {
                                count4++;
                            }
                        }
                    }

                    //from UpLeft
                    else if (corner1[0] == 0 && corner1[1] == 0)
                    {

                        if (Calc.Agents[OurTeam, AgentNumber.One].Position.Y != b && r1 % 2 == 1)
                        {

                            if ((turn1 % 2) == 1)
                            {
                                num1 = Arrow.DownRight;
                            }
                            else
                            {
                                num1 = Arrow.DownLeft;
                            }
                        }
                        else
                        {
                            if (edge1 % 2 == 0)
                            {
                                num1 = Arrow.Right;
                                edge1++;
                                ud1++;
                                r1++;
                            }
                            else
                            {
                                if (ud1 % 2 == 1)
                                {
                                    if (turn1 % 2 == i)
                                    {
                                        num1 = Arrow.DownRight;
                                    }
                                    else
                                    {
                                        num1 = Arrow.DownLeft;
                                    }
                                }
                                else if (ud1 % 2 == 0)
                                {
                                    if (turn1 % 2 == 1)
                                    {
                                        num1 = Arrow.UpRight;
                                    }
                                    else
                                    {
                                        num1 = Arrow.UpLeft;
                                    }
                                }
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.One].Position + num1).Y == 0)
                            {
                                edge1--;
                            }
                            if (Calc.Agents[OurTeam, AgentNumber.One].Position + num1 == new Coordinate(Calc.Field.Width - 1, Calc.Field.Height - 1))
                            {
                                count3++;
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.One].Position + num1).X == Calc.Field.Width - 1)
                            {
                                count4++;
                            }
                        }
                    }
                    //from DownRight
                    else if (corner1[0] == (int)Calc.Field.Width - 1 && corner1[1] == (int)Calc.Field.Height - 1)
                    {
                        if (Calc.Agents[OurTeam, AgentNumber.One].Position.Y != a && r1 % 2 == 1)
                        {

                            if (turn1 % 2 == 1)
                            {
                                num1 = Arrow.UpLeft;
                            }
                            else
                            {
                                num1 = Arrow.UpRight;
                            }
                        }
                        else
                        {
                            if (edge1 == 0)
                            {
                                num1 = Arrow.Left;
                                edge1++;
                                ud1++;
                                r1++;
                            }
                            else
                            {
                                if (ud1 % 2 == 1)
                                {
                                    if (turn1 % 2 == 1)
                                    {
                                        num1 = Arrow.DownLeft;
                                    }
                                    else
                                    {
                                        num1 = Arrow.DownRight;
                                    }
                                }
                                else if (ud1 % 2 == 0)
                                {
                                    if (turn1 % 2 == 1)
                                    {
                                        num1 = Arrow.UpLeft;
                                    }
                                    else
                                    {
                                        num1 = Arrow.UpRight;
                                    }
                                }
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.One].Position + num1).Y == Calc.Field.Height - 1)
                            {
                                edge1--;
                            }
                            if (Calc.Agents[OurTeam, AgentNumber.One].Position + num1 == new Coordinate(0, Calc.Field.Height - 1))
                            {
                                count3++;
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.One].Position + num1).X == 0)
                            {
                                count4++;
                            }
                        }
                    }

                    //from UpRight
                    else if (corner1[0] == (int)Calc.Field.Width - 1 && corner1[1] == 0)
                    {

                        if (Calc.Agents[OurTeam, AgentNumber.One].Position.Y != b && r1 % 2 == 1)
                        {

                            if ((turn1 % 2) == 1)
                            {
                                num1 = Arrow.DownLeft;
                            }
                            else
                            {
                                num1 = Arrow.DownRight;
                            }
                        }
                        else
                        {
                            if (edge1 % 2 == 0)
                            {
                                num1 = Arrow.Left;
                                edge1++;
                                ud1++;
                                r1++;
                            }
                            else
                            {
                                if (ud1 % 2 == 1)
                                {
                                    if (turn1 % 2 == i)
                                    {
                                        num1 = Arrow.DownLeft;
                                    }
                                    else
                                    {
                                        num1 = Arrow.DownRight;
                                    }
                                }
                                else if (ud1 % 2 == 0)
                                {
                                    if (turn1 % 2 == 1)
                                    {
                                        num1 = Arrow.UpLeft;
                                    }
                                    else
                                    {
                                        num1 = Arrow.UpRight;
                                    }
                                }
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.One].Position + num1).Y == 0)
                            {
                                edge1--;
                            }
                            if (Calc.Agents[OurTeam, AgentNumber.One].Position + num1 == new Coordinate(0, Calc.Field.Height - 1))
                            {
                                count3++;
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.One].Position + num1).X == 0)
                            {
                                count4++;
                            }
                        }
                    }

                }

            }
            //追いかけ
            else
            {
                //敵のタイルがあるか
                for (i=0; i <= 7; i++)
                {
                    ori1 = (Arrow)i;
                    
                    if (Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].IsTileOn[OurTeam.Opponent()])
                    {
                        num1 = ori1;
                        break;
                    }
                    else
                    {
                        num1 = (Arrow)8;
                    }
                }
                //敵のタイルがなければ、最大値を入れる
                if (num1 == (Arrow)8)
                {
                    //8方位の評価
                    //ポイントの取得
                    for (i = 0; i <= 7; i++)
                    {
                        point[i] = Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + num1].Point;
                    }

                    //最大値を求める→行き先を決める
                    //自分のマスには行かない
                    for (i=0; i<=7; i++)
                    {
                        ori1 = (Arrow)i;
                        if (Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].IsTileOn[OurTeam])
                        {
                            continue;
                        }
                        else
                        {
                            maxpoint = (maxpoint > point[i]) ? maxpoint : point[i];
                        }                    }

                    for (i = 0; i <= 7; i++)
                    {
                        if (Calc.Field[Calc.Agents[OurTeam, AgentNumber.One].Position + ori1].IsTileOn[OurTeam])
                        {
                            continue;
                        }
                        else
                        {
                            if (maxpoint == point[i])
                            {
                                num1 = (Arrow)i;
                            }
                        }
                    }
                }

            }

            go[0] = Calc.Agents[OurTeam, AgentNumber.One].Position + num1;

            //===============================================================================================================================================================================================================

            if ((Calc.Field.Width % 2 == 0 && count5 == 0) || (Calc.Field.Width % 2 == 1 && count6 == 0))
            {
                //No.2
                if (count2 == 0)
                {
                    //目的地を決める
                    if (dis2 == c_1)
                    {
                        des2 = new Coordinate(0, 0);
                        corner2[0] = des2.X;
                        corner2[1] = des2.Y;
                    }
                    else if (dis2 == c_2)
                    {
                        des2 = new Coordinate((Calc.Field.Width - 1), 0);
                        corner2[0] = des2.X;
                        corner2[1] = des2.Y;
                    }
                    else if (dis2 == c_3)
                    {
                        des2 = new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1));
                        corner2[0] = des2.X;
                        corner2[1] = des2.Y;
                    }
                    else if (dis2 == c_4)
                    {
                        des2 = new Coordinate(0, (Calc.Field.Height - 1));
                        corner2[0] = des2.X;
                        corner2[1] = des2.Y;
                    }

                    //どう動くか決める
                    if (des2.X < Calc.Agents[OurTeam, AgentNumber.Two].Position.X)
                    {
                        if (des2.Y < Calc.Agents[OurTeam, AgentNumber.Two].Position.Y)
                        {
                            num2 = Arrow.UpLeft;
                        }
                        else if (des2.Y == Calc.Agents[OurTeam, AgentNumber.Two].Position.Y)
                        {
                            num2 = Arrow.Left;
                        }
                        else
                        {
                            num2 = Arrow.DownLeft;
                        }
                    }
                    else if (des2.X > Calc.Agents[OurTeam, AgentNumber.Two].Position.X)
                    {
                        if (des2.Y < Calc.Agents[OurTeam, AgentNumber.Two].Position.Y)
                        {
                            num2 = Arrow.UpRight;
                        }
                        else if (des2.Y == Calc.Agents[OurTeam, AgentNumber.Two].Position.Y)
                        {
                            num2 = Arrow.Right;
                        }
                        else
                        {
                            num2 = Arrow.DownRight;
                        }
                    }
                    else
                    {
                        if (des2.Y < Calc.Agents[OurTeam, AgentNumber.Two].Position.Y)
                        {
                            num2 = Arrow.Up;
                        }
                        else
                        {
                            num2 = Arrow.Down;
                        }
                    }
                    //行動を返す
                    go[1] = Calc.Agents[OurTeam, AgentNumber.Two].Position + num2;

                    if (go[1] == new Coordinate(0, 0) || go[1] == new Coordinate((Calc.Field.Width - 1), 0) || go[1] == new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1)) || go[1] == new Coordinate(0, (Calc.Field.Height - 1)))
                    {
                        count2++;
                        if (go[1] == new Coordinate(0, 0) || go[1] == new Coordinate(Calc.Field.Width - 1, 0))
                        {
                            ud2++;
                        }
                    }
                }
                //ジグザグ
                else
                {
                    turn2++;

                    //from DownLeft
                    if (corner2[0] == 0 && corner2[1] == (int)Calc.Field.Height - 1)
                    {
                        if (Calc.Agents[OurTeam, AgentNumber.Two].Position.Y != a && r2 % 2 == 1)
                        {

                            if (turn2 % 2 == 1)
                            {
                                num2 = Arrow.UpRight;
                            }
                            else
                            {
                                num2 = Arrow.UpLeft;
                            }
                        }
                        else
                        {
                            if (edge2 == 0)
                            {
                                num2 = Arrow.Right;
                                edge2++;
                                ud2++;
                                r2++;
                            }
                            else
                            {
                                if (ud2 % 2 == 1)
                                {
                                    if (turn2 % 2 == 1)
                                    {
                                        num2 = Arrow.DownRight;
                                    }
                                    else
                                    {
                                        num2 = Arrow.DownLeft;
                                    }
                                }
                                else if (ud2 % 2 == 0)
                                {
                                    if (turn2 % 2 == 1)
                                    {
                                        num2 = Arrow.UpRight;
                                    }
                                    else
                                    {
                                        num2 = Arrow.UpLeft;
                                    }
                                }
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.Two].Position + num2).Y == Calc.Field.Height - 1)
                            {
                                edge2--;
                            }
                            if (Calc.Agents[OurTeam, AgentNumber.Two].Position + num2 == new Coordinate(Calc.Field.Width - 1, 0))
                            {
                                count5++;
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.Two].Position + num2).X == Calc.Field.Width - 1)
                            {
                                count6++;
                            }
                        }
                    }

                    //from UpLeft
                    else if (corner2[0] == 0 && corner2[1] == 0)
                    {

                        if (Calc.Agents[OurTeam, AgentNumber.Two].Position.Y != b && r2 % 2 == 1)
                        {

                            if ((turn2 % 2) == 1)
                            {
                                num2 = Arrow.DownRight;
                            }
                            else
                            {
                                num2 = Arrow.DownLeft;
                            }
                        }
                        else
                        {
                            if (edge2 % 2 == 0)
                            {
                                num2 = Arrow.Right;
                                edge2++;
                                ud2++;
                                r2++;
                            }
                            else
                            {
                                if (ud2 % 2 == 1)
                                {
                                    if (turn2 % 2 == i)
                                    {
                                        num2 = Arrow.DownRight;
                                    }
                                    else
                                    {
                                        num2 = Arrow.DownLeft;
                                    }
                                }
                                else if (ud2 % 2 == 0)
                                {
                                    if (turn2 % 2 == 1)
                                    {
                                        num2 = Arrow.UpRight;
                                    }
                                    else
                                    {
                                        num2 = Arrow.UpLeft;
                                    }
                                }
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.Two].Position + num2).Y == 0)
                            {
                                edge1--;
                            }
                            if (Calc.Agents[OurTeam, AgentNumber.Two].Position + num2 == new Coordinate(Calc.Field.Width - 1, Calc.Field.Height - 1))
                            {
                                count5++;
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.Two].Position + num2).X == Calc.Field.Width - 1)
                            {
                                count6++;
                            }
                        }
                    }
                    //from DownRight
                    else if (corner2[0] == (int)Calc.Field.Width - 1 && corner2[1] == (int)Calc.Field.Height - 1)
                    {
                        if (Calc.Agents[OurTeam, AgentNumber.Two].Position.Y != a && r2 % 2 == 1)
                        {

                            if (turn2 % 2 == 1)
                            {
                                num2 = Arrow.UpLeft;
                            }
                            else
                            {
                                num2 = Arrow.UpRight;
                            }
                        }
                        else
                        {
                            if (edge2 == 0)
                            {
                                num2 = Arrow.Left;
                                edge2++;
                                ud2++;
                                r2++;
                            }
                            else
                            {
                                if (ud2 % 2 == 1)
                                {
                                    if (turn2 % 2 == 1)
                                    {
                                        num2 = Arrow.DownLeft;
                                    }
                                    else
                                    {
                                        num2 = Arrow.DownRight;
                                    }
                                }
                                else if (ud2 % 2 == 0)
                                {
                                    if (turn2 % 2 == 1)
                                    {
                                        num2 = Arrow.UpLeft;
                                    }
                                    else
                                    {
                                        num2 = Arrow.UpRight;
                                    }
                                }
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.Two].Position + num2).Y == Calc.Field.Height - 1)
                            {
                                edge2--;
                            }
                            if (Calc.Agents[OurTeam, AgentNumber.Two].Position + num2 == new Coordinate(0, 0))
                            {
                                count5++;
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.Two].Position + num2).X == 0)
                            {
                                count6++;
                            }
                        }
                    }

                    //from UpRight
                    else if (corner2[0] == (int)Calc.Field.Width - 1 && corner2[1] == 0)
                    {

                        if (Calc.Agents[OurTeam, AgentNumber.Two].Position.Y != b && r2 % 2 == 1)
                        {

                            if ((turn2 % 2) == 1)
                            {
                                num2 = Arrow.DownLeft;
                            }
                            else
                            {
                                num2 = Arrow.DownRight;
                            }
                        }
                        else
                        {
                            if (edge2 % 2 == 0)
                            {
                                num2 = Arrow.Left;
                                edge2++;
                                ud2++;
                                r2++;
                            }
                            else
                            {
                                if (ud2 % 2 == 1)
                                {
                                    if (turn2 % 2 == i)
                                    {
                                        num2 = Arrow.DownLeft;
                                    }
                                    else
                                    {
                                        num2 = Arrow.DownRight;
                                    }
                                }
                                else if (ud2 % 2 == 0)
                                {
                                    if (turn2 % 2 == 1)
                                    {
                                        num2 = Arrow.UpLeft;
                                    }
                                    else
                                    {
                                        num2 = Arrow.UpRight;
                                    }
                                }
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.Two].Position + num2).Y == 0)
                            {
                                edge2--;
                            }
                            if (Calc.Agents[OurTeam, AgentNumber.Two].Position + num2 == new Coordinate(0, Calc.Field.Height - 1))
                            {
                                count5++;
                            }
                            if ((Calc.Agents[OurTeam, AgentNumber.Two].Position + num2).X == 0)
                            {
                                count6++;
                            }
                        }
                    }

                }
            }
            go[1] = Calc.Agents[OurTeam, AgentNumber.Two].Position + num2;

            //=============最後の色々===========================================================================================================================================================================================

            if (0 <= go[0].X && go[0].X < Calc.Field.Width && 0 <= go[0].Y && go[0].Y < Calc.Field.Height)
            {
                result[(int)AgentNumber.One] = MoveOrRemoveTile(go[0]);

                if (Calc.Field[go[0]].IsTileOn[OurTeam.Opponent()])
                {
                    turn1--;
                }
            }
            if (0 <= go[1].X && go[1].X < Calc.Field.Width && 0 <= go[1].Y && go[1].Y < Calc.Field.Height)
            {
                result[(int)AgentNumber.Two] = MoveOrRemoveTile(go[1]);

                if (Calc.Field[go[1]].IsTileOn[OurTeam.Opponent()])
                {
                    turn1--;
                }
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