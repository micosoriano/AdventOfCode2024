using AdventOfCode.Helpers;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputManager = new InputManager();
			Day6 day6 = new Day6(inputManager.Day6);
			day6.Task1();
		}
	}
}