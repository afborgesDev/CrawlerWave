using System.IO;
using CrawlerWave.Utils.IO;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Support.Extensions;

namespace CrawlerWave.Core.Functionalities
{
    internal class ScreenShotManager : BaseManager
    {
        public ScreenShotManager(ILogger logger) : base("CrawlerWave.ScreenShotManager", logger)
        {
        }

        public static ScreenShotManager New(Crawler parent) => new ScreenShotManager(parent.CreateLogger(LoggerCategory));

        public MemoryStream ScreenShotToStream(Crawler parent) => ImageUtils.Base64ToMemoryStream(ScreenShotToBase64(parent));

        public string ScreenShotToBase64(Crawler parent) => parent.Driver.TakeScreenshot().AsBase64EncodedString;

        public void ScreenShotToFile(Crawler parent, SuportedImageTypes imageType, string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = ImageUtils.GetRamdomNametoScreenshot(imageType);

            parent.Driver.TakeScreenshot().SaveAsFile(fileName);
        }
    }
}
