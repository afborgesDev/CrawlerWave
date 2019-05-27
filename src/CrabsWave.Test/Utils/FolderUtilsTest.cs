using System.Collections.Generic;
using System.IO;
using System.Text;
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
        [InlineData("testFile.exe", "")]
        [InlineData("testFile", "")]
        [InlineData("", "")]
        [InlineData("", "C:\\MyOnPocs\\CrawlerWave\\src\\CrabsWave.Test\\bin\\Debug\\netcoreapp2.2")]
        public void ShouldFileExist(string fileName, string directory)
        {
            CreateFakeFile($"{directory}{Path.DirectorySeparatorChar}{fileName}");
            var sut = FolderUtils.SafeCheckFileExist(fileName, directory);
            sut.Should().BeTrue();
            DeleteFakeFile(fileName);
        }

        private void CreateFakeFile(string filePath) => File.AppendAllText(filePath, "someText");

        private void DeleteFakeFile(string filePath) => File.Delete(filePath);
    }
}
