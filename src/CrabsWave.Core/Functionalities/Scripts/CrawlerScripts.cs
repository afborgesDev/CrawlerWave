using CrabsWave.Core.Functionalities.Base;

namespace CrabsWave.Core.Functionalities.Scripts
{
    public class CrawlerScripts : BaseForFunctionalityClasses, ICrawlerScripts
    {
        public CrawlerScripts(Crawler crawler, OpenQA.Selenium.IWebDriver driver) : base(crawler, driver)
        {
        }

        public Crawler ExecuteAndTakeResult(string script, out string result)
        {
            result = ScriptManager.ExecuteAndTakeResult(driver, script);
            return crawler;
        }

        public Crawler ExecuteScript(string script)
        {
            ScriptManager.ExecuteScript(driver, script);
            return crawler;
        }

        public Crawler ExecuteScript(string script, params object[] args)
        {
            ScriptManager.ExecuteScript(driver, script, args);
            return crawler;
        }
    }
}
