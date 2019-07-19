using System;
using System.Collections.ObjectModel;
using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace CrabsWave.Core.Functionalities
{
    internal class ElementsManager
    {
        //TODO: Change that for a constructor initialization
        public const string LoggerCategory = "CrawlerWave.ElementsManager";
        public const int DefaultNumberOfAttemptsOnRetry = 5;
        public const int OneAttempt = 1;

        private readonly ILogger Logger;

        public ElementsManager(ILogger logger) => Logger = logger;

        public IWebElement TryGetElement(Crawler parent, WebElementType webElementType, bool shouldRetryIfFail = true)
        {
            var attemps = DefaultNumberOfAttemptsOnRetry;
            if (!shouldRetryIfFail) attemps = OneAttempt;

            IWebElement foundElement;
            for (var i = 1; i <= attemps; i++)
            {
                try
                {
                    foundElement = parent.Driver.FindElement(webElementType.ByElement);
                    if (foundElement != null)
                        return foundElement;
                }
                catch (Exception e)
                {
                    Logger.LogError($"Could not get the element using identify: {webElementType.Identify} and type: {webElementType.ElementType.ToString()} at the attempt: {i}", e);
                }
            }

            return null;
        }

        public ReadOnlyCollection<IWebElement> TryGetElements(Crawler parent, WebElementType webElementType, bool shouldRetryIfFail = true)
        {
            var attemps = DefaultNumberOfAttemptsOnRetry;
            if (!shouldRetryIfFail) attemps = OneAttempt;

            ReadOnlyCollection<IWebElement> elements;
            for (var i = 1; i <= attemps; i++)
            {
                try
                {
                    elements = parent.Driver.FindElements(webElementType.ByElement);
                    if (elements != null)
                        return elements;
                }
                catch (Exception e)
                {
                    Logger.LogError($"Could not get the elements using identify: {webElementType.Identify} and type: {webElementType.ElementType.ToString()} at the attempt: {i}", e);
                }
            }

            return default;
        }

        public string TryGetAttribute(Crawler parent, WebElementType webElementType, string attribute, bool shouldRetryIfFail = true)
        {
            var element = TryGetElement(parent, webElementType, shouldRetryIfFail);
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
                    Logger.LogError($"Could not get the attribute for the element {webElementType.Identify}", e);
                }
            }

            return string.Empty;
        }
    }
}
