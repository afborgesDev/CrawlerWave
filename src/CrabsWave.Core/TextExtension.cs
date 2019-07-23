using System.Collections.Generic;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class TextExtension
    {
        public static Crawler ElementInnerText(this Crawler parent, WebElementType webElementType, out string textValue)
        {
            textValue = TextManager.New(parent).GetElementInnerText(parent, webElementType);
            return parent;
        }

        public static Crawler ElementsText(this Crawler parent, WebElementType webElementType, out IList<string> textValue)
        {
            textValue = TextManager.New(parent).GetTextFromMultipleElementOcurrences(parent, webElementType);
            return parent;
        }

        public static Crawler ClearAndSendKeys(this Crawler parent, WebElementType webElementType, string keys)
        {
            TextManager.New(parent).ClearAndSendKeys(parent, webElementType, keys);
            return parent;
        }
    }
}
