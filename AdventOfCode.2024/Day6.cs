namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using AdventOfCode.Helpers;

    internal class Day6 : Day
    {
        readonly string pathPattern = @"\.";
        readonly string obstructionPattern = @"\#";
        readonly string guardPattern = @"\^";
        readonly int yMapSize = 1;
        readonly int xMapSize = 0;
        readonly List<Path> pathList = new List<Path>();
        readonly List<Obstruction> obstructionList = new List<Obstruction>();
        readonly Guard guard;

        public Day6(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 5");

            guard = new Guard();

            while (!this.reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                var pathMatches = Regex.Matches(line, pathPattern);
                var obstructionMatches = Regex.Matches(line, obstructionPattern);
                var guardMatches = Regex.Matches(line, guardPattern);
                foreach (Match match in pathMatches)
                {
                    pathList.Add(new Path(new Point(match.Index + 1, yMapSize)));
                }
                foreach (Match match in obstructionMatches)
                {
                    obstructionList.Add(new Obstruction(new Point(match.Index + 1, yMapSize)));
                }
                foreach (Match match in guardMatches)
                {
                    guard.SpawnPoint = new Point(match.Index + 1, yMapSize);
                    guard.Position = guard.SpawnPoint;
                }
                yMapSize++;
                xMapSize = line.Length;
            }
        }
        public bool Task1()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            do
            {
                if (obstructionList.Any(o => o.Position == guard.NextStep()))
                {
                    guard.Rotate();
                }
                else
                {
                    guard.Move();
                }

                if (stopWatch.ElapsedMilliseconds >= 3000)
                {
                    Console.WriteLine("Guard stuck");
                    return false;
                }
            }
            while (obstructionList.Any(o => o.Position == guard.Position) || pathList.Any(p => p.Position == guard.Position) || guard.Position == guard.SpawnPoint);
            stopWatch.Stop();
            //Console.WriteLine(guard.DistinctPositions.Count);
            return true;
        }

        public void Task2()
        {
            int loops = 0;
            int y = 1;
            Obstruction newObstruction;
            while (y < yMapSize)
            {
                int x = 1;
                do
                {
                    newObstruction = new Obstruction(new Point(x, y));
                    if (!obstructionList.Any(o => o.Position == newObstruction.Position) && newObstruction.Position != guard.SpawnPoint)
                    {
                        Console.WriteLine("===Adding obstruction at position: " + newObstruction.Position + "===");
                        obstructionList.Add(newObstruction);
                        guard.Position = guard.SpawnPoint;
                        guard.Direction = Direction.North;
                        if (Task1())
                        {
                            Console.WriteLine("Guard finished");
                        }
                        else loops++;
                        obstructionList.Remove(newObstruction);
                    }

                    x++;
                }
                while (x <= xMapSize);
                y++;
            }

            Console.WriteLine("Loops: " + loops);

        }

        interface ILabItem
        {
            Point Position { get; set; }
        }

        class LabItem : ILabItem
        {
            public Point Position { get; set; }

            public LabItem(Point position)
            {
                Position = position;
            }
        }

        class Path : LabItem
        {
            public Path(Point position) : base(position)
            {
            }
        }

        class Obstruction : LabItem
        {
            public Obstruction(Point position) : base(position)
            {
            }
        }

        enum Direction
        {
            North,
            East,
            South,
            West
        }

        class Guard : ILabItem
        {
            public Point SpawnPoint { get; set; }
            public Point Position { get; set; }
            public List<Point> DistinctPositions { get; set; }
            public Direction Direction { get => direction; set => direction = value; }

            private Direction direction;

            public Guard()
            {
                direction = Direction.North;
                DistinctPositions = new List<Point>();
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

            public void Move()
            {
                Position = NextStep();
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
}
