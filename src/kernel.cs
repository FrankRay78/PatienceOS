using System;

// ______     _   _                      _____ _____ 
// | ___ \   | | (_)                    |  _  /  ___|
// | |_/ /_ _| |_ _  ___ _ __   ___ ___ | | | \ `--. 
// |  __/ _` | __| |/ _ \ '_ \ / __/ _ \| | | |`--. \
// | | | (_| | |_| |  __/ | | | (_|  __/\ \_/ /\__/ /
// \_|  \__,_|\__|_|\___|_| |_|\___\___| \___/\____/ 

unsafe class Program
{
    //https://www.kraxel.org/blog/2018/10/qemu-vga-emulation-and-bochs-display/

    private const int Width = 80;
    private const int Height = 25;

    private const int VideoBaseAddress = 0xb8000;

    static int currentVideoAddress = 0xb8000;
    static int row = 0;

    static int Main()
    {
        Clear();


        //Ascii art courtesy of https://www.asciiart.eu/text-to-ascii-art
        //Doom font, width 80

        //PrintLine(@" ______     _   _                      _____ _____ ");
        //PrintLine(@" | ___ \   | | (_)                    |  _  /  ___|");
        //PrintLine(@" | |_/ /_ _| |_ _  ___ _ __   ___ ___ | | | \ `--. ");
        //PrintLine(@" |  __/ _` | __| |/ _ \ '_ \ / __/ _ \| | | |`--. \");
        //PrintLine(@" | | | (_| | |_| |  __/ | | | (_|  __/\ \_/ /\__/ /");
        //PrintLine(@" \_|  \__,_|\__|_|\___|_| |_|\___\___| \___/\____/ ");

        Print(@"                                                                                ");
        Print(@"   ______     _   _                      _____ _____                            ");
        Print(@"   | ___ \   | | (_)                    |  _  /  ___|                           ");
        Print(@"   | |_/ /_ _| |_ _  ___ _ __   ___ ___ | | | \ `--.                            ");
        Print(@"   |  __/ _` | __| |/ _ \ '_ \ / __/ _ \| | | |`--. \                           ");
        Print(@"   | | | (_| | |_| |  __/ | | | (_|  __/\ \_/ /\__/ /                           ");
        Print(@"   \_|  \__,_|\__|_|\___|_| |_|\___\___| \___/\____/                            ");


        //for (int i = 0; i < 81; i++)
        //{
        //    Print("a");
        //}


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
    /// Print a string to the current cursor position, then
    /// move the cursor to the beginning of the next line
    /// </summary>
    unsafe static void PrintLine(string s)
    {
        Print(s);

        //Increment the row
        row++;

        //Update the current video address
        currentVideoAddress = VideoBaseAddress + (row * Width + 2);
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