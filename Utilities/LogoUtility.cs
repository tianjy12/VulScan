using VulScan.Utilities;

public static class LogoUtility
{
    private const string Logo = @"

||   / /                       //   ) )
||  / /                //     ((            ___        ___         __
|| / /     //   / /   //        \\        //   ) )   //   ) )   //   ) )
||/ /     //   / /   //           ) )    //         //   / /   //   / /
|  /     ((___( (   //     ((___ / /    ((____     ((___( (   //   / /  version: 2.0
";

    
    public static void PrintLogo()
    {
        ColorUtility.PrintColored(Logo, ColorType.Yellow);
    }
}