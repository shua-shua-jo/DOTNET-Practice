# 5-Day Learning Plan for C# & .NET Mastery

## Day 1: Introduction to C# & .NET Ecosystem

**Goal:** Understand the basics of C#, its syntax, and the .NET framework.

### Topics to Cover:

- What is C#? [[Day 1 Notes#1. What is C ?|Notes]]
- What is .NET? (Difference between .NET Framework, .NET Core, and .NET 7+) [[Day 1 Notes#2. What is .NET?|Notes]]
- Installing .NET SDK and Visual Studio [[Day 1 Notes#**3. Installing .NET SDK and Visual Studio**|Notes]]
- First C# Program (Hello, World!) [[Day 1 Notes#**4. First C Program (Hello, World!)**|Notes]]
- Understanding C# Data Types, Variables, and Constants [[Day 1 Notes#**5. Understanding C Data Types, Variables, and Constants**|Notes]]
- Operators and Expressions [[Day 1 Notes#**6. Operators and Expressions**|Notes]]
- Control Structures (if-else, switch, loops) [[Day 1 Notes#**7. Control Structures (if-else, switch, loops)**|Notes]]
- Methods and Functions [[Day 1 Notes#**8. Methods and Functions**||Notes]]

### Hands-On Exercises:

- Install Visual Studio and create a simple Console App.
- Write a program that takes user input and prints a message.

---

## Day 2: Object-Oriented Programming (OOP) in C\#

**Goal:** Learn OOP principles and how they are implemented in C#.

### Topics to Cover:

- Classes and Objects [[Day 2 Notes#1. **Classes and Objects**|Notes]]
- Constructors and Destructors [[Day 2 Notes#2. **Constructors and Destructors**|Notes]]
- Access Modifiers (public, private, protected, internal, \*virtual) [[Day 2 Notes#3. **Access Modifiers**|Notes]]
- Properties and Auto-Properties [[Day 2 Notes#4. **Properties and Auto-Properties**|Notes]]
- Methods and Method Overloading [[Day 2 Notes#5. **Methods and Method Overloading**|Notes]]
- Static vs. Instance Members [[Day 2 Notes#6. **Static vs. Instance Members**|Notes]]
- Inheritance, Polymorphism, Abstraction, and Encapsulation [[Day 2 Notes#7. **Inheritance, Polymorphism, Abstraction, and Encapsulation**|Notes]]
- Interfaces vs. Abstract Classes [[Day 2 Notes#8. **Interfaces vs. Abstract Classes**|Notes]]

### Hands-On Exercises:

- Create a class `Person` with properties like `Name` and `Age`.
- Implement a derived class `Employee` that extends `Person`.
- Create an interface `IWorkable` and implement it in a class.

---

## Day 3: C# Fundamentals

**Goal:** Dive into advanced C# features to build robust applications.

### Topics to Cover:

- Exception Handling (try-catch-finally) [[Day 3 Notes#1. **Exception Handling (try-catch-finally)**|Notes]]
- Value Types vs Reference Types [[Day 3 Notes#2. **Value Types vs Reference Types**|Notes]]
  - Primitive Types [[Day 3 Notes#**Value Types**|Notes]]
  - Enum [[Day 3 Notes#**Enums (Enumerations)**|Notes]]
  - Struct [[Day 3 Notes#**Structs (Structures)**|Notes]]
  - Object / Class [[Day 3 Notes#**Reference Types**|Notes]]
- var VS dynamic [[Day 3 Notes#3. **var vs dynamic**|Notes]]
- Boxing vs Unboxing [[Day 3 Notes#4. **Boxing vs Unboxing**|Notes]]
- Pass-by-value VS Pass-by-reference [[Day 3 Notes#5. **Pass-by-value vs Pass-by-reference**|Notes]]
- Generics [[Day 3 Notes#6. **Generics**|Notes]]
- Collections (Arrays, List, Dictionary, Queue, Stack, the IEnumerable interface) [[Day 3 Notes#7. **Collections**|Notes]]
- Delegates and Events [[Day 3 Notes#8. **Delegates and Events**|Notes]]
  - Func\<Ti, To> [[Day 3 Notes#**Func<> Delegate**|Notes]]
  - Action\<Ti> [[Day 3 Notes#**Action<> Delegate**|Notes]]

### Hands-On Exercises:

- Write a program that reads a file and displays its content.
- Create an event-driven application using Delegates & Events.
- Implement a LINQ query to filter a list of students based on grades.

---

## Day 4: The .NET Base Class Library

### Topics to Cover:

- Working with Enumerables and Collections using System.Collections and System.Collections.Generic
- Language Integrated Query (LINQ)
- Working with Files and Streams with System.IO
  - Stream base class
  - FileStream
  - Memory Stream
- ADO.NET and the System.Data namespace
  - Connecting to a database
- Entity Framework
  - Deferred execution
  - Query syntax (LINQ) VS Extension Methods
  - IQueryable vs IEnumerable
- Connecting to HTTP services using HttpClient

ASP.NET CORE
MVC

Make an address book just like in phone;

---

# Key C# & .NET Terminologies

1. **.NET Runtime** - The execution environment for .NET applications.
2. **Common Language Runtime (CLR)** - The heart of .NET that manages code execution.
3. **Intermediate Language (IL)** - Compiled C# code before execution.
4. **Just-In-Time (JIT) Compiler** - Translates IL to machine code at runtime.
5. **Managed Code** - Code that runs under CLR supervision.
6. **Garbage Collection (GC)** - Automatic memory management in .NET.
7. **Namespaces** - Used to organize code.
8. **Assemblies** - Compiled code libraries (.dll or .exe).
9. **NuGet** - Package manager for .NET.
10. **Value Types vs. Reference Types** - How data is stored in memory.
11. **LINQ** - Querying collections using SQL-like syntax.
12. **Delegates** - References to methods used for callbacks.
13. **Events** - Used for implementing event-driven programming.
14. **Dependency Injection (DI)** - Injecting dependencies instead of hardcoding them.
15. **Entity Framework Core (EF Core)** - ORM for database access.
16. **REST API** - Web services using HTTP methods.
17. **Middleware** - Components in the ASP.NET Core request pipeline.
18. **JWT (JSON Web Token)** - Authentication standard.
19. **SOLID Principles** - Best practices for maintainable code.
20. **Unit Testing** - Writing tests to ensure code correctness.
