using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class ClickExtension
    {
        public static Crawler Click(this Crawler parent, WebElementType webElementType, bool shouldRetry)
        {
            new ClickManager(parent.CreateLogger(ClickManager.LoggerCategory))
                .Click(parent, webElementType, shouldRetry);
            return parent;
        }

        public static Crawler ClickUsingScript(this Crawler parent, WebElementType webElementType, bool shouldRetry)
        {
            new ClickManager(parent.CreateLogger(ClickManager.LoggerCategory))
                .ClickUsingJavaScript(parent, webElementType, shouldRetry);
            return parent;
        }

        public static Crawler ClickFirst(this Crawler parent, WebElementType webElementType, bool shouldRetry)
        {
            new ClickManager(parent.CreateLogger(ClickManager.LoggerCategory))
                .ClickFirst(parent, webElementType, shouldRetry);
            return parent;
        }

        public static Crawler ClickIfTrue(this Crawler parent, WebElementType webElementType, bool condition, bool shouldRetry)
        {
            if (condition)
            {
                new ClickManager(parent.CreateLogger(ClickManager.LoggerCategory))
                    .Click(parent, webElementType, shouldRetry);
            }

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
