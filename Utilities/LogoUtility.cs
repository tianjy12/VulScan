using VulScan.Utilities;

public static class LogoUtility
{
    private const string Logo = @"

||   / /                       //   ) )
||  / /                //     ((            ___        ___         __
|| / /     //   / /   //        \\        //   ) )   //   ) )   //   ) )
||/ /     //   / /   //           ) )    //         //   / /   //   / /  Version: 2.1
|  /     ((___( (   //     ((___ / /    ((____     ((___( (   //   / /   By Asy0y0
";

    
    public static void PrintLogo()
    {
        ColorUtility.PrintColored(Logo, ColorType.Yellow);
    }
}