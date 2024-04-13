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
            Color foregroundColor = Color.White;

            // When
            frameBuffer.Write(0, 'H', foregroundColor);
            frameBuffer.Write(1, 'e', foregroundColor);
            frameBuffer.Write(2, 'l', foregroundColor);
            frameBuffer.Write(3, 'l', foregroundColor);
            frameBuffer.Write(4, 'o', foregroundColor);

            // Then
            Assert.Equal((byte)'H', buffer[0]);
            Assert.Equal((byte)'e', buffer[1]);
            Assert.Equal((byte)'l', buffer[2]);
            Assert.Equal((byte)'l', buffer[3]);
            Assert.Equal((byte)'o', buffer[4]);
        }
    }
}