using System.IO;
using System.Runtime.InteropServices;
using CrabsWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Utils
{
    public class FolderUtilsTest
    {
        [Fact]
        public void ShouldRetriveValidDirectory()
        {
            var sut = FolderUtils.GetAbsolutePath();
            Directory.Exists(sut).Should().BeTrue();
        }

        [Theory]
        [InlineData("testFile.exe", "C:\\MyOnPocs\\CrawlerWave\\src\\CrabsWave.Test\\bin\\Debug\\netcoreapp2.2", true)]
        [InlineData("testFile", "C:\\MyOnPocs\\CrawlerWave\\src\\CrabsWave.Test\\bin\\Debug\\netcoreapp2.2", true)]
        [InlineData("", "", false)]
        [InlineData("testFile.exe", "", false)]
        [InlineData("", "C:\\MyOnPocs\\CrawlerWave\\src\\CrabsWave.Test\\bin\\Debug\\netcoreapp2.2", false)]
        public void ShouldCheckIfFileExist(string fileName, string directory, bool exist)
        {
            var useNewFile = !string.IsNullOrWhiteSpace(fileName) && !string.IsNullOrWhiteSpace(directory);

            if (useNewFile)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !fileName.Contains(".exe"))
                    fileName += ".exe";

                CreateFakeFile($"{directory}{Path.DirectorySeparatorChar}{fileName}");
            }

            var sut = FolderUtils.SafeCheckFileExist(fileName, directory);

            if (exist)
                sut.Should().BeTrue();
            else
                sut.Should().BeFalse();

            if (useNewFile)
                DeleteFakeFile($"{directory}{Path.DirectorySeparatorChar}{fileName}");
        }

        private void CreateFakeFile(string filePath) => File.AppendAllText(filePath, "someText");

        private void DeleteFakeFile(string filePath) => File.Delete(filePath);
    }
}
