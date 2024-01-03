
# https://stackoverflow.com/questions/34687608/unrecognised-emulation-mode-elf-i386-on-mingw32
# https://stackoverflow.com/questions/53244181/ld-unsupported-pei-architecture-pei-i386-error-when-using-ld

ld -m i386pe -T linker.ld -o kernel.bin loader.o kernel.obj
objcopy -O elf32-i386 kernel.bin kernel.elf
qemu-system-i386 -kernel kernel.elf