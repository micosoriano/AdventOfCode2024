namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using AdventOfCode.Helpers;

    internal class Day6 : Day, IDay
    {
        readonly string pathPattern = @"\.";
        readonly string obstructionPattern = @"\#";
        readonly string guardPattern = @"\^";
        readonly List<Path> pathList = new List<Path>();
        readonly List<Obstruction> obstructionList = new List<Obstruction>();
        readonly Guard guard;

        public Day6(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 5");

            guard = new Guard();
            int y = 1;

            while (!this.reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                var pathMatches = Regex.Matches(line, pathPattern);
                var obstructionMatches = Regex.Matches(line, obstructionPattern);
                var guardMatches = Regex.Matches(line, guardPattern);
                foreach (Match match in pathMatches)
                {
                    pathList.Add(new Path(new Point(match.Index, y)));
                }
                foreach (Match match in obstructionMatches)
                {
                    obstructionList.Add(new Obstruction(new Point(match.Index, y)));
                }
                foreach (Match match in guardMatches)
                {
                    guard.SpawnPoint = new Point(match.Index, y);
                    guard.Position = guard.SpawnPoint;
                }
                y++;
            }
        }
        public void Task1()
        {
            guard.DistinctPositions.Add(guard.Position);
            do
            {
                if (obstructionList.Any(o => o.Position == guard.NextStep()))
                {
                    Console.WriteLine("Obstruction found at position: " + guard.NextStep());
                    guard.Rotate();
                }
                else
                {
                    guard.Move();
                }
            }
            while (obstructionList.Any(o => o.Position == guard.Position) || pathList.Any(p => p.Position == guard.Position) || guard.Position == guard.SpawnPoint);

            Console.WriteLine(guard.DistinctPositions.Count);
        }

        public void Task2()
        {
            throw new NotImplementedException();
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

            private Direction direction;

            public Guard()
            {
                direction = Direction.North;
                DistinctPositions = new List<Point>();
            }

            public void Rotate()
            {
                Console.WriteLine("Guard current direction: " + direction);
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

                Console.WriteLine("Guard rotated to direction: " + direction);
            }

            public void Move()
            {
                Console.WriteLine("Guard current position: " + Position);
                Position = NextStep();

                Console.WriteLine("Guard moved to position: " + Position);
                if (!DistinctPositions.Contains(Position))
                {
                    Console.WriteLine("Adding position to distinct positions: " + Position);
                    DistinctPositions.Add(Position);
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
}
