## Table of Contents

1. [Introduction to ASP.NET Core]
    
2. ASP.NET Core Fundamentals
    
3. MVC Pattern Overview
    
4. ASP.NET Core MVC
    
5. Controllers
    
6. Views
    
7. Models
    
8. Routing
    
9. Entity Framework Core
    
10. Dependency Injection
    
11. Middleware
    
12. Authentication & Authorization
    
13. Advanced Topics
    
14. Sample Projects
    

---

## Introduction to ASP.NET Core

### What is ASP.NET Core?

ASP.NET Core is a cross-platform, high-performance framework for building modern, cloud-based, internet-connected applications. It's a complete rewrite of ASP.NET that unites MVC and Web API into a single programming model.

**Key Features:**

- Cross-platform (Windows, Linux, macOS)
    
- Open-source
    
- High performance
    
- Unified MVC and Web API
    
- Built-in dependency injection
    
- Modular HTTP request pipeline
    
- Cloud-ready environment-based configuration
    
- Ships as NuGet packages
    

### ASP.NET Core vs ASP.NET

|Feature|ASP.NET Core|ASP.NET (Framework)|
|---|---|---|
|Cross-platform|Yes|No (Windows only)|
|Performance|Higher|Lower|
|Open-source|Yes|Mostly|
|Dependency Injection|Built-in|Requires libraries|
|Hosting|Self-hosted or IIS|IIS only|

---

## ASP.NET Core Fundamentals

### Project Structure

A typical ASP.NET Core project structure:

Copy

MyProject/
├── Properties/
│   └── launchSettings.json
├── wwwroot/            # Static files (CSS, JS, images)
├── Controllers/        # MVC Controllers
├── Views/              # Razor views
├── Models/             # Domain models
├── Services/           # Business logic services
├── appsettings.json    # Configuration
├── Program.cs          # Entry point and middleware config
└── Startup.cs          # (Older versions) Startup config

### Program.cs

The entry point of the application (since .NET 6):

csharp

Copy

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

---

## MVC Pattern Overview

### Model-View-Controller Architecture

MVC separates an application into three main components:

1. **Model**: Represents the data and business logic
    
2. **View**: Displays the data (UI)
    
3. **Controller**: Handles user input and interaction
    

**Flow:**

1. User makes a request
    
2. Routing directs request to appropriate controller
    
3. Controller processes request (may use models)
    
4. Controller selects a view to render
    
5. View renders the response
    

---

## ASP.NET Core MVC

### Creating a Controller

csharp

Copy

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(); // Renders Views/Home/Index.cshtml
    }
    
    public IActionResult About()
    {
        ViewData["Message"] = "Your application description page.";
        return View();
    }
}

### Creating a View

Views are typically Razor files (.cshtml) located in the Views folder.

**Views/Home/Index.cshtml:**

html

Copy

@{
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
</div>

Run HTML

### Creating a Model

csharp

Copy

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
}

---

## Controllers

### Controller Responsibilities

- Handle user requests
    
- Process input data
    
- Execute application logic
    
- Select appropriate views
    

### Action Results

Common return types from controller actions:

|Type|Purpose|
|---|---|
|ViewResult|Renders a view|
|PartialViewResult|Renders a partial view|
|JsonResult|Returns JSON data|
|ContentResult|Returns raw content|
|RedirectResult|Redirects to another URL|
|FileResult|Returns a file|
|StatusCodeResult|Returns HTTP status code|

### Example Controller with Various Actions

csharp

Copy

public class ProductsController : Controller
{
    private readonly IProductRepository _repository;
    
    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }
    
    // GET: /Products
    public IActionResult Index()
    {
        var products = _repository.GetAll();
        return View(products);
    }
    
    // GET: /Products/Details/5
    public IActionResult Details(int id)
    {
        var product = _repository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    
    // GET: /Products/Create
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: /Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _repository.Add(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
}

---

## Views

### Razor Syntax

Razor is a markup syntax for embedding server-based code into web pages.

**Basic Syntax:**

html

Copy

<!-- Single statement -->
<p>Current time: @DateTime.Now</p>

<!-- Multi-statement code block -->
@{
    var message = "Hello, World!";
    var currentDate = DateTime.Now;
}

<!-- Conditional statements -->
@if (DateTime.Now.Year == 2023)
{
    <p>It's 2023!</p>
}
else
{
    <p>It's not 2023</p>
}

<!-- Loops -->
<ul>
@foreach (var product in Model.Products)
{
    <li>@product.Name - @product.Price.ToString("C")</li>
}
</ul>

Run HTML

### Layouts

**Views/Shared/_Layout.cshtml:**

html

Copy

<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"] - My App</title>
    <link rel="stylesheet" href="~/css/site.css" />
</head>
<body>
    <header>
        <nav>
            <!-- Navigation menu -->
        </nav>
    </header>
    
    <main>
        @RenderBody()
    </main>
    
    <footer>
        <p>&copy; @DateTime.Now.Year - My App</p>
    </footer>
    
    <script src="~/js/site.js"></script>
    @RenderSection("Scripts", required: false)
</body>
</html>

Run HTML

### Partial Views

Reusable components that can be embedded in other views.

**Views/Shared/_ProductCard.cshtml:**

html

Copy

@model Product

<div class="product-card">
    <h3>@Model.Name</h3>
    <p>Price: @Model.Price.ToString("C")</p>
    <a asp-action="Details" asp-route-id="@Model.Id">Details</a>
</div>

Run HTML

**Using a partial view:**

html

Copy

@foreach (var product in Model)
{
    <partial name="_ProductCard" model="product" />
}

Run HTML

---

## Models

### Model Binding

ASP.NET Core MVC automatically maps request data to action method parameters.

csharp

Copy

// GET: /Products/Search?name=book
public IActionResult Search(string name)
{
    var results = _repository.SearchByName(name);
    return View(results);
}

### Model Validation

csharp

Copy

public class Product
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Range(0.01, 10000)]
    public decimal Price { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }
}

**In controller:**

csharp

Copy

[HttpPost]
public IActionResult Create(Product product)
{
    if (ModelState.IsValid)
    {
        // Save to database
        return RedirectToAction("Index");
    }
    return View(product);
}

**In view:**

html

Copy

<form asp-action="Create">
    <div class="form-group">
        <label asp-for="Name" class="control-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>
    <!-- Other fields -->
    <input type="submit" value="Create" class="btn btn-primary" />
</form>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}

Run HTML

---

## Routing

### Convention-based Routing

Configured in Program.cs:

csharp

Copy

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

### Attribute Routing

csharp

Copy

[Route("products")]
public class ProductsController : Controller
{
    [HttpGet("")]
    public IActionResult Index() { /* ... */ }
    
    [HttpGet("{id:int}")]
    public IActionResult Details(int id) { /* ... */ }
    
    [HttpGet("search/{name}")]
    public IActionResult Search(string name) { /* ... */ }
}

### Route Constraints

csharp

Copy

[Route("users/{id:int}")]
public IActionResult GetUserById(int id) { /* ... */ }

[Route("users/{name:alpha}")]
public IActionResult GetUserByName(string name) { /* ... */ }

---

## Entity Framework Core

### Setting Up DbContext

csharp

Copy

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}

**Register in Program.cs:**

csharp

Copy

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

### Repository Pattern Example

csharp

Copy

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Product> GetAll()
    {
        return _context.Products.ToList();
    }
    
    public Product GetById(int id)
    {
        return _context.Products.Find(id);
    }
    
    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
    
    // Other methods...
}

---

## Dependency Injection

### Registering Services

csharp

Copy

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();

### Constructor Injection

csharp

Copy

public class ProductsController : Controller
{
    private readonly IProductRepository _repository;
    
    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }
    
    // Action methods...
}

### View Injection

html

Copy

@inject IConfiguration Configuration

<p>API URL: @Configuration["ApiUrl"]</p>

Run HTML

---

## Middleware

### Built-in Middleware

csharp

Copy

app.UseHttpsRedirection();       // Redirect HTTP to HTTPS
app.UseStaticFiles();           // Serve static files
app.UseRouting();               // Route requests
app.UseAuthentication();        // Enable authentication
app.UseAuthorization();         // Enable authorization
app.UseEndpoints();             // Execute endpoints

### Custom Middleware

csharp

Copy

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    
    public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
    }
    
    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
        await _next(context);
        _logger.LogInformation($"Response: {context.Response.StatusCode}");
    }
}

**Register middleware:**

csharp

Copy

app.UseMiddleware<RequestLoggingMiddleware>();

---

## Authentication & Authorization

### Identity Setup

csharp

Copy

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

### Authorize Attribute

csharp

Copy

[Authorize]
public class AccountController : Controller
{
    [Authorize(Roles = "Admin")]
    public IActionResult AdminPanel()
    {
        return View();
    }
    
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
}

### Login Action

csharp

Copy

[HttpPost]
[AllowAnonymous]
public async Task<IActionResult> Login(LoginViewModel model)
{
    if (ModelState.IsValid)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    }
    return View(model);
}

---

## Advanced Topics

### API Controllers

csharp

Copy

[ApiController]
[Route("api/[controller]")]
public class ProductsApiController : ControllerBase
{
    private readonly IProductRepository _repository;
    
    public ProductsApiController(IProductRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        return _repository.GetAll().ToList();
    }
    
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = _repository.GetById(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }
}

### SignalR

Real-time functionality:

csharp

Copy

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}

**Client-side JavaScript:**

javascript

Copy

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", (user, message) => {
    // Handle received message
});

connection.start().catch(err => console.error(err.toString()));

---

## Sample Projects

### 1. Basic Product Management System

A simple CRUD application for managing products.

**Features:**

- List all products
    
- Create new products
    
- Edit existing products
    
- Delete products
    
- Basic validation
    

### 2. Blog Engine

A more complex application with multiple models and relationships.

**Features:**

- Blog posts with categories and tags
    
- User comments
    
- Authentication
    
- Admin area
    
- File uploads for images
    

### 3. E-Commerce API

A RESTful API for an e-commerce platform.

**Features:**

- Product catalog
    
- Shopping cart
    
- User authentication (JWT)
    
- Order processing
    
- Integration with payment gateway