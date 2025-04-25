namespace AdventOfCode
{
    using System.Drawing;
    using AdventOfCode.Helpers;

    internal class Day12 : Day
    {
        List<Plant> garden;
        public Day12(string input) : base(input)
        {
            garden = new List<Plant>();
            Console.WriteLine("Advent of Code Day 12");
            int y = 0;
            while (!reader.EndOfStream)
            {
                int x = 0;
                foreach (var plant in reader.ReadLine()!)
                {
                    garden.Add(new Plant(plant, new Point(x, y)));
                    x++;
                }
                y++;
            }
        }

        public void Task1()
        {

        }
    }

    class Plant
    {
        public Point Position { get; }
        public char Value { get; }
        public Plant(char value, Point position)
        {
            this.Value = value;
            this.Position = position;
        }

        public int GetPerimeter(List<Plant> garden)
        {
            int perimeter = 4;

            if (FindNorth(garden)) perimeter--;
            if (FindSouth(garden)) perimeter--;
            if (FindEast(garden)) perimeter--;
            if (FindWest(garden)) perimeter--;

            return perimeter;
        }

        public bool FindNorth(List<Plant> garden)
        {
            return garden.Find(x => x.Position == new Point(this.Position.X, this.Position.Y - 1) && x.Value == this.Value)! != null;
        }

        public bool FindSouth(List<Plant> garden)
        {
            return garden.Find(x => x.Position == new Point(this.Position.X, this.Position.Y + 1) && x.Value == this.Value)! != null;
        }

        public bool FindEast(List<Plant> garden)
        {
            return garden.Find(x => x.Position == new Point(this.Position.X + 1, this.Position.Y) && x.Value == this.Value)! != null;
        }

        public bool FindWest(List<Plant> garden)
        {
            return garden.Find(x => x.Position == new Point(this.Position.X - 1, this.Position.Y) && x.Value == this.Value)! != null;
        }
    }
}
