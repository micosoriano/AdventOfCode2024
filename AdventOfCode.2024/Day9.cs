namespace AdventOfCode
{
    using System.Text.RegularExpressions;
    using AdventOfCode.Helpers;

    internal class Day9 : Day
    {
        List<string> fileSystem;
        List<int> rawData;
        public Day9(string input) : base(input)
        {
            rawData = new List<int>();
            fileSystem = new List<string>();
            Console.WriteLine("Advent of Code Day 9");
            var line = reader.ReadLine()!;
            int y = 0;
            foreach (var file in line)
            {
                rawData.Add(int.Parse(file.ToString()));
                string fileString = file.ToString();
                for (int i = 0; i < int.Parse(fileString); i++)
                {
                    if (y % 2 == 0)
                    {
                        fileSystem.Add((y/2).ToString());
                    }
                    else
                    {
                        fileSystem.Add(".");
                    }
                }
                y++;
            }
        }

        public void Task1()
        {
            var task1System = new List<string>(fileSystem);
            for (int i = task1System.Count - 1; i > 0; i--)
            {
                var idxSpace = task1System.IndexOf(".");
                var checker = task1System.Skip(idxSpace);
                if (!checker.Any(s => int.TryParse(s, out _))) break;
                
                if (task1System[i] != ".")
                {
                    task1System[idxSpace] = task1System[i];
                    task1System[i] = ".";
                }
            }

            double sum = 0;
            for (int i = 0; i < task1System.Count; i++)
            {
                if (int.TryParse(task1System[i], out var val))
                {
                    sum += val*i;
                }
            }

            Console.WriteLine("Sum: " + sum);
        }

        public void Task2()
        {
            var task2System = new List<string>(fileSystem);
            List<List<string>> task2List = new List<List<string>>();

            int idxSystem = 0;
            for (int i = 0; i < rawData.Count; i++)
            {
                if (rawData[i] != 0)
                {
                    task2List.Add(task2System.GetRange(idxSystem, rawData[i]));
                    idxSystem += rawData[i];
                }
            }

            for (int i = task2List.Count - 1; i > 0; i--)
            {
                if (!task2List[i].Contains("."))
                {
                    var idxSpace = task2List.IndexOf(task2List.Where(x => x.Contains(".") && x.Count(y => y == ".") >= task2List[i].Count).FirstOrDefault()!);
                    if (idxSpace != -1 && idxSpace < i)
                    {
                        var temp = task2List[idxSpace];
                        if (task2List[idxSpace].Count(y => y == ".") >= task2List[i].Count)
                        {
                            var innerSpace = temp.IndexOf(".");
                            var space = temp.GetRange(innerSpace, task2List[i].Count);
                            temp.RemoveRange(innerSpace, task2List[i].Count);
                            temp.InsertRange(innerSpace, task2List[i]);
                            task2List[idxSpace] = temp;
                            task2List[i] = space;
                        }
                        else
                        {
                            task2List[idxSpace] = task2List[i];
                            task2List[i] = temp;
                        }
                    }
                }
            }

            var parsedList = task2List.SelectMany(x => x).ToList();
            double sum = 0;
            for (int i = 0; i < parsedList.Count; i++)
            {
                if (int.TryParse(parsedList[i], out var val))
                {
                    sum += val * i;
                }
            }
            Console.WriteLine("Sum: " + sum);
        }
    }
}
