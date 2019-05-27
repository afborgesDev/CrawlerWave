using System;

namespace CrabsWave.Utils.IO
{
    public static class EnvironmentVariablesUtil
    {
        public static bool CheckSplitedVariableValueExist(string variable, string valueToCheck, char splitValue) => !string.IsNullOrEmpty(
            Array.Find(GetVariableValueSplited(variable, splitValue), x => x.Contains(valueToCheck))
        );

        public static string GetVariableValue(string variable) => Environment.GetEnvironmentVariable(variable);

        public static string[] GetVariableValueSplited(string variable, char splitValue) => GetVariableValue(variable).Split(splitValue);

        public static void SetVariableValue(string variable, string value) => Environment.SetEnvironmentVariable(variable, value);
    }
}
