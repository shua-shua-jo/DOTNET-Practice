# 1. **Classes and Objects**

- **Class**: A blueprint for creating objects, defining properties, and methods.
- **Object**: An instance of a class.

	Example:
```c#
	public class Person {
		public string Name { get; set; }
		public int Age { get; set; }
	}
		// Create object
	Person person1 = new Person();
	person1.Name = "John";
	person1.Age = 30;
```
# 2. **Constructors and Destructors**

- **Constructor**: Special method used to initialize objects.
    
- **Destructor**: Used for cleanup when an object is no longer needed (usually not needed in C# due to garbage collection).
    
    Example:
```c#
	public class Person {
	    public string Name { get; set; }
	    
	    // Constructor
	    public Person(string name) {
	        Name = name;
	    }
	    
	    // Destructor
	    ~Person() {
	        Console.WriteLine("Object is being destroyed");
	    }
	}
```
# 3. **Access Modifiers**

- **public**: Accessible from anywhere.
    
- **private**: Accessible only within the class.
    
- **protected**: Accessible within the class and its derived classes.
    
- **internal**: Accessible within the same assembly.
    
- **virtual**: Used to declare a method that can be overridden in a derived class.
    
    Example:
```c#
	public class Person	{
		public string Name { get; set; }
		private int age;  // Only accessible within the class
	
		// Accessible in derived classes
		protected void SetAge(int age) {
			this.age = age;
		}
	}
```

# 4. **Properties and Auto-Properties**

- **Properties**: Provide a way to read, write, or compute values of private fields.
    
- **Auto-Properties**: Shortcut for defining properties that automatically create a backing field.
    
    Example:
```c#
	public class Person {
	    public string Name { get; set; }  // Auto-property
	    public int Age { get; set; }  // Auto-property
	}
```

# 5. **Methods and Method Overloading**

- **Method**: A function defined in a class that performs some operation.
    
- **Method Overloading**: Multiple methods with the same name but different parameters.
    
    Example:
```c#
	public class Calculator	{
	    public int Add(int a, int b) => a + b;
	    public double Add(double a, double b) => a + b;
	}
```

# 6. **Static vs. Instance Members**

- **Static**: Belongs to the class itself, not to instances.
    
- **Instance**: Belongs to a specific object instance.
    
    Example:
```c#
	public class Counter {
	    public static int Count = 0;  // Static variable
	    public int InstanceCount = 0; // Instance variable
	}
```

# 7. **Inheritance, Polymorphism, Abstraction, and Encapsulation**

- **Inheritance**: A class can inherit properties and methods from another class.
    
- **Polymorphism**: The ability to use a derived class object as if it were an instance of the base class.
    
- **Abstraction**: Hiding implementation details and exposing only essential functionality.
    
- **Encapsulation**: Bundling data and methods that operate on that data into a single unit (class).
    
    Example:
```c#
	public class Animal	{
	    public virtual void MakeSound() {
	        Console.WriteLine("Animal sound");
	    }
	}
	
	public class Dog : Animal {
	    public override void MakeSound() {
	        Console.WriteLine("Bark");
	    }
	}
	
	Animal animal = new Dog();
	animal.MakeSound();  // Outputs: Bark
```

# 8. **Interfaces vs. Abstract Classes**

- **Interface**: Defines a contract that classes must follow (no implementation).
    
- **Abstract Class**: Can have both abstract methods (no implementation) and concrete methods (with implementation).
    
    Example:
```c#
	public interface IWorkable {
	    void Work();
	}
	
	public class Employee : IWorkable {
	    public void Work() {
	        Console.WriteLine("Employee working");
	    }
	}
	
	public abstract class Shape	{
	    public abstract void Draw();  // Abstract method
	}
	
	public class Circle : Shape	{
	    public override void Draw() {
	        Console.WriteLine("Drawing Circle");
	    }
	}
```

### **Summary**
- **Classes and Objects**:
    
    - A class is a blueprint for creating objects.
        
    - Objects are instances of a class, containing data and behavior (methods).
        
    - Methods inside a class define the actions objects can perform (e.g., starting or driving a car).
        
- **Constructors and Destructors**:
    
    - Constructors initialize objects when they are created.
        
    - Destructors are used to clean up resources when an object is destroyed.
        
- **Access Modifiers**:
    
    - `public`: Accessible from anywhere.
        
    - `private`: Accessible only within the same class.
        
    - `protected`: Accessible within the class and its derived classes.
        
    - `internal`: Accessible within the same assembly.
        
- **Properties**:
    
    - Properties allow controlled access to private fields.
        
    - Auto-properties provide a shorthand way of defining properties with automatic backing fields.
        
- **Method Overloading**:
    
    - Multiple methods with the same name can exist, as long as their parameters differ.
        
    - Overloading makes methods flexible for different use cases (e.g., driving a car with or without specifying speed).
        
- **Static vs. Instance Members**:
    
    - Static members belong to the class itself and are shared across all instances.
        
    - Instance members belong to individual objects (instances).
        
- **Inheritance, Polymorphism, Abstraction, and Encapsulation**:
    
    - **Inheritance**: Allows a class to inherit properties and methods from another class.
        
    - **Polymorphism**: Allows objects of different types to be treated uniformly (e.g., overriding methods).
        
    - **Abstraction**: Hides complex details and shows only the necessary parts of an object.
        
    - **Encapsulation**: Bundles data and methods together while restricting direct access to certain parts.
        
- **Interfaces vs. Abstract Classes**:
    
    - **Interfaces**: Define a contract that classes must implement, without providing implementation.
        
    - **Abstract Classes**: Can provide partial implementation and must be inherited by other classes.