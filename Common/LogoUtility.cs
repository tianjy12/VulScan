using VulScan.Common;

public static class LogoUtility
{
    private const string Logo = @"

||   / /                       //   ) )
||  / /                //     ((            ___        ___         __
|| / /     //   / /   //        \\        //   ) )   //   ) )   //   ) )
||/ /     //   / /   //           ) )    //         //   / /   //   / /
|  /     ((___( (   //     ((___ / /    ((____     ((___( (   //   / /  version: 1.0                  
";

    
    public static void PrintLogo()
    {
        ColorUtility.PrintColored(Logo, ColorType.Green);
    }
}