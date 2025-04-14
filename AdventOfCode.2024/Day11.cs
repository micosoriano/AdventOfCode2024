namespace AdventOfCode
{
    using System.Diagnostics;
    using AdventOfCode.Helpers;
    using static System.Formats.Asn1.AsnWriter;

    internal class Day11 : Day
    {
        List<string> stones;
        Dictionary<string, List<string>> knownStones;
        public Day11(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 11");
            stones = new List<string>();
            knownStones = new Dictionary<string, List<string>>();
            stones.AddRange(reader.ReadLine()!.Split(" "));

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
            List<string> tempStones = new List<string>();
            while (curBlink <= blinks)
            {
                stopwatch.Start();
                tempStones.Clear();
                var dict = stones.GroupBy(x => x).ToList().ToDictionary(y => y.Key, y => y.Count());
                Parallel.ForEach(dict, val =>
                {
                    List<string> tempStone = new List<string>();
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
                        if (val.Key == "0") tempStone.Add("1");
                        else if (val.Key.Length % 2 == 0)
                        {
                            var list = val.Key.ToCharArray().ToList();
                            var firstHalf = list.Take(list.Count / 2).ToList();
                            var secondHalf = list.Skip(list.Count / 2).ToList();
                            tempStone.Add(new string(firstHalf.ToArray()));
                            tempStone.Add(double.Parse(secondHalf.ToArray()).ToString());
                        }
                        else
                        {
                            var doubleVal = double.Parse(val.Key);
                            tempStone.Add((doubleVal * 2024).ToString());
                        }
                        lock (knownStones)
                        {
                            knownStones.TryAdd(val.Key, tempStone);
                        }
                    }
                    lock (tempStones)
                    {
                        List<string> list = new List<string>();
                        for (int i = 0; i < val.Value; i++)
                        {
                            list.AddRange(tempStone);
                        }
                        tempStones.AddRange(list);
                    }
                });
                stones = new List<string>(tempStones);
                stopwatch.Stop();
                //Console.WriteLine($"Current Stones at blink {curBlink}: " + string.Join(", ", tempStones));
                Console.WriteLine("Current blink " + curBlink);
                Console.WriteLine("Time elapsed for blink: {0}", stopwatch.Elapsed);
                curBlink++;
            }
        }
    }
}
