namespace PatienceOS.Kernel
{
    /// <summary>
    /// Writes directly to the video memory
    /// </summary>
    unsafe public struct FrameBuffer
    {
        private int baseAddress;

        public FrameBuffer(int baseAddress)
        {
            this.baseAddress = baseAddress;
        }

        public void Write(int position, byte value)
        {
            *(byte*)(baseAddress + position) = value;
        }
    }
}
