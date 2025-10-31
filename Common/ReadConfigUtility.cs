using VulScan.Module;
using YamlDotNet.Serialization;

namespace VulScan.Common;

public class ReadConfigUtility
{
    private static string ConfigFilePath = Path.Combine(AppContext.BaseDirectory, "Config.yaml");

    /// <summary>
    /// 从配置文件中读取内容，按属性名映射，反序列化成Config对象
    /// </summary>
    /// <returns></returns>
    public static Config ReadConfig()
    {
        string configData = File.ReadAllText(ConfigFilePath);
        var deserializer = new Deserializer();
        Config config = deserializer.Deserialize<Config>(configData);
        return config;
    }
}