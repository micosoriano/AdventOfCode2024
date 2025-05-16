namespace AdventOfCode
{
    using System.Drawing;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using AdventOfCode.Helpers;

    internal class Day13 : Day
    {
        List<ClawMachine> machines;
        public Day13(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 13");

            machines = new List<ClawMachine>();
            do
            {
                string line = string.Empty;
                ClawMachine machine = new ClawMachine();
                do
                {
                    line = reader.ReadLine()!;
                    if (line == null) break;
                    if (line.Contains('A'))
                    {
                        machine.ButtonA = new Button(line, 3);
                    }
                    else if (line.Contains('B'))
                    {
                        machine.ButtonB = new Button(line, 1);
                    }
                    else if (line.Contains("Prize"))
                    {
                        var coor = Parser.ParseInput(line);
                        //machine.Prize = new Prize(coor[0] + 10000000000000, coor[1] + 10000000000000);
                        machine.Prize = new Prize(coor[0], coor[1]);
                    }
                }
                while (line != "");
                machines.Add(machine);
            }
            while (!reader.EndOfStream);
        }

        public void Task1()
        {
            long tokenCount = 0;
            foreach (var machine in machines)
            {
                tokenCount += machine.GetTokenCount();
                Console.WriteLine("Current Total token " +tokenCount);
            }
            Console.WriteLine($"Total token count: {tokenCount}");
        }
    }

    static class Parser
    {
        public static List<long> ParseInput(string input)
        {
            string digitPattern = @"\d+";
            return Regex.Matches(input, digitPattern).Select(x => long.Parse(x.Value)).ToList();
        }
    }

    class Prize
    {
        public long X { get; }
        public long Y { get; }
        public Prize(long x, long y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    class ClawMachine
    {
        public Button? ButtonA { get; set; }
        public Button? ButtonB { get; set; }
        public Prize? Prize { get; set; }

        public ClawMachine()
        {
            
        }

        public long GetTokenCount()
        {
            double btnAPress = ((Prize.X * ButtonB.YIncrement) - (Prize.Y * ButtonB.XIncrement)) / ((ButtonA.XIncrement * ButtonB.YIncrement) - (ButtonA.YIncrement * ButtonB.XIncrement));
            double btnBPress = (Prize.Y - (btnAPress * ButtonA.YIncrement)) / (ButtonB.YIncrement);

            Console.WriteLine($"");
            Console.WriteLine($"ButtonA: {btnAPress}");
            Console.WriteLine($"ButtonB: {btnBPress}");
            long tokenCount = 0;

            if (btnAPress > 100 || btnBPress > 100 || btnAPress < 0 || btnBPress < 0 || btnAPress % 1 != 0 || btnBPress % 1 != 0)
            {
                tokenCount = 0;
            }
            else
            {
                tokenCount = ((long)btnAPress * ButtonA.TokenPrice) + ((long)btnBPress * ButtonB.TokenPrice);
            }

            Console.WriteLine($"TokenCount: {tokenCount}");
            return tokenCount;
        }
    }

    class Button
    {
        public long YIncrement { get; }
        public long XIncrement { get; }
        public int TokenPrice { get; }

        public Button(string settings, int tokenPrice)
        {
            var input =  Parser.ParseInput(settings);
            this.XIncrement = input[0];
            this.YIncrement = input[1];
            this.TokenPrice = tokenPrice;
        }
    }
}
