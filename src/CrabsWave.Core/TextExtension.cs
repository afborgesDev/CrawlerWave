using System.Collections.Generic;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class TextExtension
    {
        public static Crawler ElementText(this Crawler parent, string identify, ElementsType elementsType, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, elementsType);
            return parent;
        }

        public static Crawler ElementsText(this Crawler parent, string identify, ElementsType elementsType, out IList<string> textValue)
        {
            textValue = TextManager.GetTextFromMultipleElementOcurrences(parent.Driver, identify, elementsType);
            return parent;
        }

        public static Crawler ClearAndSendKeys(this Crawler parent, string identify, ElementsType elementsType, string keys)
        {
            TextManager.ClearAndSendKeys(parent.Driver, identify, elementsType, keys);
            return parent;
        }
    }
}
