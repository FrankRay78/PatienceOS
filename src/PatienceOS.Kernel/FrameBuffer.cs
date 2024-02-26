namespace PatienceOS.Kernel
{
    /// <summary>
    /// A linear framebuffer of bytes
    /// </summary>
    unsafe public struct FrameBuffer
    {
        private byte* buffer;

        public FrameBuffer(byte* buffer)
        {
            this.buffer = buffer;
        }

        public byte Fetch(int position)
        {
            return buffer[position];
        }

        public void Write(int position, byte value)
        {
            buffer[position] = value;
        }
    }
}
