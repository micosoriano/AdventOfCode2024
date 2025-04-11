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
                while (current.Height != 9)
                {
                    current = current.FindNext(trailPoints);
                    if (current == null) break;
                }
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

        public TrailPoint FindNext(List<TrailPoint> trailPoints)
        {
            var next = trailPoints.FirstOrDefault(x => x.Position == NextStep());
            if (next != null)
            {
                if (next.Height == Height + 1)
                {
                    return next;
                }
            }

            return null!;
        }

        public void Rotate()
        {
            switch (direction)
            {
                case Direction.North:
                    direction = Direction.East;
                    break;
                case Direction.East:
                    direction = Direction.South;
                    break;
                case Direction.South:
                    direction = Direction.West;
                    break;
                case Direction.West:
                    direction = Direction.North;
                    break;
            }
        }

        public Point NextStep()
        {
            Point nextStep = new Point();
            switch (direction)
            {
                case Direction.North:
                    nextStep = new Point(Position.X, Position.Y - 1);
                    break;
                case Direction.East:
                    nextStep = new Point(Position.X + 1, Position.Y);
                    break;
                case Direction.South:
                    nextStep = new Point(Position.X, Position.Y + 1);
                    break;
                case Direction.West:
                    nextStep = new Point(Position.X - 1, Position.Y);
                    break;
            }

            return nextStep;
        }
    }
}
