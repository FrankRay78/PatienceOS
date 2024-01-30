::
:: "Manual" build script that bypasses MSBuild and directly invokes the necessary tools.
:: Good to show how things get hooked up together, but redundant with the project file.
::
:: The tools are:
::
:: * CSC, the C# compiler
::   Opening a "x64 Native Tools Command Prompt for VS 2019" will place csc.exe on your PATH.
:: * ILC, the Native AOT compiler
::   If you use the project file to build this sample at least once, you can find ILC
::   in your NuGet cache. It will be somewhere like
::   C:\Users\username\.nuget\packages\runtime.win-x64.microsoft.dotnet.ilcompiler\7.0.0-alpha.1.21430.2
:: * Linker
::   This is the platform linker. "x64 Native Tools Command Prompt for VS 2019" will place
::   the linker on your PATH.
::

@del kernel.ilexe >nul 2>&1
@del kernel.obj >nul 2>&1
@del kernel.exe >nul 2>&1
@del kernel.map >nul 2>&1
@del kernel.pdb >nul 2>&1
@del kernel.ilk >nul 2>&1
@del kernel.bin >nul 2>&1
@del kernel.elf >nul 2>&1
@del loader.obj >nul 2>&1

@if "%1" == "clean" exit /B

csc /define:WINDOWS /debug:embedded /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 kernel.cs /out:kernel.ilexe /langversion:latest /unsafe || goto Error
ilc --targetos windows --targetarch x86 kernel.ilexe -g -o kernel.obj --systemmodule kernel --map kernel.map -O || goto Error
nasm -f win32 -o loader.obj loader.asm || goto Error
link /debug /subsystem:console /machine:x86 /nodefaultlib /base:0x00200000 /ALIGN:4096 /entry:kernel_Program__Main /out:kernel.bin loader.obj kernel.obj || goto Error
objcopy -O elf32-i386 kernel.bin kernel.elf || goto Error
qemu-system-i386 -kernel kernel.elf || goto Error

@goto :EOF

:Error
@echo Tool failed.
exit /B 1
