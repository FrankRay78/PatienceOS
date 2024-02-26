using Xunit;

namespace PatienceOS.Kernel.Tests
{
    unsafe public class FrameBufferTests
    {
        [Fact]
        public void FrameBuffer_Should_Contain_Hello()
        {
            // Given
            byte* buffer = stackalloc byte[5];
            var frameBuffer = new FrameBuffer(buffer);

            // When
            frameBuffer.Write(0, (byte)'H');
            frameBuffer.Write(1, (byte)'e');
            frameBuffer.Write(2, (byte)'l');
            frameBuffer.Write(3, (byte)'l');
            frameBuffer.Write(4, (byte)'o');

            // Then
            Assert.Equal((byte)'H', frameBuffer.Fetch(0));
            Assert.Equal((byte)'e', frameBuffer.Fetch(1));
            Assert.Equal((byte)'l', frameBuffer.Fetch(2));
            Assert.Equal((byte)'l', frameBuffer.Fetch(3));
            Assert.Equal((byte)'o', frameBuffer.Fetch(4));
        }
    }
}