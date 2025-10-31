using System.Net;
using VulScan.Common;
using VulScan.Module;

namespace VulScan.Core;

public class CheckVul
{
    public static void Check(string vulName, string url, Request request)
    {
        // 1.发送HTTP请求
        string pocUrl = UrlUtility.JoinUrl(url, request.URL);
        HttpMethod httpMethod = new HttpMethod(request.Method);
        string requestBody = request.Body;
        string userAgent = request.UserAgent;

        HttpResponseMessage httpResponseMessage = HttpRequestHandler.SendHttpRequest(pocUrl, httpMethod, requestBody, userAgent);
        
        // 2.分析响应
        HttpStatusCode httpStatusCode = httpResponseMessage.StatusCode;
        string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();
        
        if (HttpResponseHandler.HandleHttpResponse(vulName, httpStatusCode, responseBody))
        {
            OutputUtility.AddNewOutputJson(pocUrl, requestBody, responseBody);
            ColorUtility.PrintColored($"[*]目标 {url} 存在漏洞 {vulName}", ColorType.Red);
            Exploit.VulExploit(vulName, requestBody, responseBody);
        }
    }
}