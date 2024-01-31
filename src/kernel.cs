using System;

unsafe class Program
{
    static int pos = 0;

    static int Main()
    {
        // Clear the screen
        for(int i = 0; i < 80 * 25 * 2; i++)
            *(byte *)(0xb8000 + i) = 0;

        Print('H');
        Print('e');
        Print('l');
        Print('l');
        Print('o');

        return 0;
    }

    unsafe static void Print(char c)
    {
        *(byte *)(0xb8000 + pos) = (byte)c;
        *(byte *)(0xb8000 + pos + 1) = 0x0f;
        pos += 2;
    }
}