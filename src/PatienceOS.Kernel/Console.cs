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

        private int column = 0;
        private int row = 0;

        private Color foregroundColor;


        public Console(int width, int height, FrameBuffer frameBuffer)
        {
            this.width = width;
            this.height = height;
            this.foregroundColor = Color.White;
            this.frameBuffer = frameBuffer;
        }

        public Console(int width, int height, Color foregroundColor, FrameBuffer frameBuffer)
        {
            this.width = width;
            this.height = height;
            this.foregroundColor = foregroundColor;
            this.frameBuffer = frameBuffer;
        }


        /// <summary>
        /// Clear the screen
        /// </summary>
        /// <remarks>
        /// Blanks the screen by writing the ASCII space character to each character cell
        /// </remarks>
        public void Clear()
        {
            // Reset the cursor position
            column = 0;
            row = 0;

            for (int i = 0; i < width * height; i++)
            {
                Print(' ');
            }

            // Reset the cursor position
            column = 0;
            row = 0;
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
                    Print(ps[i]);
                }
            }
        }

        /// <summary>
        /// Print a character to the current cursor position
        /// </summary>
        public void Print(char c)
        {
            // Scroll if the cursor has dropped off the bottom of the terminal
            if (row == height)
            {
                frameBuffer.Copy(width * 2, 0, (height - 1) * width * 2);

                row--;

                // Blank the last line ready for writing to
                for (int i = 0; i < width; i++)
                {
                    frameBuffer.Write(row * width * 2 + i * 2, (byte)' ');
                    frameBuffer.Write(row * width * 2 + i * 2 + 1, (byte)foregroundColor);
                }
            }

            // Perform a CRLF if we encounter a Newline character
            if (c == '\n')
            {
                column = 0;
                row++;

                return;
            }

            // Write directly to the video memory, calculating the
            // positional index required for the linear framebuffer
            frameBuffer.Write(row * width * 2 + column * 2, (byte)c);
            frameBuffer.Write(row * width * 2 + column * 2 + 1, (byte)foregroundColor);

            // Move the cursor right by one character
            column++;

            // Perform a CRLF when the cursor reaches the end of the terminal line
            // eg.column is 0 to 79, width = 80
            if (column == width)
            {
                column = 0;
                row++;
            }
        }
    }
}
