namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Helpers;

    internal class Day7 : Day
    {
        List<List<BigInteger>> operations;

        public Day7(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 7");
            this.operations = new List<List<BigInteger>>();

            while (!this.reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                var op = new List<BigInteger>();

                Console.WriteLine(line);
                var split = line.Split(":");
                op.Add(BigInteger.Parse(split[0]));
                var inputs = Array.ConvertAll(split[1].Trim().Split(" "), BigInteger.Parse);
                op.AddRange(inputs);
                this.operations.Add(op);
            }

            foreach (var op in this.operations)
            {
            }
        }
    }
}
