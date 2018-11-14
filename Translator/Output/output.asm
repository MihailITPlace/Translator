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
section '.code' code readable executable
start:
cinvoke scanf, '%d', x
cinvoke scanf, '%d', y

    
    invoke   ExitProcess, 0

section '.idata' import data readable
library kernel32, 'kernel32.dll',\
        msvcrt, 'msvcrt.dll'
 
import msvcrt,\
       printf, 'printf', _getch,'_getch', scanf,'scanf'
