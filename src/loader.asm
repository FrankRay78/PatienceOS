global sthrow
 
extern kmain
 
MODULEALIGN       equ     1<<0
MEMINFO           equ     1<<1
FLAGS             equ     MODULEALIGN | MEMINFO
MAGIC             equ     0x1BADB002
CHECKSUM          equ     -(MAGIC + FLAGS)
 
section .text
 
align 4
dd MAGIC
dd FLAGS
dd CHECKSUM
 
sthrow:
    hlt
    jmp sthrow