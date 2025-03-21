
Console.WriteLine("What is your first name?");
var fname = Console.ReadLine();
Console.WriteLine("What is your middle name?");
var mname = Console.ReadLine();
Console.WriteLine("What is your last name?");
var lname = Console.ReadLine();

var p1 = new Person() {FirstName = fname ?? "N/A", MiddleName = mname ?? "N/A", LastName = lname ?? "N/A"};

var currentDate = DateTime.Now;
Console.WriteLine($"{Environment.NewLine}Hello, {p1.FirstName} {p1.MiddleName} {p1.LastName}, on {currentDate:d} at {currentDate:t}!");
Console.Write($"{Environment.NewLine}Press Enter to exit...");
Console.Read();

public class Person
{
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public required string FirstName { get; set; }
}