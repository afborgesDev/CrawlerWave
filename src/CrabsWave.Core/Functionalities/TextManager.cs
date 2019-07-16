using System;
using System.Collections.Generic;
using CrabsWave.Core.Resources;
using Microsoft.Extensions.Logging;

namespace CrabsWave.Core.Functionalities
{
    internal class TextManager
    {
        public const string LoggerCategory = "CrawlerWave.TextManager";
        private const string AttributeText = "innerText";
        private readonly ILogger Logger;

        public TextManager(ILogger logger) => Logger = logger;

        public string GetElementInnerText(Crawler parent, string identify, ElementsType elementsType, bool shouldRetry = true) =>
            new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                .TryGetAttribute(parent, identify, elementsType, AttributeText, shouldRetry);

        public IList<string> GetTextFromMultipleElementOcurrences(Crawler parent, string identify, ElementsType elementsType, bool shouldRetry = true)
        {
            var items = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                            .TryGetElements(parent, identify, elementsType, shouldRetry);
            if (items == null || items.Count <= 0) return default;

            var returnList = new List<string>(items.Count);
            foreach (var item in items)
                returnList.Add(item.Text);

            return returnList;
        }

        public void ClearAndSendKeys(Crawler parent, string identify, ElementsType elementsType, string textToSend, bool shouldRetry = true)
        {
            var item = new ElementsManager(parent.CreateLogger(ElementsManager.LoggerCategory))
                           .TryGetElement(parent, identify, elementsType, shouldRetry);
            try
            {
                item.Clear();
                item.SendKeys(textToSend);
            }
            catch (Exception e)
            {
                Logger.LogError($"Cuold not clear and send key for element {identify}", e);
            }
        }
    }
}
