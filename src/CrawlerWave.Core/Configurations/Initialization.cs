using System.Threading;
using CrawlerWave.Utils.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CrawlerWave.Core.Configurations;

internal static class Initialization
{
    private const int MaxAttemptsCreateDriver = 10;

    public static (IWebDriver? driver, ChromeDriverService? service) Create(string[] capabilities)
    {
        IWebDriver? Driver = null;
        ChromeDriverService? Service = null;
        ChromeOptions? Options = null;

        for (var i = 0; i < MaxAttemptsCreateDriver; i++)
        {
            try
            {
                if (Options == null) Options = NewOptions(capabilities);

                Service = NewService();
                Driver = new ChromeDriver(Service, Options);

                return (Driver, Service);
            }
            catch
            {
                Service?.Dispose();
                Driver?.Dispose();
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }
        }

        return (null, null);
    }

    private static ChromeOptions NewOptions(string[] capabilities)
    {
        var options = new ChromeOptions() {
            AcceptInsecureCertificates = true,
            UseSpecCompliantProtocol = true
        };
        options.AddArguments(capabilities);
        return options;
    }

    private static ChromeDriverService NewService()
    {
        var service = ChromeDriverService.CreateDefaultService(FolderUtils.GetAbsolutePath());
        service.HideCommandPromptWindow = true;
        service.Port = SocketHelper.GetNewSocketPort();
        return service;
    }
}
