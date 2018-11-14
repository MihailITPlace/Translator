format  PE console

operandA = 15
operandB = 7

entry   start
include 'win32ax.inc'
include 'api\kernel32.inc'
 
section '.code' code readable executable
start:
    ;cinvoke scanf, '%d', operandA
    ;cinvoke scanf, '%d', operandB
    mov eax, operandA
    add eax, operandB
    mov [sum], eax

    mov eax, operandA
    sub eax, operandB
    mov [difference], eax

    mov eax, operandA
    imul eax, operandB
    mov [composition], eax

    mov     eax, operandA
    mov     ebx, operandB
    cdq
    idiv    ebx
    mov [quotient], eax

    cinvoke printf, 'X = %d%cY = %d%c', operandA, 0x0A, operandB, 0x0A
    cinvoke printf, 'X + Y = %d%c', [sum], 0x0A
    cinvoke printf, 'X - Y = %d%c', [difference], 0x0A
    cinvoke printf, 'X * Y = %d%c', [composition], 0x0A
    cinvoke printf, 'X / Y = %d%c', [quotient], 0x0A
    cinvoke printf, '%s', msg, 0x0A

    cinvoke _getch
    invoke   ExitProcess, 0
 
section '.data' data readable writeable
sum dd ?
difference dd ?
composition dd ?
quotient dd ?
msg db 'This is a printf example',0

section '.idata' import data readable
library kernel32, 'kernel32.dll',\
        msvcrt, 'msvcrt.dll'
 
import msvcrt,\
       printf, 'printf', _getch,'_getch', scanf,'scanf'