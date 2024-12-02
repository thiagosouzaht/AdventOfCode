namespace AdventOfCode.DownloadInputFile;

public class DonwloadInputClient
{
    private readonly string _sessionId;

    private readonly Uri _baseUri = new("https://adventofcode.com/");
    private readonly string _relativePathTemplate = "@year/day/@day/input";
    private string _relativePath = string.Empty;
    private string userFileLocation => Path.Combine(Environment.CurrentDirectory, "cache", $"{_sessionId}-{_relativePath.Replace('/', '-')}.txt");
    
    public DonwloadInputClient(int day, int year, string sessionId)
    {
        _sessionId = sessionId;
        
        _relativePath = _relativePathTemplate
            .Replace("@year", year.ToString())
            .Replace("@day", day.ToString());
    }
    
    public string DownloadInput()
    {
        if (CacheUserExists())
        {
            return LoadCacheUserInput().Result;
        }
        
        HttpClient httpClient = GetHttpClient();
        var content = httpClient.GetStringAsync(_relativePath).Result;

        CreateCacheUserInput(content);
        
        return content;
    }

    public async Task<string> DownloadInputAsync()
    {
        HttpClient httpClient = GetHttpClient();

        if (CacheUserExists())
        {
            return await LoadCacheUserInput();
        }
        
        var content = await httpClient.GetStringAsync(_relativePath);
        
        await CreateCacheUserInput(content);
        
        return content;
    }

    private HttpClient GetHttpClient()
    {
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = _baseUri;
        httpClient.DefaultRequestHeaders.Add("Cookie", $"session={_sessionId}");
        httpClient.DefaultRequestHeaders.Add("User-Agent", @"github.com-thiagosouzaht-AdventOfCode");
        
        return httpClient;
    }

    private Task CreateCacheUserInput(string content)
    {
        if(!Directory.Exists(Path.GetDirectoryName(userFileLocation)))
            Directory.CreateDirectory(Path.GetDirectoryName(userFileLocation));
        
        using var stream = new StreamWriter(userFileLocation);
        
        stream.Write(content);
        stream.Flush();

        return Task.CompletedTask;
    }

    private Task<string> LoadCacheUserInput()
    {
        using var stream = new StreamReader(userFileLocation);
        
        return Task.FromResult(stream.ReadToEnd());
    }

    private bool CacheUserExists()
    {
        return File.Exists(Path.Combine(userFileLocation));
    }
}