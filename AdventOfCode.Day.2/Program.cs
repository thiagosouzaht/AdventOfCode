using AdventOfCode.DownloadInputFile;

var downloadInputClient = new DownloadInputClient(2, 2024, args[0]);
var inputValues = await downloadInputClient.DownloadInputAsync();

var reports = inputValues
    .Split("\n", StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

var sumOfValidReport = reports.Count(IsReportSafe);

var invalidReports = reports.Where(report => !IsReportSafe(report));

var sumOfInvalidButValid = 0;

foreach (var report in invalidReports)
{
    var isValid = false;

    for (int i = 0; i < report.Count(); i++)
    {
        var reportWithoutCurrentValue = report.Take(i).Concat(report.Skip(i + 1));

        if (IsReportSafe(reportWithoutCurrentValue)) isValid = true;
    }

    if (isValid) sumOfInvalidButValid++;
}

Console.WriteLine($"Solution number 1: {sumOfValidReport}");
Console.WriteLine($"Solution number 2: {sumOfInvalidButValid + sumOfValidReport}");

bool IsReportSafe(IEnumerable<int> report)
{
    var isAllOnAscOrder = report.SequenceEqual(report.OrderBy(x => x));
    var isAllOnDescOrder = report.SequenceEqual(report.OrderByDescending(x => x));
    var isReportValid = report
        .Zip(report.Skip(1), (a, b) => a - b)
        .All(x => Math.Abs(x) is >= 1 and <= 3);
    
    return isReportValid && (isAllOnAscOrder || isAllOnDescOrder);
}