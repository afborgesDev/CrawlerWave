using System.Linq;
using System.Reflection;
using CrabsWave.Core.Resources;

namespace CrabsWave.Core.Configurations
{
    public class Behavior
    {
        [CapabilitieOption(Constants.IgnoreCertifcateErrorsOption)]
        public bool IgnoreCerticateErrorsEnabled { get; set; } = true;

        [CapabilitieOption(Constants.JavaScriptUsageOption)]
        public bool JavaScriptEnabled { get; set; } = true;

        [CapabilitieOption(Constants.HeadlessNavigationOption)]
        public bool HeadlessEnabled { get; set; } = true;

        [CapabilitieOption(Constants.NoSandboxOption),
         CapabilitieOption(Constants.DisableSetUidSandboxOption)]
        public bool SandBoxDisabled { get; set; } = true;

        [CapabilitieOption(Constants.BlinkSettingsImagesEnabledOption)]
        public bool LoadImagesDisabled { get; set; } = true;

        [CapabilitieOption(Constants.DisableGpuOption)]
        public bool GpuDisabled { get; set; } = true;

        [CapabilitieOption(Constants.DisableImlSidePaintingOption)]
        public bool ImplicitSidePaitingDisabled { get; set; } = true;

        [CapabilitieOption(Constants.DisableDevShmUsageOption)]
        public bool DevShmUsageDisabled { get; set; } = true;

        [CapabilitieOption(Constants.ProxyServerOption, true)]
        public string Proxy { get; set; }

        public bool Verbose { get; set; } = true;

        public PropertyInfo[] GetPropertyWithCapabilitieOption() => GetType().GetProperties()
                                                                             .Where(x =>
                                                                             {
                                                                                 if (x.PropertyType == typeof(bool) && (bool)x.GetValue(this)) return true;
                                                                                 if (x.PropertyType == typeof(string) && !string.IsNullOrEmpty((string)x.GetValue(this))) return true;
                                                                                 return false;
                                                                             }).ToArray();
    }
}
