namespace AdventOfCode
{
	using System.Text.RegularExpressions;
	using AdventOfCode.Helpers;

	internal class Day4 : Day, IDay
	{
		string xPattern = @"X";
		string mPattern = @"M";
		string aPattern = @"A";
		string sPattern = @"S";
		List<XMAS> xList = new List<XMAS>();
		List<XMAS> mList = new List<XMAS>();
		List<XMAS> aList = new List<XMAS>();
		List<XMAS> sList = new List<XMAS>();

		public Day4(string input) : base(input)
		{
			Console.WriteLine("Advent of Code Day 4");
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

		}

		public void Task1()
		{
			int xmasCount = 0;
			foreach (var x in xList)
			{
				for (int i = 0; i < 8; i++)
				{
					XMAS next = x.FindNext(mList, (Direction)i);

					if (next != null)
					{
						next = next.FindNext(aList, (Direction)i);
					}

					if (next != null)
					{
						next = next.FindNext(sList, (Direction)i);
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
			int xmasCount = 0;

			foreach (var a in aList)
			{
				var xmas = 0;

				// s on the north east
				var NES = a.FindNext(sList, Direction.NE);
				XMAS SWM = null;

				if (NES != null)
				{
					SWM = a.FindNext(mList, Direction.SW);
				}

				if (SWM != null)
				{
					xmas++;
				}
				
				// m on the north east
				var NEM = a.FindNext(mList, Direction.NE);
				XMAS SWS = null;

				if (NEM != null)
				{
					SWS = a.FindNext(sList, Direction.SW);
				}

				if (SWS != null)
				{
					xmas++;
				}

				// s on the south east
				var SES = a.FindNext(sList, Direction.SE);
				XMAS NWM = null;

				if (SES != null)
				{
					NWM = a.FindNext(mList, Direction.NW);
				}

				if (NWM != null)
				{
					xmas++;
				}

				// m on the south east
				var SEM = a.FindNext(mList, Direction.SE);
				XMAS NWS = null;

				if (SEM != null)
				{
					NWS = a.FindNext(sList, Direction.NW);
				}

				if (NWS != null)
				{
					xmas++;
				}

				if (xmas == 2) xmasCount++;
			}

			Console.WriteLine("XMAS Count: " + xmasCount);
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

			public XMAS FindNext(List<XMAS> list, Direction dir)
			{
				switch (dir)
				{
					case Direction.N:
						return FindNorth(list);
					case Direction.S:
						return FindSouth(list);
					case Direction.E:
						return FindEast(list);
					case Direction.W:
						return FindWest(list);
					case Direction.NE:
						return FindNorthEast(list);
					case Direction.NW:
						return FindNorthWest(list);
					case Direction.SE:
						return FindSouthEast(list);
					case Direction.SW:
						return FindSouthWest(list);
				}

				return null!;
			}

			public XMAS FindNorth(List<XMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc && x.YLoc == this.YLoc - 1)!;
			}

			public XMAS FindSouth(List<XMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc && x.YLoc == this.YLoc + 1)!;
			}

			public XMAS FindWest(List<XMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc - 1 && x.YLoc == this.YLoc)!;
			}

			public XMAS FindEast(List<XMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc)!;
			}

			public XMAS FindNorthEast(List<XMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc - 1)!;
			}

			public XMAS FindNorthWest(List<XMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc - 1 && x.YLoc == this.YLoc - 1)!;
			}

			public XMAS FindSouthEast(List<XMAS> list)
			{
				return list.Find(x => x.XLoc == this.XLoc + 1 && x.YLoc == this.YLoc + 1)!;
			}

			public XMAS FindSouthWest(List<XMAS> list)
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
			public M(int xLoc, int yLoc) : base(xLoc, yLoc) { }
		}

		class A : XMAS
		{
			public A(int xLoc, int yLoc) : base(xLoc, yLoc) { }
		}
		class S : XMAS
		{
			public S(int xLoc, int yLoc) : base(xLoc, yLoc) { }
		}
	}
}
