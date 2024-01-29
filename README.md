# PatienceOS
A baremetal C# kernel.

The repo name has been chosen to remind Frank this is a 12-month initial project (at least), as part of his [2024 professional development](https://frankray.net/blog/2023/11/writing-an-os-in-csharp-dotnet/) goals. The dopamine hit from quickly pushing out PR's, like he regularly gets from contributing to other OSS repos (eg. [spectre.console](https://github.com/spectreconsole/spectre.console)), simply won't be possible. Hence the need for patience, and perseverance.

## Objectives
A working set of things to achieve:
 
- [x] 1. Ability to replace the standard Linux init process with a custom C application, compiled to bare metal and executable. (DONE, 18 Nov 2023, see: [Boot the Linux 6.x kernel in QEMU and run a custom C application as the init process](https://gist.github.com/FrankRay78/426011c03a7fb4f890eb5b4a068720c8))
- [X] 2. Using an emulator, boot in 32-bit protected mode ~~using GRUB2~~ (nb. QEMU can boot directly into 32-bit protected mode, saving the faff of making an ISO image) and output 'hello world' by writing directly to the VGA video memory. (DONE, 4 Jan 2024, see: [Compiling a C# kernel to bare metal and booting in QEMU](https://frankray.net/blog/2024/01/compiling-a-csharp-kernel-to-bare-metal-and-booting-in-qemu/))
- [ ] 3. Hardware interrupts and outputting keyboard keystrokes to the screen.
- [ ] 5. Memory management, garbage collection. How this works underneath the covers for an AOT compiled C# kernel ie. stack, heap and virtual page application.
- [ ] 4. Loading an elf into memory and executing it. Loading multiple instances of the elf and executing them (what do they share, and how is that allocated, managed etc)
- [ ] 6. Implement/port some useful routines from the C standard library ie. vfprintf ([master](https://git.musl-libc.org/cgit/musl/tree/src/stdio/vfprintf.c), [clone](https://github.com/BlankOn/musl/blob/master/src/stdio/printf.c))
- [ ] 7. Implement custom TCPIP stack, see: [Let's code a TCP/IP stack](https://www.saminiir.com/lets-code-tcp-ip-stack-1-ethernet-arp/), [Level-IP](https://github.com/saminiir/level-ip), [tapip](https://github.com/chobits/tapip)
- [ ] 8. Ultimately, host a TFTP server, see: [Tftp.Net C# Library](https://github.com/Callisto82/tftp.net), [Booting Arch Linux from PXE](https://www.saminiir.com/boot-arch-linux-from-pxe/)

## Progress
The obligatory screenshot, which (at the moment), is pretty underwhelming I admit:
![QEMU Hello](https://github.com/FrankRay78/PatienceOS/assets/52075808/944c82c0-0f5b-4880-a0bb-ee36bb5628ee)

## Playing with PatienceOS

### Windows 10 Host
I love Visual Studio and find it a far superior IDE compared to VS Code. Unfortunately, the lack of a Linux Visual Studio version means I'm tied to Windows, at least for now ([probably forever](https://developercommunity.visualstudio.com/t/Visual-Studio-for-Linux/360479)). This is my primary reason for using Windows as my development machine ('nix users, don't hate on me).

### Installation
Perform the following one-off installations, ideally in a virtual machine:

1. Microsoft .Net 8, Visual Studio 2022
2. MSYS2 MINGW64, see: [MSYS2-Installation](https://www.msys2.org/wiki/MSYS2-installation/)
3. NASM, the native Windows binaries, see: [NASM Downloads](https://www.nasm.us/pub/nasm/releasebuilds/?C=M;O=D)
4. QEMU, the native Windows binaries, see: [Download QEMU](https://www.qemu.org/download/#windows), nb. I used the latest Stefan Weil [64-bit Windows installer](https://qemu.weilnetz.de/w64/)

### Configuration
1. ILC, the Native .Net AOT compiler, needs to be installed locally eg. `dotnet add package Microsoft.DotNet.ILCompiler --version 7.0.14`
2. The path to ILC needs to be set as an environment variable called `ILCPATH` eg. `setx ILCPATH "C:\Users\frank\.nuget\packages\runtime.win-x64.microsoft.dotnet.ilcompiler\7.0.14\tools"` (no trailing slash)

### Booting
1. Open `x64 Native Tools Command Prompt for VS 2022`
2. Set the correct tool paths in the console session, `src\setpath.cmd`
3. Compile, link and boot, `src\build.cmd`

Patience OS, a baremetal C# kernel, should boot in QEMU.

## Notes
Targeting machine-specific architectures can be done at the compiler call (eg. `csc /platform:x64`) and/or the linker call (eg. `link /machine:x64`)
