namespace AdventOfCode
{
    using System.Drawing;
    using AdventOfCode.Helpers;

    internal class Day12 : Day
    {
        List<Plant> garden;
        List<Plant> localRegion;
        public Day12(string input) : base(input)
        {
            garden = new List<Plant>();
            localRegion = new List<Plant>();
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
            List<List <Plant>> regions = new List<List<Plant>>();
            foreach (var plant in garden)
            {
                plant.TryFindNext(garden, out var adjacent);
            }

            foreach (var plant in garden)
            {
                localRegion.Clear();
                GetRegion(plant);
                regions.Add(localRegion);
            }
        }

        public List<Plant> GetRegion(Plant plant)
        {
            List<Plant> region = new List<Plant>();
            if (plant.Adjacent.Count > 0)
            {
                foreach (var adj in plant.Adjacent)
                {
                    if (localRegion.Contains(adj)) continue;
                    localRegion.Add(adj);
                    var locRegion = GetRegion(adj);
                    localRegion.AddRange(locRegion);
                    region = localRegion;
                }
            }
            else
            {
                localRegion.Add(plant);
                region.Add(plant);
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

        public int GetPerimeter(List<Plant> garden)
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
