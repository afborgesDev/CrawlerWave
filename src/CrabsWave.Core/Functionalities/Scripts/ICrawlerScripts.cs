namespace CrabsWave.Core.Functionalities.Scripts
{
    public interface ICrawlerScripts
    {
        Crawler ExecuteScript(string script);
        Crawler ExecuteScript(string script, params object[] args);
        Crawler ExecuteAndTakeResult(string script, out string result);
    }
}
