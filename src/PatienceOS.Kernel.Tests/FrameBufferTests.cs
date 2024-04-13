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
            Assert.Equal((byte)'H', buffer[0]);
            Assert.Equal((byte)'e', buffer[1]);
            Assert.Equal((byte)'l', buffer[2]);
            Assert.Equal((byte)'l', buffer[3]);
            Assert.Equal((byte)'o', buffer[4]);
        }

        [Fact]
        public void FrameBuffer_Should_Copy()
        {
            // Given
            byte* buffer = stackalloc byte[5];
            var frameBuffer = new FrameBuffer(buffer);

            frameBuffer.Write(0, (byte)'H');
            frameBuffer.Write(1, (byte)'e');
            frameBuffer.Write(2, (byte)'l');
            frameBuffer.Write(3, (byte)'l');
            frameBuffer.Write(4, (byte)'o');

            // When
            frameBuffer.Copy(3, 0, 2);

            // Then
            Assert.Equal((byte)'l', buffer[0]);
            Assert.Equal((byte)'o', buffer[1]);
            Assert.Equal((byte)'l', buffer[2]);
            Assert.Equal((byte)'l', buffer[3]);
            Assert.Equal((byte)'o', buffer[4]);
        }
    }
}