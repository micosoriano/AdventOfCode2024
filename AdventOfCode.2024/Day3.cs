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
			List<string> operations = new List<string>();
			int mulResult = 0;
			while (!this.reader.EndOfStream)
			{
				string pattern = @"mul\(\d+,\d+\)";
				string line = reader.ReadLine()!;
				var matches = Regex.Matches(line, pattern);
				foreach (Match match in matches)
				{
					operations.Add(match.Value);
					Console.WriteLine(match.Value);
					var digits = Regex.Matches(match.Value, @"\d+");
					mulResult = mulResult + (int.Parse(digits[0].Value) * int.Parse(digits[1].Value));
					Console.WriteLine("Result: " + mulResult);
				}
			}
		}

		public void Task2()
		{

		}
	}
}
