using System.Collections.Generic;
using System.IO;
using CrabsWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Utils
{
    public class FolderUtilsTest
    {
        [Fact]
        public void ShouldResultCurrentDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var folderUtilDirectory = FolderUtils.GetAbsolutePath();
            currentDirectory.Should().BeEquivalentTo(folderUtilDirectory);
        }

        [Theory]
        [MemberData(nameof(GetExecutableCombinations))]
        public void ShouldCheckExecutableExist(string fileName, string directory, bool shoudExists)
        {
            var fileExists = FolderUtils.SafeCheckExecutableExists(fileName, directory);
            fileExists.Should().Be(shoudExists);
        }

        public static IEnumerable<object[]> GetExecutableCombinations() => new List<object[]>() {
            new object[]{ "", "", false },
            new object[]{ ".exe", "", false},
            new object[]{ "", Directory.GetCurrentDirectory(), false},
            new object[]{ ".exe", Directory.GetCurrentDirectory(), false},
        };
    }
}
