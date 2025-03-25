# 1. **Exception Handling (try-catch-finally)**

Exception handling is essential for managing runtime errors and ensuring the application continues to function smoothly.

- **try**: Contains the code that might throw an exception.
    
- **catch**: Catches the exception and allows you to handle it.
    
- **finally**: A block of code that will run after the try block, regardless of whether an exception was thrown or not. Useful for cleanup.
    

**Example:**

```c#
try {
	int[] numbers = { 1, 2, 3 };
	Console.WriteLine(numbers[5]); // This will throw an exception.
} catch (IndexOutOfRangeException ex) {    
	Console.WriteLine($"Exception caught: {ex.Message}");
} finally {
	Console.WriteLine("This block is always executed.");
}
```
# 2. **Value Types vs Reference Types**

In C#, there are two categories of types: **value types** and **reference types**.
## **Value Types**

**Definition**:
- Value types hold data directly. When you assign a value type to another, a **copy** of the value is created. This means each variable has its own independent copy of the data.

**Characteristics**:

- They are stored on the **stack** (for performance reasons).
	
- When you assign one value type variable to another, the value is **copied** (i.e., it doesn’t reference the same memory location).
	
- Modifying one variable will not affect the other since each has its own copy of the data.
	

**Examples**:

- **Primitive types**: `int`, `float`, `char`, `bool`, `decimal`, etc.
	
- **Structs**: `DateTime`, `Point`, custom structs (e.g., `struct MyStruct`).
	
- **Enumerations**: `enum`.
	
### **Structs (Structures)**

A **struct** in C# is a value type that is used to define custom data types. It is similar to a class but has some key differences:

- Structs are **value types**, meaning they are stored on the **stack** and are passed by value (a copy is made when assigned or passed to a method).
    
- Structs **cannot** inherit from other structs or classes (they cannot use inheritance).
    
- They **do not** support a default constructor (but you can define parameterized constructors).
    
- Since they are value types, structs have a **faster memory allocation** and are typically used for small data types.
    

**Key characteristics**:

- Typically used for small, lightweight data structures.
    
- Can have fields, methods, properties, and events.
    

#### **Example of a Struct:**

```c#
using System;

public struct Point {
    public int X;
    public int Y;

    // Parameterized constructor
    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public void Display() {
        Console.WriteLine($"Point: ({X}, {Y})");
    }
}

class Program {
    static void Main() {
        // Creating an instance of the struct
        Point p1 = new Point(10, 20);
        p1.Display(); // Output: Point: (10, 20)

        // Structs are value types, so when assigned, a copy is made
        Point p2 = p1;
        p2.X = 30; // Modify p2, p1 remains unchanged
        Console.WriteLine($"p1: ({p1.X}, {p1.Y})"); // Output: p1: (10, 20)
        Console.WriteLine($"p2: ({p2.X}, {p2.Y})"); // Output: p2: (30, 20)
    }
}
```

**Explanation**:

- We define a struct `Point` with two fields `X` and `Y`.
    
- It also has a constructor and a method `Display()` to show the values.
    
- When we create a new `Point` and assign it to `p2`, it makes a **copy** because structs are **value types**. Changes to `p2` do not affect `p1`.
---
### **Enums (Enumerations)**

An **enum** in C# is a special type that allows you to define a set of named integral constants. Enums are **value types** and can be used to represent a collection of related values, often with names that are easier to understand and work with than numeric literals.

#### **Key Characteristics**:

- By default, the underlying type of an enum is **int**, starting at 0.
    
- You can explicitly specify the underlying type (e.g., `byte`, `long`).
    
- Enums make your code more readable and maintainable by replacing numeric constants with descriptive names.
    

#### **Example of an Enum:**

```c#
using System;

public enum DaysOfWeek {
    Sunday = 1,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

class Program {
    static void Main() {
        // Assigning an enum value
        DaysOfWeek today = DaysOfWeek.Monday;
        
        // Printing the enum value
        Console.WriteLine($"Today is: {today}"); // Output: Today is: Monday

        // Enum values are integers underneath
        int dayNumber = (int)DaysOfWeek.Friday;
        Console.WriteLine($"Friday is day number: {dayNumber}"); // Output: Friday is day number: 6

        // You can also get the name of an enum value
        Console.WriteLine($"The name of day 4 is: {Enum.GetName(typeof(DaysOfWeek), 4)}"); // Output: The name of day 4 is: Wednesday
    }
}
```

**Explanation**:

- We define an enum `DaysOfWeek` with values for each day of the week, starting from 1 (instead of the default 0).
    
- Enums are easier to work with than using raw numbers like `1` for Sunday, `2` for Monday, etc., because they have meaningful names.
    
- We demonstrate how to get the numeric value of an enum (`(int)DaysOfWeek.Friday`), and how to get the name of an enum value using `Enum.GetName`.
    
---
### **Enum with Custom Values**

You can also assign custom values to the enum constants to better suit your needs (e.g., specific days of the week starting from a different number, or for other use cases where the numeric values are important).

#### **Example of Enum with Custom Values:**

```C#
using System;

public enum ErrorCode {
    Success = 0,
    NotFound = 404,
    BadRequest = 400,
    InternalServerError = 500
}

class Program {
    static void Main() {
        ErrorCode error = ErrorCode.NotFound;
        
        if (error == ErrorCode.NotFound) {
            Console.WriteLine("Resource not found! Error code: 404");
        }

        // Displaying the numeric value of the enum
        Console.WriteLine($"Error Code for NotFound: {(int)ErrorCode.NotFound}"); // Output: Error Code for NotFound: 404
    }
}

```

**Explanation**:

- Here, we define an `ErrorCode` enum with specific HTTP status codes like `404` for "Not Found" and `500` for "Internal Server Error".
    
- You can check the error code using the enum’s name (e.g., `ErrorCode.NotFound`) instead of using raw numbers like `404`.
    
- You can also access the integer values directly (e.g., `(int)ErrorCode.NotFound`).
#### **Memory Allocation (Value Types)**

- Value types are allocated on the **stack**, which is a region of memory that stores variables.
	
- The stack is faster than the heap but has limited space and is automatically cleared once the scope is exited.
	

**Example:**

```c#
int a = 5;  // Value type, directly holds the value
int b = a;  // A copy of 'a' is made

b = 10;  // Change 'b', but 'a' remains the same
Console.WriteLine(a);  // Outputs: 5
Console.WriteLine(b);  // Outputs: 10
```

In this example:

- `a` is a value type, so when we assign it to `b`, a **copy** of the value is made.
    
- Changing `b` does not affect `a`.
        
## **Reference Types**

**Definition**:  
- Reference types store a reference (or memory address) to the data. When you assign a reference type to another, both variables refer to the **same object** in memory. Changes to one will affect the other because they point to the same data.

**Characteristics**:

- They are stored on the **heap** (which is a larger pool of memory used for dynamic memory allocation).
    
- When you assign one reference type variable to another, both variables point to the same memory location.
    
- Modifying one variable will affect the other because they are essentially two references to the same object.
    

**Examples**:

- **Class types**: `class`, `object`.
    
- **Arrays**: Even if an array is a value type (because it's stored in a variable), the elements it holds are reference types.
    
- **Delegate**: A reference type that represents a method.
    
- **String**: Although strings in C# behave like reference types, they are immutable, so operations on strings actually create new instances rather than modifying the original string.
    

#### **Memory Allocation (Reference Types)**

- Reference types are allocated on the **heap**.
    
- The heap is larger and more flexible than the stack but slower in terms of access time.
    
- Garbage collection is used to automatically manage memory for reference types when they are no longer in use.
#### Example:

```c#
class Person
{
    public string Name;
}

Person p1 = new Person();
p1.Name = "Alice";  // p1 holds a reference to the Person object

Person p2 = p1;  // p2 now refers to the same Person object as p1

p2.Name = "Bob";  // Changing p2 also changes p1, as both reference the same object
Console.WriteLine(p1.Name);  // Outputs: Bob
Console.WriteLine(p2.Name);  // Outputs: Bob
```
In this example:

- Both `p1` and `p2` are references to the **same object** in memory.
    
- Changing `p2.Name` also affects `p1.Name`, because they both point to the same `Person` object on the heap.
### **Key Differences Between Value Types and Reference Types**

|**Aspect**|**Value Types**|**Reference Types**|
|---|---|---|
|**Memory Allocation**|Stored on the **stack**|Stored on the **heap**|
|**Assignment Behavior**|When assigned, a **copy** of the value is created|When assigned, the **reference** (address) is copied|
|**Copying**|Copies the value|Copies the reference, not the actual object|
|**Nullability**|Cannot be null (except nullable value types)|Can be null, meaning no object reference|
|**Performance**|Generally faster (due to stack allocation)|Generally slower (due to heap allocation and garbage collection)|
|**Types**|`int`, `char`, `struct`, `enum`, `bool`|`class`, `object`, `string`, `array`, `delegate`|

### **Nullable Value Types**

Value types (like `int`, `bool`, etc.) cannot be `null`. However, you can allow value types to be `null` using the **nullable type** syntax (`?`).

**Example**:

```c#
int? nullableInt = null;  // Nullable int
if (nullableInt.HasValue) {     
	Console.WriteLine(nullableInt.Value);  // Access the value if not null
} else {
	Console.WriteLine("The value is null");
}
```

Here, `int?` allows the value type `int` to hold `null`, which can be useful in situations like database queries where a value might be missing.

---

### **Common Pitfalls and Considerations**

1. **Unexpected Behavior with Reference Types**:
    
    - When working with reference types, be mindful of unintended side effects when modifying one reference, as it can affect all references pointing to the same object.
        
    - Always check if a reference type is `null` before using it to avoid `NullReferenceException`.
        
2. **Performance Considerations**:
    
    - **Value types** are faster because they are stored on the stack and don’t require garbage collection.
        
    - **Reference types** require more memory and are slower because they are stored on the heap and need to be managed by the garbage collector.
        
3. **Understanding Mutability**:
    
    - **Reference types** can often be **mutable**, which means the data they point to can change.
        
    - **Value types** are usually **immutable** (unless the type is explicitly defined as mutable, like a struct).

# 3. **var vs dynamic**

**var:**
    
- Implicitly typed variable.
	
- Type is determined at compile time.
	
- Cannot be changed once assigned.

```c#
var num = 10; // num is of type int.
```
    
**dynamic:**
    
- Type is determined at runtime.
	
- Useful when the type is not known until execution.

```c#
dynamic obj = 10;
obj = "Hello"; // obj can change type at runtime.
```

 **Difference:**

- `var` requires type inference at compile-time, while `dynamic` defers type checking until runtime.
# **4. Boxing vs Unboxing**

This concept is particularly important for **value types** when interacting with **reference types**.

- **Boxing**: Converting a **value type** to a **reference type** (object).
    
- **Unboxing**: Converting a **reference type** (object) back to a **value type**.
    

**Boxing Example:**

```c#
int num = 42; // Value type
object obj = num; // Boxing: num is wrapped in an object (reference type)
```

In this case, the value `42` (a value type) is boxed into an object, meaning a new object is created on the heap that holds the value.

**Unboxing Example:**

```c#
object obj = 42;  // Boxing
int num = (int)obj;  // Unboxing: Extracting the value back from the object
```

In unboxing, the object reference is cast back to the original value type.
# 5. **Pass-by-value vs Pass-by-reference**

**Pass-by-value**: The method receives a copy of the original value (no changes to the original).
	
- A copy of the actual data is passed to the method.
    
- Changes to the parameter inside the method do not affect the original variable.
    
```c#
void Increment(int a) {
	a++; 
}

int num = 5; 
Increment(num); 
Console.WriteLine(num); // Output: 5
```
    
**Pass-by-reference**: The method can modify the original value.
	
- The method works with the original data (not a copy).
    
- Changes made inside the method affect the original variable.
    
- Use `ref` or `out` keywords.

```c#
void Increment(ref int a) { 
	a++; 
}

int num = 5; 
Increment(ref num); 
Console.WriteLine(num); // Output: 6
```
# **6. Generics**

Generics allow you to define a class, method, or interface with a placeholder for the data type, which improves type safety and performance.

**Benefits:**

- Type safety (no casting needed).
    
- Code reusability.
    
- Performance (avoids boxing/unboxing).

**Example:**
\[1\]
```C#
public class Box<T> {    
	public T Value { get; set; }
	
	public Box(T value) { 
	        Value = value; 
	}
}

var box = new Box<int>(10);
Console.WriteLine(box.Value); // Output: 10
```
\[2\]
```c#
public T Add<T>(T a, T b)
{
    return (dynamic)a + (dynamic)b; // Works for any type that supports addition.
}
```
# 7. **Collections**

### **Arrays:** Fixed-size collection of elements of the same type.
    
```c#
int[] numbers = {1, 2, 3, 4};
// or
int[] numbers = new int[3] { 1, 2, 3 };
```
    
### **List:** Dynamic array-like structure with methods to add/remove items.
    
    
```c#
List<int> list = new List<int> {1, 2, 3};
list.Add(4); // Adds 4 to the list
```
    
### **Dictionary:** Key-value pair collection, optimized for lookup by key.
    
    
```c#
Dictionary<string, int> dict = new Dictionary<string, int>
{
	{ "apple", 1 },
	{ "banana", 2 } 
};
```
    
### **Queue:** FIFO (First-In-First-Out) collection.
	
    
```c#
Queue<string> queue = new Queue<string>(); queue.Enqueue("First");
queue.Enqueue("Second");
```
    
### **Stack:** LIFO (Last-In-First-Out) collection.
	
    
```c#
Stack<string> stack = new Stack<string>();
stack.Push("First");
stack.Push("Second");
```
    
### **IEnumerable interface:** Represents any collection that can be enumerated (iterated).
    
- Most collections like `List`, `Array`, and `Dictionary` implement `IEnumerable`.
        
    
```c#
IEnumerable<int> numbers = new List<int> {1, 2, 3};
foreach (var num in numbers)
{
    Console.WriteLine(num);
}
```
# 8. **Delegates and Events**

### **Delegates:** A reference type that defines a method signature and can reference one or more methods.
    
- Type-safe function pointers.
	
- Used to define methods that can be called dynamically.
	
- Example:
	
	
```c#
delegate void Greet(string name);
Greet greetDelegate = name => Console.WriteLine($"Hello, {name}");
greetDelegate("Alice");
```
        
### **Events:** Allow for a publisher-subscriber pattern.
    
- Based on delegates, used to implement event-driven programming.
	
- Example of a simple event:
	
	csharp
	
	Copy
	
	`public delegate void EventHandler(); public event EventHandler OnEventOccurred;  public void TriggerEvent() {     OnEventOccurred?.Invoke(); }`
	
- **Func<T1, T2>:** A delegate type that returns a value and takes parameters.
	
	csharp
	
	Copy
	
	`Func<int, int, int> add = (a, b) => a + b;`
	
- **Action<T1>:** A delegate type that returns void and takes parameters.
	
	csharp
	
	Copy
	
	`Action<string> printMessage = message => Console.WriteLine(message);`