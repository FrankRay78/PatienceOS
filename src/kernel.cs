
unsafe class Program
{
    //https://www.kraxel.org/blog/2018/10/qemu-vga-emulation-and-bochs-display/
    private const int Width = 80;
    private const int Height = 25;

    private const int VideoBaseAddress = 0xb8000;
    static int currentVideoAddress = VideoBaseAddress;

    static int Main()
    {
        Clear();


        //Ascii art courtesy of https://www.asciiart.eu/text-to-ascii-art
        //Doom font, width 80

        Print(@"                                                                                ");
        Print(@"   ______     _   _                      _____ _____                            ");
        Print(@"   | ___ \   | | (_)                    |  _  /  ___|                           ");
        Print(@"   | |_/ /_ _| |_ _  ___ _ __   ___ ___ | | | \ `--.                            ");
        Print(@"   |  __/ _` | __| |/ _ \ '_ \ / __/ _ \| | | |`--. \                           ");
        Print(@"   | | | (_| | |_| |  __/ | | | (_|  __/\ \_/ /\__/ /                           ");
        Print(@"   \_|  \__,_|\__|_|\___|_| |_|\___\___| \___/\____/                            ");


        return 0;
    }

    /// <summary>
    /// Clear the screen
    /// </summary>
    unsafe static void Clear()
    {
        for (int i = 0; i < Width * Height * 2; i++)
        {
            *(byte*)(VideoBaseAddress + i) = 0;
        }
    }

    /// <summary>
    /// Print a string to the current cursor position
    /// </summary>
    unsafe static void Print(string s)
    {
        fixed (char* ps = s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                *(byte*)(currentVideoAddress) = (byte)ps[i];
                *(byte*)(currentVideoAddress + 1) = 0x0f;

                currentVideoAddress += 2;
            }
        }
    }
}