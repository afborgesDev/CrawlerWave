using CrawlerWave.Core.Functionalities;
using CrawlerWave.Core.Resources;

namespace CrawlerWave.Core
{
    public static class SelectsExtension
    {
        public static Crawler SelectByText(this Crawler parent, WebElementType webElementType, string textToSelect)
        {
            SelectManager.New(parent).SelectByText(parent, webElementType, textToSelect);
            return parent;
        }

        public static Crawler SelectByValue(this Crawler parent, WebElementType webElementType, string textToSelect)
        {
            SelectManager.New(parent).SelectByValue(parent, webElementType, textToSelect);
            return parent;
        }

        public static Crawler SelectByIndex(this Crawler parent, WebElementType webElementType, int indexToSelect)
        {
            SelectManager.New(parent).SelectByIndex(parent, webElementType, indexToSelect);
            return parent;
        }
    }
}
