using Xunit;

namespace PatienceOS.Kernel.Tests
{
    unsafe public class ConsoleTests
    {
        [Fact]
        public void Console_Should_Write_Hello()
        {
            // Given
            byte* buffer = stackalloc byte[80 * 25 * 2];
            var frameBuffer = new FrameBuffer(buffer);
            var console = new Console(80, 25, frameBuffer);

            // When
            console.Print("Hello");

            // Then
            Assert.Equal((byte)'H', frameBuffer.Fetch(0));
            Assert.Equal((byte)'e', frameBuffer.Fetch(2));
            Assert.Equal((byte)'l', frameBuffer.Fetch(4));
            Assert.Equal((byte)'l', frameBuffer.Fetch(6));
            Assert.Equal((byte)'o', frameBuffer.Fetch(8));
        }
    }
}
