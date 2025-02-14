namespace AdventOfCode
{
	using System.Text.RegularExpressions;
	using AdventOfCode.Helpers;

	internal class Day3 : Day, IDay
	{
		public Day3(string input) : base(input)
		{
			Console.WriteLine("Advent of Code Day 2");
		}

		public void Task1()
		{
			int mulResult = 0;
			List<MulOperation> mulOperations = new List<MulOperation>();
			bool isDo = true;
			while (!this.reader.EndOfStream)
			{
				List<DoOperation> doOperations = new List<DoOperation>();
				List<DontOperation> dontOperations = new List<DontOperation>();
				string pattern = @"mul\(\d+,\d+\)";
				string doPattern = "do()";
				string dontPattern = "don't()";
				string line = reader.ReadLine()!;
				var matches = Regex.Matches(line, pattern);
				var doMatches = Regex.Matches(line, doPattern);
				var dontMatches = Regex.Matches(line, dontPattern);
				foreach (Match match in matches)
				{
					Console.WriteLine(match.Value);
					var digits = Regex.Matches(match.Value, @"\d+");
					mulOperations.Add(new MulOperation(int.Parse(digits[0].Value), int.Parse(digits[1].Value), match.Index, isDo));
				}

				foreach (Match match in doMatches)
				{
					Console.WriteLine(match.Value);
					Console.WriteLine(match.Index);
					doOperations.Add(new DoOperation(match.Index));
				}

				foreach (Match match in dontMatches)
				{
					Console.WriteLine(match.Value);
					Console.WriteLine(match.Index);
					dontOperations.Add(new DontOperation(match.Index));
				}
				
				// check what was the last one on the line. do or dont
				isDo = doOperations.Last().Position > dontOperations.Last().Position;
			}
		}

		public void Task2()
		{

		}

		class MulOperation : IOperation
		{
			public int Operand1 { get; set; }
			public int Operand2 { get; set; }
			public int Position { get; set; }
			public bool IsEnabled { get; set; }

			public MulOperation(int operand1, int operand2, int position, bool isDo)
			{
				Operand1 = operand1;
				Operand2 = operand2;
				Position = position;
				IsEnabled = isDo;
			}
		}

		class DoOperation : IOperation
		{
			public int Position { get; set; }

			public DoOperation(int position)
			{
				Position = position;
			}
		}

		class DontOperation : IOperation
		{
			public int Position { get; set; }

			public DontOperation(int position)
			{
				Position = position;
			}
		}

		interface IOperation
		{
			int Position { get; set; }
		}
}
