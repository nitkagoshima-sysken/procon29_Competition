using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    static class PictureBoxExtension
    {
        static public Coordinate ToCellCordinate(this PictureBox pictureBox, Calc calc, Coordinate mouse)
        {
            var fieldWidth = ((pictureBox.Width <= 0) ? 1 : pictureBox.Width) / calc.Field.Width;
            var fieldHeight = ((pictureBox.Height <= 0) ? 1 : pictureBox.Height) / calc.Field.Height;
            return new Coordinate(
                x: mouse.X / ((fieldWidth <= 0) ? 1 : fieldWidth),
                y: mouse.Y / ((fieldHeight <= 0) ? 1 : fieldHeight));
        }
    }
}
