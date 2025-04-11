namespace AdventOfCode
{
    using System.Drawing;
    using AdventOfCode.Helpers;

    internal class Day10 : Day
    {
        List<TrailPoint> trailPoints;
        List<TrailPoint> trailPeaks;
        int yMapSize;
        int xMapSize;

        public Day10(string input) : base(input)
        {
            trailPoints = new List<TrailPoint>();
            trailPeaks = new List<TrailPoint>();
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

            int trails = 0;
            int distinctTrails = 0;
            foreach (var head in trailHeads)
            {
                var current = head;
                distinctTrails += MoveTrail(current, trailPoints);
                trails += trailPeaks.GroupBy(x => x.Position).Select(y => y.First()).Count();
                trailPeaks.Clear();
            }

            Console.WriteLine("Trails: " + trails);
            Console.WriteLine("Distinct Trails: " + distinctTrails);
        }

        private int MoveTrail(TrailPoint current, List<TrailPoint> trail)
        {
            Console.WriteLine($"Current Position: {current.Position}, Height: {current.Height}");
            int trailScore = 0;
            if (current.Height == 9)
            {
                trailPeaks.Add(current);
                trailScore = 1;
            }
            else
            {
                var nextSteps = current.FindNext(trail);
                if (nextSteps.Count == 0)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine($"Next Position: {string.Join(", ", nextSteps.Select(x => x.Position))}, Height: {nextSteps.First().Height}");
                    foreach (var next in nextSteps)
                    {
                        trailScore += MoveTrail(next, trail);
                    }
                }
            }

            Console.WriteLine("Current Trailscore: " + trailScore);
            return trailScore;
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

        public TrailPoint(int height, Point position)
        {
            this.Height = height;
            this.Position = position;
        }

        public List<TrailPoint> FindNext(List<TrailPoint> trailPoints)
        {
            List<TrailPoint> nextSteps = new List<TrailPoint>();
            if (FindNorth(trailPoints) != null) nextSteps.Add(FindNorth(trailPoints));
            if (FindSouth(trailPoints) != null) nextSteps.Add(FindSouth(trailPoints));
            if (FindEast(trailPoints) != null) nextSteps.Add(FindEast(trailPoints));
            if (FindWest(trailPoints) != null) nextSteps.Add(FindWest(trailPoints));
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
