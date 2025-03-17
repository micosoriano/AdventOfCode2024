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
            int y = 0;

            while (!this.reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                var pathMatches = Regex.Matches(line, pathPattern);
                var obstructionMatches = Regex.Matches(line, obstructionPattern);
                var guardMatches = Regex.Matches(line, guardPattern);
                foreach (Match match in pathMatches)
                {
                    pathList.Add(new Path(match.Index, y));
                }
                foreach (Match match in obstructionMatches)
                {
                    obstructionList.Add(new Obstruction(match.Index, y));
                }
                foreach (Match match in guardMatches)
                {
                    guard.XLoc = match.Index;
                    guard.YLoc = y;
                }
                y++;
            }
        }
        public void Task1()
        {
            throw new NotImplementedException();
        }

        public void Task2()
        {
            throw new NotImplementedException();
        }

        interface ILabItem
        {
            int XLoc { get; set; }
            int YLoc { get; set; }
        }

        class LabItem : ILabItem
        {
            public int XLoc { get; set; }
            public int YLoc { get; set; }

            public LabItem(int x, int y)
            {
                XLoc = x;
                YLoc = y;
            }
        }

        class Path : LabItem
        {
            public Path(int x, int y) : base(x, y)
            {
            }
        }

        class Obstruction : LabItem
        {
            public Obstruction(int x, int y) : base(x, y)
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
            public int XLoc { get; set; }
            public int YLoc { get; set; }
            public List<Point> DistinctPositions { get; set; }

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
                switch (direction)
                {
                    case Direction.North:
                        YLoc--;
                        break;
                    case Direction.East:
                        XLoc++;
                        break;
                    case Direction.South:
                        YLoc++;
                        break;
                    case Direction.West:
                        XLoc--;
                        break;
                }
            }
        }
    }
}
