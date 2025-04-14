namespace AdventOfCode
{
    using System.Diagnostics;
    using AdventOfCode.Helpers;

    internal class Day11 : Day
    {
        List<double> stones;
        Dictionary<double, List<double>> knownStones;
        public Day11(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 11");
            stones = new List<double>();
            knownStones = new Dictionary<double, List<double>>();
            stones.AddRange(Array.ConvertAll(reader.ReadLine()!.Split(" "), double.Parse));

        }

        public void Task1()
        {
            Blink(75);
            //Console.WriteLine("Stones: " + string.Join(", ", stones));
            Console.WriteLine("Count: " + stones.Count);
        }

        private void Blink(int blinks)
        {
            Stopwatch stopwatch = new Stopwatch();
            int curBlink = 1;
            List<double> tempStones = new List<double>();
            while (curBlink <= blinks)
            {
                stopwatch.Start();
                tempStones.Clear();
                var dict = stones.GroupBy(x => x).ToList().ToDictionary(y => y.Key, y => y.Count());
                Parallel.ForEach(dict, val =>
                {
                    List<double> tempStone = new List<double>();
                    bool isKnown = false;
                    lock (knownStones)
                    {
                        if (knownStones.ContainsKey(val.Key))
                        {
                            isKnown = true;
                        }
                    }
                    if (isKnown)
                    {
                        tempStone = knownStones[val.Key];
                    }
                    else
                    {
                        var valStr = val.Key.ToString();
                        if (valStr == "0") tempStone.Add(1);
                        else if (valStr.Length % 2 == 0)
                        {
                            var list = valStr.ToCharArray().ToList();
                            var firstHalf = list.Take(list.Count / 2).ToList();
                            var secondHalf = list.Skip(list.Count / 2).ToList();
                            tempStone.Add(double.Parse(firstHalf.ToArray()));
                            tempStone.Add(double.Parse(secondHalf.ToArray()));
                        }
                        else
                        {
                            var doubleVal = double.Parse(valStr);
                            tempStone.Add((doubleVal * 2024));
                        }
                        lock (knownStones)
                        {
                            knownStones.TryAdd(val.Key, tempStone);
                        }
                    }
                    lock (tempStones)
                    {
                        List<double> list = new List<double>();
                        for (int i = 0; i < val.Value; i++)
                        {
                            list.AddRange(tempStone);
                        }
                        tempStones.AddRange(list);
                    }
                });
                stones = new List<double>(tempStones);
                stopwatch.Stop();
                //Console.WriteLine($"Current Stones at blink {curBlink}: " + string.Join(", ", tempStones));
                Console.WriteLine("Current blink " + curBlink);
                Console.WriteLine("Time elapsed for blink: {0}", stopwatch.Elapsed);
                curBlink++;
            }
        }
    }
}
