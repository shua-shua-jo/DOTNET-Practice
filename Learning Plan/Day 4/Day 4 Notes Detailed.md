# **1. Working with Enumerables and Collections**

### **1.1 Core Concepts**

The .NET BCL provides extensive support for collections through:

- **System.Collections** (Legacy, non-generic)
    
- **System.Collections.Generic** (Modern, type-safe)
    
- **System.Collections.Concurrent** (Thread-safe)
    

**Key Interfaces:**

- `IEnumerable<T>` - Foundation for iteration
    
- `ICollection<T>` - Adds Count and modification
    
- `IList<T>` - Index-based access
    
- `IDictionary<TKey,TValue>` - Key-value mapping
    

## **1.2 Detailed Examples**

#### **List\<T\> - Dynamic Arrays**

```csharp
// Initialization
var numbers = new List<int> { 1, 2, 3 };

// Adding elements
numbers.Add(4);       // [1, 2, 3, 4]
numbers.Insert(1, 5); // [1, 5, 2, 3, 4]

// Capacity vs Count
numbers.Capacity = 10; // Pre-allocate memory
Console.WriteLine($"Count: {numbers.Count}, Capacity: {numbers.Capacity}");

// Performance consideration:
// - Add/Remove at end: O(1)
// - Insert/Remove middle: O(n)
```
#### **Dictionary<TKey,TValue> - Fast Lookups**

```csharp
var employeeAges = new Dictionary<string, int>
{
    ["John"] = 30,
    ["Sarah"] = 25
};

// Safe access
if (employeeAges.TryGetValue("John", out int age))
{
    Console.WriteLine($"John's age: {age}");
}

// Key considerations:
// - Keys must be unique
// - Default equality comparer uses GetHashCode()
// - Custom objects as keys require proper GetHashCode() implementation
```

## **1.3 Advanced Topics**

#### **Custom Equality Comparers**

```csharp
class CaseInsensitiveComparer : IEqualityComparer<string>
{
    public bool Equals(string x, string y) => 
        string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
    
    public int GetHashCode(string obj) => 
        obj.ToLowerInvariant().GetHashCode();
}

var caseInsensitiveDict = new Dictionary<string, int>(new CaseInsensitiveComparer());
```
### **Iterators and Yield Return**

```csharp
public static IEnumerable<int> GetFibonacci()
{
    int a = 0, b = 1;
    while (true)
    {
        yield return a;
        (a, b) = (b, a + b);
    }
}

// Usage (lazy evaluation)
foreach (var num in GetFibonacci().Take(10))
{
    Console.WriteLine(num);
}
```

# **2. Language Integrated Query (LINQ)**

## **2.1 Core Concepts**

LINQ provides a uniform way to query:

- In-memory collections (LINQ to Objects)
    
- Databases (LINQ to Entities)
    
- XML (LINQ to XML)
    

**Key Components:**

- Deferred execution
    
- Expression trees (for IQueryable)
    
- Standard Query Operators
    

## **2.2 Detailed Examples**

#### **Deferred Execution**

```csharp
var numbers = new List<int> { 1, 2, 3, 4 };
var query = numbers.Where(n => n > 2); // No execution yet

numbers.Add(5); // Affects the query!

foreach (var n in query) // Execution happens here
{
    Console.WriteLine(n); // Output: 3, 4, 5
}
```
#### **Expression Trees**

```csharp
// IQueryable builds expression trees
IQueryable<User> query = dbContext.Users
    .Where(u => u.Age > 18)
    .OrderBy(u => u.Name);

// The expression tree can be inspected
Console.WriteLine(query.Expression.ToString());
// Output shows how EF will translate to SQL
```

## **2.3 Performance Considerations**

#### **Materialization Strategies**

```csharp
// Bad: Multiple enumerations
var results = dbContext.Products.Where(p => p.Price > 100);
var count = results.Count(); // Executes query
var list = results.ToList(); // Executes again!

// Good: Single materialization
var materialized = results.ToList(); // One query
count = materialized.Count; // In-memory
```
#### **Select vs SelectMany**

```csharp
// Nested collections
var departments = new List<Department>
{
    new Department { Employees = new List<string> { "John", "Jane" } },
    new Department { Employees = new List<string> { "Bob" } }
};

// Select: List of lists
var empLists = departments.Select(d => d.Employees);

// SelectMany: Flattened list
var allEmployees = departments.SelectMany(d => d.Employees);
// ["John", "Jane", "Bob"]
```

# **3. Working with Files and Streams**

## **3.1 Stream Architecture**

```
Stream (Abstract)
├── FileStream
├── MemoryStream
├── NetworkStream
└── BufferedStream
```
## **3.2 Detailed Examples**

#### **Binary File Operations**

```csharp
// Writing binary data
using (var stream = new FileStream("data.bin", FileMode.Create))
{
    byte[] data = { 0x01, 0x02, 0x03 };
    stream.Write(data, 0, data.Length);
    
    // Seek to beginning
    stream.Seek(0, SeekOrigin.Begin);
    
    // Read back
    byte[] buffer = new byte[3];
    stream.Read(buffer, 0, 3);
}
```

#### **Async File Operations**

```csharp
async Task ProcessLargeFile()
{
    using var reader = new StreamReader("huge.log");
    while (!reader.EndOfStream)
    {
        var line = await reader.ReadLineAsync();
        // Process line
    }
}
```
## **3.3 Advanced Topics**

#### **Memory-Mapped Files**

```csharp
using (var mmf = MemoryMappedFile.CreateFromFile("large.dat"))
using (var accessor = mmf.CreateViewAccessor())
{
    // Read directly from memory
    int value = accessor.ReadInt32(0);
}
```

#### **File System Watching**

```csharp
var watcher = new FileSystemWatcher(@"C:\Temp");
watcher.NotifyFilter = NotifyFilters.LastWrite;

watcher.Changed += (s, e) => 
    Console.WriteLine($"Changed: {e.FullPath}");

watcher.EnableRaisingEvents = true;
```

# **4. ADO.NET Deep Dive**

## **4.1 Connection Management**

```csharp
// Best practice connection string
var builder = new SqlConnectionStringBuilder
{
    DataSource = "localhost",
    InitialCatalog = "Northwind",
    IntegratedSecurity = true,
    ConnectTimeout = 30,
    Pooling = true, // Connection pooling enabled
    MaxPoolSize = 100
};

using (var conn = new SqlConnection(builder.ConnectionString))
{
    conn.Open();
    // Execute commands
}
```

## **4.2 Transaction Handling**

```csharp
using (var conn = new SqlConnection(connString))
{
    conn.Open();
    using (var transaction = conn.BeginTransaction(
        IsolationLevel.ReadCommitted))
    {
        try
        {
            var cmd = conn.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = "INSERT INTO...";
            cmd.ExecuteNonQuery();
            
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
```

# **5. Entity Framework Advanced**

## **5.1 Change Tracking**

```csharp
// Disabling change tracking for read-only
var users = dbContext.Users
    .AsNoTracking()
    .Where(u => u.IsActive)
    .ToList();

// Explicit loading
var department = dbContext.Departments.Find(1);
dbContext.Entry(department)
    .Collection(d => d.Employees)
    .Load();
```

## **5.2 Raw SQL Queries**

```csharp
// FromSqlRaw with parameters
var users = dbContext.Users
    .FromSqlRaw(
        "SELECT * FROM Users WHERE Age > {0}",
        minAge)
    .ToList();

// Stored procedures
var result = dbContext.Database
    .ExecuteSqlRaw("EXEC UpdateStatistics @Year", 
        new SqlParameter("@Year", 2023));
```

## **6. HttpClient Best Practices**

### **6.1 Resilient HTTP Calls**

```csharp
// Using Polly for retries
var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
    .WaitAndRetryAsync(3, retryAttempt => 
        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

var response = await retryPolicy.ExecuteAsync(() => 
    httpClient.GetAsync("https://api.example.com/data"));
```

## **6.2 Efficient JSON Handling**

```csharp
// Using System.Text.Json
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    WriteIndented = true
};

// Stream directly from response
await using var stream = await response.Content.ReadAsStreamAsync();
var data = await JsonSerializer.DeserializeAsync<WeatherForecast[]>(stream, options);
```
## **Key Takeaways & Best Practices**

1. **Collections:**
    
    - Prefer generic collections for type safety
        
    - Choose data structure based on access patterns
        
    - Implement proper equality comparison for custom keys
        
2. **LINQ:**
    
    - Understand deferred vs immediate execution
        
    - Use `AsEnumerable()`/`AsQueryable()` appropriately
        
    - Avoid multiple enumerations of the same query
        
3. **File I/O:**
    
    - Always dispose streams
        
    - Use async for scalability
        
    - Consider memory-mapped files for large files
        
4. **Database Access:**
    
    - Parameterize all queries
        
    - Use connection pooling
        
    - Implement proper transaction isolation
        
5. **HTTP:**
    
    - Reuse HttpClient instances
        
    - Implement retry logic
        
    - Stream large responses instead of buffering