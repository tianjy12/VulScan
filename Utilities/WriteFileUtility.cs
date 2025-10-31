using System.Collections.Concurrent;
using System.Text.Json;
using VulScan.Modules;

namespace VulScan.Utilities;

public class WriteFileUtility
{
    private static string JsonFilePath = Path.Combine(AppContext.BaseDirectory, "Result_");

    private static ConcurrentBag<OutputJson> OutputJsons = new ConcurrentBag<OutputJson>(); 
    
    private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
    {
        WriteIndented = true
    };

    public static void AddNewOutputJson(string url, string requestBody, string responseBody)
    {
        var newResult = new OutputJson
        {
            URL = url,
            RequestBody = requestBody,
            ResponseBody = responseBody,
        };
        OutputJsons.Add(newResult);
    }
    
    /// <summary>
    /// 保存扫描结果到文件，只保存有漏洞的URL
    /// </summary>
    public static void OutputFile()
    {
        string fileName = "Result_" + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + ".json";
        string data = JsonSerializer.Serialize(OutputJsons, SerializerOptions);
    
        File.WriteAllText(JsonFilePath + fileName, data); 
        ColorUtility.PrintColored("漏洞扫描完成", ColorType.Green);
        ColorUtility.PrintColored($"扫描结果已保存至{fileName}", ColorType.Green);
    }
}