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