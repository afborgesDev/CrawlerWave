using CrabsWave.Core.Functionalities;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core
{
    public static class ClickExtension
    {
        public static Crawler Click(this Crawler parent, string identify, ElementsType elementsType, bool shouldRetry)
        {
            parent.RestoreLog();
            ClickManager.Click(parent.Driver, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler ClickUsingScript(this Crawler parent, string identify, ElementsType elementsType, bool shouldRetry)
        {
            parent.RestoreLog();
            ClickManager.ClickUsingJavaScript(parent.Driver, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler ClickFirst(this Crawler parent, string identify, ElementsType elementsType, bool shouldRetry)
        {
            parent.RestoreLog();
            ClickManager.ClickFirst(parent.Driver, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler ClickIfTrue(this Crawler parent, string identify, bool condition, ElementsType elementsType, bool shouldRetry)
        {
            parent.RestoreLog();
            if (condition)
                ClickManager.Click(parent.Driver, identify, elementsType, shouldRetry);
            return parent;
        }

        public static Crawler ClickAlert(this Crawler parent, bool acept)
        {
            parent.RestoreLog();
            if (acept)
                parent.Driver.SwitchTo().Alert().Accept();
            else
                parent.Driver.SwitchTo().Alert().Dismiss();

            return parent;
        }
    }
}
