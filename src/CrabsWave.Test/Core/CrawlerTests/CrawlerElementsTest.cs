using System;
using System.Collections.Generic;
using System.Text;
using CrabsWave.Core;
using CrabsWave.Core.resources;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace CrabsWave.Test.Core.CrawlerTests
{
    public class CrawlerElementsTest
    {
        private Mock<ILogger<ICrawler>> logmoq = new Mock<ILogger<ICrawler>>();
        private const string GoogleUrlBase = "https://www.google.com.br";

        [Theory]
        [MemberData(nameof(GetElementToFind))]
        public void ShouldFindObject(string url, string identify, ElementsType elementType)
        {
            
            using (var sut = CreateAndInitialize())
            {
                IWebElement element = null;
                sut.Navigation().GoToUrl(url, out _);
                
                switch (elementType)
                {
                    case ElementsType.Id:
                        sut.Elements().GetElementById(identify, out element);
                        break;
                    case ElementsType.Name:
                        sut.Elements().GetElementByName(identify, out element);
                        break;
                    case ElementsType.TagName:
                        break;
                    case ElementsType.ClassName:
                        break;
                    case ElementsType.CssSelector:
                        sut.Elements().GetElementByCssSelector(identify, out element);
                        break;
                    case ElementsType.LinkText:
                        break;
                    case ElementsType.PartialLinkText:
                        break;
                    case ElementsType.XPath:
                        sut.Elements().GetElementByXPath(identify, out element);
                        break;
                }

                element.Should().NotBeNull();
            }
        }

        private ICrawler CreateAndInitialize()
        {
            var crawler = new Crawler(logmoq.Object);
            crawler.Initializate(new CrabsWave.Core.Configurations.Behavior());
            return crawler;
        }

        public static IEnumerable<object[]> GetElementToFind() => new List<object[]> {
            new object[] { GoogleUrlBase, "gLFyf", ElementsType.CssSelector },
            new object[] { GoogleUrlBase, "tsf", ElementsType.Id},
            new object[] { GoogleUrlBase, "q", ElementsType.Name},
            new object[] { GoogleUrlBase, "//*[@id='tsf']", ElementsType.XPath}
        };
    }
}
