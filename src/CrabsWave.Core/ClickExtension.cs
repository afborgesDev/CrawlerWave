using System;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class ClickExtension
    {
        public static Crawler ById(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.Id);
            return parent;
        }

        public static Crawler ByName(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.Name);
            return parent;
        }

        public static Crawler ByTagName(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.TagName);
            return parent;
        }

        public static Crawler ByClassName(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.ClassName);
            return parent;
        }

        public static Crawler ByCssSelector(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.CssSelector);
            return parent;
        }

        public static Crawler ByLinkText(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.LinkText);
            return parent;
        }

        public static Crawler ByPartialLinkText(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.PartialLinkText);
            return parent;
        }

        public static Crawler ByXPath(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.XPath);
            return parent;
        }

        public static Crawler ByIdUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ByNameUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ByTagNameUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ByClassNameUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ByCssSelectorUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ByLinkTextUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ByPartialLinkTextUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ByXPathUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler FirstById(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.Id);
            return parent;
        }

        public static Crawler FirstByName(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.Name);
            return parent;
        }

        public static Crawler FirstByTagName(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.TagName);
            return parent;
        }

        public static Crawler FirstByClassName(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.ClassName);
            return parent;
        }

        public static Crawler FirstByCssSelector(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.CssSelector);
            return parent;
        }

        public static Crawler FirstByLinkText(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.LinkText);
            return parent;
        }

        public static Crawler FirstByPartialLinkText(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.PartialLinkText);
            return parent;
        }

        public static Crawler FirstByXPath(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.XPath);
            return parent;
        }

        public static Crawler ByIdIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.Id);
            return parent;
        }

        public static Crawler ByNameIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.Name);

            return parent;
        }

        public static Crawler ByTagNameIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.TagName);

            return parent;
        }

        public static Crawler ByClassNameIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.ClassName);

            return parent;
        }

        public static Crawler ByCssSelectorIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.CssSelector);

            return parent;
        }

        public static Crawler ByLinkTextIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.LinkText);

            return parent;
        }

        public static Crawler ByPartialLinkTextIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.PartialLinkText);

            return parent;
        }

        public static Crawler ByXPathIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.XPath);

            return parent;
        }

        public static Crawler ClickAlert(this Crawler parent, bool acept)
        {
            if (acept)
                parent.Driver.SwitchTo().Alert().Accept();
            else
                parent.Driver.SwitchTo().Alert().Dismiss();

            return parent;
        }
    }
}
