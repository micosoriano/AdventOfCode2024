namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Helpers;

    internal class Day7 : Day
    {
        public Day7(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 7");
            while (!this.reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                Console.WriteLine(line);
            }
        }
    }
}
