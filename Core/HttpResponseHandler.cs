using System.Net;

namespace VulScan.Core;

public class HttpResponseHandler
{
    public static bool HandleHttpResponse(string vulName, HttpStatusCode httpStatusCode, string responseBody)
    {
        switch (vulName)
        {
            case "AstrBot任意文件读取":
                return httpStatusCode == HttpStatusCode.OK && responseBody.Contains("username") && responseBody.Contains("password");
            default:
                break;
        }
        return false;
    }
}