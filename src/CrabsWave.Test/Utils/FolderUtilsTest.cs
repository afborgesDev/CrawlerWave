﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using CrabsWave.Core.Validations;
using CrabsWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Utils
{
    public class FolderUtilsTest
    {
        public static IEnumerable<object[]> GetExecutableCombinations() => new List<object[]>() {
            new object[]{ "", "", false },
            new object[]{ ".exe", "", false},
            new object[]{ "", Directory.GetCurrentDirectory(), false},
            new object[]{ ".exe", Directory.GetCurrentDirectory(), false},
        };

        public static IEnumerable<object[]> GetWrongFileNameExamples() => new List<object[]> {
            new object[] { ":!@#!@)(#*$&%", "_!@#!@)(#_$&%" },
            new object[] { "fil:ena:e", "fil_ena_e" },
            new object[] { "filename", "filename" },
            new object[] { "", "" }
        };

        [Fact]
        public void ShouldResultCurrentDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var folderUtilDirectory = FolderUtils.GetAbsolutePath();
            currentDirectory.Should().BeEquivalentTo(folderUtilDirectory);
        }

        [Fact]
        public void ShouldGetFileFromOs()
        {
            var fileName = FolderUtils.GetFileNameFromOs("fileName");
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                fileName.Should().Contain(".exe");
            else
                fileName.Should().NotContain(".exe");
        }

        [Theory]
        [MemberData(nameof(GetExecutableCombinations))]
        public void ShouldCheckExecutableExist(string fileName, string directory, bool shoudExists)
        {
            var fileExists = FolderUtils.SafeCheckExecutableExists(fileName, directory);
            fileExists.Should().Be(shoudExists);
        }

        [Fact]
        public void ShouldTestWebDriverAvaliable()
        {
            var fileName = SeleniumDependencies.GetWebDriverPathAvaliable();
            fileName.Should().NotBeNullOrWhiteSpace();
        }
        
        [Theory]
        [MemberData(nameof(GetWrongFileNameExamples))]
        public void ShouldValidateFileName(string wrongFileName, string expectedValue)
        {
            var fileName = FolderUtils.ReplaceInvalidFileNameChars(wrongFileName);
            fileName.Should().BeEquivalentTo(expectedValue);
        }
       
    }
}
