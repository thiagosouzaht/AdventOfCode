using AdventOfCode.DownloadInputFile;

var downloadInputClient = new DownloadInputClient(1, 2024, args[0]);
var inputValues = await downloadInputClient.DownloadInputAsync();

var list1 = new List<int>();
var list2 = new List<int>();

inputValues
    .Split("\n")
    .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
    .Where(line => line.Length == 2)
    .ToList()
    .ForEach(line =>
    {
        list1.Add(int.Parse(line[0]));
        list2.Add(int.Parse(line[1]));
    });

var orderedList1 = list1.OrderBy(x => x).ToList();
var orderedList2 = list2.OrderBy(x => x).ToList();

var sum = orderedList1.Zip(orderedList2, (x, y) => Math.Abs(x - y)).Sum();
Console.WriteLine($"Sum part 1: {sum}");

var sumPart2 = list1.Sum(valueOnList1 => list2.Count(x => x == valueOnList1) * valueOnList1);
Console.WriteLine($"Sum part 2: {sumPart2}");