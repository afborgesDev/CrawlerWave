using CrabsWave.Utils.IO;
using FluentAssertions;
using Xunit;

namespace CrabsWave.Test.Utils
{
    public class SocketHelperTest
    {
        [Fact]
        public void ShouldGetIntPortWithFourNumbers()
        {
            var port = SocketHelper.GetRandomPortToTry();
            port.Should().BeInRange(1000, 99999);
        }

        [Fact]
        public void ShouldHavePortsInUse()
        {
            var portsInUse = SocketHelper.GetSocketPortsInUse();
            portsInUse.Should().NotBeEmpty();
        }

        [Fact]
        public void ShouldGetNewPortToUse()
        {
            var portsInUse = SocketHelper.GetSocketPortsInUse();
            var newPort = SocketHelper.GetNewSocketPort();
            newPort.Should().BeInRange(1000, 99999);
            portsInUse.Should().NotContain(newPort);
        }
    }
}
