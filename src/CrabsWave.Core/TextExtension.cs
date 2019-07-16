using System.Collections.Generic;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class TextExtension
    {
        public static Crawler ElementInnerText(this Crawler parent, string identify, ElementsType elementsType, bool shouldRetry, out string textValue)
        {
            textValue = new TextManager(parent.CreateLogger(TextManager.LoggerCategory))
                            .GetElementInnerText(parent, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler ElementsText(this Crawler parent, string identify, ElementsType elementsType, bool shouldRetry, out IList<string> textValue)
        {
            textValue = new TextManager(parent.CreateLogger(TextManager.LoggerCategory))
                            .GetTextFromMultipleElementOcurrences(parent, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler ClearAndSendKeys(this Crawler parent, string identify, ElementsType elementsType, string keys, bool shouldRetry)
        {
            new TextManager(parent.CreateLogger(TextManager.LoggerCategory))
                .ClearAndSendKeys(parent, identify, elementsType, keys, shouldRetry);
            return parent;
        }
    }
}
