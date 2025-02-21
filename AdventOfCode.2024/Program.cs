using AdventOfCode.Helpers;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var inputManager = new InputManager();
			Day4 day4 = new Day4(inputManager.Day4);
			day4.Task1();
		}
	}
}