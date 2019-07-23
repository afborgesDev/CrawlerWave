using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class ClickExtension
    {
        public static Crawler Click(this Crawler parent, WebElementType webElementType)
        {
            ClickManager.New(parent).Click(parent, webElementType);
            return parent;
        }

        public static Crawler ClickUsingScript(this Crawler parent, WebElementType webElementType)
        {
            ClickManager.New(parent).ClickUsingJavaScript(parent, webElementType);
            return parent;
        }

        public static Crawler ClickFirst(this Crawler parent, WebElementType webElementType)
        {
            ClickManager.New(parent).ClickFirst(parent, webElementType);
            return parent;
        }

        public static Crawler ClickIfTrue(this Crawler parent, WebElementType webElementType, bool condition)
        {
            if (condition)
                ClickManager.New(parent).Click(parent, webElementType);

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
