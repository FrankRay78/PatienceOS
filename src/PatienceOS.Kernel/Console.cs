namespace PatienceOS.Kernel
{
    /// <summary>
    /// A virtual terminal for the kernel to interact with the user through
    /// </summary>
    unsafe public struct Console
    {
        private int width;
        private int height;

        private FrameBuffer frameBuffer;

        private int column = 0; // nb. Incremented by two for each write, to account for the text colouring
        private int row = 0;

        private byte foregroundColor = 0x0F; // White

        public Console(int width, int height, FrameBuffer frameBuffer)
        {
            this.width = width;
            this.height = height;
            this.frameBuffer = frameBuffer;
        }

        /// <summary>
        /// Clear the screen
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < width * height * 2; i += 2)
            {
                // Write directly to the video memory
                frameBuffer.Write(i, (byte)' ');
                frameBuffer.Write(i + 1, foregroundColor);
            }

            // Reset the cursor position
            column = 0;
            row = 0;
        }

        /// <summary>
        /// Print a string to the current cursor position
        /// </summary>
        /// <remarks>
        /// Assumes each screen character is represented by two bytes aligned as a 16-bit word, 
        /// see <see cref="https://en.wikipedia.org/wiki/VGA_text_mode#Data_arrangement"/>
        /// </remarks>
        public void Print(string s)
        {
            fixed (char* ps = s)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    // Perform a CRLF if we encounter a Newline character
                    if (ps[i] == '\n')
                    {
                        column = 0;
                        row++;

                        continue;
                    }

                    // Perform a CRLF when the cursor reaches the end of the terminal line
                    // eg.column is 0 to 79, width = 80
                    if (column >= width * 2)
                    {
                        column = 0;
                        row++;
                    }

                    // Write directly to the video memory
                    frameBuffer.Write(row * width * 2 + column, (byte)ps[i]);
                    frameBuffer.Write(row * width * 2 + column + 1, foregroundColor);

                    //  Move the cursor right by one
                    column += 2;
                }
            }
        }

        /// <summary>
        /// Prints a string to the current cursor position 
        /// and then moves the cursor to the next line
        /// </summary>
        public void PrintLine(string s)
        {
            Print(s);
            Print("\n");
        }
    }
}
