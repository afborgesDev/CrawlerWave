using OpenQA.Selenium;

namespace CrabsWave.Core.Resources
{
    //Change to supporte the ShouldRetry here
    public class WebElementType
    {
        public readonly string Identify;
        public readonly ElementsType ElementType;
        public readonly By ByElement;

        protected WebElementType(string identify, ElementsType elementType, By seleniumObject)
        {
            Identify = identify;
            ElementType = elementType;
            ByElement = seleniumObject;
        }

        public static WebElementType Id(string identify) => new WebElementType(identify, ElementsType.Id, By.Id(identify));
        public static WebElementType Name(string identify) => new WebElementType(identify, ElementsType.Name, By.Name(identify));
        public static WebElementType TagName(string identify) => new WebElementType(identify, ElementsType.TagName, By.TagName(identify));
        public static WebElementType ClassName(string identify) => new WebElementType(identify, ElementsType.ClassName, By.ClassName(identify));
        public static WebElementType CssSelector(string identify) => new WebElementType(identify, ElementsType.CssSelector, By.CssSelector(identify));
        public static WebElementType LinkText(string identify) => new WebElementType(identify, ElementsType.LinkText, By.LinkText(identify));
        public static WebElementType PartialLinkText(string identify) => new WebElementType(identify, ElementsType.PartialLinkText, By.PartialLinkText(identify));
        public static WebElementType XPath(string identify) => new WebElementType(identify, ElementsType.XPath, By.XPath(identify));
    }
}
