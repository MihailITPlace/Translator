format  PE console

entry   start
include 'win32ax.inc'
include 'api\kernel32.inc'

section '.data' data readable writeable
x dd ?
y dd ?
res1 dd ?
res2 dd ?
res3 dd ?
res4 dd ?
string0 db 'input x: ',0
string1 db 'input y: ',0
string2 db 'x + y = ',0
string3 db 'x - y = ',0
string4 db 'x * y = ',0
string5 db 'x / y = ',0
section '.code' code readable executable
start:
    
    invoke   ExitProcess, 0

section '.idata' import data readable
library kernel32, 'kernel32.dll',\
        msvcrt, 'msvcrt.dll'
 
import msvcrt,\
       printf, 'printf', _getch,'_getch', scanf,'scanf'
