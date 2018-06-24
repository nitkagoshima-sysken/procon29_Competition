using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procon29_Visualizer
{
    abstract class Bot
    {
        Calc calc;

        internal abstract Calc Calc { get; set; }

        Bot(Calc calc)
        {
            Calc = calc;
        }
    }
}
