using YamlDotNet.Serialization;

namespace VulScan.Modules;

public class Request
{
    public string URL { get; set; }
    
    public string Method { get; set; }
    
    [YamlMember(Alias = "User-Agent")]
    public string UserAgent { get; set; }
    
    [YamlMember(Alias = "Content-Type")]
    public string ContentType { get; set; }
    
    public string Body {get; set;}
}