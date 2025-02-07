using System;

namespace AdventOfCode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Advent of Code 2024 Day 1");

			string filename = "..\\..\\..\\inputs\\day1.txt";
			Task1(filename);

			void Task1(string fileName)
			{
				using (StreamReader reader = new StreamReader(fileName))
				{
					int distance = 0;
					List<int> leftPair = new List<int>();
					List<int> rightPair = new List<int>();
					while (!reader.EndOfStream)
					{
						string line = reader.ReadLine();
						Console.WriteLine(line);
						var pair = line.Split("  ");
						var intPair = Array.ConvertAll(pair, int.Parse);
						leftPair.Add(intPair[0]);
						rightPair.Add(intPair[1]);
						Console.WriteLine("left: " + intPair[0] + " right: " + intPair[1]);
					}

					leftPair.Sort();
					rightPair.Sort();
					Console.WriteLine("left:");
					leftPair.ForEach(Console.WriteLine);
					Console.WriteLine("right:");

					rightPair.ForEach(Console.WriteLine);
					int initialDistance = 0;
					for (int i = 0; i < leftPair.Count; i++)
					{
						if (leftPair[i] > rightPair[i])
						{
							initialDistance = leftPair[i] - rightPair[i];
						}
						else
							initialDistance = rightPair[i] - leftPair[i];

						distance = distance + initialDistance;
					}

					Console.WriteLine("Distance: " + distance);

					int similarityScore = 0;
					for (int i = 0; i < leftPair.Count; i++)
					{
						int rightCounter = 0;
						for (int j = 0; j < rightPair.Count; j++)
						{
							if (leftPair[i] == rightPair[j])
							{
								rightCounter++;
							}
						}
						similarityScore = similarityScore + (leftPair[i] * rightCounter);
					}

					Console.WriteLine("Similarity Score: " + similarityScore);
				}
			}
		}
	}
}