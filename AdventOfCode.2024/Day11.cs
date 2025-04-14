namespace AdventOfCode
{
    using AdventOfCode.Helpers;

    internal class Day11 : Day
    {
        List<string> stones;
        public Day11(string input) : base(input)
        {
            Console.WriteLine("Advent of Code Day 11");
            stones = new List<string>();
            stones.AddRange(reader.ReadLine()!.Split(" "));

        }

        public void Task1()
        {
            Blink(25);
            Console.WriteLine("Stones: " + string.Join(", ", stones));
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
                    tempStones.AddRange(tempStone);

                }
                stones = new List<string>(tempStones);
                Console.WriteLine($"Current Stones at blink {curBlink}: " + string.Join(", ", tempStones));
                curBlink++;
            }
        }
    }
}
