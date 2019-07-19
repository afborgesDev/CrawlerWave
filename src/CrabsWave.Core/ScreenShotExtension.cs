using System.IO;
using CrabsWave.Core.Functionalities;
using CrabsWave.Utils.IO;

namespace CrabsWave.Core
{
    public static class ScreenShotExtension
    {
        public static Crawler ScreenShotToStream(this Crawler parent, out MemoryStream memoryStream)
        {
            memoryStream = ScreenShotManager.Create(parent).ScreenShotToStream(parent);
            return parent;
        }

        public static Crawler ScreenShotToBase64(this Crawler parent, out string base64)
        {
            base64 = ScreenShotManager.Create(parent).ScreenShotToBase64(parent);
            return parent;
        }

        public static Crawler ScreenShotToFile(this Crawler parent, SuportedImageTypes imageType, string fileName = "")
        {
            ScreenShotManager.Create(parent).ScreenShotToFile(parent, imageType, fileName);
            return parent;
        }
    }
}
