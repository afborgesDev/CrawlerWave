using CrawlerWave.Core.Functionalities;

namespace CrawlerWave.Core;

public static class ScriptsExtension
{
    public static Crawler ExecuteJavaScript(this Crawler parent, string script)
    {
        ScriptManager.New(parent).ExecuteScriptUsingJavaScriptExecutor(parent, script);
        return parent;
    }

    public static Crawler ExecuteJavaScript(this Crawler parent, string script, params object[] args)
    {
        ScriptManager.New(parent).ExecuteScriptUsingJavaScriptExecutor(parent, script, args);
        return parent;
    }

    public static Crawler ExecuteJavaScript(this Crawler parent, string script, out string result)
    {
        result = ScriptManager.New(parent).ExecuteAndTakeResult(parent, script);
        return parent;
    }
}
