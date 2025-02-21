namespace AdventOfCode
{
	using System.Drawing;
	using System.Text.RegularExpressions;
	using AdventOfCode.Helpers;

	internal class Day4 : Day, IDay
	{
		public Day4(string input) : base(input)
		{
			Console.WriteLine("Advent of Code Day 4");
		}

		public void Task1()
		{
			string xPattern = @"X";
			string mPattern = @"M";
			string aPattern = @"A";
			string sPattern = @"S";
			List<X> xList = new List<X>();
			List<M> mList = new List<M>();
			List<A> aList = new List<A>();
			List<S> sList = new List<S>();

			int y = 0;
			while (!this.reader.EndOfStream)
			{
				string line = reader.ReadLine();
				var xMatches = Regex.Matches(line, xPattern);
				var mMatches = Regex.Matches(line, mPattern);
				var aMatches = Regex.Matches(line, aPattern);
				var sMatches = Regex.Matches(line, sPattern);

				foreach (Match match in xMatches)
				{
					X x = new X(match.Index, y);
					xList.Add(x);
				}

				foreach (Match match in mMatches)
				{
					M m = new M(match.Index, y);
					mList.Add(m);
				}

				foreach (Match match in aMatches)
				{
					A a = new A(match.Index, y);
					aList.Add(a);
				}

				foreach (Match match in sMatches)
				{
					S s = new S(match.Index, y);
					sList.Add(s);
				}

				y++;
			}

			xList.OrderBy(x => x.YLoc).ThenBy(x => x.XLoc);
			mList.OrderBy(x => x.YLoc).ThenBy(x => x.XLoc);
			aList.OrderBy(x => x.YLoc).ThenBy(x => x.XLoc);
			sList.OrderBy(x => x.YLoc).ThenBy(x => x.XLoc);

			foreach (var x in xList)
			{
				foreach (var direction in Enum.GetValues(typeof(Direction)))
				{

				}
			}
		}

		public void Task2()
		{
			throw new NotImplementedException();
		}

		enum Direction
		{
			N,
			S,
			E,
			W,
			NE,
			NW,
			SE,
			SW
		}

		class XMAS
		{
			public int XLoc { get; set; }
			public int YLoc { get; set; }
			public bool Connected { get; set; }

			public XMAS(int xLoc, int yLoc)
			{
				this.XLoc = xLoc;
				this.YLoc = yLoc;
			}

			public void FindNext(List<XMAS> list, Direction dir)
			{

			}

			public void Connect(List<XMAS> list, Direction direction)
			{
				list.Find(x => x.XLoc == this.XLoc && x.YLoc == this.YLoc - 1 && x.GetType() == typeof(M));
			}

			public void FindUp(List<XMAS> list)
			{
				list.Find(x => x.XLoc == this.XLoc && x.YLoc == this.YLoc - 1);
			}

			public void FindDown(List<XMAS> list)
			{
				list.Find(x => x.XLoc == this.XLoc && x.YLoc == this.YLoc + 1);
			}

			public void FindLeft(List<XMAS> list)
			{
				list.Find(x => x.XLoc == this.XLoc - 1 && x.YLoc == this.YLoc);
			}

			public void FindRight(List<XMAS> list)
			{
				list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc);
			}

			public void FindNE(List<XMAS> list)
			{
				list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc - 1);
			}

			public void FindNW(List<XMAS> list)
			{
				list.Find(x => x.XLoc == this.XLoc - 1 && x.YLoc == this.YLoc - 1);
			}

			public void FindSE(List<XMAS> list)
			{
				list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc + 1);
			}

			public void FindSW(List<XMAS> list)
			{
				list.Find(x => x.XLoc == this.XLoc - 1 && x.YLoc == this.YLoc + 1);
			}
		}

		class X : XMAS
		{
			public X(int xLoc, int yLoc) : base(xLoc, yLoc) { }

			public void FindM(List<XMAS> list)
			{
				var mList = list.FindAll(x => x.GetType() == typeof(M));

				foreach (var m in mList)
				{

				}
			}

		}

		class M : XMAS
		{
			public M(int xLoc, int yLoc) : base(xLoc, yLoc)
			{

			}
		}

		class A : XMAS
		{
			public A(int xLoc, int yLoc) : base(xLoc, yLoc)
			{

			}
		}
		class S : XMAS
		{
			public S(int xLoc, int yLoc) : base(xLoc, yLoc)
			{

			}
		}
	}
}
