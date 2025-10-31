namespace VulScan.Core;

public class HttpRequestHandler
{
    private static readonly HttpClient httpClient = new HttpClient();
    
    public static HttpResponseMessage SendHttpRequest(string url, HttpMethod httpMethod, string requestBody, string userAgent)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, url);

        if (httpRequestMessage.Method != HttpMethod.Get && !string.IsNullOrEmpty(requestBody))
        {
            httpRequestMessage.Content = new StringContent(requestBody);
        }
        
        httpRequestMessage.Headers.UserAgent.ParseAdd(userAgent);

        return httpClient.Send(httpRequestMessage);
    }
}