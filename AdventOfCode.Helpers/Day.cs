namespace AdventOfCode.Helpers
{
	using System;

	public class Day : IDisposable
	{
		protected string input;
		protected StreamReader reader;

		public Day(string input)
		{
			this.input = input;
			this.reader = new StreamReader(input);
		}

		public void Dispose()
		{
			this.reader.Dispose();
		}
	}
}
