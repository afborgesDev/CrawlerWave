using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class SelectsExtension
    {
        public static Crawler SelectByText(this Crawler parent, string identify, ElementsType elementsType, string textToSelect, bool shouldRetry)
        {
            new SelectManager(parent.CreateLogger(SelectManager.LoggerCategory))
                .SelectByText(parent, identify, elementsType, textToSelect, shouldRetry);
            return parent;
        }

        public static Crawler SelectByValue(this Crawler parent, string identify, ElementsType elementsType, string textToSelect, bool shouldRetry)
        {
            new SelectManager(parent.CreateLogger(SelectManager.LoggerCategory))
                .SelectByValue(parent, identify, elementsType, textToSelect, shouldRetry);
            return parent;
        }

        public static Crawler SelectByIndex(this Crawler parent, string identify, ElementsType elementsType, int indexToSelect, bool shouldRetry)
        {
            new SelectManager(parent.CreateLogger(SelectManager.LoggerCategory))
                .SelectByIndex(parent, identify, elementsType, indexToSelect, shouldRetry);
            return parent;
        }
    }
}
