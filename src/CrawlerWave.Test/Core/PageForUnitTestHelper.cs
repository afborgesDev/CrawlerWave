using System.IO;

namespace CrawlerWave.Test.Core
{
    public static class PageForUnitTestHelper
    {
        public static string GetPageForUniTestFilePath()
        {
            var file = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}PageForUnitTest.html";
            if (File.Exists(file)) return file;
            return string.Empty;
        }

        public static string GetPageForUnitTestWithMultipleItems()
        {
            var file = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}PageForTestMultipleOcurrences.html";
            if (File.Exists(file)) return file;
            return string.Empty;
        }

        public static string GetUrlForUniTestFile() => $"file:///{GetPageForUniTestFilePath()}";
    }
}
