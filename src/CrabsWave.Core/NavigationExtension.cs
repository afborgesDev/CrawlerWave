using CrabsWave.Core.Functionalities;

namespace CrabsWave.Core
{
    public static class NavigationExtension
    {
        public static Crawler GoToUrl(this Crawler parent, string url, out string errorMessage)
        {
            errorMessage = NavegationManager.GoToUrl(parent.Driver, url);
            return parent;
        }

        public static Crawler NavigateBack(this Crawler parent)
        {
            NavegationManager.NavigateBack(parent.Driver);
            return parent;
        }

        public static Crawler GetCurrentUrl(this Crawler parent, out string url)
        {
            url = NavegationManager.GetCurrentUrl(parent.Driver);
            return parent;
        }

        public static Crawler RefreshPage(this Crawler parent)
        {
            NavegationManager.RefreshPage(parent.Driver);
            return parent;
        }
    }
}
