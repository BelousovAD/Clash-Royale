namespace Bootstrap
{
    using UnityEngine;
    using UnityEngine.Rendering;

    internal sealed class SplashScreenSkipper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void SkipSplashScreen() =>
            SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
    }
}