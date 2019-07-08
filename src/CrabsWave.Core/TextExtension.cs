using System.Collections.Generic;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class TextExtension
    {
        public static Crawler ElementInnerText(this Crawler parent, string identify, ElementsType elementsType, out string textValue)
        {
            parent.RestoreLog();
            textValue = TextManager.GetElementInnerText(parent.Driver, identify, elementsType);
            return parent;
        }

        public static Crawler ElementsText(this Crawler parent, string identify, ElementsType elementsType, out IList<string> textValue)
        {
            parent.RestoreLog();
            textValue = TextManager.GetTextFromMultipleElementOcurrences(parent.Driver, identify, elementsType);
            return parent;
        }

        public static Crawler ClearAndSendKeys(this Crawler parent, string identify, ElementsType elementsType, string keys)
        {
            parent.RestoreLog();
            TextManager.ClearAndSendKeys(parent.Driver, identify, elementsType, keys);
            return parent;
        }
    }
}
