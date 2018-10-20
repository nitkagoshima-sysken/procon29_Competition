using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitkagoshima_sysken.Procon29.Visualizer
{
    class FieldGenerator
    {
        public SymmetricalPattern SymmetricalPattern { get; set; }

        public Size Size { get; set; }

        public int Seed { get; set; }

        public FieldGenerator(SymmetricalPattern symmetrical, Size size, int seed)
        {
            SymmetricalPattern = symmetrical;
            Size = size;
            Seed = seed;
        }

        public FieldGenerator(string codename)
        {
            if (codename.IndexOf("HV") >= 0 ||
                codename.IndexOf("VH") >= 0)
            {
                SymmetricalPattern = SymmetricalPattern.HorizontallyAndVerticallySymmetrical;
            }
            else if (codename.IndexOf("H") >= 0)
            {
                SymmetricalPattern = SymmetricalPattern.HorizontallySymmetrical;
            }
            else if (codename.IndexOf("V") >= 0)
            {
                SymmetricalPattern = SymmetricalPattern.VerticallySymmetrical;
            }
            codename = codename.Replace("H", "").Replace("V", "");
            Size = AlphabetSize(codename[0]);
            codename = codename.Remove(0, 1);
            Seed = int.Parse(codename);
        }

        public Field Generate()
        {
            var random = new Random(Seed);
            var field = new Field(Size.Width, Size.Height);
            switch (SymmetricalPattern)
            {
                case SymmetricalPattern.HorizontallySymmetrical:
                    for (int x = 0; x < Size.Width / 2; x++)
                    {
                        for (int y = 0; y <= Size.Height; y++)
                        {
                            var cell = new Cell(new Coordinate(x, y));
                            cell.Point = (random.NextDouble() <= 0.9f) ? random.Next(1, 16) : random.Next(-16, 0);
                            field[x, y] = cell;
                            field[Size.Width - 1 - x, y] = new Cell(cell);
                        }
                    }
                    return field;
                case SymmetricalPattern.VerticallySymmetrical:
                    for (int x = 0; x < Size.Width; x++)
                    {
                        for (int y = 0; y <= Size.Height / 2; y++)
                        {
                            var cell = new Cell(new Coordinate(x, y));
                            cell.Point = (random.NextDouble() <= 0.9f) ? random.Next(1, 16) : random.Next(-16, 0);
                            field[x, y] = cell;
                            field[x, Size.Height - 1 - y] = new Cell(cell);
                        }
                    }
                    return field;
                case SymmetricalPattern.HorizontallyAndVerticallySymmetrical:
                    for (int x = 0; x < Size.Width / 2; x++)
                    {
                        for (int y = 0; y <= Size.Height / 2; y++)
                        {
                            var cell = new Cell(new Coordinate(x, y));
                            cell.Point = (random.NextDouble() <= 0.9f) ? random.Next(1, 16) : random.Next(-16, 0);
                            field[x, y] = cell;
                            field[Size.Width - 1 - x, y] = new Cell(cell);
                            field[x, Size.Height - 1 - y] = new Cell(cell);
                            field[Size.Width - 1 - x, Size.Height - 1 - y] = new Cell(cell);
                        }
                    }
                    return field;
                default:
                    throw new Exception();
            }
        }

        public static Size AlphabetSize(char c)
        {
            var size_dictionary = new Dictionary<char, Size>();
            var alphabet = "ABCDEFGIJKLMNOPQRSTUWXYZ";
            var count = 0;
            for (int x = 1; x <= 12; x++)
            {
                for (int y = 1; y <= 12; y++)
                {
                    if (x * y >= 80)
                    {
                        size_dictionary.Add(alphabet[count++], new Size(x, y));
                    }
                }
            }
            return size_dictionary[c];
        }
    }
}
