; kernel.asm
bits 32           ; nasm directive - 32 bit

extern __managed__Main

global _start

MODULEALIGN       equ     1<<0
MEMINFO           equ     1<<1
FLAGS             equ     MODULEALIGN | MEMINFO
MAGIC             equ     0x1BADB002
CHECKSUM          equ     -(MAGIC + FLAGS)

section .text
    ; Multiboot header
    align 4
    dd MAGIC
    dd FLAGS
    dd CHECKSUM

_start:
    cli                   ; block interrupts (nb. not necessary, as QEMU boots into 32-bit protected mode with interrupts disabled)
    mov esp, stack_space  ; set stack pointer


enablesse:
    ; Is SSE supported on this CPU?
    mov eax, 0x1
    cpuid
    test edx, 1<<25
    jnz .sse                   ; If SSE supported enable it.
.nosse:
    ; SSE not supported - do something like print an error and stop
    jmp $

.sse:
    ;now enable SSE and the like
    mov eax, cr0
    and ax, 0xFFFB             ; clear coprocessor emulation CR0.EM
    or ax, 0x2                 ; set coprocessor monitoring  CR0.MP
    mov cr0, eax
    mov eax, cr4
    or ax, 3 << 9              ; set CR4.OSFXSR and CR4.OSXMMEXCPT at the same time
    mov cr4, eax


    ; Call Main
    call __managed__Main

    ; Infinite loop
    hlt
    jmp $

section .bss
resb 8192                 ; 8KB for stack
stack_space: