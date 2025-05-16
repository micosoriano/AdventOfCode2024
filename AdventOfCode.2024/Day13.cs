namespace AdventOfCode
{
    using System.Drawing;
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
                        machine.Prize = new Point(coor[0], coor[1]);
                    }
                }
                while (line != "");
                machines.Add(machine);
            }
            while (!reader.EndOfStream);
        }

        public void Task1()
        {
            var tokenCount = machines.Select(x => x.GetTokenCount()).Sum();
            Console.WriteLine(tokenCount);
        }
    }

    static class Parser
    {
        public static List<int> ParseInput(string input)
        {
            string digitPattern = @"\d+";
            return Regex.Matches(input, digitPattern).Select(x => int.Parse(x.Value)).ToList();
        }
    }

    class ClawMachine
    {
        public Button? ButtonA { get; set; }
        public Button? ButtonB { get; set; }
        public Point? Prize { get; set; }

        public ClawMachine()
        {
            
        }

        public int GetTokenCount()
        {
            int btnAPress = ((Prize.Value.X * ButtonB.YIncrement) - (Prize.Value.Y * ButtonB.XIncrement)) / ((ButtonA.XIncrement * ButtonB.YIncrement) - (ButtonA.YIncrement * ButtonB.XIncrement));
            int btnBPress = (Prize.Value.Y - (btnAPress * ButtonA.YIncrement)) / (ButtonB.YIncrement);

            if (btnAPress > 100 || btnBPress > 100 || btnAPress < 0 || btnBPress < 0)
            {
                return 0;
            }

            return btnAPress*ButtonA.TokenPrice + btnBPress*ButtonB.TokenPrice;
        }
    }

    class Button
    {
        public int YIncrement { get; }
        public int XIncrement { get; }
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
