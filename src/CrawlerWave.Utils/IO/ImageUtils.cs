using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CrawlerWave.Utils.IO
{
    public static class ImageUtils
    {
        private const string ScreenshotNameTemplate = "ScreenShot_{0}_{1}.{2}";
        private const string DatetimeWithSecondsStrFormat = "ddMMyyyyHHmmss";

        public static MemoryStream Base64ToMemoryStream(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return null;

            var buffer = new Span<byte>(new byte[base64.Length]);
            if (Convert.TryFromBase64String(base64, buffer, out _))
                return new MemoryStream(buffer.ToArray());

            return null;
        }

        public static string BitmapToBase64(Bitmap bitmap, ImageFormat imageFormat)
        {
            if (bitmap == null) return string.Empty;
            return Convert.ToBase64String(BitMapToMemoryStream(bitmap, imageFormat).ToArray());
        }

        public static MemoryStream BitMapToMemoryStream(Bitmap bitmap, ImageFormat imageFormat)
        {
            if (bitmap == null) return null;

            imageFormat ??= SuportedImageTypes.Default.ImageFormat;
            var result = new MemoryStream();
            bitmap.Save(result, imageFormat);
            return result;
        }

        public static string GetRamdomNametoScreenshot(SuportedImageTypes extension, string hashCode = "")
        {
            extension ??= SuportedImageTypes.Default;
            hashCode = ValidadeHashCode(hashCode);

            return string.Format(ScreenshotNameTemplate, hashCode,
                                 DateTime.Now.ToString(DatetimeWithSecondsStrFormat),
                                 extension.FileExtension);
        }

        private static string ValidadeHashCode(string hashCode)
        {
            if (string.IsNullOrWhiteSpace(hashCode))
                return FolderUtils.ReplaceInvalidFileNameChars(Guid.NewGuid().ToString());

            return FolderUtils.ReplaceInvalidFileNameChars(hashCode);
        }
    }
}
