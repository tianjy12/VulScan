using System.Net;

namespace VulScan.Core;

public class HttpService
{
    
    // 忽略SSL证书
    private static readonly HttpClientHandler HttpClientHandler = new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = delegate { return true; }
    };
    private static readonly HttpClient HttpClient = new HttpClient(HttpClientHandler);
    
    /// <summary>
    /// 发送HTTP请求
    /// </summary>
    public static async Task<HttpResponseMessage?> SendHttpRequestAsync(string pocUrl, HttpMethod httpMethod, string requestBody, string userAgent)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, pocUrl);

        if (httpRequestMessage.Method != HttpMethod.Get && !string.IsNullOrEmpty(requestBody))
        {
            httpRequestMessage.Content = new StringContent(requestBody);
        }
        
        httpRequestMessage.Headers.UserAgent.ParseAdd(userAgent);

        HttpResponseMessage httpResponseMessage = null;

        try
        {
            httpResponseMessage = await HttpClient.SendAsync(httpRequestMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine($"对URL {pocUrl} 发起HTTP请求失败 {e.Message}");
        }
        return httpResponseMessage;
    }
    
    /// <summary>
    /// 处理响应，判断漏洞是否存在的逻辑
    /// </summary>
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