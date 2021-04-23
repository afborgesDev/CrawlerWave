using System.IO;
using CrawlerWave.Utils.IO;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Support.Extensions;

namespace CrawlerWave.Core.Functionalities
{
    //TODO: Should revisit this class seems it can be simplified
    internal class ScreenShotManager : BaseManager
    {
        public ScreenShotManager(ILogger logger) : base("CrawlerWave.ScreenShotManager", logger)
        {
        }

        public static ScreenShotManager New(Crawler parent) => new(parent.CreateLogger(LoggerCategory));

        public static MemoryStream ScreenShotToStream(Crawler parent) => ImageUtils.Base64ToMemoryStream(ScreenShotToBase64(parent));

        public static string ScreenShotToBase64(Crawler parent) => parent.Driver.TakeScreenshot().AsBase64EncodedString;

        public static void ScreenShotToFile(Crawler parent, SuportedImageTypes imageType, string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = ImageUtils.GetRamdomNametoScreenshot(imageType);

            parent.Driver.TakeScreenshot().SaveAsFile(fileName);
        }
    }
}
