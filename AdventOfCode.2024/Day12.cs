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
    }
}
