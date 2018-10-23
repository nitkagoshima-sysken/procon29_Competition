using System;
using System.Collections.Generic;

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
                    for (int x = 0; x <= Size.Width / 2; x++)
                    {
                        for (int y = 0; y < Size.Height; y++)
                        {
                            var cell = new Cell(new Coordinate(x, y));
                            cell.Point = GetPoint(random);
                            field[x, y] = cell;
                            field[Size.Width - 1 - x, y] = new Cell(cell) { Coordinate = new Coordinate(Size.Width - 1 - x, y) };
                        }
                    }
                    return field;
                case SymmetricalPattern.VerticallySymmetrical:
                    for (int x = 0; x < Size.Width; x++)
                    {
                        for (int y = 0; y <= Size.Height / 2; y++)
                        {
                            var cell = new Cell(new Coordinate(x, y));
                            cell.Point = GetPoint(random);
                            field[x, y] = cell;
                            field[x, Size.Height - 1 - y] = new Cell(cell) { Coordinate = new Coordinate(x, Size.Height - 1 - y) };
                        }
                    }
                    return field;
                case SymmetricalPattern.HorizontallyAndVerticallySymmetrical:
                    for (int x = 0; x <= Size.Width / 2; x++)
                    {
                        for (int y = 0; y <= Size.Height / 2; y++)
                        {
                            var cell = new Cell(new Coordinate(x, y));
                            cell.Point = GetPoint(random);
                            field[x, y] = cell;
                            field[Size.Width - 1 - x, y] = new Cell(cell) { Coordinate = new Coordinate(Size.Width - 1 - x, y) };
                            field[x, Size.Height - 1 - y] = new Cell(cell) { Coordinate = new Coordinate(x, Size.Height - 1 - y) };
                            field[Size.Width - 1 - x, Size.Height - 1 - y] = new Cell(cell) { Coordinate = new Coordinate(Size.Width - 1 - x, Size.Height - 1 - y) };
                        }
                    }
                    return field;
                default:
                    throw new Exception();
            }
        }

        private int GetPoint(Random random)
        {
            return (random.NextDouble() <= 0.9f) ? random.Next(1, 17) : random.Next(-16, 0);
        }

        public Coordinate[] AgentPositionGenerate(Field field)
        {
            var random = new Random(Seed);
            var coordinates = new Coordinate[2];
            do
            {
                coordinates[0] = new Coordinate(random.Next(0, Size.Width - 1), random.Next(0, Size.Height - 1));
                coordinates[1] = new Coordinate(random.Next(0, Size.Width - 1), random.Next(0, Size.Height - 1));
            } while (coordinates[0] == coordinates[1] ||
            coordinates[0] == MainForm.ComplementEnemysPosition(field, coordinates[0]) ||
            coordinates[0] == MainForm.ComplementEnemysPosition(field, coordinates[1]) ||
            coordinates[1] == MainForm.ComplementEnemysPosition(field, coordinates[0]) ||
            coordinates[1] == MainForm.ComplementEnemysPosition(field, coordinates[1]));
            return coordinates;
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

        public static string RandomID
        {
            get
            {
                var random = new Random();
                var s = new string[] { "H", "V", "HV" };
                var id = s[random.Next(0, 3)];
                id += "ABCDEFGIJKLMNOPQRSTUWXYZ"[random.Next(0, 24)];
                id += random.Next(0, 999).ToString("D3");
                return id;
            }
        }

        public static string GetRandomID(int seed)
        {
            var random = new Random(seed);
            var s = new string[] { "H", "V", "HV" };
            var id = s[random.Next(0, 3)];
            id += "ABCDEFGIJKLMNOPQRSTUWXYZ"[random.Next(0, 24)];
            id += random.Next(0, 999).ToString("D3");
            return id;
        }
    }
}
