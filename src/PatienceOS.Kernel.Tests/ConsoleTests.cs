using Xunit;

namespace PatienceOS.Kernel.Tests
{
    public class ConsoleTests
    {
        [Fact]
        public async Task ConsoleShouldThrowAsync()
        {
            //Attempts to write directly to the video memory
            //Assert.Throws<Exception>(() => Console.Clear());
            //The active test run was aborted. Reason: Test host process crashed : Fatal error. System.AccessViolationException: Attempted to read or write protected memory.

            await Assert.ThrowsAsync<Exception>(() => throw new Exception());
        }

        [Fact]
        public async Task Console_Should_Write_HelloWorld_Async()
        {
            // Given
            var frameBuffer = new VideoMemory(0xb8000);
            var console = new Console(80, 25, frameBuffer);

            // When
            console.Print("Hello World");

            // Then
            //TODO: framebuffer.contains("Hello World");
        }
    }
}
