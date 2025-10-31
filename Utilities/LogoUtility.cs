using VulScan.Utilities;

public static class LogoUtility
{
    private const string Logo = @"

||   / /                       //   ) )
||  / /                //     ((            ___        ___         __
|| / /     //   / /   //        \\        //   ) )   //   ) )   //   ) )
||/ /     //   / /   //           ) )    //         //   / /   //   / /
|  /     ((___( (   //     ((___ / /    ((____     ((___( (   //   / /  version: 1.1
";

    
    public static void PrintLogo()
    {
        ColorUtility.PrintColored(Logo, ColorType.Yellow);
    }
}