using System;

namespace prjScreenSwitcher.Extensions
{
    public static class GeneralExtensions
    {
        public static void SafeDispose<T>(this T sender) where T : IDisposable
        {
            // If the object is null, nothing needs to be disposed of, so it shouldn't be allowed to get through here.
            if (sender != null)
            {
                sender.Dispose();
            }
        }
    }
}