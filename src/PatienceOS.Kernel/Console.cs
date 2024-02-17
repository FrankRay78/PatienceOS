namespace PatienceOS.Kernel
{
    //unsafe public struct FrameBuffer
    //{
    //    private const int VideoBaseAddress = 0xb8000;

    //    public FrameBuffer(int baseAddress, int size)
    //    {
    //    }

    //    public void Write(int position, byte value)
    //    {
    //        *(byte*)(address) = value;
    //    }
    //}

    unsafe public struct FrameBuffer
    {
        public FrameBuffer() 
        {
        }

        public void Write(int address, byte value)
        {
            *(byte*)(address) = value;
        }
    }

    /// <summary>
    /// Writes directly to the video memory
    /// </summary>
    /// <remarks>
    /// Assumes VGA text mode 7 (80 x 25)
    /// ref: https://en.wikipedia.org/wiki/VGA_text_mode
    /// </remarks>
    unsafe public struct Console
    {
        //https://www.kraxel.org/blog/2018/10/qemu-vga-emulation-and-bochs-display/
        private const int Width = 80;
        private const int Height = 25;

        private const int VideoBaseAddress = 0xb8000;

        private int pos = 0;
        private FrameBuffer frameBuffer = new FrameBuffer();

        public Console()
        {
        }

        /// <summary>
        /// Clear the screen
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Width * Height * 2; i++)
            {
                frameBuffer.Write(VideoBaseAddress + i, 0);
            }
        }

        /// <summary>
        /// Print a string to the current cursor position
        /// </summary>
        public void Print(string s)
        {
            fixed (char* ps = s)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    frameBuffer.Write(VideoBaseAddress + pos, (byte)ps[i]);
                    frameBuffer.Write(VideoBaseAddress + pos + 1, 0x0F);

                    pos += 2;
                }
            }
        }
    }
}
