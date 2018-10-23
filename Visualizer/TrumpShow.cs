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

        List<Trump> Trumps { get; set; }

        public PictureBox PictureBox { get; set; }

        void Showing(AgentActivityData[] actions)
        {

        }

        void LoadTrumpPicture()
        {
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
                        case TrumpNumber.Jack:
                            return Properties.Resources.Spade_Ten;
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
                        case TrumpNumber.Jack:
                            return Properties.Resources.Heart_Ten;
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
                        case TrumpNumber.Jack:
                            return Properties.Resources.Diamond_Ten;
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
                        case TrumpNumber.Jack:
                            return Properties.Resources.Club_Ten;
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
