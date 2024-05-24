using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestForJobProject.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Usamos AddControllersWithViews para habilitar el soporte para vistas MVC.

// DbContext for connection for SQL
builder.Services.AddTransient<Seed>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configuration to run the Seed Data
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}"); // Configuramos la ruta predeterminada para el enrutamiento de MVC.

// Configure routing for the home page
app.MapGet("/", (Func<Task<IActionResult>>)(() =>
{
    return Task.FromResult<IActionResult>(new RedirectResult("~/Employee/Index"));
}));

app.Run();
