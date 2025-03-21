namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;
    using System.Transactions;
    using AdventOfCode.Helpers;

    internal class Day7 : Day
    {
        List<List<string>> operations;

        public Day7(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 7");
            this.operations = new List<List<string>>();

            while (!this.reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                var op = new List<string>();

                var split = line.Split(":");
                op.Add(split[0]);
                var inputs = split[1].Trim().Split(" ");
                op.AddRange(inputs);
                this.operations.Add(op);
            }
        }

        public void Task1()
        {
            BigInteger sum = 0;
            foreach (var op in this.operations)
            {
                var bruteForceTries = Math.Pow(2, op.Count - 2);
                Console.WriteLine("expected output: " + op[0]);
                for (var fullOp = new BigInteger(); (double)fullOp <= bruteForceTries; fullOp++)
                {
                    if (op[0] == DoOperation(op.GetRange(1, op.Count - 1), ConvertByteToOperation(fullOp, bruteForceTries)))
                    {
                        sum += BigInteger.Parse(op[0]);
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

        private string DoOperation(List<string> values, List<string> operation)
        {
            values = values.Select(x => x = x +".0").ToList();
            int insertIdx = 1;
            int valIdx = operation.Count - 1;

            while (insertIdx < values.Count && valIdx < operation.Count)
            {
                values.Insert(insertIdx, operation[valIdx]);
                insertIdx += 2;
                valIdx--;
            }

            var fullOP = string.Join("", values);
            Console.WriteLine("Full operation: " + fullOP);
            var table = new DataTable();
            var output = table.Compute(fullOP, "").ToString();
            Console.WriteLine("Current output: "  +output);

            return Convert.ToUInt64(table.Compute(fullOP, "")).ToString();
        }

        private List<string> ConvertByteToOperation(BigInteger operation, double size)
        {
            var op = new List<string>();
            var bitCount = Math.Log(size, 2);
            for (int i = 0; i < bitCount; i++)
            {
                if ((operation & (1 << i)) != 0)
                {
                    op.Add("*");
                }
                else
                {
                    op.Add("+");
                }
            }
            return op.ToList();
        }
    }
}
