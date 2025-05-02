namespace AdventOfCode
{
    using System.Drawing;
    using System.Security;
    using AdventOfCode.Helpers;

    internal class Day12 : Day
    {
        List<Plant> garden;
        List<Plant> tempGarden;
        public Day12(string input) : base(input)
        {
            garden = new List<Plant>();
            tempGarden = new List<Plant>();
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
            foreach (var plant in garden)
            {
                plant.TryFindNext(garden, out var adjacent);
            }

            tempGarden = new List<Plant>(garden);
            var groupedGarden = new List<List<Plant>>();

            for (;tempGarden.Count > 0;)
            {
                var current = tempGarden[0];
                tempGarden.Remove(current);
                groupedGarden.Add(GetRegion(current));
            }

            int price = 0;
            foreach (var group in groupedGarden)
            {
                int area = 0;
                int perimeter = 0;
                area += group.Count;
                foreach (var plant in group)
                {
                    perimeter += plant.GetPerimeter();
                }
                price += area * perimeter;
            }


            Console.WriteLine(price);
        }

        public List<Plant> GetRegion(Plant plant)
        {
            List<Plant> region = new List<Plant>();
            region.Add(plant);
            if (plant.Adjacent.Count == 0) return region;
            else
            {
                foreach (var adj in plant.Adjacent)
                {
                    if (tempGarden.Contains(adj))
                    {
                        tempGarden.Remove(adj);
                        region.AddRange(GetRegion(adj));
                    }
                }
            }

            return region;
        }
    }

    class Plant
    {
        public Point Position { get; }
        public char Value { get; }
        public Plant? North { get; private set; }
        public Plant? South { get; private set; }
        public Plant? East { get; private set; }
        public Plant? West { get; private set; }
        public List<Plant> Adjacent { get; private set; }

        public Plant(char value, Point position)
        {
            this.Adjacent = new List<Plant>();
            this.Value = value;
            this.Position = position;
        }

        public int GetPerimeter()
        {
            int perimeter = 4;

            if (North != null) perimeter--;
            if (South != null) perimeter--;
            if (East != null) perimeter--;
            if (West != null) perimeter--;

            return perimeter;
        }

        public bool TryFindNext(List<Plant> garden, out List<Plant> adjacent)
        {
            List<Plant> nextSteps = new List<Plant>();
            if (TryFindNorth(garden, out var north))
            {
                nextSteps.Add(north);
                this.North = north;
            }

            if (TryFindSouth(garden, out var south))
            {
                nextSteps.Add(south);
                this.South = south;
            }

            if (TryFindEast(garden, out var east))
            {
                nextSteps.Add(east);
                this.East = east;
            }

            if (TryFindWest(garden, out var west))
            {
                nextSteps.Add(west);
                this.West = west;
            }
            adjacent = nextSteps;
            this.Adjacent = nextSteps;
            return nextSteps.Count > 0;
        }

        public bool TryFindNorth(List<Plant> garden, out Plant plant)
        {
            plant = garden.Find(x => x.Position == new Point(this.Position.X, this.Position.Y - 1) && x.Value == this.Value)!;
            return plant != null;
        }

        public bool TryFindSouth(List<Plant> garden, out Plant plant)
        {
            plant = garden.Find(x => x.Position == new Point(this.Position.X, this.Position.Y + 1) && x.Value == this.Value)!;
            return plant != null;
        }

        public bool TryFindEast(List<Plant> garden, out Plant plant)
        {
            plant = garden.Find(x => x.Position == new Point(this.Position.X + 1, this.Position.Y) && x.Value == this.Value)!;
            return plant != null;
        }

        public bool TryFindWest(List<Plant> garden, out Plant plant)
        {
            plant = garden.Find(x => x.Position == new Point(this.Position.X - 1, this.Position.Y) && x.Value == this.Value)!;
            return plant != null;
        }
    }
}
