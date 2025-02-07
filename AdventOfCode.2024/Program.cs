using System;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Advent of Code 2024 Day 1");

			string filename = "..\\..\\..\\inputs\\day1.txt";
			Day1 day1 = new Day1();
			day1.Task1And2(filename);
		}
	}
}