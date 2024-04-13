using System;

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

        public void Write(int position, byte value)
        {
            buffer[position] = value;
        }

        public void Copy(int sourcePosition, int destinationPosition, int length)
        {
            for (int i = 0; i < length; i++)
            {
                buffer[destinationPosition + i] = buffer[sourcePosition + i];
            }
        }
    }
}
