using System.IO;
using CrawlerWave.Core.Functionalities;
using CrawlerWave.Utils.IO;

namespace CrawlerWave.Core
{
    public static class ScreenShotExtension
    {
        public static Crawler ScreenShotToStream(this Crawler parent, out MemoryStream memoryStream)
        {
            memoryStream = ScreenShotManager.New(parent).ScreenShotToStream(parent);
            return parent;
        }

        public static Crawler ScreenShotToBase64(this Crawler parent, out string base64)
        {
            base64 = ScreenShotManager.New(parent).ScreenShotToBase64(parent);
            return parent;
        }

        public static Crawler ScreenShotToFile(this Crawler parent, SuportedImageTypes imageType, string fileName = "")
        {
            ScreenShotManager.New(parent).ScreenShotToFile(parent, imageType, fileName);
            return parent;
        }
    }
}
