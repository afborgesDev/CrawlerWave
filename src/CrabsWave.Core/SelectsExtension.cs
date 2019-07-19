using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class SelectsExtension
    {
        public static Crawler SelectByText(this Crawler parent, WebElementType webElementType, string textToSelect, bool shouldRetry)
        {
            new SelectManager(parent.CreateLogger(SelectManager.LoggerCategory))
                .SelectByText(parent, webElementType, textToSelect, shouldRetry);
            return parent;
        }

        public static Crawler SelectByValue(this Crawler parent, WebElementType webElementType, string textToSelect, bool shouldRetry)
        {
            new SelectManager(parent.CreateLogger(SelectManager.LoggerCategory))
                .SelectByValue(parent, webElementType, textToSelect, shouldRetry);
            return parent;
        }

        public static Crawler SelectByIndex(this Crawler parent, WebElementType webElementType, int indexToSelect, bool shouldRetry)
        {
            new SelectManager(parent.CreateLogger(SelectManager.LoggerCategory))
                .SelectByIndex(parent, webElementType, indexToSelect, shouldRetry);
            return parent;
        }
    }
}
