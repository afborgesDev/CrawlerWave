namespace CrabsWave.Core.Navegation
{
    public interface ICrawlerNavigation
    {
        ICrawler GoToUrl(string url, out string errorMessage);
    }
}
