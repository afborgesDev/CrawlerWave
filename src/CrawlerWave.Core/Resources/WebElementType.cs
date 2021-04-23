﻿using System.Diagnostics;
using OpenQA.Selenium;

namespace CrawlerWave.Core.Resources
{
    [DebuggerDisplay("ElementType = {ElementType}, Identify = {Identify}, ShouldRetry = {ShouldRetry}")]
    public class WebElementType
    {
        public readonly string Identify;
        public readonly ElementsType ElementType;
        public readonly By ByElement;
        public readonly bool ShouldRetry;

        protected WebElementType(string identify, ElementsType elementType, By seleniumObject, bool shouldRetry = false)
        {
            Identify = identify;
            ElementType = elementType;
            ByElement = seleniumObject;
            ShouldRetry = shouldRetry;
        }

        public static WebElementType Id(string identify, bool shouldRetry = false) => new(identify, ElementsType.Id, By.Id(identify), shouldRetry);

        public static WebElementType Name(string identify, bool shouldRetry = false) => new(identify, ElementsType.Name, By.Name(identify), shouldRetry);

        public static WebElementType TagName(string identify, bool shouldRetry = false) => new(identify, ElementsType.TagName, By.TagName(identify), shouldRetry);

        public static WebElementType ClassName(string identify, bool shouldRetry = false) => new(identify, ElementsType.ClassName, By.ClassName(identify), shouldRetry);

        public static WebElementType CssSelector(string identify, bool shouldRetry = false) => new(identify, ElementsType.CssSelector, By.CssSelector(identify), shouldRetry);

        public static WebElementType LinkText(string identify, bool shouldRetry = false) => new(identify, ElementsType.LinkText, By.LinkText(identify), shouldRetry);

        public static WebElementType PartialLinkText(string identify, bool shouldRetry = false) => new(identify, ElementsType.PartialLinkText, By.PartialLinkText(identify), shouldRetry);

        public static WebElementType XPath(string identify, bool shouldRetry = false) => new(identify, ElementsType.XPath, By.XPath(identify), shouldRetry);
    }
}
