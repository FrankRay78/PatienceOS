# PatienceOS
A baremetal C# kernel.

The repo name has been chosen to remind Frank this is a 12-month initial project (at least), as part of his [2024 professional development](https://frankray.net/blog/2023/11/writing-an-os-in-csharp-dotnet/) goals. The dopamine hit from quickly pushing out PR's, like he regularly gets from contributing to other OSS repos (eg. [spectre.console](https://github.com/spectreconsole/spectre.console)), simply won't be possible. Hence the need for patience, and perseverance.

## Objectives
A working set of things to achieve:
 
1. Ability to replace the standard Linux init process with a custom C application, compiled to bare metal and executable. (Done, 18 Nov 2023, see: [Boot the Linux 6.x kernel in QEMU and run a custom C application as the init process](https://gist.github.com/FrankRay78/426011c03a7fb4f890eb5b4a068720c8))
3. Using an emulator, boot in 32-bit protected mode ~~using GRUB2~~ (nb. QEMU can boot directly into 32-bit protected mode, saving the faff of making an ISO image) and output 'hello world' by writing directly to the VGA video memory.

## Environment setup

1. Install - .Net 7, Visual Studio 2022, Windows SDK (?), NASM, MSYS2 UCRT64 including qemu via pacman package manager
2. ILC, the Native .Net AOT compiler, needs to be installed locally eg. `dotnet add package Microsoft.DotNet.ILCompiler --version 7.0.14`
3. The path to ILC needs to be set as an environment variable called `ILCPATH` eg. `setx ILCPATH "C:\Users\frank\.nuget\packages\runtime.win-x64.microsoft.dotnet.ilcompiler\7.0.14\tools"` (no trailing slash)

## Build instructions

1. Open `x86 Native Tools Command Prompt for VS 2022`, change to `src` and run `build.cmd`
2. Open `MSYS2 UCRT64 Shell`, change to `src` and run `build_msys2.sh`

## Notes
Targeting machine-specific architectures can be done at the compiler call (eg. `csc /platform:x64`) and/or the linker call (eg. `link /machine:x64`)
