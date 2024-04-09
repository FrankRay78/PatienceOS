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

        private int pos = 0;

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
            for (int i = 0; i < width * height * 2; i++)
            {
                frameBuffer.Write(i, 0);
            }
        }

        private byte foregroundColor = 0x0F;

        /// <summary>
        /// Print a string to the current cursor position
        /// </summary>
        public void Print(string s)
        {
            fixed (char* ps = s)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    frameBuffer.Write(pos, (byte)ps[i]);
                    frameBuffer.Write(pos + 1, 0x0F);

                    pos += 2;
                }
            }
        }
    }
}
