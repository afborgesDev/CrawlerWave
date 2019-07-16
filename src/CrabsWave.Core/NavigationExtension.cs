using CrabsWave.Core.Functionalities;

namespace CrabsWave.Core
{
    public static class NavigationExtension
    {
        public static Crawler GoToUrl(this Crawler parent, string url, out string errorMessage)
        {
            errorMessage = new NavegationManager(parent.CreateLogger(NavegationManager.LoggerCategory))
                              .GoToUrl(parent, url);
            return parent;
        }

        public static Crawler NavigateBack(this Crawler parent)
        {
            new NavegationManager(parent.CreateLogger(NavegationManager.LoggerCategory))
                .NavigateBack(parent);
            return parent;
        }

        public static Crawler GetCurrentUrl(this Crawler parent, out string url)
        {
            url = new NavegationManager(parent.CreateLogger(NavegationManager.LoggerCategory))
                  .GetCurrentUrl(parent);
            return parent;
        }

        public static Crawler RefreshPage(this Crawler parent)
        {
            new NavegationManager(parent.CreateLogger(NavegationManager.LoggerCategory))
                .RefreshPage(parent);
            return parent;
        }
    }
}
