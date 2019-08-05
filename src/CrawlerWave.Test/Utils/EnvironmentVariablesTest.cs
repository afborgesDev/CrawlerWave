using System;
using CrawlerWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrawlerWave.Test.Utils
{
    public class EnvironmentVariablesTest
    {
        [Theory]
        [InlineData("MyOnValue", "MyOnValue")]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void ShouldSetVariable(string value, string expectedValue)
        {
            var VariableName = $"MyOnVariable-{new Random().Next(10, 100)}";
            EnvironmentVariablesUtil.SetVariableValue(VariableName, value);

            var settedValue = Environment.GetEnvironmentVariable(VariableName);
            settedValue.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void ShouldGetVariable()
        {
            var VariableName = $"MyOnVariable-{new Random().Next(10, 100)}";
            const string SimpleValue = "MyOnVariableValue";
            EnvironmentVariablesUtil.SetVariableValue(VariableName, SimpleValue);

            var value = EnvironmentVariablesUtil.GetVariableValue(VariableName);
            value.Should().NotBeNullOrWhiteSpace();
            value.Should().BeEquivalentTo(SimpleValue);
        }

        [Theory]
        [InlineData("1;2;3;4", new string[] { "1", "2", "3", "4" })]
        [InlineData("", default)]
        public void ShouldGetValueSplited(string value, string[] expectedReturn)
        {
            var VariableName = $"MyOnVariable-{new Random().Next(10, 100)}";
            EnvironmentVariablesUtil.SetVariableValue(VariableName, value);

            var splited = EnvironmentVariablesUtil.GetVariableValueSplited(VariableName, ';');
            splited.Should().BeEquivalentTo(expectedReturn);
        }

        [Theory]
        [InlineData("1;2;3;4", "2", ';', true)]
        [InlineData("1;2;3;4", "9", ';', false)]
        [InlineData("1;2;3;4", "2", ',', true)]
        [InlineData("1;2;3;4", "2", ' ', true)]
        [InlineData("", "2", ';', false)]
        public void ShouldCheckSplitedVariableValueExist(string value, string valueToCheck, char splitChar, bool expetectedValue)
        {
            var VariableName = $"MyOnVariable-{new Random().Next(10, 100)}";
            EnvironmentVariablesUtil.SetVariableValue(VariableName, value);
            var exists = EnvironmentVariablesUtil.CheckSplitedVariableValueExist(VariableName, valueToCheck, splitChar);
            exists.Should().Be(expetectedValue);
        }
    }
}
