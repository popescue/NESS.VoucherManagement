using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace NESS.VoucherManagement
{
    internal static class IconExtensions
    {
        public static BitmapImage ToBitmapImage(this Icon icon)
        {
            using (var stream = new MemoryStream())
            {
                icon.ToBitmap().Save(stream, ImageFormat.Png); // Was .Bmp, but this did not show a transparent background.

                stream.Position = 0;
                var result = new BitmapImage();
                result.BeginInit();

                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}