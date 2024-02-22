namespace PatienceOS.Kernel
{
    /// <summary>
    /// A linear framebuffer of bytes
    /// </summary>
    unsafe public interface IFrameBuffer
    {
        byte Fetch(int position);
        void Write(int position, byte value);
    }

    /// <summary>
    /// Writes directly to the video memory
    /// </summary>
    unsafe public struct VideoMemory : IFrameBuffer
    {
        private int baseAddress;

        public VideoMemory(int baseAddress)
        {
            this.baseAddress = baseAddress;
        }

        public byte Fetch(int position)
        {
            return *(byte*)(baseAddress + position);
        }

        public void Write(int position, byte value)
        {
            *(byte*)(baseAddress + position) = value;
        }
    }

    /// <summary>
    /// Writes to an in memory buffer
    /// </summary>
    unsafe public struct MemoryBuffer : IFrameBuffer
    {
        private byte* buffer;

        public MemoryBuffer(int size)
        {
            // In C#, you cannot declare a pointer without initializing it
            byte* buffer = stackalloc byte[size];

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
