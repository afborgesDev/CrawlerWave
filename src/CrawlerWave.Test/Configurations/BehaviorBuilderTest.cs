using System.Collections.Generic;
using CrawlerWave.Core.Configurations;
using CrawlerWave.Core.Resources;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Configurations
{
    public class BehaviorBuilderTest
    {
        public static IEnumerable<object[]> GetBehaviorPropertiesToTest()
        {
            yield return new object[] { new Behavior() { HeadlessEnabled = false }, Constants.HeadlessNavigationOption };
            yield return new object[] { new Behavior() { IgnoreCerticateErrorsEnabled = false }, Constants.IgnoreCertifcateErrorsOption };
            yield return new object[] { new Behavior() { JavaScriptEnabled = false }, Constants.JavaScriptUsageOption };
            yield return new object[] { new Behavior() { SandBoxDisabled = false }, Constants.NoSandboxOption };
            yield return new object[] { new Behavior() { LoadImagesDisabled = false }, Constants.BlinkSettingsImagesEnabledOption };
            yield return new object[] { new Behavior() { GpuDisabled = false }, Constants.DisableGpuOption };
            yield return new object[] { new Behavior() { ImplicitSidePaitingDisabled = false }, Constants.DisableImlSidePaintingOption };
            yield return new object[] { new Behavior() { DevShmUsageDisabled = false }, Constants.DisableDevShmUsageOption };
            yield return new object[] { new Behavior() { Proxy = "" }, Constants.ProxyServerOption };
        }

        [Fact]
        public void ShouldUseDefaultIfNoBehavior()
        {
            var items = BehaviorBuilder.Build(default);

            items.Should().NotBeNullOrEmpty();
            items.Should().HaveCountGreaterThan(0);
            items.Should().OnlyHaveUniqueItems();
        }

        [Fact]
        public void ShouldHaveDefaultOnActiveOptions()
        {
            var items = BehaviorBuilder.Build(new Behavior());

            items.Should().HaveCountGreaterThan(0);
            items.Should().OnlyHaveUniqueItems();
            items.Should().Contain(new List<string>() {
                Constants.IgnoreCertifcateErrorsOption,
                Constants.JavaScriptUsageOption,
                Constants.HeadlessNavigationOption,
                Constants.DisableSetUidSandboxOption,
                Constants.NoSandboxOption,
                Constants.BlinkSettingsImagesEnabledOption,
                Constants.DisableGpuOption,
                Constants.DisableImlSidePaintingOption,
                Constants.DisableDevShmUsageOption
            });
        }

        [Fact]
        public void ShouldFormatStringCapabilitie()
        {
            var items = BehaviorBuilder.Build(new Behavior() { Proxy = "myproxy.com" });

            items.Should().HaveCountGreaterThan(0);
            items.Should().OnlyHaveUniqueItems();
            items.Should().Contain(string.Format(Constants.ProxyServerOption, "myproxy.com"));
        }

        [Theory]
        [MemberData(nameof(GetBehaviorPropertiesToTest))]
        public void ShouldMachPropetiesBehavior(Behavior behavior, string identify)
        {
            var items = BehaviorBuilder.Build(behavior);

            items.Should().HaveCountGreaterThan(0);
            items.Should().OnlyHaveUniqueItems();
            items.Should().NotContain(identify);
        }
    }
}
