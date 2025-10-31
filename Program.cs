using VulScan.Utilities;
using VulScan.Core;
using VulScan.Modules;

public class Progarm
{
    public static string Url;
    public static string FileName;
    public static int Threads = 25;
    
    private static void ParseArguments(string[] args)
    {
        
        int index = Array.IndexOf(args, "-u");
        if (index != -1)
        {
            Url = args[index + 1];
        }
        
        index = Array.IndexOf(args, "-f");
        if (index != -1)
        {
            FileName = args[index + 1];
        }
        
        index = Array.IndexOf(args, "-t");
        if (index != -1)
        {
            Threads = int.Parse(args[index + 1]);
        }
        
        index = Array.IndexOf(args, "-h");
        if (index != -1)
        {
            ColorUtility.PrintColored("Usage: ./VulScan -u <url> -f <fileName> -v <threads>", ColorType.Yellow);
        }
    }
    
    public static async Task Main(string[] args)
    {
        LogoUtility.PrintLogo();

        ParseArguments(args);
        
        POC poc = ReadFileUtility.ReadConfig();
        
        List<Task> tasks = new List<Task>();
        
        var semaphore = new SemaphoreSlim(Threads);

        Action<string> createTask = (url) =>
        {
            foreach (KeyValuePair<string, Request> kvp in poc.Requests)
            {
                string vulName = kvp.Key;
                Request request = kvp.Value;

                tasks.Add(Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        await VulnerabilityChecker.CheckAsync(vulName, url, request);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }
        };
        
        // 指定-f参数
        if (!string.IsNullOrEmpty(FileName))
        {
            List<string> urls = ReadFileUtility.ReadUrls(FileName);
            foreach (string url in urls)
            {
                createTask(url);
            }
        }
        
        // 指定-u参数
        else if (!string.IsNullOrEmpty(Url))
        {
            createTask(Url);
        }
        
        await Task.WhenAll(tasks);
        WriteFileUtility.OutputFile();
    }
}