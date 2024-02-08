using System;

unsafe class Program
{
    static int pos = 0;

    static int Main()
    {
        // Clear the screen
        for(int i = 0; i < 80 * 25 * 2; i++)
            *(byte *)(0xb8000 + i) = 0;

        //Print('H');
        //Print('e');
        //Print('l');
        //Print('l');
        //Print('o');

        //Ascii art courtesy of https://www.asciiart.eu/text-to-ascii-art
        //Doom, width 80

        //Print("______     _   _                      _____ _____ ");
        //Print("| ___ \\   | | (_)                    |  _  /  ___|");
        //Print("| |_/ /_ _| |_ _  ___ _ __   ___ ___ | | | \\ `--. ");
        //Print("|  __/ _` | __| |/ _ \\ '_ \\ / __/ _ \\| | | |`--. \\");
        //Print("| | | (_| | |_| |  __/ | | | (_|  __/\\ \\_/ /\\__/ /");
        //Print("\\_|  \\__,_|\\__|_|\\___|_| |_|\\___\\___| \\___/\\____/ ");

        //Print(@"______     _   _                      _____ _____ ");
        //Print(@"| ___ \   | | (_)                    |  _  /  ___|");
        //Print(@"| |_/ /_ _| |_ _  ___ _ __   ___ ___ | | | \ `--. ");
        //Print(@"|  __/ _` | __| |/ _ \ '_ \ / __/ _ \| | | |`--. \");
        //Print(@"| | | (_| | |_| |  __/ | | | (_|  __/\ \_/ /\__/ /");
        //Print(@"\_|  \__,_|\__|_|\___|_| |_|\___\___| \___/\____/ ");

        Print("Hello");

        return 0;
    }

    unsafe static void Print(string s)
    {
        fixed (char* ps = s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Print((char)ps[i]);
            }
        }
    }

    unsafe static void Print(char c)
    {
        *(byte *)(0xb8000 + pos) = (byte)c;
        *(byte *)(0xb8000 + pos + 1) = 0x0f;
        pos += 2;
    }
}