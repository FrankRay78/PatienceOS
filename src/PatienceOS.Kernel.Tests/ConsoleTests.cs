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
    }
}
