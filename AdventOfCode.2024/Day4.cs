namespace AdventOfCode
{
	using AdventOfCode.Helpers;

	internal class Day4 : Day, IDay
	{
		public Day4(string input) : base(input)
		{
			Console.WriteLine("Advent of Code Day 4");
		}

		public void Task1()
		{
			while (!this.reader.EndOfStream)
			{
				string line = reader.ReadLine();
				Console.WriteLine(line);
			}
		}

		public void Task2()
		{
			throw new NotImplementedException();
		}
	}
}
