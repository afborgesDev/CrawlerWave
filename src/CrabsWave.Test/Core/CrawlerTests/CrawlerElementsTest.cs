using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        [Theory]
        [MemberData(nameof(GetElementToFind))]
        public void ShouldGetElements(string url, string identify, ElementsType elementType)
        {
            using (var sut = CreateAndInitialize())
            {
                ReadOnlyCollection<IWebElement> elements = null;

                sut.GoToUrl(url, out _);

                switch (elementType)
                {
                    case ElementsType.Id:
                        sut.GetElementsById(identify, out elements);
                        break;
                    case ElementsType.Name:
                        sut.GetElementsByName(identify, out elements);
                        break;
                    case ElementsType.TagName:
                        sut.GetElementsByTagName(identify, out elements);
                        break;
                    case ElementsType.ClassName:
                        sut.GetElementsByClassName(identify, out elements);                            
                        break;
                    case ElementsType.CssSelector:
                        sut.GetElementsByCssSelector(identify, out elements);
                        break;
                    case ElementsType.LinkText:
                        sut.GetElementsByLinkText(identify, out elements);
                        break;
                    case ElementsType.PartialLinkText:
                        sut.GetElementsByPartialText(identify, out elements);
                        break;
                    case ElementsType.XPath:
                        sut.GetElementsByXPath(identify, out elements);
                        break;
                }

                elements.Should().NotBeNull();
                elements.Should().NotBeEmpty();
            }
        }

        [Fact]
        public void ShouldGetElementAttribute()
        {
            var logMoq = new Mock<ILogger<Crawler>>();
            using (var sut = new Crawler(logMoq.Object))
            {
                sut.Initializate(new CrabsWave.Core.Configurations.Behavior())
                   .GoToUrl("https://www.google.com", out _)
                   .GetElementAttributeByName("q", "title", out var title);

                title.Should().NotBeNullOrWhiteSpace();
            }
        }

        private Crawler CreateAndInitialize()
        {
            var crawler = new Crawler(logmoq.Object);
            crawler.Initializate(new CrabsWave.Core.Configurations.Behavior());
            return crawler;
        }

        public static IEnumerable<object[]> GetElementToFind()
        {
            var url = $"file:///{PageForUnitTestHelper.GetPageForUniTestFilePath()}";
            return new List<object[]> {
                new object[] { url, "//*[@id='ButtonsToXPath']", ElementsType.XPath },
                new object[] { url, "inputName", ElementsType.Name },
                new object[] { url, "INPUT", ElementsType.TagName},
                new object[] { url, "body > a", ElementsType.CssSelector},
                new object[] { url, "btnOne", ElementsType.Id},
                new object[] { url, "someClass", ElementsType.ClassName },
                new object[] { url, "click to increment", ElementsType.PartialLinkText},
                new object[] { url, "click to increment" , ElementsType.LinkText}
            };
        }
    }
}
