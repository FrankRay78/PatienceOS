# PatienceOS
A baremetal C# kernel - Frank Ray, [Better Software UK](https://bettersoftware.uk/).

Built using a combination of Microsoft's C# IL and native AOT compilers, and the GNU toolchain.

#### Commentary on the name
PatienceOS was chosen to remind Frank this is a 12-month initial project (at least), as part of his [2024 professional development](https://frankray.net/blog/2023/11/writing-an-os-in-csharp-dotnet/) goals. The dopamine hit from quickly pushing out PR's, like he regularly enjoys from contributing to other OSS repos (eg. [spectre.console](https://github.com/spectreconsole/spectre.console)), simply won't be possible with OS development. Hence the need for patience, and perseverance.

#### References
I will lean heavily on OSDev Wiki ([C Sharp Bare Bones](https://wiki.osdev.org/C_Sharp_Bare_Bones) initially) and the tutorials written by Philipp Oppermann ([Writing an OS in Rust](https://os.phil-opp.com/)), Guanzhou Hu ([Hux kernel](https://github.com/josehu07/hux-kernel/wiki)) and Carlos Fenollosa ([os-tutorial](https://github.com/cfenollosa/os-tutorial)), and also the C# educational operating system written by Ed Nutting ([FlingOS](https://github.com/FlingOS/FlingOS)).

## Progress
The obligatory screenshot, which (at the moment), is pretty underwhelming I admit:
![PatienceOS - boot splash](https://github.com/FrankRay78/PatienceOS/assets/52075808/4ffa65a2-9818-4502-a2cf-ceee99b70e93)

## Playing with PatienceOS

### Windows 10 Host
I love Visual Studio and find it a far superior IDE compared to VS Code. Unfortunately, the lack of a Linux Visual Studio version means I'm tied to Windows, at least for now ([probably forever](https://developercommunity.visualstudio.com/t/Visual-Studio-for-Linux/360479)). This is my primary reason for using Windows as my development machine ('nix users, don't hate on me).

### Installation
Perform the following one-off installs, ideally in a virtual machine:

1. Microsoft .Net 8, Visual Studio 2022
2. MSYS2 MINGW64, see: [MSYS2-Installation](https://www.msys2.org/wiki/MSYS2-installation/)
3. QEMU, the native Windows binaries, see: [Download QEMU](https://www.qemu.org/download/#windows), nb. I used the latest Stefan Weil [64-bit Windows installer](https://qemu.weilnetz.de/w64/)

### Configuration

#### ILC, the Native .Net AOT compiler
ILC needs to be installed locally. It's a nuget package and will be present if you have configured and built at least one C# .Net project that emits native AOT code. I'm using version 8.0.1 of the IL compiler and so expect to see it installed in the nuget cache for my user, `info`, here: `C:\Users\info\.nuget\packages\runtime.win-x64.microsoft.dotnet.ilcompiler\8.0.1\tools`. If you don't see something similar, then perform the following steps in a temporary directory to create a temporary application, simply for the purpose of installing ILC locally:

```
dotnet new console -n MyConsoleApp
cd MyConsoleApp
dotnet add package Microsoft.DotNet.ILCompiler

EITHER:
- Load the project in Visual Studio and enable 'Publish native AOT', or
- Edit the csproj file and add <PublishAot>True</PublishAot> within the PropertyGroup

dotnet build
```
`ilc.exe` should now be installed here: `C:\Users\info\.nuget\packages\runtime.win-x64.microsoft.dotnet.ilcompiler\8.0.1\tools`

#### MSYS2 MINGW64
Open the MSYS2 MINGW64 console and perform the following actions:

1. A full package upgrade: `pacman -Syuu`, 
2. Install binutils: `pacman -S mingw-w64-x86_64-binutils` (required to get a Windows compiled `objcopy`)
3. Install NASM: `pacman -S mingw-w64-x86_64-nasm`

#### Build tool paths
Several tools are required in the build and link process. Update `src\setpath.cmd` to point to the correct install locations on your machine. The existing entries in the file will give you a good idea of where to find them, if you have followed the instructions above and accepted default install locations.

### Booting
1. Open `x64 Native Tools Command Prompt for VS 2022`
2. Set the correct tool paths in the console session, `src\setpath.cmd` (only do this once, after opening the command prompt)
3. Compile, link and boot, `src\build.cmd`

Patience OS, a baremetal C# kernel, should boot in QEMU.

## Developing PatienceOS

There are three different ways to build PatienceOS, each with a very different purpose in mind. These are:

File | Type | Purpose |
--- | --- | --- 
`build\build.cmd` | Windows Command script | Builds the kernel and boots in QEMU (see [Playing with PatienceOS](https://github.com/FrankRay78/PatienceOS#playing-with-patienceos) above for instructions)
`src\PatienceOS.NoStdLib.sln` | Visual Studio 2022 solution; only contains the PatienceOS project | Builds and links the kernel against the custom .Net runtime, `zerosharp.cs`. Handy for when you are coding PatienceOS within Visual Studio and want to quickly check building against the custom runtime types.
`src\PatienceOS.sln` | Visual Studio 2022 solution; contains the PatienceOS project and accompanying unit test project | Builds and links the kernel against the standard .Net 8.0 runtime. Allows you to run the PatienceOS unit tests within the built-in Visual Studio Test Explorer, as per any other unit test project. 

I'm not currently accepting pull requests as this is a personal learning project. However, please feel free to raise issues if you want to discuss a particular matter with your fellow GitHub users and/or fork the repository for your own purposes.

## Build toolchain

#### Commentary on the build process
Relying on Visual Studio specific, MSBuild or csproj files to build the kernel has been avoided in favour of directly calling the individual build tools. I want fine-grained control over the compile and link process, MSBuild feels like a poorly documented 'black box' that changes every .Net release in subtle ways that aren't clear, and I don't want to invest a huge amount of time developing a C# kernel only to find I can't build it in some future .Net release. 

I would have sincerely loved to use [bflat](https://github.com/bflattened/bflat) as my IL to native compiler, but even that didn't seem to allow intermediate object file output for class libraries, and I didn't want to raise an issue requesting the CLI options expose more of the underlying compiler switches. And so I've stuck to the approach demo'd in [zerosharp](https://github.com/MichalStrehovsky/zerosharp), namely a Windows command/batch file ([build.cmd](https://github.com/MichalStrehovsky/zerosharp/blob/master/no-runtime/build.cmd)). 

The following lines in the PatienceOS `src\build.cmd` are of particular note:

###### C# IL Compiler

`csc /debug:embedded /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 ../src/kernel.cs ../src/zerosharp.cs /out:kernel.ilexe /langversion:latest /unsafe`

```text
/noconfig: Don't reference the standard set of assemblies and configuration files.
/nostdlib: Don't reference the standard library assemblies.
/unsafe: Allow unsafe code blocks in C#, enabling pointers and other low-level constructs that aren't type-safe.
```

###### C# AOT Compiler

`ilc --targetos windows --targetarch x86 kernel.ilexe -g -o kernel.obj --systemmodule kernel --map kernel.map -O`

```text
--targetos windows: The code is intended to run on the Windows operating system.
--targetarch x86: The code is being compiled for the x86 architecture, typically used in 32-bit Windows systems.
```


Targeting machine-specific architectures can be done at the compiler call (eg. `csc /platform:x64` or `ilc --targetarch x86`) and/or the linker call (eg. `link /machine:x64`)

#### Commentary on the build environment
Believe me, I really did try to have a single environment for the end-to-end build process. It was much harder than I thought, and unfortunately, I've not managed to do it (yet). However, I have managed to get everything to run inside a single `x64 Native Tools Command Prompt for VS 2022`, no small feat. The build tools called, in order, are `csc` and `ilc` the .Net compilers; `nasm`, `ld` and `objcopy` from within MSYS2/MINGW; and then `qemu-system-i386`, installed natively on the host Windows 10 machine. 

It sounds mad, but Philipp Oppermann had the same experience when developing his Rust kernel, relying on various GNU tools that also made it difficult to build on macOS and Windows (nb. a major hurdle seems to be getting a native Windows build of GRUB and grub-mkrescue, see [here](https://github.com/intermezzOS/book/issues/53) and [here](https://forum.osdev.org/viewtopic.php?f=1&t=55959)). Then Philipp decided to re-write the entire toolchain in Rust for the second edition of his blog, see [Writing an OS in pure Rust](https://os.phil-opp.com/news/pure-rust/), thus making it possible to build the OS natively on Windows, macOS, and Linux **without any non-Rust dependendencies**.

Here are the various build environments I tried, and the reasons why they were discarded:

Environment | Findings
--- | --- 
`Windows only` | kernel.bin linker output is 'PE32 executable (console) Intel 80386, for MS Windows', which is not supported by QEMU direct kernel boot. Hence needing objcopy from MSYS2/MINGW64 to convert it to the elf32-i386 format.
`WSL only` | ilc, the .Net AOT compiler, doesn't seem to support 32-bit x86 compilation output on Linux, which I really want. OSDev Wiki agrees, saying *"If this is your first operating system project, you should do a 32-bit kernel first."*, see: [Bare Bones](https://wiki.osdev.org/Bare_Bones).
`MSYS2/MINGW64` | Heaps of issues with spaces in path names, see [mingw make can't handle spaces in path?](https://stackoverflow.com/questions/5999507/mingw-make-cant-handle-spaces-in-path) as an example. Unfortunately, no amount of escaping, 8.3 shortening or quotes could get the csc compiler running from its default install location, C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\Roslyn\csc.exe
`bflat` | bflat decides to output an executable when it finds a main() function, also seemingly without the intermediate object files. This is no good, given I also need to link in the compiled boot loader. (nb. with a bit of further effort, I suspect I might be able to include the bootloader asm directly in the csproj file, somehow)

## References
#### IL to Native compilation
Inspiration has been drawn from the following precursors to the AOT compiler we see in .Net 7/8:
* [bflat](https://github.com/bflattened/bflat) - C# as you know it but with Go-inspired tooling (small, self-contained, and native executables).
* [zerosharp](https://github.com/MichalStrehovsky/zerosharp) - Demo of the potential of C# for systems programming with the .NET native ahead-of-time compilation technology.
* [WDK.NET](https://github.com/ZeroLP/WDK.Net) - Windows Kernel Driver Development in C# with Windows Driver Kit (WDK).
* [IL2CPU](https://github.com/CosmosOS/IL2CPU) - IL2CPU is a compiler for .NET IL code to compile to assembly language for direct booting.
* [FlingOS](https://github.com/FlingOS/FlingOS) - An educational operating system written in C#. A great stepping stone from high to low-level development.

## Experiments
A working backlog of Frank's personal learning objectives for 2024 (and beyond?):
 
- [x] Ability to replace the standard Linux init process with a custom C application, compiled to bare metal and executable. (DONE, 18 Nov 2023, see: [Boot the Linux 6.x kernel in QEMU and run a custom C application as the init process](https://gist.github.com/FrankRay78/426011c03a7fb4f890eb5b4a068720c8))
- [X] Using an emulator, boot in 32-bit protected mode ~~using GRUB2~~ (nb. QEMU can boot directly into 32-bit protected mode, saving the faff of making an ISO image) and output 'hello world' by writing directly to the VGA video memory. (DONE, 4 Jan 2024, see: [Compiling a C# kernel to bare metal and booting in QEMU](https://frankray.net/blog/2024/01/compiling-a-csharp-kernel-to-bare-metal-and-booting-in-qemu/))
- [X] Basic terminal output (DONE, 10 Feb 2024, Displays a multiline splash screen/logo on boot, commit [c2850ae094a3db14cb4b74e465afa26ab3f4a49c](https://github.com/FrankRay78/PatienceOS/commit/c2850ae094a3db14cb4b74e465afa26ab3f4a49c))
- [ ] See if I can get the Microsoft linker, link, to output exactly the same file as the GNU linker, ld
- [ ] Advanced terminal output, including terminal sizes other than 80x25, buffered output and scrolling
- [X] Testable kernel components, including unit test framework and tests coverage (ideally leveraging an already established C# test framework eg. [NUnit](https://nunit.org/))
- [ ] Hardware interrupts and outputting keyboard keystrokes to the screen.
- [ ] Basic memory management ie. stack and heap allocation. Learn how this works underneath the covers for an AOT compiled C# kernel.
- [ ] Advanced memory management, including allocation, virtual page management and garbage collection.
- [ ] Loading an elf into memory and executing it. Loading multiple instances of the elf and executing them (what do they share, and how is that allocated, managed etc)
- [ ] Implement/port some useful routines from the C standard library ie. vfprintf ([master](https://git.musl-libc.org/cgit/musl/tree/src/stdio/vfprintf.c), [clone](https://github.com/BlankOn/musl/blob/master/src/stdio/printf.c))
- [ ] Implement custom TCPIP stack, see: [Let's code a TCP/IP stack](https://www.saminiir.com/lets-code-tcp-ip-stack-1-ethernet-arp/), [Level-IP](https://github.com/saminiir/level-ip), [tapip](https://github.com/chobits/tapip)
- [ ] Ultimately, host a TFTP server, see: [Tftp.Net C# Library](https://github.com/Callisto82/tftp.net), [Booting Arch Linux from PXE](https://www.saminiir.com/boot-arch-linux-from-pxe/)

## Blog posts
Chronological order:

1. 20231115 [Writing an OS in C#](https://frankray.net/blog/2023/11/writing-an-os-in-csharp-dotnet/)
2. 20240104 [Compiling a C# kernel to bare metal and booting in QEMU](https://frankray.net/blog/2024/01/compiling-a-csharp-kernel-to-bare-metal-and-booting-in-qemu/)
3. 20240209 [PatienceOS isn’t living up to its name](https://frankray.net/blog/2024/02/patienceos-isnt-living-up-to-its-name/)
