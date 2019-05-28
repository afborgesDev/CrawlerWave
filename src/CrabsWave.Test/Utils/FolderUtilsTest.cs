using System.IO;
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

    }
}
