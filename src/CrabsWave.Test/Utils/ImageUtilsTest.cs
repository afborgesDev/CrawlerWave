using System;
using System.Collections.Generic;
using System.Text;
using CrabsWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Utils
{
    public class ImageUtilsTest
    {
        public static IEnumerable<object[]> GetHappyPathExamplesToRamdomFileName() => new List<object[]> {
            new object[] {"", "" , false, false} ,
            new object[] {"", "png", false, true} ,
            new object[] {"4df5b153-7dcc-45da-ad0a-7df68cdd8d84", "", true, false},
            new object[] {"4df5b153-7dcc-45da-ad0a-7df68cdd8d84", "png", true, true},
            new object[] {"4df5b153-7dcc-45da-ad0a-7df68cdd8d84", "jpg", true, true},
            new object[] {"", "jpg", false, true}
        };

        public static IEnumerable<object[]> GetUnHappyPathExamplesToRamdomFileName() => new List<object[]> {
            new object[] {":::::::::::::", "" , "_____________", "png"} ,
            new object[] {":::::::::::::", "gif" , "_____________", "png"} ,
        };

        [Theory]
        [MemberData(nameof(GetHappyPathExamplesToRamdomFileName))]
        public static void ShouldTestHappyPathRomdomFileName(string hashValue, string extensionValue, bool shouldCompareHash, bool shouldCompareExtension)
        {
            var fileName = ImageUtils.GetRamdomNametoScreenshot(hashValue, extensionValue);

            fileName.Should().NotBeNullOrWhiteSpace();
            if (shouldCompareHash)
                fileName.Should().Contain(hashValue);

            if (shouldCompareExtension)
                fileName.Should().Contain(extensionValue);
        }

        [Theory]
        [MemberData(nameof(GetUnHappyPathExamplesToRamdomFileName))]
        public static void ShouldTestUnHappyPathRomdomFileName(string hashValue, string extensionValue, string expectedHash, string expectedExtension)
        {
            var fileName = ImageUtils.GetRamdomNametoScreenshot(hashValue, extensionValue);

            fileName.Should().NotBeNullOrWhiteSpace();
            fileName.Should().Contain(expectedHash);
            fileName.Should().Contain(expectedExtension);
        }
    }
}
