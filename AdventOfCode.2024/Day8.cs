namespace AdventOfCode
{
    using System.Drawing;
    using System.Text.RegularExpressions;
    using AdventOfCode.Helpers;

    internal class Day8 : Day
    {
        readonly string freqPattern = @"[a-zA-Z0-9]";
        readonly string pathPattern = @"\.";
        readonly int yMapSize;
        readonly int xMapSize;
        readonly List<Antenna> antennaList = new List<Antenna>();
        readonly List<AntennaPair> antennaPairs;

        public Day8(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 8");
            int y = 0;
            while (!this.reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                var freqMatches = Regex.Matches(line, freqPattern);
                var pathMatches = Regex.Matches(line, pathPattern);
                foreach (Match match in freqMatches) antennaList.Add(new Antenna(match.Value, new Point(match.Index, y)));
                xMapSize = Math.Max(xMapSize, pathMatches.OrderBy(x => x.Index).Select(x => x.Index + 1).Last());
                y++;
            }
            yMapSize = y;

            antennaPairs = new List<AntennaPair>();
            foreach (var antenna in antennaList)
            {
                foreach (var otherAntenna in antennaList)
                {
                    if (antenna.Position != otherAntenna.Position & antenna.Value == otherAntenna.Value)
                    {
                        antennaPairs.Add(new AntennaPair(antenna, otherAntenna));
                    }
                }
            }
        }

        public void Task1()
        {

        }
    }

    class Antenna
    {
        public string Value { get; }
        public Point Position { get; }
        public Antenna(string value, Point position)
        {
            this.Value = value;
            this.Position = position;
        }
    }

    class AntiNode
    {
        public Point Position { get; }
        public AntiNode(Point position)
        {
            this.Position = position;
        }
    }

    class AntennaPair
    {
        public AntiNodePair AntiNodePair { get; }

        public AntennaPair(Antenna pair1, Antenna pair2)
        {
            AntiNodePair = new AntiNodePair(pair1, pair2);
        }
    }


    class AntiNodePair
    {
        public (AntiNode, AntiNode) NodePair { get; }
        public AntiNodePair(Antenna pair1, Antenna pair2)
        {
            int xDiff = Math.Abs(pair1.Position.X - pair2.Position.X);
            int yDiff = Math.Abs(pair1.Position.Y - pair2.Position.Y);

            NodePair = (new AntiNode(new Point(pair1.Position.X + xDiff, pair1.Position.Y + yDiff)), new AntiNode(new Point(pair2.Position.X - xDiff, pair2.Position.Y - yDiff)));
        }
    }
}
