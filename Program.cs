using VulScan.Utilities;
using VulScan.Core;
using VulScan.Modules;

public class Progarm
{
    private static ScanArtuments ParseArguments(string[] args)
    {
        ScanArtuments scanArtuments = new ScanArtuments();
        
        int index = Array.IndexOf(args, "-u");
        if (index != -1)
        {
            scanArtuments.Url = args[index + 1];
        }
        
        index = Array.IndexOf(args, "-f");
        if (index != -1)
        {
            scanArtuments.FileName = args[index + 1];
        }
        
        index = Array.IndexOf(args, "-h");
        if (index != -1)
        {
            ColorUtility.PrintColored("Usage: ./VulScan -u <url> -f <fileName>", ColorType.Yellow);
        }
        
        return scanArtuments;
    }
    
    public static void Main(string[] args)
    {
        LogoUtility.PrintLogo();

        ScanArtuments scanArtuments = ParseArguments(args);
        
        Config config = ReadFileUtility.ReadConfig();

        if (!string.IsNullOrEmpty(scanArtuments.FileName))
        {
            List<string> urls = ReadFileUtility.ReadUrls(scanArtuments.FileName);
            foreach (var kv in config.Requests)
            {
                string vulName = kv.Key;
                Request request = kv.Value;

                foreach (string url in urls)
                {
                    VulnerabilityChecker.Check(vulName, url, request);
                }
            }
            OutputUtility.OutputFile();
        }
        else if (!string.IsNullOrEmpty(scanArtuments.Url))
        {
            foreach (var kv in config.Requests)
            {
                string vulName = kv.Key;
                Request request = kv.Value;
        
                VulnerabilityChecker.Check(vulName, scanArtuments.Url, request);
            }
            OutputUtility.OutputFile();
        }
    }
}