using CrabsWave.Core.Functionalities;

namespace CrabsWave.Core
{
    public static class TextExtension
    {
        public static Crawler ElementTextById(this Crawler parent, string identify, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, Resources.ElementsType.Id);
            return parent;
        }

        public static Crawler ElementTextByName(this Crawler parent, string identify, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, Resources.ElementsType.Name);
            return parent;
        }
        public static Crawler ElementTextByTagName(this Crawler parent, string identify, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, Resources.ElementsType.TagName);
            return parent;
        }
        public static Crawler ElementTextByClassName(this Crawler parent, string identify, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, Resources.ElementsType.ClassName);
            return parent;
        }
        public static Crawler ElementTextByCssSelector(this Crawler parent, string identify, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, Resources.ElementsType.CssSelector);
            return parent;
        }
        public static Crawler ElementTextByLinkText(this Crawler parent, string identify, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, Resources.ElementsType.LinkText);
            return parent;
        }
        public static Crawler ElementTextByPartialLinkText(this Crawler parent, string identify, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, Resources.ElementsType.PartialLinkText);
            return parent;
        }
        public static Crawler ElementTextByXPath(this Crawler parent, string identify, out string textValue)
        {
            textValue = TextManager.GetElementText(parent.Driver, identify, Resources.ElementsType.XPath);
            return parent;
        }
    }
}
