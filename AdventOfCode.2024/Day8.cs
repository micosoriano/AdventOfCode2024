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
            List<AntiNode> totalNodes = new List<AntiNode>();
            foreach (var pair in antennaPairs)
            {
                totalNodes.AddRange(pair.AntiNodePair.NodePair);
            }

            totalNodes = totalNodes.GroupBy(x => x.Position).Select(g => g.First()).ToList();

            var validNodes = totalNodes.Where(x => x.Position.X < xMapSize && x.Position.Y < yMapSize && x.Position.X >= 0 && x.Position.Y >= 0);
            Console.WriteLine("Valid Nodes: " + validNodes.Count());
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
        public (Antenna, Antenna) Pair { get; set; }
        public AntiNodePair AntiNodePair { get; }

        public AntennaPair(Antenna pair1, Antenna pair2)
        {
            Pair = (pair1, pair2);
            AntiNodePair = new AntiNodePair(pair1, pair2);
        }
    }


    class AntiNodePair
    {
        public List<AntiNode> NodePair { get; }
        public AntiNodePair(Antenna pair1, Antenna pair2)
        {
            int xDiff = Math.Abs(pair1.Position.X - pair2.Position.X);
            int yDiff = Math.Abs(pair1.Position.Y - pair2.Position.Y);
            int xNode1;
            int yNode1;
            int xNode2;
            int yNode2;

            if (pair1.Position.X < pair2.Position.X)
            {
                xNode1 = pair1.Position.X - xDiff;
                xNode2 = pair2.Position.X + xDiff;
            }
            else
            {
                xNode1 = pair1.Position.X + xDiff;
                xNode2 = pair2.Position.X - xDiff;
            }

            if (pair1.Position.Y < pair2.Position.Y)
            {
                yNode1 = pair1.Position.Y - yDiff;
                yNode2 = pair2.Position.Y + yDiff;
            }
            else
            {
                yNode1 = pair1.Position.Y + yDiff;
                yNode2 = pair2.Position.Y - yDiff;
            }

            NodePair = new List<AntiNode>();
            NodePair.Add(new AntiNode(new Point(xNode1, yNode1)));
            NodePair.Add(new AntiNode(new Point(xNode2, yNode2)));
        }
    }
}
