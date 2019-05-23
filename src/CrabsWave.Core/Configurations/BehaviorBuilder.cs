using System.Collections.Generic;
using CrabsWave.Core.resources;

namespace CrabsWave.Core.Configurations
{
    public static class BehaviorBuilder
    {

        public static string[] Build(Behavior behavior)
        {
            if (behavior == null) behavior = new Behavior();

            var ActiveOptions = new List<string>();
            ActiveOptions.AddRange(GetBooleanPropertiesAttribues(behavior));
            return ActiveOptions.ToArray();
        }

        private static IEnumerable<string> GetBooleanPropertiesAttribues(Behavior behavior)
        {
            foreach (var prop in behavior.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof(bool) && !(bool)prop.GetValue(behavior)) continue;
                if (prop.PropertyType == typeof(string) && string.IsNullOrEmpty((string)prop.GetValue(behavior))) continue;

                foreach (var attribute in prop.GetCustomAttributes(typeof(CapabilitieOptionAttribute), false))
                {
                    if (attribute is CapabilitieOptionAttribute capabilitie)
                    {
                        if (capabilitie.UsePropValue)
                            yield return string.Format(capabilitie.Value, prop.GetValue(behavior));
                        yield return capabilitie.Value;
                    }
                }
            }
        }
    }
}
