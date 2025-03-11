using API.Middleware;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Scoped means that the service will be created once per request as long as HTTP request.
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//register the generic repository with the service container to be used in the application
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddCors();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

//cors added here to allow the angular app to access the api from a different origin 
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();//create a scope for the service provider to resolve the services in the scope
    var services = scope.ServiceProvider;//get the service provider
    var context = services.GetRequiredService<StoreContext>();//get the store context
    await context.Database.MigrateAsync();//migrate the database
    await StoreContextSeed.SeedAsync(context);//seed the database
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);//if there is an exception print the message
    throw;
}

app.Run();