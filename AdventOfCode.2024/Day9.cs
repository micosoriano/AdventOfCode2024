namespace AdventOfCode
{
    using System.Text.RegularExpressions;
    using AdventOfCode.Helpers;

    internal class Day9 : Day
    {
        List<int> rawData;
        List<string> fileSystem;
        public Day9(string input) : base(input)
        {
            rawData = new List<int>();
            fileSystem = new List<string>();
            Console.WriteLine("Advent of Code Day 9");
            var line = reader.ReadLine()!;
            int y = 0;
            foreach (var file in line)
            {
                string fileString = file.ToString();
                for (int i = 0; i < int.Parse(fileString); i++)
                {
                    if (y % 2 == 0)
                    {
                        fileSystem.Add((y/2).ToString());
                    }
                    else
                    {
                        fileSystem.Add(".");
                    }
                }
                y++;
            }
        }

        public void Task1()
        {
            for (int i = fileSystem.Count - 1; i > 0; i--)
            {
                var idxSpace = fileSystem.IndexOf(".");
                var checker = fileSystem.Skip(idxSpace);
                if (!checker.Any(s => int.TryParse(s, out _))) break;
                
                if (fileSystem[i] != ".")
                {
                    fileSystem[idxSpace] = fileSystem[i];
                    fileSystem[i] = ".";
                }
            }

            double sum = 0;
            for (int i = 0; i < fileSystem.Count; i++)
            {
                if (int.TryParse(fileSystem[i], out var val))
                {
                    sum += val*i;
                }
            }

            Console.WriteLine("Sum: " + sum);
        }
    }
}
