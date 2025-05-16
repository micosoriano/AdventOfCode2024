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
                    if (line.Contains("6869"))
                    {
                        int x = 0;
                    }
                    if (line.Contains("A"))
                    {
                        machine.ButtonA = new Button(line);
                    }
                    else if (line.Contains("B"))
                    {
                        machine.ButtonB = new Button(line);
                    }
                    else if (line.Contains("Prize"))
                    {
                        var coor = Parser.ParseInput(line);
                        machine.Prize = new Point(coor[0], coor[1]);
                    }
                }
                while (line != "");
                {

                }
                machines.Add(machine);
            }
            while (!reader.EndOfStream);
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
    }

    class Button
    {
        public int YIncrement { get; }
        public int XIncrement { get; }

        public Button(string settings)
        {
            var input =  Parser.ParseInput(settings);
            XIncrement = input[0];
            YIncrement = input[1];
        }
    }
}
