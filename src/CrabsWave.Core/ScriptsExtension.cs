using CrabsWave.Core.Functionalities.Scripts;

namespace CrabsWave.Core
{
    public static class ScriptsExtension
    {
        public static Crawler ExecuteJavaScript(this Crawler parent, string script)
        {
            ScriptManager.ExecuteScriptUsingJavaScriptExecutor(parent.Driver, script);
            return parent;
        }

        public static Crawler ExecuteJavaScript(this Crawler parent, string script, params object[] args)
        {
            ScriptManager.ExecuteScriptUsingJavaScriptExecutor(parent.Driver, script, args);
            return parent;
        }

        public static Crawler ExecuteJavaScript(this Crawler parent, string script, out string result)
        {
            result = ScriptManager.ExecuteAndTakeResult(parent.Driver, script);
            return parent;
        }
    }
}
