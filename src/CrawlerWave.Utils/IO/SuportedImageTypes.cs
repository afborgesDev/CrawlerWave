using System.Drawing.Imaging;

namespace CrawlerWave.Utils.IO
{
    public class SuportedImageTypes
    {
        public readonly string FileExtension;
        public readonly ImageFormat ImageFormat;

        protected SuportedImageTypes(string extension, ImageFormat imageFormat)
        {
            FileExtension = extension;
            ImageFormat = imageFormat;
        }

        public static SuportedImageTypes JPG => new("jpg", ImageFormat.Jpeg);
        public static SuportedImageTypes PNG => new("png", ImageFormat.Png);
        public static SuportedImageTypes Default => PNG;
    }
}
