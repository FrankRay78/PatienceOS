namespace PatienceOS.Kernel
{
    /// <summary>
    /// Writes directly to the video memory
    /// </summary>
    /// <remarks>
    /// Assumes VGA text mode 7 (80 x 25)
    /// ref: https://en.wikipedia.org/wiki/VGA_text_mode
    /// </remarks>
    unsafe public static class Console
    {
        //https://www.kraxel.org/blog/2018/10/qemu-vga-emulation-and-bochs-display/
        private const int Width = 80;
        private const int Height = 25;

        private const int VideoBaseAddress = 0xb8000;

        private static int pos = 0;

        /// <summary>
        /// Clear the screen
        /// </summary>
        unsafe public static void Clear()
        {
            for (int i = 0; i < Width * Height * 2; i++)
            {
                *(byte*)(VideoBaseAddress + i) = 0;
            }
        }

        /// <summary>
        /// Print a string to the current cursor position
        /// </summary>
        unsafe public static void Print(string s)
        {
            fixed (char* ps = s)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    *(byte*)(VideoBaseAddress + pos) = (byte)ps[i];
                    *(byte*)(VideoBaseAddress + pos + 1) = 0x0f;

                    pos += 2;
                }
            }
        }
    }
}
