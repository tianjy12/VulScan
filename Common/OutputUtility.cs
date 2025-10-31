using System.Text.Json;
using VulScan.Module;

namespace VulScan.Common;

public class OutputUtility
{
    private static string JsonFilePath = Path.Combine(AppContext.BaseDirectory, "Result_");

    private static List<OutputJson> jsonList = new List<OutputJson>();
    
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
        jsonList.Add(newResult);
    }
    
    public static void OutputFile()
    {
        string data = JsonSerializer.Serialize(jsonList, SerializerOptions);
    
        File.WriteAllText(JsonFilePath + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + ".json", data); 
    }
}