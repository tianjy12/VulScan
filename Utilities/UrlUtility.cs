namespace VulScan.Utilities;

public class UrlUtility
{
    
    public static string JoinUrl(string url, string path)
    {
        return url.TrimEnd('/') + '/' + path;
    }
}