using System.IO;

namespace CrabsWave.Test.Core
{
    public static class PageForUnitTestHelper
    {
        public static string GetPageForUniTestFilePath()
        {
            var file = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}PageForUnitTest.html";
            if (File.Exists(file)) return file;
            return string.Empty;
        }
    }
}
