using CrabsWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Utils
{
    public class EnvironmentUtilTest
    {
        [Fact]
        public void ShouldRetriveEnvironmentValue()
        {
            var sut = EnvironmentVariablesUtil.GetVariableValue("PATH");
            sut.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ShouldRetriveArrayFromEnvironmentValue()
        {
            var sut = EnvironmentVariablesUtil.GetVariableValueSplited("Path", ';');
            sut.Should().NotBeNull();
            sut.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void ShouldSetEnvironmentVariableValue()
        {
            const string variableName = "sutTest";
            const string variableValue = "sutTest";

            EnvironmentVariablesUtil.SetVariableValue(variableName, variableValue);
            var sut = EnvironmentVariablesUtil.GetVariableValue(variableName);
            sut.Should().Be(variableValue);
        }

        [Fact]
        public void ShouldHaveValueOnSplitedEnvironmentVariableValue()
        {
            const string variableName = "sutSplitTest";
            const string variableValue = "sut;split;test";
            const string expected = "test";

            EnvironmentVariablesUtil.SetVariableValue(variableName, variableValue);
            var sut = EnvironmentVariablesUtil.CheckSplitedVariableValueExist(variableName, expected, ';');
            sut.Should().BeTrue();
        }
    }
}
