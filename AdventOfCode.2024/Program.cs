using AdventOfCode.Helpers;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputManager = new InputManager();
			Day2 day2 = new Day2(inputManager.Day2);
			day2.Task1();
		}
	}
}