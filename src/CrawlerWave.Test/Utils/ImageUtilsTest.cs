using System.Collections.Generic;
using System.Drawing;
using CrawlerWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Utils
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

        public static IEnumerable<object[]> GetBase64ToExample() => new List<object[]> {
            new object[] { "", false },
            new object[] { "     ", false },
            new object[] { "123asd123ase12adsd1qsd1asd", false },
            new object[] { "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAACXBIWXMAAA7EAAAOxAGVKw4bAAACn0lEQVRYhe2XP09TURjGf+fPbQvBOEkQQkKVkhgV4mDi6EAddOM7oLOfxF2+gQOriTA6mLgohDAUYVABdTJCabnnnNfhwm0by+mVRYeeoenNr33Svu99ntxHuXdtUZMaPW3pPuHAE3YccuiR/YB88T1cTWj0rEVOJcrVhKGv/r4nNBxWTRvSF0foWxZTL4NV+PU2YSMlWR7FvT8leT5G2Hb4tTY4wSyW0fMJ6UoTjkKU2/ulP/XXWoRNR7I8itWTBn3bErYcasLAiCJspKgZg55LkIMmYcth7iTIoYcTwSyUCHsOabjs30R4X/1Nl+tbAPOoghpPsY8r+Yj0lM7f+zct5LvHv26fD5jw1RfmMf3s1QAjqrO/CpB0rvHAieSX0hJIpTiP6Fsg2/mHNIdutYWqGUpPx7LvL5Yx90qAQlqCXaogDU+6clyIx/Rt+OYJH892PqUhUaiaQRqesOdQ4xq9kBD2XDbWVJCGR89Z1IyBpkR5X/3ZLn33tiXqmkbPJXSfsOeyG+mnID9CfsPlY5wx6KpF2nGurir66u86wq5Dnb46lkv5+CwnVElF+YU5cl2jaxYrn/3f+7grJxjTf58j9Qr6rs305SAU9nHY7oz53MdAlPfNkfkk189cUMDHYeuoZ4S2Xi7MY/pZDhTxcVcsAL05MYhH9DVkPrZPRjAPy+gHJexSBVvvpJZfb0PX7wFwa63CPKZvB/m828e2XoZE4dZauY8ZVVHeV3+no69OV5tyKR+f5YQqx/mFOVI16BsWjZOe/eTnfE9W9eR4fioq2/MgPkB/mAPDHBjmwD/PATXsBcNekH2C/7AXzBpKzyK9YMeTvoz0gi5++V6wO6AXVA0cR3pB9YJeUCvaCz455Ffkuf+mRU7iXF2J58Rvzxkyc8K4cugAAAAASUVORK5CYII=", true}
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

        [TheoryIgnoreOnLinuxRunnerAttribute]
        [MemberData(nameof(GetBitMapExample))]
        public void ShouldReturnMemoryStreamFromBitmap(Bitmap bitmap, SuportedImageTypes suportedImageTypes, bool shouldFail)
        {
            var image = ImageUtils.BitMapToMemoryStream(bitmap, suportedImageTypes?.ImageFormat);

            if (shouldFail)
                image.Should().BeNull();
            else
                image.Should().NotBeNull();
        }

        [TheoryIgnoreOnLinuxRunnerAttribute]
        [MemberData(nameof(GetBitMapExample))]
        public void ShouldReturnBase64FromBitmap(Bitmap bitmap, SuportedImageTypes suportedImageTypes, bool shouldFail)
        {
            var image = ImageUtils.BitmapToBase64(bitmap, suportedImageTypes?.ImageFormat);

            if (shouldFail)
                image.Should().BeNullOrEmpty();
            else
                image.Should().NotBeNullOrEmpty();
        }

        [TheoryIgnoreOnLinuxRunnerAttribute]
        [MemberData(nameof(GetBase64ToExample))]
        public void ShoudConvertToMemoryStreamTheBase64(string base64, bool shouldConvert)
        {
            var memory = ImageUtils.Base64ToMemoryStream(base64);
            if (shouldConvert)
                memory.Should().NotBeNull();
            else
                memory.Should().BeNull();
        }
    }
}
