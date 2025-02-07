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
				//Console.WriteLine(line);
				var intVal = Array.ConvertAll(line!.Split(" "), int.Parse).ToList();
				List<Level> levels = new List<Level>();
				foreach (var level in intVal)
				{
					levels.Add(new Level(level));
				}
				reports.Add(new Report(levels));
			}

			// reports
			foreach(var report in reports)
			{
				//levels
				int prevDiff = 0;
				int currentDiff = 0;

				if (report.Levels[0].Value == 38 && report.Levels[1].Value == 41 && report.Levels[2].Value == 42)
				{
					var val = 0;
				}
				for (int i = 0; i < (report.Levels.Count - 1) && report.IsOneWay && report.IsSafe; i++)
				{
					prevDiff = currentDiff;
					currentDiff = report.Levels[i].Value - report.Levels[i + 1].Value;
					if ((prevDiff < 0 && currentDiff > 0) || (prevDiff > 0 && currentDiff < 0) || (report.Levels[i].Value == report.Levels[i + 1].Value))
					{
						report.IsOneWay = false;
					}
					else
					{
						report.IsOneWay = true;
					}

					if (Math.Abs(currentDiff) <= 3 && report.IsOneWay)
					{
						report.IsSafe = true;
					}
					else
					{
						report.IsSafe = false;
					}
				}

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
