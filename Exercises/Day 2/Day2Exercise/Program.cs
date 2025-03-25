
Person person = new Person("Joshua", 23);

person.DisplayInfo();

Employee employee = new Employee(person.Name, person.Age, "Software Engineer");

employee.DisplayEmployeeInfo();

Worker worker = new Worker("Joshua");
worker.Work();

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}");
    }
}

class Employee : Person
{
    public string JobTitle { get; set; }

    public Employee(string name, int age, string jobTitle) : base(name, age)
    {
        JobTitle = jobTitle;
    }

    public void DisplayEmployeeInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Job Title: {JobTitle}");
    }
}

interface IWorkable
{
    void Work();
}

// Class implementing IWorkable interface
class Worker : IWorkable
{
    public string Name { get; set; }

    public Worker(string name)
    {
        Name = name;
    }

    // Implement the Work method
    public void Work()
    {
        Console.WriteLine($"{Name} is working.");
    }
}