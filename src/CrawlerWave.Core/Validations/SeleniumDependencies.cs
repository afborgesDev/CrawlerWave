using CrawlerWave.Utils.IO;

namespace CrawlerWave.Core.Validations;

public static class SeleniumDependencies
{
    private const string WebDriverName = "chromedriver";

    public static bool CheckLocalWebDriverAvialability()
    {
        var HasOnEnv = CheckWebDriverOnEnviroment();
        var HasWithApp = FolderUtils.SafeCheckExecutableExists(WebDriverName);

        return HasOnEnv || HasWithApp;
    }

    public static string GetWebDriverPathAvaliable() => FolderUtils.GetCurrentPathWithDriverFileName(WebDriverName);

    private static bool CheckWebDriverOnEnviroment() => EnvironmentVariablesUtil.CheckSplitedVariableValueExist("Path", WebDriverName, ';');
}
