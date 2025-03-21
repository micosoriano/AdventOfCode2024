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
			Day7 day7 = new Day7(inputManager.Day7);
			//day7.Task1();
			stopwatch.Stop();
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }
	}
}