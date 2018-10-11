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

        static int count = 0;
        static int turn = 1;

        public override AgentActivityData[] Answer()
        {
            int decide = Calc.Field.Height % 2;
            var result = new AgentActivityData[2] { new AgentActivityData(), new AgentActivityData() };
            var go = new Coordinate[2];
            Arrow num1 = Arrow.Down;
            Arrow num2 = Arrow.Down;
            Coordinate des1 = new Coordinate(0, 0);
            Coordinate des2 = new Coordinate(0, 0);
            int c1 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate(0, 0));
            int c2 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate((Calc.Field.Width-1), 0));
            int c3 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate((Calc.Field.Width-1), (Calc.Field.Height-1)));
            int c4 = Calc.Agents[OurTeam, AgentNumber.One].Position.ChebyshevDistance(new Coordinate(0, (Calc.Field.Height-1)));
            int dis1 = Math.Min((Math.Min(c1, c2)), (Math.Min(c3, c4)));

            int c_1 = Calc.Agents[OurTeam, AgentNumber.Two].Position.ChebyshevDistance(new Coordinate(0, 0));
            int c_2 = Calc.Agents[OurTeam, AgentNumber.Two].Position.ChebyshevDistance(new Coordinate((Calc.Field.Width-1), 0));
            int c_3 = Calc.Agents[OurTeam, AgentNumber.Two].Position.ChebyshevDistance(new Coordinate((Calc.Field.Width-1), (Calc.Field.Height-1)));
            int c_4 = Calc.Agents[OurTeam, AgentNumber.Two].Position.ChebyshevDistance(new Coordinate(0, (Calc.Field.Height-1)));
            int dis2 = Math.Min((Math.Min(c_1, c_2)), (Math.Min(c_3, c_4)));

//========================================================================================角にたどり着くまで========================================================================================================
            if (count == 0)
            {
                ///  No.1 目的地を決める
                if (dis1 == c1)
                {
                    des1 = new Coordinate(0, 0);
                }
                if (dis1 == c2)
                {
                    des1 = new Coordinate((Calc.Field.Width - 1), 0);
                }
                if (dis1 == c3)
                {
                    des1 = new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1));
                }
                if (dis1 == c4)
                {
                    des1 = new Coordinate(0, (Calc.Field.Height - 1));
                }

                ///  No.2　目的地を決める
                if (dis2 == c_1)
                {
                    des2 = new Coordinate(0, 0);
                }
                if (dis2 == c_2)
                {
                    des2 = new Coordinate((Calc.Field.Width - 1), 0);
                }
                if (dis2 == c_3)
                {
                    des2 = new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1));
                }
                if (dis2 == c_4)
                {
                    des2 = new Coordinate(0, (Calc.Field.Height - 1));
                }


                ///  No.1　どう動くか決める
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

                ///  No.2　どう動くか決める
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
                go[0] = Calc.Agents[OurTeam, AgentNumber.One].Position + num1;
                go[1] = Calc.Agents[OurTeam, AgentNumber.Two].Position + num2;

                //自分は角にいるが、もう片方が角に向かっている時は自分は動かない
                if (des1 == Calc.Agents[OurTeam, AgentNumber.One].Position)
                {
                    go[0] = Calc.Agents[OurTeam, AgentNumber.One].Position;
                }
                if (des2 == Calc.Agents[OurTeam, AgentNumber.Two].Position)
                {
                    go[1] = Calc.Agents[OurTeam, AgentNumber.Two].Position;
                }

                if ((go[0] == new Coordinate(0, 0) || go[0] == new Coordinate((Calc.Field.Width - 1), 0) || go[0] == new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1)) || go[0] == new Coordinate(0, (Calc.Field.Height - 1))) && (go[1] == new Coordinate(0, 0) || go[1] == new Coordinate((Calc.Field.Width - 1), 0) || go[1] == new Coordinate((Calc.Field.Width - 1), (Calc.Field.Height - 1)) || go[1] == new Coordinate(0, (Calc.Field.Height - 1))))
                {
                    count++;
                }

            }

            //====================ジグザグ======================================================================================================================================================================================

            else
            {
                
                // No.1
                if (decide == 0)
                {
                    if (Calc.Agents[OurTeam, AgentNumber.One].Position.Y != 0)
                    {
                        if ((turn % 2) == 1)
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
                        num1 = Arrow.Right;
                    }
                }
                
                go[0] = Calc.Agents[OurTeam, AgentNumber.One].Position + num1;
                go[1] = Calc.Agents[OurTeam, AgentNumber.Two].Position + num2;
                turn++;
            }

 
//=============最後の色々===========================================================================================================================================================================================

            if (0 <= go[0].X && go[0].X < Calc.Field.Width && 0 <= go[0].Y && go[0].Y < Calc.Field.Height)
            {
                result[(int)AgentNumber.One] = MoveOrRemoveTile(go[0]);
            }
            if (0 <= go[1].X && go[1].X < Calc.Field.Width && 0 <= go[1].Y && go[1].Y < Calc.Field.Height)
            {
                result[(int)AgentNumber.Two] = MoveOrRemoveTile(go[1]);
            }
            return result;

        }


        private AgentActivityData MoveOrRemoveTile(Coordinate coordinate)
        {
            if (Calc.Field[coordinate].IsTileOn[OurTeam.Opponent()])
            {
                return new AgentActivityData(AgentStatusCode.RequestRemovementOpponentTile, coordinate);
            }
            return new AgentActivityData(AgentStatusCode.RequestMovement, coordinate);

        }

    }
}