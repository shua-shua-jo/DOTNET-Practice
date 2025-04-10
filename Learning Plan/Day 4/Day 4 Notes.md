# 1. Working with Enumerables and Collections

### **System.Collections and System.Collections.Generic**

- **System.Collections**: Contains non-generic collection types such as:
    
    - `ArrayList`
        
    - `Hashtable`
        
    - `Queue`
        
    - `Stack`
        
- **System.Collections.Generic**: Provides type-safe and more performant collection types such as:
    
    - `List<T>`
        
    - `Dictionary<TKey, TValue>`
        
    - `Queue<T>`
        
    - `Stack<T>`
        
    - `HashSet<T>`
        
    - `LinkedList<T>`
        
- **IEnumerable**\*\* and **IEnumerator**\*\*:
    
    - `IEnumerable<T>` allows iteration over a collection using `foreach`.
        
    - `IEnumerator<T>` provides methods for iterating through a collection manually.
        

# 2. Language Integrated Query (LINQ)

### **Introduction to LINQ**

- LINQ provides a consistent, readable, and type-safe way to query data from different sources.
    
- Supports querying objects, databases, XML, and more.
    

### **Query Syntax vs. Extension Methods**

#### Query Syntax (SQL-like)

```csharp
var results = from user in users
              where user.Age > 18
              select user;
```

#### Method Syntax (Fluent API)

```csharp
var results = users.Where(user => user.Age > 18).Select(user => user);
```

### **IQueryable vs. IEnumerable**

- **IEnumerable**: Used for in-memory collections, executes queries immediately.
    
- **IQueryable**: Used for database queries (e.g., Entity Framework), enables deferred execution and optimized SQL generation.
    

### **Deferred Execution**

- LINQ queries are executed when they are iterated over, not when they are declared.
    
- Methods like `ToList()`, `FirstOrDefault()`, or `Count()` trigger execution immediately.
    

# 3. Working with Files and Streams with `System.IO`

### **Stream Base Class**

- `Stream` is an abstract class used for reading and writing data.
    
- Common derived classes:
    
    - `FileStream`
        
    - `MemoryStream`
        
    - `NetworkStream`
        
    - `BufferedStream`
        

### **FileStream**

- Allows reading from and writing to files.
    
- Example:
    

```csharp
using (FileStream fs = new FileStream("example.txt", FileMode.OpenOrCreate))
{
    byte[] data = Encoding.UTF8.GetBytes("Hello, World!");
    fs.Write(data, 0, data.Length);
}
```

### **MemoryStream**

- Used for temporary in-memory storage of data.
    
- Example:
    

```csharp
using (MemoryStream ms = new MemoryStream())
{
    byte[] data = Encoding.UTF8.GetBytes("Hello, World!");
    ms.Write(data, 0, data.Length);
}
```

# 4. ADO.NET and the `System.Data` Namespace

### **Connecting to a Database**

- ADO.NET provides database connectivity for relational databases such as SQL Server, MySQL, and PostgreSQL.
    
- Uses `SqlConnection`, `SqlCommand`, and `SqlDataReader`.
    
- Example:
    

```csharp
using (SqlConnection conn = new SqlConnection("your_connection_string"))
{
    conn.Open();
    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn))
    {
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine(reader["Username"].ToString());
                
            }
        }
    }
}
```

System.Data.Common:
IDbConnection
IDbCommand
IDbDataReader
# 5. Entity Framework (EF)

### **Introduction to Entity Framework**

- ORM (Object-Relational Mapping) framework for .NET applications.
    
- Provides an abstraction over ADO.NET for database access.
    

### **Basic Example using EF Core**

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
}
```

### **Deferred Execution in EF**

- EF uses deferred execution when working with LINQ queries.
    
- Queries are only executed when needed.
    

# 6. Connecting to HTTP Services using `HttpClient`

### **Making HTTP Requests**

- `HttpClient` is used for sending HTTP requests and receiving responses.
    

#### **GET Request Example**

```csharp
using (HttpClient client = new HttpClient())
{
    HttpResponseMessage response = await client.GetAsync("https://api.example.com/data");
    if (response.IsSuccessStatusCode)
    {
        string data = await response.Content.ReadAsStringAsync();
        Console.WriteLine(data);
    }
}
```

#### **POST Request Example**

```csharp
using (HttpClient client = new HttpClient())
{
    var content = new StringContent("{\"name\":\"John\"}", Encoding.UTF8, "application/json");
    HttpResponseMessage response = await client.PostAsync("https://api.example.com/users", content);
}
```

---