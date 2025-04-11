namespace AdventOfCode
{
    using System.Drawing;
    using AdventOfCode.Helpers;

    internal class Day10 : Day
    {
        List<TrailPoint> trailPoints;
        int yMapSize;
        int xMapSize;

        public Day10(string input) : base(input)
        {
            trailPoints = new List<TrailPoint>();
            Console.WriteLine("Advent of Code Day 10");

            int y = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine()!;
                int x = 0;
                foreach (var c in line)
                {
                    trailPoints.Add(new TrailPoint(int.Parse(c.ToString()), new Point(x, y)));
                    x++;
                }
                y++;
            }

            xMapSize = trailPoints.Max(x => x.Position.X);
            yMapSize = trailPoints.Max(x => x.Position.Y);
        }

        public void Task1()
        {
            var trailHeads = trailPoints.Where(x => x.Height == 0);

            foreach (var head in trailHeads)
            {
                var current = head;
                var nextSteps = new List<TrailPoint>();
                nextSteps = current.FindNext(trailPoints);
                if (nextSteps.Count == 0) break;

                Parallel.ForEach(nextSteps, i =>
                {
                    while (nextSteps.Where(x => x.Height == 9) == null)
                    {

                    }
                });
            }
        }
    }

    enum Direction
    {
        North,
        East,
        South,
        West
    }

    class TrailPoint
    {
        public Point Position { get; }
        public int Height { get; }

        Direction direction;

        public TrailPoint(int height, Point position)
        {
            this.Height = height;
            this.Position = position;
        }

        public List<TrailPoint> FindNext(List<TrailPoint> trailPoints)
        {
            List<TrailPoint> nextSteps = new List<TrailPoint>();
            nextSteps.Add(FindNorth(trailPoints));
            nextSteps.Add(FindSouth(trailPoints));
            nextSteps.Add(FindEast(trailPoints));
            nextSteps.Add(FindWest(trailPoints));
            return nextSteps;
        }

        public TrailPoint FindNorth(List<TrailPoint> trail)
        {
            return trail.Find(x => x.Position == new Point(this.Position.X, this.Position.Y - 1) && x.Height == this.Height + 1)!;
        }

        public TrailPoint FindSouth(List<TrailPoint> trail)
        {
            return trail.Find(x => x.Position == new Point(this.Position.X, this.Position.Y + 1) && x.Height == this.Height + 1)!;
        }

        public TrailPoint FindEast(List<TrailPoint> trail)
        {
            return trail.Find(x => x.Position == new Point(this.Position.X + 1, this.Position.Y) && x.Height == this.Height + 1)!;
        }

        public TrailPoint FindWest(List<TrailPoint> trail)
        {
            return trail.Find(x => x.Position == new Point(this.Position.X - 1, this.Position.Y) && x.Height == this.Height + 1)!;
        }
    }
}
