using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using prjScreenSwitcher.Utilities;
using System.Windows.Forms;

namespace prjScreenSwitcher.Extensions
{
    public static class ProcessExtensions
    {
        public static Bitmap CaptureScreenshot(this Process sender)
        {
            if (sender.IsWindowable())
            {
                Win32Types.RECT rect = new Win32Types.RECT();

                Bitmap bmp = null;
                try
                {
                    if (Win32.GetWindowRect(sender.MainWindowHandle, out rect))
                    {
                        bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                        if (bmp != null)
                        {
                            using (Graphics graphics = Graphics.FromImage(bmp))
                            {
                                graphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size, CopyPixelOperation.SourceCopy);

                                return bmp;
                            }
                        }
                    }
                }
                catch
                {
                    // There was an issue with capturing the screenshot that can't be fixed.
                    // Make sure that the Bitmap that would have been returned is instead disposed.
                    if (bmp != null)
                    {
                        bmp.Dispose();
                    }
                }
            }

            return null;
        }

        public static bool IsWindowable(this Process sender)
        {
            try
            {
                if (Screen.AllScreens.Length > 0 && sender.MainWindowHandle != IntPtr.Zero)
                {
                    return Win32.IsWindow(sender.MainWindowHandle);
                }
            }
            catch
            { }

            return false;
        }
    }
}