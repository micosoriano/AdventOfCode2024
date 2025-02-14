namespace AdventOfCode
{
	using System.Text.RegularExpressions;
	using AdventOfCode.Helpers;

	internal class Day3 : Day, IDay
	{
		public Day3(string input) : base(input)
		{
			Console.WriteLine("Advent of Code Day 3");
		}

		public void Task1()
		{
			int mulResult = 0;
			List<MulOperation> mulOperations = new List<MulOperation>();
			bool doOperation = true;
			while (!this.reader.EndOfStream)
			{
				List<IOperation> operations = new List<IOperation>();
				string pattern = @"mul\(\d+,\d+\)";
				string doPattern = @"do\(\)";
				string dontPattern = @"don't\(\)";
				string line = reader.ReadLine()!;
				var matches = Regex.Matches(line, pattern);
				var doMatches = Regex.Matches(line, doPattern);
				var dontMatches = Regex.Matches(line, dontPattern);
				foreach (Match match in matches)
				{
					var digits = Regex.Matches(match.Value, @"\d+");
					operations.Add(new MulOperation(int.Parse(digits[0].Value), int.Parse(digits[1].Value), match.Index));
				}

				foreach (Match match in doMatches)
				{
					operations.Add(new DoOperation(match.Index));
				}

				foreach (Match match in dontMatches)
				{
					operations.Add(new DontOperation(match.Index));
				}

				operations.Sort((x, y) => x.Position.CompareTo(y.Position));

				foreach (var operation in operations)
				{ 
					Console.WriteLine(operation.GetType().Name);
					Console.WriteLine(operation.Position);

					if (operation is DoOperation) doOperation = true;
					else if (operation is DontOperation) doOperation = false;
					
					if (operation is MulOperation mulOperation)
					{
						operation.IsEnabled = doOperation;

						if (operation.IsEnabled)
						{
							Console.WriteLine("LETS MULTIPLY!");
							mulResult = mulResult + ( mulOperation.Operand1 * mulOperation.Operand2);
							Console.WriteLine($"mulResult: {mulResult}");
						}
					}
				}
			}
			
			Console.WriteLine($"Mul Result: {mulResult}");
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

			public MulOperation(int operand1, int operand2, int position)
			{
				Operand1 = operand1;
				Operand2 = operand2;
				Position = position;
			}
		}

		class DoOperation : IOperation
		{
			public int Position { get; set; }
			public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

			public DoOperation(int position)
			{
				Position = position;
			}
		}

		class DontOperation : IOperation
		{
			public int Position { get; set; }
			public bool IsEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

			public DontOperation(int position)
			{
				Position = position;
			}
		}

		interface IOperation
		{
			int Position { get; set; }

			bool IsEnabled { get; set; }
		}
	}
}
