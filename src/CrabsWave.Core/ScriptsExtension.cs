using CrabsWave.Core.Functionalities.Scripts;

namespace CrabsWave.Core
{
    public static class ScriptsExtension
    {
        public static Crawler ExecuteJavaScript(this Crawler parent, string script)
        {
            new ScriptManager(parent.CreateLogger(ScriptManager.LoggerCategory))
                .ExecuteScriptUsingJavaScriptExecutor(parent, script);
            return parent;
        }

        public static Crawler ExecuteJavaScript(this Crawler parent, string script, params object[] args)
        {
            new ScriptManager(parent.CreateLogger(ScriptManager.LoggerCategory))
                .ExecuteScriptUsingJavaScriptExecutor(parent, script, args);
            return parent;
        }

        public static Crawler ExecuteJavaScript(this Crawler parent, string script, out string result)
        {
            result = new ScriptManager(parent.CreateLogger(ScriptManager.LoggerCategory))
                         .ExecuteAndTakeResult(parent, script);
            return parent;
        }
    }
}
