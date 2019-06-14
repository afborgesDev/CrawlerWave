using System.Collections.Generic;
using CrabsWave.Core;
using CrabsWave.Core.Resources;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerElementsTest
    {
        private readonly Mock<ILogger<Crawler>> logmoq = new Mock<ILogger<Crawler>>();
        private const string GoogleUrlBase = "https://www.google.com.br";

        [Theory]
        [MemberData(nameof(GetElementToFind))]
        public void ShouldFindObject(string url, string identify, ElementsType elementType)
        {

            using (var sut = CreateAndInitialize())
            {
                IWebElement element = null;
                
                sut.GoToUrl(url, out _);

                switch (elementType)
                {
                    case ElementsType.Id:
                        sut.GetElementById(identify, out element);
                        break;
                    case ElementsType.Name:
                        sut.GetElementByName(identify, out element);
                        break;
                    case ElementsType.TagName:
                        sut.GetElementByTagName(identify, out element);
                        break;
                    case ElementsType.ClassName:
                        sut.GetElementByClassName(identify, out element);
                        break;
                    case ElementsType.CssSelector:
                        sut.GetElementByCssSelector(identify, out element);
                        break;
                    case ElementsType.LinkText:
                        sut.GetElementByLinkText(identify, out element);
                        break;
                    case ElementsType.PartialLinkText:
                        sut.GetElementByPartialText(identify, out element);
                        break;
                    case ElementsType.XPath:
                        sut.GetElementByXPath(identify, out element);
                        break;
                }

                element.Should().NotBeNull();
            }
        }

        private Crawler CreateAndInitialize()
        {
            var crawler = new Crawler(logmoq.Object);
            crawler.Initializate(new CrabsWave.Core.Configurations.Behavior());
            return crawler;
        }

        public static IEnumerable<object[]> GetElementToFind() => new List<object[]> {
            new object[] { GoogleUrlBase, "input", ElementsType.CssSelector },
            new object[] { GoogleUrlBase, "tsf", ElementsType.Id},
            new object[] { GoogleUrlBase, "q", ElementsType.Name},
            new object[] { GoogleUrlBase, "//*[@id='tsf']", ElementsType.XPath},
            new object[] { GoogleUrlBase, "FORM", ElementsType.TagName},
            new object[] { GoogleUrlBase, "SDkEP", ElementsType.ClassName},
        };
    }
}
