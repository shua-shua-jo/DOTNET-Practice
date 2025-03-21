# **1. What is C#?**

C# (pronounced "C-sharp") is a modern, object-oriented programming language developed by Microsoft. It’s part of the .NET ecosystem and is commonly used for building applications for Windows, web, mobile, and cloud platforms. C# is designed to be simple, powerful, and type-safe, offering high-level features like garbage collection and type checking.

**Key Features of C#:**

- Object-Oriented (OOP)
- Strongly typed
- Easy to learn with a clean syntax
- Supports modern programming paradigms (LINQ, async/await, etc.)
# **2. What is .NET?**

The **.NET** framework is a platform for building and running applications. The framework provides a vast class library and runtime environment for executing code. It supports various programming languages (such as C#, Visual Basic, F#) and is cross-platform.

- **.NET Framework**: The traditional, Windows-only version of the .NET platform.
- **.NET Core**: A cross-platform, open-source version of .NET designed to work on Windows, macOS, and Linux.
- **.NET 7+**: The unification of .NET Framework and .NET Core into a single platform, continuing to be cross-platform and open-source.
# **3. Installing .NET SDK and Visual Studio**

**To install .NET SDK:**

1. Go to the official [Download .NET SDK](https://dotnet.microsoft.com/download).
2. Download the SDK for your operating system.
3. Install it and verify by running `dotnet --version` in your terminal/command prompt.

**To install Visual Studio:**

1. Go to the official [Visual Studio download page](https://visualstudio.microsoft.com/downloads/).
2. Download the **Community Edition** (free) or any other version you prefer.
3. During the installation, make sure to select the **.NET desktop development** workload to get all the necessary tools for C# development.

# **4. First C# Program (Hello, World!)**

Once Visual Studio is installed, you can create your first C# program.

**Steps to create a Console Application:**

1. Open Visual Studio.
2. Create a new project.
3. Select **Console App**.
4. Choose **C#** and click **Next**.
5. Name your project (e.g., `FirstApp`) and click **Create**.

**Hello, World! Code:**

```C#
using System;

class Program {
	static void Main() {
		Console.WriteLine("Hello, World!");
	}
}
```

- This code outputs the message `Hello, World!` to the console.
# **5. Understanding C# Data Types, Variables, and Constants** 

In C#, data types define the kind of data a variable can store.

## **Common Data Types:**

- `int` (Integer numbers)
- `double` (Floating-point numbers)
- `char` (Single character)
- `bool` (True or False)
- `string` (Text)
- `var` (Implicitly typed variable)

**Declaring Variables:**

```C#
int age = 30;
double price = 19.99;
char grade = 'A';
string name = "John";
bool isActive = true;
```

## **Integral Numeric Types:**

| C# type/keyword | Range                                                   | Size                              | .NET type                                                                     |
| --------------- | ------------------------------------------------------- | --------------------------------- | ----------------------------------------------------------------------------- |
| `sbyte`         | -128 to 127                                             | Signed 8-bit integer              | [System.SByte](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte)     |
| `byte`          | 0 to 255                                                | Unsigned 8-bit integer            | [System.Byte](https://learn.microsoft.com/en-us/dotnet/api/system.byte)       |
| `short`         | -32,768 to 32,767                                       | Signed 16-bit integer             | [System.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16)     |
| `ushort`        | 0 to 65,535                                             | Unsigned 16-bit integer           | [System.UInt16](https://learn.microsoft.com/en-us/dotnet/api/system.uint16)   |
| `int`           | -2,147,483,648 to 2,147,483,647                         | Signed 32-bit integer             | [System.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)     |
| `uint`          | 0 to 4,294,967,295                                      | Unsigned 32-bit integer           | [System.UInt32](https://learn.microsoft.com/en-us/dotnet/api/system.uint32)   |
| `long`          | -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 | Signed 64-bit integer             | [System.Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64)     |
| `ulong`         | 0 to 18,446,744,073,709,551,615                         | Unsigned 64-bit integer           | [System.UInt64](https://learn.microsoft.com/en-us/dotnet/api/system.uint64)   |
| `nint`          | Depends on platform (computed at runtime)               | Signed 32-bit or 64-bit integer   | [System.IntPtr](https://learn.microsoft.com/en-us/dotnet/api/system.intptr)   |
| `nuint`         | Depends on platform (computed at runtime)               | Unsigned 32-bit or 64-bit integer | [System.UIntPtr](https://learn.microsoft.com/en-us/dotnet/api/system.uintptr) |

### **Integer literals**

Integer literals can be

- _decimal_: without any prefix
- _hexadecimal_: with the `0x` or `0X` prefix
- _binary_: with the `0b` or `0B` prefix

The following code demonstrates an example of each:

```C#
var decimalLiteral = 42;
var hexLiteral = 0x2A;
var binaryLiteral = 0b_0010_1010;
```

The preceding example also shows the use of `_` as a _digit separator_. You can use the digit separator with all kinds of numeric literals.

**Constants:** Use `const` for values that do not change.

```c#
const double Pi = 3.14159;
```

## **Floating-point numeric types**

|C# type/keyword|Approximate range|Precision|Size|.NET type|
|---|---|---|---|---|
|`float`|±1.5 x 10−45 to ±3.4 x 1038|~6-9 digits|4 bytes|[System.Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)|
|`double`|±5.0 × 10−324 to ±1.7 × 10308|~15-17 digits|8 bytes|[System.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)|
|`decimal`|±1.0 x 10-28 to ±7.9228 x 1028|28-29 digits|16 bytes|[System.Decimal](https://learn.microsoft.com/en-us/dotnet/api/system.decimal)|
### **Real literals**

The type of a real literal is determined by its suffix as follows:

- The literal without suffix or with the `d` or `D` suffix is of type `double`
- The literal with the `f` or `F` suffix is of type `float`
- The literal with the `m` or `M` suffix is of type `decimal`

The following code demonstrates an example of each:

```C#
double d = 3D;
d = 4d;
d = 3.934_001;

float f = 3_000.5F;
f = 5.4f;

decimal myMoney = 3_000.5m;
myMoney = 400.75M;
```

The preceding example also shows the use of `_` as a _digit separator_. You can use the digit separator with all kinds of numeric literals.
## Implicit numeric conversions

The following table shows the predefined implicit conversions between the built-in numeric types:

Expand table

|From|To|
|---|---|
|[sbyte](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`short`, `int`, `long`, `float`, `double`, `decimal`, or `nint`|
|[byte](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`short`, `ushort`, `int`, `uint`, `long`, `ulong`, `float`, `double`, `decimal`, `nint`, or `nuint`|
|[short](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`int`, `long`, `float`, `double`, or `decimal`, or `nint`|
|[ushort](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`int`, `uint`, `long`, `ulong`, `float`, `double`, or `decimal`, `nint`, or `nuint`|
|[int](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`long`, `float`, `double`, or `decimal`, `nint`|
|[uint](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`long`, `ulong`, `float`, `double`, or `decimal`, or `nuint`|
|[long](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`float`, `double`, or `decimal`|
|[ulong](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`float`, `double`, or `decimal`|
|[float](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types)|`double`|
|[nint](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`long`, `float`, `double`, or `decimal`|
|[nuint](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)|`ulong`, `float`, `double`, or `decimal`|
# **6. Operators and Expressions**

Operators are symbols used to perform operations on variables.

## **Arithmetic Operators:**

**Increment operator `++`**

The unary increment operator `++` increments its operand by 1. The operand must be a variable, a [property](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties) access, or an [indexer](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/) access.

The increment operator is supported in two forms: the postfix increment operator, `x++`, and the prefix increment operator, `++x`.

**Postfix increment operator**

The result of `x++` is the value of `x` _before_ the operation, as the following example shows:

```C#
int i = 3;
Console.WriteLine(i);   // output: 3
Console.WriteLine(i++); // output: 3
Console.WriteLine(i);   // output: 4
```

 **Prefix increment operator**

The result of `++x` is the value of `x` _after_ the operation, as the following example shows:

```c#
double a = 1.5;
Console.WriteLine(a);   // output: 1.5
Console.WriteLine(++a); // output: 2.5
Console.WriteLine(a);   // output: 2.5
```

- `+`, `-`, `*`, `/`, `%` (Addition, Subtraction, Multiplication, Division, Modulus)

## **Comparison Operators:**

- `==`, `!=`, `<`, `>`, `<=`, `>=` (Equal, Not Equal, Less Than, Greater Than, Less Than or Equal, Greater Than or Equal)

## **Logical Operators:**

- `&&` (AND), `||` (OR), `!` (NOT), `^` (EXCLUSIVE OR)

## **Assignment Operators:**

- `=`, `+=`, `-=`, `*=`, `/=`

## **Bitwise and shift Operations**

The bitwise and shift operators include unary bitwise complement, binary left and right shift, unsigned right shift, and the binary logical AND, OR, and exclusive OR operators. These operands take operands of the [integral numeric types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types) or the [char](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/char) type.

- Unary [`~` (bitwise complement)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#bitwise-complement-operator-) operator
- Binary [`<<` (left shift)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#left-shift-operator-), [`>>` (right shift)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#right-shift-operator-), and [`>>>` (unsigned right shift)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#unsigned-right-shift-operator-) operators
- Binary [`&` (logical AND)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-and-operator-), [`|` (logical OR)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-or-operator-), and [`^` (logical exclusive OR)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#logical-exclusive-or-operator-) operators

# **7. Control Structures (if-else, switch, loops)**

Control structures are used to alter the flow of execution based on certain conditions.

**if-else:**

```c#
int number = 10;
if (number > 0) {
	Console.WriteLine("Positive number");
} else {
	Console.WriteLine("Non-positive number");
}
```

**switch:**

```c#
int day = 3;
switch (day) { 
	case 1: Console.WriteLine("Monday"); break;
	case 2: Console.WriteLine("Tuesday"); break;
	case 3: Console.WriteLine("Wednesday"); break;
	default: Console.WriteLine("Invalid day"); break; 
}
```

**Loops:**

- **for loop** (for a known number of iterations):

```c#
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}
```

- **while loop** (for an unknown number of iterations, based on a condition):
```c#
int count = 0;
while (count < 5)
{
    Console.WriteLine(count);
    count++;
}
```

- **foreach loop** (used to iterate over collections like arrays or lists):

```c#
string[] colors = { "Red", "Green", "Blue" };
foreach (string color in colors)
{
    Console.WriteLine(color);
}
```
### **8. Methods and Functions**

A method is a block of code that performs a specific task. You can define methods to make your code modular and reusable.

**Method Declaration:**

```c#
// Syntax: return_type MethodName(parameters) { }
static void Greet(string name)
{
    Console.WriteLine($"Hello, {name}!");
}

static int AddNumbers(int a, int b)
{
    return a + b;
}
```

**Calling Methods:**

```C#
Greet("Alice");
int sum = AddNumbers(5, 7);
Console.WriteLine(sum); // Output: 12
```
### **Summary:**

- **C#** is a modern, object-oriented programming language developed by Microsoft.
- The **.NET** framework provides a platform for building applications across different platforms (Windows, macOS, Linux).
- I learned how to install **.NET SDK** and **Visual Studio**.
- I wrote a simple **Hello, World!** program in C#.
- I explored **data types**, **variables**, **constants**, **operators**, and **control structures** (if-else, switch, loops).
- I got an introduction to **methods/functions** in C# to organize your code better.