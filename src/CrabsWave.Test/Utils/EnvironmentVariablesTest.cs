using System;
using CrabsWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Utils
{
    public class EnvironmentVariablesTest
    {
        [Fact]
        public void ShouldSetVariable()
        {
            const string VariableName = "MyOnVariable";
            const string VariableValue = "MyOnVariableValue";

            EnvironmentVariablesUtil.SetVariableValue(VariableName, VariableValue);

            var value = Environment.GetEnvironmentVariable(VariableName);

            value.Should().NotBeNullOrEmpty();
            value.Should().BeEquivalentTo(VariableValue);
        }
    }
}
