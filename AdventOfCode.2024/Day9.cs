namespace AdventOfCode
{
    using System.Text.RegularExpressions;
    using AdventOfCode.Helpers;

    internal class Day9 : Day
    {
        List<int> fileSystem;
        public Day9(string input) : base(input)
        {
            fileSystem = new List<int>();
            Console.WriteLine("Advent of Code Day 9");
            foreach(var file in reader.ReadLine()!)
            {
                fileSystem.Add(int.Parse(file.ToString()));
            }
        }
    }
}
