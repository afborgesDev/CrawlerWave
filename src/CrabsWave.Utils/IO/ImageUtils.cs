using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CrabsWave.Utils.IO
{
    public static class ImageUtils
    {
        private const string ScreenshotNameTemplate = "ScreenShot_{0}_{1}.{2}";
        private const string DefaultScreenShotExtension = "png";
        private const string DatetimeWithSecondsStrFormat = "ddMMyyyyHHmmss";
        private static readonly string[] SupportedScreenShotExtensions = new string[] { "png", "jpg" };
        public static string BitmapToBase64(Bitmap bitmap) => Convert.ToBase64String(BitMapToMemoryStream(bitmap).ToArray());

        public static MemoryStream BitMapToMemoryStream(Bitmap bitmap)
        {
            var result = new MemoryStream();
            bitmap.Save(result, ImageFormat.Png);
            return result;
        }

        public static string GetRamdomNametoScreenshot(string hashCode = "",  string extension = DefaultScreenShotExtension)
        {
            hashCode = ValidadeHashCode(hashCode);
            extension = ValidateExtension(extension);
            return string.Format(ScreenshotNameTemplate, hashCode, DateTime.Now.ToString(DatetimeWithSecondsStrFormat), extension);
        }

        private static string ValidadeHashCode(string hashCode)
        {
            if (string.IsNullOrWhiteSpace(hashCode))
                return FolderUtils.ReplaceInvalidFileNameChars(Guid.NewGuid().ToString());

            return FolderUtils.ReplaceInvalidFileNameChars(hashCode);
        }

        private static string ValidateExtension(string extension)
        {
            extension = extension.Replace(".", string.Empty).ToLower();

            if (string.IsNullOrWhiteSpace(extension))
                return $".{DefaultScreenShotExtension}";

            
            if (Array.IndexOf(SupportedScreenShotExtensions, extension) < 0)
                return $".{DefaultScreenShotExtension}";

            return $".{extension}";
        }
    }
}
