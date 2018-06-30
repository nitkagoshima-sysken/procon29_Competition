using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Coordinate() { X = 0; Y = 0; }
        public Coordinate(int x, int y) { X = x; Y = y; }
        public static implicit operator System.Drawing.Point(Coordinate point) { return new System.Drawing.Point(point.X, point.Y); }
        public static implicit operator Coordinate(System.Drawing.Point point) { return new Coordinate(point.X, point.Y); }
    }
}
