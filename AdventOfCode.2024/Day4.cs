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
				string line = reader.ReadLine()!;
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

			int xmasCount = 0;
			foreach (var x in xList)
			{
				for (int i = 0; i < 8; i++)
				{
					IXMAS next = x.FindNext(mList.Cast<IXMAS>().ToList(), (Direction)i);

					if (next != null)
					{
						next = next.FindNext(aList.Cast<IXMAS>().ToList(), (Direction)i);
					}

					if (next != null)
					{
						next = next.FindNext(sList.Cast<IXMAS>().ToList(), (Direction)i);
					}

					if (next != null)
					{
						xmasCount++;
					}
				}
			}

			Console.WriteLine("XMAS Count: " + xmasCount);
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

		interface IXMAS
		{
			public int XLoc { get; set; }
			public int YLoc { get; set; }
			IXMAS FindNext(List<IXMAS> list, Direction dir);
		}

		class XMAS : IXMAS
		{
			public int XLoc { get; set; }
			public int YLoc { get; set; }
			public bool Connected { get; set; }

			public XMAS(int xLoc, int yLoc)
			{
				this.XLoc = xLoc;
				this.YLoc = yLoc;
			}

			public IXMAS FindNext(List<IXMAS> list, Direction dir)
			{
				switch (dir)
				{
					case Direction.N:
						return FindUp(list);
					case Direction.S:
						return FindDown(list);
					case Direction.E:
						return FindRight(list);
					case Direction.W:
						return FindLeft(list);
					case Direction.NE:
						return FindNE(list);
					case Direction.NW:
						return FindNW(list);
					case Direction.SE:
						return FindSE(list);
					case Direction.SW:
						return FindSW(list);
				}

				return null!;
			}

			public IXMAS FindUp(List<IXMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc && x.YLoc == this.YLoc - 1)!;
			}

			public IXMAS FindDown(List<IXMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc && x.YLoc == this.YLoc + 1)!;
			}

			public IXMAS FindLeft(List<IXMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc - 1 && x.YLoc == this.YLoc)!;
			}

			public IXMAS FindRight(List<IXMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc)!;
			}

			public IXMAS FindNE(List<IXMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc - 1)!;
			}

			public IXMAS FindNW(List<IXMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc - 1 && x.YLoc == this.YLoc - 1)!;
			}

			public IXMAS FindSE(List<IXMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc + 1)!;
			}

			public IXMAS FindSW(List<IXMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc - 1 && x.YLoc == this.YLoc + 1)!;
			}
		}

		class X : XMAS
		{
			public X(int xLoc, int yLoc) : base(xLoc, yLoc) { }
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
