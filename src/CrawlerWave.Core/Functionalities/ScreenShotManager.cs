using System.IO;
using CrawlerWave.Utils.IO;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Support.Extensions;

namespace CrawlerWave.Core.Functionalities;

//TODO: Should revisit this class seems it can be simplified
internal class ScreenShotManager : BaseManager
{
    private const string LoggerCategoryName = "CrawlerWave.ScreenShotManager";

    public ScreenShotManager(ILogger logger) : base(LoggerCategoryName, logger)
    {
    }

    public static ScreenShotManager New(Crawler parent) => new(parent.CreateLogger(LoggerCategoryName));

    public void ScreenShotToFile(Crawler parent, SuportedImageTypes imageType, string fileName = "")
    {
        if (string.IsNullOrEmpty(fileName))
            fileName = ImageUtils.GetRamdomNametoScreenshot(imageType);

        parent.Driver.TakeScreenshot().SaveAsFile(fileName);
    }

    public string ScreenShotToBase64(Crawler parent) => parent.Driver.TakeScreenshot().AsBase64EncodedString;

    public MemoryStream ScreenShotToStream(Crawler parent) => ImageUtils.Base64ToMemoryStream(ScreenShotToBase64(parent));
}
