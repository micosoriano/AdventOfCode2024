namespace AdventOfCode
{
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
            int curBlink = 1;
            List<string> tempStones = new List<string>();
            while (curBlink <= blinks)
            {
                tempStones.Clear();
                foreach (var stone in stones)
                {
                    List<string> tempStone = new List<string>();

                    if (knownStones.ContainsKey(stone))
                    {
                        tempStone = knownStones[stone];
                    }
                    else
                    {
                        if (stone == "0") tempStone.Add("1");
                        else if (stone.Length % 2 == 0)
                        {
                            var list = stone.ToCharArray().ToList();
                            var firstHalf = list.Take(list.Count / 2).ToList();
                            var secondHalf = list.Skip(list.Count / 2).ToList();
                            tempStone.Add(new string(firstHalf.ToArray()));
                            tempStone.Add(double.Parse(secondHalf.ToArray()).ToString());
                        }
                        else
                        {
                            var doubleVal = double.Parse(stone);
                            tempStone.Add((doubleVal * 2024).ToString());
                        }
                        knownStones.Add(stone, tempStone);

                    }
                    tempStones.AddRange(tempStone);

                }
                stones = new List<string>(tempStones);
                //Console.WriteLine($"Current Stones at blink {curBlink}: " + string.Join(", ", tempStones));
                Console.WriteLine("Current blink " + curBlink);
                curBlink++;
            }
        }
    }
}
