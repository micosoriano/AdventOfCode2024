using System.Diagnostics;
using AdventOfCode.Helpers;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var inputManager = new InputManager();
			Day11 day = new Day11(inputManager.Day11);
			day.Task2();
			stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }
	}
}