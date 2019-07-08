using CrabsWave.Core.Functionalities.Scripts;

namespace CrabsWave.Core
{
    public static class ScriptsExtension
    {
        public static Crawler ExecuteJavaScript(this Crawler parent, string script)
        {
            parent.RestoreLog();
            ScriptManager.ExecuteScriptUsingJavaScriptExecutor(parent.Driver, script);
            return parent;
        }

        public static Crawler ExecuteJavaScript(this Crawler parent, string script, params object[] args)
        {
            parent.RestoreLog();
            ScriptManager.ExecuteScriptUsingJavaScriptExecutor(parent.Driver, script, args);
            return parent;
        }

        public static Crawler ExecuteJavaScript(this Crawler parent, string script, out string result)
        {
            parent.RestoreLog();
            result = ScriptManager.ExecuteAndTakeResult(parent.Driver, script);
            return parent;
        }
    }
}
