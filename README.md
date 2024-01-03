# PatienceOS
A baremetal C# kernel.

The repo name has been chosen to remind Frank this is a 12 month initial project (at least), as part of his [2024 professional development](https://frankray.net/blog/2023/11/writing-an-os-in-csharp-dotnet/) goals. The domamine hit from quickly pushing out PR's, like he regularly gets from contributing to other OSS repos, simply won't be possible. Hence the need for patience, and perseverance.

## Objectives
A working set of things to achieve:
 
1. Ability to replace the standard Linux init process with a custom C application, compiled to bare metal and executable. (Done, 18 Nov 2023, see: [Boot the Linux 6.x kernel in QEMU and run a custom C application as the init process](https://gist.github.com/FrankRay78/426011c03a7fb4f890eb5b4a068720c8))
3. Using an emulator, boot in 32-bit protected mode using GRUB2 and output 'hello world' by writing directly to the VGA video memory.

## Build setup

Before running, `src\build.cmd`, ensure the following:

1. (.Net, Visual Studio, Windows SDK installs, details TBD)
2. ILC, the Native .Net AOT compiler, needs to be installed localled eg. `dotnet add package Microsoft.DotNet.ILCompiler --version 7.0.14`
3. The path to ILC needs to be set as an environment variable called `ILCPATH` eg. `setx ILCPATH "C:\Users\frank\.nuget\packages\runtime.win-x64.microsoft.dotnet.ilcompiler\7.0.14\tools"` (no trailing slash)

## Notes
Targeting machine-specific architectures can be done at the compiler call (eg. `csc /platform:x64`) and/or the linker call (eg. `link /machine:x64`)
