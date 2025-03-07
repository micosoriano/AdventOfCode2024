﻿namespace AdventOfCode
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
			PageHandler pageHandler = new PageHandler();
			int sum = 0;
			do {
				var line = reader.ReadLine()!;
				if (line == "") break;
				var split = Array.ConvertAll(line.Split("|"), int.Parse);
				pageHandler.AddLeft(new Page(split[0]));
				pageHandler.AddRight(new Page(split[1]));
				Console.WriteLine(line);
			}
			while (true);

			while (!this.reader.EndOfStream) {
				string line = reader.ReadLine()!;
				var split = Array.ConvertAll(line.Split(","), int.Parse).ToList();
				if (pageHandler.IsInOrder(split)) sum += split[split.Count / 2];
			}

			Console.WriteLine(sum);
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
		public int Value { get; }
		public int Rank { get; set; }
		public Page(int val)
		{
			this.Value = val;
			this.Rank = 100;
		}
	}

	class PageHandler
	{
		List<Page> pages;
		public List<Page> Pages { get => pages.OrderBy(p => p.Rank).ToList(); }

		public PageHandler()
		{
			pages = new List<Page>();
		}

		public void AddLeft(Page page)
		{
			var left = pages.FirstOrDefault(p => p.Value == page.Value);
			if (left != null)
			{
				left.Rank += 1;
			}
			else
			{
				page.Rank += 1;
				pages.Add(page);
			}
		}

		public void AddRight(Page page)
		{
			var right = pages.FirstOrDefault(p => p.Value == page.Value);
			if (right != null)
			{
				right.Rank -= 1;
			}
			else
			{
				page.Rank -= 1;
				pages.Add(page);
			}
		}

		public bool IsInOrder(List<int> lstPage)
		{
			bool isInOrder = false;
			for (int i = 0; i < (lstPage.Count - 1); i++) {
				var msb = pages.FirstOrDefault(p => p.Value == lstPage[i]);
				var lsb = pages.FirstOrDefault(p => p.Value == lstPage[i + 1]);
				if (msb?.Rank <= lsb?.Rank) {
					Console.WriteLine("Not in order");
					isInOrder = false;
					break;
				}
				else isInOrder = true;
			}
			return isInOrder;
		}
	}
}
