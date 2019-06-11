namespace CrabsWave.Core.Functionalities.Scripts
{
    public interface ICrawlerScripts
    {
        ICrawler ExecuteScript(string script);
        ICrawler ExecuteScript(string script, params object[] args);
        ICrawler ExecuteAndTakeResult(string script, out string result);
    }
}
