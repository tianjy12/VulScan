using VulScan.Modules;
using YamlDotNet.Serialization;

namespace VulScan.Utilities;

public class ReadFileUtility
{
    /// <summary>
    /// 从配置文件中读取内容，按属性名映射，反序列化成Config对象
    /// </summary>
    /// <returns></returns>
    public static Config ReadConfig()
    {
        string ConfigFilePath = Path.Combine(AppContext.BaseDirectory, "Config.yaml");
        string configData = File.ReadAllText(ConfigFilePath);
        var deserializer = new Deserializer();
        Config config = deserializer.Deserialize<Config>(configData);
        return config;
    }


    /// <summary>
    /// 从文件中批量读取URL进行扫描
    /// </summary>
    /// <returns></returns>
    public static List<string> ReadUrls(string filePath)
    {
        filePath = Path.Combine(AppContext.BaseDirectory, filePath);
        if (File.Exists(filePath))
        {
            List<string> list = File.ReadAllLines(filePath)
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(line => line.Trim())
                .ToList();
            ColorUtility.PrintColored($"成功加载{list.Count}条URL", ColorType.Green);
            return list;
        }

        return new List<string>();
    }
}