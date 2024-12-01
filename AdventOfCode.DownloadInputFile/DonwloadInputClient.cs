namespace AdventOfCode.DownloadInputFile;

public class DonwloadInputClient
{
    private readonly string _sessionId;

    private readonly Uri _baseUri = new("https://adventofcode.com/");
    private readonly string _relativePathTemplate = "@year/day/@day/input";
    private string _relativePath = string.Empty;
    
    public DonwloadInputClient(int day, int year, string sessionId)
    {
        _sessionId = sessionId;
        
        _relativePath = _relativePathTemplate
            .Replace("@year", year.ToString())
            .Replace("@day", day.ToString());
    }
    
    public string DownloadInput()
    {
        HttpClient httpClient = GetHttpClient();
        var input = httpClient.GetStringAsync(_relativePath).Result;
        
        return input;
    }

    public async Task<string> DownloadInputAsync()
    {
        HttpClient httpClient = GetHttpClient();
        
        return await httpClient.GetStringAsync(_relativePath);
    }

    private HttpClient GetHttpClient()
    {
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = _baseUri;
        httpClient.DefaultRequestHeaders.Add("Cookie", $"session={_sessionId}");
        
        return httpClient;
    }
}