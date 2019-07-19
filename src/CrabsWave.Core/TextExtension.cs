using System.Collections.Generic;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class TextExtension
    {
        public static Crawler ElementInnerText(this Crawler parent, WebElementType webElementType, bool shouldRetry, out string textValue)
        {
            textValue = new TextManager(parent.CreateLogger(TextManager.LoggerCategory))
                            .GetElementInnerText(parent, webElementType, shouldRetry);
            return parent;
        }

        public static Crawler ElementsText(this Crawler parent, WebElementType webElementType, bool shouldRetry, out IList<string> textValue)
        {
            textValue = new TextManager(parent.CreateLogger(TextManager.LoggerCategory))
                            .GetTextFromMultipleElementOcurrences(parent, webElementType, shouldRetry);
            return parent;
        }

        public static Crawler ClearAndSendKeys(this Crawler parent, WebElementType webElementType, string keys, bool shouldRetry)
        {
            new TextManager(parent.CreateLogger(TextManager.LoggerCategory))
                .ClearAndSendKeys(parent, webElementType, keys, shouldRetry);
            return parent;
        }
    }
}
