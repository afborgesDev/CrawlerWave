using System.Collections.Generic;
using System.Drawing;
using CrabsWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Utils
{
    public class ImageUtilsTest
    {
        public static IEnumerable<object[]> GetExamplesToRamdomFileName() => new List<object[]> {
            new object[] {"", null , false, false} ,
            new object[] {"", SuportedImageTypes.PNG, false, true} ,
            new object[] {"4df5b153-7dcc-45da-ad0a-7df68cdd8d84", null, true, false},
            new object[] {"4df5b153-7dcc-45da-ad0a-7df68cdd8d84", SuportedImageTypes.PNG, true, true},
            new object[] {"4df5b153-7dcc-45da-ad0a-7df68cdd8d84", SuportedImageTypes.JPG, true, true},
            new object[] {"", SuportedImageTypes.JPG, false, true},
            new object[] {":::::::::::::", SuportedImageTypes.PNG, false, true } ,
        };

        public static IEnumerable<object[]> GetBitMapExample() => new List<object[]> {
            new object[] {new Bitmap(10, 10), SuportedImageTypes.PNG, false},
            new object[] {new Bitmap(10, 10), null, false},
            new object[] {null, null, true },
            new object[] {null, SuportedImageTypes.PNG, true},
        };

        [Theory]
        [MemberData(nameof(GetExamplesToRamdomFileName))]
        public void ShouldTestHappyPathRamdomFileName(string hashValue, SuportedImageTypes extensionValue, bool shouldCompareHash, bool shouldCompareExtension)
        {
            var invalidCharsForPlatform = string.Join(" ", System.IO.Path.GetInvalidFileNameChars()).Split(" ");
            var fileName = ImageUtils.GetRamdomNametoScreenshot(extensionValue, hashValue);

            fileName.Should().NotBeNullOrWhiteSpace();
            fileName.Should().NotContainAny(invalidCharsForPlatform);
            if (shouldCompareHash)
                fileName.Should().Contain(hashValue);

            if (shouldCompareExtension)
                fileName.Should().Contain(extensionValue.FileExtension);
        }

        [Theory]
        [MemberData(nameof(GetBitMapExample))]
        public void ShouldReturnMemoryStreamFromBitmap(Bitmap bitmap, SuportedImageTypes suportedImageTypes, bool shouldFail)
        {
            var image = ImageUtils.BitMapToMemoryStream(bitmap, suportedImageTypes?.ImageFormat);

            if (shouldFail)
                image.Should().BeNull();
            else
                image.Should().NotBeNull();
        }

        [Theory]
        [MemberData(nameof(GetBitMapExample))]
        public void ShouldReturnBase64FromBitmap(Bitmap bitmap, SuportedImageTypes suportedImageTypes, bool shouldFail)
        {
            var image = ImageUtils.BitmapToBase64(bitmap, suportedImageTypes?.ImageFormat);

            if (shouldFail)
                image.Should().BeNullOrEmpty();
            else
                image.Should().NotBeNullOrEmpty();
        }
    }
}
