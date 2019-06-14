using System;
using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class ClickExtension
    {
        public static Crawler ClickById(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.Id);
            return parent;
        }

        public static Crawler ClickByName(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.Name);
            return parent;
        }

        public static Crawler ClickByTagName(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.TagName);
            return parent;
        }

        public static Crawler ClickByClassName(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.ClassName);
            return parent;
        }

        public static Crawler ClickByCssSelector(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.CssSelector);
            return parent;
        }

        public static Crawler ClickByLinkText(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.LinkText);
            return parent;
        }

        public static Crawler ClickByPartialLinkText(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.PartialLinkText);
            return parent;
        }

        public static Crawler ClickByXPath(this Crawler parent, string identify)
        {
            ClickManager.Click(parent.Driver, identify, ElementsType.XPath);
            return parent;
        }

        public static Crawler ClickByIdUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ClickByNameUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ClickByTagNameUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ClickByClassNameUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ClickByCssSelectorUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ClickByLinkTextUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ClickByPartialLinkTextUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ClickByXPathUsingScript(this Crawler parent, string identify)
        {
            throw new NotImplementedException();
            return parent;
        }

        public static Crawler ClickFirstById(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.Id);
            return parent;
        }

        public static Crawler ClickFirstByName(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.Name);
            return parent;
        }

        public static Crawler ClickFirstByTagName(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.TagName);
            return parent;
        }

        public static Crawler ClickFirstByClassName(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.ClassName);
            return parent;
        }

        public static Crawler ClickFirstByCssSelector(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.CssSelector);
            return parent;
        }

        public static Crawler ClickFirstByLinkText(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.LinkText);
            return parent;
        }

        public static Crawler ClickFirstByPartialLinkText(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.PartialLinkText);
            return parent;
        }

        public static Crawler ClickFirstByXPath(this Crawler parent, string identify)
        {
            ClickManager.ClickFirst(parent.Driver, identify, ElementsType.XPath);
            return parent;
        }

        public static Crawler ClickByIdIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.Id);
            return parent;
        }

        public static Crawler ClickByNameIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.Name);

            return parent;
        }

        public static Crawler ClickByTagNameIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.TagName);

            return parent;
        }

        public static Crawler ClickByClassNameIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.ClassName);

            return parent;
        }

        public static Crawler ClickByCssSelectorIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.CssSelector);

            return parent;
        }

        public static Crawler ClickByLinkTextIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.LinkText);

            return parent;
        }

        public static Crawler ClickByPartialLinkTextIfTrue(this Crawler parent, string identify, bool condition)
        {
            if (condition)
                ClickManager.Click(parent.Driver, identify, ElementsType.PartialLinkText);

            return parent;
        }

        public static Crawler ClickByXPathIfTrue(this Crawler parent, string identify, bool condition)
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
