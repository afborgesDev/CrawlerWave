using CrabsWave.Core.Functionalities.Scripts;

namespace CrabsWave.Core
{
    public static class ScriptsExtension
    {
        public static Crawler ExecuteScript(this Crawler parent, string script)
        {
            ScriptManager.ExecuteScript(parent.Driver, script);
            return parent;
        }

        public static Crawler ExecuteScript(this Crawler parent, string script, params object[] args)
        {
            ScriptManager.ExecuteScript(parent.Driver, script, args);
            return parent;
        }

        public static Crawler ExecuteAndTakeResult(this Crawler parent, string script, out string result)
        {
            result = ScriptManager.ExecuteAndTakeResult(parent.Driver, script);
            return parent;
        }
    }
}
