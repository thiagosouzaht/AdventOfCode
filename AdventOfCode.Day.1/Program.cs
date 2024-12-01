using AdventOfCode.DownloadInputFile;

var donwloadInputClient = new DonwloadInputClient( 1 , 2024, args[0]);

var list1 = new List<int>();
var list2 = new List<int>();

var inputValues = await donwloadInputClient.DownloadInputAsync();

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

var sum = 0;
for (int i = 0; i < list1.Count; i++)
{
    sum += Math.Abs(orderedList1[i] - orderedList2[i]);
}

//part 1
Console.WriteLine($"Sum: {sum}");

//part 2
//This time, you'll need to figure out exactly how often each number from the left list appears in the right list.
//Calculate a total similarity score by adding up each number in the left list after multiplying it by the number of times that number appears in the right list.

var sumPart2 = 0;
foreach (var valueOnList1 in list1)
{
    sumPart2 += list2.Count(x => x == valueOnList1) * valueOnList1;
}

Console.WriteLine($"Sum: {sumPart2}");