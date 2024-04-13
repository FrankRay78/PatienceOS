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

        public void Write(int position, char value, Color forgroundColor)
        {
            buffer[position] = (byte)value;
            buffer[position + 1] = (byte)forgroundColor;
        }
    }
}
