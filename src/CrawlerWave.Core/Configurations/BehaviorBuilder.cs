using System.Collections.Generic;
using System.Linq;
using CrawlerWave.Core.Resources;

namespace CrawlerWave.Core.Configurations
{
    //TODO: Should revisit this class seems it can be simplified | maybe now start using yml
    public static class BehaviorBuilder
    {
        public static string[] Build(Behavior behavior) => GetBooleanPropertiesAttribues(behavior ?? new Behavior()).ToArray();

        private static IEnumerable<string> GetBooleanPropertiesAttribues(Behavior behavior)
        {
            foreach (var prop in behavior.GetPropertyWithCapabilitieOption())
            {
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
