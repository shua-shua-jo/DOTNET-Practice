// try
// {
//     string path = "../Day3.md";
//     var arr = File.ReadLines(path);


// }
// catch (Exception err)
// {
//     Console.WriteLine(err);
//     throw;
// }
// catch (FileNotFoundException err)
// {
//     Console.WriteLine(err);
//     throw;
// }

int a = 13;
int b = a;

b = 5;

List<int> ab = new List<int>();
List<int> ba = ab;

ab.Add(7);
addElement(ref ab);

foreach (var l in ab)
{
    Console.WriteLine(l);
}

static void addElement(ref List<int> arr)
{
    arr.Add(10);
    arr = new List<int>();
}
// LOOK UP: ref and out
// LOOK UP: structs (memory)

var i = 10; //(implicit typing)

dynamic i = 10;
i = "string";

int j = 0;
object b = j;

// FIND EXAMPLES FOR GENERICS
// class Triangle<T> {
//     public T HeightNum {get; set;}
//     public T BaseNum {get; set;}

//     static void printTriangleInfo() {
//         Console.WriteLine($"{heightNum}");
//     }    

// }
// IEnumerable

// public delegate void Hello(string name);



// static void DoSomething()
// {
//     // Func<int, int> multiply = inp => inp * 2;
//     Func<int, int> multiply = MultiplyByTwo;

//     var ak = multiply(6);

// }

// static int MultiplyByTwo(int a)
// {
//     return a * 2;
// }