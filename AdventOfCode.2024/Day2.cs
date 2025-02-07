namespace AdventOfCode
{
	using System.Reflection.Metadata.Ecma335;
	using AdventOfCode.Helpers;

	internal class Day2 : Day, IDay
	{
		public Day2(string input) : base(input)
		{
			Console.WriteLine("Advent of Code Day 2");
		}
		public void Task1()
		{
			List<Report> reports = new List<Report>(); 
			int safeReports = 0;
			while (!this.reader.EndOfStream)
			{
				string line = reader.ReadLine();
				var intVal = Array.ConvertAll(line!.Split(" "), int.Parse).ToList();
				List<Level> levels = new List<Level>();
				foreach (var level in intVal)
				{
					levels.Add(new Level(level));
				}
				reports.Add(new Report(levels));
			}

			foreach(var report in reports)
			{
				report.CheckIsSafe();

				if (report.IsSafe && report.IsOneWay)
				{
					safeReports += 1;
				}
			}

			Console.WriteLine("Safe Reports: " + safeReports);

		}
		public void Task2()
		{
		}
	}

	class Report
	{
		public List<Level> Levels { get; set; }
		public bool IsOneWay { get; set; }
		public bool IsSafe { get; set; }

		public Report(List<Level> levels)
		{
			this.Levels = levels;
			this.IsSafe = true;
			this.IsOneWay = true;
		}

		public void CheckIsSafe()
		{
			//levels
			int prevDiff = 0;
			int currentDiff = 0;

			if (this.Levels[0].Value == 38 && this.Levels[1].Value == 41 && this.Levels[2].Value == 42)
			{
				var val = 0;
			}
			for (int i = 0; i < (this.Levels.Count - 1) && this.IsOneWay && this.IsSafe; i++)
			{
				prevDiff = currentDiff;
				currentDiff = this.Levels[i].Value - this.Levels[i + 1].Value;
				if ((prevDiff < 0 && currentDiff > 0) || (prevDiff > 0 && currentDiff < 0) || (this.Levels[i].Value == this.Levels[i + 1].Value))
				{
					this.IsOneWay = false;
				}
				else
				{
					this.IsOneWay = true;
				}

				if (Math.Abs(currentDiff) <= 3 && this.IsOneWay)
				{
					this.IsSafe = true;
				}
				else
				{
					this.IsSafe = false;
				}
			}
		}
	}

	class Level
	{
		public int Value { get; }
		public Level(int value)
		{
			this.Value = value;
		}
	}
}
