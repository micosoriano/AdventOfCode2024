namespace AdventOfCode
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using System.Text;
	using System.Threading.Tasks;
	using AdventOfCode.Helpers;

	internal class Day5 : Day, IDay
	{
		public Day5(string input) : base(input)
		{
			Console.WriteLine("Advent of Code Day 5");

			PageHandler pageHandler = new PageHandler();
			int sum = 0;
			int sum2 = 0;
			do {
				var line = reader.ReadLine()!;
				if (line == "") break;
				var split = Array.ConvertAll(line.Split("|"), int.Parse);

				pageHandler.AddPages(new Page(split[0]), new Page(split[1]));
			}
			while (true);

			while (!this.reader.EndOfStream) {
				string line = reader.ReadLine()!;
				var split = Array.ConvertAll(line.Split(","), int.Parse).ToList();
				if (pageHandler.IsInOrder(split)) sum += split[split.Count / 2];
				else
				{
					split = pageHandler.ArrangePages(split);
					sum2 += split[split.Count / 2];
				}
			}

			Console.WriteLine(sum);
			Console.WriteLine(sum2);
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
		public List<int> OnRightOf { get; set; }
		public List<int> OnLeftOf { get; set; }
		public int Value { get; }
		public Page(int val)
		{
			this.Value = val;
			OnRightOf = new List<int>();
			OnLeftOf = new List<int>();
		}
	}

	class PageHandler
	{
		List<Page> pages;
		public List<Page> Pages { get => pages; set => pages = value; }

		public PageHandler()
		{
			pages = new List<Page>();
		}

		public void AddPages(Page onLeft, Page onRight)
		{
			var left = pages.FirstOrDefault(p => p.Value == onLeft.Value);
			var right = pages.FirstOrDefault(p => p.Value == onRight.Value);
			if (left == null)
			{
				left = onLeft;
				pages.Add(onLeft);
			}
			if (right == null)
			{
				right = onRight;
				pages.Add(right);
			}
			left.OnLeftOf.Add(right.Value);
			right.OnRightOf.Add(left.Value);
		}

		public bool IsInOrder(List<int> lstPage)
		{
			bool isInOrder = false;
			for (int i = 0; i < (lstPage.Count); i++)
			{
				var onLeft = pages.FirstOrDefault(p => p.Value == lstPage[i]);

				for (int j = i + 1; j < (lstPage.Count); j++)
				{
					var onRight = pages.FirstOrDefault(p => p.Value == lstPage[j]);
					if (!onLeft!.OnLeftOf.Contains(onRight!.Value) ||
						!onRight!.OnRightOf.Contains(onLeft!.Value))
					{
						isInOrder = false;
						break;
					}
					else isInOrder = true;
				}
				if (!isInOrder) break;
			}
			return isInOrder;
		}

		public List<int> ArrangePages(List<int> lstPage)
		{
			List<int> arranged = lstPage;
			for (int i = 0; i < lstPage.Count; i++)
			{
				var onLeft = pages.FirstOrDefault(p => p.Value == lstPage[i]);

				for (int j = i + 1; j < lstPage.Count; j++)
				{
					var onRight = pages.FirstOrDefault(p => p.Value == lstPage[j]);

					if (!onLeft!.OnLeftOf.Contains(onRight!.Value))
					{
						arranged[j - 1] = onRight.Value;
						arranged[j] = onLeft.Value;
					}
					else break;
				}
			}

			foreach (var item in arranged)
			{
				Console.Write(item + " ");
			}
			Console.WriteLine();
			return arranged;
		}
	}
}
