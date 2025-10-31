using VulScan.Utilities;
using VulScan.Core;
using VulScan.Modules;

public class Progarm
{
    public static string Url;
    public static string FileName;
    public static int Threads = 25;
    public static POC Poc = ReadFileUtility.ReadConfig();
    public static List<Task> Tasks = new List<Task>();
    public static SemaphoreSlim SemaphoreSlim;
    
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

    private static void CreateTask(string url)
    {
        foreach (KeyValuePair<string, Request> kvp in Poc.Requests)
        {
            string vulName = kvp.Key;
            Request request = kvp.Value;

            Tasks.Add(Task.Run(async () =>
            {
                await SemaphoreSlim.WaitAsync();
                try
                {
                    await VulnerabilityChecker.CheckAsync(vulName, url, request);
                }
                finally
                {
                    SemaphoreSlim.Release();
                }
            }));
        }
    }
    
    public static async Task Main(string[] args)
    {
        LogoUtility.PrintLogo();

        ParseArguments(args);
        
        SemaphoreSlim = new SemaphoreSlim(Threads);

        // 指定-f参数
        if (!string.IsNullOrEmpty(FileName))
        {
            List<string> urls = ReadFileUtility.ReadUrls(FileName);
            foreach (string url in urls)
            {
                CreateTask(url);
            }
        }
        
        // 指定-u参数
        else if (!string.IsNullOrEmpty(Url))
        {
            CreateTask(Url);
        }
        
        await Task.WhenAll(Tasks);
        WriteFileUtility.OutputFile();
    }
}