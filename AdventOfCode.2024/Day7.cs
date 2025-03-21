namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;
    using System.Transactions;
    using AdventOfCode.Helpers;

    internal class Day7 : Day
    {
        List<List<double>> operations;

        public Day7(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 7");
            this.operations = new List<List<double>>();

            while (!this.reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                var op = new List<double>();

                var split = line.Split(":");
                op.Add(double.Parse(split[0]));
                var inputs = split[1].Trim().Split(" ");
                op.AddRange(Array.ConvertAll(inputs, double.Parse));
                this.operations.Add(op);
            }
        }

        public void Task1()
        {
            double sum = 0;
            foreach (var op in this.operations)
            {
                var bruteForceTries = Math.Pow(3, op.Count - 2);
                Console.WriteLine("expected output: " + op[0]);
                for (int i = 0; i <= bruteForceTries; i++)
                {
                    if (op[0] == DoOperation(op.GetRange(1, op.Count - 1), i, bruteForceTries))
                    {
                        sum += op[0];
                        Console.WriteLine(sum);
                        break;
                    }
                }
            }

            Console.WriteLine("Sum: " + sum);
        }

        public void Task2()
        {
            throw new NotImplementedException();
        }

        private double DoOperation(List<double> values, int operation, double size)
        {
            double total = values[0];
            var bitCount = Math.Log(size, 3);
            var val = 1;
            for (int i = (int)bitCount; i > 0; i--)
            {
                var tempOperation = operation;
                var state = 0;
                for (int j = 1; j < i; j++)
                {
                    tempOperation = tempOperation / 3;
                }
                state = tempOperation % 3;
                if (state == 0)
                {
                    total = total * values[val];
                }
                else if (state == 1)
                {
                    total = total + values[val];
                }
                else
                {
                    total = double.Parse($"{total}{values[val]}");
                }
                val++;
            }

            return total;
        }
    }
}
