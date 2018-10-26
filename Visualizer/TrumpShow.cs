using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    class TrumpShow
    {
        Trump Trump { get; set; }

        List<Trump> Trumps1 { get; set; } = new List<Trump>();
        List<Trump> Trumps2 { get; set; } = new List<Trump>();

        Random Random { get; set; } = new Random(1000);

        public PictureBox PictureBox { get; set; }

        private readonly float Small = 7.0f;

        float Start { get; set; } = 0.0f;

        private readonly float Margin = 0.5f;

        public void Showing(Calc calc, Team team, AgentActivityData[] actions)
        {
            Start = 0.0f;
            Trumps1.Clear();
            Trumps2.Clear();
            Coordinate coordinate = actions[0].Destination - calc.Agents[team, AgentNumber.One].Position;
            if (coordinate.X < -1 || 1 < coordinate.X ||
                coordinate.Y < -1 || 1 < coordinate.Y ||
                coordinate == new Coordinate(0, 0))
            {
                return;
            }
            var arrow = ToArrow(coordinate);
            switch (arrow)
            {
                case Arrow.Up:
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = TrumpNumber.Six });
                    break;
                case Arrow.UpRight:
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = TrumpNumber.Six });
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = ReturnAnotherNumber() });
                    break;
                case Arrow.Right:
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = TrumpNumber.Nine });
                    break;
                case Arrow.DownRight:
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = TrumpNumber.Nine });
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = ReturnAnotherNumber() });
                    break;
                case Arrow.Down:
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = TrumpNumber.Queen });
                    break;
                case Arrow.DownLeft:
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = TrumpNumber.Queen });
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = ReturnAnotherNumber() });
                    break;
                case Arrow.Left:
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = TrumpNumber.Three });
                    break;
                case Arrow.UpLeft:
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = TrumpNumber.Three });
                    Trumps1.Add(new Trump { TrumpMark = ReturnBlack(), TrumpNumber = ReturnAnotherNumber() });
                    break;
            }

            coordinate = actions[1].Destination - calc.Agents[team, AgentNumber.Two].Position;
            if (coordinate.X < -1 || 1 < coordinate.X ||
                coordinate.Y < -1 || 1 < coordinate.Y)
            {
                return;
            }
            arrow = ToArrow(coordinate);
            switch (arrow)
            {
                case Arrow.Up:
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = TrumpNumber.Six });
                    break;
                case Arrow.UpRight:
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = TrumpNumber.Six });
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = ReturnAnotherNumber() });
                    break;
                case Arrow.Right:
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = TrumpNumber.Nine });
                    break;
                case Arrow.DownRight:
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = TrumpNumber.Nine });
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = ReturnAnotherNumber() });
                    break;
                case Arrow.Down:
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = TrumpNumber.Queen });
                    break;
                case Arrow.DownLeft:
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = TrumpNumber.Queen });
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = ReturnAnotherNumber() });
                    break;
                case Arrow.Left:
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = TrumpNumber.Three });
                    break;
                case Arrow.UpLeft:
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = TrumpNumber.Three });
                    Trumps2.Add(new Trump { TrumpMark = ReturnRed(), TrumpNumber = ReturnAnotherNumber() });
                    break;
            }

            Bitmap canvas = new Bitmap(PictureBox.Width, PictureBox.Height);
            Graphics g = Graphics.FromImage(canvas);

            if (actions[0].AgentStatusData.IsMovement())
            {
                foreach (var trump in Trumps1)
                {
                    var bmp = GetTrumpBitMap(trump);
                    g.DrawImage(bmp, Start, 0, bmp.Width / Small, bmp.Height / Small);
                    Start += bmp.Width / Small + 5.0f;
                }
            }
            else
            {
                foreach (var trump in Trumps1)
                {
                    var bmp = GetTrumpBitMap(trump);
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(bmp, Start, 0, bmp.Width / Small, bmp.Height / Small);
                    Start += bmp.Width / Small + 5.0f;
                }
            }

            if (actions[1].AgentStatusData.IsMovement())
            {
                foreach (var trump in Trumps2)
                {
                    var bmp = GetTrumpBitMap(trump);
                    g.DrawImage(bmp, Start, 0, bmp.Width / Small, bmp.Height / Small);
                    Start += bmp.Width / Small + 5.0f;
                }
            }
            else
            {
                foreach (var trump in Trumps2)
                {
                    var bmp = GetTrumpBitMap(trump);
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(bmp, Start, 0, bmp.Width / Small, bmp.Height / Small);
                    Start += bmp.Width / Small + 5.0f;
                }
            }

            g.Dispose();
            PictureBox.Image = canvas;
        }

        TrumpMark ReturnBlack()
        {
            return (Random.Next(1) == 0) ? TrumpMark.Club : TrumpMark.Spade;
        }

        TrumpMark ReturnRed()
        {
            return (Random.Next(1) == 0) ? TrumpMark.Diamond : TrumpMark.Heart;
        }

        TrumpNumber ReturnAnotherNumber()
        {
            var list = new List<TrumpNumber>
            {
                TrumpNumber.Ace,
                TrumpNumber.Two,
                TrumpNumber.Four,
                TrumpNumber.Five,
                TrumpNumber.Seven,
                TrumpNumber.Eight,
                TrumpNumber.Ten,
                TrumpNumber.Jack,
                TrumpNumber.King,
            };
            return list[Random.Next(list.Count)];
        }

        Arrow ToArrow(Coordinate coordinate)
        {
            foreach (Arrow arrow in Enum.GetValues(typeof(Arrow)))
            {
                if (new Coordinate(0, 0) + arrow == coordinate)
                {
                    return arrow;
                }
            }
            throw new ArgumentException();
        }

        private Bitmap GetTrumpBitMap(Trump trump)
        {
            switch (trump.TrumpMark)
            {
                case TrumpMark.Spade:
                    switch (trump.TrumpNumber)
                    {
                        case TrumpNumber.Ace:
                            return Properties.Resources.Spade_Ace;
                        case TrumpNumber.Two:
                            return Properties.Resources.Spade_Two;
                        case TrumpNumber.Three:
                            return Properties.Resources.Spade_Three;
                        case TrumpNumber.Four:
                            return Properties.Resources.Spade_Four;
                        case TrumpNumber.Five:
                            return Properties.Resources.Spade_Five;
                        case TrumpNumber.Six:
                            return Properties.Resources.Spade_Six;
                        case TrumpNumber.Seven:
                            return Properties.Resources.Spade_Seven;
                        case TrumpNumber.Eight:
                            return Properties.Resources.Spade_Eight;
                        case TrumpNumber.Nine:
                            return Properties.Resources.Spade_Nine;
                        case TrumpNumber.Ten:
                            return Properties.Resources.Spade_Ten;
                        case TrumpNumber.Jack:
                            return Properties.Resources.Spade_Jack;
                        case TrumpNumber.Queen:
                            return Properties.Resources.Spade_Queen;
                        case TrumpNumber.King:
                            return Properties.Resources.Spade_King;
                    }
                    break;
                case TrumpMark.Heart:
                    switch (trump.TrumpNumber)
                    {
                        case TrumpNumber.Ace:
                            return Properties.Resources.Heart_Ace;
                        case TrumpNumber.Two:
                            return Properties.Resources.Heart_Two;
                        case TrumpNumber.Three:
                            return Properties.Resources.Heart_Three;
                        case TrumpNumber.Four:
                            return Properties.Resources.Heart_Four;
                        case TrumpNumber.Five:
                            return Properties.Resources.Heart_Five;
                        case TrumpNumber.Six:
                            return Properties.Resources.Heart_Six;
                        case TrumpNumber.Seven:
                            return Properties.Resources.Heart_Seven;
                        case TrumpNumber.Eight:
                            return Properties.Resources.Heart_Eight;
                        case TrumpNumber.Nine:
                            return Properties.Resources.Heart_Nine;
                        case TrumpNumber.Ten:
                            return Properties.Resources.Heart_Ten;
                        case TrumpNumber.Jack:
                            return Properties.Resources.Heart_Jack;
                        case TrumpNumber.Queen:
                            return Properties.Resources.Heart_Queen;
                        case TrumpNumber.King:
                            return Properties.Resources.Heart_King;
                    }
                    break;
                case TrumpMark.Diamond:
                    switch (trump.TrumpNumber)
                    {
                        case TrumpNumber.Ace:
                            return Properties.Resources.Diamond_Ace;
                        case TrumpNumber.Two:
                            return Properties.Resources.Diamond_Two;
                        case TrumpNumber.Three:
                            return Properties.Resources.Diamond_Three;
                        case TrumpNumber.Four:
                            return Properties.Resources.Diamond_Four;
                        case TrumpNumber.Five:
                            return Properties.Resources.Diamond_Five;
                        case TrumpNumber.Six:
                            return Properties.Resources.Diamond_Six;
                        case TrumpNumber.Seven:
                            return Properties.Resources.Diamond_Seven;
                        case TrumpNumber.Eight:
                            return Properties.Resources.Diamond_Eight;
                        case TrumpNumber.Nine:
                            return Properties.Resources.Diamond_Nine;
                        case TrumpNumber.Ten:
                            return Properties.Resources.Diamond_Ten;
                        case TrumpNumber.Jack:
                            return Properties.Resources.Diamond_Jack;
                        case TrumpNumber.Queen:
                            return Properties.Resources.Diamond_Queen;
                        case TrumpNumber.King:
                            return Properties.Resources.Diamond_King;
                    }
                    break;
                case TrumpMark.Club:
                    switch (trump.TrumpNumber)
                    {
                        case TrumpNumber.Ace:
                            return Properties.Resources.Club_Ace;
                        case TrumpNumber.Two:
                            return Properties.Resources.Club_Two;
                        case TrumpNumber.Three:
                            return Properties.Resources.Club_Three;
                        case TrumpNumber.Four:
                            return Properties.Resources.Club_Four;
                        case TrumpNumber.Five:
                            return Properties.Resources.Club_Five;
                        case TrumpNumber.Six:
                            return Properties.Resources.Club_Six;
                        case TrumpNumber.Seven:
                            return Properties.Resources.Club_Seven;
                        case TrumpNumber.Eight:
                            return Properties.Resources.Club_Eight;
                        case TrumpNumber.Nine:
                            return Properties.Resources.Club_Nine;
                        case TrumpNumber.Ten:
                            return Properties.Resources.Club_Ten;
                        case TrumpNumber.Jack:
                            return Properties.Resources.Club_Jack;
                        case TrumpNumber.Queen:
                            return Properties.Resources.Club_Queen;
                        case TrumpNumber.King:
                            return Properties.Resources.Club_King;
                    }
                    break;
                case TrumpMark.Joker:
                    return Properties.Resources.Joker;
            }
            throw new ArgumentException();
        }
    }
}
