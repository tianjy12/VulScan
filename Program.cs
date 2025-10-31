using VulScan.Common;
using VulScan.Core;
using VulScan.Module;

public class Progarm
{
    public static void Main(string[] args)
    {
        LogoUtility.PrintLogo();
        
        Config config = ReadConfigUtility.ReadConfig();

        string url = "";
        
        int index = Array.IndexOf(args, "-u");
        if (index != -1)
        {
            url = args[index + 1];
        }
        
        foreach (var kvp in config.Requests)
        {
            string vulName = kvp.Key;
            Request request = kvp.Value;
        
            CheckVul.Check(vulName, url, request);
        }
        OutputUtility.OutputFile();
    }
}