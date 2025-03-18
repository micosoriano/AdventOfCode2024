namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Helpers;

    internal class Day6 : Day
    {
        readonly string pathPattern = @"\.";
        readonly string obstructionPattern = @"\#";
        readonly string guardPattern = @"\^";
        readonly int yMapSize = 1;
        readonly Guard mainGuard;

        public Day6(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 5");
            mainGuard = new Guard();
            List<Path> pathList = new List<Path>();
            List<Obstruction> obstructionList = new List<Obstruction>();

            while (!this.reader.EndOfStream) {
                string line = reader.ReadLine()!;
                var pathMatches = Regex.Matches(line, pathPattern);
                var obstructionMatches = Regex.Matches(line, obstructionPattern);
                var guardMatches = Regex.Matches(line, guardPattern);
                foreach (Match match in pathMatches) pathList.Add(new Path(new Point(match.Index + 1, yMapSize)));
                foreach (Match match in obstructionMatches) obstructionList.Add(new Obstruction(new Point(match.Index + 1, yMapSize)));
                foreach (Match match in guardMatches) {
                    mainGuard.SpawnPoint = new Point(match.Index + 1, yMapSize);
                    mainGuard.Position = mainGuard.SpawnPoint;
                    mainGuard.DistinctPositions.Add(mainGuard.SpawnPoint);
                }
                yMapSize++;
            }

            mainGuard.Obstructions = obstructionList;
        }


        public void Task1()
        {
            mainGuard.DoPatrol();
            Console.WriteLine("Distinct positions: " + mainGuard.DistinctPositions.Count);
        }

        public void Task2()
        {
            mainGuard.DoPatrol();
            int loops = 0;
            Parallel.ForEach(mainGuard.DistinctPositions, i =>
            {
                var newObstruction = new Obstruction(i);
                if (!mainGuard.Obstructions.Any(o => o.Position == newObstruction.Position) && newObstruction.Position != mainGuard.SpawnPoint)
                {
                    var guard = new Guard();
                    guard.Obstructions = new List<Obstruction>(mainGuard.Obstructions);
                    guard.DistinctPositions = new List<Point>(mainGuard.DistinctPositions);
                    guard.SpawnPoint = mainGuard.SpawnPoint;
                    guard.Position = mainGuard.SpawnPoint;
                    guard.Direction = Direction.North;
                    guard.Obstructions.Add(newObstruction);

                    if (guard.DoPatrol(true)) Console.WriteLine("Guard finished " + i);
                    else {
                        Console.WriteLine("Guard stuck" + i);
                        loops++;
                    }
                    guard.Obstructions.Remove(newObstruction);
                }
            });

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
            public List<Obstruction> Obstructions { get; set; }

            private Direction direction;
            int yMapSize;
            int xMapSize;

            public Guard()
            {
                direction = Direction.North;
                DistinctPositions = new List<Point>();
                Obstructions = new List<Obstruction>();
            }

            private void SetMapSize()
            {
                yMapSize = Obstructions.Max(o => o.Position.Y);
                xMapSize = Obstructions.Max(o => o.Position.X);
            }

            public void Rotate()
            {
                switch (direction) {
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
                switch (direction) {
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

            public bool DoPatrol(bool forTask2 = false)
            {
                SetMapSize();
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                while ((Position.X < xMapSize && Position.Y < yMapSize)
                    && (Position.X > 0 && Position.Y > 0)) {
                    if (Obstructions.Any(o => o.Position == NextStep())) Rotate();
                    else {
                        Move();
                        if (!forTask2) if (!DistinctPositions.Contains(Position)) DistinctPositions.Add(Position);
                    }

                    if (stopWatch.ElapsedMilliseconds >= 5000) {
                        return false;
                    }
                }
                stopWatch.Stop();
                return true;
            }
        }
    }
}
