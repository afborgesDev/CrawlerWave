using System;
using System.Collections.ObjectModel;
using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities
{
    internal class ElementsManager
    {
        public const string LoggerCategory = "CrawlerWave.ElementsManager";
        public const int DefaultNumberOfAttemptsOnRetry = 5;
        public const int OneAttempt = 1;

        private readonly ILogger Logger;

        public ElementsManager(ILogger logger) => Logger = logger;

        public By CreateElementBy(string identify, ElementsType elementsType)
        {
            switch (elementsType)
            {
                case ElementsType.Id:
                    return By.Id(identify);

                case ElementsType.Name:
                    return By.Name(identify);

                case ElementsType.TagName:
                    return By.TagName(identify);

                case ElementsType.ClassName:
                    return By.ClassName(identify);

                case ElementsType.CssSelector:
                    return By.CssSelector(identify);

                case ElementsType.LinkText:
                    return By.LinkText(identify);

                case ElementsType.PartialLinkText:
                    return By.PartialLinkText(identify);

                default:
                    return By.XPath(identify);
            }
        }

        public IWebElement TryGetElement(Crawler parent, string elementIdentify, ElementsType elementsType, bool shouldRetryIfFail = true)
        {
            var attemps = DefaultNumberOfAttemptsOnRetry;
            if (!shouldRetryIfFail) attemps = OneAttempt;

            var element = CreateElementBy(elementIdentify, elementsType);
            IWebElement foundElement;
            for (var i = 1; i <= attemps; i++)
            {
                try
                {
                    foundElement = parent.Driver.FindElement(element);
                    if (foundElement != null)
                        return foundElement;
                }
                catch (Exception e)
                {
                    Logger.LogError($"Could not get the element using identify: {elementIdentify} and type: {elementsType.ToString()} at the attempt: {i}", e);
                }
            }

            return null;
        }

        public ReadOnlyCollection<IWebElement> TryGetElements(Crawler parent, string elementIdentify, ElementsType elementsType, bool shouldRetryIfFail = true)
        {
            var attemps = DefaultNumberOfAttemptsOnRetry;
            if (!shouldRetryIfFail) attemps = OneAttempt;

            var element = CreateElementBy(elementIdentify, elementsType);
            ReadOnlyCollection<IWebElement> elements;
            for (var i = 1; i <= attemps; i++)
            {
                try
                {
                    elements = parent.Driver.FindElements(element);
                    if (elements != null)
                        return elements;
                }
                catch (Exception e)
                {
                    Logger.LogError($"Could not get the elements using identify: {elementIdentify} and type: {elementsType.ToString()} at the attempt: {i}", e);
                }
            }

            return default;
        }

        public string TryGetAttribute(Crawler parent, string elementIdentify, ElementsType elementsType, string attribute, bool shouldRetryIfFail = true)
        {
            var element = TryGetElement(parent, elementIdentify, elementsType, shouldRetryIfFail);
            if (element == null) return string.Empty;
            var attemps = DefaultNumberOfAttemptsOnRetry;
            if (!shouldRetryIfFail) attemps = OneAttempt;

            for (var i = 1; i <= attemps; i++)
            {
                try
                {
                    return element.GetAttribute(attribute);
                }
                catch (Exception e)
                {
                    Logger.LogError($"Could not get the attribute for the element {elementIdentify}", e);
                }
            }

            return string.Empty;
        }
    }
}
