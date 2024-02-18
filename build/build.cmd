::
:: "Manual" build script that bypasses MSBuild and directly invokes the necessary tools.
:: 


:Clean
rmdir /S /Q ..\bin >nul 2>&1

@if "%1" == "clean" exit /B


:Build
mkdir ..\bin
cd ..\bin

csc /debug:embedded /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 ../src/PatienceOS.Kernel/kernel.cs ../src/PatienceOS.Kernel/Console.cs ../src/PatienceOS.Kernel/FrameBuffer.cs ../src/PatienceOS.Kernel/zerosharp.cs /out:kernel.ilexe /langversion:latest /unsafe || goto Error
ilc --targetos windows --targetarch x86 kernel.ilexe -g -o kernel.obj --systemmodule kernel --map kernel.map -O || goto Error
nasm -f win32 -o loader.obj ../src/loader.asm || goto Error

:: QEMU doesn't boot properly when I use the Microsoft linker, continually stuck in a BIOS loop looking for something to boot.
:: I suspect it's producing a slightly different output than the GNU `ld` command that follows.
:: link /debug /subsystem:console /machine:x86 /nodefaultlib /base:0x00200000 /ALIGN:4096 /entry:kernel_Program__Main /out:kernel.bin loader.obj kernel.obj || goto Error

ld -m i386pe -T ../src/linker.ld -o kernel.bin loader.obj kernel.obj || goto Error
objcopy -O elf32-i386 kernel.bin kernel.elf || goto Error
qemu-system-i386 -kernel kernel.elf || goto Error

cd ..\build
exit /B


:Error
@echo Tool failed.
cd ..\build
exit /B 1
