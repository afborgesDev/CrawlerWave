using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities.Elements
{
    public interface ICrawlerElements
    {
        ICrawler GetElementById(string identify, out IWebElement webElement);
        ICrawler GetElementByName(string identify, out IWebElement webElement);
        ICrawler GetElementByTagName(string identify, out IWebElement webElement);
        ICrawler GetElementByCssSelector(string identify, out IWebElement webElement);
        ICrawler GetElementByLinkText(string identify, out IWebElement webElement);
        ICrawler GetElementByPartialText(string identify, out IWebElement webElement);
        ICrawler GetElementByXPath(string identify, out IWebElement webElement);
    }
}
