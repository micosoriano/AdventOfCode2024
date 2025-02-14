using AdventOfCode.Helpers;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputManager = new InputManager();
			Day3 day3 = new Day3(inputManager.Day3);
			day3.Task1();
		}
	}
}