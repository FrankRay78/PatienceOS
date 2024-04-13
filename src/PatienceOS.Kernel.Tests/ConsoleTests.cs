using Xunit;

namespace PatienceOS.Kernel.Tests
{
    unsafe public class ConsoleTests
    {
        [Fact]
        public void Should_Write_AB()
        {
            // Given
            byte* buffer = stackalloc byte[80 * 25 * 2];
            var frameBuffer = new FrameBuffer(buffer);
            var console = new Console(80, 25, frameBuffer);

            // When
            console.Print("AB");

            // Then
            Assert.Equal((byte)'A', buffer[0]);
            Assert.Equal((byte)'B', buffer[2]);
        }

        [Fact]
        public void Should_Clear_AB()
        {
            // Given
            byte* buffer = stackalloc byte[80 * 25 * 2];
            var frameBuffer = new FrameBuffer(buffer);
            var console = new Console(80, 25, frameBuffer);

            // When
            console.Print("AB");
            console.Clear();

            // Then
            Assert.Equal((byte)' ', buffer[0]);
            Assert.Equal((byte)' ', buffer[2]);
        }

        [Fact]
        public void Should_Write_A_EOL_B()
        {
            // Given
            byte* buffer = stackalloc byte[80 * 25 * 2];
            var frameBuffer = new FrameBuffer(buffer);
            var console = new Console(80, 25, frameBuffer);

            // When
            console.Print("A\nB");

            // Then
            Assert.Equal((byte)'A', buffer[0]);
            Assert.Equal((byte)'B', buffer[80 * 2 + 0]);
        }

        [Fact]
        public void Should_Clear_A_EOL_B()
        {
            // Given
            byte* buffer = stackalloc byte[80 * 25 * 2];
            var frameBuffer = new FrameBuffer(buffer);
            var console = new Console(80, 25, frameBuffer);

            // When
            console.Print("A\nB");
            console.Clear();

            // Then
            Assert.Equal((byte)' ', buffer[0]);
            Assert.Equal((byte)' ', buffer[80 * 2 + 0]);
        }

        unsafe public class ThreeXThree
        {
            [Fact]
            public void Should_Write_ABC_DEF_GHI()
            {
                // Given
                byte* buffer = stackalloc byte[3 * 3 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(3, 3, frameBuffer);

                // When
                console.Print("ABCDEFGHI");

                // Then
                Assert.Equal((byte)'A', buffer[0]);
                Assert.Equal((byte)'B', buffer[2]);
                Assert.Equal((byte)'C', buffer[4]);
                Assert.Equal((byte)'D', buffer[6]);
                Assert.Equal((byte)'E', buffer[8]);
                Assert.Equal((byte)'F', buffer[10]);
                Assert.Equal((byte)'G', buffer[12]);
                Assert.Equal((byte)'H', buffer[14]);
                Assert.Equal((byte)'I', buffer[16]);
            }

            [Fact]
            public void Should_Clear_ABC_DEF_GHI()
            {
                // Given
                byte* buffer = stackalloc byte[3 * 3 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(3, 3, frameBuffer);

                // When
                console.Print("ABCDEFGHI");
                console.Clear();

                // Then
                Assert.Equal((byte)' ', buffer[0]);
                Assert.Equal((byte)' ', buffer[2]);
                Assert.Equal((byte)' ', buffer[4]);
                Assert.Equal((byte)' ', buffer[6]);
                Assert.Equal((byte)' ', buffer[8]);
                Assert.Equal((byte)' ', buffer[10]);
                Assert.Equal((byte)' ', buffer[12]);
                Assert.Equal((byte)' ', buffer[14]);
                Assert.Equal((byte)' ', buffer[16]);
            }

            [Fact]
            public void Should_Scroll_One_Line()
            {
                // Given
                byte* buffer = stackalloc byte[3 * 3 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(3, 3, frameBuffer);

                // When
                console.PrintLine("AAA");
                console.PrintLine("BBB");
                console.PrintLine("CCC");
                console.PrintLine("DDD");

                // Then
                Assert.Equal((byte)'B', buffer[0]);
                Assert.Equal((byte)'B', buffer[2]);
                Assert.Equal((byte)'B', buffer[4]);
                Assert.Equal((byte)'C', buffer[6]);
                Assert.Equal((byte)'C', buffer[8]);
                Assert.Equal((byte)'C', buffer[10]);
                Assert.Equal((byte)'D', buffer[12]);
                Assert.Equal((byte)'D', buffer[14]);
                Assert.Equal((byte)'D', buffer[16]);
            }

            [Fact]
            public void Should_Scroll_Two_Lines()
            {
                // Given
                byte* buffer = stackalloc byte[3 * 3 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(3, 3, frameBuffer);

                // When
                console.PrintLine("AAA");
                console.PrintLine("BBB");
                console.PrintLine("CCC");
                console.PrintLine("DDD");
                console.PrintLine("EEE");

                // Then
                Assert.Equal((byte)'C', buffer[0]);
                Assert.Equal((byte)'C', buffer[2]);
                Assert.Equal((byte)'C', buffer[4]);
                Assert.Equal((byte)'D', buffer[6]);
                Assert.Equal((byte)'D', buffer[8]);
                Assert.Equal((byte)'D', buffer[10]);
                Assert.Equal((byte)'E', buffer[12]);
                Assert.Equal((byte)'E', buffer[14]);
                Assert.Equal((byte)'E', buffer[16]);
            }

            [Fact]
            public void Should_Scroll_Three_Lines()
            {
                // Given
                byte* buffer = stackalloc byte[3 * 3 * 2];
                var frameBuffer = new FrameBuffer(buffer);
                var console = new Console(3, 3, frameBuffer);

                // When
                console.PrintLine("AAA");
                console.PrintLine("BBB");
                console.PrintLine("CCC");
                console.PrintLine("DDD");
                console.PrintLine("EEE");
                console.PrintLine("FFF");

                // Then
                Assert.Equal((byte)'D', buffer[0]);
                Assert.Equal((byte)'D', buffer[2]);
                Assert.Equal((byte)'D', buffer[4]);
                Assert.Equal((byte)'E', buffer[6]);
                Assert.Equal((byte)'E', buffer[8]);
                Assert.Equal((byte)'E', buffer[10]);
                Assert.Equal((byte)'F', buffer[12]);
                Assert.Equal((byte)'F', buffer[14]);
                Assert.Equal((byte)'F', buffer[16]);
            }
        }

        unsafe public class HelloWorld
        {
            [Fact]
            public void Should_Write_Hello_World_With_Print_Statement()
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
            public void Should_Write_Hello_World_With_Print_Statements()
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
            public void Should_Write_Hello_CRLF_World_With_Print_Statement()
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
            public void Should_Write_Hello_CRLF_World_With_Print_Statements()
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
            public void Should_Write_Hello_CRLF_World_With_PrintLine_Statements()
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
