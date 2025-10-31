using System.Text.Json.Serialization;

namespace VulScan.Modules;

public class OutputJson
{
    [JsonPropertyName("URL")]
    public string URL { get; set; }
    
    [JsonPropertyName("Payload")]
    public object RequestBody { get; set; }
    
    [JsonPropertyName("Response")]
    public object ResponseBody { get; set; }
}