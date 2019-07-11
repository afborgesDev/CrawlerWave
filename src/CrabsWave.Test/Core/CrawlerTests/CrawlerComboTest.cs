using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using CrabsWave.Test.TestHelpers;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerComboTest
    {
        private static readonly string LocalUrl = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";
        private readonly ITestOutputHelper testOutput;

        public CrawlerComboTest(ITestOutputHelper testOutput) => this.testOutput = testOutput;

        public static IEnumerable<object[]> GetSelectOptionsToIdentify => new List<object[]> {
            new object[] { "select1", ElementsType.Id, "First Value", "first", false, false, string.Empty } ,
            new object[] { "select1", ElementsType.Name, "First Value", "first", false, false, string.Empty } ,
            new object[] { "select1", ElementsType.ClassName, "First Value", "first", false, false, string.Empty } ,
            new object[] { "select1", ElementsType.Id, "invalid 123", "-1", false, true, "Could not select element: select1 by using the text: invalid 123" } ,
            new object[] { "#$select1%¨&", ElementsType.Id, "invalid 123", "", false, true, "Could not find a select with the identify: #$select1%¨&" } ,
        };

        [Theory]
        [MemberData(nameof(GetSelectOptionsToIdentify))]
        public void ShouldSelectByText(string identify, ElementsType elementsType, string textToSelect,
            string expectedValue, bool shouldRetry, bool shouldFail, string errorMessage)
        {
            var (logMoq, logOutPut) = TestLoggerBuilder.Create<Crawler>();
            using (var sut = new Crawler(logMoq))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl(LocalUrl, out _)
                   .SelectByText(identify, elementsType, textToSelect, shouldRetry)
                   .GetElementAttribute(identify, elementsType, "value", shouldRetry, out var value);

                testOutput.WriteLine(logOutPut.Output);
                value.Should().Be(expectedValue);

                if (shouldFail)
                    logOutPut.Output.Should().Contain(errorMessage);
            }
        }
    }
}
