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

        public static TextManager New(Crawler parent) => new TextManager(parent.CreateLogger(LoggerCategory));

        public string GetElementInnerText(Crawler parent, WebElementType webElementType) =>
            ElementsManager.New(parent).TryGetAttribute(parent, webElementType, AttributeText);

        public IList<string> GetTextFromMultipleElementOcurrences(Crawler parent, WebElementType webElementType)
        {
            var items = ElementsManager.New(parent).TryGetElements(parent, webElementType);
            if (items == null || items.Count <= 0) return default;

            var returnList = new List<string>(items.Count);
            foreach (var item in items)
                returnList.Add(item.Text);

            return returnList;
        }

        public void ClearAndSendKeys(Crawler parent, WebElementType webElementType, string textToSend)
        {
            var item = ElementsManager.New(parent).TryGetElement(parent, webElementType);

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
