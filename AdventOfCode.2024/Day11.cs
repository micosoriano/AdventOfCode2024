namespace AdventOfCode
{
    using System.Diagnostics;
    using AdventOfCode.Helpers;

    internal class Day11 : Day
    {
        Dictionary<double, double> currentStones;
        Dictionary<double, List<double>> knownStones;
        public Day11(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 11");
            currentStones = new Dictionary<double, double>();
            knownStones = new Dictionary<double, List<double>>();
            var line = Array.ConvertAll(reader.ReadLine()!.Split(" "), double.Parse);
            currentStones = line.GroupBy(x => x).ToList().ToDictionary(y => y.Key, y => (double)y.Count());
        }

        public void Task1()
        {
            Blink(75);
            double count = 0;
            foreach (var stone in currentStones)
            {
                count += stone.Value;
            }
            Console.WriteLine(count);
        }

        private void Blink(int blinks)
        {
            Stopwatch stopwatch = new Stopwatch();
            int curBlink = 1;
            Dictionary<double, double> tempStones = new Dictionary<double, double>();
            while (curBlink <= blinks)
            {
                stopwatch.Start();
                tempStones.Clear();
                var dict = currentStones;
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
                        foreach (var stone in tempStone)
                        {
                            if (!tempStones.TryAdd(stone, val.Value))
                            {
                                tempStones[stone] += val.Value;
                            }
                        }
                    }
                });
                currentStones = new Dictionary<double, double>(tempStones);
                stopwatch.Stop();
                curBlink++;
            }
        }
    }
}
