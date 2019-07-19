using System;
using System.Collections.Generic;
using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Core.Functionalities
{
    internal class TextManager : BaseManager
    {
        private const string AttributeText = "innerText";

        public TextManager(ILogger logger) : base("CrawlerWave.TextManager", logger)
        {
        }

        public string GetElementInnerText(Crawler parent, WebElementType webElementType, bool shouldRetry = true) =>
            new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                .TryGetAttribute(parent, webElementType, AttributeText, shouldRetry);

        public IList<string> GetTextFromMultipleElementOcurrences(Crawler parent, WebElementType webElementType, bool shouldRetry = true)
        {
            var items = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                            .TryGetElements(parent, webElementType, shouldRetry);
            if (items == null || items.Count <= 0) return default;

            var returnList = new List<string>(items.Count);
            foreach (var item in items)
                returnList.Add(item.Text);

            return returnList;
        }

        public void ClearAndSendKeys(Crawler parent, WebElementType webElementType, string textToSend, bool shouldRetry = true)
        {
            var item = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                           .TryGetElement(parent, webElementType, shouldRetry);
            try
            {
                item.Clear();
                item.SendKeys(textToSend);
            }
            catch (Exception e)
            {
                Logger.LogError($"Cuold not clear and send key for element {webElementType.Identify}", e);
            }
        }
    }
}
