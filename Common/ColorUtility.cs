namespace VulScan.Common;

public enum ColorType
{
    Red,
    Green,
    Yellow,
    Default
}

public class ColorUtility
{

    private static void SetConsoleColor(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.Red:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case ColorType.Yellow:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case ColorType.Green:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case ColorType.Default:
                Console.ResetColor();
                break;
        }
    }

    public static void PrintColored(string content, ColorType colorType)
    {
        SetConsoleColor(colorType);
        Console.WriteLine(content);
        Console.ResetColor();
    }
}