namespace AdventOfCode.Helpers
{
	public class InputManager
	{
		public string InputPath { get; }
		public string Day1 { get => InputPath + "/day1.txt"; }
		public string Day2 { get => InputPath + "/day2.txt"; }
		public string Day3 { get => InputPath + "/day3.txt"; }
		public string Day4 { get => InputPath + "/day4.txt"; }
		public string Day5 { get => InputPath + "/day5.txt"; }
		public string Day6 { get => InputPath + "/day6.txt"; }
		public string Day7 { get => InputPath + "/day7.txt"; }
		public string Day8 { get => InputPath + "/day8.txt"; }
		public string Day9 { get => InputPath + "/day9.txt"; }
		public string Day10 { get => InputPath + "/day10.txt"; }
		public InputManager()
		{
			Console.WriteLine("Initializing Input Manager");

			string csprojPath = Directory.GetParent(Environment.CurrentDirectory).Parent!.FullName;
			this.InputPath = Path.Combine(Path.GetDirectoryName(csprojPath)!, "inputs");
			
			Console.WriteLine("Input Path: " + this.InputPath);
			Console.WriteLine("Input Manager Initialized");
		}
	}
}
