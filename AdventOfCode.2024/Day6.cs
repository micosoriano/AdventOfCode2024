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
                    //guard.DistinctPositions.Add(guard.SpawnPoint);
                }
                yMapSize++;
            }

            mainGuard.Paths = pathList;
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
                    guard.Paths = new List<Path>(mainGuard.Paths);
                    guard.Obstructions = new List<Obstruction>(mainGuard.Obstructions);
                    guard.DistinctPositions = new List<Point>(mainGuard.DistinctPositions);
                    guard.SpawnPoint = mainGuard.SpawnPoint;
                    guard.Position = mainGuard.SpawnPoint;
                    guard.Direction = Direction.North;
                    Console.WriteLine("===Adding obstruction at position: " + newObstruction.Position + "===");
                    guard.Obstructions.Add(newObstruction);
                    if (guard.DoPatrol(true)) Console.WriteLine("Guard finished " + i);
                    else loops++;
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
            public List<Path> Paths { get; set; }

            private Direction direction;

            public Guard()
            {
                direction = Direction.North;
                DistinctPositions = new List<Point>();
                Obstructions = new List<Obstruction>();
                Paths = new List<Path>();
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
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                while (Obstructions.Any(o => o.Position == Position) || Paths.Any(p => p.Position == Position) || Position == SpawnPoint) {
                    if (Obstructions.Any(o => o.Position == NextStep())) Rotate();
                    else {
                        Move();
                        if (!DistinctPositions.Contains(Position) && !forTask2) DistinctPositions.Add(Position);
                    }

                    if (stopWatch.ElapsedMilliseconds >= 10000) {
                        Console.WriteLine("OH NOOOOOOO!!! IM STUUUCCCKKK HELP STEPBRO!!!");
                        return false;
                    }
                }
                stopWatch.Stop();
                Console.WriteLine("Guard finished in " + stopWatch.ElapsedMilliseconds + "ms");
                return true;
            }
        }
    }
}
