using AdventOfCode.Helpers;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputManager = new InputManager();
			Day5 day5 = new Day5(inputManager.Day5);
			//day5.Task2();
		}
	}
}