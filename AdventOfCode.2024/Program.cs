using AdventOfCode.Helpers;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputManager = new InputManager();

			Day1 day1 = new Day1(inputManager.Day1);
			day1.Task1And2();
		}
	}
}