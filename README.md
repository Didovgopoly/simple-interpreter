# Simple Interpreter
Intereter which alow to calculate simple expressions and assign the result to variable.
```
a = 7 + 5
b = a * 2
```
Supported operations: +, -, *, /, ()

## Grammar description
```
assignment = variable "=" sum
sumOp = "+" | "-"
sum = product { sumOp product }
productOp = "*" | "/"
product = statement { productOp statement }
statement = variable | number | "(" sum ")"
variable = letter { letter }
letter = "A" | "B" | "C" | "D" | "E" | "F" | "G"
       | "H" | "I" | "J" | "K" | "L" | "M" | "N"
       | "O" | "P" | "Q" | "R" | "S" | "T" | "U"
       | "V" | "W" | "X" | "Y" | "Z" | "a" | "b"
       | "c" | "d" | "e" | "f" | "g" | "h" | "i"
       | "j" | "k" | "l" | "m" | "n" | "o" | "p"
       | "q" | "r" | "s" | "t" | "u" | "v" | "w"
       | "x" | "y" | "z" 
number = digit { digit }
digit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9"
```
  
## Building and running
1. Download .Net Core SDK from https://dotnet.microsoft.com/download
1. In CLI run `dotnet build Parser.sln`
1. In bin\netcoreapp3.1\ you will find exe and dll file
   1. run SimpleInterpreter.exe
   1. or execute `dotnet SimpleInterpreter.dll` in CLI

To run unit tests run `dotnet test Parser.sln -v n`

