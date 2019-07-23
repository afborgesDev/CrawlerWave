using CrabsWave.Core.Functionalities;

namespace CrabsWave.Core
{
    public static class NavigationExtension
    {
        public static Crawler GoToUrl(this Crawler parent, string url, out string errorMessage)
        {
            errorMessage = NavegationManager.New(parent).GoToUrl(parent, url);
            return parent;
        }

        public static Crawler NavigateBack(this Crawler parent)
        {
            NavegationManager.New(parent).NavigateBack(parent);
            return parent;
        }

        public static Crawler GetCurrentUrl(this Crawler parent, out string url)
        {
            url = NavegationManager.New(parent).GetCurrentUrl(parent);
            return parent;
        }

        public static Crawler RefreshPage(this Crawler parent)
        {
            NavegationManager.New(parent).RefreshPage(parent);
            return parent;
        }
    }
}
