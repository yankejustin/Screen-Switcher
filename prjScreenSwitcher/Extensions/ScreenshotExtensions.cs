using System.Drawing;

namespace prjScreenSwitcher.Extensions
{
    public static class ScreenshotExtensions
    {
        public static Bitmap Scale(this Bitmap sender, Size newSize)
        {
            if (sender != null)
            {
                using (sender)
                {
                    return new Bitmap(sender, newSize);
                }
            }
            else
            {
                return null;
            }
        }
    }
}