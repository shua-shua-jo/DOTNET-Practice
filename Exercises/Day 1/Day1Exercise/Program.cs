// Program that takes user input and prints a message

string? fname, lname;

Console.WriteLine("What's your first name?");
fname = Console.ReadLine();
Console.WriteLine("What's your last name?");
lname = Console.ReadLine();

Console.WriteLine($"Hello {fname} {lname}, congrats on completing Day 1 of the C#|.NET Learning Plan.");