using System.Text;
using Xunit.Abstractions;

namespace CrabsWave.Test.TestHelpers
{
    public class TestTestOutputHelper : ITestOutputHelper
    {
        private readonly StringBuilder BuilderOutput = new StringBuilder();
        public string Output => BuilderOutput.ToString();

        public void WriteLine(string message) => BuilderOutput.AppendLine(message);

        public void WriteLine(string format, params object[] args) => BuilderOutput.Append(string.Format(format, args));
    }
}
