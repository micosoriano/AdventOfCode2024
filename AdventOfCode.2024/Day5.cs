namespace AdventOfCode
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using AdventOfCode.Helpers;

	internal class Day5 : Day, IDay
	{
		public Day5(string input) : base(input)
		{
			Console.WriteLine("Advent of Code Day 5");

			List<Page> pages = new List<Page>();
			do
			{
				var line = reader.ReadLine()!;
				if (line == "") break;
				pages.Add(new Page { Value = int.Parse(line.Split("|")[0]) });
				pages.Add(new Page { Value = int.Parse(line.Split("|")[1]) });
				Console.WriteLine(line);
			}
			while (true);
		}

		public void Task1()
		{
			throw new NotImplementedException();
		}

		public void Task2()
		{
			throw new NotImplementedException();
		}
	}

	class Page
	{
		public int Value { get; set; }
		public int Rank { get; set; }
	}
}
