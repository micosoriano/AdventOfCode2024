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
			Day10 day = new Day10(inputManager.Day10);
			//day9.Task2();
			stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }
	}
}