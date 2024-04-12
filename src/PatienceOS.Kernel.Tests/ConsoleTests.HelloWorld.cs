using Xunit;

namespace PatienceOS.Kernel.Tests
{
    unsafe public class ConsoleTests
    {
        unsafe public class HelloWorld
        {
            [Fact]
            public void Should_Write_Hello_Space_World_With_Print_Statement()
            {
                // Given
                byte* buffer = stackalloc byte[80 * 25 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(80, 25, frameBuffer);

                // When
                console.Print("Hello World");

                // Then
                Assert.Equal((byte)'H', buffer[0]);
                Assert.Equal((byte)'e', buffer[2]);
                Assert.Equal((byte)'l', buffer[4]);
                Assert.Equal((byte)'l', buffer[6]);
                Assert.Equal((byte)'o', buffer[8]);
                Assert.Equal((byte)' ', buffer[10]);
                Assert.Equal((byte)'W', buffer[12]);
                Assert.Equal((byte)'o', buffer[14]);
                Assert.Equal((byte)'r', buffer[16]);
                Assert.Equal((byte)'l', buffer[18]);
                Assert.Equal((byte)'d', buffer[20]);
            }

            [Fact]
            public void Should_Write_Hello_Space_World_With_Print_Statements()
            {
                // Given
                byte* buffer = stackalloc byte[80 * 25 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(80, 25, frameBuffer);

                // When
                console.Print("Hello");
                console.Print(" ");
                console.Print("World");

                // Then
                Assert.Equal((byte)'H', buffer[0]);
                Assert.Equal((byte)'e', buffer[2]);
                Assert.Equal((byte)'l', buffer[4]);
                Assert.Equal((byte)'l', buffer[6]);
                Assert.Equal((byte)'o', buffer[8]);
                Assert.Equal((byte)' ', buffer[10]);
                Assert.Equal((byte)'W', buffer[12]);
                Assert.Equal((byte)'o', buffer[14]);
                Assert.Equal((byte)'r', buffer[16]);
                Assert.Equal((byte)'l', buffer[18]);
                Assert.Equal((byte)'d', buffer[20]);
            }

            [Fact]
            public void Should_Write_Hello_EOL_World_EOL_With_Print_Statement()
            {
                // Given
                byte* buffer = stackalloc byte[80 * 25 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(80, 25, frameBuffer);

                // When
                console.Print("Hello\nWorld\n");

                // Then
                Assert.Equal((byte)'H', buffer[0]);
                Assert.Equal((byte)'e', buffer[2]);
                Assert.Equal((byte)'l', buffer[4]);
                Assert.Equal((byte)'l', buffer[6]);
                Assert.Equal((byte)'o', buffer[8]);
                Assert.Equal((byte)'W', buffer[80 * 2 + 0]);
                Assert.Equal((byte)'o', buffer[80 * 2 + 2]);
                Assert.Equal((byte)'r', buffer[80 * 2 + 4]);
                Assert.Equal((byte)'l', buffer[80 * 2 + 6]);
                Assert.Equal((byte)'d', buffer[80 * 2 + 8]);
            }

            [Fact]
            public void Should_Write_Hello_EOL_World_EOL_With_Print_Statements()
            {
                // Given
                byte* buffer = stackalloc byte[80 * 25 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(80, 25, frameBuffer);

                // When
                console.Print("Hello\n");
                console.Print("World\n");

                // Then
                Assert.Equal((byte)'H', buffer[0]);
                Assert.Equal((byte)'e', buffer[2]);
                Assert.Equal((byte)'l', buffer[4]);
                Assert.Equal((byte)'l', buffer[6]);
                Assert.Equal((byte)'o', buffer[8]);
                Assert.Equal((byte)'W', buffer[80 * 2 + 0]);
                Assert.Equal((byte)'o', buffer[80 * 2 + 2]);
                Assert.Equal((byte)'r', buffer[80 * 2 + 4]);
                Assert.Equal((byte)'l', buffer[80 * 2 + 6]);
                Assert.Equal((byte)'d', buffer[80 * 2 + 8]);
            }

            [Fact]
            public void Should_Write_Hello_EOL_World_EOL_With_PrintLine_Statements()
            {
                // Given
                byte* buffer = stackalloc byte[80 * 25 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(80, 25, frameBuffer);

                // When
                console.PrintLine("Hello");
                console.PrintLine("World");

                // Then
                Assert.Equal((byte)'H', buffer[0]);
                Assert.Equal((byte)'e', buffer[2]);
                Assert.Equal((byte)'l', buffer[4]);
                Assert.Equal((byte)'l', buffer[6]);
                Assert.Equal((byte)'o', buffer[8]);
                Assert.Equal((byte)'W', buffer[80 * 2 + 0]);
                Assert.Equal((byte)'o', buffer[80 * 2 + 2]);
                Assert.Equal((byte)'r', buffer[80 * 2 + 4]);
                Assert.Equal((byte)'l', buffer[80 * 2 + 6]);
                Assert.Equal((byte)'d', buffer[80 * 2 + 8]);
            }
        }
    }
}
