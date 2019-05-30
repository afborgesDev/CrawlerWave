using CrabsWave.Utils.IO;

namespace CrabsWave.Core.Validations
{
    public static class SeleniumDependencies
    {
        private const string WebDriverName = "chromedriver";

        public static bool CheckLocalWebDriverAvialability()
        {
            var HasOnEnv = CheckWebDriverOnEnviroment();
            var HasWithApp = FolderUtils.SafeCheckExecutableExists(WebDriverName);

            return HasOnEnv || HasWithApp;
        }

        private static bool CheckWebDriverOnEnviroment() => EnvironmentVariablesUtil.CheckSplitedVariableValueExist("Path", WebDriverName, ';');
    }
}
